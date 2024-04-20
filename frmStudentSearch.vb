Imports MySql.Data.MySqlClient
Public Class frmStudentSearch
    Private Sub frmStudentSearch_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SirPaya.darkLemon(DataGridView1, SirPaya.Mode.NORMAL)
    End Sub
    Sub LoadReacords()
        Try
            DataGridView1.Rows.Clear()
            cn.Open()
            cm = New MySqlCommand("select * from VWENROLL where fullname like '%" & txtSearch.Text & "%'  and sy like '" & _sy & "'", cn)
            dr = cm.ExecuteReader
            While dr.Read
                DataGridView1.Rows.Add(dr.Item("id").ToString, dr.Item("lrn").ToString, dr.Item("fullname").ToString, dr.Item("grade").ToString, dr.Item("section").ToString, dr.Item("parent").ToString, dr.Item("pcontact").ToString, "SELECT")
            End While
            dr.Close()
            cn.Close()
            DataGridView1.ClearSelection()
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

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Dim colname As String = DataGridView1.Columns(e.ColumnIndex).Name
        If colname = "btnSelect" Then
            With frmStudentViolationDetails
                ._id = DataGridView1.Rows(e.RowIndex).Cells(1).Value.ToString
                .txtLRN.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value.ToString
                .ttxName.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value.ToString
                .txtYear.Text = DataGridView1.Rows(e.RowIndex).Cells(3).Value.ToString & " " & DataGridView1.Rows(e.RowIndex).Cells(4).Value.ToString
                .txtParents.Text = DataGridView1.Rows(e.RowIndex).Cells(5).Value.ToString
                .txtContact.Text = DataGridView1.Rows(e.RowIndex).Cells(6).Value.ToString
                cn.Open()
                cm = New MySqlCommand("select pic from tblstudent where id like '" & DataGridView1.Rows(e.RowIndex).Cells(0).Value.ToString & "'", cn)
                dr = cm.ExecuteReader
                dr.Read()
                If dr.HasRows Then
                    Dim Len1 As Long = dr.GetBytes(0, 0, Nothing, 0, 0)
                    Dim Array1(CInt(Len1)) As Byte
                    dr.GetBytes(0, 0, Array1, 0, CInt(Len1))

                    Dim MemoryStream1 As New System.IO.MemoryStream(Array1)
                    Dim Bitmap1 As New System.Drawing.Bitmap(MemoryStream1)
                    .picImage.BackgroundImage = Bitmap1
                End If
                dr.Close()
                cn.Close()
                Me.Dispose()
                .LoadReacords()
            End With
        End If

    End Sub

    Private Sub frmStudentSearch_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Me.Dispose()
    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Me.Dispose()
    End Sub
End Class