/*
 * Copyright © 2005-2007 Danilo Mendez <danilo.mendez@kontac.net>
 * forum: http://www.kontac.net/forum.aspx
 * www.kontac.net 
 * All rights reserved.
 * Released under both BSD license and Lesser GPL library license.
 * Whenever there is any discrepancy between the two licenses,
 * the BSD license will take precedence.
 */

namespace SmartCode.Template
{
    using System;
    using System.Collections;
    using SmartCode.Model;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Collections.Specialized;
    using System.Collections.Generic;
    using System.Reflection;
    using System.IO;
    using System.Configuration;

    /// <summary>
    /// base class for al Templates
    /// </summary>
    public abstract class TemplateBase : SmartCode.Template.ITemplateBase 
    {
        /// <summary>
        /// The name of the template. For example: "Update Row By Primary Key".
        /// </summary>
        private string name;

        /// <summary>
        /// The description of what the template does. For example: "Generates a stored procedure to update a row by its primary key".
        /// </summary>
        private string description;

        /// <summary>
        ///The relative path of the folder in which to place the file with the generated code
        ///before generating code SmartCode asks in which folder to place the generated files. 
        ///For example: "Stored Procedures\\UpdateByPK".
        /// </summary>
        private string outputFolder;

        /// <summary>
        /// This variable hold the output code generated
        /// </summary>
        private StringBuilder code;

        /// <summary>
        /// If the template run and not create any output file
        /// </summary>
        private bool createFile;

        /// <summary>
        /// Is project level template, it run once for the project, and not for entities assigned.
        /// </summary>
        private bool isProjectTemplate;

        /// <summary>
        /// 
        /// </summary>
        private NamedObject entity;

        /// <summary>
        /// 
        /// </summary>
        private Domain domain;

        private static string templatesBaseDirectory;

       
        static TemplateBase()
        {
            templatesBaseDirectory = ConfigurationSettings.AppSettings["TemplatesBaseDirectory"];
        }

        protected TemplateBase()
        {
            name = "Template name";
            description = "Template description";
            outputFolder = "Relative folder, for output file";
            createFile = true;
            isProjectTemplate = false;
            code = new StringBuilder();
        }

        public ArrayList Run(Domain currentDomain, NamedObject currentEntity)
        {
            domain = currentDomain;
            entity = currentEntity;
            ProduceCode();

            ArrayList results = new ArrayList(4);
            results.Add(code);
            results.Add(OutputFileName());
            results.Add(outputFolder);
            results.Add(createFile.ToString());

            return results;
        }

        /// <summary>
        /// The current name of the file in which to store the generated code
        /// </summary>
        /// <returns></returns>
        public abstract string OutputFileName();

       /// <summary>
       ///  The routine that generates the code and places it in the internal System.String property "code". 
       /// To generate the code, this method uses the internal properties "Table" and "Domain", 
       /// which SmartCode will set prior to invoking it. 
       /// Table and Domain are properties of types Table and Domain, respectively
       /// </summary>
        public abstract void ProduceCode();

        #region Write Methods
        protected virtual void W()
        {
            WriteLine("");
        }

        protected virtual void W(string strValue)
        {
            WriteLine(strValue);
        }

        protected virtual void W(string strValue, params object[] args)
        {
            WriteLine(string.Format(strValue, args));
        }

        protected virtual void Write(String value)
        {
            code.Append(value);
        }

        protected virtual void WriteLine()
        {
            WriteLine("");
        }

        protected virtual void WriteLine(string strValue)
        {
            code.Append(strValue + System.Environment.NewLine);
        }

        protected virtual void WriteLine(string strValue, params object[] args)
        {
            WriteLine(string.Format(strValue, args));
        }

        protected virtual void WriteLine1(string strValue)
        {
            code.Append("\t" + strValue + System.Environment.NewLine);
        }

        protected virtual void WriteLine1(string strValue, params object[] args)
        {
            WriteLine("\t" + string.Format(strValue, args));
        }


        protected virtual void WriteLine2(string strValue)
        {
            code.Append("\t\t" + strValue + System.Environment.NewLine);
        }

        protected virtual void WriteLine2(string strValue, params object[] args)
        {
            WriteLine("\t\t" + string.Format(strValue, args));
        }


        protected virtual void WriteLine3(string strValue)
        {
            code.Append("\t\t\t" + strValue + System.Environment.NewLine);
        }

        protected virtual void WriteLine3(string strValue, params object[] args)
        {
            WriteLine("\t\t\t" + string.Format(strValue, args));
        }

        protected virtual void WriteLine4(string strValue)
        {
            code.Append("\t\t\t\t" + strValue + System.Environment.NewLine);
        }

        protected virtual void WriteLine4(string strValue, params object[] args)
        {
            WriteLine("\t\t\t\t" + string.Format(strValue, args));
        }

