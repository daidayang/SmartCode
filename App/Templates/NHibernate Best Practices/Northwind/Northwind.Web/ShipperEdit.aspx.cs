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
    public partial class ShipperEdit : System.Web.UI.Page
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
            if ( Request.QueryString["ShipperID"] != null  )
            {
             uiShipperID.Value = Request.QueryString["ShipperID"];
             System.Int32 ID = Convert.ToInt32(uiShipperID.Value);

                IDaoFactory daoFactory = new NHibernateDaoFactory();
                IShipperDao dao = daoFactory.GetShipperDao();
                Shipper entity = dao.GetById(ID, false );

                uiCompanyName.Text = entity.CompanyName;
                uiPhone.Text = entity.Phone;
                uiIsNew.Checked = false;
            }
        }

        protected void Update(object sender, System.EventArgs e)
        {
            Shipper entity = null;

            IDaoFactory daoFactory = new NHibernateDaoFactory();
            IShipperDao dao = daoFactory.GetShipperDao();

            System.Int32 ID = Convert.ToInt32(uiShipperID.Value);
            if (! uiIsNew.Checked )
            {
                entity = dao.GetById(ID, false );
                }
                else
                {
                    entity = new Shipper(ID);
                }

            entity.CompanyName = uiCompanyName.Text;
            entity.Phone = uiPhone.Text;

            if (uiIsNew.Checked)
            {
                dao.Save(entity);
                uiShipperID.Value = Convert.ToString(entity.ID);
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
                System.Int32 ID = Convert.ToInt32(uiShipperID.Value);
                IDaoFactory daoFactory = new NHibernateDaoFactory();
                IShipperDao dao = daoFactory.GetShipperDao();
                Shipper entity = dao.GetById(ID, false );

                dao.Delete(entity);

                Response.Redirect("ShipperList.aspx");
            }
        }


    }
}
