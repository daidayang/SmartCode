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
    public partial class TerritoryEdit : System.Web.UI.Page
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
        }

        private void fillForm()
        {
            if ( Request.QueryString["TerritoryID"] != null  )
            {
             uiTerritoryID.Text = Request.QueryString["TerritoryID"];
             System.String ID = Convert.ToString(uiTerritoryID.Text);

                IDaoFactory daoFactory = new NHibernateDaoFactory();
                ITerritoryDao dao = daoFactory.GetTerritoryDao();
                Territory entity = dao.GetById(ID, false );

                uiTerritoryDescription.Text = entity.TerritoryDescription;
                uiRegionID.Text = Convert.ToString(entity.RegionID);
                uiIsNew.Checked = false;
            }
        }

        protected void Update(object sender, System.EventArgs e)
        {
            Territory entity = null;

            IDaoFactory daoFactory = new NHibernateDaoFactory();
            ITerritoryDao dao = daoFactory.GetTerritoryDao();

            System.String ID = Convert.ToString(uiTerritoryID.Text);
            if (! uiIsNew.Checked )
            {
                entity = dao.GetById(ID, false );
                }
                else
                {
                    entity = new Territory(ID);
                }

            entity.TerritoryDescription = uiTerritoryDescription.Text;
            entity.RegionID = Convert.ToInt32(uiRegionID.Text);

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
                System.String ID = Convert.ToString(uiTerritoryID.Text);
                IDaoFactory daoFactory = new NHibernateDaoFactory();
                ITerritoryDao dao = daoFactory.GetTerritoryDao();
                Territory entity = dao.GetById(ID, false );

                dao.Delete(entity);

                Response.Redirect("TerritoryList.aspx");
            }
        }


    }
}
