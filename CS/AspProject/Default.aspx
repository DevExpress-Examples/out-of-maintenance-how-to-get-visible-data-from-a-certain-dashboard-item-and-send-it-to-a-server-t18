<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AspProject.Default" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Dashboard.v14.1.Web, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.DashboardWeb" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="DashboardScripts.js" type="text/javascript"></script>
    <script type="text/javascript">
        function GetItemData() {
            var selectedItem = lbDashboardItems.GetSelectedItem();
            popupControl.SetHeaderText(selectedItem.text);
            if (!popupControl.GetVisible()) {
                popupControl.SetSize($(window).width() / 2, $(window).height() / 2);
                popupControl.Show();
            }
            CallbackPanel.PerformCallback(GetDashboardItemData(dashboardViewer, selectedItem.value));
        }
    </script>
    <style type="text/css">
        .padded-table td
        {
            padding: 2px;
        }
       
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <dx:ASPxPopupControl ID="ASPxPopupControl1" runat="server" ScrollBars="Auto"
        ClientInstanceName="popupControl" AllowDragging="True" AllowResize="True">        
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">                
                    <dx:ASPxCallbackPanel ID="ASPxCallbackPanel1" runat="server" CssClass="padded-table"  ClientInstanceName="CallbackPanel"
                        OnCallback="ASPxCallbackPanel1_Callback">
                        <PanelCollection>
<dx:PanelContent runat="server"></dx:PanelContent>
</PanelCollection>
                    </dx:ASPxCallbackPanel>                
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
    <div>
        <table style="width: 100%">
            <tr>
                <td style="vertical-align: top">
                    <dx:ASPxButton ID="ASPxButton1" runat="server" Text="Get Item Data" AutoPostBack="False">
                        <ClientSideEvents Click="function(s, e) { GetItemData( ); }" />
                    </dx:ASPxButton>
                    <dx:ASPxRadioButtonList ID="lbDashboardItems" runat="server" ClientInstanceName="lbDashboardItems">
                    </dx:ASPxRadioButtonList>
                </td>
                <td style="width: 100%">
                    <dx:ASPxDashboardViewer ID="ASPxDashboardViewer1" runat="server" DashboardSource="~/Dashboards/Invoices.xml"
                        FullscreenMode="True" OnConfigureDataConnection="ASPxDashboardViewer1_ConfigureDataConnection"
                        RegisterJQuery="True" ClientInstanceName="dashboardViewer" UseDataAccessApi="true">
                    </dx:ASPxDashboardViewer>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
