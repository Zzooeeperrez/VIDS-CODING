Imports MySql.Data.MySqlClient
Public Class frmSectionDetails
    Public _id As String
    Private Sub frmSectionDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Sub LoadLevel()
        Try
            cboGrade.Items.Clear()
            cn.Open()
            cm = New MySqlCommand("select * from tbllevel", cn)
            dr = cm.ExecuteReader
            While dr.Read
                cboGrade.Items.Add(dr.Item(1).ToString)
            End While
            dr.Close()
            cn.Close()
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If IS_EMPTY(cboGrade) = True Then Return
            If IS_EMPTY(txtSection) = True Then Return
            If MsgBox("Do you want to save this record?", vbYesNo + vbQuestion) = vbYes Then


                cn.Open()
                cm = New MySqlCommand("insert into tblsection(level, section) values (@level, @section)", cn)
                cm.Parameters.AddWithValue("@level", cboGrade.Text)
                cm.Parameters.AddWithValue("@section", txtSection.Text)
                cm.ExecuteNonQuery()
                cn.Close()
                MsgBox("Record successfully saved.", vbInformation)
                ActivityLog("Creating new section " & txtSection.Text & " for grade level " & cboGrade.Text)
                frmSection.LoadReacords()
                Clear()
            End If
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Sub Clear()
        cboGrade.Text = ""
        txtSection.Clear()
        btnSave.Enabled = True
        btnUpdate.Enabled = False
        cboGrade.Focus()
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Try
            If IS_EMPTY(cboGrade) = True Then Return
            If IS_EMPTY(txtSection) = True Then Return
            If MsgBox("Do you want to update this record?", vbYesNo + vbQuestion) = vbYes Then
                cn.Open()
                cm = New MySqlCommand("update tblsection set level=@level, section=@section where id = @id", cn)
                cm.Parameters.AddWithValue("@level", cboGrade.Text)
                cm.Parameters.AddWithValue("@section", txtSection.Text)
                cm.Parameters.AddWithValue("@id", _id)
                cm.ExecuteNonQuery()
                cn.Close()
                MsgBox("Record successfully updated.", vbInformation)
                ActivityLog("Updating section " & txtSection.Text & " for grade level " & cboGrade.Text)
                frmSection.LoadReacords()
                Clear()
                Me.Dispose()
            End If
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Clear()
    End Sub

    Private Sub cboGrade_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboGrade.KeyPress
        e.Handled = True
    End Sub

    Private Sub frmSectionDetails_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Me.Dispose()
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Me.Dispose()
    End Sub
End Class