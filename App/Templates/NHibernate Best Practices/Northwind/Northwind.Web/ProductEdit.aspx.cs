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
    public partial class ProductEdit : System.Web.UI.Page
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
            uiCategoryID.Items.Add("");
            uiCategoryID.DataSource = daoFactory.GetCategoryDao().GetAll();
            uiCategoryID.DataValueField = "ID";
            uiCategoryID.DataTextField  = "CategoryName";
            uiCategoryID.DataBind();

            uiSupplierID.Items.Add("");
            uiSupplierID.DataSource = daoFactory.GetSupplierDao().GetAll();
            uiSupplierID.DataValueField = "ID";
            uiSupplierID.DataTextField  = "CompanyName";
            uiSupplierID.DataBind();

        }

        private void fillForm()
        {
            if ( Request.QueryString["ProductID"] != null  )
            {
             uiProductID.Value = Request.QueryString["ProductID"];
             System.Int32 ID = Convert.ToInt32(uiProductID.Value);

                IDaoFactory daoFactory = new NHibernateDaoFactory();
                IProductDao dao = daoFactory.GetProductDao();
                Product entity = dao.GetById(ID, false );

                uiProductName.Text = entity.ProductName;
                uiSupplierID.SelectedValue = Convert.ToString(entity.SupplierID);
                uiCategoryID.SelectedValue = Convert.ToString(entity.CategoryID);
                uiQuantityPerUnit.Text = entity.QuantityPerUnit;
                uiUnitPrice.Text = Convert.ToString(entity.UnitPrice);
                uiUnitsInStock.Text = Convert.ToString(entity.UnitsInStock);
                uiUnitsOnOrder.Text = Convert.ToString(entity.UnitsOnOrder);
                uiReorderLevel.Text = Convert.ToString(entity.ReorderLevel);
                uiDiscontinued.Checked = entity.Discontinued;
                uiIsNew.Checked = false;
            }
        }

        protected void Update(object sender, System.EventArgs e)
        {
            Product entity = null;

            IDaoFactory daoFactory = new NHibernateDaoFactory();
            IProductDao dao = daoFactory.GetProductDao();

            System.Int32 ID = Convert.ToInt32(uiProductID.Value);
            if (! uiIsNew.Checked )
            {
                entity = dao.GetById(ID, false );
                }
                else
                {
                    entity = new Product(ID);
                }

            entity.ProductName = uiProductName.Text;
            entity.SupplierID = Convert.ToInt32(uiSupplierID.SelectedValue);
            entity.CategoryID = Convert.ToInt32(uiCategoryID.SelectedValue);
            entity.QuantityPerUnit = uiQuantityPerUnit.Text;
            entity.UnitPrice = Convert.ToDecimal(uiUnitPrice.Text);
            entity.UnitsInStock = Convert.ToInt16(uiUnitsInStock.Text);
            entity.UnitsOnOrder = Convert.ToInt16(uiUnitsOnOrder.Text);
            entity.ReorderLevel = Convert.ToInt16(uiReorderLevel.Text);
            entity.Discontinued = uiDiscontinued.Checked;

            if (uiIsNew.Checked)
            {
                dao.Save(entity);
                uiProductID.Value = Convert.ToString(entity.ID);
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
                System.Int32 ID = Convert.ToInt32(uiProductID.Value);
                IDaoFactory daoFactory = new NHibernateDaoFactory();
                IProductDao dao = daoFactory.GetProductDao();
                Product entity = dao.GetById(ID, false );

                dao.Delete(entity);

                Response.Redirect("ProductList.aspx");
            }
        }


    }
}
