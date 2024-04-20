Imports MySql.Data.MySqlClient
Public Class frmSY
    Private Sub frmSY_Load(sender As Object, e As EventArgs) Handles Me.Load
        SirPaya.darkLemon(DataGridView1, SirPaya.Mode.NORMAL)
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        With frmSYDetails
            .ShowDialog()
        End With
    End Sub

    Sub LoadReacords()
        Try
            DataGridView1.Rows.Clear()
            cn.Open()
            cm = New MySqlCommand("select * from tblsy", cn)
            dr = cm.ExecuteReader
            While dr.Read
                DataGridView1.Rows.Add(dr.Item(0).ToString, dr.Item(1).ToString, "OPEN SY", "CLOSE SY")
            End While
            dr.Close()
            cn.Close()
            DataGridView1.ClearSelection()
            lblCount.Text = DataGridView1.RowCount
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        Me.Dispose()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Dim colName As String = DataGridView1.Columns(e.ColumnIndex).Name
        If colName = "btnEdit" Then
            If MsgBox("Do you want to open this school year " & DataGridView1.Rows(e.RowIndex).Cells(0).Value.ToString & "?", vbYesNo + vbQuestion) = vbYes Then
                cn.Open()
                cm = New MySqlCommand("update tblsy set status = 'CLOSE'", cn)
                cm.ExecuteNonQuery()
                cn.Close()

                cn.Open()
                cm = New MySqlCommand("update tblsy set status = 'OPEN' where sy like '" & DataGridView1.Rows(e.RowIndex).Cells(0).Value.ToString & "'", cn)
                cm.ExecuteNonQuery()
                cn.Close()
                MsgBox("School year " & DataGridView1.Rows(e.RowIndex).Cells(0).Value.ToString & " successfully opened!", vbInformation)
                ActivityLog("Open school year " & DataGridView1.Rows(e.RowIndex).Cells(0).Value.ToString)
                LoadReacords()
            End If
        ElseIf colName = "btnDelete" Then
            If MsgBox("Do you want to close this school year " & DataGridView1.Rows(e.RowIndex).Cells(0).Value.ToString & "?", vbYesNo + vbQuestion) = vbYes Then
                cn.Open()
                cm = New MySqlCommand("update tblsy set status = 'CLOSE' where sy like '" & DataGridView1.Rows(e.RowIndex).Cells(0).Value.ToString & "'", cn)
                cm.ExecuteNonQuery()
                cn.Close()
                MsgBox("School year " & DataGridView1.Rows(e.RowIndex).Cells(0).Value.ToString & " successfully closed!", vbInformation)
                ActivityLog("Close school year " & DataGridView1.Rows(e.RowIndex).Cells(0).Value.ToString)
                LoadReacords()
            End If
        End If
        Dashboard()
    End Sub
End Class