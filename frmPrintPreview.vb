Imports MySql.Data.MySqlClient
Imports Microsoft.Reporting.WinForms
Public Class frmPrintPreview
    Private Sub frmPrintPreview_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Sub Violation(ByVal sql As String)
        Dim rptDataSource As ReportDataSource
        ReportViewer1.RefreshReport()

        Try
            With ReportViewer1.LocalReport
                .ReportPath = "Report\rptViolation.rdlc"
                .DataSources.Clear()
            End With

            Dim ds As New DataSet1
            Dim da As New MySqlDataAdapter

            cn.Open()
            da.SelectCommand = New MySqlCommand(sql, cn)
            da.Fill(ds.Tables("dtViolation"))
            Dim pLrn As New ReportParameter("pLrn", frmStudentRecords.lblLRN.Text)
            Dim pName As New ReportParameter("pName", frmStudentRecords.lblName.Text)
            Dim pSy As New ReportParameter("pSy", _sy)


            ReportViewer1.LocalReport.SetParameters(pLrn)
            ReportViewer1.LocalReport.SetParameters(pName)
            ReportViewer1.LocalReport.SetParameters(pSy)


            rptDataSource = New ReportDataSource("DataSet1", ds.Tables("dtViolation"))

            ReportViewer1.LocalReport.DataSources.Add(rptDataSource)

            ReportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout)
            ReportViewer1.ZoomMode = ZoomMode.Percent
            ReportViewer1.ZoomPercent = 100
            cn.Close()
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try

    End Sub


    Sub Vio(ByVal sql As String)
        Dim rptDataSource As ReportDataSource
        ReportViewer1.RefreshReport()

        Try
            With ReportViewer1.LocalReport
                .ReportPath = "Report\rptVio.rdlc"
                .DataSources.Clear()
            End With

            Dim ds As New DataSet1
            Dim da As New MySqlDataAdapter

            cn.Open()
            da.SelectCommand = New MySqlCommand(sql, cn)
            da.Fill(ds.Tables("dtVio"))


            Dim pSy As New ReportParameter("pSy", _sy)
            Dim pLevel As New ReportParameter("pLevel", frmStudentViolation.cboGrade.Text)



            ReportViewer1.LocalReport.SetParameters(pSy)
            ReportViewer1.LocalReport.SetParameters(plevel)



            rptDataSource = New ReportDataSource("DataSet1", ds.Tables("dtVio"))

            ReportViewer1.LocalReport.DataSources.Add(rptDataSource)

            ReportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout)
            ReportViewer1.ZoomMode = ZoomMode.Percent
            ReportViewer1.ZoomPercent = 100
            cn.Close()
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try

    End Sub

    Sub Grade(ByVal sql As String)
        Dim rptDataSource As ReportDataSource
        ReportViewer1.RefreshReport()

        Try
            With ReportViewer1.LocalReport
                .ReportPath = "Report\rptGrade.rdlc"
                .DataSources.Clear()
            End With

            Dim ds As New DataSet1
            Dim da As New MySqlDataAdapter

            cn.Open()
            da.SelectCommand = New MySqlCommand(sql, cn)
            da.Fill(ds.Tables("dtGrade"))
            Dim pLrn As New ReportParameter("pLrn", frmStudentRecords.lblLRN.Text)
            Dim pName As New ReportParameter("pName", frmStudentRecords.lblName.Text)
            Dim pSy As New ReportParameter("pSy", _sy)


            ReportViewer1.LocalReport.SetParameters(pLrn)
            ReportViewer1.LocalReport.SetParameters(pName)
            ReportViewer1.LocalReport.SetParameters(pSy)


            rptDataSource = New ReportDataSource("DataSet1", ds.Tables("dtGrade"))

            ReportViewer1.LocalReport.DataSources.Add(rptDataSource)

            ReportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout)
            ReportViewer1.ZoomMode = ZoomMode.Percent
            ReportViewer1.ZoomPercent = 100
            cn.Close()
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try

    End Sub

    Sub Schedule(ByVal sql As String)
        Dim rptDataSource As ReportDataSource
        ReportViewer1.RefreshReport()

        Try
            With ReportViewer1.LocalReport
                .ReportPath = "Report\rptSchedule.rdlc"
                .DataSources.Clear()
            End With

            Dim ds As New DataSet1
            Dim da As New MySqlDataAdapter

            cn.Open()
            da.SelectCommand = New MySqlCommand(sql, cn)
            da.Fill(ds.Tables("dtSchedule"))
            Dim pLrn As New ReportParameter("pLrn", frmStudentRecords.lblLRN.Text)
            Dim pName As New ReportParameter("pName", frmStudentRecords.lblName.Text)
            Dim pSy As New ReportParameter("pSy", _sy)


            ReportViewer1.LocalReport.SetParameters(pLrn)
            ReportViewer1.LocalReport.SetParameters(pName)
            ReportViewer1.LocalReport.SetParameters(pSy)


            rptDataSource = New ReportDataSource("DataSet1", ds.Tables("dtSchedule"))

            ReportViewer1.LocalReport.DataSources.Add(rptDataSource)

            ReportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout)
            ReportViewer1.ZoomMode = ZoomMode.Percent
            ReportViewer1.ZoomPercent = 100
            cn.Close()
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try

    End Sub
    Private Sub frmPrintPreview_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Me.Dispose()
    End Sub
End Class