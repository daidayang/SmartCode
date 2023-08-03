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
    public partial class OrderEdit : System.Web.UI.Page
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
            if ( Request.QueryString["OrderID"] != null  )
            {
             uiOrderID.Value = Request.QueryString["OrderID"];
             System.Int32 ID = Convert.ToInt32(uiOrderID.Value);

                IDaoFactory daoFactory = new NHibernateDaoFactory();
                IOrderDao dao = daoFactory.GetOrderDao();
                Order entity = dao.GetById(ID, false );

                uiCustomerID.Text = entity.CustomerID;
                uiEmployeeID.Text = Convert.ToString(entity.EmployeeID);
                uiOrderDate.Text = Convert.ToString(entity.OrderDate);
                uiRequiredDate.Text = Convert.ToString(entity.RequiredDate);
                uiShippedDate.Text = Convert.ToString(entity.ShippedDate);
                uiShipVia.Text = Convert.ToString(entity.ShipVia);
                uiFreight.Text = Convert.ToString(entity.Freight);
                uiShipName.Text = entity.ShipName;
                uiShipAddress.Text = entity.ShipAddress;
                uiShipCity.Text = entity.ShipCity;
                uiShipRegion.Text = entity.ShipRegion;
                uiShipPostalCode.Text = entity.ShipPostalCode;
                uiShipCountry.Text = entity.ShipCountry;
                uiIsNew.Checked = false;
            }
        }

        protected void Update(object sender, System.EventArgs e)
        {
            Order entity = null;

            IDaoFactory daoFactory = new NHibernateDaoFactory();
            IOrderDao dao = daoFactory.GetOrderDao();

            System.Int32 ID = Convert.ToInt32(uiOrderID.Value);
            if (! uiIsNew.Checked )
            {
                entity = dao.GetById(ID, false );
                }
                else
                {
                    entity = new Order(ID);
                }

            entity.CustomerID = uiCustomerID.Text;
            entity.EmployeeID = Convert.ToInt32(uiEmployeeID.Text);
            entity.OrderDate = Convert.ToDateTime(uiOrderDate.Text);
            entity.RequiredDate = Convert.ToDateTime(uiRequiredDate.Text);
            entity.ShippedDate = Convert.ToDateTime(uiShippedDate.Text);
            entity.ShipVia = Convert.ToInt32(uiShipVia.Text);
            entity.Freight = Convert.ToDecimal(uiFreight.Text);
            entity.ShipName = uiShipName.Text;
            entity.ShipAddress = uiShipAddress.Text;
            entity.ShipCity = uiShipCity.Text;
            entity.ShipRegion = uiShipRegion.Text;
            entity.ShipPostalCode = uiShipPostalCode.Text;
            entity.ShipCountry = uiShipCountry.Text;

            if (uiIsNew.Checked)
            {
                dao.Save(entity);
                uiOrderID.Value = Convert.ToString(entity.ID);
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
                System.Int32 ID = Convert.ToInt32(uiOrderID.Value);
                IDaoFactory daoFactory = new NHibernateDaoFactory();
                IOrderDao dao = daoFactory.GetOrderDao();
                Order entity = dao.GetById(ID, false );

                dao.Delete(entity);

                Response.Redirect("OrderList.aspx");
            }
        }


    }
}
