Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Security.Cryptography
Imports System.Text

Partial Class Login
    Inherits System.Web.UI.Page

    Protected Sub btnRegister_Click(sender As Object, e As EventArgs) Handles btnRegister.Click
        Dim connectionString As String = ConfigurationManager.ConnectionStrings("PVFC").ConnectionString

        Using connection As New SqlConnection(connectionString)
            Dim query As String = "SELECT UserID, Username, UserType, Email, PhoneNumber, Password FROM Users WHERE Email = @Email"
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@Email", email.Value)

                Try
                    connection.Open()
                    Using reader As SqlDataReader = command.ExecuteReader()
                        If reader.Read() Then
                            ' Get the hashed password from the database
                            Dim storedHashedPassword As String = reader("Password").ToString()
                            ' Hash the entered password
                            Dim enteredHashedPassword As String = HashPassword(password.Value)

                            ' Compare the hashed passwords
                            If storedHashedPassword = enteredHashedPassword Then
                                ' User authenticated successfully, store user information in session variables
                                Session("UserID") = reader("UserID")
                                Session("Username") = reader("Username")
                                Session("UserType") = reader("UserType")
                                Session("Email") = reader("Email")
                                Session("PhoneNumber") = reader("PhoneNumber")
                                Session("isAuthenticated") = True

                                ' Redirect to a different page based on UserType
                                If reader("UserType").ToString() = "TourismCompany" Then
                                    Session("isCompany") = True
                                    Response.Redirect("CompanyDashboard.aspx")
                                ElseIf reader("UserType").ToString() = "Customer" Then
                                    Session("isCompany") = False
                                    Response.Redirect("Tours.aspx")
                                End If
                            Else
                                ' Authentication failed
                                Session("isAuthenticated") = False
                                Response.Write("Invalid email or password. Please try again.")
                            End If
                        Else
                            ' Authentication failed
                            Session("isAuthenticated") = False
                            Response.Write("Invalid email or password. Please try again.")
                        End If
                    End Using
                Catch ex As SqlException
                    ' Handle any errors that may have occurred
                    Response.Write("Error: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub

    Private Function HashPassword(password As String) As String
        Using sha256 As SHA256 = SHA256.Create()
            Dim hashedBytes As Byte() = sha256.ComputeHash(Encoding.UTF8.GetBytes(password))
            Return Convert.ToBase64String(hashedBytes)
        End Using
    End Function

End Class