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
    public partial class SupplierEdit : System.Web.UI.Page
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
            if ( Request.QueryString["SupplierID"] != null  )
            {
             uiSupplierID.Value = Request.QueryString["SupplierID"];
             System.Int32 ID = Convert.ToInt32(uiSupplierID.Value);

                IDaoFactory daoFactory = new NHibernateDaoFactory();
                ISupplierDao dao = daoFactory.GetSupplierDao();
                Supplier entity = dao.GetById(ID, false );

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
                uiHomePage.Text = entity.HomePage;
                uiIsNew.Checked = false;
            }
        }

        protected void Update(object sender, System.EventArgs e)
        {
            Supplier entity = null;

            IDaoFactory daoFactory = new NHibernateDaoFactory();
            ISupplierDao dao = daoFactory.GetSupplierDao();

            System.Int32 ID = Convert.ToInt32(uiSupplierID.Value);
            if (! uiIsNew.Checked )
            {
                entity = dao.GetById(ID, false );
                }
                else
                {
                    entity = new Supplier(ID);
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
            entity.HomePage = uiHomePage.Text;

            if (uiIsNew.Checked)
            {
                dao.Save(entity);
                uiSupplierID.Value = Convert.ToString(entity.ID);
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
                System.Int32 ID = Convert.ToInt32(uiSupplierID.Value);
                IDaoFactory daoFactory = new NHibernateDaoFactory();
                ISupplierDao dao = daoFactory.GetSupplierDao();
                Supplier entity = dao.GetById(ID, false );

                dao.Delete(entity);

                Response.Redirect("SupplierList.aspx");
            }
        }


    }
}
