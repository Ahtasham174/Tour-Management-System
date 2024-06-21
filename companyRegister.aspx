<%@ Page Language="VB" AutoEventWireup="false" CodeFile="companyRegister.aspx.vb" Inherits="companyRegister" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Company Registration</title>
</head>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous" />

<link href="home.css" rel="stylesheet" />
    <link href="somee.css" rel="stylesheet" />
<body>

    <form id="myForm" class="validated-form" runat="server">
        <header class="mb-auto">
            <div>
                <h3 class="float-md-left mb-0">VoyagerVista</h3>
                <nav class="nav nav-masthead justify-content-center float-md-right">
                    <a class="nav-link " aria-current="page" href="Default.aspx">Home</a>
                    <a class="nav-link" href="Tours.aspx">Tours</a>
                    <a class="nav-link" href="Login.aspx">Login</a>
                    <a class="nav-link" href="Signup.aspx">Signup</a>
                    <a class="nav-link active" href="companyRegister.aspx">Register your Company</a>
                </nav>
            </div>
        </header>
        <br />
        <div class="container">
            <div class="row">
                <div class="col-md-6 offset-md-3 col-xl-4 offset-xl-4">
                    <div class="card shadow">
                        <div class="card-body">
                            <h5 class="card-title text-center">Register</h5>

                            <div class="mb-3">
                                <label class="form-label" for="CompanyName">Company Name</label>
                                <asp:TextBox ID="CompanyName" placeholder="company name" runat="server" CssClass="form-control" required="required" />
                                <asp:RequiredFieldValidator ControlToValidate="CompanyName" Display="Dynamic" CssClass="text-danger" ErrorMessage="Company name is required." runat="server" />
                                <asp:RegularExpressionValidator ControlToValidate="CompanyName" Display="Dynamic" CssClass="text-danger" ErrorMessage="Invalid company name format." runat="server" ValidationExpression="^[a-zA-Z0-9\s]{1,50}$" />
                            </div>

                            <div class="mb-3">
                                <label class="form-label" for="Email">Email</label>
                                <asp:TextBox ID="Email" placeholder="abc@email.com" runat="server" CssClass="form-control" required="required" />
                                <asp:RequiredFieldValidator ControlToValidate="Email" Display="Dynamic" CssClass="text-danger" ErrorMessage="Email is required." runat="server" />
                                <asp:RegularExpressionValidator ControlToValidate="Email" Display="Dynamic" CssClass="text-danger" ErrorMessage="Invalid email format." runat="server" ValidationExpression="^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$" />
                            </div>

                            <div class="mb-3">
                                <label class="form-label" for="CompanyRegNum">Registration Number</label>
                                <asp:TextBox ID="CompanyRegNum" placeholder="8-digit Reg Number"  runat="server" CssClass="form-control" required="required" />
                                <asp:RequiredFieldValidator ControlToValidate="CompanyRegNum" Display="Dynamic" CssClass="text-danger" ErrorMessage="Registration number is required." runat="server" />
                                <asp:RegularExpressionValidator ControlToValidate="CompanyRegNum" Display="Dynamic" CssClass="text-danger" ErrorMessage="Invalid registration number format." runat="server" ValidationExpression="^[0-9]{8,8}$" />
                            </div>

                            <div class="mb-3">
                                <label class="form-label" for="Address">Office Address</label>
                                <asp:TextBox ID="Address" placeholder="Address" runat="server" CssClass="form-control" required="required" />
                                <asp:RequiredFieldValidator ControlToValidate="Address" Display="Dynamic" CssClass="text-danger" ErrorMessage="Office address is required." runat="server" />
                                <asp:RegularExpressionValidator ControlToValidate="Address" Display="Dynamic" CssClass="text-danger" ErrorMessage="Invalid address format." runat="server" ValidationExpression="^[a-zA-Z0-9\s,.-]{1,100}$" />
                            </div>

                            <div class="mb-3">
                                <label class="form-label" for="Contact">Contact Number</label>
                                <asp:TextBox ID="Contact" placeholder="11-digit" runat="server" CssClass="form-control" required="required" />
                                <asp:RequiredFieldValidator ControlToValidate="Contact" Display="Dynamic" CssClass="text-danger" ErrorMessage="Contact number is required." runat="server" />
                                <asp:RegularExpressionValidator ControlToValidate="Contact" Display="Dynamic" CssClass="text-danger" ErrorMessage="Invalid contact number format. Must be 11 digits." runat="server" ValidationExpression="^\d{11}$" />
                            </div>

                            <div class="mb-3">
                                <label class="form-label" for="Password">Password</label>
                                <asp:TextBox ID="Password" placeholder="min 6 characters" TextMode="Password" runat="server" CssClass="form-control" required="required" />
                                <asp:RequiredFieldValidator ControlToValidate="Password" Display="Dynamic" CssClass="text-danger" ErrorMessage="Password is required." runat="server" />
                                <asp:RegularExpressionValidator ControlToValidate="Password" Display="Dynamic" CssClass="text-danger" ErrorMessage="Password must be at least 6 characters long." runat="server" ValidationExpression="^.{6,}$" />
                            </div>

                            <div class="d-grid gap-2">
                                <asp:Button runat="server" ID="btnRegister" CssClass="btn btn-success btn-block" Text="Register" OnClick="btnRegister_Click" />
                            </div>
                            <asp:ValidationSummary DisplayMode="BulletList" CssClass="text-danger" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>


     <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>

</body>
</html>
