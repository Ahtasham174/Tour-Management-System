Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Partial Class companyDashboard
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        If Session("isAuthenticated") Is Nothing OrElse Not Convert.ToBoolean(Session("isAuthenticated")) Then
            Response.Redirect("Default.aspx")

        ElseIf Not Convert.ToBoolean(Session("isCompany")) Then
            Response.Redirect("Tours.aspx")
        End If


        If Not IsPostBack Then
            GetCompanyDetails()
        End If
    End Sub

    Private Sub GetCompanyDetails()
        Dim userId As Integer = Convert.ToInt32(Session("UserID"))
        Dim connectionString As String = ConfigurationManager.ConnectionStrings("PVFC").ConnectionString

        Using connection As New SqlConnection(connectionString)
            Using command As New SqlCommand("SELECT CompanyID, CompanyName FROM TourismCompanies WHERE UserID = @UserID", connection)
                command.Parameters.AddWithValue("@UserID", userId)

                Try
                    connection.Open()
                    Using reader As SqlDataReader = command.ExecuteReader()
                        If reader.Read() Then
                            Dim companyId As Integer = Convert.ToInt32(reader("CompanyID"))
                            Dim companyName As String = reader("CompanyName").ToString()

                            ' You can now use these variables or store them in session for later use
                            Session("CompanyID") = companyId
                            Session("CompanyName") = companyName
                            Company.Controls.Add(New LiteralControl(companyName))

                        Else
                            Response.Write("No company details found for the logged-in user.")
                        End If
                    End Using
                Catch ex As SqlException
                    ' Handle any errors that may have occurred
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
