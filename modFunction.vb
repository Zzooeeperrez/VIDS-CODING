Imports MySql.Data.MySqlClient
Module modFunction
    Public Function IS_EMPTY(ByRef sText As Object) As Boolean
        On Error Resume Next
        If sText.Text = "" Then
            IS_EMPTY = True
            MsgBox("Warning: Required missing field.", vbExclamation)
            sText.BackColor = Color.LemonChiffon
            sText.SetFocus()
        Else
            IS_EMPTY = False
            sText.BackColor = Color.White
        End If
        Return IS_EMPTY
    End Function
End Module
