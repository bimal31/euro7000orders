using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OrderApp
{
    public partial class AddOrder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {


                    string UserType = "";
                    UserType = Convert.ToString(HttpContext.Current.Session["UserType"]);
                    txtOrderDate.Text = DateTime.Now.ToString("dd/MM/yyyy", new CultureInfo("en-GB"));
                    lblTotal.Attributes.Add("readonly", "readonly");
                    lblTotal.Text = "0";
                    hdTotalKgCount.Value = "0";


                    bindSale();
                    BindGrid();

                    lblheading.Text = CommMessage.addOrder;

                    if (Request.QueryString["q"] != null)
                    {
                        string strKey = Convert.ToString(Request.QueryString["q"]);
                        Common cmn = new Common();
                        strKey = cmn.Decrypt(strKey);
                        Int32 OrderId = Convert.ToInt32(strKey);
                        GetOrderDetails(OrderId);
                        lblheading.Text = CommMessage.EditOrder;
                    }
                    if (Request.QueryString["BackButton"] != null && Request.QueryString["BackButton"] == "N")
                        ViewState["BackButton"] = "N";
                    else
                        ViewState["BackButton"] = "Y";

                    if (Request.QueryString["View"] != null && Request.QueryString["View"] == "Y")
                    {
                        lblheading.Text = CommMessage.viewOrder;



                        btnAddDealer.Attributes.Add("style", "display:none");
                        btnSubmitOrder.Attributes.Add("style", "display:none");
                       // btnClear.Attributes.Add("style", "display:none");
                    }
                }
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        public void bindSale()
        {
            BA_tblUser ObjBA_tblUser = new BA_tblUser();
            DataTable dt = new DataTable();
            try
            {
                ObjBA_tblUser.SELECT_ALL_tblUserSalesman(ref dt);
                drpsSalesExe.DataSource = dt;
                drpsSalesExe.DataTextField = "UserName";
                drpsSalesExe.DataValueField = "UserID";
                drpsSalesExe.DataBind();


                drpsSalesExe.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
            catch (Exception ex)
            {

                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }

        }

        protected void BindGrid()
        {
            try
            {
                BA_tblProductPacking Objtbl = new BA_tblProductPacking();

                DataTable dt1 = new DataTable();
                Objtbl.ProductID = "1";
                Objtbl.GET_RECORDS_FROM_tblProductById(ref dt1);
                gridEUROXTRA.DataSource = dt1;
                gridEUROXTRA.DataBind();


                DataTable dt2 = new DataTable();
                Objtbl.ProductID = "2";
                Objtbl.GET_RECORDS_FROM_tblProductById(ref dt2);
                grdEUROWP.DataSource = dt2;
                grdEUROWP.DataBind();


                DataTable dt3 = new DataTable();
                Objtbl.ProductID = "3";
                Objtbl.GET_RECORDS_FROM_tblProductById(ref dt3);
                grdeuro2in1.DataSource = dt3;
                grdeuro2in1.DataBind();


                DataTable dt4 = new DataTable();
                Objtbl.ProductID = "4";
                Objtbl.GET_RECORDS_FROM_tblProductById(ref dt4);
                grdExtreme.DataSource = dt4;
                grdExtreme.DataBind();


                DataTable dt5 = new DataTable();
                Objtbl.ProductID = "5";
                Objtbl.GET_RECORDS_FROM_tblProductById(ref dt5);
                grdEuroUltra.DataSource = dt5;
                grdEuroUltra.DataBind();


                DataTable dt6 = new DataTable();
                Objtbl.ProductID = "6";
                Objtbl.GET_RECORDS_FROM_tblProductById(ref dt6);
                GrdPvcGlue.DataSource = dt6;
                GrdPvcGlue.DataBind();

                DataTable dt7 = new DataTable();
                Objtbl.ProductID = "7";
                Objtbl.GET_RECORDS_FROM_tblProductById(ref dt7);
                grdWoodStrong.DataSource = dt7;
                grdWoodStrong.DataBind();

                DataTable dt8 = new DataTable();
                Objtbl.ProductID = "8";
                Objtbl.GET_RECORDS_FROM_tblProductById(ref dt8);
                grdEuroEWR.DataSource = dt8;
                grdEuroEWR.DataBind();

            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        protected void txtDealerCodeSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (GetDealerRecord())
                {
                    lblErrorMessage.Text = "";
                }
                else
                {
                    lblErrorMessage.Text = CommMessage.DealerNotfound;
                }
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }



        protected void btnSaveDealer_Click(object sender, EventArgs e)
        {
            try
            {
                BA_tblDealer ObjDealer = new BA_tblDealer();
                Common Cmn = new Common();
                ObjDealer.DealerName = txtDealerName.Text;
                ObjDealer.ContactName = txtContactName.Text;
                ObjDealer.Address = txtAddress.Text;
                ObjDealer.Area = txtArea.Text;
                ObjDealer.GST = txtGST.Text;
                ObjDealer.Phone = txtPhoneNo.Text;
                ObjDealer.Pincode = txtpincode.Text;
                ObjDealer.CreateBy = Convert.ToInt32(Session["UserId"]);
                ObjDealer.Isdeleted = false;
                ObjDealer.UpdateBy = Convert.ToInt32(Session["UserId"]);

                bool output;
                output = ObjDealer.INSERT_tblDealer();

                if (output == true)
                {

                    divAddOrder.Visible = true;
                    btnSubmitOrder.Visible = true;
                    //btnClear.Visible = true;
                    divOtherDetails.Visible = true;
                    GetDealerRecord();

                    if (txtDealerCode.Text == "")
                    {
                        txtDealerCodeSearch.Text = txtDealerName.Text;
                        lbldealercode.Text = "Dealer Name";
                    }
                    else
                    {
                        txtDealerCodeSearch.Text = txtDealerCode.Text;
                        lbldealercode.Text = "Dealer Code";
                    }
                }
                else
                {
                    lblErrorMessage.Text = CommMessage.Recordcouldnotable;
                }
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        //protected void btnCancel_Click(object sender, EventArgs e)
        //{
        //    try
        //    {

        //        divAddOrder.Visible = true;
        //        btnSubmitOrder.Visible = true;
        //        //btnClear.Visible = true;
        //        divOtherDetails.Visible = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        BA_ErrorLog ObjError = new BA_ErrorLog();
        //        ObjError.INSERT_ErrorLog(ex);
        //    }
        //}

        protected bool GetDealerRecord()
        {
            try
            {
                BA_tblDealer ObjDealer = new BA_tblDealer();
                DataTable dt = new DataTable();

                ObjDealer.DealerCode = txtDealerCodeSearch.Text;
                ObjDealer.GET_RECORDS_FROM_tblDealer_ByCode(ref dt);

                if (dt != null && dt.Rows.Count > 0)
                {
                    txtDealerCode.Text = Convert.ToString(dt.Rows[0]["DealerCode"]);
                    txtDealerName.Text = Convert.ToString(dt.Rows[0]["DealerName"]);
                    txtdealernamesearch.Text = Convert.ToString(dt.Rows[0]["DealerName"]);
                    txtContactName.Text = Convert.ToString(dt.Rows[0]["ContactName"]);
                    txtAddress.Text = Convert.ToString(dt.Rows[0]["Address"]);
                    txtArea.Text = Convert.ToString(dt.Rows[0]["Area"]);
                    txtPhoneNo.Text = Convert.ToString(dt.Rows[0]["Phone"]);
                    txtGST.Text = Convert.ToString(dt.Rows[0]["GST"]);
                    txtpincode.Text = Convert.ToString(dt.Rows[0]["Pincode"]);
                    hdDelaerId.Value = Convert.ToString(dt.Rows[0]["DealerId"]);
                    return true;
                }
                else
                {
                    txtDealerCode.Text = "";
                    txtdealernamesearch.Text = "";
                    txtDealerName.Text = "";
                    txtContactName.Text = "";
                    txtAddress.Text = "";
                    txtArea.Text = "";
                    txtPhoneNo.Text = "";
                    txtGST.Text = "";
                    txtpincode.Text = "";
                    hdDelaerId.Value = "";
                    return false;
                    // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "alert('dealer not found, pls try again.');", true);
                }
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
                return false;
            }
        }

        protected void btnSubmitOrder_Click(object sender, EventArgs e)
        {
            try
            {
                int ReturnId = 0;
                string XMLData = "";

                BA_tblOrder ObjOrder = new BA_tblOrder();
                if (txtOrderDate.Text == "")
                {
                    lblErrorMessage.Text = CommMessage.enterorderdate;
                }
                else if (drpsSalesExe.SelectedIndex == 0)
                {
                    lblErrorMessage.Text = CommMessage.SelectSalesExecutive;
                }
                else if (hdDelaerId.Value == "")
                {
                    lblErrorMessage.Text = CommMessage.DealerNotfound;
                }
                else
                {

                    ObjOrder.OrderType = CommMessage.OrderType_Order;
                    ObjOrder.DealerId = hdDelaerId.Value;
                    if (hdOrderId.Value == "")
                    {
                        ObjOrder.ParentOrderId = "0";
                    }
                    else
                    {
                        ObjOrder.ParentOrderId = hdOrderId.Value;
                    }
                    ObjOrder.Transport = txttransport.Text;
                    ObjOrder.Other = txtOther.Text;
                    ObjOrder.POP = txtPOP.Text;
                    ObjOrder.SiteDelivery = txtsitedelivery.Text;



                    ObjOrder.CreateBy = Convert.ToInt32(Session["UserId"]);
                    ObjOrder.UpdateBy = Convert.ToInt32(Session["UserId"]);
                    ObjOrder.OrderStatus = drpOrderStatus.SelectedValue;

                    ObjOrder.SalesId = Convert.ToInt32(drpsSalesExe.SelectedValue);


                    try
                    {
                        XMLData = xmlCreate();
                    }
                    catch (Exception ex)
                    {
                        BA_ErrorLog ObjError = new BA_ErrorLog();
                        ObjError.INSERT_ErrorLog(ex);
                    }
                    if (XMLData != "")
                    {

                        ObjOrder.xmlProd = XMLData;
                        ObjOrder.TotalKgGm = Convert.ToDecimal(lblTotal.Text);
                        if (hdEditOrderId.Value != "")
                        {
                            ObjOrder.OrderID = hdEditOrderId.Value;
                            ObjOrder.UPDATE_tblOrder(ref ReturnId);
                        }
                        else
                        {
                            ObjOrder.INSERT_tblOrder(ref ReturnId);
                        }
                        hdOrderId.Value = Convert.ToString(ReturnId);
                        if (ReturnId > 0)
                        {
                            lblErrorMessage.Text = CommMessage.OrderSave;
                            Response.Redirect("OrderList.aspx", false);
                        }
                        else
                        {
                            lblErrorMessage.Text = CommMessage.somethingwrong;
                        }
                    }
                    else
                    {
                        lblErrorMessage.Text = CommMessage.enterprodqty;
                    }
                }
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }


        public string xmlCreate()
        {
            try
            {
                int kk = 0;
                decimal totalkg = 0;
                #region Product Order Details
                string XML = "";
                XML = "<OrderProduct>";
                for (int i = 0; i < gridEUROXTRA.Rows.Count; i++)
                {
                    HiddenField HdnProductId1 = gridEUROXTRA.Rows[i].FindControl("HdnProductId1") as HiddenField;
                    HiddenField HdnProductPckId1 = gridEUROXTRA.Rows[i].FindControl("HdnProductPckId1") as HiddenField;
                    HiddenField HdnProductPck1 = gridEUROXTRA.Rows[i].FindControl("HdnProductPck1") as HiddenField;
                    HiddenField HdnPackingNos1 = gridEUROXTRA.Rows[i].FindControl("HdnPackingNos1") as HiddenField;
                    HiddenField HdnPackingType1 = gridEUROXTRA.Rows[i].FindControl("HdnPackingType1") as HiddenField;
                    HiddenField HdnIsScheme1 = gridEUROXTRA.Rows[i].FindControl("HdnIsScheme1") as HiddenField;
                    HiddenField HdnTotalProductPckKG1 = gridEUROXTRA.Rows[i].FindControl("HdnTotalProductPckKG1") as HiddenField;

                    TextBox txtEUROXTRA = gridEUROXTRA.Rows[i].FindControl("txtEUROXTRA") as TextBox;
                    TextBox txtschemeEUROXTRA = gridEUROXTRA.Rows[i].FindControl("txtschemeEUROXTRA") as TextBox;

                    if (txtEUROXTRA.Text != "")
                    {
                        XML += "<TABLE>";
                        XML += "<ProductId>" + HdnProductId1.Value + "</ProductId>";
                        XML += "<ProductPckIds>" + HdnProductPckId1.Value + "</ProductPckIds>";
                        XML += "<ProductPck>" + HdnProductPck1.Value + "</ProductPck>";
                        XML += "<PackingNos>" + HdnPackingNos1.Value + "</PackingNos>";
                        XML += "<PackingType>" + HdnPackingType1.Value + "</PackingType>";
                        XML += "<BoxORNos>Box</BoxORNos>";
                        XML += "<PckTotalKg>" + HdnTotalProductPckKG1.Value + "</PckTotalKg>";
                        XML += "<ProductQty>" + txtEUROXTRA.Text + "</ProductQty>";
                        XML += "<IsScheme>" + HdnIsScheme1.Value + "</IsScheme>";
                        if (Convert.ToString(txtschemeEUROXTRA.Text) == "")
                        {
                            XML += "<Scheme></Scheme>";
                        }
                        else
                        {
                            XML += "<Scheme>" + Convert.ToString(txtschemeEUROXTRA.Text) + "</Scheme>";
                        }
                        XML += "</TABLE>";


                        kk = kk + 1;
                        totalkg = totalkg + Caltotal(Convert.ToDecimal(HdnTotalProductPckKG1.Value), Convert.ToInt32(txtEUROXTRA.Text));
                    }
                }

                for (int i = 0; i < grdEUROWP.Rows.Count; i++)
                {

                    HiddenField HdnProductId2 = grdEUROWP.Rows[i].FindControl("HdnProductId2") as HiddenField;
                    HiddenField HdnProductPckId2 = grdEUROWP.Rows[i].FindControl("HdnProductPckId2") as HiddenField;
                    HiddenField HdnProductPck2 = grdEUROWP.Rows[i].FindControl("HdnProductPck2") as HiddenField;
                    HiddenField HdnPackingNos2 = grdEUROWP.Rows[i].FindControl("HdnPackingNos2") as HiddenField;
                    HiddenField HdnPackingType2 = grdEUROWP.Rows[i].FindControl("HdnPackingType2") as HiddenField;
                    HiddenField HdnIsScheme2 = grdEUROWP.Rows[i].FindControl("HdnIsScheme2") as HiddenField;
                    HiddenField HdnTotalProductPckKG2 = grdEUROWP.Rows[i].FindControl("HdnTotalProductPckKG2") as HiddenField;


                    TextBox txteurowp = grdEUROWP.Rows[i].FindControl("txteurowp") as TextBox;
                    TextBox txtschemeEUROWP = grdEUROWP.Rows[i].FindControl("txtschemeEUROWP") as TextBox;


                    if (txteurowp.Text != "")
                    {

                        XML += "<TABLE>";
                        XML += "<ProductId>" + HdnProductId2.Value + "</ProductId>";
                        XML += "<ProductPckIds>" + HdnProductPckId2.Value + "</ProductPckIds>";
                        XML += "<ProductPck>" + HdnProductPck2.Value + "</ProductPck>";
                        XML += "<PackingNos>" + HdnPackingNos2.Value + "</PackingNos>";
                        XML += "<PackingType>" + HdnPackingType2.Value + "</PackingType>";
                        XML += "<BoxORNos>Box</BoxORNos>";
                        XML += "<PckTotalKg>" + HdnTotalProductPckKG2.Value + "</PckTotalKg>";
                        XML += "<ProductQty>" + txteurowp.Text + "</ProductQty>";
                        XML += "<IsScheme>" + HdnIsScheme2.Value + "</IsScheme>";
                        if (Convert.ToString(txtschemeEUROWP.Text) == "")
                        {
                            XML += "<Scheme></Scheme>";
                        }
                        else
                        {
                            XML += "<Scheme>" + Convert.ToString(txtschemeEUROWP.Text) + "</Scheme>";
                        }
                        XML += "</TABLE>";
                        kk = kk + 1;
                        totalkg = totalkg + Caltotal(Convert.ToDecimal(HdnTotalProductPckKG2.Value), Convert.ToInt32(txteurowp.Text));
                    }
                }

                for (int i = 0; i < grdeuro2in1.Rows.Count; i++)
                {

                    HiddenField HdnProductId3 = grdeuro2in1.Rows[i].FindControl("HdnProductId3") as HiddenField;
                    HiddenField HdnProductPckId3 = grdeuro2in1.Rows[i].FindControl("HdnProductPckId3") as HiddenField;
                    HiddenField HdnProductPck3 = grdeuro2in1.Rows[i].FindControl("HdnProductPck3") as HiddenField;
                    HiddenField HdnPackingNos3 = grdeuro2in1.Rows[i].FindControl("HdnPackingNos3") as HiddenField;
                    HiddenField HdnPackingType3 = grdeuro2in1.Rows[i].FindControl("HdnPackingType3") as HiddenField;
                    HiddenField HdnIsScheme3 = grdeuro2in1.Rows[i].FindControl("HdnIsScheme3") as HiddenField;
                    HiddenField HdnTotalProductPckKG3 = grdeuro2in1.Rows[i].FindControl("HdnTotalProductPckKG3") as HiddenField;

                    TextBox txteuro2in1 = grdeuro2in1.Rows[i].FindControl("txteuro2in1") as TextBox;
                    TextBox txtschemeeuro2in1 = grdeuro2in1.Rows[i].FindControl("txtschemeeuro2in1") as TextBox;


                    if (txteuro2in1.Text != "")
                    {

                        XML += "<TABLE>";
                        XML += "<ProductId>" + HdnProductId3.Value + "</ProductId>";
                        XML += "<ProductPckIds>" + HdnProductPckId3.Value + "</ProductPckIds>";
                        XML += "<ProductPck>" + HdnProductPck3.Value + "</ProductPck>";
                        XML += "<PackingNos>" + HdnPackingNos3.Value + "</PackingNos>";
                        XML += "<PackingType>" + HdnPackingType3.Value + "</PackingType>";
                        XML += "<BoxORNos>Box</BoxORNos>";
                        XML += "<PckTotalKg>" + HdnTotalProductPckKG3.Value + "</PckTotalKg>";
                        XML += "<ProductQty>" + txteuro2in1.Text + "</ProductQty>";
                        XML += "<IsScheme>" + HdnIsScheme3.Value + "</IsScheme>";
                        if (Convert.ToString(txtschemeeuro2in1.Text) == "")
                        {
                            XML += "<Scheme></Scheme>";
                        }
                        else
                        {
                            XML += "<Scheme>" + Convert.ToString(txtschemeeuro2in1.Text) + "</Scheme>";
                        }
                        XML += "</TABLE>";
                        kk = kk + 1;
                        totalkg = totalkg + Caltotal(Convert.ToDecimal(HdnTotalProductPckKG3.Value), Convert.ToInt32(txteuro2in1.Text));
                    }
                }

                for (int i = 0; i < grdExtreme.Rows.Count; i++)
                {
                    HiddenField HdnProductId4 = grdExtreme.Rows[i].FindControl("HdnProductId4") as HiddenField;
                    HiddenField HdnProductPckId4 = grdExtreme.Rows[i].FindControl("HdnProductPckId4") as HiddenField;
                    HiddenField HdnProductPck4 = grdExtreme.Rows[i].FindControl("HdnProductPck4") as HiddenField;
                    HiddenField HdnPackingNos4 = grdExtreme.Rows[i].FindControl("HdnPackingNos4") as HiddenField;
                    HiddenField HdnPackingType4 = grdExtreme.Rows[i].FindControl("HdnPackingType4") as HiddenField;
                    HiddenField HdnIsScheme4 = grdExtreme.Rows[i].FindControl("HdnIsScheme4") as HiddenField;
                    HiddenField HdnTotalProductPckKG4 = grdExtreme.Rows[i].FindControl("HdnTotalProductPckKG4") as HiddenField;



                    TextBox txtExtreme = grdExtreme.Rows[i].FindControl("txtExtreme") as TextBox;
                    TextBox txtschemeExtreme = grdExtreme.Rows[i].FindControl("txtschemeExtreme") as TextBox;


                    if (txtExtreme.Text != "")
                    {

                        XML += "<TABLE>";
                        XML += "<ProductId>" + HdnProductId4.Value + "</ProductId>";
                        XML += "<ProductPckIds>" + HdnProductPckId4.Value + "</ProductPckIds>";
                        XML += "<ProductPck>" + HdnProductPck4.Value + "</ProductPck>";
                        XML += "<PackingNos>" + HdnPackingNos4.Value + "</PackingNos>";
                        XML += "<PackingType>" + HdnPackingType4.Value + "</PackingType>";
                        XML += "<BoxORNos>Box</BoxORNos>";
                        XML += "<PckTotalKg>" + HdnTotalProductPckKG4.Value + "</PckTotalKg>";
                        XML += "<ProductQty>" + txtExtreme.Text + "</ProductQty>";
                        XML += "<IsScheme>" + HdnIsScheme4.Value + "</IsScheme>";
                        if (Convert.ToString(txtschemeExtreme.Text) == "")
                        {
                            XML += "<Scheme></Scheme>";
                        }
                        else
                        {
                            XML += "<Scheme>" + Convert.ToString(txtschemeExtreme.Text) + "</Scheme>";
                        }
                        XML += "</TABLE>";
                        kk = kk + 1;
                        totalkg = totalkg + Caltotal(Convert.ToDecimal(HdnTotalProductPckKG4.Value), Convert.ToInt32(txtExtreme.Text));
                    }
                }

                for (int i = 0; i < grdEuroUltra.Rows.Count; i++)
                {
                    HiddenField HdnProductId5 = grdEuroUltra.Rows[i].FindControl("HdnProductId5") as HiddenField;
                    HiddenField HdnProductPckId5 = grdEuroUltra.Rows[i].FindControl("HdnProductPckId5") as HiddenField;
                    HiddenField HdnProductPck5 = grdEuroUltra.Rows[i].FindControl("HdnProductPck5") as HiddenField;
                    HiddenField HdnPackingNos5 = grdEuroUltra.Rows[i].FindControl("HdnPackingNos5") as HiddenField;
                    HiddenField HdnPackingType5 = grdEuroUltra.Rows[i].FindControl("HdnPackingType5") as HiddenField;
                    HiddenField HdnIsScheme5 = grdEuroUltra.Rows[i].FindControl("HdnIsScheme5") as HiddenField;
                    HiddenField HdnTotalProductPckKG5 = grdEuroUltra.Rows[i].FindControl("HdnTotalProductPckKG5") as HiddenField;



                    TextBox txtEuroUltra = grdEuroUltra.Rows[i].FindControl("txtEuroUltra") as TextBox;
                    TextBox txtschemeEuroUltra = grdEuroUltra.Rows[i].FindControl("txtschemeEuroUltra") as TextBox;


                    if (txtEuroUltra.Text != "")
                    {

                        XML += "<TABLE>";
                        XML += "<ProductId>" + HdnProductId5.Value + "</ProductId>";
                        XML += "<ProductPckIds>" + HdnProductPckId5.Value + "</ProductPckIds>";
                        XML += "<ProductPck>" + HdnProductPck5.Value + "</ProductPck>";
                        XML += "<PackingNos>" + HdnPackingNos5.Value + "</PackingNos>";
                        XML += "<PackingType>" + HdnPackingType5.Value + "</PackingType>";
                        XML += "<BoxORNos>Box</BoxORNos>";
                        XML += "<PckTotalKg>" + HdnTotalProductPckKG5.Value + "</PckTotalKg>";
                        XML += "<ProductQty>" + txtEuroUltra.Text + "</ProductQty>";
                        XML += "<IsScheme>" + HdnIsScheme5.Value + "</IsScheme>";

                        if (Convert.ToString(txtschemeEuroUltra.Text) == "")
                        {
                            XML += "<Scheme></Scheme>";
                        }
                        else
                        {
                            XML += "<Scheme>" + Convert.ToString(txtschemeEuroUltra.Text) + "</Scheme>";
                        }
                        XML += "</TABLE>";
                        kk = kk + 1;
                        totalkg = totalkg + Caltotal(Convert.ToDecimal(HdnTotalProductPckKG5.Value), Convert.ToInt32(txtEuroUltra.Text));
                    }
                }

                for (int i = 0; i < GrdPvcGlue.Rows.Count; i++)
                {
                    HiddenField HdnProductId6 = GrdPvcGlue.Rows[i].FindControl("HdnProductId6") as HiddenField;
                    HiddenField HdnProductPckId6 = GrdPvcGlue.Rows[i].FindControl("HdnProductPckId6") as HiddenField;
                    HiddenField HdnProductPck6 = GrdPvcGlue.Rows[i].FindControl("HdnProductPck6") as HiddenField;
                    HiddenField HdnPackingNos6 = GrdPvcGlue.Rows[i].FindControl("HdnPackingNos6") as HiddenField;
                    HiddenField HdnPackingType6 = GrdPvcGlue.Rows[i].FindControl("HdnPackingType6") as HiddenField;
                    HiddenField HdnIsScheme6 = GrdPvcGlue.Rows[i].FindControl("HdnIsScheme6") as HiddenField;
                    HiddenField HdnTotalProductPckKG6 = GrdPvcGlue.Rows[i].FindControl("HdnTotalProductPckKG6") as HiddenField;



                    TextBox txtPVcGlue = GrdPvcGlue.Rows[i].FindControl("txtPVcGlue") as TextBox;
                    TextBox txtschemePvcGlue = GrdPvcGlue.Rows[i].FindControl("txtschemePvcGlue") as TextBox;



                    if (txtPVcGlue.Text != "")
                    {

                        XML += "<TABLE>";
                        XML += "<ProductId>" + HdnProductId6.Value + "</ProductId>";
                        XML += "<ProductPckIds>" + HdnProductPckId6.Value + "</ProductPckIds>";
                        XML += "<ProductPck>" + HdnProductPck6.Value + "</ProductPck>";
                        XML += "<PackingNos>" + HdnPackingNos6.Value + "</PackingNos>";
                        XML += "<PackingType>" + HdnPackingType6.Value + "</PackingType>";
                        XML += "<BoxORNos>Box</BoxORNos>";
                        XML += "<PckTotalKg>" + HdnTotalProductPckKG6.Value + "</PckTotalKg>";
                        XML += "<ProductQty>" + txtPVcGlue.Text + "</ProductQty>";
                        XML += "<IsScheme>" + HdnIsScheme6.Value + "</IsScheme>";
                        if (Convert.ToString(txtschemePvcGlue.Text) == "")
                        {
                            XML += "<Scheme></Scheme>";
                        }
                        else
                        {
                            XML += "<Scheme>" + Convert.ToString(txtschemePvcGlue.Text) + "</Scheme>";
                        }
                        XML += "</TABLE>";
                        kk = kk + 1;
                        totalkg = totalkg + Caltotal(Convert.ToDecimal(HdnTotalProductPckKG6.Value), Convert.ToInt32(txtPVcGlue.Text));
                    }
                }

                for (int i = 0; i < grdWoodStrong.Rows.Count; i++)
                {
                    HiddenField HdnProductId7 = grdWoodStrong.Rows[i].FindControl("HdnProductId7") as HiddenField;
                    HiddenField HdnProductPckId7 = grdWoodStrong.Rows[i].FindControl("HdnProductPckId7") as HiddenField;
                    HiddenField HdnProductPck7 = grdWoodStrong.Rows[i].FindControl("HdnProductPck7") as HiddenField;
                    HiddenField HdnPackingNos7 = grdWoodStrong.Rows[i].FindControl("HdnPackingNos7") as HiddenField;
                    HiddenField HdnPackingType7 = grdWoodStrong.Rows[i].FindControl("HdnPackingType7") as HiddenField;
                    HiddenField HdnIsScheme7 = grdWoodStrong.Rows[i].FindControl("HdnIsScheme7") as HiddenField;
                    HiddenField HdnTotalProductPckKG7 = grdWoodStrong.Rows[i].FindControl("HdnTotalProductPckKG7") as HiddenField;



                    TextBox txtWoodStrong = grdWoodStrong.Rows[i].FindControl("txtWoodStrong") as TextBox;
                    TextBox txtschemeWoodStrong = grdWoodStrong.Rows[i].FindControl("txtschemeWoodStrong") as TextBox;


                    if (txtWoodStrong.Text != "")
                    {

                        XML += "<TABLE>";
                        XML += "<ProductId>" + HdnProductId7.Value + "</ProductId>";
                        XML += "<ProductPckIds>" + HdnProductPckId7.Value + "</ProductPckIds>";
                        XML += "<ProductPck>" + HdnProductPck7.Value + "</ProductPck>";
                        XML += "<PackingNos>" + HdnPackingNos7.Value + "</PackingNos>";
                        XML += "<PackingType>" + HdnPackingType7.Value + "</PackingType>";
                        XML += "<BoxORNos>Box</BoxORNos>";
                        XML += "<PckTotalKg>" + HdnTotalProductPckKG7.Value + "</PckTotalKg>";
                        XML += "<ProductQty>" + txtWoodStrong.Text + "</ProductQty>";
                        XML += "<IsScheme>" + HdnIsScheme7.Value + "</IsScheme>";
                        if (Convert.ToString(txtschemeWoodStrong.Text) == "")
                        {
                            XML += "<Scheme></Scheme>";
                        }
                        else
                        {
                            XML += "<Scheme>" + Convert.ToString(txtschemeWoodStrong.Text) + "</Scheme>";
                        }
                        XML += "</TABLE>";
                        kk = kk + 1;
                        totalkg = totalkg + Caltotal(Convert.ToDecimal(HdnTotalProductPckKG7.Value), Convert.ToInt32(txtWoodStrong.Text));
                    }
                }



                for (int i = 0; i < grdEuroEWR.Rows.Count; i++)
                {
                    HiddenField HdnProductId8 = grdEuroEWR.Rows[i].FindControl("HdnProductId8") as HiddenField;
                    HiddenField HdnProductPckId8 = grdEuroEWR.Rows[i].FindControl("HdnProductPckId8") as HiddenField;
                    HiddenField HdnProductPck8 = grdEuroEWR.Rows[i].FindControl("HdnProductPck8") as HiddenField;
                    HiddenField HdnPackingNos8 = grdEuroEWR.Rows[i].FindControl("HdnPackingNos8") as HiddenField;
                    HiddenField HdnPackingType8 = grdEuroEWR.Rows[i].FindControl("HdnPackingType8") as HiddenField;
                    HiddenField HdnIsScheme8 = grdEuroEWR.Rows[i].FindControl("HdnIsScheme8") as HiddenField;
                    HiddenField HdnTotalProductPckKG8 = grdEuroEWR.Rows[i].FindControl("HdnTotalProductPckKG8") as HiddenField;



                    TextBox txtEuroEWR = grdEuroEWR.Rows[i].FindControl("txtEuroEWR") as TextBox;
                    TextBox txtschemeEuroEWR = grdEuroEWR.Rows[i].FindControl("txtschemeEuroEWR") as TextBox;


                    if (txtEuroEWR.Text != "")
                    {

                        XML += "<TABLE>";
                        XML += "<ProductId>" + HdnProductId8.Value + "</ProductId>";
                        XML += "<ProductPckIds>" + HdnProductPckId8.Value + "</ProductPckIds>";
                        XML += "<ProductPck>" + HdnProductPck8.Value + "</ProductPck>";
                        XML += "<PackingNos>" + HdnPackingNos8.Value + "</PackingNos>";
                        XML += "<PackingType>" + HdnPackingType8.Value + "</PackingType>";
                        XML += "<BoxORNos>Box</BoxORNos>";
                        XML += "<PckTotalKg>" + HdnTotalProductPckKG8.Value + "</PckTotalKg>";
                        XML += "<ProductQty>" + txtEuroEWR.Text + "</ProductQty>";
                        XML += "<IsScheme>" + HdnIsScheme8.Value + "</IsScheme>";
                        if (Convert.ToString(txtschemeEuroEWR.Text) == "")
                        {
                            XML += "<Scheme></Scheme>";
                        }
                        else
                        {
                            XML += "<Scheme>" + Convert.ToString(txtschemeEuroEWR.Text) + "</Scheme>";
                        }
                        XML += "</TABLE>";
                        kk = kk + 1;
                        totalkg = totalkg + Caltotal(Convert.ToDecimal(HdnTotalProductPckKG8.Value), Convert.ToInt32(txtEuroEWR.Text));
                    }
                }







                if (kk == 0)
                {
                    return "";
                }
                ViewState["TotalKg"] = totalkg;
                return XML += "</OrderProduct>";

                #endregion
            }
            catch (Exception ex)
            {

                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
                return "";
            }
        }
        public decimal Caltotal(decimal totalkg, int ProductQty)
        {
            decimal totalkgcount = 0;
            try
            {

                totalkgcount = (totalkg * ProductQty);
            }
            catch (Exception)
            {
            }
            return totalkgcount;

        }


        protected void ClearData()
        {
            try
            {
                for (int i = 0; i < gridEUROXTRA.Rows.Count; i++)
                {
                    TextBox txtEUROXTRA = gridEUROXTRA.Rows[i].FindControl("txtEUROXTRA") as TextBox;
                    txtEUROXTRA.Text = "";
                }

                for (int i = 0; i < grdEUROWP.Rows.Count; i++)
                {
                    TextBox txteurowp = grdEUROWP.Rows[i].FindControl("txteurowp") as TextBox;
                    txteurowp.Text = "";
                }

                for (int i = 0; i < grdeuro2in1.Rows.Count; i++)
                {
                    TextBox txteuro2in1 = grdeuro2in1.Rows[i].FindControl("txteuro2in1") as TextBox;
                    txteuro2in1.Text = "";
                }

                for (int i = 0; i < grdExtreme.Rows.Count; i++)
                {
                    TextBox txtExtreme = grdExtreme.Rows[i].FindControl("txtExtreme") as TextBox;
                    txtExtreme.Text = "";
                }

                for (int i = 0; i < grdEuroUltra.Rows.Count; i++)
                {
                    TextBox txtEuroUltra = grdEuroUltra.Rows[i].FindControl("txtEuroUltra") as TextBox;
                    txtEuroUltra.Text = "";
                }

                for (int i = 0; i < GrdPvcGlue.Rows.Count; i++)
                {
                    TextBox txtPVcGlue = GrdPvcGlue.Rows[i].FindControl("txtPVcGlue") as TextBox;
                    txtPVcGlue.Text = "";
                }

                for (int i = 0; i < grdWoodStrong.Rows.Count; i++)
                {
                    TextBox txtWoodStrong = grdWoodStrong.Rows[i].FindControl("txtWoodStrong") as TextBox;
                    txtWoodStrong.Text = "";
                }

                hdOrderId.Value = "";
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        //protected void btnClear_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        ClearData();
        //    }
        //    catch (Exception ex)
        //    {
        //        BA_ErrorLog ObjError = new BA_ErrorLog();
        //        ObjError.INSERT_ErrorLog(ex);
        //    }
        //}

        protected void GetOrderDetails(Int32 OrderId)
        {
            try
            {
                BA_tblOrder ObjOrder = new BA_tblOrder();
                DataTable _dt = new DataTable();
                ObjOrder.OrderID = Convert.ToString(OrderId);
                ObjOrder.GET_RECORDS_FROM_tblOrderByOrderId(ref _dt);

                if (_dt != null)
                {
                    txtOrderDate.Text = Convert.ToDateTime(Convert.ToString(_dt.Rows[0]["OrderDate"])).ToString("dd/MM/yyyy", new CultureInfo("en-GB"));
                    drpOrderStatus.SelectedValue = Convert.ToString(_dt.Rows[0]["OrderStatus"]);
                    txttransport.Text = Convert.ToString(_dt.Rows[0]["Transport"]);
                    txtOther.Text = Convert.ToString(_dt.Rows[0]["Other"]);
                    txtPOP.Text = Convert.ToString(_dt.Rows[0]["POP"]);
                    txtsitedelivery.Text = Convert.ToString(_dt.Rows[0]["SiteDelivery"]);
                    lblTotal.Text = Convert.ToString(_dt.Rows[0]["TotalKgGm"]);
                    hdTotalKgCount.Value = Convert.ToString(_dt.Rows[0]["TotalKgGm"]);
                    hdEditOrderId.Value = Convert.ToString(_dt.Rows[0]["OrderId"]);

                    drpsSalesExe.SelectedValue = Convert.ToString(_dt.Rows[0]["SalesManId"]);

                    txtDealerCode.Text = Convert.ToString(_dt.Rows[0]["DealerCode"]);
                    txtdealernamesearch.Text = Convert.ToString(_dt.Rows[0]["DealerName"]);
                    txtDealerCodeSearch.Text = Convert.ToString(_dt.Rows[0]["DealerCode"]);
                    txtContactName.Text = Convert.ToString(_dt.Rows[0]["ContactName"]);
                    txtDealerName.Text = Convert.ToString(_dt.Rows[0]["DealerName"]);
                    txtAddress.Text = Convert.ToString(_dt.Rows[0]["Address"]);
                    txtArea.Text = Convert.ToString(_dt.Rows[0]["Area"]);
                    txtPhoneNo.Text = Convert.ToString(_dt.Rows[0]["Phone"]);
                    txtGST.Text = Convert.ToString(_dt.Rows[0]["GST"]);
                    txtpincode.Text = Convert.ToString(_dt.Rows[0]["Pincode"]);
                    hdDelaerId.Value = Convert.ToString(_dt.Rows[0]["DealerId"]);


                    DataTable _dtOrder = new DataTable();
                    ObjOrder.GET_RECORDS_FROM_tblOrderByOrderIdDetails(ref _dtOrder);

                    if (_dtOrder != null && _dtOrder.Rows.Count > 0)
                    {
                        for (int i = 0; i < _dtOrder.Rows.Count; i++)
                        {
                            for (int k = 0; k < gridEUROXTRA.Rows.Count; k++)
                            {
                                HiddenField HdnProductPckId1 = gridEUROXTRA.Rows[k].FindControl("HdnProductPckId1") as HiddenField;
                                string strProductPckId = HdnProductPckId1.Value;
                                if (Convert.ToString(_dtOrder.Rows[i]["ProductPckId"]) == strProductPckId)
                                {
                                    TextBox txtEUROXTRA = gridEUROXTRA.Rows[k].FindControl("txtEUROXTRA") as TextBox;
                                    txtEUROXTRA.Text = Convert.ToString(Convert.ToInt32(_dtOrder.Rows[i]["ProductQty"]));

                                    TextBox txtschemeEUROXTRA = gridEUROXTRA.Rows[k].FindControl("txtschemeEUROXTRA") as TextBox;
                                    txtschemeEUROXTRA.Text = Convert.ToString(_dtOrder.Rows[i]["Scheme"]);

                                    HiddenField hdEUROXTRA = gridEUROXTRA.Rows[k].FindControl("hdEUROXTRA") as HiddenField;
                                    hdEUROXTRA.Value = Convert.ToString(Convert.ToInt32(_dtOrder.Rows[i]["ProductQty"]));




                                }
                            }

                            for (int k = 0; k < grdEUROWP.Rows.Count; k++)
                            {
                                HiddenField HdnProductPckId2 = grdEUROWP.Rows[k].FindControl("HdnProductPckId2") as HiddenField;
                                string strProductPckId = HdnProductPckId2.Value;
                                if (Convert.ToString(_dtOrder.Rows[i]["ProductPckId"]) == strProductPckId)
                                {
                                    TextBox txteurowp = grdEUROWP.Rows[k].FindControl("txteurowp") as TextBox;
                                    HiddenField hdeurowp = grdEUROWP.Rows[k].FindControl("hdeurowp") as HiddenField;
                                    txteurowp.Text = Convert.ToString(Convert.ToInt32(_dtOrder.Rows[i]["ProductQty"]));
                                    hdeurowp.Value = Convert.ToString(Convert.ToInt32(_dtOrder.Rows[i]["ProductQty"]));

                                    TextBox txtschemeEUROWP = grdEUROWP.Rows[k].FindControl("txtschemeEUROWP") as TextBox;
                                    txtschemeEUROWP.Text = Convert.ToString(_dtOrder.Rows[i]["Scheme"]);
                                }
                            }

                            for (int k = 0; k < grdeuro2in1.Rows.Count; k++)
                            {
                                HiddenField HdnProductPckId3 = grdeuro2in1.Rows[k].FindControl("HdnProductPckId3") as HiddenField;
                                string strProductPckId = HdnProductPckId3.Value;
                                if (Convert.ToString(_dtOrder.Rows[i]["ProductPckId"]) == strProductPckId)
                                {
                                    TextBox txteuro2in1 = grdeuro2in1.Rows[k].FindControl("txteuro2in1") as TextBox;
                                    HiddenField hdeuro2in1 = grdeuro2in1.Rows[k].FindControl("hdeuro2in1") as HiddenField;
                                    txteuro2in1.Text = Convert.ToString(Convert.ToInt32(_dtOrder.Rows[i]["ProductQty"]));
                                    hdeuro2in1.Value = Convert.ToString(Convert.ToInt32(_dtOrder.Rows[i]["ProductQty"]));

                                    TextBox txtschemeeuro2in1 = grdeuro2in1.Rows[k].FindControl("txtschemeeuro2in1") as TextBox;
                                    txtschemeeuro2in1.Text = Convert.ToString(_dtOrder.Rows[i]["Scheme"]);
                                }
                            }

                            for (int k = 0; k < grdExtreme.Rows.Count; k++)
                            {
                                HiddenField HdnProductPckId4 = grdExtreme.Rows[k].FindControl("HdnProductPckId4") as HiddenField;
                                string strProductPckId = HdnProductPckId4.Value;
                                if (Convert.ToString(_dtOrder.Rows[i]["ProductPckId"]) == strProductPckId)
                                {
                                    TextBox txtExtreme = grdExtreme.Rows[k].FindControl("txtExtreme") as TextBox;
                                    HiddenField hdExtreme = grdExtreme.Rows[k].FindControl("hdExtreme") as HiddenField;
                                    txtExtreme.Text = Convert.ToString(Convert.ToInt32(_dtOrder.Rows[i]["ProductQty"]));
                                    hdExtreme.Value = Convert.ToString(Convert.ToInt32(_dtOrder.Rows[i]["ProductQty"]));

                                    TextBox txtschemeExtreme = grdExtreme.Rows[k].FindControl("txtschemeExtreme") as TextBox;
                                    txtschemeExtreme.Text = Convert.ToString(_dtOrder.Rows[i]["Scheme"]);
                                }
                            }

                            for (int k = 0; k < grdEuroUltra.Rows.Count; k++)
                            {
                                HiddenField HdnProductPckId5 = grdEuroUltra.Rows[k].FindControl("HdnProductPckId5") as HiddenField;
                                string strProductPckId = HdnProductPckId5.Value;
                                if (Convert.ToString(_dtOrder.Rows[i]["ProductPckId"]) == strProductPckId)
                                {
                                    TextBox txtEuroUltra = grdEuroUltra.Rows[k].FindControl("txtEuroUltra") as TextBox;
                                    HiddenField hdEuroUltra = grdEuroUltra.Rows[k].FindControl("hdEuroUltra") as HiddenField;
                                    txtEuroUltra.Text = Convert.ToString(Convert.ToInt32(_dtOrder.Rows[i]["ProductQty"]));
                                    hdEuroUltra.Value = Convert.ToString(Convert.ToInt32(_dtOrder.Rows[i]["ProductQty"]));

                                    TextBox txtschemeEuroUltra = grdEuroUltra.Rows[k].FindControl("txtschemeEuroUltra") as TextBox;
                                    txtschemeEuroUltra.Text = Convert.ToString(_dtOrder.Rows[i]["Scheme"]);
                                }
                            }

                            for (int k = 0; k < GrdPvcGlue.Rows.Count; k++)
                            {
                                HiddenField HdnProductPckId6 = GrdPvcGlue.Rows[k].FindControl("HdnProductPckId6") as HiddenField;
                                string strProductPckId = HdnProductPckId6.Value;
                                if (Convert.ToString(_dtOrder.Rows[i]["ProductPckId"]) == strProductPckId)
                                {
                                    TextBox txtPVcGlue = GrdPvcGlue.Rows[k].FindControl("txtPVcGlue") as TextBox;
                                    HiddenField hdPVcGlue = GrdPvcGlue.Rows[k].FindControl("hdPVcGlue") as HiddenField;
                                    txtPVcGlue.Text = Convert.ToString(Convert.ToInt32(_dtOrder.Rows[i]["ProductQty"]));
                                    hdPVcGlue.Value = Convert.ToString(Convert.ToInt32(_dtOrder.Rows[i]["ProductQty"]));

                                    TextBox txtschemePvcGlue = GrdPvcGlue.Rows[k].FindControl("txtschemePvcGlue") as TextBox;
                                    txtschemePvcGlue.Text = Convert.ToString(_dtOrder.Rows[i]["Scheme"]);
                                }
                            }

                            for (int k = 0; k < grdWoodStrong.Rows.Count; k++)
                            {
                                HiddenField HdnProductPckId7 = grdWoodStrong.Rows[k].FindControl("HdnProductPckId7") as HiddenField;
                                string strProductPckId = HdnProductPckId7.Value;
                                if (Convert.ToString(_dtOrder.Rows[i]["ProductPckId"]) == strProductPckId)
                                {
                                    TextBox txtWoodStrong = grdWoodStrong.Rows[k].FindControl("txtWoodStrong") as TextBox;
                                    HiddenField hdWoodStrong = grdWoodStrong.Rows[k].FindControl("hdWoodStrong") as HiddenField;
                                    txtWoodStrong.Text = Convert.ToString(Convert.ToInt32(_dtOrder.Rows[i]["ProductQty"]));
                                    hdWoodStrong.Value = Convert.ToString(Convert.ToInt32(_dtOrder.Rows[i]["ProductQty"]));

                                    TextBox txtschemeWoodStrong = grdWoodStrong.Rows[k].FindControl("txtschemeWoodStrong") as TextBox;
                                    txtschemeWoodStrong.Text = Convert.ToString(_dtOrder.Rows[i]["Scheme"]);
                                }
                            }

                            for (int k = 0; k < grdEuroEWR.Rows.Count; k++)
                            {
                                HiddenField HdnProductPckId8 = grdEuroEWR.Rows[k].FindControl("HdnProductPckId8") as HiddenField;
                                string strProductPckId = HdnProductPckId8.Value;
                                if (Convert.ToString(_dtOrder.Rows[i]["ProductPckId"]) == strProductPckId)
                                {
                                    TextBox txtEuroEWR = grdEuroEWR.Rows[k].FindControl("txtEuroEWR") as TextBox;
                                    HiddenField hdEuroEWR = grdEuroEWR.Rows[k].FindControl("hdEuroEWR") as HiddenField;
                                    txtEuroEWR.Text = Convert.ToString(Convert.ToInt32(_dtOrder.Rows[i]["ProductQty"]));
                                    hdEuroEWR.Value = Convert.ToString(Convert.ToInt32(_dtOrder.Rows[i]["ProductQty"]));

                                    TextBox txtschemeWoodStrong = grdEuroEWR.Rows[k].FindControl("txtschemeEuroEWR") as TextBox;
                                    txtschemeWoodStrong.Text = Convert.ToString(_dtOrder.Rows[i]["Scheme"]);
                                }
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }


        protected void btnback_Click(object sender, EventArgs e)
        {
            if (Convert.ToString(ViewState["BackButton"]) == "Y")
                Response.Redirect("OrderList.aspx", false);
            else
                Response.Redirect("Dashboard.aspx", false);

        }


        protected void gridEUROXTRA_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField HdnIsScheme1 = e.Row.FindControl("HdnIsScheme1") as HiddenField;
                if (Convert.ToBoolean(HdnIsScheme1.Value))
                {
                    Label lblschEUROXTRA = e.Row.FindControl("lblschEUROXTRA") as Label;
                    lblschEUROXTRA.Visible = true;

                    TextBox txtschemeEUROXTRA = e.Row.FindControl("txtschemeEUROXTRA") as TextBox;
                    txtschemeEUROXTRA.Visible = true;

                }
                HiddenField HdnProductPck1 = e.Row.FindControl("HdnProductPck1") as HiddenField;
                Label lblpkgboxnoEUROXTRA = e.Row.FindControl("lblpkgboxnoEUROXTRA") as Label;

                HiddenField HdnPackingType1 = e.Row.FindControl("HdnPackingType1") as HiddenField;
                if (Convert.ToString(HdnPackingType1.Value) == "kg")
                {
                    if (Convert.ToDecimal(HdnProductPck1.Value) == 20)
                    {
                        lblpkgboxnoEUROXTRA.Text = "BKT";
                    }
                    if (Convert.ToDecimal(HdnProductPck1.Value) > 20)
                    {
                        lblpkgboxnoEUROXTRA.Text = "CRB";
                    }
                }
            }
        }

        protected void grdEUROWP_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField HdnIsScheme1 = e.Row.FindControl("HdnIsScheme2") as HiddenField;
                if (Convert.ToBoolean(HdnIsScheme1.Value))
                {
                    Label lblschEUROWP = e.Row.FindControl("lblschEUROWP") as Label;
                    lblschEUROWP.Visible = true;

                    TextBox txtschemeEUROWP = e.Row.FindControl("txtschemeEUROWP") as TextBox;
                    txtschemeEUROWP.Visible = true;

                }
                HiddenField HdnProductPck2 = e.Row.FindControl("HdnProductPck2") as HiddenField;
                Label lblpkgboxnoeurowp = e.Row.FindControl("lblpkgboxnoeurowp") as Label;
                HiddenField HdnPackingType2 = e.Row.FindControl("HdnPackingType2") as HiddenField;
                if (Convert.ToString(HdnPackingType2.Value) == "kg")
                {
                    if (Convert.ToDecimal(HdnProductPck2.Value) == 20)
                    {
                        lblpkgboxnoeurowp.Text = "BKT";
                    }
                    if (Convert.ToDecimal(HdnProductPck2.Value) > 20)
                    {
                        lblpkgboxnoeurowp.Text = "CRB";
                    }
                }
            }
        }

        protected void grdeuro2in1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField HdnIsScheme1 = e.Row.FindControl("HdnIsScheme3") as HiddenField;
                if (Convert.ToBoolean(HdnIsScheme1.Value))
                {
                    Label lblscheuro2in1 = e.Row.FindControl("lblscheuro2in1") as Label;
                    lblscheuro2in1.Visible = true;

                    TextBox txtschemeeuro2in1 = e.Row.FindControl("txtschemeeuro2in1") as TextBox;
                    txtschemeeuro2in1.Visible = true;

                }

                HiddenField HdnProductPck3 = e.Row.FindControl("HdnProductPck3") as HiddenField;
                Label lblpkgboxnoeuro2in1 = e.Row.FindControl("lblpkgboxnoeuro2in1") as Label;
                HiddenField HdnPackingType3 = e.Row.FindControl("HdnPackingType3") as HiddenField;
                if (Convert.ToString(HdnPackingType3.Value) == "kg")
                {
                    if (Convert.ToDecimal(HdnProductPck3.Value) == 20)
                    {
                        lblpkgboxnoeuro2in1.Text = "BKT";
                    }
                    if (Convert.ToDecimal(HdnProductPck3.Value) > 20)
                    {
                        lblpkgboxnoeuro2in1.Text = "CRB";
                    }
                }
            }

        }

        protected void grdExtreme_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField HdnIsScheme1 = e.Row.FindControl("HdnIsScheme4") as HiddenField;
                if (Convert.ToBoolean(HdnIsScheme1.Value))
                {
                    Label lblschExtreme = e.Row.FindControl("lblschExtreme") as Label;
                    lblschExtreme.Visible = true;

                    TextBox txtschemeExtreme = e.Row.FindControl("txtschemeExtreme") as TextBox;
                    txtschemeExtreme.Visible = true;

                }
                HiddenField HdnProductPck4 = e.Row.FindControl("HdnProductPck4") as HiddenField;
                Label lblpkgboxnoExtreme = e.Row.FindControl("lblpkgboxnoExtreme") as Label;
                HiddenField HdnPackingType4 = e.Row.FindControl("HdnPackingType4") as HiddenField;
                if (Convert.ToString(HdnPackingType4.Value) == "kg")
                {
                    if (Convert.ToDecimal(HdnProductPck4.Value) == 20)
                    {
                        lblpkgboxnoExtreme.Text = "BKT";
                    }
                    if (Convert.ToDecimal(HdnProductPck4.Value) > 20)
                    {
                        lblpkgboxnoExtreme.Text = "CRB";
                    }
                }
            }

        }

        protected void grdEuroUltra_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField HdnIsScheme1 = e.Row.FindControl("HdnIsScheme5") as HiddenField;
                if (Convert.ToBoolean(HdnIsScheme1.Value))
                {
                    Label lblschEuroUltra = e.Row.FindControl("lblschEuroUltra") as Label;
                    lblschEuroUltra.Visible = true;

                    TextBox txtschemeEuroUltra = e.Row.FindControl("txtschemeEuroUltra") as TextBox;
                    txtschemeEuroUltra.Visible = true;

                }
                HiddenField HdnProductPck5 = e.Row.FindControl("HdnProductPck5") as HiddenField;
                Label llblpkgboxnoEuroUltra = e.Row.FindControl("llblpkgboxnoEuroUltra") as Label;
                HiddenField HdnPackingType5 = e.Row.FindControl("HdnPackingType5") as HiddenField;
                if (Convert.ToString(HdnPackingType5.Value) == "kg")
                {

                    if (Convert.ToDecimal(HdnProductPck5.Value) == 20)
                    {
                        llblpkgboxnoEuroUltra.Text = "BKT";
                    }
                    if (Convert.ToDecimal(HdnProductPck5.Value) > 20)
                    {
                        llblpkgboxnoEuroUltra.Text = "CRB";
                    }
                }
            }
        }

        protected void GrdPvcGlue_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField HdnIsScheme1 = e.Row.FindControl("HdnIsScheme6") as HiddenField;
                if (Convert.ToBoolean(HdnIsScheme1.Value))
                {
                    Label lblschPvcGlue = e.Row.FindControl("lblschPvcGlue") as Label;
                    lblschPvcGlue.Visible = true;

                    TextBox txtschemePvcGlue = e.Row.FindControl("txtschemePvcGlue") as TextBox;
                    txtschemePvcGlue.Visible = true;

                }
                HiddenField HdnProductPck6 = e.Row.FindControl("HdnProductPck6") as HiddenField;
                Label lblpkgboxnoPVcGlue = e.Row.FindControl("lblpkgboxnoPVcGlue") as Label;
                HiddenField HdnPackingType6 = e.Row.FindControl("HdnPackingType6") as HiddenField;
                if (Convert.ToString(HdnPackingType6.Value) == "kg")
                {

                    if (Convert.ToDecimal(HdnProductPck6.Value) == 20)
                    {
                        lblpkgboxnoPVcGlue.Text = "BKT";
                    }
                    if (Convert.ToDecimal(HdnProductPck6.Value) > 20)
                    {
                        lblpkgboxnoPVcGlue.Text = "CRB";
                    }
                }
            }
        }

        protected void grdWoodStrong_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField HdnIsScheme1 = e.Row.FindControl("HdnIsScheme7") as HiddenField;
                if (Convert.ToBoolean(HdnIsScheme1.Value))
                {
                    Label lblschWoodStrong = e.Row.FindControl("lblschWoodStrong") as Label;
                    lblschWoodStrong.Visible = true;

                    TextBox txtschemeWoodStrong = e.Row.FindControl("txtschemeWoodStrong") as TextBox;
                    txtschemeWoodStrong.Visible = true;

                }
                HiddenField HdnProductPck7 = e.Row.FindControl("HdnProductPck7") as HiddenField;
                Label lblpkgboxnoWoodStrong = e.Row.FindControl("lblpkgboxnoWoodStrong") as Label;
                HiddenField HdnPackingType7 = e.Row.FindControl("HdnPackingType7") as HiddenField;
                if (Convert.ToString(HdnPackingType7.Value) == "kg")
                {
                    if (Convert.ToDecimal(HdnProductPck7.Value) == 20)
                    {
                        lblpkgboxnoWoodStrong.Text = "BKT";
                    }
                    if (Convert.ToDecimal(HdnProductPck7.Value) > 20)
                    {
                        lblpkgboxnoWoodStrong.Text = "CRB";
                    }
                }
            }
        }

        protected void grdEuroEWR_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField HdnIsScheme1 = e.Row.FindControl("HdnIsScheme8") as HiddenField;
                if (Convert.ToBoolean(HdnIsScheme1.Value))
                {
                    Label lblschEUROEWR = e.Row.FindControl("lblschEuroEWR") as Label;
                    lblschEUROEWR.Visible = true;

                    TextBox txtschemeEUROEWR = e.Row.FindControl("txtschemeEuroEWR") as TextBox;
                    txtschemeEUROEWR.Visible = true;

                }
                HiddenField HdnProductPck1 = e.Row.FindControl("HdnProductPck8") as HiddenField;
                Label lblpkgboxnoEUROEWR = e.Row.FindControl("llblpkgboxnoEuroEWR") as Label;

                HiddenField HdnPackingType1 = e.Row.FindControl("HdnPackingType8") as HiddenField;
                if (Convert.ToString(HdnPackingType1.Value) == "kg")
                {
                    if (Convert.ToDecimal(HdnProductPck1.Value) == 20)
                    {
                        lblpkgboxnoEUROEWR.Text = "BKT";
                    }
                    if (Convert.ToDecimal(HdnProductPck1.Value) > 20)
                    {
                        lblpkgboxnoEUROEWR.Text = "CRB";
                    }
                }
            }
        }
    }
}