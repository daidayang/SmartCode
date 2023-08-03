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

namespace Northwind.Web
{
    public partial class CategoryList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid()
        {
            IDaoFactory daoFactory = new NHibernateDaoFactory();
            ICategoryDao dao = daoFactory.GetCategoryDao();

            gridData.DataSource = dao.GetAll();
            gridData.DataBind();
        }

        protected void ChangeGridPage(object obj, DataGridPageChangedEventArgs e)
        {
            gridData.CurrentPageIndex = e.NewPageIndex;
            BindGrid();
        }

    }
}
