<!-- default file list -->
*Files to look at*:

* [DashboardScripts.js](./CS/AspProject/DashboardScripts.js) (VB: [DashboardScripts.js](./VB/AspProject/DashboardScripts.js))
* [Default.aspx](./CS/AspProject/Default.aspx) (VB: [Default.aspx](./VB/AspProject/Default.aspx))
* [Default.aspx.cs](./CS/AspProject/Default.aspx.cs) (VB: [Default.aspx](./VB/AspProject/Default.aspx))
* [DesignerForm.cs](./CS/WinFormsProject/DesignerForm.cs) (VB: [DesignerForm.vb](./VB/WinFormsProject/DesignerForm.vb))
* [Program.cs](./CS/WinFormsProject/Program.cs) (VB: [Program.vb](./VB/WinFormsProject/Program.vb))
<!-- default file list end -->
# How to get visible data from a certain dashboard item and send it to a server


This solution can be used with both <a href="https://documentation.devexpress.com/#Dashboard/clsDevExpressDashboardWebASPxDashboardViewertopic">ASPxDashboardViewer </a> and <a href="https://documentation.devexpress.com/#Dashboard/clsDevExpressDashboardWebMvcMVCxDashboardViewertopic">MVCxDashboardViewer </a> controls:<br />To request data displayed in the specific <a href="https://documentation.devexpress.com/#Dashboard/CustomDocument16718">Dashboard Item</a>, you should call <a href="https://documentation.devexpress.com/#Dashboard/DevExpressDashboardWebScriptsASPxClientDashboardViewer_GetItemDatatopic">GetItemData</a> methods of the <a href="https://documentation.devexpress.com/#Dashboard/clsDevExpressDashboardWebScriptsASPxClientDashboardViewertopic">ASPxClientDashboardViewer</a> object. <br />Then, you need to iterate through the item's elements (rows, cells, points, etc ), collect information about corresponding dimensions and measures and store it as JSON collection. You can send this collection to the server in a string format using the <a href="http://msdn.microsoft.com/en-us/library/ie/cc836459(v=vs.94).aspx">JSON.stringify Function</a>. To parse the string on the server, you can use the <a href="http://msdn.microsoft.com/en-us/library/system.web.helpers.json.decode(v=vs.111).aspx">Json.Decode Method</a>.<br /><br /><strong>See Also:</strong> <br /><a href="https://www.devexpress.com/Support/Center/p/T170990">T170990: How to retrieve last level data from Client Dashboard Item and display it using different ASP.NET controls</a><br /><a href="https://www.devexpress.com/Support/Center/p/T148767">T148767: How to obtain a dashboard item's client data in the Web Viewer</a>

<br/>


