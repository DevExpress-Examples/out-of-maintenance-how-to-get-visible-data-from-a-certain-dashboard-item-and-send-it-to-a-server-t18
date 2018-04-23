Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports DevExpress.DataAccess.ConnectionParameters
Imports DevExpress.DashboardCommon
Imports DevExpress.Web
Imports System.Web.UI.HtmlControls
Imports System.Web.Helpers

Namespace AspProject
    Partial Public Class [Default]
        Inherits System.Web.UI.Page

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
            If (Not IsPostBack) AndAlso (Not IsCallback) Then
                Dim dashboard As New Dashboard()

                If TypeOf ASPxDashboardViewer1.DashboardSource Is String Then
                    dashboard.LoadFromXml(Server.MapPath(CStr(ASPxDashboardViewer1.DashboardSource)))
                End If
                For Each item In dashboard.Items
                    If TryCast(item, DataDashboardItem) IsNot Nothing AndAlso Not(TypeOf item Is RangeFilterDashboardItem) Then
                        lbDashboardItems.Items.Add(item.Name, item.ComponentName)
                    End If
                Next item
                lbDashboardItems.SelectedIndex = 0
            End If
        End Sub

        Protected Sub ASPxDashboardViewer1_ConfigureDataConnection(ByVal sender As Object, ByVal e As DevExpress.DashboardWeb.ConfigureDataConnectionWebEventArgs)
            Select Case e.ConnectionName
                Case "nwindConnection"
                    Dim access As Access97ConnectionParameters = TryCast(e.ConnectionParameters, Access97ConnectionParameters)
                    If access IsNot Nothing Then
                        access.FileName = Server.MapPath("~/App_Data/nwind.mdb")
                    End If
            End Select
        End Sub

        Protected Sub ASPxCallbackPanel1_Callback(ByVal sender As Object, ByVal e As DevExpress.Web.CallbackEventArgsBase)
            Dim panel As ASPxCallbackPanel = TryCast(sender, ASPxCallbackPanel)
            panel.Controls.Clear()
            Dim result As Object = Json.Decode(e.Parameter)
            panel.Controls.Add(New Label() With {.Text = result.I})

            Dim table As New HtmlTable()

            table.Border = 1
            For Each row In result.D
                Dim tableRow As New HtmlTableRow()
                For Each cell In row
                    Dim tableCell As New HtmlTableCell() With {.InnerText = Convert.ToString(cell)}
                    'tableCell.Controls.Add( new Label() { Text = Convert.ToString( cell ) });
                    tableRow.Cells.Add(tableCell)
                Next cell
                table.Rows.Add(tableRow)
            Next row
            panel.Controls.Add(table)
        End Sub




    End Class
End Namespace