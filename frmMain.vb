Public Class frmMain
    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles Me.Load
        With frmDashboard
            .TopLevel = False
            Panel5.Controls.Clear()
            Panel5.Controls.Add(frmDashboard)
            Dashboard()
            .BringToFront()
            .Show()
        End With
    End Sub

    Private Sub frmMain_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        Dim intX As Integer = Screen.PrimaryScreen.Bounds.Width
        Dim intY As Integer = Screen.PrimaryScreen.Bounds.Height
        Me.Width = intX
        Me.Height = intY - 30
        Me.Left = 0
        Me.Top = 0
    End Sub


    Private Sub btnStudent_Click(sender As Object, e As EventArgs) Handles btnStudent.Click
        With frmStudent
            .TopLevel = False
            Panel2.Controls.Add(frmStudent)
            .LoadReacords()
            .BringToFront()
            .Show()
        End With
    End Sub

    Private Sub btnStudentViolation_Click(sender As Object, e As EventArgs) Handles btnStudentViolation.Click
        With frmStudentViolation
            .TopLevel = False
            Panel2.Controls.Add(frmStudentViolation)
            .LoadLevel()
            .LoadReacords()
            .BringToFront()
            .Show()
        End With
    End Sub



    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        With frmDashboard
            .TopLevel = False
            Panel5.Controls.Add(frmDashboard)
            Dashboard()
            .BringToFront()
            .Show()
        End With
    End Sub

    Private Sub btnSchedule_Click(sender As Object, e As EventArgs)
        With frmSchedule
            .TopLevel = False
            Panel2.Controls.Add(frmSchedule)
            .Label2.Visible = True
            .cboLevel.Visible = True
            .Label3.Visible = True
            .cboSection.Visible = True
            .Button2.Visible = False
            .Button3.Visible = True
            .DataGridView1.Rows.Clear()
            .lblCount.Text = "0"
            .LoadLevel()
            .BringToFront()
            .Show()
        End With
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        If MsgBox("Do you want to Logout?", vbYesNo + vbQuestion) = vbYes Then
            With frmLogin
                .txtUser.Clear()
                .txtPass.Clear()
                .Show()
            End With
            Me.Dispose()
        End If
    End Sub

    Private Sub btnSched_Click(sender As Object, e As EventArgs)
        With frmSchedule
            .TopLevel = False

            Panel2.Controls.Add(frmSchedule)

            .Label2.Visible = False
            .cboLevel.Visible = False
            .Label3.Visible = False
            .cboSection.Visible = False
            .Button2.Visible = True
            .Button3.Visible = False
            .DataGridView1.Rows.Clear()
            .lblCount.Text = "0"
            .LoadLevel()
            .BringToFront()
            .Show()
        End With
    End Sub

    Private Sub btnManageViolation_Click(sender As Object, e As EventArgs) Handles btnManageViolation.Click
        With frmViolation
            .TopLevel = False
            Panel2.Controls.Add(frmViolation)
            .LoadReacords()
            .BringToFront()
            .Show()
        End With
    End Sub

    Private Sub btnSection_Click(sender As Object, e As EventArgs) Handles btnSection.Click
        With frmSection
            .TopLevel = False
            Panel2.Controls.Add(frmSection)
            .LoadReacords()
            .BringToFront()
            .Show()
        End With
    End Sub

    Private Sub btnSY_Click(sender As Object, e As EventArgs) Handles btnSY.Click
        With frmSY
            .TopLevel = False
            Panel2.Controls.Add(frmSY)
            .LoadReacords()
            .BringToFront()
            .Show()
        End With
    End Sub

    Private Sub btnGrade_Click(sender As Object, e As EventArgs)
        With frmGrade
            .TopLevel = False
            Panel2.Controls.Add(frmGrade)
            .Label2.Visible = True
            .cboLevel.Visible = True
            .Label3.Visible = True
            .cboSection.Visible = True
            .Button2.Visible = False
            .Button3.Visible = True
            .Label4.Visible = True
            .cboSubject.Visible = True
            .DataGridView1.Rows.Clear()
            .lblCount.Text = "0"
            .LoadLevel()
            .BringToFront()
            .Show()
        End With
    End Sub

    Private Sub btnGradeImport_Click(sender As Object, e As EventArgs)
        With frmGrade
            .TopLevel = False
            Panel2.Controls.Add(frmGrade)
            .Label2.Visible = False
            .cboLevel.Visible = False
            .Label3.Visible = False
            .cboSection.Visible = False
            .Button2.Visible = True
            .Button3.Visible = False
            .Label4.Visible = False
            .cboSubject.Visible = False
            .DataGridView1.Rows.Clear()
            .lblCount.Text = "0"
            .LoadLevel()
            .BringToFront()
            .Show()
        End With
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        With frmStudentRecords
            .TopLevel = False
            Panel2.Controls.Add(frmStudentRecords)
            .LoadStudent()
            .BringToFront()
            .Show()
        End With
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        With frmUserAccount
            .ShowDialog()
        End With
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        With frmChangePassword
            .ShowDialog()
        End With
    End Sub

    Private Sub btnActivity_Click(sender As Object, e As EventArgs) Handles btnActivity.Click
        With frmActivityLog
            .TopLevel = False
            Panel2.Controls.Add(frmActivityLog)
            .LoadReacords()
            .BringToFront()
            .Show()
        End With
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        With frmBackup
            .ShowDialog()
        End With
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        With frmEnrollStudent
            .TopLevel = False
            Panel2.Controls.Add(frmEnrollStudent)
            .LoadLevel()
            .LoadStudent()
            .lblSY.Text = _sy
            .CountEnroll()
            .LoadEnroll()
            .BringToFront()
            .Show()
        End With
    End Sub
End Class
