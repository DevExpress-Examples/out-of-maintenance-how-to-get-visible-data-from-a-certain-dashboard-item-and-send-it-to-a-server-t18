Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.DataAccess.ConnectionParameters

Namespace WinFormsProject
    Partial Public Class DesignerForm
        Inherits Form

        Public Sub New()
            InitializeComponent()
        End Sub



        Private Sub DesignerForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            dashboardDesigner1.LoadDashboard("../../../Invoices.xml")
        End Sub

        Private Sub dashboardDesigner1_ConfigureDataConnection(ByVal sender As Object, ByVal e As DevExpress.DashboardCommon.DashboardConfigureDataConnectionEventArgs) Handles dashboardDesigner1.ConfigureDataConnection
            Select Case e.ConnectionName
                Case "nwindConnection"
                    Dim access As Access97ConnectionParameters = TryCast(e.ConnectionParameters, Access97ConnectionParameters)
                    If access IsNot Nothing Then
                        access.FileName = "../../../nwind.mdb"
                    End If
            End Select
        End Sub
    End Class
End Namespace
