Imports System.Data.SqlClient
Imports System.Configuration

Partial Class AddDestination
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("isAuthenticated") Is Nothing OrElse Not Convert.ToBoolean(Session("isAuthenticated")) Then
            Response.Redirect("Default.aspx")

        ElseIf Not Convert.ToBoolean(Session("isCompany")) Then
            Response.Redirect("Tours.aspx")
        End If

    End Sub

    Protected Sub btnAddDestination_Click(sender As Object, e As EventArgs)
        Dim connectionString As String = ConfigurationManager.ConnectionStrings("PVFC").ConnectionString

        Dim companyId As Integer = Convert.ToInt32(Session("CompanyID"))
        Dim destinationName As String = DirectCast(FindControl("destinationName"), TextBox).Text
        Dim description As String = DirectCast(FindControl("description"), TextBox).Text
        Dim location As String = DirectCast(FindControl("location"), TextBox).Text
        Dim picture1Link As String = DirectCast(FindControl("picture1Link"), TextBox).Text
        Dim picture2Link As String = DirectCast(FindControl("picture2Link"), TextBox).Text

        If DestinationExistsForCompany(destinationName, companyId, connectionString) Then
            ' Destination with the same name already exists for this company
            Response.Write("Destination with the same name already exists for your company.")
            Return ' Exit the function to prevent further execution
        End If

        ' Continue with the insertion process
        Using connection As New SqlConnection(connectionString)
            Using command As New SqlCommand("INSERT INTO Destinations (Name, Description, Location, Picture1Link, Picture2Link, CompanyID) VALUES (@Name, @Description, @Location, @Picture1Link, @Picture2Link, @CompanyID)", connection)
                command.Parameters.AddWithValue("@Name", destinationName)
                command.Parameters.AddWithValue("@Description", description)
                command.Parameters.AddWithValue("@Location", location)
                command.Parameters.AddWithValue("@Picture1Link", picture1Link)
                command.Parameters.AddWithValue("@Picture2Link", picture2Link)
                command.Parameters.AddWithValue("@CompanyID", companyId.ToString())

                Try
                    connection.Open()
                    command.ExecuteNonQuery()
                    Response.Redirect("AddDestination.aspx")
                Catch ex As SqlException
                    ' Handle any errors that may have occurred
                    Response.Write("Error: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub

    Private Function DestinationExistsForCompany(destinationName As String, companyId As Integer, connectionString As String) As Boolean
        Using connection As New SqlConnection(connectionString)
            Using command As New SqlCommand("SELECT COUNT(*) FROM Destinations WHERE Name = @Name AND CompanyID = @CompanyID", connection)
                command.Parameters.AddWithValue("@Name", destinationName)
                command.Parameters.AddWithValue("@CompanyID", companyId)

                connection.Open()
                Dim count As Integer = Convert.ToInt32(command.ExecuteScalar())
                Return count > 0
            End Using
        End Using
    End Function





End Class
