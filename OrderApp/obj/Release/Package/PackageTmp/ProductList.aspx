<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductList.aspx.cs" Inherits="OrderApp.ProductList" MasterPageFile="~/MainMaster.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="panel panel-default">
          <div class="panel-heading page-titles">
           <div class="form-group pull-left">
                  <h3>Product List</h3>
            </div>
            <div class="form-group pull-right">
                 <asp:Button ID="btnAdd" runat="server" Text="Add Product" OnClick="btnAdd_Click" CssClass="btn btn-info btn-rounded" />
            </div>
        </div>
        
       <center>
        <asp:Label ID="lblErrorMessage"  ForeColor="Red"   runat="server"></asp:Label>
         </center>
        <div class="panel-body">
            <div class="form-group">
                <asp:GridView ID="grdProductList" runat="server" ShowHeaderWhenEmpty="True" EmptyDataText="No records Found" Width="100%"
                    CssClass="mygrdContent" PagerStyle-CssClass="pager" HeaderStyle-CssClass="header" RowStyle-CssClass="rows" AutoGenerateColumns="false"
                    OnRowCommand="grdProduct_RowCommand" AllowPaging="true" OnPageIndexChanging="grdProductList_PageIndexChanging" PageSize="10">
                    <Columns>
                        <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                        <asp:BoundField DataField="ProductDesc" HeaderText="Product Description" />                        
                        <asp:TemplateField Visible ="false">
                            <ItemTemplate>
                                <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn btn-success pull-right" CommandName="EditValue" CommandArgument='<%# Eval("ProductId") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible ="false">
                            <ItemTemplate>
                                <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-warning pull-right" CommandName="DeleteValue" CommandArgument='<%# Eval("ProductId") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="pagination-ys" Wrap="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:GridView>
            </div>
        </div>

        

    </div>
</asp:Content>
