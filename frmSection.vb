Imports MySql.Data.MySqlClient
Public Class frmSection
    Private Sub frmSection_Load(sender As Object, e As EventArgs) Handles Me.Load
        SirPaya.darkLemon(DataGridView1, SirPaya.Mode.NORMAL)
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        With frmSectionDetails
            .btnUpdate.Enabled = False
            .LoadLevel()
            .ShowDialog()
        End With
    End Sub

    Sub LoadReacords()
        Try
            DataGridView1.Rows.Clear()
            cn.Open()
            cm = New MySqlCommand("select * from tblsection", cn)
            dr = cm.ExecuteReader
            While dr.Read
                DataGridView1.Rows.Add(dr.Item(0).ToString, dr.Item(1).ToString, dr.Item(2).ToString, "EDIT", "DELETE")
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
            If colname = "btnEdit" Then
                With frmSectionDetails
                    ._id = DataGridView1.Rows(e.RowIndex).Cells(0).Value.ToString
                    .cboGrade.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value.ToString
                    .txtSection.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value.ToString

                    .btnSave.Enabled = False
                    .ShowDialog()
                End With
            ElseIf colname = "btnDelete" Then
                If MsgBox("Do you want to delete this record?", vbYesNo + vbQuestion) = vbYes Then
                    cn.Open()
                    cm = New MySqlCommand("delete from tblsection where id like '" & DataGridView1.Rows(e.RowIndex).Cells(0).Value.ToString & "'", cn)
                    cm.ExecuteNonQuery()
                    cn.Close()
                    ActivityLog("Deleting " & DataGridView1.Rows(e.RowIndex).Cells(1).Value.ToString & " section record.")
                    MsgBox("Record successfully deleted!", vbInformation)
                    LoadReacords()
                End If
            End If
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        Me.Dispose()
    End Sub
End Class