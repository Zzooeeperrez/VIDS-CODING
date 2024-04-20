Imports MySql.Data.MySqlClient
Public Class frmViolationSearch
    Private Sub frmViolationSearch_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Me.Dispose()
    End Sub

    Private Sub frmViolationSearch_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SirPaya.darkLemon(DataGridView1, SirPaya.Mode.NORMAL)
    End Sub

    Sub LoadReacords()
        Try
            DataGridView1.Rows.Clear()
            cn.Open()
            cm = New MySqlCommand("select * from tblviolation where violation like '%" & txtSearch.Text & "%'", cn)
            dr = cm.ExecuteReader
            While dr.Read
                DataGridView1.Rows.Add("SELECT", dr.Item(0).ToString, dr.Item(1).ToString, dr.Item(2).ToString, dr.Item(3).ToString, dr.Item(4).ToString)
            End While
            dr.Close()
            cn.Close()
            DataGridView1.ClearSelection()
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Try
            Dim colName As String = DataGridView1.Columns(e.ColumnIndex).Name
            If colName = "btnSelect" Then
                With frmStudentViolationDetails
                    ._vid = DataGridView1.Rows(e.RowIndex).Cells(1).Value.ToString
                    .txtType.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value.ToString
                    .txtViolation.Text = DataGridView1.Rows(e.RowIndex).Cells(3).Value.ToString
                    .cboOffense.Text = DataGridView1.Rows(e.RowIndex).Cells(4).Value.ToString
                    .cboSanction.Text = DataGridView1.Rows(e.RowIndex).Cells(5).Value.ToString
                    .LoadOffense()
                    Me.Dispose()
                End With
            End If
        Catch ex As Exception

            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub txtSearch_Click(sender As Object, e As EventArgs) Handles txtSearch.Click

    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        LoadReacords()
    End Sub
End Class