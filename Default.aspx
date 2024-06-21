<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Voyager Vista</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous" />
    <link href="home.css" rel="stylesheet" />
    <link href="somee.css" rel="stylesheet" />
<style>
btn-secondary,
.btn-secondary:hover {
    color: #333;
    text-shadow: none;
}
    </style>
</head>

<body class="d-flex text-center text-white bg-dark">
    <div class="cover-container d-flex w-100 h-100 p-3 mx-auto flex-column">
        <header class="mb-auto">
            <div>
                <h3 class="float-md-left mb-0">VoyagerVista</h3>
                <nav class="nav nav-masthead justify-content-center float-md-right">
                    <a class="nav-link active" aria-current="page" href="#">Home</a>
                    <a class="nav-link" href="Tours.aspx">Tours</a>

                   <!--Add Logout functionality later (instead of register and login; logout appears when logged in-->
                    <a class="nav-link" href="Login.aspx">Login</a>
                    <a class="nav-link" href="Signup.aspx">Signup</a>
                    <a class="nav-link" href="companyRegister.aspx">Register your Company</a>

                </nav>
            </div>
        </header>
        <br>
        <main class="px-3">
            <h1>VoyagerVista</h1>
            <p class="lead"> Welcome to VoyagerVista! <br> Jump right in and explore. <br> </p><br>
            <a href="Tours.aspx" class="btn btn-lg btn-secondary font-weight-bold border-white bg-white">View Tours</a>
        </main>

        <footer class="mt-auto text-white-50">
            <p>&copy; 2024 </p>
        </footer>


    </div>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>

</body>
</html>
