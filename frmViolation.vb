Imports MySql.Data.MySqlClient
Public Class frmViolation
    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        Me.Dispose()
    End Sub

    Sub LoadReacords()
        Try
            DataGridView1.Rows.Clear()
            cn.Open()
            cm = New MySqlCommand("select * from tblviolation where violation like '%" & txtSearch.Text & "%'", cn)
            dr = cm.ExecuteReader
            While dr.Read
                DataGridView1.Rows.Add(dr.Item(0).ToString, dr.Item(1).ToString, dr.Item(2).ToString, dr.Item(3).ToString, dr.Item(4).ToString, "EDIT", "DELETE")
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

    Private Sub frmViolation_Load(sender As Object, e As EventArgs) Handles Me.Load
        SirPaya.darkLemon(DataGridView1, SirPaya.Mode.NORMAL)
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        With frmViolationDetails
            .btnUpdate.Enabled = False
            .ShowDialog()
        End With
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Try
            Dim colName As String = DataGridView1.Columns(e.ColumnIndex).Name
            If colName = "btnEdit" Then
                With frmViolationDetails
                    ._id = DataGridView1.Rows(e.RowIndex).Cells(0).Value.ToString
                    .cboType.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value.ToString
                    .txtViolation.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value.ToString
                    .cboOffense.Text = DataGridView1.Rows(e.RowIndex).Cells(3).Value.ToString
                    .txtSanction.Text = DataGridView1.Rows(e.RowIndex).Cells(4).Value.ToString
                    .btnSave.Enabled = False
                    .ShowDialog()
                End With
            ElseIf colname = "btnDelete" Then
                If MsgBox("Do you want to delete this record?", vbYesNo + vbQuestion) = vbYes Then
                    cn.Open()
                    cm = New MySqlCommand("delete from tblviolation where id like '" & DataGridView1.Rows(e.RowIndex).Cells(0).Value.ToString & "'", cn)
                    cm.ExecuteNonQuery()
                    cn.Close()
                    MsgBox("Record has been successfully deleted!", vbInformation)
                    ActivityLog("Deleting " & DataGridView1.Rows(e.RowIndex).Cells(2).Value.ToString & " record.")
                    LoadReacords()
                    Dashboard()
                End If
            End If
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        LoadReacords()
    End Sub
End Class