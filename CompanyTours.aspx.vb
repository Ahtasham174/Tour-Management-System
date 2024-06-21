Imports System.Data.SqlClient
Imports System.Configuration

Partial Class CompanyTours
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        If Session("isAuthenticated") Is Nothing OrElse Not Convert.ToBoolean(Session("isAuthenticated")) Then
            Response.Redirect("Default.aspx")

        ElseIf Not Convert.ToBoolean(Session("isCompany")) Then
            Response.Redirect("Tours.aspx")
        End If


        If Not IsPostBack Then
            TourGeneration()
        End If
    End Sub

    Protected Sub TourGeneration()
        Dim connectionString As String = ConfigurationManager.ConnectionStrings("PVFC").ConnectionString
        Dim companyId As Integer = Convert.ToInt32(Session("CompanyID"))
        Try
            Using connection As New SqlConnection(connectionString)
                connection.Open()
                Dim query As String = "SELECT tp.PackageName, tp.DurationInDays AS Duration, d.Name AS Destination, tp.StartingCity, tc.CompanyName AS Company, d.Picture1Link FROM TourPackages tp JOIN Destinations d ON tp.DestinationID = d.DestinationID JOIN TourismCompanies tc ON tp.CompanyID = tc.CompanyID WHERE tp.CompanyID = @CompanyID"
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@CompanyID", companyId)
                    Using reader As SqlDataReader = command.ExecuteReader()
                        While reader.Read()
                            ' Create the card panel
                            Dim cardPanel As New Panel()
                            cardPanel.CssClass = "card mb-3"

                            ' Create the row div
                            Dim rowDiv As New Literal()
                            rowDiv.Text = "<div class='row'>"
                            cardPanel.Controls.Add(rowDiv)

                            ' Create the image column
                            Dim imgCol As New Literal()
                            Dim imgSrc As String = If(Not IsDBNull(reader("Picture1Link")), reader("Picture1Link").ToString(), "https://res.cloudinary.com/dvjtyfx3v/image/upload/v1695484389/samples/breakfast.jpg")
                            imgCol.Text = "<div class='col-md-4'><img class='img-fluid' alt='' src='" & imgSrc & "'></div>"
                            cardPanel.Controls.Add(imgCol)

                            ' Create the content column
                            Dim contentCol As New Literal()
                            contentCol.Text = "<div class='col-md-8'><div class='card-body'>"

                            ' Add tour details
                            contentCol.Text &= "<h4 class='card-title'>" & reader("PackageName").ToString() & "</h4>"
                            contentCol.Text &= "<p class='card-text'><strong>Duration:</strong> " & reader("Duration").ToString() & " days</p>"
                            contentCol.Text &= "<p class='card-text'><strong>Destination:</strong> " & reader("Destination").ToString() & "</p>"
                            contentCol.Text &= "<p class='card-text'><strong>Starting City:</strong> " & reader("StartingCity").ToString() & "</p>"
                            contentCol.Text &= "<p class='card-text'><strong>Company:</strong> " & reader("Company").ToString() & "</p>"

                            ' Add a details button (assuming you have a details page)
                            contentCol.Text &= "<a href='CompTourDetails.aspx?PackageID=" & reader("PackageName").ToString() & "' class='btn btn-primary'>Show Details</a>"

                            ' Close the content column
                            contentCol.Text &= "</div></div>"
                            cardPanel.Controls.Add(contentCol)

                            ' Close the row div
                            Dim rowDivClose As New Literal()
                            rowDivClose.Text = "</div>"
                            cardPanel.Controls.Add(rowDivClose)

                            ' Add the card panel to the placeholder
                            tourList.Controls.Add(cardPanel)
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ' Handle any exceptions here
            Response.Write("Error: " & ex.Message)
        End Try
    End Sub


    Protected Sub btnLogout_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnLogout.Click
        Session.Clear()
        Session.Abandon()
        Response.Redirect("Default.aspx")
    End Sub


End Class
