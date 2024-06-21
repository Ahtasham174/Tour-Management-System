Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration

Partial Class addTour
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("isAuthenticated") Is Nothing OrElse Not Convert.ToBoolean(Session("isAuthenticated")) Then
            Response.Redirect("Default.aspx")

        ElseIf Not Convert.ToBoolean(Session("isCompany")) Then
            Response.Redirect("Tours.aspx")
        End If


        If Not IsPostBack Then
            PopulateDestinations()
        End If
    End Sub

    Protected Sub PopulateDestinations()
        Dim companyId As Integer = Convert.ToInt32(Session("CompanyID"))
        Dim connectionString As String = ConfigurationManager.ConnectionStrings("PVFC").ConnectionString

        Using connection As New SqlConnection(connectionString)
            Using command As New SqlCommand("SELECT DestinationID, Name FROM Destinations WHERE CompanyID = @CompanyID", connection)
                command.Parameters.AddWithValue("@CompanyID", companyId)
                Try
                    connection.Open()
                    Dim reader As SqlDataReader = command.ExecuteReader()
                    DestinationID.DataSource = reader
                    DestinationID.DataTextField = "Name"
                    DestinationID.DataValueField = "DestinationID"
                    DestinationID.DataBind()
                Catch ex As SqlException
                    ' Handle any errors that may have occurred
                    Response.Write("Error: " & ex.Message)
                End Try
            End Using
        End Using

    End Sub

    Protected Sub btnCreateTourPackage_Click(sender As Object, e As EventArgs)
        Dim connectionString As String = ConfigurationManager.ConnectionStrings("PVFC").ConnectionString

        ' Getting values from the controls using FindControl
        Dim packageName As String = DirectCast(FindControl("PackageName"), TextBox).Text
        Dim description As String = DirectCast(FindControl("Description"), TextBox).Text
        Dim price As Decimal = Convert.ToDecimal(DirectCast(FindControl("Price"), TextBox).Text)
        Dim durationInDays As Integer = Convert.ToInt32(DirectCast(FindControl("DurationInDays"), TextBox).Text)
        Dim transportationMode As String = DirectCast(FindControl("TransportationMode"), TextBox).Text
        Dim startingCity As String = DirectCast(FindControl("StartingCity"), TextBox).Text
        Dim tourGuide As String = DirectCast(FindControl("TourGuide"), DropDownList).SelectedValue
        Dim destinationId As Integer = Convert.ToInt32(DirectCast(FindControl("DestinationID"), DropDownList).SelectedValue)
        Dim accomodationDetails As String = DirectCast(FindControl("AccomodationDetails"), TextBox).Text

        ' Get the CompanyID from the session
        Dim companyId As Integer = Convert.ToInt32(Session("CompanyID"))

        Using connection As New SqlConnection(connectionString)
            Using command As New SqlCommand("INSERT INTO TourPackages (CompanyID, PackageName, Description, Price, DurationInDays, TransportationMode, StartingCity, TourGuide, DestinationID, AccomodationDetails) VALUES (@CompanyID, @PackageName, @Description, @Price, @DurationInDays, @TransportationMode, @StartingCity, @TourGuide, @DestinationID, @AccomodationDetails)", connection)
                command.Parameters.AddWithValue("@CompanyID", companyId)
                command.Parameters.AddWithValue("@PackageName", packageName)
                command.Parameters.AddWithValue("@Description", description)
                command.Parameters.AddWithValue("@Price", price)
                command.Parameters.AddWithValue("@DurationInDays", durationInDays)
                command.Parameters.AddWithValue("@TransportationMode", transportationMode)
                command.Parameters.AddWithValue("@StartingCity", startingCity)
                command.Parameters.AddWithValue("@TourGuide", tourGuide)
                command.Parameters.AddWithValue("@DestinationID", destinationId)
                command.Parameters.AddWithValue("@AccomodationDetails", accomodationDetails)

                Try
                    connection.Open()
                    command.ExecuteNonQuery()
                    Response.Redirect("CompanyTours.aspx")
                Catch ex As SqlException
                    ' Handle any errors that may have occurred
                    Response.Write("Error: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub


End Class
