Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Security.Cryptography
Imports System.Text

Partial Class Signup
    Inherits System.Web.UI.Page

    Protected Sub BtnRegister_Click(sender As Object, e As EventArgs)
        If Page.IsValid Then
            Dim connectionString As String = ConfigurationManager.ConnectionStrings("PVFC").ConnectionString
            Dim userExists As Boolean = CheckUserExists(username.Text, email.Text, connectionString)

            If userExists Then
                Response.Write("<script>alert('Username or Email already exists. Please choose another.');</script>")
            Else
                Try
                    InsertUser(connectionString)
                    Response.Redirect("Login.aspx")
                Catch ex As Exception
                    Response.Write("<script>alert('Error: " & ex.Message & "');</script>")
                End Try
            End If
        End If
    End Sub

    Private Sub InsertUser(connectionString As String)
        Using connection As New SqlConnection(connectionString)
            Using command As New SqlCommand("insert_user_and_customer", connection)
                command.CommandType = CommandType.StoredProcedure

                ' Hash the password before storing it in the database
                Dim hashedPassword As String = HashPassword(Password.Text)

                command.Parameters.AddWithValue("@FirstName", FirstName.Text)
                command.Parameters.AddWithValue("@LastName", LastName.Text)
                command.Parameters.AddWithValue("@Username", username.Text)
                command.Parameters.AddWithValue("@Email", email.Text)
                command.Parameters.AddWithValue("@PhoneNumber", PhoneNumber.Text)
                command.Parameters.AddWithValue("@Password", hashedPassword)
                command.Parameters.AddWithValue("@UserType", "Customer")

                connection.Open()
                command.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Private Function CheckUserExists(username As String, email As String, connectionString As String) As Boolean
        Using connection As New SqlConnection(connectionString)
            Using command As New SqlCommand("SELECT COUNT(*) FROM Users WHERE Username = @Username OR Email = @Email", connection)
                command.Parameters.AddWithValue("@Username", username)
                command.Parameters.AddWithValue("@Email", email)
                connection.Open()
                Dim count As Integer = Convert.ToInt32(command.ExecuteScalar())
                Return count > 0
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