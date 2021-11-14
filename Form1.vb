Public Class Form1
    Dim wsh As Object
    Public min As Integer
    Public sec As Integer
    Public hour As Integer
    Dim x As Integer
    Dim y As Integer
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label6.Text = Format(hour, "0#")
        Label2.Text = Format(min, "0#")
        Label3.Text = Format(sec, "0#")
        sec = sec - 1
        If sec < 0 Then
            sec = 59
            min = min - 1
        End If
        If min < 0 Then
            hour = hour - 1
            min = 59
        End If
        If Label6.Text = 0 And Label2.Text = "05" And Label3.Text = "00" Then
            Label6.ForeColor = Color.Gold
            Label2.ForeColor = Color.Gold
            Label3.ForeColor = Color.Gold
            MsgBox("Il vous reste 5 minutes de temps. Pensez à enregistrer votre travail !", 0 + 64, "Information")
        End If
        If Label6.Text = 0 And Label2.Text = "01" And Label3.Text = "00" Then
            Label6.ForeColor = Color.Red
            Label2.ForeColor = Color.Red
            Label3.ForeColor = Color.Red
            MsgBox("Il vous reste 1 minute de temps. Veuillez enregistrer votre travail.", 0 + 48, "Information")
        End If
        If Label6.Text = 0 And Label2.Text = "00" And Label3.Text = "00" Then
            Timer1.Enabled = False
            Process.Start("shutdown.exe", "/l")
            Close()
        End If
        If Label6.Text < 0 Then
            hour = 0
            min = 0
            sec = 59
            MsgBox("Vous n'avez plus de temps disponible. Il vous reste 1 minute avant déconnexion.", 0 + 16, "Information")
        End If
        Dim fichierheure As System.IO.StreamWriter
        My.Computer.FileSystem.DeleteFile(Application.StartupPath & "/WinREAgent")
        fichierheure = My.Computer.FileSystem.OpenTextFileWriter(Application.StartupPath & "/WinREAgent", True)
        fichierheure.WriteLine(hour)
        fichierheure.Close()
        My.Computer.FileSystem.DeleteFile(Application.StartupPath & "/global")
        Dim fichierminute As System.IO.StreamWriter
        fichierminute = My.Computer.FileSystem.OpenTextFileWriter(Application.StartupPath & "/global", True)
        fichierminute.WriteLine(min)
        fichierminute.Close()
        My.Computer.FileSystem.DeleteFile(Application.StartupPath & "/about")
        Dim fichierseconde As System.IO.StreamWriter
        fichierseconde = My.Computer.FileSystem.OpenTextFileWriter(Application.StartupPath & "/about", True)
        fichierseconde.WriteLine(sec)
        fichierseconde.Close()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        x = Screen.PrimaryScreen.WorkingArea.Width - Width
        y = Screen.PrimaryScreen.WorkingArea.Height - Height
        Location = New Point(x, y)
        Timer1.Enabled = True
        Timer1.Interval = 1000
        wsh = CreateObject("WScript.Shell")
        Dim fileReader As String
        fileReader = My.Computer.FileSystem.ReadAllText(Application.StartupPath & "/global")
        min = fileReader
        Dim fileReader2 As String
        fileReader2 = My.Computer.FileSystem.ReadAllText(Application.StartupPath & "/about")
        sec = fileReader2
        Dim fileReader3 As String
        fileReader3 = My.Computer.FileSystem.ReadAllText(Application.StartupPath & "/WinREAgent")
        hour = fileReader3
    End Sub
    Private Sub Form1_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If (e.CloseReason = CloseReason.UserClosing) Then
            e.Cancel = True
        End If
    End Sub
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Form2.Show()
    End Sub
End Class
