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
    public partial class EmployeeEdit : System.Web.UI.Page
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
            if ( Request.QueryString["EmployeeID"] != null  )
            {
             uiEmployeeID.Value = Request.QueryString["EmployeeID"];
             System.Int32 ID = Convert.ToInt32(uiEmployeeID.Value);

                IDaoFactory daoFactory = new NHibernateDaoFactory();
                IEmployeeDao dao = daoFactory.GetEmployeeDao();
                Employee entity = dao.GetById(ID, false );

                uiLastName.Text = entity.LastName;
                uiFirstName.Text = entity.FirstName;
                uiTitle.Text = entity.Title;
                uiTitleOfCourtesy.Text = entity.TitleOfCourtesy;
                uiBirthDate.Text = Convert.ToString(entity.BirthDate);
                uiHireDate.Text = Convert.ToString(entity.HireDate);
                uiAddress.Text = entity.Address;
                uiCity.Text = entity.City;
                uiRegion.Text = entity.Region;
                uiPostalCode.Text = entity.PostalCode;
                uiCountry.Text = entity.Country;
                uiHomePhone.Text = entity.HomePhone;
                uiExtension.Text = entity.Extension;
                uiNotes.Text = entity.Notes;
                uiReportsTo.Text = Convert.ToString(entity.ReportsTo);
                uiPhotoPath.Text = entity.PhotoPath;
                uiIsNew.Checked = false;
            }
        }

        protected void Update(object sender, System.EventArgs e)
        {
            Employee entity = null;

            IDaoFactory daoFactory = new NHibernateDaoFactory();
            IEmployeeDao dao = daoFactory.GetEmployeeDao();

            System.Int32 ID = Convert.ToInt32(uiEmployeeID.Value);
            if (! uiIsNew.Checked )
            {
                entity = dao.GetById(ID, false );
                }
                else
                {
                    entity = new Employee(ID);
                }

            entity.LastName = uiLastName.Text;
            entity.FirstName = uiFirstName.Text;
            entity.Title = uiTitle.Text;
            entity.TitleOfCourtesy = uiTitleOfCourtesy.Text;
            entity.BirthDate = Convert.ToDateTime(uiBirthDate.Text);
            entity.HireDate = Convert.ToDateTime(uiHireDate.Text);
            entity.Address = uiAddress.Text;
            entity.City = uiCity.Text;
            entity.Region = uiRegion.Text;
            entity.PostalCode = uiPostalCode.Text;
            entity.Country = uiCountry.Text;
            entity.HomePhone = uiHomePhone.Text;
            entity.Extension = uiExtension.Text;
            entity.Notes = uiNotes.Text;
            entity.ReportsTo = Convert.ToInt32(uiReportsTo.Text);
            entity.PhotoPath = uiPhotoPath.Text;

            if (uiIsNew.Checked)
            {
                dao.Save(entity);
                uiEmployeeID.Value = Convert.ToString(entity.ID);
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
                System.Int32 ID = Convert.ToInt32(uiEmployeeID.Value);
                IDaoFactory daoFactory = new NHibernateDaoFactory();
                IEmployeeDao dao = daoFactory.GetEmployeeDao();
                Employee entity = dao.GetById(ID, false );

                dao.Delete(entity);

                Response.Redirect("EmployeeList.aspx");
            }
        }


    }
}
