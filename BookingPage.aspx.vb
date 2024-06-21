Imports System.Data.SqlClient
Imports System.Configuration

Partial Class BookingPage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        If Session("isAuthenticated") Is Nothing OrElse Not Convert.ToBoolean(Session("isAuthenticated")) Then
            Response.Redirect("Default.aspx")

        ElseIf Convert.ToBoolean(Session("isCompany")) Then
            Response.Redirect("CompanyDashboard.aspx")
        End If


        If Not IsPostBack Then
            LoadPackagePrice()
        End If
    End Sub

    Private Sub LoadPackagePrice()
        Dim connectionString As String = ConfigurationManager.ConnectionStrings("PVFC").ConnectionString
        Dim packageId As Integer = Convert.ToInt32(Session("itemID"))

        Using connection As New SqlConnection(connectionString)
            Using command As New SqlCommand("SELECT Price FROM TourPackages WHERE PackageID = @PackageID", connection)
                command.Parameters.AddWithValue("@PackageID", packageId)

                Try
                    connection.Open()
                    Dim pricePerPerson As Decimal = Convert.ToDecimal(command.ExecuteScalar())
                    DirectCast(FindControl("PricePerPerson"), TextBox).Text = pricePerPerson.ToString("F2")
                Catch ex As Exception
                    Response.Write("Error: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub

    Protected Sub btnBookTour_Click(sender As Object, e As EventArgs)
        Dim connectionString As String = ConfigurationManager.ConnectionStrings("PVFC").ConnectionString

        Dim packageId As Integer = Convert.ToInt32(Session("itemID"))
        Dim userId As Integer = Convert.ToInt32(Session("UserID"))
        Dim customerId As Integer = GetCustomerId(userId, connectionString)
        Dim bookingDate As DateTime = DateTime.Now
        Dim numberOfPeople As Integer = Convert.ToInt32(DirectCast(FindControl("NumberOfPeople"), TextBox).Text)
        Dim pricePerPerson As Decimal = Convert.ToDecimal(DirectCast(FindControl("PricePerPerson"), TextBox).Text)
        Dim totalPrice As Decimal = pricePerPerson * numberOfPeople
        Dim paymentMethod As String = DirectCast(FindControl("PaymentMethod"), DropDownList).SelectedValue

        Try
            Using connection As New SqlConnection(connectionString)
                connection.Open()

                ' Insert booking information
                Using command As New SqlCommand("INSERT INTO Bookings (PackageID, CustomerID, BookingDate, NumberOfPeople, TotalPrice) VALUES (@PackageID, @CustomerID, @BookingDate, @NumberOfPeople, @TotalPrice); SELECT SCOPE_IDENTITY();", connection)
                    command.Parameters.AddWithValue("@PackageID", packageId)
                    command.Parameters.AddWithValue("@CustomerID", customerId)
                    command.Parameters.AddWithValue("@BookingDate", bookingDate)
                    command.Parameters.AddWithValue("@NumberOfPeople", numberOfPeople)
                    command.Parameters.AddWithValue("@TotalPrice", totalPrice)

                    Dim bookingId As Integer = Convert.ToInt32(command.ExecuteScalar())

                    ' Insert payment information
                    Using commandPayment As New SqlCommand("INSERT INTO Payments (BookingID, PaymentDate, Amount, PaymentMethod) VALUES (@BookingID, @PaymentDate, @Amount, @PaymentMethod)", connection)
                        commandPayment.Parameters.AddWithValue("@BookingID", bookingId)
                        commandPayment.Parameters.AddWithValue("@PaymentDate", bookingDate)
                        commandPayment.Parameters.AddWithValue("@Amount", totalPrice)
                        commandPayment.Parameters.AddWithValue("@PaymentMethod", paymentMethod)

                        commandPayment.ExecuteNonQuery()
                    End Using
                End Using
            End Using

            ' Redirect to a success page or display a success message
            Response.Redirect("BookedTours.aspx")
        Catch ex As Exception
            ' Handle any exceptions
            Response.Write("Error: " & ex.Message)
        End Try
    End Sub

    Private Function GetCustomerId(userId As Integer, connectionString As String) As Integer
        Dim customerId As Integer = -1

        Using connection As New SqlConnection(connectionString)
            Using command As New SqlCommand("SELECT CustomerID FROM Customers WHERE UserID = @UserID", connection)
                command.Parameters.AddWithValue("@UserID", userId)

                Try
                    connection.Open()
                    customerId = Convert.ToInt32(command.ExecuteScalar())
                Catch ex As Exception
                    Response.Write("Error: " & ex.Message)
                End Try
            End Using
        End Using

        Return customerId
    End Function
End Class
