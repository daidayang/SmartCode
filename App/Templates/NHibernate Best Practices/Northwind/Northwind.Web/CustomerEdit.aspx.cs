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
    public partial class CustomerEdit : System.Web.UI.Page
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
            if ( Request.QueryString["CustomerID"] != null  )
            {
             uiCustomerID.Text = Request.QueryString["CustomerID"];
             System.String ID = Convert.ToString(uiCustomerID.Text);

                IDaoFactory daoFactory = new NHibernateDaoFactory();
                ICustomerDao dao = daoFactory.GetCustomerDao();
                Customer entity = dao.GetById(ID, false );

                uiCompanyName.Text = entity.CompanyName;
                uiContactName.Text = entity.ContactName;
                uiContactTitle.Text = entity.ContactTitle;
                uiAddress.Text = entity.Address;
                uiCity.Text = entity.City;
                uiRegion.Text = entity.Region;
                uiPostalCode.Text = entity.PostalCode;
                uiCountry.Text = entity.Country;
                uiPhone.Text = entity.Phone;
                uiFax.Text = entity.Fax;
                uiIsNew.Checked = false;
            }
        }

        protected void Update(object sender, System.EventArgs e)
        {
            Customer entity = null;

            IDaoFactory daoFactory = new NHibernateDaoFactory();
            ICustomerDao dao = daoFactory.GetCustomerDao();

            System.String ID = Convert.ToString(uiCustomerID.Text);
            if (! uiIsNew.Checked )
            {
                entity = dao.GetById(ID, false );
                }
                else
                {
                    entity = new Customer(ID);
                }

            entity.CompanyName = uiCompanyName.Text;
            entity.ContactName = uiContactName.Text;
            entity.ContactTitle = uiContactTitle.Text;
            entity.Address = uiAddress.Text;
            entity.City = uiCity.Text;
            entity.Region = uiRegion.Text;
            entity.PostalCode = uiPostalCode.Text;
            entity.Country = uiCountry.Text;
            entity.Phone = uiPhone.Text;
            entity.Fax = uiFax.Text;

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
                System.String ID = Convert.ToString(uiCustomerID.Text);
                IDaoFactory daoFactory = new NHibernateDaoFactory();
                ICustomerDao dao = daoFactory.GetCustomerDao();
                Customer entity = dao.GetById(ID, false );

                dao.Delete(entity);

                Response.Redirect("CustomerList.aspx");
            }
        }


    }
}
