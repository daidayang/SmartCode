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
    public partial class CustomerDemographicEdit : System.Web.UI.Page
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
            if ( Request.QueryString["CustomerTypeID"] != null  )
            {
             uiCustomerTypeID.Text = Request.QueryString["CustomerTypeID"];
             System.String ID = Convert.ToString(uiCustomerTypeID.Text);

                IDaoFactory daoFactory = new NHibernateDaoFactory();
                ICustomerDemographicDao dao = daoFactory.GetCustomerDemographicDao();
                CustomerDemographic entity = dao.GetById(ID, false );

                uiCustomerDesc.Text = entity.CustomerDesc;
                uiIsNew.Checked = false;
            }
        }

        protected void Update(object sender, System.EventArgs e)
        {
            CustomerDemographic entity = null;

            IDaoFactory daoFactory = new NHibernateDaoFactory();
            ICustomerDemographicDao dao = daoFactory.GetCustomerDemographicDao();

            System.String ID = Convert.ToString(uiCustomerTypeID.Text);
            if (! uiIsNew.Checked )
            {
                entity = dao.GetById(ID, false );
                }
                else
                {
                    entity = new CustomerDemographic(ID);
                }

            entity.CustomerDesc = uiCustomerDesc.Text;

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
                System.String ID = Convert.ToString(uiCustomerTypeID.Text);
                IDaoFactory daoFactory = new NHibernateDaoFactory();
                ICustomerDemographicDao dao = daoFactory.GetCustomerDemographicDao();
                CustomerDemographic entity = dao.GetById(ID, false );

                dao.Delete(entity);

                Response.Redirect("CustomerDemographicList.aspx");
            }
        }


    }
}
