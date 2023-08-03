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
    public partial class RegionEdit : System.Web.UI.Page
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
            if ( Request.QueryString["RegionID"] != null  )
            {
             uiRegionID.Text = Request.QueryString["RegionID"];
             System.Int32 ID = Convert.ToInt32(uiRegionID.Text);

                IDaoFactory daoFactory = new NHibernateDaoFactory();
                IRegionDao dao = daoFactory.GetRegionDao();
                Region entity = dao.GetById(ID, false );

                uiRegionDescription.Text = entity.RegionDescription;
                uiIsNew.Checked = false;
            }
        }

        protected void Update(object sender, System.EventArgs e)
        {
            Region entity = null;

            IDaoFactory daoFactory = new NHibernateDaoFactory();
            IRegionDao dao = daoFactory.GetRegionDao();

            System.Int32 ID = Convert.ToInt32(uiRegionID.Text);
            if (! uiIsNew.Checked )
            {
                entity = dao.GetById(ID, false );
                }
                else
                {
                    entity = new Region(ID);
                }

            entity.RegionDescription = uiRegionDescription.Text;

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
                System.Int32 ID = Convert.ToInt32(uiRegionID.Text);
                IDaoFactory daoFactory = new NHibernateDaoFactory();
                IRegionDao dao = daoFactory.GetRegionDao();
                Region entity = dao.GetById(ID, false );

                dao.Delete(entity);

                Response.Redirect("RegionList.aspx");
            }
        }


    }
}
