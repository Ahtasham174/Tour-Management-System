Imports System.Data.SqlClient
Imports System.Configuration

Partial Class BookedTours
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        If Session("IsAuthenticated") Is Nothing OrElse Not Convert.ToBoolean(Session("IsAuthenticated")) Then
            Response.Redirect("Default.aspx")

        ElseIf Convert.ToBoolean(Session("isCompany")) Then
            Response.Redirect("CompanyDashboard.aspx")
        End If




        If Not IsPostBack Then
            LoadBookedTours()
        End If
    End Sub

    Private Sub LoadBookedTours()
        Dim connectionString As String = ConfigurationManager.ConnectionStrings("PVFC").ConnectionString
        Dim userId As Integer = Convert.ToInt32(Session("UserID"))

        Dim query As String = "SELECT b.BookingDate, b.NumberOfPeople, b.TotalPrice, b.PackageID, t.PackageName " &
                              "FROM Bookings b " &
                              "INNER JOIN Customers c ON b.CustomerID = c.CustomerID " &
                              "INNER JOIN TourPackages t ON b.PackageID = t.PackageID " &
                              "WHERE c.UserID = @UserID " &
                              "ORDER BY b.BookingDate DESC"

        Using connection As New SqlConnection(connectionString)
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@UserID", userId)

                Try
                    connection.Open()
                    Dim reader As SqlDataReader = command.ExecuteReader()

                    While reader.Read()
                        Dim bookingDate As DateTime = Convert.ToDateTime(reader("BookingDate"))
                        Dim numberOfPeople As Integer = Convert.ToInt32(reader("NumberOfPeople"))
                        Dim totalPrice As Decimal = Convert.ToDecimal(reader("TotalPrice"))
                        Dim packageId As Integer = Convert.ToInt32(reader("PackageID"))
                        Dim packageName As String = Convert.ToString(reader("PackageName"))

                        ' Create a panel to display booking information
                        Dim bookingPanel As New Panel()
                        bookingPanel.CssClass = "card mb-3"

                        ' Create the card body
                        Dim cardBody As New Literal()
                        cardBody.Text = "<div class='card-body'>"

                        ' Display booking details
                        cardBody.Text &= "<h5 class='card-title'>Booking Date: " & bookingDate.ToString("dd/MM/yyyy") & "</h5>"
                        cardBody.Text &= "<p class='card-text'>Tour Package: " & packageName & "</p>"
                        cardBody.Text &= "<p class='card-text'>Number of People: " & numberOfPeople & "</p>"
                        cardBody.Text &= "<p class='card-text'>Total Price: " & totalPrice.ToString("F2") & " Rs</p>"

                        ' Close the card body
                        cardBody.Text &= "</div>"
                        bookingPanel.Controls.Add(cardBody)

                        ' Add the booking panel to the placeholder
                        tourList.Controls.Add(bookingPanel)
                    End While

                    reader.Close()
                Catch ex As Exception
                    ' Handle any exceptions
                    Response.Write("Error: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub


    Protected Sub btnLogout_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnLogout.Click
        Session.Clear()
        Session.Abandon()
        Response.Redirect("Default.aspx")
    End Sub

End Class
