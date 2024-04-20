Imports MySql.Data.MySqlClient
Public Class frmLogin
    Private Sub frmLogin_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Me.Dispose()
    End Sub

    Private Sub frmLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Connection()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If MsgBox("Do you want to exit this application?", vbYesNo + vbQuestion) = vbYes Then
            End
        End If
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Try
            Dim found As Boolean = False
            If IS_EMPTY(txtUser) = True Then Return
            If IS_EMPTY(txtPass) = True Then Return
            cn.Open()
            cm = New MySqlCommand("select * from tbluser where username =@username and password = @password", cn)
            cm.Parameters.AddWithValue("@username", txtUser.Text)
            cm.Parameters.AddWithValue("@password", txtPass.Text)
            dr = cm.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                _user = dr.Item("username").ToString
                _pass = dr.Item("password").ToString
                _name = dr.Item("name").ToString
                _role = dr.Item("role").ToString
                found = True
            Else
                found = False
            End If
            dr.Close()
            cn.Close()

            If found = True Then
                MsgBox("Access granted! Welcom " & _name & "!", vbInformation)
                ActivityLog("User " & txtUser.Text & " successfully login.")
                With frmMain
                    txtUser.Clear()
                    txtPass.Clear()
                    If _role = "ADMINISTRATOR" Then .Button9.Enabled = True
                    If _role = "USER" Then .Button9.Enabled = False
                    .lblstrName.Text = _name
                    Me.Hide()
                    .Show()
                End With
            Else
                MsgBox("Access denied! Invalid username or password!", vbExclamation)
            End If
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub
End Class