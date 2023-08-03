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
    public partial class CustomerCustomerDemoEdit : System.Web.UI.Page
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
            uiCustomerID.Items.Add("");
            uiCustomerID.DataSource = daoFactory.GetCustomerDao().GetAll();
            uiCustomerID.DataValueField = "ID";
            uiCustomerID.DataTextField  = "CompanyName";
            uiCustomerID.DataBind();

            uiCustomerTypeID.Items.Add("");
            uiCustomerTypeID.DataSource = daoFactory.GetCustomerDemographicDao().GetAll();
            uiCustomerTypeID.DataValueField = "ID";
            uiCustomerTypeID.DataTextField  = "CustomerDesc";
            uiCustomerTypeID.DataBind();

        }

        private void fillForm()
        {
            if ( Request.QueryString["CustomerID"] != null  && Request.QueryString["CustomerTypeID"] != null  )
            {
             uiCustomerID.SelectedValue = Request.QueryString["CustomerID"];
             uiCustomerTypeID.SelectedValue = Request.QueryString["CustomerTypeID"];
             CustomerCustomerDemo.DomainObjectID ID = new CustomerCustomerDemo.DomainObjectID(Convert.ToString(uiCustomerID.SelectedValue), Convert.ToString(uiCustomerTypeID.SelectedValue));

                IDaoFactory daoFactory = new NHibernateDaoFactory();
                ICustomerCustomerDemoDao dao = daoFactory.GetCustomerCustomerDemoDao();
                CustomerCustomerDemo entity = dao.GetById(ID, false );

                uiIsNew.Checked = false;
            }
        }

        protected void Update(object sender, System.EventArgs e)
        {
            CustomerCustomerDemo entity = null;

            IDaoFactory daoFactory = new NHibernateDaoFactory();
            ICustomerCustomerDemoDao dao = daoFactory.GetCustomerCustomerDemoDao();

         CustomerCustomerDemo.DomainObjectID ID = new CustomerCustomerDemo.DomainObjectID(Convert.ToString(uiCustomerID.SelectedValue), Convert.ToString(uiCustomerTypeID.SelectedValue));
            if (! uiIsNew.Checked )
            {
                entity = dao.GetById(ID, false );
                }
                else
                {
                    entity = new CustomerCustomerDemo(ID);
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
                CustomerCustomerDemo.DomainObjectID ID = new CustomerCustomerDemo.DomainObjectID(Convert.ToString(uiCustomerID.SelectedValue), Convert.ToString(uiCustomerTypeID.SelectedValue));
                IDaoFactory daoFactory = new NHibernateDaoFactory();
                ICustomerCustomerDemoDao dao = daoFactory.GetCustomerCustomerDemoDao();
                CustomerCustomerDemo entity = dao.GetById(ID, false );

                dao.Delete(entity);

                Response.Redirect("CustomerCustomerDemoList.aspx");
            }
        }


    }
}
