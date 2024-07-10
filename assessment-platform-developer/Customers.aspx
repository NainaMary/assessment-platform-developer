<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="~/Customers.aspx.cs" Inherits="assessment_platform_developer.Customers" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title><%: Page.Title %> RPM Platform Developer Assessment</title>

    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
	</asp:PlaceHolder>

    <webopt:BundleReference runat="server" Path="~/Content/css" />
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server">
            <Scripts>
                <%--To learn more about bundling scripts in ScriptManager see https://go.microsoft.com/fwlink/?LinkID=301884 --%>
                <%--Framework Scripts--%>
                <asp:ScriptReference Name="MsAjaxBundle" />
                <asp:ScriptReference Name="jquery" />
                <asp:ScriptReference Name="WebForms.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebForms.js" />
                <asp:ScriptReference Name="WebUIValidation.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebUIValidation.js" />
                <asp:ScriptReference Name="MenuStandards.js" Assembly="System.Web" Path="~/Scripts/WebForms/MenuStandards.js" />
                <asp:ScriptReference Name="GridView.js" Assembly="System.Web" Path="~/Scripts/WebForms/GridView.js" />
                <asp:ScriptReference Name="DetailsView.js" Assembly="System.Web" Path="~/Scripts/WebForms/DetailsView.js" />
                <asp:ScriptReference Name="TreeView.js" Assembly="System.Web" Path="~/Scripts/WebForms/TreeView.js" />
                <asp:ScriptReference Name="WebParts.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebParts.js" />
                <asp:ScriptReference Name="Focus.js" Assembly="System.Web" Path="~/Scripts/WebForms/Focus.js" />
                <asp:ScriptReference Name="WebFormsBundle" />
                <%--Site Scripts--%>
            </Scripts>
        </asp:ScriptManager>

        <nav class="navbar navbar-expand-sm navbar-toggleable-sm navbar-dark bg-dark">
            <div class="container body-content">
                <a class="navbar-brand" runat="server" href="~/">RPM Platform Developer Assessment</a>
                <button type="button" class="navbar-toggler" data-bs-toggle="collapse" data-bs-target=".navbar-collapse" title="Toggle navigation" aria-controls="navbarSupportedContent"
                    aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collapse navbar-collapse d-sm-inline-flex justify-content-between">
                    <ul class="navbar-nav flex-grow-1">
                        <li class="nav-item">
                            <a class="nav-link" runat="server" href="~/">Home</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" runat="server" href="~/Customers">Customers</a>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>

        <div>
            <div class="container body-content">
                <h2>Customer Registry</h2>
                <asp:DropDownList runat="server" ID="CustomersDDL" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="CustomersDDL_SelectedIndexChanged" />
                <asp:Label ID="ErrMsgLabel" runat="server" Visible="False"></asp:Label>
            </div>

            <div class="container body-content">
                <div class="card">

                    <div class="card-body">

                        <div class="row justify-content-center">

                            <div class="col-md-6">
                                <%--<h1>Add customer</h1>--%>
                                <h1>
                                    <asp:Label ID="HeadingLabel" runat="server" CssClass="form-label"></asp:Label></h1>

                                <div class="form-group">
                                    <div class="form-group">
                                        <asp:Label ID="CustomerNameLabel" runat="server" Text="Name" CssClass="form-label"></asp:Label>
                                        <asp:TextBox ID="CustomerName" MaxLength="100" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvCustomerName" runat="server" ControlToValidate="CustomerName" ErrorMessage="Name is required." CssClass="text-danger" Display="Dynamic">    </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revCustomerName" runat="server" ControlToValidate="CustomerName" ErrorMessage="Name can only contain letters and spaces." ValidationExpression="^[a-zA-Z\s]+$" CssClass="text-danger" Display="Dynamic">    </asp:RegularExpressionValidator>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label ID="CustomerAddressLabel" runat="server" Text="Address" CssClass="form-label"></asp:Label>
                                        <asp:TextBox ID="CustomerAddress" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator
                                            ID="rfvCustomerAddress"
                                            runat="server"
                                            ControlToValidate="CustomerAddress"
                                            ErrorMessage="Address is required."
                                            CssClass="text-danger"
                                            Display="Dynamic">
    </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label ID="CustomerEmailLabel" runat="server" Text="Email" CssClass="form-label"></asp:Label>
                                        <asp:TextBox ID="CustomerEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator
                                            ID="rfvCustomerEmail"
                                            runat="server"
                                            ControlToValidate="CustomerEmail"
                                            ErrorMessage="Email is required."
                                            CssClass="text-danger"
                                            Display="Dynamic">
    </asp:RequiredFieldValidator>

                                        <asp:RegularExpressionValidator
                                            ID="revCustomerEmail"
                                            runat="server"
                                            ControlToValidate="CustomerEmail"
                                            ErrorMessage="Please enter a valid email address."
                                            ValidationExpression="^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"
                                            CssClass="text-danger"
                                            Display="Dynamic">
    </asp:RegularExpressionValidator>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label ID="CustomerPhoneLabel" runat="server" Text="Phone" CssClass="form-label"></asp:Label>
                                        <asp:TextBox ID="CustomerPhone" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RegularExpressionValidator
                                            ID="revCustomerPhone"
                                            runat="server"
                                            ControlToValidate="CustomerPhone"
                                            ErrorMessage="Invalid phone number format"
                                            ValidationExpression="^(\+?1\s*[-.\s]?)?(\(?[2-9][0-9]{2}\)?[-.\s]?)?[2-9][0-9]{2}[-.\s]?[0-9]{4}$"
                                            Display="Dynamic" CssClass="text-danger">
