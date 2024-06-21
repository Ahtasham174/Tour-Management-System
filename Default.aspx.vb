Imports System.Security.Cryptography
Imports System.Text
Imports System.Data.SqlClient
Imports System.Configuration


Partial Class _Default

    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        Session("isAuthenticated") = False
    End Sub




End Class
