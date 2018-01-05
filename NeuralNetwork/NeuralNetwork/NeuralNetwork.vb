Imports System.IO
' The main form class
' Handles the setup of data and the display of results and errors etc.
Public Class NeuralNetwork

    Public NeuralNet As Net ' The Net object
    Private TrainData As New List(Of List(Of Double)) ' The set of training data
    Private TestData As New List(Of List(Of Double)) ' The set of test data
    Private Templates As New List(Of String)({"Sine Graph", "X and O classification", "XOR"}) ' The names of the network templates
    Private CustomTemplates As New List(Of String)
    Private DrawingResults As Boolean = False ' If True, then the results are being drawn on the picture box. If False, then either nothing or the errors are being drawn.
    Private Errors As New List(Of Double) ' List of training errors
    Private Results As New List(Of Double) ' List of testing results

    Public Sub NeuralNetwork_Load() Handles MyBase.Load ' Executed when the form loads, sets up form elements

        MyBase.Icon = New Icon("icon.ico") ' Set the form's icon

        Dim DebugFiles As String() = Directory.GetFiles(Environment.CurrentDirectory & "\custom_templates") ' Find all the files in the custom templates directory

        For Each Doc In DebugFiles ' For each file in the custom templates directory
            Dim DocSplit As String() = Doc.Split("\") ' Get the file name
            Dim FileName As String = DocSplit(DocSplit.Length - 1)
            CustomTemplates.Add(DocSplit(DocSplit.Length - 1)) ' Add the file name to the custom templates list
        Next

        For Each Template In Templates
            Templates_ComboBox.Items.Add(Template) ' Add all the template names to the combo box
        Next
        For Each Template In CustomTemplates
            Templates_ComboBox.Items.Add(Template) ' Add all the custom templates to the combo box
        Next

        ' Run the trackbar scroll functions to set the labels to the correct numbers
        Epochs_TrackBar_Scroll(Me, New EventArgs)
        LearningRate_TrackBar_Scroll(Me, New EventArgs)
        Momentum_TrackBar_Scroll(Me, New EventArgs)
        HiddenNeurons_TrackBar_Scroll(Me, New EventArgs)

        CustomResults_ListBox.Hide() ' Hide the custom results box

    End Sub

    ' Called when the combo box option is changed
    Private Sub Templates_ComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Templates_ComboBox.SelectedIndexChanged

        Errors.Clear() ' Clear the errors
        Results.Clear() ' Clear the results
        Result_PictureBox.Refresh() ' Reset the picture box
        CustomResults_ListBox.Hide()

        If Templates_ComboBox.SelectedItem = "Sine Graph" Then
            SetupNet_SineGraph() ' Start the sine graph net setup
        ElseIf Templates_ComboBox.SelectedItem = "XOR" Then
            SetupNet_XOR() ' Start the XOR net setup
        ElseIf Templates_ComboBox.SelectedItem = "X and O classification" Then
            SetupNet_XandO() ' Start the X and O classification net setup
        Else
            SetupNet_Custom("custom_templates\" & Templates_ComboBox.SelectedItem) ' Start the custom network setup
        End If

        ' Enable form elements and update their values to the correct setting
        Train_Button.Enabled = True
        Test_Button.Enabled = True
        ChangeView_Button.Enabled = True

        Epochs_TrackBar.Enabled = True
        Epochs_TrackBar_Scroll(Me, New EventArgs)

        LearningRate_TrackBar.Enabled = True
        LearningRate_TrackBar.Value = NeuralNet.LearningRate * 100
        LearningRate_TrackBar_Scroll(Me, New EventArgs)

        Bias_CheckBox.Enabled = True
        Bias_CheckBox.Checked = Convert.ToBoolean(NeuralNet.BiasValue)

        Momentum_TrackBar.Enabled = True
        Momentum_TrackBar.Value = NeuralNet.Momentum * 100
        Momentum_TrackBar_Scroll(Me, New EventArgs)

        HiddenNeurons_TrackBar.Enabled = True
        HiddenNeurons_TrackBar.Value = NeuralNet.HiddenLayerSize
        HiddenNeurons_TrackBar_Scroll(Me, New EventArgs)

        Reset_Button.Enabled = True

        SaveWeights_Button.Enabled = True
        LoadWeights_Button.Enabled = True

    End Sub

    ' Called when the change view button is pressed
    Private Sub ChangeView_Button_Click(sender As Object, e As EventArgs) Handles ChangeView_Button.Click
        If DrawingResults Then ' If the results are being drawn, then change to drawing the errors
            If Errors.Count > 0 Then ' Only draw errors if there are any
                CustomResults_ListBox.Hide() ' Hide the custom results box
                Draw_Errors(Errors) ' Draw the specified errors on the picture box
            End If
        Else ' If the results are not being drawn, then draw the results
            If Results.Count > 0 Then ' Only draw results if there are any
                CustomResults_ListBox.Hide()
                If Templates_ComboBox.SelectedItem = "Sine Graph" Then
                    Draw_SineGraph(Results) ' Draw the results in the sine graph format
                ElseIf Templates_ComboBox.SelectedItem = "XOR" Then
                    Draw_XOR(Results) ' Draw the results in the XOR format
                ElseIf Templates_ComboBox.SelectedItem = "X and O classification" Then
                    Draw_XandO(Results) ' Draw the results in the X and O classification format
                Else
                    Draw_Custom(Results) ' Draw the results in the custom format
                End If
            End If
        End If
    End Sub

    ' Handles the drawing of the errors on the picture box
    Private Sub Draw_Errors(ErrorList As List(Of Double), Optional eGraphics As Graphics = Nothing)

        DrawingResults = False ' Indicates that the errors are being drawn, not the results
        Dim g As Graphics ' The graphics object that will be used
        If IsNothing(eGraphics) Then ' If this subroutine is being run from the picture box's paint event, then a graphics object will be passed as a parameter
            g = Result_PictureBox.CreateGraphics() ' If there is no graphics object parameter then create a new graphics object for the picture box
        Else
            g = eGraphics ' Otherwise, use the graphics object that was passed through
        End If
        g.Clear(DefaultBackColor) ' Clear the picture box
        Dim BlackBrush As Brush = Brushes.Black
        Dim BlackPen As New Drawing.Pen(Color.Black)
        Dim CenteredFormat As New StringFormat()
        CenteredFormat.Alignment = StringAlignment.Center
        g.DrawLine(BlackPen, 5, 5, 5, Result_PictureBox.Height - 20) ' Draw the Y-axis
        g.DrawLine(BlackPen, 5, Result_PictureBox.Height - 20, Result_PictureBox.Width - 5, Result_PictureBox.Height - 20) ' Draw the X-axis
        g.DrawString("0", SystemFonts.DefaultFont, BlackBrush, 5, Result_PictureBox.Height - 15, CenteredFormat) ' Write the 0 at the bottom left corner of the graph
        g.DrawString(ErrorList.Count, SystemFonts.DefaultFont, BlackBrush, Result_PictureBox.Width - 20, Result_PictureBox.Height - 15, CenteredFormat) ' Write the amount of epochs completed in the bottom right corner of the graph 
        Dim ErrorStepVal As Integer = 500 ' The amount of points to put on the graph
        For X = 0 To ErrorList.Count - 1 - ErrorList.Count / ErrorStepVal Step ErrorList.Count / ErrorStepVal ' Cycle through the errors using the ErrorStepVal
            ' Draw a line between 2 error points
            g.DrawLine(BlackPen, CInt(5 + (Result_PictureBox.Width - 10) / (ErrorList.Count) * X), CInt(5 + (Result_PictureBox.Height - 25) * (1 - ErrorList(X))), CInt(5 + (Result_PictureBox.Width - 10) / (ErrorList.Count) * (X + ErrorList.Count / ErrorStepVal)), CInt(5 + ((Result_PictureBox.Height - 25) * (1 - ErrorList(X + ErrorList.Count / ErrorStepVal)))))
        Next

    End Sub

    Private Sub TrainNet() ' This subroutine handles the training of the neural network
        If NeuralNet IsNot Nothing Then ' If a neural network exists
            ProgressBar_Label.Text = "Training" ' Update the progress label to show that training is in progress
            ProgressBar_Label.Refresh()
            Errors = NeuralNet.Train(TrainData, Epochs_TrackBar.Value, True, TrainTest_ProgressBar, ProgressBar_Label) ' Train the network and return the errors
            Draw_Errors(Errors) ' Draw the errors on the picture box
            CustomResults_ListBox.Hide() ' Hide the custom result box
            ProgressBar_Label.Text = "Training Complete" ' Update the progress label to show that training has completed
        End If
    End Sub

    Private Sub TestNet() ' This subroutine handles the testing of the neural network
        If NeuralNet IsNot Nothing Then ' If a neural network exists
            ProgressBar_Label.Text = "Testing" ' Update the progress label to show that testing is in progress
            ProgressBar_Label.Refresh()
            Results = NeuralNet.Test(TestData, TrainTest_ProgressBar) ' Test the network with the specified testing data and return the results
            Dim SelectedTemplate As String = Templates_ComboBox.SelectedItem
            CustomResults_ListBox.Hide()
            If SelectedTemplate = "Sine Graph" Then
                Draw_SineGraph(Results) ' Draw the results in the sine graph format
            ElseIf SelectedTemplate = "XOR" Then
                Draw_XOR(Results) ' Draw the results in the XOR format
            ElseIf SelectedTemplate = "X and O classification" Then
                Draw_XandO(Results) ' Draw the results in the X and O classification format
            Else
                Draw_Custom(Results) ' Draw the results in the custom format
            End If
            ProgressBar_Label.Text = "Testing Complete" ' Update the progress label to show that testing has completed
        End If
    End Sub

    Private Sub SetupNet_Custom(FileName As String) ' Sets up the custom network
        SetupData_Custom(FileName) ' Set up the data using the references in the .txt file
        NeuralNet = New Net(FileName) ' Initialises the network with the specified .txt file
        Epochs_TrackBar.Value = 10000 ' Set the epochs to a default value of 10000
    End Sub

    Private Sub SetupData_Custom(FileName As String) ' Sets up the data from the references in the .txt file
        TrainData.Clear() ' Clear the training data
        TestData.Clear() ' Clear the testing data
        Dim FileReader As New StreamReader(FileName) ' Open the file
        For X = 0 To 3 ' Read the first 3 lines to get past the parameters
            FileReader.ReadLine()
        Next
        Dim TrainData_File As String = Environment.CurrentDirectory.Split("\")(0) & FileReader.ReadLine() ' Read the training data location
        Dim TestData_File As String = Environment.CurrentDirectory.Split("\")(0) & FileReader.ReadLine() ' Read the testing data location
        FileReader.Close() ' Close the file
        FileReader = New StreamReader(TrainData_File) ' Open the training data file
        While FileReader.Peek <> -1 ' Loop until we reach the end of the file
            Dim CurrentData As String() = FileReader.ReadLine().Split(",") ' File is CSV, so split the line into seperate values
            Dim CurrentDataConvert As New List(Of Double)
            For X = 0 To CurrentData.Length - 1 ' Convert from string values to double values
                CurrentDataConvert.Add(Convert.ToDouble(CurrentData(X)))
            Next
            TrainData.Add(CurrentDataConvert) ' Add the data to the training data list
        End While
        FileReader.Close() ' Close the file
        FileReader = New StreamReader(TestData_File) ' Open the testing data file
        While FileReader.Peek <> -1 ' SAME AS FOR TRAINING DATA
            Dim CurrentData As String() = FileReader.ReadLine().Split(",")
            Dim CurrentDataConvert As New List(Of Double)
            For X = 0 To CurrentData.Length - 1
                CurrentDataConvert.Add(Convert.ToDouble(CurrentData(X)))
            Next
            TestData.Add(CurrentDataConvert)
        End While
        FileReader.Close()
    End Sub

    Private Sub Draw_Custom(Results As List(Of Double)) ' Draws the results of the custom network

        DrawingResults = True
        CustomResults_ListBox.Show() ' Show the custom results listbox
        CustomResults_ListBox.Items.Clear() ' Clear the custom results items
        For X = 0 To Results.Count - 1 ' Loop through each result
            Dim ResultString As String = "" ' The string to write to the items list
            For I = 0 To TestData(X).Count - 1 ' Loop through the size of the current data
                If I <> TestData(X).Count - 1 Then ' If this is not the last piece of data
                    ResultString = ResultString & TestData(X)(I) & ", " ' Add the test input and a , to the string
                Else
                    ResultString = ResultString & TestData(X)(I) & " : " ' Add the test input and a : to the string
                End If
            Next
            ResultString = ResultString & Results(X) ' Add the result to the string
            CustomResults_ListBox.Items.Add(ResultString) ' Add the string to the items list
        Next

    End Sub

    Private Sub SetupNet_XandO() ' This subroutine set's up the X and O classification network
        SetupData_XandO() ' Setup the training and testing data for the network
        NeuralNet = New Net("net_xando.txt") ' Create a new Net object using the specified .txt file
        Epochs_TrackBar.Value = 40000 ' Set the epochs to the default amount
    End Sub

    Private Sub SetupData_XandO() ' Sets up the data for the X and O classification network
        TrainData.Clear() ' Clear the training data
        TestData.Clear() ' Clear the testing data
        For I = 1 To 20 ' 20 images of Xs
            Dim LoadedImage As Image = Image.FromFile("data_xando/x/" + I.ToString() + ".png") ' Select the image
            Dim BitmapImage As New Bitmap(LoadedImage) ' Create a bitmap so each pixel can be evaluated
            Dim TempPixelList As New List(Of Double) ' A list of the images pixels as 1's and 0's
            For Y = 0 To BitmapImage.Height - 1 ' Loop through each Y value
                For X = 0 To BitmapImage.Width - 1 ' Loop through each X value
                    Dim Pixel As Color = BitmapImage.GetPixel(X, Y) ' Get the pixel at X,Y
                    If Pixel.Name = "ff000000" Then ' If the pixel is black
                        TempPixelList.Add(1) ' Add a 1 to the pixel list
                    Else
                        TempPixelList.Add(0) ' Otherwise, add a 0 for white
                    End If
                Next
            Next
            If I < 15 Then ' 14 pieces of training data
                TempPixelList.Add(0) ' Set the target to 0 (X)
                TrainData.Add(TempPixelList) ' Add to the list of training data
            Else ' 6 pieces of test data
                TestData.Add(TempPixelList) ' Add to the list of testing data
            End If
        Next
        For I = 1 To 20 ' 20 images of Os
            ' SAME AS ABOVE
            Dim LoadedImage As Image = Image.FromFile("data_xando/o/" + I.ToString() + ".png")
            Dim BitmapImage As New Bitmap(LoadedImage)
            Dim TempPixelList As New List(Of Double)
            For Y = 0 To BitmapImage.Height - 1
                For X = 0 To BitmapImage.Width - 1
                    Dim Pixel As Color = BitmapImage.GetPixel(X, Y)
                    If Pixel.Name = "ff000000" Then
                        TempPixelList.Add(1) ' Black
                    Else
                        TempPixelList.Add(0) ' White
                    End If
                Next
            Next
            If I < 15 Then
                TempPixelList.Add(1) ' Set the target to 1 (O)
                TrainData.Add(TempPixelList)
            Else
                TestData.Add(TempPixelList)
            End If
        Next
    End Sub

    Private Sub Draw_XandO(Results As List(Of Double), Optional eGraphics As Graphics = Nothing) ' Controls the drawing of the X and O classification results

        DrawingResults = True ' Indicates that results are currently being drawn
        ' ------------- SAME AS Draw_Errors() ---------------
        Dim g As Graphics
        If IsNothing(eGraphics) Then
            g = Result_PictureBox.CreateGraphics()
        Else
            g = eGraphics
        End If
        g.Clear(DefaultBackColor)
        Dim BlackBrush As Brush = Brushes.Black
        Dim WritingFont As New Font("Calibri", 13)
        Dim CenteredFormat As New StringFormat()
        CenteredFormat.Alignment = StringAlignment.Center
        ' ---------------------------------------------------
        For X = 0 To Results.Count - 1 ' Loop through all the results
            Dim ResultText As String ' The text to write on the picture box
            If Results(X) < 0.5 Then ' If the result is less than 0.5 then assume that it has guessed it is an X
                ResultText = "X (" + Math.Round((1 - Results(X)) * 100, 2).ToString() + "%)" ' Confidence percentage
            Else ' Otherwise, assume it has guessed it is an O
                ResultText = "O (" + Math.Round(Results(X) * 100, 2).ToString() + "%)" ' Confidence percentage
            End If
            If X + 1 <= Results.Count / 2 Then ' Tests with X images
                Dim DrawImage As New Bitmap(64, 64) ' Image that was tested will now be drawn on picture box
                Dim BitGraphics As Graphics = Graphics.FromImage(DrawImage) ' Create a graphics object from the bitmap
                BitGraphics.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic ' Prevent blurring
                BitGraphics.DrawImage(Image.FromFile("data_xando/drawingimages/x/" + (X + 1).ToString() + ".png"), 0, 0, 32, 32) ' Draw the image onto the bitmap
                g.DrawImage(DrawImage, CInt(Result_PictureBox.Width / 8), CInt(Result_PictureBox.Height / 6 * X)) ' Draw the bitmap onto the picture box
                ' Write the guess and confidence percentage on the picture box
                g.DrawString(ResultText, WritingFont, BlackBrush, CInt(Result_PictureBox.Width / 8 * 3), CInt(Result_PictureBox.Height / 6 * X), CenteredFormat)
            Else ' Tests with O images
                ' SAME AS ABOVE
                Dim DrawImage As New Bitmap(64, 64)
                Dim BitGraphics As Graphics = Graphics.FromImage(DrawImage)
                BitGraphics.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                BitGraphics.DrawImage(Image.FromFile("data_xando/drawingimages/o/" + (X - 5).ToString() + ".png"), 0, 0, 32, 32)
                g.DrawImage(DrawImage, CInt(Result_PictureBox.Width / 8 * 5), CInt(Result_PictureBox.Height / 6 * (X - Results.Count / 2)))
                g.DrawString(ResultText, WritingFont, BlackBrush, CInt(Result_PictureBox.Width / 8 * 7), CInt(Result_PictureBox.Height / 6 * (X - Results.Count / 2)), CenteredFormat)
            End If
        Next

    End Sub

    Private Sub SetupNet_XOR() ' Sets up the network for the XOR template
        SetupData_XOR() ' Set up the testing and training data
        NeuralNet = New Net("net_xor.txt") ' Create a network with the XOR .txt file
        Epochs_TrackBar.Value = 5000 ' Set the epochs to the default value
    End Sub

    Private Sub SetupData_XOR() ' Sets up the data for the XOR network
        TrainData.Clear() ' Clear the training data
        TestData.Clear() ' Clear the testing data
        ' Add the XOR rules to the training and testing data
        TrainData.Add(New List(Of Double)({0, 0, 0}))
        TrainData.Add(New List(Of Double)({0, 1, 1}))
        TrainData.Add(New List(Of Double)({1, 0, 1}))
        TrainData.Add(New List(Of Double)({1, 1, 0}))
        TestData.Add(New List(Of Double)({0, 0}))
        TestData.Add(New List(Of Double)({0, 1}))
        TestData.Add(New List(Of Double)({1, 0}))
        TestData.Add(New List(Of Double)({1, 1}))
    End Sub

    Private Sub Draw_XOR(Results As List(Of Double), Optional eGraphics As Graphics = Nothing) ' Draws the XOR results on the picture box

        ' ------------------ SAME AS Draw_XandO() --------------------
        DrawingResults = True
        Dim g As Graphics
        If IsNothing(eGraphics) Then
            g = Result_PictureBox.CreateGraphics()
        Else
            g = eGraphics
        End If
        g.Clear(DefaultBackColor)
        Dim BlackBrush As Brush = Brushes.Black
        Dim BlackPen As New Drawing.Pen(Color.Black)
        Dim WritingFont As New Font("Calibri", 16)
        Dim WritingFont_B As New Font("Calibri", 16, FontStyle.Bold)
        Dim CenteredFormat As New StringFormat()
        CenteredFormat.Alignment = StringAlignment.Center
        ' ------------------------------------------------------------
        ' Write the table headings
        g.DrawString("Inputs", WritingFont_B, BlackBrush, Result_PictureBox.Width / 12 * 3, Result_PictureBox.Height / 6 * 0.25, CenteredFormat)
        g.DrawString("Outputs", WritingFont_B, BlackBrush, Result_PictureBox.Width / 12 * 8, Result_PictureBox.Height / 6 * 0.25, CenteredFormat)
        ' Draw the center line
        g.DrawLine(BlackPen, CInt(Result_PictureBox.Width / 2 * 0.95), 0, CInt(Result_PictureBox.Width / 2 * 0.95), CInt(Result_PictureBox.Height / 6 * 4.75))
        For X = 0 To Results.Count - 1 ' Cycle through each result
            ' Draw a line underneath each result
            g.DrawLine(BlackPen, CInt(Result_PictureBox.Width / 12 * 1.25), CInt(Result_PictureBox.Height / 6 * (0.75 + X)), CInt(Result_PictureBox.Width / 12 * 10.25), CInt(Result_PictureBox.Height / 6 * (0.75 + X)))
            ' Write the inputs and outputs into the table
            g.DrawString(Math.Round(TestData(X)(0)), WritingFont, BlackBrush, Result_PictureBox.Width / 6, Result_PictureBox.Height / 6 * (X + 1), CenteredFormat)
            g.DrawString(Math.Round(TestData(X)(1)), WritingFont, BlackBrush, Result_PictureBox.Width / 6 * 2, Result_PictureBox.Height / 6 * (X + 1), CenteredFormat)
            g.DrawString(Math.Round(Results(X), 4), WritingFont, BlackBrush, Result_PictureBox.Width / 6 * 4, Result_PictureBox.Height / 6 * (X + 1), CenteredFormat)
        Next

    End Sub

    Private Sub SetupNet_SineGraph() ' Sets up the network for the sine graph template
        SetupData_SineGraph() ' Set up the training and testing data
        NeuralNet = New Net("net_sinegraph.txt") ' Create the Net object using the sine graph .txt file
        Epochs_TrackBar.Value = 200000 ' Set the epochs to the default value
    End Sub

    Private Sub SetupData_SineGraph() ' Sets up the training/testing data for the sine graph network
        TrainData.Clear() ' Empty the training data
        TestData.Clear() ' Empty the testing data
        For X = 0 To 360 ' 0 degrees to 360 degrees
            TrainData.Add(New List(Of Double)({X / 360, Map(Math.Sin(ToRadians(X)), -1, 1, 0, 1)})) ' Add the degrees and the sin mapped between 0 and 1 to the training data
            TestData.Add(New List(Of Double)({X / 360})) ' Add the degrees mapped between 0 and 1 to the testing data
        Next
    End Sub

    Private Sub Draw_SineGraph(Results As List(Of Double), Optional eGraphics As Graphics = Nothing) ' Draw's the sine graph results on the picture box

        DrawingResults = True ' Indicates that the results are being drawn

        Dim TempResults As New List(Of Double) ' Used to unmap the results from 0 to 1 to between -1 and 1
        For X = 0 To Results.Count - 1 ' Loop through each result
            TempResults.Add(Map(Results(X), 0, 1, -1, 1)) ' Map the result from 0 to 1 to between -1 and 1
        Next

        ' ---------------- SAME AS Draw_Errors() --------------------
        Dim g As Graphics
        If IsNothing(eGraphics) Then
            g = Result_PictureBox.CreateGraphics()
        Else
            g = eGraphics
        End If
        g.Clear(DefaultBackColor)
        Dim BlackPen As New Drawing.Pen(Color.Black)
        Dim RedPen As New Drawing.Pen(Color.Red)
        ' -----------------------------------------------------------
        g.DrawLine(BlackPen, 0, CInt(Result_PictureBox.Height / 2), Result_PictureBox.Width, CInt(Result_PictureBox.Height / 2)) ' Draw the X-axis
        g.DrawLine(BlackPen, 0, 0, 0, Result_PictureBox.Height) ' Draw the Y-axis

        For X = 0 To 359 ' Cycle through each degree
            ' Draw the actual sine graph by diving the width of the picture box into 360 sections
            g.DrawLine(BlackPen, CInt(X * (Result_PictureBox.Width / 360)), CInt(Result_PictureBox.Height / 2 - ((Result_PictureBox.Height / 4) * Math.Sin(ToRadians(X)))), CInt((X + 1) * (Result_PictureBox.Width / 360)), CInt(Result_PictureBox.Height / 2 - ((Result_PictureBox.Height / 4) * Math.Sin(ToRadians(X + 1)))))
            ' Draw the predicted sine graph in the same way
            g.DrawLine(RedPen, CInt(X * (Result_PictureBox.Width / 360)), CInt(Result_PictureBox.Height / 2 - (Result_PictureBox.Height / 4 * TempResults(X))), CInt((X + 1) * (Result_PictureBox.Width / 360)), CInt(Result_PictureBox.Height / 2 - (Result_PictureBox.Height / 4 * TempResults(X + 1))))
        Next

    End Sub

    Private Function ToRadians(Degrees As Double) ' Used to convert degrees to radians (used for the sine graph network)
        Return Degrees * Math.PI / 180
    End Function

    ' Maps a value within StartRange1 and StopRange1 to between StartRange2 and StopRange2
    Private Function Map(Value As Double, StartRange1 As Double, StopRange1 As Double, StartRange2 As Double, StopRange2 As Double) As Double
        Return ((Value - StartRange1) / (StopRange1 - StartRange1)) * (StopRange2 - StartRange2) + StartRange2
    End Function

    Private Sub Epochs_TrackBar_Scroll(sender As Object, e As EventArgs) Handles Epochs_TrackBar.Scroll ' Occurs when the epochs scrollbar is scrolled
        Epochs_TrackBar.Value = Math.Max(1, Math.Round(Epochs_TrackBar.Value / 1000) * 1000) ' Round the epochs to the nearest thousand (allows the option of 1)
        EpochNum_Label.Text = Epochs_TrackBar.Value ' Update the epoch label to be the track bar value
    End Sub

    Private Sub LearningRate_TrackBar_Scroll(sender As Object, e As EventArgs) Handles LearningRate_TrackBar.Scroll ' Occurs when the learning rate scrollbar is scrolled
        LearningRateNum_Label.Text = LearningRate_TrackBar.Value / 100 ' Set the label value to the trackbar value divided by 100 (2 decimal places)
        If TypeOf sender IsNot Form Then ' If the subroutine is not called by the form
            NeuralNet.LearningRate = LearningRate_TrackBar.Value / 100 ' Update the network's learning rate
        End If
    End Sub

    Private Sub Momentum_TrackBar_Scroll(sender As Object, e As EventArgs) Handles Momentum_TrackBar.Scroll ' Occurs when the momentum scrollbar is scrolled
        MomentumNum_Label.Text = Momentum_TrackBar.Value / 100 ' Set the label value to the trackbar value divided by 100 (2 decimal places)
        If TypeOf sender IsNot Form Then ' If the subroutine is not called by the form
            NeuralNet.Momentum = Momentum_TrackBar.Value / 100 ' Update the network's momentum
        End If
    End Sub

    Private Sub HiddenNeurons_TrackBar_Scroll(sender As Object, e As EventArgs) Handles HiddenNeurons_TrackBar.Scroll ' Occurs when the hidden neurons scrollbar is scrolled
        HiddenNeuronNum_Label.Text = HiddenNeurons_TrackBar.Value ' Set the label value to the trackbar value
        If TypeOf sender IsNot Form Then ' Of the subroutine is not called by the form
            NeuralNet.UpdateHiddenLayer(HiddenNeurons_TrackBar.Value) ' Update the network's layout
        End If
    End Sub

    Private Sub Train_Button_Click(sender As Object, e As EventArgs) Handles Train_Button.Click ' Train the network when the train button is pressed
        TrainNet()
    End Sub

    Private Sub Test_Button_Click(sender As Object, e As EventArgs) Handles Test_Button.Click ' Test the network when the test button is pressed
        TestNet()
    End Sub

    Private Sub Bias_CheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles Bias_CheckBox.CheckedChanged ' Change the bias value when the bias checkbox is changed
        NeuralNet.BiasValue = Convert.ToInt16(Bias_CheckBox.Checked) ' Convert the boolean to either 1 or 0 and update the network's bias value
    End Sub

    Private Sub Reset_Button_Click(sender As Object, e As EventArgs) Handles Reset_Button.Click ' Create a new network when the reset button is pressed
        Templates_ComboBox_SelectedIndexChanged(Me, New EventArgs)
    End Sub

    Private Sub Result_PictureBox_Paint(sender As Object, e As PaintEventArgs) Handles Result_PictureBox.Paint ' When the paint event occurs, redraw on the picture box
        If Not DrawingResults Then ' If results are not being drawn then draw the errors
            If Errors.Count > 0 Then ' Only draw if there are any errors
                CustomResults_ListBox.Hide()
                Draw_Errors(Errors, e.Graphics) ' Draw the errors using the specified graphics object
            End If
        Else ' Otherwise, draw the results
            If Results.Count > 0 Then ' Only draw if there are any results
                CustomResults_ListBox.Hide()
                If Templates_ComboBox.SelectedItem = "Sine Graph" Then
                    Draw_SineGraph(Results, e.Graphics) ' Draw the sine graph results with the specified graphics object
                ElseIf Templates_ComboBox.SelectedItem = "XOR" Then
                    Draw_XOR(Results, e.Graphics) ' Draw the XOR results with the specified graphics object
                ElseIf Templates_ComboBox.SelectedItem = "X and O classification" Then
                    Draw_XandO(Results, e.Graphics) ' Draw the X and O classification results with the specified graphics object
                End If
            End If
        End If
    End Sub

    Private Sub LoadWeights_Button_Click(sender As Object, e As EventArgs) Handles LoadWeights_Button.Click ' When the load weights button is clicked, open a file dialog and save the file path
        Dim FileWindow As New OpenFileDialog() ' Create a new open file dialog
        Dim FileName As String ' Will store the selected file name

        ' Set up the file dialog settings
        FileWindow.Title = "Load Weights"
        FileWindow.InitialDirectory = "C:\"
        FileWindow.Filter = "CSV files (*.csv)|*.csv"
        FileWindow.FilterIndex = 1
        FileWindow.RestoreDirectory = True

        ' Show the file dialog
        If FileWindow.ShowDialog() = DialogResult.OK Then ' If the user presses OK
            FileName = FileWindow.FileName ' Set the file name to the selected file
            Dim TempWeights As String() = File.ReadAllText(FileName).Split(",") ' File is CSV, so split into seperate values
            If Not NeuralNet.NewWeights(TempWeights) Then ' If the size of the weight matrixes is different
                MessageBox.Show("Too many/little weight in file.") ' Display an error message
            Else ' Otherwise, test the network and display results
                Test_Button_Click(Me, New EventArgs)
            End If
        End If
    End Sub

    Private Sub SaveWeights_Button_Click(sender As Object, e As EventArgs) Handles SaveWeights_Button.Click
        Dim FileWindow As New SaveFileDialog() ' Create a new save file dialog
        Dim FileName As String ' Will store the selected file name

        ' Set up the file dialog settings
        FileWindow.Title = "Load Weights"
        FileWindow.InitialDirectory = "C:\"
        FileWindow.Filter = "CSV files (*.csv)|*.csv"
        FileWindow.FilterIndex = 1
        FileWindow.RestoreDirectory = True

        If FileWindow.ShowDialog() = DialogResult.OK Then ' If the user presses OK
            FileName = FileWindow.FileName ' Set the file name to the selected file
            Dim FileWriter As New StreamWriter(FileName) ' Open the selected file
            Dim ToWrite As New List(Of String) ' To be written to the file
            For X = 0 To NeuralNet.Neurons.Count - 1 ' Loop through every neuron
                For J = 0 To NeuralNet.Neurons(X).InputConnections.Count - 1 ' Loop through all the input connections
                    If X = NeuralNet.Neurons.Count - 1 And J = NeuralNet.Neurons(X).InputConnections.Count - 1 Then ' If it is the last neuron's last connection
                        ToWrite.Add(NeuralNet.Neurons(X).InputConnections.Values(J).Weight) ' Add the weight to the list
                    Else
                        ToWrite.Add(NeuralNet.Neurons(X).InputConnections.Values(J).Weight & ",") ' Add the weight and a , to the list
                    End If
                Next
            Next
            For Each Value In ToWrite
                FileWriter.Write(Value) ' Write all the weights to the file
            Next
            FileWriter.Close() ' Close the file
        End If
    End Sub

    Private Sub CreateCustom_Button_Click(sender As Object, e As EventArgs) Handles CreateCustom_Button.Click ' Occurs when the custom template creation button is clicked
        Dim CustomDialog As Form = New CustomCreation() ' Create a new CustomCreation form
        CustomDialog.ShowDialog() ' Show the form as a dialog box

        Templates_ComboBox.Items.Clear() ' Empty the templates combobox
        CustomTemplates.Clear() ' Clear the custom templates list

        ' SAME AS IN LOAD FUNCTION
        Dim DebugFiles As String() = Directory.GetFiles(Environment.CurrentDirectory & "\custom_templates")

        For Each Doc In DebugFiles
            Dim DocSplit As String() = Doc.Split("\")
            Dim FileName As String = DocSplit(DocSplit.Length - 1)
            CustomTemplates.Add(DocSplit(DocSplit.Length - 1))
        Next

        For Each Template In Templates
            Templates_ComboBox.Items.Add(Template) ' Add all the template names to the combo box
        Next
        For Each Template In CustomTemplates
            Templates_ComboBox.Items.Add(Template)
        Next
    End Sub
End Class