</asp:RegularExpressionValidator>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label ID="CustomerCityLabel" runat="server" Text="City" CssClass="form-label"></asp:Label>
                                        <asp:TextBox ID="CustomerCity" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label ID="CustomerStateLabel" runat="server" Text="Province/State" CssClass="form-label"></asp:Label>
                                        <asp:DropDownList ID="StateDropDownList" runat="server" CssClass="form-control" />
                                    </div>

                                    <div class="form-group">
                                        <asp:Label ID="CustomerZipLabel" runat="server" Text="Postal/Zip Code" CssClass="form-label"></asp:Label>
                                        <asp:TextBox ID="CustomerZip" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="revCustomerZip" runat="server"
                                            ControlToValidate="CustomerZip" CssClass="text-danger"
                                            ValidationExpression="^[ABCEGHJ-NPRSTVXY]\d[ABCEGHJ-NPRSTV-Z] \d[ABCEGHJ-NPRSTV-Z]\d$"
                                            ErrorMessage="Enter a valid Canadian Postal/ZIP code (e.g., K1A 0B1)"
                                            Display="Dynamic" />
                                    </div>

                                    <div class="form-group">
                                        <asp:Label ID="CustomerCountryLabel" runat="server" Text="Country" CssClass="form-label"></asp:Label>
                                        <asp:DropDownList ID="CountryDropDownList" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="CountryDropDownList_SelectedIndexChanged" />
                                    </div>

                                    <div class="form-group">
                                        <asp:Label ID="CustomerNotesLabel" runat="server" Text="Notes" CssClass="form-label"></asp:Label>
                                        <asp:TextBox ID="CustomerNotes" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>

                                    <h1>Customer contact details</h1>

                                    <div class="form-group">
                                        <asp:Label ID="ContactNameLabel" runat="server" Text="Name" CssClass="form-label"></asp:Label>
                                        <asp:TextBox ID="ContactName" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label ID="ContactEmailLabel" runat="server" Text="Email" CssClass="form-label"></asp:Label>
                                        <asp:TextBox ID="ContactEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RegularExpressionValidator
                                            ID="revContactEmail"
                                            runat="server"
                                            ControlToValidate="ContactEmail"
                                            ErrorMessage="Please enter a valid email address."
                                            ValidationExpression="^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"
                                            CssClass="text-danger"
                                            Display="Dynamic">
</asp:RegularExpressionValidator>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label ID="ContactPhoneLabel" class="col-form-label" runat="server" Text="Phone" CssClass="form-label"></asp:Label>
                                        <asp:TextBox ID="ContactPhone" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RegularExpressionValidator
                                            ID="revContactPhone"
                                            runat="server"
                                            ControlToValidate="ContactPhone"
                                            ErrorMessage="Invalid phone number format"
                                            ValidationExpression="^(\+?1\s*[-.\s]?)?(\(?[2-9][0-9]{2}\)?[-.\s]?)?[2-9][0-9]{2}[-.\s]?[0-9]{4}$"
                                            Display="Dynamic" CssClass="text-danger">
</asp:RegularExpressionValidator>
                                    </div>

                                    <div class="form-group p-1">
                                        <asp:Button ID="AddButton" class="btn btn-primary btn-md" runat="server" Text="Add" OnClick="AddButton_Click" />
                                        <asp:Button ID="UpdateButton" class="btn btn-primary btn-md" runat="server" Text="Update" OnClick="UpdateButton_Click" />
                                        <asp:Button ID="DeleteButton" class="btn btn-primary btn-md" runat="server" Text="Delete" OnClientClick="return confirm('Are you sure you want to delete this customer?');" OnClick="DeleteButton_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
