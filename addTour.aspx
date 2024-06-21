<%@ Page Language="VB" AutoEventWireup="false" CodeFile="addTour.aspx.vb" Inherits="addTour" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Create Tour Package</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous" />
</head>
<body>
    <form id="myForm" class="validated-form" runat="server">
        
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
                </div>
            </div>
        </div>
    </nav>

      
        <div class="container mt-2">
            <div class="row">
                <div class="col-md-6 offset-md-3">
                    <div class="card">
                        <div class="card-body">
                            <h5 class="card-title text-center">Create New Tour Package</h5>
                            <div class="mb-3">
                                <label for="PackageName" class="form-label">Package Name</label>
                                <asp:TextBox ID="PackageName" runat="server" CssClass="form-control" required="required"></asp:TextBox>
                            </div>
                            <div class="mb-3">
                                <label for="Description" class="form-label">Description</label>
                                <asp:TextBox ID="Description" runat="server" CssClass="form-control" TextMode="MultiLine" cols="30" rows="3" required="required"></asp:TextBox>
                            </div>
                            <div class="mb-3">
                                <label for="Price" class="form-label">Price</label>
                                <asp:TextBox ID="Price" runat="server" CssClass="form-control" type="number" min="1" required="required"></asp:TextBox>

                            </div>
                            <div class="mb-3">
                                <label for="DurationInDays" class="form-label">Duration (in days)</label>
                                <asp:TextBox ID="DurationInDays" runat="server" CssClass="form-control"  type="number" min="1" required="required"></asp:TextBox>
                            </div>
                            <div class="mb-3">
                                <label for="TransportationMode" class="form-label">Transportation Mode</label>
                                <asp:TextBox ID="TransportationMode" runat="server" CssClass="form-control" required="required"></asp:TextBox>
                            </div>
                            <div class="mb-3">
                                <label for="StartingCity" class="form-label">Starting City</label>
                                <asp:TextBox ID="StartingCity" runat="server" CssClass="form-control" required="required"></asp:TextBox>
                            </div>
                            <div class="mb-3">
                                <label for="TourGuide" class="form-label">Tour Guide (Yes/No)</label>
                                <asp:DropDownList ID="TourGuide" runat="server" CssClass="form-control" required="required">
                                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                    <asp:ListItem Value="No">No</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="mb-3">
                                <label for="DestinationID" class="form-label">Destination</label>
                                <asp:DropDownList ID="DestinationID" runat="server" CssClass="form-control" required="required">
                                    
                                </asp:DropDownList>
                            </div>
                            <div class="mb-3">
                                <label for="AccomodationDetails" class="form-label">Accommodation Details</label>
                                <asp:TextBox ID="AccomodationDetails" runat="server" CssClass="form-control" TextMode="MultiLine" required="required"></asp:TextBox>
                            </div>
                            <div class="d-grid">
                                <asp:Button runat="server" ID="btnCreateTourPackage" CssClass="btn btn-primary" Text="Create Tour Package" OnClick="btnCreateTourPackage_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
