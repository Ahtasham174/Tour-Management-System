<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Signup.aspx.vb" Inherits="Signup" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Signup</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous" />
    <link href="home.css" rel="stylesheet" />
    <link href="somee.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>

</head>
<body>
    <form id="myForm" class="validated-form" runat="server">
        <header class="mb-auto">
            <div>
                <h3 class="float-md-left mb-0">VoyagerVista</h3>
                <nav class="nav nav-masthead justify-content-center float-md-right">
                    <a class="nav-link" aria-current="page" href="Default.aspx">Home</a>
                    <a class="nav-link" href="Tours.aspx">Tours</a>
                    <a class="nav-link" href="Login.aspx">Login</a>
                    <a class="nav-link active" href="Signup.aspx">Signup</a>
                    <a class="nav-link" href="companyRegister.aspx">Register your Company</a>
                </nav>
            </div>
        </header>
        <br />
     
        <div class="container">
            <div class="row">
                <div class="col-md-6 offset-md-3 col-xl-4 offset-xl-4">
                    <div class="card shadow">
                        <div class="card-body">
                            <h5 class="card-title text-center">Signup</h5>
                            <div class="mb-3">
                                <label class="form-label" for="FirstName">First Name</label>
                                <asp:TextBox ID="FirstName" placeholder="Jon" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidatorFirstName" runat="server" ControlToValidate="FirstName" Display="Dynamic" CssClass="text-danger" ErrorMessage="Invalid First Name. Only letters are allowed." ValidationExpression="^[a-zA-Z]+$" ValidationGroup="SignUpValidationGroup"></asp:RegularExpressionValidator>

                            </div>
                            <div class="mb-3">
                                <label class="form-label" for="LastName">Last Name</label>
                                <asp:TextBox ID="LastName" placeholder="Walker" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidatorLastName" runat="server" ControlToValidate="LastName" Display="Dynamic" CssClass="text-danger" ErrorMessage="Invalid Last Name. Only letters are allowed." ValidationExpression="^[a-zA-Z]+$" ValidationGroup="SignUpValidationGroup"></asp:RegularExpressionValidator>

                            </div>
                            
                            <div class="mb-3">
                                <label class="form-label" for="username">Username</label>
                                <asp:TextBox ID="username" placeholder="jon13" runat="server" CssClass="form-control" required="required" />
                                <asp:RequiredFieldValidator ControlToValidate="username" Display="Dynamic" CssClass="text-danger" ErrorMessage="Username is required." runat="server" />
                                <asp:RegularExpressionValidator ControlToValidate="username" Display="Dynamic" CssClass="text-danger" ErrorMessage="Invalid Username format." runat="server" ValidationExpression="^[a-zA-Z0-9\s]{1,50}$" />
                            </div>
                      
                            <div class="mb-3">
                                <label class="form-label" for="email">Email</label>
                                <asp:TextBox ID="email" placeholder="Jon@email.com" runat="server" CssClass="form-control" required="required" />
                                <asp:RequiredFieldValidator ControlToValidate="email" Display="Dynamic" CssClass="text-danger" ErrorMessage="Email is required." runat="server" />
                                <asp:RegularExpressionValidator ControlToValidate="email" Display="Dynamic" CssClass="text-danger" ErrorMessage="Invalid email format." runat="server" ValidationExpression="^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$" />
                            </div>

                            <div class="mb-3">
                                <label class="form-label" for="PhoneNumber">Phone Number</label>
                                <asp:TextBox ID="PhoneNumber" placeholder="11-digit" runat="server" CssClass="form-control" required="required" />
                                <asp:RequiredFieldValidator ControlToValidate="PhoneNumber" Display="Dynamic" CssClass="text-danger" ErrorMessage="Phone number is required." runat="server" />
                                <asp:RegularExpressionValidator ControlToValidate="PhoneNumber" Display="Dynamic" CssClass="text-danger" ErrorMessage="Invalid Phone number format. Must be 11 digits." runat="server" ValidationExpression="^\d{11}$" />
                            </div>
                            <div class="mb-3">
                                <label class="form-label" for="Password">Password</label>
                                <asp:TextBox ID="Password" placeholder="min 6 characters" TextMode="Password" runat="server" CssClass="form-control" required="required" />
                                <asp:RequiredFieldValidator ControlToValidate="Password" Display="Dynamic" CssClass="text-danger" ErrorMessage="Password is required." runat="server" />
                                <asp:RegularExpressionValidator ControlToValidate="Password" Display="Dynamic" CssClass="text-danger" ErrorMessage="Password must be at least 6 characters long." runat="server" ValidationExpression="^.{6,}$" />
                            </div>
                            <div class="d-grid gap-2">
                                <asp:Button runat="server" ID="btnRegister" CssClass="btn btn-success btn-block" Text="Register" ValidationGroup="SignUpValidationGroup" OnClick="btnRegister_Click" />
                            </div>
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="SignUpValidationGroup" DisplayMode="List" CssClass="alert alert-danger mt-3" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
