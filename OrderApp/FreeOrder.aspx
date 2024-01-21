<%@ Page Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="FreeOrder.aspx.cs" Inherits="OrderApp.FreeOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Script/jquery-1.9.1.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading page-titles">
            <div class="form-group pull-left">
                <h3>
                    <asp:Label ID="lblheading" runat="server"></asp:Label>
                </h3>
            </div>
            <div class="form-group pull-right">
                <asp:Button ID="btnback" runat="server" Text="Back To List" CssClass="btn btn-info btn-rounded" OnClick="btnback_Click" />
            </div>
        </div>
        <center>
            <asp:Label ID="lblErrorMessage"  ForeColor="Red"  runat="server"></asp:Label>
         </center>
        <div class="panel-body">
            <div class="col-md-12">
                <div class="form-group">
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lbldealercode" runat="server" Text="Dealer Code"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtDealerCodeSearch" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label>Order Date:</label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtOrderDate" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-12">
                <div class="form-group">
                    <div class="row">

                        <div class="col-md-2">
                            <label>Dealer Name:</label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtdealernamesearch" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>

                        <div class="col-md-2">
                            <label>Sales Executive :</label>
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="drpsSalesExe" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>


                    </div>
                </div>
            </div>

            <div class="col-md-12">
                <div class="form-group">
                    <div class="row">

                        <div class="col-md-2">
                            <label>Transport:</label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txttransport" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label>Order Status:</label>
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="drpOrderStatus" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                                <asp:ListItem Text="Factory" Value="Factory"></asp:ListItem>
                                 <asp:ListItem Text="Dispatch Department" Value="Dispatch Department"></asp:ListItem>
                                <asp:ListItem Text="Dispatched" Value="Dispatched"></asp:ListItem>
                                <asp:ListItem Text="Cancel" Value="Cancel"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-12">
                <div class="form-group">
                    <div class="row">
                        <div class="col-md-2">
                            From Scheme:
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtFromScheme" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            To Scheme:
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtToScheme" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-12">
                <div class="form-group">
                    <div class="row">
                        <div class="col-md-2">
                            Total Kgs:
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtTotalKgsF" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            Free Total Kgs:
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtFreetotalkg" runat="server" Text="0" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>


            <div id="divAddOrder" runat="server">
                <div class="col-md-12">
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-2">
                                <label>EURO XTRA</label>
                                <asp:GridView ID="gridEUROXTRA" runat="server" AutoGenerateColumns="false" OnRowDataBound="gridEUROXTRA_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="PKG.">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("ProductPacking") %>'></asp:Label>
                                                <asp:HiddenField ID="HdnProductId1" runat="server" Value='<%# Bind("ProductId") %>' />
                                                <asp:HiddenField ID="HdnProductPckId1" runat="server" Value='<%# Bind("ProductPckID") %>' />
                                                <asp:HiddenField ID="HdnProductPck1" runat="server" Value='<%# Bind("ProductPck") %>' />
                                                <asp:HiddenField ID="HdnPackingNos1" runat="server" Value='<%# Bind("PackingNos") %>' />
                                                <asp:HiddenField ID="HdnPackingType1" runat="server" Value='<%# Bind("PackingType") %>' />
                                                <asp:HiddenField ID="HdnIsScheme1" runat="server" Value='<%# Bind("IsScheme") %>' />
                                                <asp:HiddenField ID="HdnTotalProductPckKG1" runat="server" Value='<%# Bind("TotalKG") %>' />

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="QTY.">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtEUROXTRA" runat="server" Width="60px" CssClass="form-control" MaxLength="3" onkeypress="return isNumber(event)" onchange=<%# "TotalNumber(" + Eval("ProductPck") + ", '" + Eval("PackingType") + "', this)"  %>></asp:TextBox>
                                                <asp:Label ID="lblpkgboxnoEUROXTRA" runat="server">Nos.</asp:Label>
                                                <asp:HiddenField ID="hdEUROXTRA" runat="server" />
                                                <br />
                                                <asp:Label ID="lblschEUROXTRA" runat="server" Text="Scheme" Visible="false"></asp:Label>
                                                <asp:TextBox ID="txtschemeEUROXTRA" runat="server" CssClass="form-control" MaxLength="160" TextMode="MultiLine" Visible="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>

                            <div class="col-md-2">
                                <label>EURO WP</label>
                                <asp:GridView ID="grdEUROWP" runat="server" AutoGenerateColumns="false" OnRowDataBound="grdEUROWP_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="PKG.">
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("ProductPacking") %>'></asp:Label>
                                                <asp:HiddenField ID="HdnProductId2" runat="server" Value='<%# Bind("ProductId") %>' />
                                                <asp:HiddenField ID="HdnProductPckId2" runat="server" Value='<%# Bind("ProductPckID") %>' />
                                                <asp:HiddenField ID="HdnProductPck2" runat="server" Value='<%# Bind("ProductPck") %>' />
                                                <asp:HiddenField ID="HdnPackingNos2" runat="server" Value='<%# Bind("PackingNos") %>' />
                                                <asp:HiddenField ID="HdnPackingType2" runat="server" Value='<%# Bind("PackingType") %>' />
                                                <asp:HiddenField ID="HdnIsScheme2" runat="server" Value='<%# Bind("IsScheme") %>' />
                                                <asp:HiddenField ID="HdnTotalProductPckKG2" runat="server" Value='<%# Bind("TotalKG") %>' />


                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="QTY.">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txteurowp" runat="server" Width="60px" CssClass="form-control" MaxLength="3" onkeypress="return isNumber(event)" onchange=<%# "TotalNumber(" + Eval("ProductPck") + ", '" + Eval("PackingType") + "', this)"  %>></asp:TextBox>
                                                <asp:Label ID="lblpkgboxnoeurowp" runat="server">Nos.</asp:Label>
                                                <asp:HiddenField ID="hdeurowp" runat="server" />
                                                <br />
                                                <asp:Label ID="lblschEUROWP" runat="server" Text="Scheme" Visible="false"></asp:Label>
                                                <asp:TextBox ID="txtschemeEUROWP" runat="server" CssClass="form-control" MaxLength="160" TextMode="MultiLine" Visible="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>

                            <div class="col-md-2">
                                <label>EURO HI STRONG</label>
                                <asp:GridView ID="grdeuro2in1" runat="server" AutoGenerateColumns="false" OnRowDataBound="grdeuro2in1_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="PKG.">
                                            <ItemTemplate>
                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("ProductPacking") %>'></asp:Label>
                                                <asp:HiddenField ID="HdnProductId3" runat="server" Value='<%# Bind("ProductId") %>' />
                                                <asp:HiddenField ID="HdnProductPckId3" runat="server" Value='<%# Bind("ProductPckID") %>' />
                                                <asp:HiddenField ID="HdnProductPck3" runat="server" Value='<%# Bind("ProductPck") %>' />
                                                <asp:HiddenField ID="HdnPackingNos3" runat="server" Value='<%# Bind("PackingNos") %>' />
                                                <asp:HiddenField ID="HdnPackingType3" runat="server" Value='<%# Bind("PackingType") %>' />
                                                <asp:HiddenField ID="HdnIsScheme3" runat="server" Value='<%# Bind("IsScheme") %>' />
                                                <asp:HiddenField ID="HdnTotalProductPckKG3" runat="server" Value='<%# Bind("TotalKG") %>' />

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="QTY.">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txteuro2in1" runat="server" Width="60px" CssClass="form-control" MaxLength="3" onkeypress="return isNumber(event)" onchange=<%# "TotalNumber(" + Eval("ProductPck") + ", '" + Eval("PackingType") + "', this)"  %>></asp:TextBox>
                                                <asp:Label ID="lblpkgboxnoeuro2in1" runat="server">Nos.</asp:Label>
                                                <asp:HiddenField ID="hdeuro2in1" runat="server" />
                                                <br />
                                                <asp:Label ID="lblscheuro2in1" runat="server" Text="Scheme" Visible="false"></asp:Label>
                                                <asp:TextBox ID="txtschemeeuro2in1" runat="server" CssClass="form-control" MaxLength="160" TextMode="MultiLine" Visible="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>

                            <div class="col-md-2">
                                <label>EXTREME 3</label>
                                <asp:GridView ID="grdExtreme" runat="server" AutoGenerateColumns="false" OnRowDataBound="grdExtreme_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="PKG.">
                                            <ItemTemplate>
                                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("ProductPacking") %>'></asp:Label>
                                                <asp:HiddenField ID="HdnProductId4" runat="server" Value='<%# Bind("ProductId") %>' />
                                                <asp:HiddenField ID="HdnProductPckId4" runat="server" Value='<%# Bind("ProductPckID") %>' />
                                                <asp:HiddenField ID="HdnProductPck4" runat="server" Value='<%# Bind("ProductPck") %>' />
                                                <asp:HiddenField ID="HdnPackingNos4" runat="server" Value='<%# Bind("PackingNos") %>' />
                                                <asp:HiddenField ID="HdnPackingType4" runat="server" Value='<%# Bind("PackingType") %>' />
                                                <asp:HiddenField ID="HdnIsScheme4" runat="server" Value='<%# Bind("IsScheme") %>' />
                                                <asp:HiddenField ID="HdnTotalProductPckKG4" runat="server" Value='<%# Bind("TotalKG") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="QTY.">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtExtreme" runat="server" CssClass="form-control" Width="60px" MaxLength="3" onkeypress="return isNumber(event)" onchange=<%# "TotalNumber(" + Eval("ProductPck") + ", '" + Eval("PackingType") + "', this)"  %>></asp:TextBox>
                                                <asp:Label ID="lblpkgboxnoExtreme" runat="server">Nos.</asp:Label>
                                                <asp:HiddenField ID="hdExtreme" runat="server" />
                                                <br />
                                                <asp:Label ID="lblschExtreme" runat="server" Text="Scheme" Visible="false"></asp:Label>
                                                <asp:TextBox ID="txtschemeExtreme" runat="server" CssClass="form-control" MaxLength="160" TextMode="MultiLine" Visible="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>

                            <div class="col-md-2">
                                <label>EURO ULTRA</label>
                                <asp:GridView ID="grdEuroUltra" runat="server" AutoGenerateColumns="false" OnRowDataBound="grdEuroUltra_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="PKG.">
                                            <ItemTemplate>
                                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("ProductPacking") %>'></asp:Label>
                                                <asp:HiddenField ID="HdnProductId5" runat="server" Value='<%# Bind("ProductId") %>' />
                                                <asp:HiddenField ID="HdnProductPckId5" runat="server" Value='<%# Bind("ProductPckID") %>' />
                                                <asp:HiddenField ID="HdnProductPck5" runat="server" Value='<%# Bind("ProductPck") %>' />
                                                <asp:HiddenField ID="HdnPackingNos5" runat="server" Value='<%# Bind("PackingNos") %>' />
                                                <asp:HiddenField ID="HdnPackingType5" runat="server" Value='<%# Bind("PackingType") %>' />
                                                <asp:HiddenField ID="HdnIsScheme5" runat="server" Value='<%# Bind("IsScheme") %>' />
                                                <asp:HiddenField ID="HdnTotalProductPckKG5" runat="server" Value='<%# Bind("TotalKG") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="QTY.">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtEuroUltra" runat="server" CssClass="form-control" Width="60px" MaxLength="3" onkeypress="return isNumber(event)" onchange=<%# "TotalNumber(" + Eval("ProductPck") + ", '" + Eval("PackingType") + "', this)"  %>></asp:TextBox>
                                                <asp:Label ID="llblpkgboxnoEuroUltra" runat="server">Nos.</asp:Label>
                                                <asp:HiddenField ID="hdEuroUltra" runat="server" />
                                                <br />
                                                <asp:Label ID="lblschEuroUltra" runat="server" Text="Scheme" Visible="false"></asp:Label>
                                                <asp:TextBox ID="txtschemeEuroUltra" MaxLength="160" TextMode="MultiLine" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>

                            <div class="col-md-2">
                                <label>PVC GLUE</label>
                                <asp:GridView ID="GrdPvcGlue" runat="server" AutoGenerateColumns="false" OnRowDataBound="GrdPvcGlue_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="PKG.">
                                            <ItemTemplate>
                                                <asp:Label ID="Label6" Width="45px" runat="server" Text='<%# Bind("ProductPacking") %>'></asp:Label>
                                                <asp:HiddenField ID="HdnProductId6" runat="server" Value='<%# Bind("ProductId") %>' />
                                                <asp:HiddenField ID="HdnProductPckId6" runat="server" Value='<%# Bind("ProductPckID") %>' />
                                                <asp:HiddenField ID="HdnProductPck6" runat="server" Value='<%# Bind("ProductPck") %>' />
                                                <asp:HiddenField ID="HdnPackingNos6" runat="server" Value='<%# Bind("PackingNos") %>' />
                                                <asp:HiddenField ID="HdnPackingType6" runat="server" Value='<%# Bind("PackingType") %>' />
                                                <asp:HiddenField ID="HdnIsScheme6" runat="server" Value='<%# Bind("IsScheme") %>' />
                                                <asp:HiddenField ID="HdnTotalProductPckKG6" runat="server" Value='<%# Bind("TotalKG") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="QTY.">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtPVcGlue" runat="server" CssClass="form-control" Width="55px" MaxLength="3" onkeypress="return isNumber(event)" onchange=<%# "TotalNumber(" + Eval("ProductPck") + ", '" + Eval("PackingType") + "', this)"  %>></asp:TextBox>
                                                <asp:Label ID="lblpkgboxnoPVcGlue" runat="server">Nos.</asp:Label>
                                                <asp:HiddenField ID="hdPVcGlue" runat="server" />
                                                <br />
                                                <asp:Label ID="lblschPvcGlue" runat="server" Text="Scheme" Visible="false"></asp:Label>
                                                <asp:TextBox ID="txtschemePvcGlue" runat="server" MaxLength="160" TextMode="MultiLine" CssClass="form-control" Visible="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <br />
                                <label>WOOD STRONG</label>
                                <asp:GridView ID="grdWoodStrong" runat="server" AutoGenerateColumns="false" OnRowDataBound="grdWoodStrong_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="PKG.">
                                            <ItemTemplate>
                                                <asp:Label ID="Label7" runat="server" Width="45px" Text='<%# Bind("ProductPacking") %>'></asp:Label>
                                                <asp:HiddenField ID="HdnProductId7" runat="server" Value='<%# Bind("ProductId") %>' />
                                                <asp:HiddenField ID="HdnProductPckId7" runat="server" Value='<%# Bind("ProductPckID") %>' />
                                                <asp:HiddenField ID="HdnProductPck7" runat="server" Value='<%# Bind("ProductPck") %>' />
                                                <asp:HiddenField ID="HdnPackingNos7" runat="server" Value='<%# Bind("PackingNos") %>' />
                                                <asp:HiddenField ID="HdnPackingType7" runat="server" Value='<%# Bind("PackingType") %>' />
                                                <asp:HiddenField ID="HdnIsScheme7" runat="server" Value='<%# Bind("IsScheme") %>' />
                                                <asp:HiddenField ID="HdnTotalProductPckKG7" runat="server" Value='<%# Bind("TotalKG") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="QTY.">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtWoodStrong" runat="server" CssClass="form-control" Width="55px" MaxLength="3" onkeypress="return isNumber(event)" onchange=<%# "TotalNumber(" + Eval("ProductPck") + ", '" + Eval("PackingType") + "', this)"  %>></asp:TextBox>
                                                <asp:Label ID="lblpkgboxnoWoodStrong" runat="server">Nos.</asp:Label>
                                                <asp:HiddenField ID="hdWoodStrong" runat="server" />
                                                <br />
                                                <asp:Label ID="lblschWoodStrong" runat="server" Text="Scheme" Visible="false"></asp:Label>
                                                <asp:TextBox ID="txtschemeWoodStrong" runat="server" CssClass="form-control" MaxLength="160" TextMode="MultiLine" Visible="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>

                            <div class="col-md-10"></div>

                            <div class="col-md-2 pull-right">
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-12" id="divOtherDetails" runat="server">
                <div class="form-group">
                    <div class="row">

                        <div class="col-md-1">Site Delivery</div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtsitedelivery" runat="server" TextMode="MultiLine" Style="width: 100%; height: 100px;">
                            </asp:TextBox>
                        </div>

                        <div class="col-md-1">Other :</div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtOther" runat="server" TextMode="MultiLine" Style="width: 100%; height: 100px;">
                            </asp:TextBox>
                        </div>

                        <div class="col-md-1">Pop :</div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtPOP" runat="server" TextMode="MultiLine" Style="width: 100%; height: 100px;">
                            </asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-12">
                <div class="form-group">
                    <div class="row">
                        <div class="col-md-1">
                            <label>Total :</label>
                        </div>
                        <div class="col-md-2">
                            <asp:TextBox ID="txtFreeKg" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-8">
                        </div>
                        <div class="col-md-1 pull-right;">
                            <asp:Button ID="btnSubmitOrder" runat="server" Text="Send" CssClass="btn btn-success" OnClick="btnSubmitOrder_Click" />
                            <%--<asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-dark" OnClick="btnClear_Click" />--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>

    <asp:HiddenField ID="hdDelaerId" runat="server" />
    <asp:HiddenField ID="hdOrderId" runat="server" />
    <asp:HiddenField ID="hdFreeOrderId" runat="server" />
    <asp:HiddenField ID="hdTotalFreeKgCount" runat="server" />
    <asp:HiddenField ID="hdTotalKgCount" runat="server" />
    <asp:HiddenField ID="hdEditOrderId" runat="server" />

    <script type="text/javascript">
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }

        function TotalNumber(TotalKG, PackingType, this1) {
            // Box/Qty Value
            var ProductQty = $(this1).val();
            if (ProductQty == '') {
                ProductQty = parseFloat(0);
            }

            // Last Box/Qty Value
            var HdnLastQty = $(this1).parent().find('input:hidden').val();
            if (HdnLastQty == '') {
                HdnLastQty = parseFloat(0);
            }

            // Total Value
            var HdnTotalKgCount = $('#<%=hdTotalFreeKgCount.ClientID%>').val();
            if (HdnTotalKgCount == '') {
                HdnTotalKgCount = parseFloat(0);
            }
            var TotalKgCount = 0;
            TotalKgCount = TotalKG * ProductQty;
            TotalKgCount = parseFloat(TotalKgCount);

            HdnLastQty = TotalKG * HdnLastQty;
            HdnLastQty = parseFloat(HdnLastQty);

            if (PackingType.toLowerCase() == 'gm') {
                TotalKgCount = TotalKgCount / 1000;
                HdnLastQty = HdnLastQty / 1000;
            }

            var FinalKgCount = parseFloat(TotalKgCount) + parseFloat(HdnTotalKgCount);
            // if Box value is 0 then substract it with Last value
            //if (TotalKgCount == 0) {
            //    FinalKgCount = parseFloat(FinalKgCount) - parseFloat(HdnLastQty);
            //}       
            FinalKgCount = parseFloat(FinalKgCount) - parseFloat(HdnLastQty);
            $('#<%=txtFreeKg.ClientID%>').val(round(FinalKgCount, 0));
            $('#<%=hdTotalFreeKgCount.ClientID%>').val(FinalKgCount);
            $(this1).parent().find('input:hidden').val($(this1).val());

            var txtFreetotalkg = $('#<%=txtFreetotalkg.ClientID%>').val();
            var txtFreeKg = $('#<%=txtFreeKg.ClientID%>').val();

            //if (parseFloat(txtFreeKg) > parseFloat(txtFreetotalkg)) {
            //    alert("You can't add more Free kg than allowed kg.");
            //}
        }

        $('#ContentPlaceHolder1_txtFromScheme').change(function () {

            if ($('#ContentPlaceHolder1_txtFromScheme').val() == "" || $('#ContentPlaceHolder1_txtToScheme').val() == "") {

                $('#ContentPlaceHolder1_txtFreetotalkg').val(0);
            }
            else {

              
                $('#ContentPlaceHolder1_txtFreetotalkg').val((($('#ContentPlaceHolder1_txtTotalKgsF').val() * $('#ContentPlaceHolder1_txtToScheme').val()) / $('#ContentPlaceHolder1_txtFromScheme').val()));
                var FreeTotalKg = Math.ceil($('#ContentPlaceHolder1_txtFreetotalkg').val());
                $('#ContentPlaceHolder1_txtFreetotalkg').val(FreeTotalKg);

                var txtFreetotalkg = $('#<%=txtFreetotalkg.ClientID%>').val();
                var txtFreeKg = $('#<%=txtFreeKg.ClientID%>').val();

            }

        });

        $('#ContentPlaceHolder1_txtToScheme').change(function () {

            if ($('#ContentPlaceHolder1_txtFromScheme').val() == "" || $('#ContentPlaceHolder1_txtToScheme').val() == "") {

                $('#ContentPlaceHolder1_txtFreetotalkg').val(0);
            }
            else {

                $('#ContentPlaceHolder1_txtFreetotalkg').val((($('#ContentPlaceHolder1_txtTotalKgsF').val() * $('#ContentPlaceHolder1_txtToScheme').val()) / $('#ContentPlaceHolder1_txtFromScheme').val()));
                var FreetotalKg = Math.ceil($('#ContentPlaceHolder1_txtFreetotalkg').val());
                $('#ContentPlaceHolder1_txtFreetotalkg').val(FreetotalKg);

                var txtFreetotalkg = $('#<%=txtFreetotalkg.ClientID%>').val();
                var txtFreeKg = $('#<%=txtFreeKg.ClientID%>').val();

                //if (parseFloat(txtFreeKg) > parseFloat(txtFreetotalkg)) {
                //    alert("You can't add more Free kg than allowed kg.");
                //}
            }
        });


        function round(value, exp) {
            if (typeof exp === 'undefined' || +exp === 0)
                return Math.round(value);

            value = +value;
            exp = +exp;

            if (isNaN(value) || !(typeof exp === 'number' && exp % 1 === 0))
                return NaN;

            // Shift
            value = value.toString().split('e');
            value = Math.round(+(value[0] + 'e' + (value[1] ? (+value[1] + exp) : exp)));

            // Shift back
            value = value.toString().split('e');
            return +(value[0] + 'e' + (value[1] ? (+value[1] - exp) : -exp));
        }

    </script>
</asp:Content>
