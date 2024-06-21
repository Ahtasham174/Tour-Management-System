<%@ Page Language="VB" AutoEventWireup="false" CodeFile="BookingPage.aspx.vb" Inherits="BookingPage" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Booking Page</title>
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"></script>
    <script type="text/javascript">
        function showConfirmation() {
            var numberOfPeople = document.getElementById('<%= NumberOfPeople.ClientID %>').value;
            var pricePerPerson = document.getElementById('<%= PricePerPerson.ClientID %>').value;
            var totalPrice = numberOfPeople * pricePerPerson;

            document.getElementById('modalTotalPrice').innerText = totalPrice.toFixed(2);
            $('#confirmationModal').modal('show');
        }

        function submitForm() {
            document.getElementById('<%= btnBookTour.ClientID %>').click();
        }
    </script>
    <link href="somee.css" rel="stylesheet" />
</head>
<body>



    <form id="form1" runat="server">
        <div class="container mt-5">

            <div class="row">
                <div class="col-md-6 offset-md-3 col-xl-4 offset-xl-4"> 
                    <div class="card shadow"> 
                          <img src="/Assets/picture_3.jpg" alt="" class="card-img-top" />
                       <div class="card-body">
                            <h5 class="card-title text-center">Book Your Tour</h5>

                            <div class="mb-3">
                <label for="NumberOfPeople" class="form-label">Number of People</label>
                <asp:TextBox ID="NumberOfPeople" runat="server" CssClass="form-control" type="number" min="1" required="required"></asp:TextBox>
            </div>

                            <div class="mb-3">
                <label for="PricePerPerson" class="form-label">Price Per Person</label>
                <asp:TextBox ID="PricePerPerson" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
            </div>

                            <div class="mb-3">
                <label for="PaymentMethod" class="form-label">Payment Method</label>
                <asp:DropDownList ID="PaymentMethod" runat="server" CssClass="form-control" required="required">
                    <asp:ListItem Text="Cash" Value="Cash"></asp:ListItem>
                    <asp:ListItem Text="Credit Card" Value="Credit Card"></asp:ListItem>
                </asp:DropDownList>
            </div>

                           <div class="d-grid gap-2 text-center">
                                <asp:Button ID="btnShowConfirmation" runat="server" Text="Book Tour" CssClass="btn btn-primary" OnClientClick="showConfirmation(); return false;" />
            <asp:Button ID="btnBookTour" runat="server" Text="Book Tour" CssClass="btn btn-primary" OnClick="btnBookTour_Click" Style="display:none;" />
     
                           </div>

                       </div>
                    </div>
                </div>
            </div>

            




        <!-- Modal -->
        <div class="modal fade" id="confirmationModal" tabindex="-1" role="dialog" aria-labelledby="confirmationModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="confirmationModalLabel">Confirm Booking</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        Total Price: Rs<span id="modalTotalPrice"></span>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancel</button>
                        <button type="button" class="btn btn-primary" onclick="submitForm()">Confirm</button>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
