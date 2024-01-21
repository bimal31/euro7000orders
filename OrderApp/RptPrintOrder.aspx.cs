using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using OrderApp.App_Code;

namespace OrderApp
{
    public partial class RptPrintOrder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["q"] != null)
                {
                    ReportViewer ReportViewer1 = new ReportViewer();
                    LocalReport localReport = new LocalReport();

                    string strKey = Convert.ToString(Request.QueryString["q"]);
                    Common cmn = new Common();
                    strKey = cmn.Decrypt(strKey);
                    Int32 OrderId = Convert.ToInt32(strKey);

                    BA_tblOrder ObjOrder = new BA_tblOrder();
                    DataSet _ds = new DataSet();
                    ObjOrder.OrderID = Convert.ToString(OrderId);
                    ObjOrder.GET_RECORDS_FROM_PrinttblOrder(ref _ds);

                    if (_ds.Tables.Count > 0)
                    {

                        ReportViewer1.ProcessingMode = ProcessingMode.Local;
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("rptPrintOrder.rdlc");
                        ReportDataSource datasource = new ReportDataSource("dsOrderDetails", _ds.Tables[0]);

                        decimal totalProduct1 = 0, totalProduct2 = 0, totalProduct3 = 0, totalProduct4 = 0, totalProduct5 = 0, totalProduct6 = 0, totalProduct7 = 0, totalProduct8 = 0; 

                        #region Product 1
                        DataTable dtEuroXTRA = new DataTable();
                        dtEuroXTRA.Clear();
                        dtEuroXTRA.Columns.Add("ProductPackType");
                        dtEuroXTRA.Columns.Add("TotalKg");
                        DataRow _row; // = dtEuroXTRA.NewRow();
                        if (_ds.Tables[1] != null && _ds.Tables[1].Rows.Count > 0)
                        {
                            for (int i = 0; i < _ds.Tables[1].Rows.Count; i++)
                            {
                                
                                _row = dtEuroXTRA.NewRow();
                                _row["ProductPackType"] = _ds.Tables[1].Rows[i]["ProductPackType"];
                                _row["TotalKg"] = _ds.Tables[1].Rows[i]["TotalKg"];
                                dtEuroXTRA.Rows.Add(_row);
                                totalProduct1 += Convert.ToDecimal(_ds.Tables[1].Rows[i]["Tkg"]);
                              
                                if (Convert.ToBoolean(_ds.Tables[1].Rows[i]["IsScheme"]))
                                {
                                    _row = dtEuroXTRA.NewRow();
                                    _row["ProductPackType"] = "Scheme";
                                    _row["TotalKg"] = _ds.Tables[1].Rows[i]["Scheme"];
                                    dtEuroXTRA.Rows.Add(_row);
                                }

                            }
                        }

                         _row = dtEuroXTRA.NewRow();
                         _row["ProductPackType"] = "Total";
                         _row["TotalKg"] = totalProduct1;
                         dtEuroXTRA.Rows.Add(_row);
                        #endregion

                        #region Product 2
                        DataTable dtEuroWP = new DataTable();
                        dtEuroWP.Clear();
                        dtEuroWP.Columns.Add("ProductPackType");
                        dtEuroWP.Columns.Add("TotalKg");
                        if (_ds.Tables[2] != null && _ds.Tables[2].Rows.Count > 0)
                        {
                            for (int i = 0; i < _ds.Tables[2].Rows.Count; i++)
                            {
                                _row = dtEuroWP.NewRow();
                                _row["ProductPackType"] = _ds.Tables[2].Rows[i]["ProductPackType"];
                                _row["TotalKg"] = _ds.Tables[2].Rows[i]["TotalKg"];
                                dtEuroWP.Rows.Add(_row);
                                totalProduct2 += Convert.ToDecimal(_ds.Tables[2].Rows[i]["Tkg"]);
                           
                                if (Convert.ToBoolean(_ds.Tables[2].Rows[i]["IsScheme"]))
                                {
                                    _row = dtEuroWP.NewRow();
                                    _row["ProductPackType"] = "Scheme";
                                    _row["TotalKg"] = _ds.Tables[2].Rows[i]["Scheme"];
                                    dtEuroWP.Rows.Add(_row);
                                }

                            }
                        }

                        _row = dtEuroWP.NewRow();
                        _row["ProductPackType"] = "Total";
                        _row["TotalKg"] = totalProduct2;
                        dtEuroWP.Rows.Add(_row);
                        #endregion

                        #region Product 3
                        DataTable dtEuro2in1 = new DataTable();
                        dtEuro2in1.Clear();
                        dtEuro2in1.Columns.Add("ProductPackType");
                        dtEuro2in1.Columns.Add("TotalKg");
                        if (_ds.Tables[3] != null && _ds.Tables[3].Rows.Count > 0)
                        {
                            for (int i = 0; i < _ds.Tables[3].Rows.Count; i++)
                            {
                                _row = dtEuro2in1.NewRow();
                                _row["ProductPackType"] = _ds.Tables[3].Rows[i]["ProductPackType"];
                                _row["TotalKg"] = _ds.Tables[3].Rows[i]["TotalKg"];
                                dtEuro2in1.Rows.Add(_row);
                                totalProduct3 += Convert.ToDecimal(_ds.Tables[3].Rows[i]["Tkg"]);

                                if (Convert.ToBoolean(_ds.Tables[3].Rows[i]["IsScheme"]))
                                {
                                    _row = dtEuro2in1.NewRow();
                                    _row["ProductPackType"] = "Scheme";
                                    _row["TotalKg"] = _ds.Tables[3].Rows[i]["Scheme"];
                                    dtEuro2in1.Rows.Add(_row);
                                }

                            }
                        }

                        _row = dtEuro2in1.NewRow();
                        _row["ProductPackType"] = "Total";
                        _row["TotalKg"] = totalProduct3;
                        dtEuro2in1.Rows.Add(_row);
                        #endregion


                        #region Product 4
                        DataTable dtExtreme3 = new DataTable();
                        dtExtreme3.Clear();
                        dtExtreme3.Columns.Add("ProductPackType");
                        dtExtreme3.Columns.Add("TotalKg");
                        if (_ds.Tables[4] != null && _ds.Tables[4].Rows.Count > 0)
                        {
                            for (int i = 0; i < _ds.Tables[4].Rows.Count; i++)
                            {
                                _row = dtExtreme3.NewRow();
                                _row["ProductPackType"] = _ds.Tables[4].Rows[i]["ProductPackType"];
                                _row["TotalKg"] = _ds.Tables[4].Rows[i]["TotalKg"];
                                dtExtreme3.Rows.Add(_row);
                                totalProduct4 += Convert.ToDecimal(_ds.Tables[4].Rows[i]["Tkg"]);

                                if (Convert.ToBoolean(_ds.Tables[4].Rows[i]["IsScheme"]))
                                {
                                    _row = dtExtreme3.NewRow();
                                    _row["ProductPackType"] = "Scheme";
                                    _row["TotalKg"] = _ds.Tables[4].Rows[i]["Scheme"];
                                    dtExtreme3.Rows.Add(_row);
                                }

                            }
                        }
                        _row = dtExtreme3.NewRow();
                        _row["ProductPackType"] = "Total";
                        _row["TotalKg"] = totalProduct4;
                        dtExtreme3.Rows.Add(_row);

                        #endregion

                        #region Product 5
                        DataTable dtEuroULTRA = new DataTable();
                        dtEuroULTRA.Clear();
                        dtEuroULTRA.Columns.Add("ProductPackType");
                        dtEuroULTRA.Columns.Add("TotalKg");
                        if (_ds.Tables[5] != null && _ds.Tables[5].Rows.Count > 0)
                        {
                            for (int i = 0; i < _ds.Tables[5].Rows.Count; i++)
                            {
                                _row = dtEuroULTRA.NewRow();
                                _row["ProductPackType"] = _ds.Tables[5].Rows[i]["ProductPackType"];
                                _row["TotalKg"] = _ds.Tables[5].Rows[i]["TotalKg"];
                                dtEuroULTRA.Rows.Add(_row);
                                totalProduct5 += Convert.ToDecimal(_ds.Tables[5].Rows[i]["Tkg"]);

                                if (Convert.ToBoolean(_ds.Tables[5].Rows[i]["IsScheme"]))
                                {
                                    _row = dtEuroULTRA.NewRow();
                                    _row["ProductPackType"] = "Scheme";
                                    _row["TotalKg"] = _ds.Tables[5].Rows[i]["Scheme"];
                                    dtEuroULTRA.Rows.Add(_row);
                                }

                            }
                        }

                        _row = dtEuroULTRA.NewRow();
                        _row["ProductPackType"] = "Total";
                        _row["TotalKg"] = totalProduct5;
                        dtEuroULTRA.Rows.Add(_row);
                        #endregion

                        #region Product 6
                        DataTable dtPVCGLUE = new DataTable();
                        dtPVCGLUE.Clear();
                        dtPVCGLUE.Columns.Add("ProductPackType");
                        dtPVCGLUE.Columns.Add("TotalKg");
                        if (_ds.Tables[6] != null && _ds.Tables[6].Rows.Count > 0)
                        {
                            for (int i = 0; i < _ds.Tables[6].Rows.Count; i++)
                            {
                                _row = dtPVCGLUE.NewRow();
                                _row["ProductPackType"] = _ds.Tables[6].Rows[i]["ProductPackType"];
                                _row["TotalKg"] = _ds.Tables[6].Rows[i]["TotalKg"];
                                dtPVCGLUE.Rows.Add(_row);
                                totalProduct6 += Convert.ToDecimal(_ds.Tables[6].Rows[i]["Tkg"]);

                                if (Convert.ToBoolean(_ds.Tables[6].Rows[i]["IsScheme"]))
                                {
                                    _row = dtPVCGLUE.NewRow();
                                    _row["ProductPackType"] = "Scheme";
                                    _row["TotalKg"] = _ds.Tables[6].Rows[i]["Scheme"];
                                    dtPVCGLUE.Rows.Add(_row);
                                }

                            }
                        }

                        _row = dtPVCGLUE.NewRow();
                        _row["ProductPackType"] = "Total";
                        _row["TotalKg"] = totalProduct6;
                        dtPVCGLUE.Rows.Add(_row);

                        #endregion

                        #region Product 7
                        DataTable dtWoodStrong = new DataTable();
                        dtWoodStrong.Clear();
                        dtWoodStrong.Columns.Add("ProductPackType");
                        dtWoodStrong.Columns.Add("TotalKg");
                        if (_ds.Tables[7] != null && _ds.Tables[7].Rows.Count > 0)
                        {
                            for (int i = 0; i < _ds.Tables[7].Rows.Count; i++)
                            {
                                _row = dtWoodStrong.NewRow();
                                _row["ProductPackType"] = _ds.Tables[7].Rows[i]["ProductPackType"];
                                _row["TotalKg"] = _ds.Tables[7].Rows[i]["TotalKg"];
                                dtWoodStrong.Rows.Add(_row);

                                totalProduct7 += Convert.ToDecimal(_ds.Tables[7].Rows[i]["Tkg"]);

                                if (Convert.ToBoolean(_ds.Tables[7].Rows[i]["IsScheme"]))
                                {
                                    _row = dtWoodStrong.NewRow();
                                    _row["ProductPackType"] = "Scheme";
                                    _row["TotalKg"] = _ds.Tables[7].Rows[i]["Scheme"];
                                    dtWoodStrong.Rows.Add(_row);
                                }

                            }
                        }

                        _row = dtWoodStrong.NewRow();
                        _row["ProductPackType"] = "Total";
                        _row["TotalKg"] = totalProduct7;
                        dtWoodStrong.Rows.Add(_row);

                        #endregion

                        #region Product 8
                        DataTable dtEuroEWR = new DataTable();
                        dtEuroEWR.Clear();
                        dtEuroEWR.Columns.Add("ProductPackType");
                        dtEuroEWR.Columns.Add("TotalKg");
                        if (_ds.Tables[8] != null && _ds.Tables[8].Rows.Count > 0)
                        {
                            for (int i = 0; i < _ds.Tables[8].Rows.Count; i++)
                            {
                                _row = dtEuroEWR.NewRow();
                                _row["ProductPackType"] = _ds.Tables[8].Rows[i]["ProductPackType"];
                                _row["TotalKg"] = _ds.Tables[8].Rows[i]["TotalKg"];
                                dtEuroEWR.Rows.Add(_row);

                                totalProduct8 += Convert.ToDecimal(_ds.Tables[8].Rows[i]["Tkg"]);

                                if (Convert.ToBoolean(_ds.Tables[8].Rows[i]["IsScheme"]))
                                {
                                    _row = dtEuroEWR.NewRow();
                                    _row["ProductPackType"] = "Scheme";
                                    _row["TotalKg"] = _ds.Tables[8].Rows[i]["Scheme"];
                                    dtEuroEWR.Rows.Add(_row);
                                }

                            }
                        }

                        _row = dtEuroEWR.NewRow();
                        _row["ProductPackType"] = "Total";
                        _row["TotalKg"] = totalProduct8;
                        dtEuroEWR.Rows.Add(_row);

                        #endregion

                        localReport.ReportPath = Server.MapPath("~/rptPrintOrder.rdlc");
                        ReportDataSource datasource1 = new ReportDataSource("dsEuroXTRA", dtEuroXTRA);
                        ReportDataSource datasource2 = new ReportDataSource("dsEuroWP", dtEuroWP);
                        ReportDataSource datasource3 = new ReportDataSource("dsEuro2in1", dtEuro2in1);
                        ReportDataSource datasource4 = new ReportDataSource("dsExtreme3", dtExtreme3);
                        ReportDataSource datasource5 = new ReportDataSource("dsEuroULTRA", dtEuroULTRA);
                        ReportDataSource datasource6 = new ReportDataSource("dsPVCGLUE", dtPVCGLUE);
                        ReportDataSource datasource7 = new ReportDataSource("dsWoodStrong", dtWoodStrong);
                        ReportDataSource datasource8 = new ReportDataSource("dsEuroEWR", dtEuroEWR);

                       
                        localReport.DataSources.Clear();
                        localReport.DataSources.Add(datasource);
                        localReport.DataSources.Add(datasource1);
                        localReport.DataSources.Add(datasource2);
                        localReport.DataSources.Add(datasource3);
                        localReport.DataSources.Add(datasource4);
                        localReport.DataSources.Add(datasource5);
                        localReport.DataSources.Add(datasource6);
                        localReport.DataSources.Add(datasource7);
                        localReport.DataSources.Add(datasource8);
                        if (_ds.Tables.Count == 10)
                        {
                            foreach (DataColumn dc in _ds.Tables[9].Columns) // trim column names
                            {
                                dc.ColumnName = dc.ColumnName.Replace(" ", "");
                            }
                            ReportDataSource datasource9 = new ReportDataSource("dtFreeScheme", _ds.Tables[9]);
                            localReport.DataSources.Add(datasource9);
                        }
                        

                        


                        string reportType = "pdf";
                        string mimeType;
                        string encoding;
                        string fileNameExtension;
                        string deviceInfo = "";
                        int sal = 120511;
                        deviceInfo = "<DeviceInfo>" +
                                             "  <OutputFormat>" + sal + "</OutputFormat>" +
                                             "</DeviceInfo>";

                        Warning[] warnings;
                        string[] streams;
                        byte[] renderedBytes;
                        //Render the report       

                        renderedBytes = localReport.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
                        // return File(renderedBytes, mimeType);


                        //string strPrint = "var mywindow = window.open('', 'PRINT', 'height=1000,width=1000');";

                        //strPrint += "mywindow.document.write('<html><head><title>' + document.title  + '</title>');";
                        //strPrint += "mywindow.document.write('</head><body >');";
                        //strPrint += "mywindow.document.write('<h1>' + document.title  + '</h1>');";
                        //strPrint += "mywindow.document.write(renderedBytes);";
                        //strPrint += "mywindow.document.write('</body></html>');";
                        //strPrint += "mywindow.document.close();";
                        //strPrint += "mywindow.focus();";
                        //strPrint += "mywindow.print();";
                        //strPrint += "mywindow.close();";

                        string file_name = Request.QueryString["file"];
                        string path = Server.MapPath("Print_Files/" + file_name);

                        // Open PDF File in Web Browser 

                        WebClient client = new WebClient();
                        Byte[] buffer = renderedBytes;
                        if (buffer != null)
                        {
                            Response.ContentType = "application/pdf";
                            Response.AddHeader("content-length", buffer.Length.ToString());
                            Response.BinaryWrite(buffer);
                        }

                        //  PrintReport.Export(report, true);
                        //  ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "javascript: " + strPrint, true);
                        //   Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "PrintReport(); ", true);
                    }
                }
            }
        }
    }
}