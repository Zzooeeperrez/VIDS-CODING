Imports MySql.Data.MySqlClient
Public Class frmStudent
    Sub LoadReacords()
        Try
            Dim i As Integer
            DataGridView1.Rows.Clear()
            cn.Open()
            cm = New MySqlCommand("select * from tblstudent where lname like '%" & txtSearch.Text & "%' or fname like '%" & txtSearch.Text & "%'", cn)
            dr = cm.ExecuteReader
            While dr.Read
                i += 1
                DataGridView1.Rows.Add(dr.Item("id").ToString, dr.Item("lrn").ToString, dr.Item("lname").ToString, dr.Item("fname").ToString, dr.Item("mname").ToString, Format(CDate(dr.Item("bdate").ToString).ToShortDateString), dr.Item("gender").ToString, dr.Item("address").ToString, dr.Item("FATHER").ToString, dr.Item("FO").ToString, dr.Item("MOTHER").ToString, dr.Item("MO").ToString, dr.Item("pcontact").ToString, dr.Item("paddress").ToString, "EDIT", "DELETE")
            End While
            dr.Close()
            cn.Close()
            lblCount.Text = DataGridView1.RowCount
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub frmStudent_Load(sender As Object, e As EventArgs) Handles Me.Load
        SirPaya.darkLemon(DataGridView1, SirPaya.Mode.NORMAL)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        With frmStudentDetails
            .btnUpdate.Enabled = False
            .ShowDialog()
        End With
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Try
            Dim colname As String = DataGridView1.Columns(e.ColumnIndex).Name
            If colname = "btnEdit" Then
                With frmStudentDetails

                    ._id = DataGridView1.Rows(e.RowIndex).Cells(0).Value.ToString
                    .txtLrn.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value.ToString
                    .txtLname.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value.ToString
                    .txtFname.Text = DataGridView1.Rows(e.RowIndex).Cells(3).Value.ToString
                    .txtMname.Text = DataGridView1.Rows(e.RowIndex).Cells(4).Value.ToString
                    .dtBdate.Value = CDate(DataGridView1.Rows(e.RowIndex).Cells(5).Value.ToString)
                    .cboGender.Text = DataGridView1.Rows(e.RowIndex).Cells(6).Value.ToString
                    .txtAddress.Text = DataGridView1.Rows(e.RowIndex).Cells(7).Value.ToString
                    .txtFather.Text = DataGridView1.Rows(e.RowIndex).Cells(8).Value.ToString
                    .txtFO.Text = DataGridView1.Rows(e.RowIndex).Cells(9).Value.ToString
                    .txtMother.Text = DataGridView1.Rows(e.RowIndex).Cells(10).Value.ToString
                    .txtMO.Text = DataGridView1.Rows(e.RowIndex).Cells(11).Value.ToString
                    .txtPContact.Text = DataGridView1.Rows(e.RowIndex).Cells(12).Value.ToString
                    .txtPAddress.Text = DataGridView1.Rows(e.RowIndex).Cells(13).Value.ToString
                    .txtLrn.Enabled = False
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
                    .btnSave.Enabled = False
                    .ShowDialog()
                End With
            ElseIf colname = "btnDelete" Then
                If MsgBox("Do you want to delete this record?", vbYesNo + vbQuestion) = vbYes Then
                    cn.Open()
                    cm = New MySqlCommand("delete from tblstudent where id like '" & DataGridView1.Rows(e.RowIndex).Cells(0).Value.ToString & "'", cn)
                    cm.ExecuteNonQuery()
                    cn.Close()
                    MsgBox("Record has been successfully deleted!", vbInformation)
                    ActivityLog("Deleting student " & DataGridView1.Rows(e.RowIndex).Cells(2).Value.ToString & ", " & DataGridView1.Rows(e.RowIndex).Cells(3).Value.ToString & " record.")
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

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Me.Dispose()
    End Sub
End Class