using Newtonsoft.Json;
using OrderApp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace OrderApp
{
    [RoutePrefix("api")]
    public class ApisController : ApiController
    {
        public ApisController()
        {

        }

        #region Login Page
        [HttpGet]
        [Route("Login/{userName}/{password}")]
        public WSResponseObject LoginUser(string userName, string password)
        {
            System.Web.Script.Serialization.JavaScriptSerializer jSearializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            WSResponseObject response = new WSResponseObject();
            BA_tblUser objUser = new BA_tblUser();
            var blankarray = new BA_tblUser();
            dynamic blankData = "";
            response.total_records = 0;
            response.Message = "";
            try
            {

                if (userName.Trim() == "")
                {
                    response.status = 1;
                    objUser = newobj();
                    response.Data = objUser;
                    response.Message = "Please Enter UserName";

                }
                else if (password.Trim() == "")
                {
                    response.status = 1;
                    objUser = newobj();
                    response.Data = objUser;
                    response.Message = "Please Enter Password";
                }


                Common cmn = new Common();
                DataTable dt = new DataTable();
                objUser.UserName = userName.Trim();
                objUser.Pwd = cmn.Encrypt(password.Trim());
                objUser.GET_RECORDS_FROM_tblUser_Login_API(ref dt);
                if (dt != null)
                {

                    if (dt.Rows.Count > 0)
                    {


                        if (Convert.ToString(dt.Rows[0]["UserType"]).ToLower() == "salesman")
                        {

                            objUser.UserID = Convert.ToInt32(dt.Rows[0]["UserID"]);
                            objUser.UserName = Convert.ToString(dt.Rows[0]["UserName"]);
                            objUser.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
                            objUser.MiddleName = Convert.ToString(dt.Rows[0]["MiddleName"]);
                            objUser.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
                            objUser.UserType = Convert.ToString(dt.Rows[0]["UserType"]);
                            objUser.PhoneNo = Convert.ToString(dt.Rows[0]["PhoneNo"]);
                            objUser.MobileNo = Convert.ToString(dt.Rows[0]["MobileNo"]);
                            objUser.CreateData = "";
                            objUser.UpdateDate = "";

                            response.status = 0;
                            response.Message = "Success";
                            response.Data = objUser;
                        }
                        else
                        {



                            response.status = 1;
                            response.Message = "you are not access this app.";
                            objUser = newobj();
                            response.Data = objUser;
                        }
                    }
                    else
                    {

                        response.status = 1;
                        response.Message = "Username not found.";
                        objUser = newobj();
                        response.Data = objUser;
                    }


                }
                else
                {

                    response.status = 1;
                    response.Message = "Username or password is not valid.";
                    //response.Data = blankarray;
                    objUser = newobj();
                    response.Data = objUser;

                }
            }
            catch (Exception ex)
            {

                response.status = 1;
                response.Message = (("Error LoginUser: " + ex.Message) ?? "");
                objUser = newobj();
                response.Data = objUser;

            }

            return response;
        }

        public BA_tblUser newobj()
        {
            BA_tblUser objUser = new BA_tblUser();
            objUser.UserID = 0;
            objUser.UserName = "";
            objUser.Pwd = "";
            objUser.FirstName = "";
            objUser.MiddleName = "";
            objUser.LastName = "";
            objUser.UserType = "";
            objUser.PhoneNo = "";
            objUser.MobileNo = "";
            objUser.CreateData = "";
            objUser.UpdateDate = "";
            return objUser;
        }
        #endregion

        #region OrderList
        [HttpGet]
        [Route("OrderList/{dFrom}/{dTo}/{UserId}")]
        public WSResponseObject OrderList(string dFrom, string dTo, int UserId)
        {
            WSResponseObject response = new WSResponseObject();
            var blankarray = new List<string>();
            response.total_records = 0;
            response.Message = "";
            try
            {
                DateTime FromDt = Convert.ToDateTime(dFrom, CultureInfo.InvariantCulture);
                DateTime ToDt = Convert.ToDateTime(dTo, CultureInfo.InvariantCulture);

                DataTable dt = new DataTable();
                BA_tblOrder objBA_tblOrder = new BA_tblOrder();
                objBA_tblOrder.FromDate = FromDt;

                DateTime ToDate = Convert.ToDateTime(dTo);
                ToDate = Convert.ToDateTime(new DateTime(ToDate.Year, ToDate.Month, ToDate.Day, 23, 59, 59).ToString("MM/dd/yyyy hh:mm:ss tt"));
                objBA_tblOrder.ToDate = ToDate;
                objBA_tblOrder.CreateBy = UserId;

                objBA_tblOrder.GET_Order_API(ref dt);


                if (dt != null && dt.Rows.Count > 0)
                {
                    response.status = 0;
                    response.Message = "Success";
                    response.total_records = dt.Rows.Count;
                    response.Data = dt;
                }
                else
                {
                    response.status = 1;
                    response.Message = "No record found";
                    response.Data = blankarray;
                }

            }
            catch (Exception ex)
            {
                response.status = 1;
                response.Message = (("Error OrderList: " + ex.Message) ?? "");
                response.Data = blankarray;
            }
            return response;
        }
        #endregion

        public DateTime getDate(string date)
        {
            DateTime dt;
            try
            {
                //  dt = DateTime.ParseExact(date, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                //var formats = new[] { "dd-MM-yyyy" };

              //  dt = DateTime.ParseExact(date.Replace("-", "/"), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                dt = DateTime.ParseExact(date, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                return dt;
            }
            catch (Exception)
            {
                dt = new DateTime();
                return dt;
            }
        }

        #region FreeOrderList
        [HttpGet]
        [Route("FreeOrderList/{dFrom}/{dTo}/{UserId}")]
        public WSResponseObject FreeOrderList(string dFrom, string dTo, int UserId)
        {
            WSResponseObject response = new WSResponseObject();
            var blankarray = new List<string>();
            response.total_records = 0;
            response.Message = "";
            try
            {
                DateTime FromDt = Convert.ToDateTime(dFrom, CultureInfo.InvariantCulture);
                DateTime ToDt = Convert.ToDateTime(dTo, CultureInfo.InvariantCulture);

                DataTable dt = new DataTable();
                BA_tblOrder objBA_tblOrder = new BA_tblOrder();
                objBA_tblOrder.FromDate = FromDt;

                DateTime ToDate = Convert.ToDateTime(dTo);
                ToDate = Convert.ToDateTime(new DateTime(ToDate.Year, ToDate.Month, ToDate.Day, 23, 59, 59).ToString("MM/dd/yyyy hh:mm:ss tt"));
                objBA_tblOrder.ToDate = ToDate;
                objBA_tblOrder.CreateBy = UserId;

            
                objBA_tblOrder.GET_FreeOrder_API(ref dt);


                if (dt != null && dt.Rows.Count > 0)
                {
                    response.status = 0;
                    response.Message = "Success";
                    response.total_records = dt.Rows.Count;
                    response.Data = dt;
                }
                else
                {
                    response.status = 1;
                    response.Message = "No record found";
                    response.Data = blankarray;
                }

            }
            catch (Exception ex)
            {
                response.status = 1;
                response.Message = (("Error OrderList: " + ex.Message) ?? "");
                response.Data = blankarray;
            }
            return response;
        }
        #endregion

        #region OrderList
        [HttpGet]
        [Route("OrderView/{OrderId}")]
        public WSResponseObject OrderView(int OrderId)
        {
            WSResponseObject response = new WSResponseObject();
            var blankarray = new List<string>();
            response.total_records = 0;
            response.Message = "";
            try
            {

                DataSet ds = new DataSet();
                BA_tblOrder objBA_tblOrder = new BA_tblOrder();

                objBA_tblOrder.OrderID = Convert.ToString(OrderId);

                objBA_tblOrder.GET_OrderView_API(ref ds);


                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    string json = Common.GetJSON(ds);
                    response.status = 0;
                    response.Message = "Success";
                    response.total_records = 1;
                    response.Data = ds;
                }
                else
                {
                    response.status = 1;
                    response.Message = "No record found";
                    response.Data = blankarray;
                }

            }
            catch (Exception ex)
            {
                response.status = 1;
                response.Message = (("Error OrderList: " + ex.Message) ?? "");
                response.Data = blankarray;
            }
            return response;
        }
        #endregion
        
        #region Free OrderList Get
        [HttpGet]
        [Route("FreeOrderView/{OrderId}")]
        public WSResponseObject FreeOrderView(int OrderId)
        {
            WSResponseObject response = new WSResponseObject();
            var blankarray = new List<string>();
            response.total_records = 0;
            response.Message = "";
            try
            {

                DataSet ds = new DataSet();
                BA_tblOrder objBA_tblOrder = new BA_tblOrder();

                objBA_tblOrder.OrderID = Convert.ToString(OrderId);

                objBA_tblOrder.GET_FreeOrderView_API(ref ds);
                DataTable dts = ds.Tables[0];
                
             bool ischange=  ChangeColumnDataType(dts, "OrderDate", typeof(string));
               ChangeColumnDataType(dts, "PurchaseDurationFromDate", typeof(string));
                ChangeColumnDataType(dts, "PurchaseDurationToDate", typeof(string));
                foreach (DataRow row in ds.Tables[0].Rows)
                {

                    DateTime OrderDate = DateTime.Parse(row["OrderDate"].ToString());
                    row["OrderDate"] = OrderDate.ToString("yyyy-MM-dd");
                    DateTime PurchaseDurationFromDate = DateTime.Parse(row["PurchaseDurationFromDate"].ToString());
                    row["PurchaseDurationFromDate"] = PurchaseDurationFromDate.ToString("yyyy-MM-dd");
                    DateTime PurchaseDurationToDate = DateTime.Parse(row["PurchaseDurationToDate"].ToString());
                    row["PurchaseDurationToDate"] = PurchaseDurationToDate.ToString("yyyy-MM-dd");
                }
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    string json = Common.GetJSON(ds);
                    response.status = 0;
                    response.Message = "Success";
                    response.total_records = 1;
                    response.Data = ds;
                }
                else
                {
                    response.status = 1;
                    response.Message = "No record found";
                    response.Data = blankarray;
                }

            }
            catch (Exception ex)
            {
                response.status = 1;
                response.Message = (("Error OrderList: " + ex.Message) ?? "");
                response.Data = blankarray;
            }
            return response;
        }
        #endregion

        public static bool ChangeColumnDataType(DataTable table, string columnname, Type newtype)
        {
            if (table.Columns.Contains(columnname) == false)
                return false;

            DataColumn column = table.Columns[columnname];
            if (column.DataType == newtype)
                return true;

            try
            {
                DataColumn newcolumn = new DataColumn("temporary", newtype);
                table.Columns.Add(newcolumn);
                foreach (DataRow row in table.Rows)
                {
                    try
                    {
                        row["temporary"] = Convert.ChangeType(row[columnname], newtype);
                    }
                    catch
                    {
                    }
                }
                table.Columns.Remove(columnname);
                newcolumn.ColumnName = columnname;
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        #region Add Dealer
        [HttpPost]
        [Route("AddDealer")]//A For Add And E For Edit
        public WSResponseObject AddDealer(BA_tblDealer list)
        {
            WSResponseObject response = new WSResponseObject();
            var blankarray = new BA_tblDealer();
            response.total_records = 0;
            response.Message = "";
            try
            {

                BA_tblDealer ObjDealer = new BA_tblDealer();
                Common Cmn = new Common();
                ObjDealer = list;
                //ObjDealer.DealerName = list.DealerName;
                //ObjDealer.Address = list.Address;
                //ObjDealer.Area = list.Area;
                //ObjDealer.GST = list.GST;
                //ObjDealer.Phone = list.Phone;
                //ObjDealer.Transport = list.Transport;
                //ObjDealer.CreateBy = list.CreateBy;
                //ObjDealer.Isdeleted = false;
                //ObjDealer.UpdateBy = list.CreateBy;

                int dealerid = ObjDealer.INSERT_tblDealer_api();
                if (dealerid > 0)
                {
                    DataTable dtdeal = new DataTable();
                    ObjDealer.DealerID = dealerid;
                    ObjDealer.GET_RECORDS_FROM_tblDealer(ref dtdeal);
                    if (dtdeal != null && dtdeal.Rows.Count > 0)
                    {

                        ObjDealer.DealerID = Convert.ToInt32(dtdeal.Rows[0]["DealerID"]);
                        ObjDealer.DealerCode = Convert.ToString(dtdeal.Rows[0]["DealerCode"]);
                        ObjDealer.DealerName = Convert.ToString(dtdeal.Rows[0]["DealerName"]);
                        ObjDealer.ContactName = Convert.ToString(dtdeal.Rows[0]["ContactName"]);
                        ObjDealer.Address = Convert.ToString(dtdeal.Rows[0]["Address"]);
                        ObjDealer.Area = Convert.ToString(dtdeal.Rows[0]["Area"]);
                        ObjDealer.Pincode = Convert.ToString(dtdeal.Rows[0]["Pincode"]);
                        ObjDealer.Phone = Convert.ToString(dtdeal.Rows[0]["Phone"]);
                        ObjDealer.GST = Convert.ToString(dtdeal.Rows[0]["GST"]);
                        ObjDealer.CreateDate = "";
                        ObjDealer.UpdateDate = "";
                        if (dtdeal.Rows.Count > 0)
                        {
                            response.status = 0;
                            response.Message = "Success";
                            response.Data = ObjDealer;
                        }
                        else
                        {
                            response.status = 1;
                            response.Message = "Dealer not found.";
                            ObjDealer = retrundealer();
                            response.Data = ObjDealer;
                        }
                    }
                    else
                    {
                        response.status = 1;
                        response.Message = "Dealer does not exist.";
                        ObjDealer = retrundealer();
                        response.Data = ObjDealer;
                    }


                }
                else
                {
                    response.status = 0;
                    response.Message = "Record could not able to store, please try again.";
                    response.Data = blankarray;
                }
            }
            catch (Exception ex)
            {
                response.status = 1;
                response.Message = (("Error AddDealer: " + ex.Message) ?? "");
                response.Data = blankarray;
            }
            return response;
        }
        #endregion
        
        #region View Dealer
        [HttpGet]
        [Route("ViewDealer/{Dealercode}")]
        public WSResponseObject ViewDealer(string Dealercode)
        {
            System.Web.Script.Serialization.JavaScriptSerializer jSearializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            WSResponseObject response = new WSResponseObject();
            BA_tblDealer ObjDealer = new BA_tblDealer();
            DataTable dt = new DataTable();

            //var blankarray = new BA_tblDealer();
            var blankarray = new List<string>();
            response.total_records = 0;
            response.Message = "";
            try
            {

                if (Dealercode.Trim() == "")
                {
                    response.status = 1;
                    ObjDealer = retrundealer();
                    response.Data = ObjDealer;
                    response.Message = "Please Enter Dealercode";
                }



                ObjDealer.DealerCode = Dealercode;
                ObjDealer.GET_RECORDS_FROM_tblDealer_ByCode(ref dt);

                if (dt != null && dt.Rows.Count > 0)
                {

                    ObjDealer.DealerID = Convert.ToInt32(dt.Rows[0]["DealerID"]);
                    ObjDealer.DealerCode = Convert.ToString(dt.Rows[0]["DealerCode"]);
                    ObjDealer.DealerName = Convert.ToString(dt.Rows[0]["DealerName"]);
                    ObjDealer.ContactName = Convert.ToString(dt.Rows[0]["ContactName"]);
                    ObjDealer.Address = Convert.ToString(dt.Rows[0]["Address"]);
                    ObjDealer.Area = Convert.ToString(dt.Rows[0]["Area"]);
                    ObjDealer.Pincode = Convert.ToString(dt.Rows[0]["Pincode"]);
                    ObjDealer.Phone = Convert.ToString(dt.Rows[0]["Phone"]);
                    ObjDealer.GST = Convert.ToString(dt.Rows[0]["GST"]);
                    ObjDealer.CreateDate = "";
                    ObjDealer.UpdateDate = "";
                    if (dt.Rows.Count > 0)
                    {
                        response.status = 0;
                        response.Message = "Success";
                        response.Data = ObjDealer;
                    }
                    else
                    {
                        response.status = 1;
                        response.Message = "Dealer not found.";
                        ObjDealer = retrundealer();
                        response.Data = ObjDealer;
                    }
                }
                else
                {
                    response.status = 1;
                    response.Message = "Dealer does not exist.";
                    ObjDealer = retrundealer();
                    response.Data = ObjDealer;
                }
            }
            catch (Exception ex)
            {
                response.status = 1;
                response.Message = (("Error ViewDealer : " + ex.Message) ?? "");

                ObjDealer = retrundealer();
                response.Data = ObjDealer;
            }
            return response;
        }


        public BA_tblDealer retrundealer()
        {
            BA_tblDealer ObjDealer = new BA_tblDealer();
            ObjDealer.DealerID = 0;
            ObjDealer.DealerCode = "";
            ObjDealer.DealerName = "";
            ObjDealer.Address = "";
            ObjDealer.Area = "";
            ObjDealer.Phone = "";
            ObjDealer.ContactName = "";
            ObjDealer.Pincode = "";
            ObjDealer.GST = "";
            ObjDealer.CreateDate = "";
            ObjDealer.UpdateDate = "";
            return ObjDealer;
        }
        #endregion

        #region ProductList
        [HttpGet]
        [Route("ProductList")]
        public WSResponseObject ProductList()
        {
            WSResponseObject response = new WSResponseObject();
            var blankarray = new List<string>();
            response.total_records = 0;
            response.Message = "";
            try
            {
                DataTable dt = new DataTable();
                BA_tblProduct objUser = new BA_tblProduct();
                objUser.SELECT_ALL_tblProduct_API(ref dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    response.status = 0;
                    response.Message = "Success";
                    response.total_records = dt.Rows.Count;
                    response.Data = dt;
                }
                else
                {
                    response.status = 1;
                    response.Message = "No record found";
                    response.Data = blankarray;
                }

            }
            catch (Exception ex)
            {
                response.status = 1;
                response.Message = (("Error ProductList: " + ex.Message) ?? "");
                response.Data = blankarray;
            }
            return response;
        }
        #endregion
        
        #region Product package details
        [HttpGet]
        [Route("Productpackage/{ProductId}")]
        public WSResponseObject ProductpackageList(int ProductId)
        {
            WSResponseObject response = new WSResponseObject();
            var blankarray = new List<string>();
            response.total_records = 0;
            response.Message = "";
            try
            {

                BA_tblProductPacking ObjProductPck = new BA_tblProductPacking();
                DataTable dt = new DataTable();

                ObjProductPck.ProductID = Convert.ToString(ProductId);
                ObjProductPck.GET_RECORDS_FROM_tblProductPacking_API(ref dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    response.status = 0;
                    response.Message = "Success";
                    response.total_records = dt.Rows.Count;
                    response.Data = dt;
                }
                else
                {
                    response.status = 1;
                    response.Message = "No record found";
                    response.Data = blankarray;
                }

            }
            catch (Exception ex)
            {
                response.status = 1;
                response.Message = (("Error ProductpackageList : " + ex.Message) ?? "");
                response.Data = blankarray;
            }
            return response;
        }
        #endregion

        #region Add Order
        [HttpPost]
        [Route("AddOrder")]//A For Add And E For Edit
        public WSResponseObject AddOrder(AddOrderList listOrder)
        // public WSResponseObject AddOrder(BA_tblOrder listOrder)
        {
            WSResponseObject response = new WSResponseObject();
            var blankarray = new List<string>();
            response.total_records = 0;
            response.Message = "";
            try
            {
                BA_tblOrder ObjOrder = new BA_tblOrder();

                ObjOrder.OrderType = listOrder.Order.OrderType;
                ObjOrder.DealerId = listOrder.Order.DealerId;

                ObjOrder.FreeSchemeFrom = listOrder.Order.FreeSchemeFrom;
                ObjOrder.FreeSchemeTO = listOrder.Order.FreeSchemeTO;
                ObjOrder.TotalKgGm = listOrder.Order.TotalKgGm;

                ObjOrder.Transport = listOrder.Order.Transport;
                ObjOrder.Other = listOrder.Order.Other;
                ObjOrder.POP = listOrder.Order.POP;
                ObjOrder.SiteDelivery = listOrder.Order.SiteDelivery;


                ObjOrder.ParentOrderId = listOrder.Order.ParentOrderId;
                ObjOrder.CreateBy = listOrder.Order.CreateBy;

                string XML = "";
                XML = "<OrderProduct>";
                if (listOrder.OrderProductDetails.Count > 0)
                {
                    foreach (var item in listOrder.OrderProductDetails)
                    {
                        XML += "<TABLE>";
                        XML += "<ProductId>" + item.ProductId + "</ProductId>";
                        XML += "<ProductPckIds>" + item.ProductPckID + "</ProductPckIds>";
                        XML += "<ProductPck>" + item.ProductPck + "</ProductPck>";
                        XML += "<PackingNos>" + item.PackingNos + "</PackingNos>";
                        XML += "<PackingType>" + item.PackingType + "</PackingType>";
                        XML += "<BoxORNos>" + item.BoxORNos + "</BoxORNos>";
                        XML += "<PckTotalKg>" + item.PckTotalKg + "</PckTotalKg>";
                        XML += "<ProductQty>" + item.ProductQty + "</ProductQty>";
                        XML += "<IsScheme>" + item.IsScheme + "</IsScheme>";
                        XML += "<Scheme>" + item.Scheme + "</Scheme>";
                        XML += "</TABLE>";

                    }


                }
                XML += "</OrderProduct>";
                ObjOrder.xmlProd = XML;

                int ReturnId = 0;
                bool output;
                output = ObjOrder.INSERT_tblOrder_API(ref ReturnId);
                if (output == true)
                {
                    response.status = 0;
                    response.Message = "Success";
                    response.Data = blankarray;
                }
                else
                {
                    response.status = 0;
                    response.Message = "Record could not able to store, please try again.";
                    response.Data = blankarray;
                }
            }
            catch (Exception ex)
            {
                response.status = 1;
                response.Message = (("Error AddOrder : " + ex.Message) ?? "");
                response.Data = blankarray;
            }
            return response;
        }

        #endregion

        #region Update User
        [HttpPost]
        [Route("UpdateUser")]//A For Add And E For Edit
        public WSResponseObject UpdateUser(BA_tblUser list)
        {
            WSResponseObject response = new WSResponseObject();
            BA_tblUser ObjUser = new BA_tblUser();
            Common Cmn = new Common();
            var blankarray = new BA_tblUser();
            response.total_records = 0;
            response.Message = "";
            try
            {


                ObjUser = list;
                if (list.Pwd != "")
                    ObjUser.Pwd = Cmn.Encrypt(list.Pwd);
                else
                    ObjUser.Pwd = "";

                bool output;
                output = ObjUser.UPDATE_tblUser_API();
                if (output == true)
                {

                    ObjUser.CreateBy = ObjUser.UpdateBy;
                    ObjUser.CreateData = "";
                    ObjUser.UpdateDate = "";
                    response.status = 0;
                    response.Message = "Success";
                    response.Data = ObjUser;
                }
                else
                {
                    ObjUser.UserID = 0;
                    ObjUser.UserName = "";
                    ObjUser.FirstName = "";
                    ObjUser.MiddleName = "";
                    ObjUser.LastName = "";
                    ObjUser.PhoneNo = "";
                    ObjUser.MobileNo = "";
                    response.status = 0;
                    ObjUser.CreateBy = 0;
                    ObjUser.CreateData = "";
                    ObjUser.UpdateBy = 0;
                    ObjUser.UpdateDate = "";
                    ObjUser.UserType = "";

                    response.Message = "Record could not able to store, please try again.";
                    response.Data = ObjUser;
                }
            }
            catch (Exception ex)
            {
                ObjUser.UserID = 0;
                ObjUser.UserName = "";
                ObjUser.FirstName = "";
                ObjUser.MiddleName = "";
                ObjUser.LastName = "";
                ObjUser.PhoneNo = "";
                ObjUser.MobileNo = "";
                ObjUser.CreateBy = 0;
                ObjUser.CreateData = "";
                ObjUser.UpdateBy = 0;
                ObjUser.UpdateDate = "";
                ObjUser.UserType = "";


                response.status = 1;
                response.Message = (("Error UpdateUser: " + ex.Message) ?? "");
                response.Data = ObjUser;
            }
            return response;
        }
        #endregion

        #region Class Member Declaration
        public class AddOrderList
        {
            public BA_tblOrder Order { get; set; }//Day Of Week
            public List<BA_tblOrderProductDetails> OrderProductDetails { get; set; }
        }

        #endregion

        #region Add Dealer Order Free Scheme
        [HttpPost]
        [Route("AddDealerOrderScheme")]
        public WSResponseObject AddDealerOrderScheme(AllDataOrder data)
        {  
            int ReturnId = 0;
            string xmlOrderFreeCreates = "";
            string xmlOrderProductDetails = "";
            WSResponseObject response = new WSResponseObject();
            List<OrderProductDetailData> OrderProductDetailDatadtl = new List<OrderProductDetailData>();
            List<OrderFreeProduct> OrderFreeProductdtl = new List<OrderFreeProduct>();
            OrderProductDetailDatadtl = data._BA_Product_dtl;
            OrderFreeProductdtl = data._BA_Free_product;

            BA_tblOrder ObjOrder = new BA_tblOrder();
            ObjOrder = data._BA_tblOrder;
            ObjOrder.SalesId = ObjOrder.CreateBy;
            ObjOrder.OrderType = CommMessage.OrderType_FreeScheme;
            ObjOrder.PurchaseDurationFromDate = DateTime.ParseExact(data._BA_tblOrder.PurchaseDurationFromDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", new CultureInfo("en-GB"));

            ObjOrder.PurchaseDurationToDate = DateTime.ParseExact(data._BA_tblOrder.PurchaseDurationToDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", new CultureInfo("en-GB"));
            ObjOrder.PurchaseKgs = Convert.ToDecimal(data._BA_tblOrder.PurchaseKgs);
            ObjOrder.TotalKgGm = Convert.ToDecimal(data._BA_tblOrder.TotalKgGm);
            ObjOrder.CreateBy = Convert.ToInt32(data._BA_tblOrder.CreateBy);

            try
            {
                xmlOrderFreeCreates = xmlOrderFreeCreate(OrderFreeProductdtl);
                xmlOrderProductDetails = xmlOrderProductDetail(OrderProductDetailDatadtl);
                if (xmlOrderFreeCreates != "")
                {
                    ObjOrder.xmlProd = xmlOrderProductDetails;
                    ObjOrder.xmlFreeProd = xmlOrderFreeCreates;

                    ObjOrder.INSERT_tblOrderDealerScheme(ref ReturnId);
                    if (ReturnId != 0 || ReturnId.ToString() != "")
                    {

                        response.status = 0;
                        response.Message = "Success";
                        response.total_records = 0;
                        response.Data = ReturnId;
                    }
                    else
                    {
                        response.status = 1;
                        response.Message = "Data not save";
                        response.total_records = 0;
                        response.Data = ReturnId;
                    }

                }

            }
            catch (Exception ex)
            {
                response.status = 1;
                response.Message = (("Error Free Scheme: " + ex.Message) ?? "");
                response.total_records = 0;
                response.Data = 0;
            }

            return response;
        }


        public static string xmlOrderFreeCreate(List<OrderFreeProduct> OrderFreeProductdtl)
        {
            string XML = "";
            try
            {

                XML = "<OrderFree>";
                for (int i = 0; i < OrderFreeProductdtl.Count; i++)
                {

                    XML += "<TABLE>";
                    XML += "<OrderSrNo>" + OrderFreeProductdtl[i].hdProdSrno + "</OrderSrNo>";
                    XML += "<ProductId>" + Convert.ToInt64(OrderFreeProductdtl[i].item) + "</ProductId>";
                    XML += "<AnnualPurchasQty>" + Convert.ToDecimal(OrderFreeProductdtl[i].PurchaseK) + "</AnnualPurchasQty>";
                    XML += "<FreeSchemeFrom>" + Convert.ToDecimal(OrderFreeProductdtl[i].FromScheme) + "</FreeSchemeFrom>";
                    XML += "<FreeSchemeTO>" + Convert.ToInt64(OrderFreeProductdtl[i].ToScheme) + "</FreeSchemeTO>";

                    XML += "</TABLE>";


                }
            }
            catch (Exception)
            { }
            return XML += "</OrderFree>";


        }

        public static string xmlOrderProductDetail(List<OrderProductDetailData> OrderProductDetailDatadtl)
        {

            string XML = "";
            try
            {
                decimal totalkg = 0;
                XML = "<OrderProduct>";
                for (int i = 0; i < OrderProductDetailDatadtl.Count; i++)
                {

                    XML += "<TABLE>";
                    XML += "<ProductId>" + Convert.ToInt64(OrderProductDetailDatadtl[i].Productid) + "</ProductId>";
                    if (OrderProductDetailDatadtl[i].ProductPckID != "")
                    {
                        XML += "<ProductPckIds>" + Convert.ToInt64(OrderProductDetailDatadtl[i].ProductPckID) + "</ProductPckIds>";
                    }
                    else
                    {
                        XML += "<ProductPckIds>" + 0 + "</ProductPckIds>";

                    }

                    XML += "<ProductPck>" + OrderProductDetailDatadtl[i].PKG + "</ProductPck>";
                    XML += "<PackingNos>" + Convert.ToInt32(OrderProductDetailDatadtl[i].QTY) + "</PackingNos>";
                    XML += "<PackingType>" + OrderProductDetailDatadtl[i].PackingType + "</PackingType>";
                    XML += "<BoxORNos>NO</BoxORNos>";
                    XML += "<PckTotalKg>" + OrderProductDetailDatadtl[i].PckTotalKg + "</PckTotalKg>";
                    XML += "<ProductQty>" + Convert.ToInt32(OrderProductDetailDatadtl[i].QTY) + "</ProductQty>";
                    XML += "<IsScheme>" + OrderProductDetailDatadtl[i].isscheme + "</IsScheme>";
                    if (OrderProductDetailDatadtl[i].isscheme == "")
                    {
                        XML += "<Scheme></Scheme>";
                    }
                    else
                    {
                        XML += "<Scheme>" + OrderProductDetailDatadtl[i].Schemetext + "</Scheme>";
                    }
                    XML += "<PDtlSrno>" + Convert.ToInt32(OrderProductDetailDatadtl[i].srno) + "</PDtlSrno>";
                    XML += "<FreeOrderSRNO>" + Convert.ToInt32(OrderProductDetailDatadtl[i].arrOrderProductDetailscounterdata) + "</FreeOrderSRNO>";
                    XML += "</TABLE>";
                    // totalkg = totalkg + staticCaltotal(Convert.ToDecimal(OrderProductDetailDatadtl[i].PckTotalKg), Convert.ToString(OrderProductDetailDatadtl[i].PackingType), Convert.ToInt32(OrderProductDetailDatadtl[i].QTY));

                }
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }
            return XML += "</OrderProduct>";


        }

        #endregion
    }
}

public class AllDataOrder
{
    public BA_tblOrder _BA_tblOrder { get; set; }
    public List<OrderFreeProduct> _BA_Free_product { get; set; }
    public List<OrderProductDetailData> _BA_Product_dtl { get; set; }

}
