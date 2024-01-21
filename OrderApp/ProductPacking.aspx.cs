using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OrderApp
{
    public partial class ProductPacking : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    GetProductPackingList();
                }
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("AddProductPacking.aspx", false);
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        protected void grdProductPackingList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "EditValue")
                {
                    Int32 ProductPckID = Convert.ToInt32(e.CommandArgument);

                    Common cmn = new Common();
                    string strEncryptValue = cmn.Encrypt(Convert.ToString(ProductPckID));

                    Response.Redirect("AddProductPacking.aspx?q=" + strEncryptValue, false);
                }

                if (e.CommandName == "DeleteValue")
                {
                    Int32 ProductPckID = Convert.ToInt32(e.CommandArgument);

                    BA_tblProductPacking ObjProductPacking = new BA_tblProductPacking();

                    ObjProductPacking.ProductPckID = Convert.ToString(ProductPckID);
                    bool output = ObjProductPacking.DELETE_RECORDS_FROM_tblProductPacking();

                    if (output == true)
                    {
                        GetProductPackingList();
                    }
                    else
                    {
                        lblErrorMessage.Text = CommMessage.CouldnotabletoDelete;
                    }
                }
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        protected void GetProductPackingList()
        {
            try
            {
                DataTable dt = new DataTable();
                BA_tblProductPacking objProductPacking = new BA_tblProductPacking();
                objProductPacking.SELECT_ALL_tblProductPacking(ref dt);

                Session["dtProductPacking"] = dt;

                grdProductPackingList.DataSource = dt;
                grdProductPackingList.DataBind();
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        protected void grdProductPackingList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                DataTable dtProductPacking = new DataTable();
                dtProductPacking = Session["dtProductPacking"] as DataTable;

                grdProductPackingList.PageIndex = e.NewPageIndex;
                grdProductPackingList.DataSource = dtProductPacking;
                grdProductPackingList.DataBind();
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }
    }
}