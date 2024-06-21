Imports System.Data.SqlClient
Imports System.Configuration

Partial Class TourDetails
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        Dim isAuthenticated As Boolean = False

        If Session("isAuthenticated") IsNot Nothing Then
            isAuthenticated = Convert.ToBoolean(Session("isAuthenticated"))
        End If

        ' Set visibility based on authentication status


        If isAuthenticated Then
            If Convert.ToBoolean(Session("isCompany")) Then
                booking.NavigateUrl = "OurBookings.aspx"
            Else
                booking.NavigateUrl = "BookedTours.aspx"
                CompanyDashoboardLink.Visible = False
            End If
        End If



        If Session("IsAuthenticated") Is Nothing Then
            Response.Redirect("Default.aspx")

        ElseIf Convert.ToBoolean(Session("isCompany")) Or Not Convert.ToBoolean(Session("isAuthenticated")) Then
            ' Condition that if not logged as Customer in then the reviews must not be visible
            Reviewdiv.Controls.Add(New LiteralControl("visually-hidden"))
            BookToura.Controls.Add(New LiteralControl("visually-hidden"))
        End If

        If Not IsPostBack Then
            If Request.QueryString("PackageID") IsNot Nothing Then
                Dim packageName As String = Request.QueryString("PackageID").ToString()
                FetchTourDetails(packageName)

            End If
        End If
    End Sub

    Private Sub FetchTourDetails(ByVal packageName As String)
        Dim connectionString As String = ConfigurationManager.ConnectionStrings("PVFC").ConnectionString

        Try
            Using connection As New SqlConnection(connectionString)
                connection.Open()
                Dim query As String = "SELECT tp.PackageID, tp.PackageName, tc.CompanyName, tp.Description, tp.DurationInDays, tp.TransportationMode, tp.StartingCity, d.Name AS DestinationName, tp.AccomodationDetails, tp.Price, d.Picture1Link, d.Picture2Link " &
                                      "FROM TourPackages tp " &
                                      "JOIN TourismCompanies tc ON tp.CompanyID = tc.CompanyID " &
                                      "JOIN Destinations d ON tp.DestinationID = d.DestinationID " &
                                      "WHERE tp.PackageName = @packageName"

                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@packageName", packageName)
                    Using reader As SqlDataReader = command.ExecuteReader()
                        If reader.Read() Then
                            ' Display the fetched details on the page
                            DisplayTourDetails(reader)
                        Else
                            ' Package not found, display a message
                            Title.Controls.Add(New LiteralControl("<h1>Package not found</h1>"))
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ' Handle any exceptions here
            Response.Write("Error: An Error Accured While Fethcing Data From DB")
        End Try
    End Sub

    Private Sub DisplayTourDetails(ByVal reader As SqlDataReader)
        Dim PackageID As Integer = Integer.Parse(reader("PackageID"))
        Title.Controls.Add(New LiteralControl(reader("PackageName").ToString()))
        Company.Controls.Add(New LiteralControl("<strong>Owned By:</strong> " & reader("CompanyName").ToString()))
        Details.Controls.Add(New LiteralControl("<strong></strong> " & reader("Description").ToString()))
        Duration.Controls.Add(New LiteralControl("<strong>Duration:</strong> " & reader("DurationInDays").ToString() & " days"))
        TransportationMode.Controls.Add(New LiteralControl("<strong>Transportation Mode:</strong> " & reader("TransportationMode").ToString()))
        StartingCity.Controls.Add(New LiteralControl("<strong>Starting City:</strong> " & reader("StartingCity").ToString()))
        Destination.Controls.Add(New LiteralControl("<strong>Destination:</strong> " & reader("DestinationName").ToString()))
        AccomodationDetails.Controls.Add(New LiteralControl("<strong>Accomodation Details:</strong> " & reader("AccomodationDetails").ToString()))
        Price.Controls.Add(New LiteralControl("<strong>Price:</strong> Rs." & reader("Price").ToString()))

        Dim picturesHtml As String = String.Empty
        Dim picture1Link As String = reader("Picture1Link").ToString()
        Dim picture2Link As String = reader("Picture2Link").ToString()

        Picture1.Controls.Add(New LiteralControl(reader("Picture1Link").ToString()))
        Picture2.Controls.Add(New LiteralControl(reader("Picture2Link").ToString()))

        Session("itemID") = PackageID

        FetchTourReviews(PackageID)


        'Also save this data in session as well as userdata to the use in payments and other pages

    End Sub

    Private Sub FetchTourReviews(ByVal packageID As Integer)
        Dim connectionString As String = ConfigurationManager.ConnectionStrings("PVFC").ConnectionString

        Try
            Using connection As New SqlConnection(connectionString)
                connection.Open()
                ' Fetch reviews for the tour
                Dim query As String = "SELECT r.Rating, r.ReviewText, r.ReviewDate, c.FirstName, c.LastName " &
                                  "FROM Reviews r " &
                                  "JOIN Customers c ON r.CustomerID = c.CustomerID " &
                                  "WHERE r.PackageID = @packageID"

                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@packageID", packageID)
                    Using reader As SqlDataReader = command.ExecuteReader()
                        While reader.Read()
                            ' Display review details
                            Dim rating As Integer = Convert.ToInt32(reader("Rating"))
                            Dim reviewText As String = reader("ReviewText").ToString()
                            Dim reviewDate As Date = Convert.ToDateTime(reader("ReviewDate"))
                            Dim customerName As String = reader("FirstName").ToString() & " " & reader("LastName").ToString()

                            ' Create review HTML using Bootstrap card
                            Dim reviewHtml As String = "<div class='card mb-3'>" &
                                                   "<div class='card-body'>" &
                                                   "<h5 class='card-title'>Rating: " & rating.ToString() & "</h5>" &
                                                   "<p class='card-text'>Review: " & reviewText & "</p>" &
                                                   "<p class='card-text'><small class='text-muted'>Posted By: " & customerName & "</small></p>" &
                                                   "<p class='card-text'><small class='text-muted'>Date: " & reviewDate.ToShortDateString() & "</small></p>" &
                                                   "</div>" &
                                                   "</div>"

                            ' Add review HTML to Reviews placeholder
                            Reviews.Controls.Add(New LiteralControl(reviewHtml))
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ' Handle any exceptions here
            Response.Write("Error fetching reviews: " & ex.Message)
        End Try
    End Sub


    Protected Sub Submit_Review(ByVal sender As Object, ByVal e As EventArgs)
        ' Retrieve the rating value from the TextBox using DirectCast
        Dim ratingTextBox As TextBox = DirectCast(FindControl("rating"), TextBox)
        Dim ratingValue As Integer

        ' Validate the rating value
        If Integer.TryParse(ratingTextBox.Text, ratingValue) AndAlso ratingValue >= 1 AndAlso ratingValue <= 5 Then
            Dim connectionString As String = ConfigurationManager.ConnectionStrings("PVFC").ConnectionString
            Dim userId As Integer = Convert.ToInt32(Session("UserID"))

            ' Ensure itemID and customerID retrieval are correct
            Dim packageID As Integer = Convert.ToInt32(Session("itemID"))
            Dim customerID As Integer = GetCustomerId(userId, connectionString)
            Dim reviewText As String = rtext.Text
            Dim reviewDate As Date = Date.Now

            Try
                Using connection As New SqlConnection(connectionString)
                    connection.Open()
                    Dim query As String = "INSERT INTO Reviews (PackageID, CustomerID, Rating, ReviewText, ReviewDate) VALUES (@PackageID, @CustomerID, @Rating, @ReviewText, @ReviewDate)"

                    Using command As New SqlCommand(query, connection)
                        command.Parameters.AddWithValue("@PackageID", packageID)
                        command.Parameters.AddWithValue("@CustomerID", customerID)
                        command.Parameters.AddWithValue("@Rating", ratingValue)
                        command.Parameters.AddWithValue("@ReviewText", reviewText)
                        command.Parameters.AddWithValue("@ReviewDate", reviewDate)
                        command.ExecuteNonQuery()
                    End Using
                End Using
                ' Redirect to the same page after submitting the review
                Response.Redirect(Request.Url.ToString())
            Catch ex As Exception
                ' Handle the exception
                Response.Write("Error submitting review: " & ex.Message)
            End Try
        Else
            ' Display an error message if the rating is not valid
            Response.Write("Please enter a valid rating between 1 and 5.")
        End If
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
