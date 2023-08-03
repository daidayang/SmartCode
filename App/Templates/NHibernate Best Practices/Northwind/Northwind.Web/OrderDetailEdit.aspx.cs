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
    public partial class OrderDetailEdit : System.Web.UI.Page
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
            if ( Request.QueryString["OrderID"] != null  && Request.QueryString["ProductID"] != null  )
            {
             uiOrderID.Text = Request.QueryString["OrderID"];
             uiProductID.Text = Request.QueryString["ProductID"];
             OrderDetail.DomainObjectID ID = new OrderDetail.DomainObjectID(Convert.ToInt32(uiOrderID.Text), Convert.ToInt32(uiProductID.Text));

                IDaoFactory daoFactory = new NHibernateDaoFactory();
                IOrderDetailDao dao = daoFactory.GetOrderDetailDao();
                OrderDetail entity = dao.GetById(ID, false );

                uiUnitPrice.Text = Convert.ToString(entity.UnitPrice);
                uiQuantity.Text = Convert.ToString(entity.Quantity);
                uiDiscount.Text = Convert.ToString(entity.Discount);
                uiIsNew.Checked = false;
            }
        }

        protected void Update(object sender, System.EventArgs e)
        {
            OrderDetail entity = null;

            IDaoFactory daoFactory = new NHibernateDaoFactory();
            IOrderDetailDao dao = daoFactory.GetOrderDetailDao();

         OrderDetail.DomainObjectID ID = new OrderDetail.DomainObjectID(Convert.ToInt32(uiOrderID.Text), Convert.ToInt32(uiProductID.Text));
            if (! uiIsNew.Checked )
            {
                entity = dao.GetById(ID, false );
                }
                else
                {
                    entity = new OrderDetail(ID);
                }

            entity.UnitPrice = Convert.ToDecimal(uiUnitPrice.Text);
            entity.Quantity = Convert.ToInt16(uiQuantity.Text);
            entity.Discount = Convert.ToDouble(uiDiscount.Text);

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
                OrderDetail.DomainObjectID ID = new OrderDetail.DomainObjectID(Convert.ToInt32(uiOrderID.Text), Convert.ToInt32(uiProductID.Text));
                IDaoFactory daoFactory = new NHibernateDaoFactory();
                IOrderDetailDao dao = daoFactory.GetOrderDetailDao();
                OrderDetail entity = dao.GetById(ID, false );

                dao.Delete(entity);

                Response.Redirect("OrderDetailList.aspx");
            }
        }


    }
}
