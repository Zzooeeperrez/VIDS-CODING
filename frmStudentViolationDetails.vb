Imports MySql.Data.MySqlClient
Public Class frmStudentViolationDetails
    Public _id As String
    Public _vid As String
    Private Sub frmStudentViolationDetails_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Me.Dispose()
    End Sub

    Private Sub frmStudentViolationDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SirPaya.darkLemon(DataGridView1, SirPaya.Mode.NORMAL)
    End Sub

    Sub LoadOffense()
        Try
            cboOffense.Items.Clear()
            cboSanction.Items.Clear()
            cn.Open()
            cm = New MySqlCommand("select * from tblviolation where violation =@violation", cn)
            cm.Parameters.AddWithValue("@violation", txtViolation.Text)
            dr = cm.ExecuteReader
            While dr.Read
                cboOffense.Items.Add(dr.Item("offense").ToString)
                cboSanction.Items.Add(dr.Item("sanction").ToString)
            End While
            dr.Close()
            cn.Close()
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        With frmStudentSearch
            .LoadReacords()
            .ShowDialog()
        End With
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        With frmViolationSearch
            .LoadReacords()
            .ShowDialog()
        End With
    End Sub

    Sub LoadReacords()
        Try
            DataGridView1.Rows.Clear()
            cn.Open()
            cm = New MySqlCommand("select * from vwViolation where lrn like '" & txtLRN.Text & "'", cn)
            dr = cm.ExecuteReader
            While dr.Read
                DataGridView1.Rows.Add(dr.Item("type").ToString, dr.Item("violation").ToString, dr.Item("offense").ToString, dr.Item("sanction").ToString, dr.Item("remarks").ToString, dr.Item("sy").ToString, "DELETE")
            End While
            dr.Close()
            cn.Close()
            DataGridView1.ClearSelection()
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If IS_EMPTY(txtSY) = True Then Return
            If IS_EMPTY(txtLRN) = True Then Return
            If IS_EMPTY(txtViolation) = True Then Return
            Dim found As Boolean = False
            cn.Open()
            cm = New MySqlCommand("select * from vwviolation where lid =@lid and type =@type and violation=@violation and offense =@offense and sanction=@sanction and sy =@sy", cn)
            cm.Parameters.AddWithValue("@lid", _id)
            cm.Parameters.AddWithValue("@type", txtType.Text)
            cm.Parameters.AddWithValue("@violation", txtViolation.Text)
            cm.Parameters.AddWithValue("@offense", cboOffense.Text)
            cm.Parameters.AddWithValue("@sanction", cboSanction.Text)
            cm.Parameters.AddWithValue("@sy", txtSY.Text)
            dr = cm.ExecuteReader
            dr.Read()
            If dr.HasRows Then found = True Else found = False
            dr.Close()
            cn.Close()


            If found = True Then
                MsgBox("Duplicate offense for this student! Please select another offense.", vbExclamation)
                Return
            End If


            If MsgBox("Do you want to save this violation?", vbYesNo + vbQuestion) = vbYes Then
                cn.Open()

                cm = New MySqlCommand("insert into tblstudentviolation (lid, section, vid,  remarks, sy)values(@lid, @section , @vid,  @remarks, @sy)", cn)
                With cm
                    .Parameters.AddWithValue("@lid", _id)
                    .Parameters.AddWithValue("@section", txtYear.Text)
                    .Parameters.AddWithValue("@vid", _vid)
                    .Parameters.AddWithValue("@remarks", txtremarks.Text)
                    .Parameters.AddWithValue("@sy", txtSY.Text)
                    cm.ExecuteNonQuery()
                End With
                cn.Close()
                MsgBox("Record successfully saved!", vbInformation)
                ActivityLog("Creating new violation; student " & ttxName.Text & " violation " & txtViolation.Text)
                Clear()
                With frmStudentViolation
                    .LoadReacords()
                End With

                Dashboard()
            End If
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Sub Clear()
        picImage.BackgroundImage = Image.FromFile(Application.StartupPath & "\man1.png")

        txtLRN.Clear()
        ttxName.Clear()
        cboSanction.Text = ""
        txtType.Clear()
        txtViolation.Clear()
        txtremarks.Clear()
        txtYear.Clear()
        txtParents.Clear()
        txtContact.Clear()
        LoadReacords()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Clear()
    End Sub

    Private Sub cboSanction_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboSanction.KeyPress, cboOffense.KeyPress
        e.Handled = True
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Me.Dispose()
    End Sub

    Private Sub cboOffense_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboOffense.SelectedIndexChanged

    End Sub
End Class