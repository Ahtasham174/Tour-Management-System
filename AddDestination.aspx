<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AddDestination.aspx.vb" Inherits="AddDestination" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add Destination</title>
</head>
<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous" />
<link href="somee.css" rel="stylesheet" />

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

        <br />
        <br />

        <div class="container mt-2">
            <div class="row">
                <div class="col-md-6 offset-md-3">
                    <div class="card">
                        <div class="card-body">
                            <h5 class="card-title">Add New Destination</h5>

                            <div class="mb-3">
                                <label for="DestinationName" class="form-label">Destination Name</label>
                                <asp:TextBox ID="DestinationName" runat="server" CssClass="form-control" required></asp:TextBox>
                            </div>

                            <div class="mb-3">
                                <label for="Description" class="form-label">Description</label>
                                <asp:TextBox ID="Description" runat="server" CssClass="form-control" TextMode="MultiLine" required></asp:TextBox>
                            </div>

                            <div class="mb-3">
                                <label for="Location" class="form-label">Location</label>
                                <asp:TextBox ID="Location" runat="server" CssClass="form-control" required></asp:TextBox>
                            </div>

                            <div class="mb-3">
                                <label for="Picture1Link" class="form-label">Picture 1 Link</label>
                                <asp:TextBox ID="Picture1Link" runat="server" CssClass="form-control" required></asp:TextBox>
                            </div>

                            <div class="mb-3">
                                <label for="Picture2Link" class="form-label">Picture 2 Link</label>
                                <asp:TextBox ID="Picture2Link" runat="server" CssClass="form-control" required></asp:TextBox>
                            </div>

                            <div class="d-grid">
                                <asp:Button ID="btnAddDestination" runat="server" CssClass="btn btn-primary" Text="Add Destination" OnClick="btnAddDestination_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </form>
</body>
</html>
