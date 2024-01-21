﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OrderApp
{
    public partial class Dealer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    txtSearch.Attributes.Add("Placeholder", "Search by Dealer details");
                    GetdealerList();
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
                Response.Redirect("AddDealer.aspx", false);
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        protected void grdDealerList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "EditValue")
                {
                    Int32 DealerId = Convert.ToInt32(e.CommandArgument);

                    Common cmn = new Common();
                    //   string strEncryptId = cmn.Encrypt("DealerId");
                    string strEncryptValue = cmn.Encrypt(Convert.ToString(DealerId));

                    Response.Redirect("AddDealer.aspx?q=" + strEncryptValue, false);
                    // Response.Redirect("AddDealer.aspx?DealerId=" + DealerId, false);
                }

                if (e.CommandName == "DeleteValue")
                {
                    Int32 DealerId = Convert.ToInt32(e.CommandArgument);

                    //hdDeleteId.Value = Convert.ToString(DealerId);

                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowConfirmDelete()", true);

                    BA_tblDealer ObjDealer = new BA_tblDealer();

                    ObjDealer.DealerID = Convert.ToInt32(DealerId);
                    bool output = ObjDealer.DELETE_RECORDS_FROM_tblDealer();

                    if (output == true)
                    {
                        GetdealerList();
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

        protected void GetdealerList() {
            try
            {
                DataTable dt = new DataTable();
                BA_tblDealer objUser = new BA_tblDealer();
                if (chkDealerCode.Checked)
                {
                    objUser.IsDealerCode = 1;
                }
                else
                {
                    objUser.IsDealerCode = 0;
                }
                objUser.DealerName = txtSearch.Text;
                objUser.SELECT_ALL_tblDealer(ref dt);

                Session["dtDealer"] = dt;

                grdDealerList.DataSource = dt;
                grdDealerList.DataBind();
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        protected void btnDeleteOk_ServerClick(object sender, EventArgs e)
        {
            try
            {
                BA_tblDealer ObjDealer = new BA_tblDealer();

                ObjDealer.DealerID = Convert.ToInt32(hdDeleteId.Value);
                bool output = ObjDealer.DELETE_RECORDS_FROM_tblDealer();

                if (output == true)
                {
                    GetdealerList();
                }
                else
                {
                    lblErrorMessage.Text = CommMessage.CouldnotabletoDelete;
                }
            }
            catch(Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        protected void grdDealerList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                DataTable dtDealer = new DataTable();
                dtDealer = Session["dtDealer"] as DataTable;

                grdDealerList.PageIndex = e.NewPageIndex;
                grdDealerList.DataSource = dtDealer;
                grdDealerList.DataBind();
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        protected void chkDealerCode_CheckedChanged(object sender, EventArgs e)
        {
            try {
                GetdealerList();
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        protected void ExportToExcelDealer(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=DeakerList" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                grdDealerList.AllowPaging = false;
                this.GetdealerList();
                grdDealerList.RenderControl(hw);
                
                string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

    }
}