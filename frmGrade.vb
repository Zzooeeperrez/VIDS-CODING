Imports MySql.Data.MySqlClient
Imports System.Runtime.InteropServices
Imports Microsoft.Office.Interop
Imports System.IO
Public Class frmGrade

    Dim filePath As String = ""
    Dim xlApp As Excel.Application
    Dim xlWorkbook As Excel.Workbook
    Dim xlWorkSheet As Excel.Worksheet
    Dim xlRange As Excel.Range
    Dim xlRow As Integer
    Dim strDestination As String
    Dim thread As System.Threading.Thread
    Dim icount As Integer

    Dim sy_, level_, section_, code_, desc_, day_, time_, room_, teacher_, adviser_ As String

    Private Sub cboSubject_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSubject.SelectedIndexChanged
        Try
            DataGridView1.Rows.Clear()
            cn.Open()
            cm = New MySqlCommand("select sc.*, concat(s.lname, ', ', s.fname , ' ' , s.mname) as studentname from tblschedule as sc inner join tblstudent as s  on s.lrn = sc.lrn where sc.level = @level and sc.section =@section and subject =@subject", cn)
            With cm
                .Parameters.AddWithValue("@level", cboLevel.Text)
                .Parameters.AddWithValue("@section", cboSection.Text)
                .Parameters.AddWithValue("@subject", cboSubject.Text)
            End With
            dr = cm.ExecuteReader
            While dr.Read
                sy_ = dr.Item("sy").ToString
                level_ = dr.Item("level").ToString
                section_ = dr.Item("section").ToString
                desc_ = dr.Item("description").ToString
                day_ = dr.Item("sday").ToString
                time_ = dr.Item("stime").ToString
                room_ = dr.Item("room").ToString
                teacher_ = dr.Item("teacher").ToString
                adviser_ = dr.Item("adviser").ToString
                DataGridView1.Rows.Add("", dr.Item("lrn").ToString, dr.Item("studentname").ToString, "", "", "", "", "", "", "")
            End While
            dr.Close()
            cn.Close()
            lblCount.Text = DataGridView1.RowCount
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            If IS_EMPTY(cboLevel) = True Then Return
            If IS_EMPTY(cboSection) = True Then Return
            If DataGridView1.RowCount = 0 Then Return
            If MsgBox("Do you want to export grade template?", vbYesNo + vbQuestion) = vbYes Then

                ActivityLog("Export grade template level " & cboLevel.Text & " section " & cboSection.Text & " subject " & cboSubject.Text)
                Dim path As String = CurDir() & "\Grade Template\Grade Template Files"
                If Not Directory.Exists(path) Then
                    Directory.CreateDirectory(path)
                End If

                Dim counter = 1

                Dim newFileName = cboSection.Text & "-" & cboSubject.Text & ".xlsx"

                While File.Exists(path & "\" & newFileName)
                    newFileName = cboSection.Text & "-" & cboSubject.Text & " (" & counter.ToString & ")" & ".xlsx"
                    counter += 1
                End While

                SaveFileDialog1.InitialDirectory = path
                SaveFileDialog1.Filter = "Excel files (*.xlsx)|*.xlsx"
                SaveFileDialog1.FileName = newFileName
                If SaveFileDialog1.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
                    filePath = SaveFileDialog1.FileName
                    exportExcel()
                End If
            End If
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
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

    Private Sub cboSection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSection.SelectedIndexChanged
        Try
            cboSubject.Items.Clear()
            cboSubject.Text = ""
            cn.Open()
            cm = New MySqlCommand("select distinct subject from tblschedule where level like '" & cboLevel.Text & "' and section like '" & cboSection.Text & "' and sy like '" & _sy & "'", cn)
            dr = cm.ExecuteReader
            While dr.Read
                cboSubject.Items.Add(dr.Item("subject").ToString)
            End While
            dr.Close()
            cn.Close()
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Dispose()
    End Sub

    Private Sub cboSubject_KeyDown(sender As Object, e As KeyEventArgs) Handles cboSubject.KeyDown, cboSection.KeyDown, cboLevel.KeyDown
        e.Handled = True
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

    Public Sub exportExcel()

        Dim cnt As Integer = 0

        Dim xlApp As Microsoft.Office.Interop.Excel.Application
        Dim xlWorkBook As Microsoft.Office.Interop.Excel.Workbook
        Dim xlWorkSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim misValue As Object = System.Reflection.Missing.Value


        xlApp = New Microsoft.Office.Interop.Excel.Application
        xlWorkBook = xlApp.Workbooks.Add(misValue)
        xlWorkSheet = xlWorkBook.Sheets("sheet1")

        GetSY()
        xlWorkSheet.Cells(2, 2).Value = "SY :"
        xlWorkSheet.Cells(3, 2).Value = "LEVEL | SECTION :"
        xlWorkSheet.Cells(4, 2).Value = "SUBJECT CODE :"
        xlWorkSheet.Cells(5, 2).Value = "DESCRIPTION :"
        xlWorkSheet.Cells(6, 2).Value = "SCHEDULE :"
        xlWorkSheet.Cells(7, 2).Value = "TEACHER :"
        xlWorkSheet.Cells(8, 2).Value = "ADVISER :"
        xlWorkSheet.Cells(9, 2).Value = "LIST OF STUDENTS"

        xlWorkSheet.Cells(2, 3).Value = _sy
        xlWorkSheet.Cells(3, 3).Value = level_ & " - " & section_
        xlWorkSheet.Cells(4, 3).Value = cboSubject.Text
        xlWorkSheet.Cells(5, 3).Value = desc_
        xlWorkSheet.Cells(6, 3).Value = time_ & " " & day_ & " ROOM : " & room_
        xlWorkSheet.Cells(7, 3).Value = teacher_
        xlWorkSheet.Cells(8, 3).Value = adviser_
        ' xlWorkSheet.Cells(5, 2).Value = cboTeacher.Text

        xlWorkSheet.Range("C2:g2").Merge()
        xlWorkSheet.Range("C3:g3").Merge()
        xlWorkSheet.Range("C4:g4").Merge()
        xlWorkSheet.Range("C5:g5").Merge()
        xlWorkSheet.Range("C6:g6").Merge()
        xlWorkSheet.Range("C7:g7").Merge()
        xlWorkSheet.Range("C8:g8").Merge()
        xlWorkSheet.Range("B9:g9").Merge()


        'Export Column Header
        Dim columnsCount As Integer = DataGridView1.Columns.Count
        cnt = 10


        For index As Integer = 1 To columnsCount - 2
            xlWorkSheet.Cells(cnt, index + 1).Value = DataGridView1.Columns(index).HeaderText
            xlWorkSheet.Cells(cnt, index + 1).BorderAround(Weight:=Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin)
            xlWorkSheet.Cells(cnt, index + 1).Font.Size = 10
            xlWorkSheet.Cells(cnt, index + 1).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter

        Next
        cnt = 11

        'Export Each Row Start
        Dim row = 0
        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            For index As Integer = 1 To columnsCount - 2

                With xlWorkSheet.Cells(i + cnt, index + 1)
                    .Value = DataGridView1.Item(index, i).Value.ToString

                    If index = 1 Then
                        .NumberFormat = "0"
                    End If

                    .HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft

                    .BorderAround(Weight:=Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin)
                End With
            Next
            row += 1
        Next

        cnt += row - 1

        xlWorkSheet.Range("B10:g10").BorderAround(Weight:=Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin)
        xlWorkSheet.Range("B10:g" + (cnt).ToString).Cells.BorderAround(Weight:=Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium)

        xlWorkSheet.Range("B10:g10").Interior.Color = RGB(255, 230, 153)
        xlWorkSheet.Range("B10:g10").Font.Bold = True
        xlWorkSheet.Range("B10:g10").Font.Size = 10

        'SA TAAS NA BORDER
        Dim ccc As Integer = 11

        xlWorkSheet.Range("B2:g" + (ccc).ToString).Cells.BorderAround(Weight:=Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium)


        xlWorkSheet.Range(xlWorkSheet.Cells(cnt + 2, 2), xlWorkSheet.Cells(cnt + 2, 3)).Merge()
        xlWorkSheet.Cells(cnt + 2, 2).Font.Bold = True
        xlWorkSheet.Cells(cnt + 2, 2).Font.Size = 12

        xlWorkSheet.Cells(cnt + 3, 2).Font.Bold = True
        xlWorkSheet.Range(xlWorkSheet.Cells(cnt + 3, 2), xlWorkSheet.Cells(cnt + 3, 3)).Merge()

        xlWorkSheet.Range(xlWorkSheet.Cells(cnt + 4, 2), xlWorkSheet.Cells(cnt + 4, 3)).Merge()
        xlWorkSheet.Cells(cnt + 4, 2).Font.Bold = True
        xlWorkSheet.Cells(cnt + 4, 2).Font.Size = 10


        xlWorkSheet.Range("A1:i" + cnt.ToString).Font.Size = 10

        xlWorkSheet.Range("B1:i1").EntireColumn.AutoFit()

        xlWorkSheet.Range("a2:z2").EntireColumn.Font.Size = 8

        xlWorkSheet.Range("B9").Font.Bold = True
        xlWorkSheet.Range("B9").Font.Size = 12
        'xlWorkSheet.Range("c2").Font.Bold = True
        'xlWorkSheet.Range("c2").Font.Size = 16
        xlWorkSheet.Cells(cnt + 3, 2).Font.Size = 12


        'SETTINGS

        With xlWorkSheet
            .Columns(1).Delete()
            Try
                With .PageSetup
                    .Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlLandscape
                    .LeftMargin = 0.2
                    .TopMargin = 0.2
                    .RightMargin = 0.2
                    .BottomMargin = 0.2
                    .HeaderMargin = 0
                    .FooterMargin = 0
                    .Zoom = False
                    .FitToPagesTall = 1
                    .FitToPagesWide = 1
                    .CenterHorizontally = True
                    .CenterVertically = False
                End With
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        End With



        Try
            xlWorkSheet.SaveAs(filePath)
        Catch ex As Exception
            MsgBox("Excel file is in use", vbCritical)
            Return
        End Try

        If MessageBox.Show("Do you want to open the exported file?", "Exported File", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
            xlWorkBook.Close()
            xlApp.Quit()
        Else
            xlApp.Visible = True
            xlWorkSheet = xlWorkBook.Sheets("Sheet1")
            'xlWorkSheet.PrintOutEx(From:=1, [To]:=1, Copies:=1, Preview:=True)
        End If


    End Sub

    Private Sub frmGrade_Load(sender As Object, e As EventArgs) Handles Me.Load
        SirPaya.darkLemon(DataGridView1, SirPaya.Mode.NORMAL)
        CheckForIllegalCrossThreadCalls = False
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            DataGridView1.Rows.Clear()
            lblCount.Text = "0"

            With OpenFileDialog1
                .Filter = "Excel Office| *.xls;*.xlsx"
                .FileName = String.Empty
                .ShowDialog()
                strDestination = .FileName
            End With
            If strDestination <> String.Empty Then
                xlApp = New Excel.Application
                xlWorkbook = xlApp.Workbooks.Open(strDestination)
                xlWorkSheet = xlWorkbook.Worksheets("sheet1")
                xlRange = xlWorkSheet.UsedRange
                icount = xlRange.Rows.Count
                thread = New System.Threading.Thread(AddressOf LoadData)
                thread.Start()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub LoadData()
        Try

            Dim found As Boolean = False
            Dim rowCount As Integer = 0

            For xlRow = 11 To xlRange.Rows.Count

                sy_ = xlRange.Cells(2, 2).Text

                code_ = xlRange.Cells(4, 2).Text

                If sy_ = "" Then
                    MsgBox("Please input school year!", vbExclamation)
                    Exit For
                    Return
                End If

                If code_ = "" Then
                    MsgBox("Please input subject code!", vbExclamation)
                    Exit For
                    Return

                End If

                If xlRange.Cells(xlRow, 1).Text <> String.Empty Then
                    rowCount += 1
                    Dim ave As Double = (CDbl(xlRange.Cells(xlRow, 3).Text) + CDbl(xlRange.Cells(xlRow, 4).Text) + CDbl(xlRange.Cells(xlRow, 5).Text) + CDbl(xlRange.Cells(xlRow, 6).Text)) / 4
                    DataGridView1.Rows.Add(sy_, xlRange.Cells(xlRow, 1).Text, xlRange.Cells(xlRow, 2).Text, xlRange.Cells(xlRow, 3).Text, xlRange.Cells(xlRow, 4).Text, xlRange.Cells(xlRow, 5).Text, xlRange.Cells(xlRow, 6).Text, ave.ToString)
                    lblCount.Text = rowCount
                    found = True
                End If
                Me.Refresh()
            Next
            xlWorkbook.Close()
            xlApp.Quit()
            DataGridView1.Refresh()
            If MsgBox("Do you want to save this record?", vbYesNo + vbQuestion) = vbYes Then

                For i = 0 To DataGridView1.RowCount - 1

                    Dim duplicate As Boolean = False

                    cn.Open()
                    cm = New MySqlCommand("select * from tblgrade where lrn = @lrn and subject=@subject and sy = @sy", cn)
                    cm.Parameters.AddWithValue("@lrn", DataGridView1.Rows(i).Cells(1).Value.ToString)
                    cm.Parameters.AddWithValue("@subject", code_)
                    cm.Parameters.AddWithValue("@sy", sy_)
                    dr = cm.ExecuteReader
                    dr.Read()
                    If dr.HasRows Then
                        duplicate = True
                    Else
                        duplicate = False
                    End If
                    dr.Close()
                    cn.Close()

                    If duplicate = True Then
                        cn.Open()
                        cm = New MySqlCommand("update tblgrade set q1 = @q1, q2 =@q2, q3 = @q3, q4 =@q4, ave=@ave where lrn = @lrn and subject=@subject and sy=@sy", cn)
                        With cm
                            .Parameters.AddWithValue("@q1", CDbl(DataGridView1.Rows(i).Cells(3).Value.ToString))
                            .Parameters.AddWithValue("@q2", CDbl(DataGridView1.Rows(i).Cells(4).Value.ToString))
                            .Parameters.AddWithValue("@q3", CDbl(DataGridView1.Rows(i).Cells(5).Value.ToString))
                            .Parameters.AddWithValue("@q4", CDbl(DataGridView1.Rows(i).Cells(6).Value.ToString))
                            .Parameters.AddWithValue("@ave", CDbl(DataGridView1.Rows(i).Cells(7).Value.ToString))
                            .Parameters.AddWithValue("@lrn", DataGridView1.Rows(i).Cells(1).Value.ToString)
                            .Parameters.AddWithValue("@subject", code_)
                            .Parameters.AddWithValue("@sy", sy_)
                            .ExecuteNonQuery()
                        End With
                        cn.Close()
                    Else
                        cn.Open()
                        cm = New MySqlCommand("insert into tblgrade(lrn, subject, q1, q2, q3, q4, ave, sy)values(@lrn, @subject, @q1, @q2, @q3, @q4, @ave, @sy)", cn)
                        With cm
                            .Parameters.AddWithValue("@lrn", DataGridView1.Rows(i).Cells(1).Value.ToString)
                            .Parameters.AddWithValue("@subject", code_)
                            .Parameters.AddWithValue("@q1", CDbl(DataGridView1.Rows(i).Cells(3).Value.ToString))
                            .Parameters.AddWithValue("@q2", CDbl(DataGridView1.Rows(i).Cells(4).Value.ToString))
                            .Parameters.AddWithValue("@q3", CDbl(DataGridView1.Rows(i).Cells(5).Value.ToString))
                            .Parameters.AddWithValue("@q4", CDbl(DataGridView1.Rows(i).Cells(6).Value.ToString))
                            .Parameters.AddWithValue("@ave", CDbl(DataGridView1.Rows(i).Cells(7).Value.ToString))
                            .Parameters.AddWithValue("@sy", sy_)
                            .ExecuteNonQuery()
                        End With
                        cn.Close()
                    End If

                Next

                MsgBox("Record has been successfully saved!", vbInformation)
            End If
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub
End Class