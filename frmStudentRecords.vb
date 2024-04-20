Imports MySql.Data.MySqlClient
Public Class frmStudentRecords
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Dispose()
    End Sub

    Private Sub frmStudentRecords_Load(sender As Object, e As EventArgs) Handles Me.Load
        SirPaya.darkLemon(DataGridView1, SirPaya.Mode.NORMAL)
        SirPaya.darkLemon(DataGridView4, SirPaya.Mode.NORMAL)
    End Sub

    Sub LoadStudent()
        Try
            DataGridView4.Rows.Clear()
            cn.Open()
            cm = New MySqlCommand("select * from tblstudent where lname like '%" & txtSearch.Text & "%' or fname like '%" & txtSearch.Text & "%'", cn)
            dr = cm.ExecuteReader
            While dr.Read
                DataGridView4.Rows.Add(dr.Item("lrn").ToString, dr.Item("lname").ToString & ", " & dr.Item("fname").ToString & " " & dr.Item("mname").ToString, dr.Item("level").ToString & " - " & dr.Item("section").ToString, dr.Item("father").ToString & " " & dr.Item("mother").ToString, dr.Item("pcontact").ToString, "SELECT")
            End While
            dr.Close()
            cn.Close()
            DataGridView4.ClearSelection()
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub txtSearch_Click(sender As Object, e As EventArgs) Handles txtSearch.Click

    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        LoadStudent()
    End Sub

    Private Sub DataGridView4_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView4.CellContentClick
        Dim colname As String = DataGridView4.Columns(e.ColumnIndex).Name
        If colname = "btnSelect" Then
            lblLRN.Text = DataGridView4.Rows(e.RowIndex).Cells(0).Value.ToString
            lblName.Text = DataGridView4.Rows(e.RowIndex).Cells(1).Value.ToString
            lblGrade.Text = DataGridView4.Rows(e.RowIndex).Cells(2).Value.ToString
            lblParents.Text = DataGridView4.Rows(e.RowIndex).Cells(3).Value.ToString
            lblContact.Text = DataGridView4.Rows(e.RowIndex).Cells(4).Value.ToString


            cn.Open()
            cm = New MySqlCommand("select pic from tblstudent where lrn like '" & DataGridView4.Rows(e.RowIndex).Cells(0).Value.ToString & "'", cn)
            dr = cm.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                Dim Len1 As Long = dr.GetBytes(0, 0, Nothing, 0, 0)
                Dim Array1(CInt(Len1)) As Byte
                dr.GetBytes(0, 0, Array1, 0, CInt(Len1))

                Dim MemoryStream1 As New System.IO.MemoryStream(Array1)
                Dim Bitmap1 As New System.Drawing.Bitmap(MemoryStream1)
                picImage.BackgroundImage = Bitmap1
            End If
            dr.Close()
            cn.Close()

            LoadViolation()

        End If
    End Sub

    Sub LoadViolation()
        Try
            DataGridView1.Rows.Clear()
            cn.Open()
            cm = New MySqlCommand("select * from vwviolation where lrn like '" & lblLRN.Text & "'", cn) ' and sy like '" & _sy & "'", cn)
            dr = cm.ExecuteReader
            While dr.Read
                DataGridView1.Rows.Add(dr.Item("type").ToString, dr.Item("violation").ToString, dr.Item("offense").ToString, dr.Item("sanction").ToString, dr.Item("remarks").ToString, dr.Item("sy").ToString, dr.Item("section").ToString)
            End While
            dr.Close()
            cn.Close()
            lblTotal.Text = "Total violation (" & DataGridView1.RowCount & ")"
            DataGridView1.ClearSelection()
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        With frmPrintPreview
            ActivityLog("Student record " & lblName.Text & " print preview.")
            .Violation("select * from vwviolation where lrn like '" & lblLRN.Text & "'") ' and sy like '" & _sy & "'")
            .ShowDialog()
        End With
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        With frmPrintPreview
            .Schedule("select * from tblschedule where lrn like '" & lblLRN.Text & "'  and sy like '" & _sy & "'")
            .ShowDialog()
        End With
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs)

        With frmPrintPreview
            .Grade("select * from tblgrade where lrn like '" & lblLRN.Text & "'  and sy like '" & _sy & "'")
            .ShowDialog()
        End With
    End Sub
End Class