Imports MySql.Data.MySqlClient
Public Class frmChangePassword
    Private Sub frmChangePassword_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Me.Dispose()
    End Sub

    Private Sub frmChangePassword_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Clear()
    End Sub
    Sub Clear()
        txtOld.Clear()
        txtPass.Clear()
        txtRetype.Clear()
        txtOld.Focus()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If IS_EMPTY(txtOld) = True Then Return
            If IS_EMPTY(txtPass) = True Then Return
            If IS_EMPTY(txtRetype) = True Then Return
            If txtOld.Text <> _pass Then
                MsgBox("Old password did not match!", vbExclamation)
                Return
            End If
            If txtPass.Text <> txtRetype.Text Then
                MsgBox("Retype password did not match!", vbExclamation)
                Return
            End If
            If MsgBox("Do you want to change your password?", vbYesNo + vbQuestion) = vbYes Then
                cn.Open()
                cm = New MySqlCommand("update tbluser set password =@password where username =@username", cn)
                With cm
                    .Parameters.AddWithValue("@password", txtPass.Text)
                    .Parameters.AddWithValue("@username", _user)
                    .ExecuteNonQuery()
                End With
                cn.Close()
                MsgBox("Password successfully changed!", vbInformation)
                ActivityLog("Change password.")
                Clear()
            End If
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Me.Dispose()
    End Sub
End Class