<%@ Page Language="VB" AutoEventWireup="false" CodeFile="BookedTours.aspx.vb" Inherits="BookedTours" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Booked Tours</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous" />
</head>
    <link href="somee.css" rel="stylesheet" />
<body>
    <form id="form1" runat="server">
        <nav class="navbar sticky-top navbar-expand-lg navbar-dark bg-dark">
            <div class="container-fluid">
                <a class="navbar-brand" href="#">VoyagerVista</a>
                <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNavAltMarkup" aria-controls="navbarNavAltMarkup" aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collapse navbar-collapse" id="navbarNavAltMarkup">
                    <div class="navbar-nav">
                        <a class="nav-link active" href="Tours.aspx">Tours</a>
                    </div>
                    <div class="navbar-nav ms-auto">
                        <asp:Button runat="server" ID="btnLogout" CssClass="btn nav-link" Text="Logout" OnClick="btnLogout_Click" />
                    </div>
                </div>
            </div>
        </nav>

        <main class="container mt-4">
            <div>
                <h1 class="mb-3 text-center">Booked Tours</h1>
            </div>
            <div>
                <br />
                <asp:PlaceHolder ID="tourList" runat="server"></asp:PlaceHolder>
            </div>
        </main>
    </form>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>
</body>
</html>
