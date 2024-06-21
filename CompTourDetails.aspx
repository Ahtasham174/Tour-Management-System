<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CompTourDetails.aspx.vb" Inherits="CompTourDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Tour Details</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous" />
<link href="somee.css" rel="stylesheet" />
</head>

<body>
    <form id="form1" runat="server">
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
                    <a class="nav-link active" href="CompanyDashboard.aspx">Dashboard</a>
                     <asp:Button runat="server" ID="btnLogout" CssClass="btn nav-link" Text="Logout" OnClick="btnLogout_Click" />
                </div>
            </div>
        </div>
    </nav>


     <main class="container mt-4">
    
         <div class="row"> 
      <div class="col-6"> 
        <div class="container mt-4">

            <div class="card mb-3">
                <!--<img class="d-block w-100" src="<asp:PlaceHolder ID='Picture1' runat='server' />" alt="Picture 1" /> -->
            <img class="d-block w-100" src="<asp:PlaceHolder ID='Picture2' runat='server' />" alt="Picture 2" />
                <div class="card-body">
                    <h4 class="card-title">
                        <asp:PlaceHolder ID="Title" runat="server"></asp:PlaceHolder>
                    </h4>
                    <p class="card-text">
                        <asp:PlaceHolder ID="Details" runat="server"></asp:PlaceHolder>
                    </p>
                </div>
                <ul class="list-group list-group-flush">
                     <li class="list-group-item">
                    </li>
                    <li class="list-group-item">
                        <asp:PlaceHolder ID="Destination" runat="server"></asp:PlaceHolder>
                    </li>
                    <li class="list-group-item">
                        <asp:PlaceHolder ID="Duration" runat="server"></asp:PlaceHolder>
                    </li>
                    <li class="list-group-item">
                        <asp:PlaceHolder ID="StartingCity" runat="server"></asp:PlaceHolder>
                    </li>
                    <li class="list-group-item">
                        <asp:PlaceHolder ID="TransportationMode" runat="server"></asp:PlaceHolder>
                    </li>
                    <li class="list-group-item">
                        <asp:PlaceHolder ID="AccomodationDetails" runat="server"></asp:PlaceHolder>
                    </li>
                    <li class="list-group-item">
                        <asp:PlaceHolder ID="Price" runat="server"></asp:PlaceHolder>
                    </li>
                    <li class="list-group-item">
                        <asp:PlaceHolder ID="Company" runat="server"></asp:PlaceHolder>
                    </li>
                   
                </ul>
                  <div class="card-footer text-body-secondary">
                      <div class="d-flex justify-content-center">
           
                          <asp:Button ID="DeleteTour" runat="server" Text="Delete Tour" OnClick="Delete_Package"  CssClass="btn btn-danger" OnClientClick="return confirm('Are you sure you want to delete this tour package?');" />
        </div>

                  </div>

                </div>
            </div>
        </div>

             <div class="col-6">

    <br />
    <asp:PlaceHolder ID="Reviews" runat="server"></asp:PlaceHolder>
 </div>

          </div>



          </main>
     </form>
  


    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>
<br />
<br />
<br />
<br />
</body>
</html>