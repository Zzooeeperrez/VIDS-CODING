Imports MySql.Data.MySqlClient
Public Class frmActivityLog
    Private Sub frmActivityLog_Load(sender As Object, e As EventArgs) Handles Me.Load
        SirPaya.darkLemon(DataGridView1, SirPaya.Mode.NORMAL)
    End Sub

    Sub LoadReacords()
        Try
            DataGridView1.Rows.Clear()
            cn.Open()
            cm = New MySqlCommand("select * from tbllog where summary like '%" & txtSearch.Text & "%' or suser like '%" & txtSearch.Text & "%' and sdate between '" & dt1.Value.ToString("yyyy-MM-dd") & "' and '" & dt2.Value.ToString("yyyy-MM-dd") & "'", cn)
            dr = cm.ExecuteReader
            While dr.Read
                DataGridView1.Rows.Add(dr.Item(1).ToString, dr.Item(2).ToString, Format(CDate(dr.Item(3).ToString).ToShortDateString), dr.Item(4).ToString)
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

    Private Sub dt2_ValueChanged(sender As Object, e As EventArgs) Handles dt2.ValueChanged
        LoadReacords()
    End Sub

    Private Sub dt1_ValueChanged(sender As Object, e As EventArgs) Handles dt1.ValueChanged
        LoadReacords()
    End Sub

    Private Sub txtSearch_Click(sender As Object, e As EventArgs) Handles txtSearch.Click

    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        LoadReacords()
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Me.Dispose()
    End Sub
End Class