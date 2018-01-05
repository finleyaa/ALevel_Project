' The CustomCreation class is a form that is used to create a custom network
' It contains elements to help the user select their own parameters and save them to a text file
Public Class CustomCreation
    Private Sub CustomCreation_Load(sender As Object, e As EventArgs) Handles MyBase.Load ' Called when the form loads
        MyBase.Icon = New Icon("icon.ico")

        ' Update the label values
        INSize_Label.Text = InputNeurons_Trackbar.Value
        HNSize_Label.Text = HiddenNeurons_Trackbar.Value
        ONSize_Label.Text = OutputNeurons_Trackbar.Value

        LRValue_Label.Text = LearningRate_Trackbar.Value / 100
        MomentumValue_Label.Text = Momentum_Trackbar.Value / 100
    End Sub

    Private Sub InputNeurons_Trackbar_Scroll(sender As Object, e As EventArgs) Handles InputNeurons_Trackbar.Scroll ' Called when the input neurons trackbar is scrolled
        INSize_Label.Text = InputNeurons_Trackbar.Value ' Set the label value
    End Sub

    Private Sub HiddenNeurons_Trackbar_Scroll(sender As Object, e As EventArgs) Handles HiddenNeurons_Trackbar.Scroll ' Called when the hidden neurons trackbar is scrolled
        HNSize_Label.Text = HiddenNeurons_Trackbar.Value ' Set the label value
    End Sub

    Private Sub OutputNeurons_Trackbar_Scroll(sender As Object, e As EventArgs) Handles OutputNeurons_Trackbar.Scroll ' Called when the output neurons trackbar is scrolled
        ONSize_Label.Text = OutputNeurons_Trackbar.Value ' Set the label value
    End Sub

    Private Sub LearningRate_Trackbar_Scroll(sender As Object, e As EventArgs) Handles LearningRate_Trackbar.Scroll ' Called when the learning rate trackbar is scrolled
        LRValue_Label.Text = LearningRate_Trackbar.Value / 100 ' Set the label value
    End Sub

    Private Sub Momentum_Trackbar_Scroll(sender As Object, e As EventArgs) Handles Momentum_Trackbar.Scroll ' Called when the momentum trackbar is scrolled
        MomentumValue_Label.Text = Momentum_Trackbar.Value / 100 ' Set the label value
    End Sub

    Private Sub TrainBrowse_Button_Click(sender As Object, e As EventArgs) Handles TrainBrowse_Button.Click ' Called when the training data browse button is clicked
        Dim FileWindow As New OpenFileDialog() ' Create a new open file dialog

        ' Set the file window parameters
        FileWindow.Title = "Select Training Data"
        FileWindow.InitialDirectory = Environment.CurrentDirectory().Split("\")(0) & "\" ' The drive the program is stored on
        FileWindow.Filter = "CSV files (*.csv)|*.csv"
        FileWindow.FilterIndex = 1
        FileWindow.RestoreDirectory = True

        If FileWindow.ShowDialog() = DialogResult.OK Then ' If the user presses OK
            If FileWindow.FileName.Split("\")(0) = Environment.CurrentDirectory().Split("\")(0) Then ' If the file is in the correct drive
                TrainData_TextBox.Text = FileWindow.FileName ' Set the text box value to the file name
            Else ' Otherwise, display an error
                MessageBox.Show("Choose a file in the " & Environment.CurrentDirectory().Split("\")(0) & "\" & " drive!") ' Display an error message
                TrainBrowse_Button.PerformClick() ' Re-open the file dialog
            End If
        End If
    End Sub

    Private Sub TestBrowse_Button_Click(sender As Object, e As EventArgs) Handles TestBrowse_Button.Click ' Called when the testing data browse button is clicked
        ' SAME AS TrainBrowse_Button_Click
        Dim FileWindow As New OpenFileDialog()

        FileWindow.Title = "Select Testing Data"
        FileWindow.InitialDirectory = Environment.CurrentDirectory().Split("\")(0) & "\" ' The drive the program is stored on
        FileWindow.Filter = "CSV files (*.csv)|*.csv"
        FileWindow.FilterIndex = 1
        FileWindow.RestoreDirectory = True

        If FileWindow.ShowDialog() = DialogResult.OK Then
            If FileWindow.FileName.Split("\")(0) = Environment.CurrentDirectory().Split("\")(0) Then ' If the file is in the correct drive
                TestData_TextBox.Text = FileWindow.FileName ' Set the text box value to the file name
            Else ' Otherwise, display an error
                MessageBox.Show("Choose a file in the " & Environment.CurrentDirectory().Split("\")(0) & "\" & " drive!") ' Display an error message
                TestBrowse_Button.PerformClick() ' Re-open the file dialog
            End If
        End If
    End Sub

    Private Sub Finish_Button_Click(sender As Object, e As EventArgs) Handles Finish_Button.Click ' Called when the finish button is clicked
        Dim FileWindow As New SaveFileDialog() ' Create a new save file dialog

        ' Set the file window parameters
        FileWindow.Title = "Save Custom Template"
        FileWindow.InitialDirectory = Environment.CurrentDirectory & "\custom_templates"
        FileWindow.Filter = "Text files (*.txt)|*.txt"
        FileWindow.FilterIndex = 1
        FileWindow.RestoreDirectory = True

        If FileWindow.ShowDialog() = DialogResult.OK Then ' If the user presses OK
            Dim FileWriter As New System.IO.StreamWriter(FileWindow.FileName) ' Open the specified file
            FileWriter.WriteLine(INSize_Label.Text & "," & HNSize_Label.Text & "," & ONSize_Label.Text) ' Write the network layout
            FileWriter.WriteLine(LRValue_Label.Text) ' Write the learning rate
            FileWriter.WriteLine(MomentumValue_Label.Text) ' Write the momentum value
            FileWriter.WriteLine(Convert.ToInt32(Bias_CheckBox.Checked)) ' Write the bias value
            FileWriter.WriteLine(TrainData_TextBox.Text.Remove(0, 2)) ' Write the training data file path
            FileWriter.WriteLine(TestData_TextBox.Text.Remove(0, 2)) ' Write the testing data file path
            FileWriter.Close() ' Close the file
        End If
    End Sub
End Class