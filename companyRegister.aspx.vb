Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Security.Cryptography
Imports System.Text

Partial Class companyRegister
    Inherits System.Web.UI.Page

    Protected Sub btnRegister_Click(sender As Object, e As EventArgs)
        If Page.IsValid Then
            Dim connectionString As String = ConfigurationManager.ConnectionStrings("PVFC").ConnectionString

            Dim checkResult As String = CheckUserExistsOrRegistrationNumber(CompanyName.Text, Email.Text, CompanyRegNum.Text, connectionString)

            If checkResult IsNot Nothing Then
                Response.Write("<script>alert('" & checkResult & "');</script>")
            Else
                Using connection As New SqlConnection(connectionString)
                    Using command As New SqlCommand("insert_user_and_company", connection)
                        command.CommandType = CommandType.StoredProcedure

                        ' Hash the password before storing it in the database
                        Dim hashedPassword As String = HashPassword(Password.Text)

                        command.Parameters.AddWithValue("@CompanyName", CompanyName.Text)
                        command.Parameters.AddWithValue("@Email", Email.Text)
                        command.Parameters.AddWithValue("@PhoneNumber", Contact.Text)
                        command.Parameters.AddWithValue("@Password", hashedPassword)
                        command.Parameters.AddWithValue("@UserType", "TourismCompany")
                        command.Parameters.AddWithValue("@Address", Address.Text)
                        command.Parameters.AddWithValue("@RegistrationNumber", CompanyRegNum.Text)

                        Try
                            connection.Open()
                            command.ExecuteNonQuery()
                            Response.Redirect("Login.aspx")
                        Catch ex As SqlException
                            ' Handle any errors that may have occurred
                            Response.Write("<script>alert('Error: " & ex.Message & "');</script>")
                        End Try
                    End Using
                End Using
            End If
        End If
    End Sub

    Private Function CheckUserExistsOrRegistrationNumber(username As String, email As String, registrationNumber As String, connectionString As String) As String
        Using connection As New SqlConnection(connectionString)
            Dim query As String = "SELECT " &
                                  "(SELECT COUNT(*) FROM Users WHERE Username = @Username) AS UsernameCount, " &
                                  "(SELECT COUNT(*) FROM Users WHERE Email = @Email) AS EmailCount, " &
                                  "(SELECT COUNT(*) FROM TourismCompanies WHERE RegistrationNumber = @RegistrationNumber) AS RegistrationNumberCount"
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@Username", username)
                command.Parameters.AddWithValue("@Email", email)
                command.Parameters.AddWithValue("@RegistrationNumber", registrationNumber)

                Try
                    connection.Open()
                    Using reader As SqlDataReader = command.ExecuteReader()
                        If reader.Read() Then
                            If Convert.ToInt32(reader("UsernameCount")) > 0 Then
                                Return "Company name is already registered. Please choose another."
                            End If
                            If Convert.ToInt32(reader("EmailCount")) > 0 Then
                                Return "Email is already registered. Please choose another."
                            End If
                            If Convert.ToInt32(reader("RegistrationNumberCount")) > 0 Then
                                Return "Registration number is already registered. Please choose another."
                            End If
                        End If
                    End Using
                    Return Nothing
                Catch ex As SqlException
                    ' Handle any errors that may have occurred
                    Return "Error checking user existence or registration number: " & ex.Message
                End Try
            End Using
        End Using
    End Function

    Private Function HashPassword(password As String) As String
        Using sha256 As SHA256 = SHA256.Create()
            Dim hashedBytes As Byte() = sha256.ComputeHash(Encoding.UTF8.GetBytes(password))
            Return Convert.ToBase64String(hashedBytes)
        End Using
    End Function

End Class