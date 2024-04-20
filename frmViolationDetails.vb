Imports MySql.Data.MySqlClient
Public Class frmViolationDetails
    Public _id As String
    Private Sub frmViolationDetails_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Me.Dispose()
    End Sub

    Private Sub frmViolationDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If IS_EMPTY(cboType) = True Then Return
            If IS_EMPTY(txtViolation) = True Then Return
            If IS_EMPTY(txtSanction) = True Then Return
            Dim found As Boolean = False
            cn.Open()
            cm = New MySqlCommand("select * from tblviolation where type like '" & cboType.Text & "' and violation like '" & txtViolation.Text & "' and sanction like '" & txtSanction.Text & "' and offense like '" & cboOffense.Text & "'", cn)
            dr = cm.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                found = True
            Else
                found = False
            End If
            dr.Close()
            cn.Close()

            If found = True Then
                MsgBox("Duplicate entry! Unable to save.", vbExclamation)
                Return
            End If
            If MsgBox("Do you want to save this record?", vbYesNo + vbQuestion) = vbYes Then
                cn.Open()
                cm = New MySqlCommand("insert into tblviolation(type, violation, sanction, offense) values (@type, @violation, @sanction,@offense)", cn)
                cm.Parameters.AddWithValue("@type", cboType.Text)
                cm.Parameters.AddWithValue("@violation", txtViolation.Text)
                cm.Parameters.AddWithValue("@sanction", txtSanction.Text)
                cm.Parameters.AddWithValue("@offense", cboOffense.Text)
                cm.ExecuteNonQuery()
                cn.Close()
                MsgBox("Record successfully saved.", vbInformation)
                ActivityLog("Creating new violation " & txtViolation.Text)
                frmViolation.LoadReacords()
                Clear()
                Dashboard()
            End If
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Sub Clear()
        txtSanction.Clear()
        txtViolation.Clear()
        cboType.Text = ""
        cboOffense.Text = ""
        btnSave.Enabled = True
        btnUpdate.Enabled = False
        cboType.Focus()
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Try
            If IS_EMPTY(cboType) = True Then Return
            If IS_EMPTY(txtViolation) = True Then Return
            If IS_EMPTY(txtSanction) = True Then Return
            Dim found As Boolean = False
            cn.Open()
            cm = New MySqlCommand("select * from tblviolation where type like '" & cboType.Text & "' and violation like '" & txtViolation.Text & "' and sanction like '" & txtSanction.Text & "' and offense like '" & cboOffense.Text & "'", cn)
            dr = cm.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                found = True
            Else
                found = False
            End If
            dr.Close()
            cn.Close()

            If found = True Then
                MsgBox("Duplicate entry or no changes! Unable to save.", vbExclamation)
                Return
            End If

            If MsgBox("Do you want to update this record?", vbYesNo + vbQuestion) = vbYes Then
                cn.Open()
                cm = New MySqlCommand("update tblviolation set type=@type, violation=@violation, sanction=@sanction, offense=@offense where id =  @id", cn)
                cm.Parameters.AddWithValue("@type", cboType.Text)
                cm.Parameters.AddWithValue("@violation", txtViolation.Text)
                cm.Parameters.AddWithValue("@sanction", txtSanction.Text)
                cm.Parameters.AddWithValue("@offense", cboOffense.Text)
                cm.Parameters.AddWithValue("@id", _id)
                cm.ExecuteNonQuery()
                cn.Close()
                MsgBox("Record successfully updated.", vbInformation)
                frmViolation.LoadReacords()
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

    Private Sub cboType_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboType.KeyPress
        e.Handled = True
    End Sub

    Private Sub cboOffense_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboOffense.KeyPress
        e.Handled = True
    End Sub
End Class