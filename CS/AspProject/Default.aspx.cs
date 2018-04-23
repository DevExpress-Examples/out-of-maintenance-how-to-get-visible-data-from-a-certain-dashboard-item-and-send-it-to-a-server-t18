using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DashboardCommon;
using DevExpress.Web;
using System.Web.UI.HtmlControls;
using System.Web.Helpers;

namespace AspProject
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && !IsCallback)
            {
                Dashboard dashboard = new Dashboard(); ;
                if (ASPxDashboardViewer1.DashboardSource is string)
                {
                    dashboard.LoadFromXml(Server.MapPath((string)ASPxDashboardViewer1.DashboardSource));
                }
                foreach (var item in dashboard.Items)
                {
                    if (item as DataDashboardItem != null && !(item is RangeFilterDashboardItem))
                        lbDashboardItems.Items.Add(item.Name, item.ComponentName);
                }
                lbDashboardItems.SelectedIndex = 0;
            }
        }

        protected void ASPxDashboardViewer1_ConfigureDataConnection(object sender, DevExpress.DashboardWeb.ConfigureDataConnectionWebEventArgs e)
        {
            switch (e.ConnectionName)
            {
                case "nwindConnection":
                    Access97ConnectionParameters access = e.ConnectionParameters as Access97ConnectionParameters;
                    if (access != null)
                        access.FileName = Server.MapPath( @"~/App_Data/nwind.mdb");
                    break;                
            }
        }

        protected void ASPxCallbackPanel1_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.Controls.Clear();
            dynamic result = Json.Decode(e.Parameter);
            panel.Controls.Add(new Label() { Text = result.I });

            HtmlTable table = new HtmlTable();

            table.Border = 1;
            foreach (var row in result.D)
            {
                HtmlTableRow tableRow = new HtmlTableRow();
                foreach (var cell in row)
                {
                    HtmlTableCell tableCell = new HtmlTableCell() { InnerText = Convert.ToString(cell) };
                    //tableCell.Controls.Add( new Label() { Text = Convert.ToString( cell ) });
                    tableRow.Cells.Add(tableCell);
                }
                table.Rows.Add(tableRow);
            }
            panel.Controls.Add(table);
        }

        

        
    }
}