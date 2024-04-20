Imports MySql.Data.MySqlClient
Public Class frmEnrollStudent
    Private Sub frmEnrollStudent_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SirPaya.darkLemon(DataGridView4, SirPaya.Mode.NORMAL)
        SirPaya.darkLemon(DataGridView1, SirPaya.Mode.NORMAL)
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


    Sub LoadLevel()
        Try
            cboLevel.Items.Clear()
            cn.Open()
            cm = New MySqlCommand("select * from tbllevel", cn)
            dr = cm.ExecuteReader
            While dr.Read
                cboLevel.Items.Add(dr.Item(1).ToString)
            End While
            dr.Close()
            cn.Close()
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub
    Sub LoadEnroll()
        Try
            DataGridView1.Rows.Clear()
            cn.Open()
            cm = New MySqlCommand("select * from vwenroll where fullname like '%" & txtSearch.Text & "%' and sy like '" & lblSY.Text & "' order by grade, section, fullname", cn)
            dr = cm.ExecuteReader
            While dr.Read
                DataGridView1.Rows.Add(dr.Item("id").ToString, dr.Item("lrn").ToString, dr.Item("fullname").ToString, dr.Item("grade").ToString, dr.Item("section").ToString, dr.Item("sy").ToString, "DELETE")
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

    Private Sub DataGridView4_CellContentClick_1(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView4.CellContentClick
        Dim colname As String = DataGridView4.Columns(e.ColumnIndex).Name
        If colname = "btnSelect" Then
            lblLRN.Text = DataGridView4.Rows(e.RowIndex).Cells(0).Value.ToString
            lblName.Text = DataGridView4.Rows(e.RowIndex).Cells(1).Value.ToString

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

        End If
    End Sub

    Private Sub cboLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboLevel.SelectedIndexChanged
        Try
            cboSection.Items.Clear()
            cboSection.Text = ""
            cn.Open()
            cm = New MySqlCommand("select * from tblsection where level like '" & cboLevel.Text & "'", cn)
            dr = cm.ExecuteReader
            While dr.Read
                cboSection.Items.Add(dr.Item("section").ToString)
            End While
            dr.Close()
            cn.Close()
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub cboLevel_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboSection.KeyPress, cboLevel.KeyPress
        e.Handled = True
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Me.Dispose()
    End Sub

    Sub CountEnroll()
        Try
            cn.Open()
            cm = New MySqlCommand("select count(*) from tblenroll where sy =@sy", cn)
            cm.Parameters.AddWithValue("@sy", lblSY.Text)
            lblCount.Text = CLng(cm.ExecuteScalar) + 1
            cn.Close()
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If IS_EMPTY(lblSY) = True Then Return
            If IS_EMPTY(lblLRN) = True Then Return
            If IS_EMPTY(cboLevel) = True Then Return
            If IS_EMPTY(cboSection) = True Then Return

            cn.Open()
            cm = New MySqlCommand("select count(*) from tblenroll where lrn =@lrn and sy =@sy", cn)
            cm.Parameters.AddWithValue("@lrn", lblLRN.Text)
            cm.Parameters.AddWithValue("@sy", lblSY.Text)
            Dim count As Integer = CLng(cm.ExecuteScalar)
            cn.Close()

            If count > 0 Then
                MsgBox("Unable to save. Duplicate entry!", vbExclamation)
                Return
            End If
            If MsgBox("Do you want to enroll this student?", vbYesNo + vbQuestion) = vbYes Then


                cn.Open()
                cm = New MySqlCommand("insert into tblenroll (lrn, sy, grade, section) values(@lrn, @sy, @grade, @section)", cn)
                cm.Parameters.AddWithValue("@lrn", lblLRN.Text)
                cm.Parameters.AddWithValue("@sy", lblSY.Text)
                cm.Parameters.AddWithValue("@grade", cboLevel.Text)
                cm.Parameters.AddWithValue("@section", cboSection.Text)
                cm.ExecuteNonQuery()
                cn.Close()
                MsgBox("Student " & lblName.Text & " successfully enrolled!", vbInformation)
                ActivityLog("Student " & lblName.Text & " successfully enrolled")
                LoadEnroll()
                Clear()
            End If
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Sub Clear()
        CountEnroll()
        lblLRN.Text = ""
        lblName.Text = ""
        cboLevel.Text = ""
        cboSection.Text = ""
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Try
            Dim COLNAME As String = DataGridView1.Columns(e.ColumnIndex).Name
            If COLNAME = "btnDelete" Then
                If MsgBox("Dou you want to delete this record?", vbYesNo + vbQuestion) = vbYes Then
                    cn.Open()
                    cm = New MySqlCommand("delete from tblenroll where id like '" & DataGridView1.Rows(e.RowIndex).Cells(0).Value.ToString & "'", cn)
                    cm.ExecuteNonQuery()
                    cn.Close()

                    MsgBox("Record successfully deleted!", vbInformation)

                    ActivityLog("Delete student enroll record " & DataGridView1.Rows(e.RowIndex).Cells(2).Value.ToString)

                    LoadEnroll()
                End If
            End If
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub
End Class