Imports MySql.Data.MySqlClient
Public Class frmStudentViolation
    Private Sub frmStudentViolation_Load(sender As Object, e As EventArgs) Handles Me.Load
        SirPaya.darkLemon(DataGridView1, SirPaya.Mode.NORMAL)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        With frmStudentViolationDetails
            .txtSY.Text = _sy
            .ShowDialog()
        End With
    End Sub

    Sub LoadReacords()
        Try
            If cboGrade.Text = "" Then Return
            DataGridView1.Rows.Clear()
            cn.Open()
            If cboGrade.Text = "All" Then
                cm = New MySqlCommand("select * from vwViolation where studentname like '%" & txtSearch.Text & "%' and sy like '" & _sy & "' order by studentname", cn)
            Else
                cm = New MySqlCommand("select * from vwViolation where studentname like '%" & txtSearch.Text & "%' and section like '" & cboGrade.Text & "%' and sy like '" & _sy & "' order by studentname", cn)
            End If
            dr = cm.ExecuteReader
            While dr.Read
                DataGridView1.Rows.Add(dr.Item("id").ToString, dr.Item("lrn").ToString, dr.Item("studentname").ToString, dr.Item("section").ToString, dr.Item("type").ToString, dr.Item("violation").ToString, dr.Item("offense").ToString, dr.Item("sanction").ToString, dr.Item("remarks").ToString, dr.Item("sy").ToString, "DELETE")
            End While
            dr.Close()
            cn.Close()
            lblCount.Text = DataGridView1.RowCount
            DataGridView1.ClearSelection()
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Try
            Dim colname As String = DataGridView1.Columns(e.ColumnIndex).Name
            If colname = "btnDelete" Then
                If MsgBox("Do you want to delete this record?", vbYesNo + vbQuestion) = vbYes Then
                    cn.Open()
                    cm = New MySqlCommand("delete from tblstudentviolation where id like '" & DataGridView1.Rows(e.RowIndex).Cells(0).Value.ToString & "'", cn)
                    cm.ExecuteNonQuery()
                    cn.Close()
                    MsgBox("Record successfully deleted!", vbInformation)
                    ActivityLog("Deleting student " & DataGridView1.Rows(e.RowIndex).Cells(2).Value.ToString & " violation record.")
                    LoadReacords()
                    Dashboard()
                End If
            End If
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub txtSearch_Click(sender As Object, e As EventArgs) Handles txtSearch.Click

    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        LoadReacords()
    End Sub

    Private Sub cboGrade_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboGrade.SelectedIndexChanged
        LoadReacords()
    End Sub

    Sub LoadLevel()
        Try
            cboGrade.Items.Clear()
            cboGrade.Items.Add("All")
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

    Private Sub cboGrade_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboGrade.KeyPress
        e.Handled = True
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Me.Dispose()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        With frmPrintPreview
            If cboGrade.Text = "All" Then
                .Vio("select * from vwViolation where studentname like '%" & txtSearch.Text & "%' and sy like '" & _sy & "' order by studentname")
            Else
                .Vio("select * from vwViolation where studentname like '%" & txtSearch.Text & "%' and section like '" & cboGrade.Text & "%' and sy like '" & _sy & "' order by studentname")
            End If
            .ShowDialog()
        End With
    End Sub
End Class