        protected virtual void WriteLineI(int i, string strValue)
        {
            string s = "";
            for (int j = 0; j < i; j++)
            {
                s += "\t";
            }
 
            code.Append(s + strValue + System.Environment.NewLine);
        }

        protected virtual void WriteLineI(int i, string strValue, params object[] args)
        {
            string s = "";
            for (int j = 0; j < i; j++)
            {
                s += "\t";
            }

            WriteLine(s + string.Format(strValue, args));
        }

        protected string NewLine
        {
            get
            {
                return Environment.NewLine;
            }
        }

        #endregion

        public virtual bool CreateOutputFile
        {
            get{return createFile;}
            set{createFile = value;}
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public bool IsProjectTemplate
        {
            get { return isProjectTemplate; }
            set { isProjectTemplate = value; }
        }
  
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        
        public virtual string OutputFolder
        {
            get { return outputFolder; }
            set { outputFolder = value; }
        }

        public virtual string Code
        {
            get{return code.ToString();}
        }

        protected NamedObject Entity
        {
            get { return entity; }
        }

        protected Domain Domain
        {
            get { return domain; }
        }

        protected TableSchema Table
        {
            get{return entity as TableSchema;}
        }

        public string OutputFullPath
        {
            get { return Path.GetFullPath(Path.Combine(OutputFolder, OutputFileName())); }
        }

        public static string TemplatesBaseDirectory
        {
            get { return TemplateBase.templatesBaseDirectory ; }
        }

        #region StringInterpolation

        /// <summary> Format: (Basic logic taken from a CodeProject artical by Bevan Arp).
        /// <p>Handles substitution in Template strings three different ways:</p>
        /// <p>1.  Using String.Format with placeholders of the form {n} or 
        /// {n:fmtspec} to insert zero or more Objects into the text of strSource.</p>
        /// <p>2.  Using placeholders of the form #{name} or #{name:fmtspec} to
        /// insert named Properties from zero or more Reference Objects into the text.</p>
        /// <p>3.  Using placeholders of the same form as (2.) above, but with Objects
        /// that are either StringDictionary or Dictionary<string,string> to provide
        /// named replacement values.</p>
        /// <p>Note: All methods use Regex.Unescape on strSource to substitute 
        /// for escape values such as \n for NewLine, \f for FormFeed, etc.</p>
        /// <p></p><p>Note: The reason for the (#) in front of the curly braces in methods
        /// 2 and 3 is to make it easier to include curly brace sections in a 
        /// Template (c# for example) without them being interpreted as placeholders.
        /// Also, that's the format used by Ruby -- might as well jump on the BandWagon ;-)</p>
        /// 
        ///</summary>
        public string Format(string strSource, params Object[] Objects)
        {
            if ((Objects == null) || (Objects.Length == 0)) return strSource;

            try
            {
                strSource = Regex.Unescape(strSource);
            }
            catch
            {
                //ignore any unescape error
            }

            //See if caller is expecting the original String.Format logic
            int cx = strSource.IndexOf("{");
            if ((cx >= 0) && ((cx + 1) < strSource.Length) && (Char.IsDigit(strSource[cx + 1])))
            {
                try
                {
                    return String.Format(strSource, Objects);

                }
                catch
                {
                    throw new Exception(string.Format("Error while trying to Format the following string: {0}   {1}",
                        Environment.NewLine, FirstPartOf(strSource, 80)));
                }
            }

            //Look for placeholders of the form #{name}
            MatchCollection mc = RegexMain.Matches(strSource);
            StringBuilder result = new StringBuilder(strSource);  //copy strSource into result

            // Loop over each match, replacing the placeholders with the result
            foreach (Match m in mc)
            {
                // Get the details of this match
                string placeholder = m.Groups["placeholder"].Value;
                string id = m.Groups["id"].Value;
                string spec = m.Groups["spec"].Value;
                Object newValue;
                foreach (Object obj in Objects)
                {
                    newValue = this.ExtractPropertyValue(obj, id);
                    if (newValue != null)   //found a match
                    {
                        string replacement = string.Format("{0" + spec + "}", newValue);
                        result.Replace(placeholder, replacement);
                        break;   //this match handled, go to the next m in mc
                    }
                }
            }
            return result.ToString();
        }

        // This regular expression will find placeholders of the form #{id} or #{id:formatspec} 
        //    defined as a class field so it will be constructed and compiled once, not every time Format is called.
        private Regex RegexMain = new Regex(@"(?<placeholder>\#\{(?<id>[^}.]+(\.[^}.]+)*)(?<spec>\:[^}]+)?\})");
        //built and verified with Expresso              )


        public String FirstPartOf(string srcString, int maxLen)
        {
            if (srcString.Length <= maxLen)
                return srcString;
            else
                return srcString.Substring(0, maxLen);
        }


        private Object ExtractPropertyValue(Object Source, string PropertyPath)
        {
            //Handle lookups in a StringDictionary, returns null if no hit
            if (Source is StringDictionary)
            {
                return ((StringDictionary)Source)[PropertyPath];
            }

            //Handle lookups in a Dictionary<string,string>, returns null if no hit
            if (Source is Dictionary<string, string>)
            {
                try
                {
                    return ((Dictionary<string, string>)Source)[PropertyPath];
                }
                catch
                {
                    return null;
                }
            }

            //Handle property access by Reflection into Source
            if (PropertyPath.Contains("."))  //handle dotted notation for referenced objects
            {
                // Name references a sub property
                int index = PropertyPath.IndexOf(".");
                string subObjectName = PropertyPath.Substring(0, index);
                Object subObject = ExtractPropertyValue(Source, subObjectName);
                return ExtractPropertyValue(subObject, PropertyPath.Substring(index + 1));
            }

            // PropertyPath does not contain a "."
            // We are looking for something directly on this object

            Type t = Source.GetType();
            PropertyInfo p = t.GetProperty(PropertyPath);
            if (p != null)
            {
                // We found a Public Property, return it's value
                return p.GetValue(Source, null);
            }

            return null;   //ignore, if not found
            //throw new InvalidOperationException("Did not find value " + PropertyPath + " on " + Source.ToString());
        }

        #endregion

        #region MiscHelperMethods

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Returns a string from a text file that was compiled as an embedded resource.
        /// Good for storing Template strings, etc.  They're easier to maintain in separate
        /// files, rather directly as strings in the code.  Usually, such a file will
        /// contain many lines which are handled as a single string.
        /// </summary>
        /// <param name="pFileName">Some unique part of the resource string filename
        /// (usually the filename portion of the fully qualified path).  
        /// The comparison is NOT case sensitive.
        /// </param>
        /// <param name="pAssembly">The assembly that contains the resource string -- 
        /// usually the calling assembly which may be obtained by the caller as follows:
        /// this.GetType().Assembly (VB: Me.GetType.Assembly).</param>
        /// <returns>The entire file as a string.</returns>
        /// <remarks>Add a text file to your project.  Change its properties to "Build as
        /// an Embedded Resource file".  That's all there is to it.
        /// </remarks>
        /// <history>
        /// 	[kb]	7/8/2004	Created
        /// </history>
        /// -----------------------------------------------------------------------------
        public string GetResourceString(string pFileName, System.Reflection.Assembly pAssembly)
        {
            string[] resnames;
            Stream stream = null;
            StreamReader reader = null;
            pFileName = pFileName.ToUpper();
            try
            {
                resnames = pAssembly.GetManifestResourceNames();
                for (int rx = 0; rx <= resnames.Length - 1; rx++)
                {
                    if (resnames[rx].ToUpper().IndexOf(pFileName) >= 0)
                    {
                        stream = pAssembly.GetManifestResourceStream(resnames[rx]);
                        if (stream == null)
                        {
                            throw new Exception("Couldn't find a resource string matching [" + pFileName + "].");
                        }
                        try
                        {
                            reader = new StreamReader(stream);
                            return reader.ReadToEnd();
                        }
                        finally
                        {
                            if (reader != null) reader.Close();
                            if (stream != null) stream.Close();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("There was a problem getting the resource string [" + pFileName + "].");
            }
            return string.Empty;
        }


        /// <summary>
        /// Creates a Dictionary<string,string> instance and populates it by
        /// calling AddTemplateSectionsToDictionary.
        /// </summary>
        public Dictionary<string, string> GetDictionaryOfTemplateSections(string strSource)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            AddTemplateSectionsToDictionary(strSource, dict);
            return dict;
        }

        /// <summary>
        /// Loads the contents of a specially formatted Template string into a dictionary for use
        /// in generating files with SmartCodeGenerator.  The Template string may have one or more
        /// multi-line sections.  Each section is headed by a line containing [SECTION: section-name]
        /// starting in column 1, and is ended by a line containing only [END] starting
        /// in column 1.  The lines between the section header and [END] are entered into the
        /// dictionary as a single named string.
        /// 
        /// Lines between sections are ignored and may be used for comments.
        /// </summary>
        public void AddTemplateSectionsToDictionary(string strSource, Dictionary<string, string> pDict)
        {
            MatchCollection matchCol = RegexGetTemplateSectionsFrom.Matches(strSource);

            // Loop over each match, storing the results in the dictionary
            foreach (Match match in matchCol)
            {
                // Get the details of this match
                string id = match.Groups["id"].Value;
                string body = match.Groups["body"].Value;
                pDict[id] = body;
            }
        }
        private Regex RegexGetTemplateSectionsFrom =
            new Regex(@"(?<section>^\[SECTION: *(?<id>[^\]]+)\]\s*$(?<body>(.(?!^\[END\]))*))",
                RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnoreCase);
        //built and verified with Expresso

        #endregion
    }
}

