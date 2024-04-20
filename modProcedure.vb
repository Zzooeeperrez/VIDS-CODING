Imports MySql.Data.MySqlClient
Module modProcedure

    Public Sub Connection()
        Try
            cn = New MySqlConnection
            cn.ConnectionString = "server=localhost;user id=root;password=;database=studentviolation"
            cn.Open()
            cn.Close()

            GetSY()

        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Sub GetSY()
        cn.Open()
        cm = New MySqlCommand("select * from tblSY where status like 'OPEN'", cn)
        dr = cm.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            _sy = dr.Item(0).ToString
        Else
            _sy = ""
        End If
        dr.Close()
        cn.Close()
    End Sub

    Sub ActivityLog(ByVal summary As String)
        Try
            cn.Open()
            cm = New MySqlCommand("insert into tbllog (suser, summary, sdate, stime)values(@suser, @summary, @sdate, @stime)", cn)
            cm.Parameters.AddWithValue("@suser", _user)
            cm.Parameters.AddWithValue("@summary", summary)
            cm.Parameters.AddWithValue("@sdate", Now.ToString("yyyy-MM-dd"))
            cm.Parameters.AddWithValue("@stime", Now.ToShortTimeString)
            cm.ExecuteNonQuery()
            cn.Close()
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Sub Dashboard()
        Try
            GetSY()
            frmDashboard.lblSY.Text = _sy

            cn.Open()
            cm = New MySqlCommand("select count(*) From tblenroll where sy like '" & _sy & "'", cn)
            frmDashboard.lblStudent.Text = cm.ExecuteScalar
            cn.Close()

            cn.Open()
            cm = New MySqlCommand("select count(*) From tblStudentviolation where sy like '" & _sy & "' ", cn)
            frmDashboard.lblStudentViolation.Text = cm.ExecuteScalar
            cn.Close()

            cn.Open()
            cm = New MySqlCommand("select count(*) From tblviolation", cn)
            frmDashboard.lblViolation.Text = cm.ExecuteScalar
            cn.Close()


        Catch ex As Exception
            cn.Close()
        End Try
    End Sub
    Public Class SirPaya

        Public Enum Mode As Integer
            SMALL
            NORMAL
            LARGE
        End Enum
        Public Shared Sub darkLemon(ByVal datagrid As DataGridView, Optional ByVal mode As SirPaya.Mode = Mode.SMALL)

            Dim size = mode
            Select Case size
                Case SirPaya.Mode.SMALL
                    size = 8.25!
                Case SirPaya.Mode.NORMAL
                    size = 8.75!
                Case SirPaya.Mode.LARGE
                    size = 9.0!
            End Select

            datagrid.AllowUserToAddRows = False
            datagrid.AllowUserToDeleteRows = False
            datagrid.AllowUserToResizeColumns = False
            datagrid.AllowUserToResizeRows = False
            datagrid.BackgroundColor = Color.White

            datagrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
            With datagrid.ColumnHeadersDefaultCellStyle
                .Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
                .BackColor = Color.FromArgb(69, 170, 242)
                .Font = New System.Drawing.Font("calibri", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

                .ForeColor = Color.White
                .SelectionBackColor = System.Drawing.SystemColors.Highlight
                .SelectionForeColor = System.Drawing.SystemColors.HighlightText
                .WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            End With
            datagrid.ColumnHeadersHeight = 30
            datagrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
            datagrid.EnableHeadersVisualStyles = False
            datagrid.RowHeadersVisible = False
            With datagrid.RowsDefaultCellStyle
                .BackColor = Color.FromArgb(193, 226, 250)
                .Font = New System.Drawing.Font("CALIBRI", size, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                .ForeColor = System.Drawing.Color.FromArgb(37, 37, 38)
                .SelectionBackColor = Color.White
                .SelectionForeColor = System.Drawing.Color.Black

            End With
            datagrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            datagrid.RowTemplate.Height = 28
            datagrid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(234, 245, 253)
            datagrid.AlternatingRowsDefaultCellStyle.SelectionBackColor = Nothing
            datagrid.GridColor = Color.FromArgb(193, 226, 250)
        End Sub

    End Class
End Module
