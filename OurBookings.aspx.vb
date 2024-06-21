Imports System.Data.SqlClient
Imports System.Configuration

Partial Class OurBookings
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        If Session("isAuthenticated") Is Nothing OrElse Not Convert.ToBoolean(Session("isAuthenticated")) Then
            Response.Redirect("Default.aspx")

        ElseIf Not Convert.ToBoolean(Session("isCompany")) Then
            Response.Redirect("Tours.aspx")
        End If


        If Not IsPostBack Then
            LoadCompanyBookings()
        End If
    End Sub

    Private Sub LoadCompanyBookings()
        Dim connectionString As String = ConfigurationManager.ConnectionStrings("PVFC").ConnectionString
        Dim companyId As Integer = Convert.ToInt32(Session("CompanyID"))

        Dim query As String = "SELECT b.BookingDate, b.NumberOfPeople, b.TotalPrice, b.PackageID, t.PackageName, c.FirstName, c.LastName, u.PhoneNumber " &
                              "FROM Bookings b " &
                              "INNER JOIN TourPackages t ON b.PackageID = t.PackageID " &
                              "INNER JOIN Customers c ON b.CustomerID = c.CustomerID " &
                              "INNER JOIN Users u ON c.UserID = u.UserID " &
                              "WHERE t.CompanyID = @CompanyID " &
                              "ORDER BY b.BookingDate DESC"

        Using connection As New SqlConnection(connectionString)
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@CompanyID", companyId)

                Try
                    connection.Open()
                    Dim reader As SqlDataReader = command.ExecuteReader()

                    While reader.Read()
                        Dim bookingDate As DateTime = Convert.ToDateTime(reader("BookingDate"))
                        Dim numberOfPeople As Integer = Convert.ToInt32(reader("NumberOfPeople"))
                        Dim totalPrice As Decimal = Convert.ToDecimal(reader("TotalPrice"))
                        Dim packageId As Integer = Convert.ToInt32(reader("PackageID"))
                        Dim packageName As String = Convert.ToString(reader("PackageName"))
                        Dim firstName As String = Convert.ToString(reader("FirstName"))
                        Dim lastName As String = Convert.ToString(reader("LastName"))
                        Dim phoneNumber As String = Convert.ToString(reader("PhoneNumber"))

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
                        cardBody.Text &= "<p class='card-text'>Total Price: $" & totalPrice.ToString("F2") & "</p>"
                        cardBody.Text &= "<p class='card-text'>Customer Name: " & firstName & " " & lastName & "</p>"
                        cardBody.Text &= "<p class='card-text'>Phone Number: " & phoneNumber & "</p>"

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
