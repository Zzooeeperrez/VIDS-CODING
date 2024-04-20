Imports MySql.Data.MySqlClient
Public Class frmUserAccount
    Private Sub frmUserAccount_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Me.Dispose()
    End Sub

    Private Sub frmUserAccount_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If IS_EMPTY(txtUser) = True Then Return
            If IS_EMPTY(txtPass) = True Then Return
            If IS_EMPTY(txtRetype) = True Then Return
            If IS_EMPTY(txtName) = True Then Return
            If IS_EMPTY(cboRole) = True Then Return
            If txtPass.Text <> txtRetype.Text Then
                MsgBox("Re-type password did not match!", vbExclamation)
                Return
            End If
            If MsgBox("Do you want to save this account?", vbYesNo + vbQuestion) = vbYes Then
                cn.Open()
                cm = New MySqlCommand("insert into tbluser(username, password, name, role)values(@username, @password, @name, @role)", cn)
                With cm
                    .Parameters.AddWithValue("@username", txtUser.Text)
                    .Parameters.AddWithValue("@password", txtPass.Text)
                    .Parameters.AddWithValue("@name", txtName.Text)
                    .Parameters.AddWithValue("@role", cboRole.Text)
                    .ExecuteNonQuery()
                End With
                cn.Close()
                MsgBox("New account successfully saved!", vbInformation)
                ActivityLog("Creating new useraccount " & txtUser.Text)
                Clear()
            End If
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Sub Clear()
        txtName.Clear()
        txtPass.Clear()
        txtRetype.Clear()
        txtUser.Clear()
        cboRole.Text = ""
        txtUser.Focus()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Clear()
    End Sub

    Private Sub cboRole_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboRole.KeyPress
        e.Handled = True
    End Sub
End Class