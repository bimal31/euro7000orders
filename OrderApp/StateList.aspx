<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StateList.aspx.cs" Inherits="OrderApp.StateList" MasterPageFile="~/MainMaster.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">   
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="panel panel-default">
          <div class="panel-heading page-titles">
           <div class="form-group pull-left">
               <h3>State List</h3>
            </div>
            <div class="form-group pull-right">
                <asp:Button ID="btnAdd" runat="server" Text="Add State" OnClick="btnAdd_Click"  CssClass="btn btn-info btn-rounded" />
            </div>
        </div>

        
            
         <center>
        <asp:Label ID="lblErrorMessage"  ForeColor="Red"   runat="server"></asp:Label>
         </center>

        <div class="panel-body">
            <div class="form-group">
                <asp:GridView ID="grdStateList" runat="server" ShowHeaderWhenEmpty="True" EmptyDataText="No records Found" Width="100%"
                    CssClass="mygrdContent" PagerStyle-CssClass="pager" HeaderStyle-CssClass="header" RowStyle-CssClass="rows" AutoGenerateColumns="false"
                    OnRowCommand="grdStateList_RowCommand" AllowPaging="true" OnPageIndexChanging="grdStateList_PageIndexChanging" PageSize="10">                    
                    <Columns>
                        <asp:BoundField DataField="country_name" HeaderText="Country Name" />    
                        <asp:BoundField DataField="state_name" HeaderText="State" />    
                        <asp:TemplateField >
                            <ItemTemplate>
                                <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn btn-success pull-right" CommandName="EditValue" CommandArgument='<%# Eval("state_id") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField >
                            <ItemTemplate>
                                <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-warning pull-right" CommandName="DeleteValue" CommandArgument='<%# Eval("state_id") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="pagination-ys" Wrap="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:GridView>
            </div>
        </div>

     

    </div>
</asp:Content>
