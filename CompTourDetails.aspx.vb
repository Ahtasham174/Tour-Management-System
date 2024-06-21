Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports System.Configuration

Partial Class CompTourDetails
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Session("isAuthenticated") Is Nothing OrElse Not Convert.ToBoolean(Session("isAuthenticated")) Then
            Response.Redirect("Default.aspx")
        ElseIf Not Convert.ToBoolean(Session("isCompany")) Then
            Response.Redirect("Tours.aspx")
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
            Response.Write("Error: An Error Occurred While Fetching Data From DB")
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
        AccomodationDetails.Controls.Add(New LiteralControl("<strong>Accommodation Details:</strong> " & reader("AccomodationDetails").ToString()))
        Price.Controls.Add(New LiteralControl("<strong>Price:</strong> Rs." & reader("Price").ToString()))

        Dim picturesHtml As String = String.Empty
        Dim picture1Link As String = reader("Picture1Link").ToString()
        Dim picture2Link As String = reader("Picture2Link").ToString()

        Picture1.Controls.Add(New LiteralControl(reader("Picture1Link").ToString()))
        Picture2.Controls.Add(New LiteralControl(reader("Picture2Link").ToString()))

        Session("itemID") = PackageID

        FetchTourReviews(PackageID)

        'Also save this data in session as well as userdata to use in payments and other pages
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

    Protected Sub Delete_Package(ByVal sender As Object, ByVal e As EventArgs) Handles DeleteTour.Click
        Dim packageID As Integer = Convert.ToInt32(Session("itemID"))
        Dim connectionString As String = ConfigurationManager.ConnectionStrings("PVFC").ConnectionString

        Try
            Using connection As New SqlConnection(connectionString)
                connection.Open()

                Dim deleteReviewsQuery As String = "DELETE FROM Reviews WHERE PackageID = @packageID"
                Using deleteReviewsCommand As New SqlCommand(deleteReviewsQuery, connection)
                    deleteReviewsCommand.Parameters.AddWithValue("@packageID", packageID)
                    deleteReviewsCommand.ExecuteNonQuery()
                End Using

                Dim query As String = "DELETE FROM TourPackages WHERE PackageID = @packageID"

                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@packageID", packageID)
                    command.ExecuteNonQuery()
                End Using
            End Using
            ' Redirect to the company dashboard or the list of tour packages
            Response.Redirect("CompanyTours.aspx") ' Change this to your target page
        Catch ex As Exception
            Response.Write("Error deleting tour package: " & ex.Message)
        End Try
    End Sub

    Protected Sub btnLogout_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnLogout.Click
        Session.Clear()
        Session.Abandon()
        Response.Redirect("Default.aspx")
    End Sub
End Class
