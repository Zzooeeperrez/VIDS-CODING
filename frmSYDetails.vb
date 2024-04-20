Imports MySql.Data.MySqlClient
Public Class frmSYDetails
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If IS_EMPTY(txtSY) = True Then Return

            If MsgBox("Are you sure you want to create new school year " & txtSY.Text & "'?", vbYesNo + vbQuestion) = vbYes Then
                cn.Open()
                cm = New MySqlCommand("update tblsy set status = 'CLOSE'", cn)
                cm.ExecuteNonQuery()
                cn.Close()

                cn.Open()
                cm = New MySqlCommand("insert into tblsy(sy) values (@sy)", cn)
                cm.Parameters.AddWithValue("@sy", txtSY.Text)
                cm.ExecuteNonQuery()
                cn.Close()
                MsgBox("New school year successfully created.", vbInformation)
                ActivityLog("Creating new school year " & txtSY.Text)
                txtSY.Clear()
                txtSY.Focus()
                frmSY.LoadReacords()
                GetSY()
                Dashboard()
                Me.Dispose()
            End If
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub frmSYDetails_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Me.Dispose()
    End Sub

    Private Sub frmSYDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        txtSY.Clear()
        txtSY.Focus()
    End Sub
End Class