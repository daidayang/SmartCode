using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Northwind.Core.DataInterfaces;
using Northwind.Data;
using Northwind.Core.Domain;

namespace Northwind.Web
{
    public partial class EmployeeTerritoryEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                fillComboBoxes();
                fillForm();
            }
        }


        private void fillComboBoxes()
        {
            IDaoFactory daoFactory = new NHibernateDaoFactory();
            uiEmployeeID.Items.Add("");
            uiEmployeeID.DataSource = daoFactory.GetEmployeeDao().GetAll();
            uiEmployeeID.DataValueField = "ID";
            uiEmployeeID.DataTextField  = "LastName";
            uiEmployeeID.DataBind();

            uiTerritoryID.Items.Add("");
            uiTerritoryID.DataSource = daoFactory.GetTerritoryDao().GetAll();
            uiTerritoryID.DataValueField = "ID";
            uiTerritoryID.DataTextField  = "TerritoryDescription";
            uiTerritoryID.DataBind();

        }

        private void fillForm()
        {
            if ( Request.QueryString["EmployeeID"] != null  && Request.QueryString["TerritoryID"] != null  )
            {
             uiEmployeeID.SelectedValue = Request.QueryString["EmployeeID"];
             uiTerritoryID.SelectedValue = Request.QueryString["TerritoryID"];
             EmployeeTerritory.DomainObjectID ID = new EmployeeTerritory.DomainObjectID(Convert.ToInt32(uiEmployeeID.SelectedValue), Convert.ToString(uiTerritoryID.SelectedValue));

                IDaoFactory daoFactory = new NHibernateDaoFactory();
                IEmployeeTerritoryDao dao = daoFactory.GetEmployeeTerritoryDao();
                EmployeeTerritory entity = dao.GetById(ID, false );

                uiIsNew.Checked = false;
            }
        }

        protected void Update(object sender, System.EventArgs e)
        {
            EmployeeTerritory entity = null;

            IDaoFactory daoFactory = new NHibernateDaoFactory();
            IEmployeeTerritoryDao dao = daoFactory.GetEmployeeTerritoryDao();

         EmployeeTerritory.DomainObjectID ID = new EmployeeTerritory.DomainObjectID(Convert.ToInt32(uiEmployeeID.SelectedValue), Convert.ToString(uiTerritoryID.SelectedValue));
            if (! uiIsNew.Checked )
            {
                entity = dao.GetById(ID, false );
                }
                else
                {
                    entity = new EmployeeTerritory(ID);
                }


            if (uiIsNew.Checked)
            {
                dao.Save(entity);
            }
            else
            {
                dao.SaveOrUpdate(entity);
            }
            uiIsNew.Checked = false;
        }

        protected void Delete(object sender, System.EventArgs e)
        {
            if (! uiIsNew.Checked)
            {
                EmployeeTerritory.DomainObjectID ID = new EmployeeTerritory.DomainObjectID(Convert.ToInt32(uiEmployeeID.SelectedValue), Convert.ToString(uiTerritoryID.SelectedValue));
                IDaoFactory daoFactory = new NHibernateDaoFactory();
                IEmployeeTerritoryDao dao = daoFactory.GetEmployeeTerritoryDao();
                EmployeeTerritory entity = dao.GetById(ID, false );

                dao.Delete(entity);

                Response.Redirect("EmployeeTerritoryList.aspx");
            }
        }


    }
}
