<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Login.aspx.vb" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous" />
    <link href="home.css" rel="stylesheet" />
    <link href="somee.css" rel="stylesheet" />
</head>
    
<body>
    <form id="myForm" class="validated-form" runat="server">
        <header class="mb-auto">
            <div>
                <h3 class="float-md-left mb-0">VoyagerVista</h3>
                <nav class="nav nav-masthead justify-content-center float-md-right">
                    <a class="nav-link " aria-current="page" href="Default.aspx">Home</a>
                    <a class="nav-link" href="Tours.aspx">Tours</a>
                    <a class="nav-link active" href="Login.aspx">Login</a>
                    <a class="nav-link " href="Signup.aspx">Signup</a>
                    <a class="nav-link " href="companyRegister.aspx">Register your Company</a>
                </nav>
            </div>
        </header>
        <br />
        <div class="container ">
            <div class="row">
                <div class="col-md-6 offset-md-3 col-xl-4 offset-xl-4">
                    <div class="card shadow">
                        <img src="/Assets/picture_5.jpg" alt="" class="card-img-top" />
                        <div class="card-body">
                            <h5 class="card-title text-center">Login</h5>
                            <div class="mb-3">
                                <label class="form-label" for="email">Email</label>
                                <input type="text" id="email" name="email" class="form-control" runat="server" required="required" pattern="[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}" title="Please enter a valid email address" />
                            </div>
                            <div class="mb-3">
                                <label class="form-label" for="password">Password</label>
                                <input type="password" id="password" name="password" class="form-control" runat="server" required="required" />
                            </div>
                            <div class="d-grid gap-2">
                                <asp:Button runat="server" ID="btnRegister" CssClass="btn btn-success btn-block" OnClick="btnRegister_Click" Text="Login" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
