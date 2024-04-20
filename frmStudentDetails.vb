Imports MySql.Data.MySqlClient
Imports System.Runtime.InteropServices
Imports Microsoft.Office.Interop

Public Class frmStudentDetails
    Public _id As String
    Private Sub frmStudentDetails_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Me.Dispose()
    End Sub

    Private Sub frmStudentDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Public Function GetCurrentAge(ByVal dob As Date) As Integer
        Dim age As Integer
        age = Today.Year - dob.Year
        If (dob > Today.AddYears(-age)) Then age -= 1
        Return age
    End Function


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Using O As New OpenFileDialog With {.Filter = "(Image Files)|*.jpg;*.png;*.bmp;*.gif;*.ico|Jpg, | *.jpg|Png, | *.png|Bmp, | *.bmp|Gif, | *.gif|Ico | *.ico", .Multiselect = False, .Title = "Select Image"}
            If O.ShowDialog = 1 Then
                picImage.BackgroundImage = Image.FromFile(O.FileName)
                OpenFileDialog1.FileName = O.FileName
            End If
        End Using
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If IS_EMPTY(txtLrn) = True Then Return
            If IS_EMPTY(txtLname) = True Then Return
            If IS_EMPTY(txtFname) = True Then Return
            If IS_EMPTY(txtMname) = True Then Return
            If IS_EMPTY(cboGender) = True Then Return
            If IS_EMPTY(txtAddress) = True Then Return
            If MsgBox("Do you want to save this record?", vbYesNo + vbQuestion) = vbYes Then
                cn.Open()
                cm = New MySqlCommand
                With cm
                    .Connection = cn
                    .CommandType = CommandType.Text
                    .CommandText = "insert into tblstudent (lrn,lname, fname, mname, bdate, gender, address,  father, fo, mother, mo, pcontact, paddress, age, pic) values(@lrn,@lname, @fname, @mname, @bdate, @gender, @address,  @father, @fo, @mother, @mo, @pcontact, @paddress, @age, @pic)"

                    Dim mstream As New System.IO.MemoryStream()
                    picImage.BackgroundImage.Save(mstream, System.Drawing.Imaging.ImageFormat.Jpeg)
                    Dim arrImage() As Byte = mstream.GetBuffer()

                    .Parameters.AddWithValue("@lrn", txtLrn.Text)
                    .Parameters.AddWithValue("@lname", txtLname.Text)
                    .Parameters.AddWithValue("@fname", txtFname.Text)
                    .Parameters.AddWithValue("@mname", txtMname.Text)
                    .Parameters.AddWithValue("@bdate", dtBdate.Value.ToString("yyyy-MM-dd"))
                    .Parameters.AddWithValue("@gender", cboGender.Text)
                    .Parameters.AddWithValue("@address", txtAddress.Text)
                    .Parameters.AddWithValue("@father", txtFather.Text)
                    .Parameters.AddWithValue("@fo", txtFO.Text)
                    .Parameters.AddWithValue("@mother", txtMother.Text)
                    .Parameters.AddWithValue("@mo", txtMO.Text)
                    .Parameters.AddWithValue("@pcontact", txtPContact.Text)
                    .Parameters.AddWithValue("@paddress", txtPAddress.Text)
                    .Parameters.AddWithValue("@age", txtAge.Text)
                    .Parameters.AddWithValue("@pic", arrImage)
                    .ExecuteNonQuery()
                    MsgBox("Record successfully saved", vbInformation)
                End With
                cn.Close()
                ActivityLog("Creating new record for " & txtLname.Text & ", " & txtFname.Text)
                With frmStudent
                    .LoadReacords()
                End With
                Clear()
                Dashboard()
            End If
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try

    End Sub


    Sub Clear()
        picImage.BackgroundImage = Image.FromFile(Application.StartupPath & "\man1.png")
        txtLrn.Clear()
        txtLname.Clear()
        txtFname.Clear()
        txtMname.Clear()
        txtAddress.Clear()
        txtFather.Clear()
        txtFO.Clear()
        txtMother.Clear()
        txtMO.Clear()
        txtPContact.Clear()
        txtPAddress.Clear()
        cboGender.Text = ""
        btnSave.Enabled = True
        btnUpdate.Enabled = False
        txtLrn.Enabled = True
        txtLrn.Focus()
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Try
            If IS_EMPTY(txtLrn) = True Then Return
            If IS_EMPTY(txtLname) = True Then Return
            If IS_EMPTY(txtFname) = True Then Return
            If IS_EMPTY(txtMname) = True Then Return
            If IS_EMPTY(cboGender) = True Then Return
            If IS_EMPTY(txtAddress) = True Then Return
            If MsgBox("Do you want to update this record?", vbYesNo + vbQuestion) = vbYes Then
                cn.Open()
                cm = New MySqlCommand
                With cm
                    .Connection = cn
                    .CommandType = CommandType.Text
                    .CommandText = "update tblstudent set lrn=@lrn,lname=@lname, fname=@fname, mname=@mname, bdate=@bdate, gender=@gender, address=@address, father=@father, fo=@fo, mother=@mother, mo=@mo, pcontact=@pcontact, paddress=@paddress, age=@age,pic=@pic where id = @id"

                    Dim mstream As New System.IO.MemoryStream()
                    picImage.BackgroundImage.Save(mstream, System.Drawing.Imaging.ImageFormat.Jpeg)
                    Dim arrImage() As Byte = mstream.GetBuffer()

                    .Parameters.AddWithValue("@lrn", txtLrn.Text)
                    .Parameters.AddWithValue("@lname", txtLname.Text)
                    .Parameters.AddWithValue("@fname", txtFname.Text)
                    .Parameters.AddWithValue("@mname", txtMname.Text)
                    .Parameters.AddWithValue("@bdate", dtBdate.Value.ToString("yyyy-MM-dd"))
                    .Parameters.AddWithValue("@gender", cboGender.Text)
                    .Parameters.AddWithValue("@address", txtAddress.Text)
                    .Parameters.AddWithValue("@father", txtFather.Text)
                    .Parameters.AddWithValue("@fo", txtFO.Text)
                    .Parameters.AddWithValue("@mother", txtMother.Text)
                    .Parameters.AddWithValue("@mo", txtMO.Text)
                    .Parameters.AddWithValue("@pcontact", txtPContact.Text)
                    .Parameters.AddWithValue("@paddress", txtPAddress.Text)
                    .Parameters.AddWithValue("@age", txtAge.Text)
                    .Parameters.AddWithValue("@pic", arrImage)
                    .Parameters.AddWithValue("@id", _id)
                    .ExecuteNonQuery()
                    MsgBox("Record successfully updated", vbInformation)
                End With
                cn.Close()
                ActivityLog("Updating " & txtLname.Text & ", " & txtFname.Text & " record.")
                With frmStudent
                    .LoadReacords()
                End With
                Clear()
                Me.Dispose()
            End If
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Clear()
    End Sub

    Private Sub cboGender_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboGender.KeyPress
        e.Handled = True
    End Sub

    Private Sub dtBdate_ValueChanged(sender As Object, e As EventArgs) Handles dtBdate.ValueChanged
        txtAge.Text = GetCurrentAge(dtBdate.Value)
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Me.Dispose()
    End Sub
End Class