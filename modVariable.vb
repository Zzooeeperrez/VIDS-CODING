Imports MySql.Data.MySqlClient
Module modVariable
    Public cn As New MySqlConnection
    Public cm As New MySqlCommand
    Public dr As MySqlDataReader

    Public _user As String
    Public _pass As String
    Public _name As String
    Public _role As String

    Public _sy As String
End Module
