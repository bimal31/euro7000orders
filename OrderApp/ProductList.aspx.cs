﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OrderApp
{
    public partial class ProductList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    GetProductList();
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
                Response.Redirect("AddProduct.aspx", false);
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        protected void grdProduct_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "EditValue")
                {
                    Int32 ProductId = Convert.ToInt32(e.CommandArgument);

                    Common cmn = new Common();
                    string strEncryptValue = cmn.Encrypt(Convert.ToString(ProductId));

                    Response.Redirect("AddProduct.aspx?q=" + strEncryptValue, false);
                   // Response.Redirect("AddProduct.aspx?ProductId=" + ProductId, false);
                }

                if (e.CommandName == "DeleteValue")
                {
                    Int32 ProductId = Convert.ToInt32(e.CommandArgument);

                    BA_tblProduct ObjDealer = new BA_tblProduct();

                    ObjDealer.ProductId = Convert.ToString(ProductId);
                    bool output = ObjDealer.DELETE_RECORDS_FROM_tblProduct();

                    if (output == true)
                    {
                        GetProductList();
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

        protected void GetProductList()
        {
            try
            {
                DataTable dt = new DataTable();
                BA_tblProduct objUser = new BA_tblProduct();
                objUser.SELECT_ALL_tblProduct(ref dt);

                Session["dtProduct"] = dt;

                grdProductList.DataSource = dt;
                grdProductList.DataBind();
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        protected void grdProductList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                DataTable dtProduct = new DataTable();
                dtProduct = Session["dtProduct"] as DataTable;

                grdProductList.PageIndex = e.NewPageIndex;
                grdProductList.DataSource = dtProduct;
                grdProductList.DataBind();
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }
    }
}