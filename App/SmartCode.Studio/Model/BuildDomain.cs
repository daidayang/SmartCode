/*
 * Copyright © 2005-2007 Danilo Mendez <danilo.mendez@kontac.net>
 * forum: http://www.kontac.net/forum.aspx
 * www.kontac.net 
 * All rights reserved.
 * Released under both BSD license and Lesser GPL library license.
 * Whenever there is any discrepancy between the two licenses,
 * the BSD license will take precedence.
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using SmartCode.Studio.Database;
using SmartCode.Model;
using SmartCode.Studio.Database.Info;

namespace SmartCode.Studio.Model
{
    public class BuildDomain
    {
        private BuildDomainDlg sender = null;
        private Delegate senderDelegate = null;

        private Domain domain;

        public Domain Domain
        {
            get { return domain; }
        }

        private BuildDomain()
        {
        }

        public BuildDomain(BuildDomainDlg sender, Delegate senderDelegate)
        {
            this.sender = sender;
            this.senderDelegate = senderDelegate;
        }

        /// <summary>
        /// Method for ThreadStart delegate
        /// </summary>
        public void RunProcess()
        {
            Thread.CurrentThread.IsBackground = true; //make them a daemon
            LocalRunProcess();
        }

        /// <summary>
        /// Local Method for the actual work.
        /// </summary>
        private void LocalRunProcess()
        {
            try
            {
                domain = new Domain(sender.Driver.DatabaseSchema);

                int totalOfMessages = sender.Tables.Length + sender.Views.Length;
                int i = 0;
                for (; i < sender.Tables.Length; i++)
                {
                    string tableName = sender.Tables[i];
                    string message = "Loading objects for " + tableName;
                    LoadEntityInfo(tableName, true);

                    sender.BeginInvoke(senderDelegate, new object[] { totalOfMessages, i, message, false });
                }

                int j = 0;
                for (; j < sender.Views.Length; j++)
                {
                    string message = "Loading objects for " + sender.Views[j];
                    LoadEntityInfo(sender.Views[j], false);
                    sender.BeginInvoke(senderDelegate, new object[] { totalOfMessages, i + j, message, false });
                }
                sender.BeginInvoke(senderDelegate, new object[] { totalOfMessages, 0, "Loading Constraints", false });

                totalOfMessages = sender.Tables.Length;
                i = 0;
                for (; i < sender.Tables.Length; i++)
                {
                    string tableName = sender.Tables[i];
                    string message = "Creating references for " + tableName;

                    LoadConstraints(tableName);
                    sender.BeginInvoke(senderDelegate, new object[] { totalOfMessages, i, message, false });
                }


                sender.BeginInvoke(senderDelegate, new object[] { totalOfMessages, i, "Domain created successful", true });
                sender.DialogResult = DialogResult.OK;
                sender.Close();

            }
            catch  
            {
                Thread.CurrentThread.Abort();
                sender.DialogResult = DialogResult.Cancel;
                throw;

            }
            finally
            {
            }
        }

        private void LoadConstraints(string tableName)
        {
            TableSchema childTable = this.domain.DatabaseSchema.FindTable(tableName);
            if (childTable != null)
            {
                SchemaExtractor extractor = sender.Driver.Extractor;

                ConstraintInfo[] constraintsInfo = extractor.GetConstraints(tableName);
                foreach (ConstraintInfo constraint in constraintsInfo)
                {
                    TableSchema parentTable = this.domain.DatabaseSchema.FindTable(constraint.PrimaryKeyTable);
                    if (parentTable != null)
                    {
                        // ConstraintInfo.PrimaryKeyTableColumns and ConstraintInfo.Columns are RelatedImageListAttribute 1-1
                        ReferenceSchema reference = new ReferenceSchema(constraint.Name, parentTable, childTable);
                        reference.OnDeleteCascade = constraint.OnDeleteCascade;
                        reference.OnUpdateCascade = constraint.OnUpdateCascade;

                        for (int i = 0; i < constraint.PrimaryKeyTableColumns.Length; i++)
                        {
                            ColumnSchema parentColumn = parentTable.FindColumn(constraint.PrimaryKeyTableColumns[i]);
                            ColumnSchema childColumn = childTable.FindColumn(constraint.Columns[i]);
                            reference.AddNewJoin(parentColumn, childColumn);
                        }
                        parentTable.AddOutReference(reference);
                        childTable.AddInReference(reference);
                    }
                }
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="isTable"></param>
        private void LoadEntityInfo(string tableName, bool isTable)
        {
            try
            {
                TableSchema table = new TableSchema(this.domain, tableName);
                table.IsTable = isTable;

                ColumnInfo[] columnsInfo = sender.Driver.Extractor.GetColumns(tableName);
                foreach (ColumnInfo columnInfo in columnsInfo)
                {
                    ColumnSchema column = new ColumnSchema(columnInfo.Name, table);
                    column.SqlType = columnInfo.SqlType;

                    column.NetDataType = columnInfo.NetDataType;
                    column.Length = columnInfo.Size;
                    column.OriginalSQLType = columnInfo.OriginalSQLType;
                    column.IsIdentity = columnInfo.AutoIncrement;
                    column.IsRequired = !columnInfo.AllowNull;
                    column.Precision = columnInfo.Precision; //[Added by Fredy Muñoz] The value for the precision wasn't set at all.
                    column.Scale = columnInfo.Scale;
                    column.DefaultValue = columnInfo.DefaultValue;
                    table.AddColumn(column);
                }

                this.domain.DatabaseSchema.AddTable(table);

                KeyInfo[] keysInfo = sender.Driver.Extractor.GetKeys(tableName);
                foreach (KeyInfo keyInfo in keysInfo)
                {
                    //Bug Fix
                    if (keyInfo.TableName == table.Name)
                    {
                        ColumnSchema keyColumn = table.FindColumn(keyInfo.ColumnName);
                        if (keyColumn != null)
                        {
                            keyColumn.IsPrimaryKey = true;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
