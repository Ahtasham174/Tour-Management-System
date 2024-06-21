<%@ Page Language="VB" AutoEventWireup="false" CodeFile="companyDashboard.aspx.vb" Inherits="companyDashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Company Dashboard</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous" />
    <link href="somee.css" rel="stylesheet" />
</head>
<body>
    <form id="form1"  runat="server">


         <nav class="navbar sticky-top navbar-expand-lg navbar-dark bg-dark mb-5">
        <div class="container-fluid">
            <a class="navbar-brand" href="#">VoyagerVista</a>
            <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNavAltMarkup" aria-controls="navbarNavAltMarkup" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="navbarNavAltMarkup">
                <div class="navbar-nav">
                    <a class="nav-link active" href="Tours.aspx">Tours</a>
                    <a class="nav-link active" href="OurBookings.aspx">Bookings</a>
                </div>
                <div class="navbar-nav ms-auto">
                     <asp:Button runat="server" ID="btnLogout" CssClass="btn nav-link" Text="Logout" OnClick="btnLogout_Click" />
                </div>
            </div>
        </div>
    </nav>

        <h1 class="text-center">Company Dashboard</h1>
        <br />

        <h3 class="text-center"><asp:PlaceHolder ID='Company' runat='server' /></h3>

        <main class="container mt-3 mb-3 pt-3 pb-3">
            <div class="row">
                <div class="col-6 text-center">
                    <div class="container mt-5 mb-5 pt-3 pb-3">
                    <a class="btn btn-success px-5 py-4" href="CompanyTours.aspx">Our Tours</a>
                        </div>
                </div>
                <div class="col-6 text-center">
                    <div class="container mt-5 mb-5 pt-3 pb-3  pl-3 pr-3 ">
                    <a class="btn btn-success  px-5 py-4" href="addTour.aspx">Add Tour</a>
                        </div>
                </div>
               
            </div>
             <div class=" text-center">
                    <div class="container mt-5 mb-5 pt-3 pb-3  pl-3 pr-3 ">
                    <a class="btn btn-success  px-5 py-4" href="AddDestination.aspx">Add Destination</a>
                        </div>
                </div>
            <div>
             
            </div>
        </main>
    </form>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>

</body>
</html>
