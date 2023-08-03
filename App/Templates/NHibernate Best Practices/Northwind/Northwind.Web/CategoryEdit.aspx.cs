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
    public partial class CategoryEdit : System.Web.UI.Page
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
            if ( Request.QueryString["CategoryID"] != null  )
            {
             uiCategoryID.Value = Request.QueryString["CategoryID"];
             System.Int32 ID = Convert.ToInt32(uiCategoryID.Value);

                IDaoFactory daoFactory = new NHibernateDaoFactory();
                ICategoryDao dao = daoFactory.GetCategoryDao();
                Category entity = dao.GetById(ID, false );

                uiCategoryName.Text = entity.CategoryName;
                uiDescription.Text = entity.Description;
                uiIsNew.Checked = false;
            }
        }

        protected void Update(object sender, System.EventArgs e)
        {
            Category entity = null;

            IDaoFactory daoFactory = new NHibernateDaoFactory();
            ICategoryDao dao = daoFactory.GetCategoryDao();

            System.Int32 ID = Convert.ToInt32(uiCategoryID.Value);
            if (! uiIsNew.Checked )
            {
                entity = dao.GetById(ID, false );
                }
                else
                {
                    entity = new Category(ID);
                }

            entity.CategoryName = uiCategoryName.Text;
            entity.Description = uiDescription.Text;

            if (uiIsNew.Checked)
            {
                dao.Save(entity);
                uiCategoryID.Value = Convert.ToString(entity.ID);
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
                System.Int32 ID = Convert.ToInt32(uiCategoryID.Value);
                IDaoFactory daoFactory = new NHibernateDaoFactory();
                ICategoryDao dao = daoFactory.GetCategoryDao();
                Category entity = dao.GetById(ID, false );

                dao.Delete(entity);

                Response.Redirect("CategoryList.aspx");
            }
        }


    }
}
