Imports System.IO

Public Class NeuralNetwork

    Public NeuralNet As Net
    Private TrainData As New List(Of List(Of Double))
    Private TestData As New List(Of List(Of Double))
    Private Templates As New List(Of String)({"Sine Graph", "X and O classification", "XOR"})
    Private DrawingResults As Boolean = False
    Private Errors As New List(Of Double)
    Private Results As New List(Of Double)

    Public Sub Form1_Load() Handles MyBase.Load

        For Each Template In Templates
            Templates_ComboBox.Items.Add(Template)
        Next
        Epochs_TrackBar_Scroll(Me, New EventArgs)
        LearningRate_TrackBar_Scroll(Me, New EventArgs)
        Momentum_TrackBar_Scroll(Me, New EventArgs)
        HiddenNeurons_TrackBar_Scroll(Me, New EventArgs)

    End Sub

    Private Sub Templates_ComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Templates_ComboBox.SelectedIndexChanged

        Errors.Clear()
        Results.Clear()
        Result_PictureBox.Refresh()

        If Templates_ComboBox.SelectedItem = "Sine Graph" Then
            SetupNet_SineGraph()
        ElseIf Templates_ComboBox.SelectedItem = "XOR" Then
            SetupNet_XOR()
        ElseIf Templates_ComboBox.SelectedItem = "X and O classification" Then
            SetupNet_XandO()
        End If

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

    End Sub

    Private Sub ChangeView_Button_Click(sender As Object, e As EventArgs) Handles ChangeView_Button.Click
        If DrawingResults Then
            If Errors.Count > 0 Then
                Draw_Errors(Errors)
            End If
        Else
            If Results.Count > 0 Then
                If Templates_ComboBox.SelectedItem = "Sine Graph" Then
                    Draw_SineGraph(Results)
                ElseIf Templates_ComboBox.SelectedItem = "XOR" Then
                    Draw_XOR(Results)
                ElseIf Templates_ComboBox.SelectedItem = "X and O classification" Then
                    Draw_XandO(Results)
                End If
            End If
        End If
    End Sub

    Private Sub Draw_Errors(ErrorList As List(Of Double))

        DrawingResults = False
        Dim g As Graphics
        g = Result_PictureBox.CreateGraphics()
        g.Clear(DefaultBackColor)
        Dim BlackBrush As Brush = Brushes.Black
        Dim BlackPen As New Drawing.Pen(Color.Black)
        Dim CenteredFormat As New StringFormat()
        CenteredFormat.Alignment = StringAlignment.Center
        g.DrawLine(BlackPen, 5, 5, 5, Result_PictureBox.Height - 20)
        g.DrawLine(BlackPen, 5, Result_PictureBox.Height - 20, Result_PictureBox.Width - 5, Result_PictureBox.Height - 20)
        g.DrawString("0", SystemFonts.DefaultFont, BlackBrush, 5, Result_PictureBox.Height - 15, CenteredFormat)
        g.DrawString(ErrorList.Count, SystemFonts.DefaultFont, BlackBrush, Result_PictureBox.Width - 20, Result_PictureBox.Height - 15, CenteredFormat)
        Dim ErrorStepVal As Integer = 500
        For X = 0 To ErrorList.Count - 1 - ErrorList.Count / ErrorStepVal Step ErrorList.Count / ErrorStepVal
            g.DrawLine(BlackPen, CInt(5 + (Result_PictureBox.Width - 10) / (ErrorList.Count) * X), CInt(5 + (Result_PictureBox.Height - 25) * (1 - ErrorList(X))), CInt(5 + (Result_PictureBox.Width - 10) / (ErrorList.Count) * (X + ErrorList.Count / ErrorStepVal)), CInt(5 + ((Result_PictureBox.Height - 25) * (1 - ErrorList(X + ErrorList.Count / ErrorStepVal)))))
        Next

    End Sub

    Private Sub TrainNet()
        If NeuralNet IsNot Nothing Then
            ProgressBar_Label.Text = "Training"
            ProgressBar_Label.Refresh()
            Errors = NeuralNet.Train(TrainData, Epochs_TrackBar.Value, True, TrainTest_ProgressBar)
            Draw_Errors(Errors)
            ProgressBar_Label.Text = "Training Complete"
        End If
    End Sub

    Private Sub TestNet()
        If NeuralNet IsNot Nothing Then
            ProgressBar_Label.Text = "Testing"
            ProgressBar_Label.Refresh()
            Results = NeuralNet.Test(TestData, TrainTest_ProgressBar)
            Dim SelectedTemplate As String = Templates_ComboBox.SelectedItem
            If SelectedTemplate = "Sine Graph" Then
                Draw_SineGraph(Results)
            ElseIf SelectedTemplate = "XOR" Then
                Draw_XOR(Results)
            ElseIf SelectedTemplate = "X and O classification" Then
                Draw_XandO(Results)
            End If
            ProgressBar_Label.Text = "Testing Complete"
        End If
    End Sub

    Private Sub SetupNet_XandO()
        SetupData_XandO()
        NeuralNet = New Net("net_xando.txt")
        Epochs_TrackBar.Value = 40000
    End Sub

    Private Sub SetupData_XandO()
        TrainData.Clear()
        TestData.Clear()
        For I = 1 To 20
            Dim LoadedImage As Image = Image.FromFile("data_xando/x/" + I.ToString() + ".png")
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
                TempPixelList.Add(0)
                TrainData.Add(TempPixelList)
            Else
                TestData.Add(TempPixelList)
            End If
        Next
        For I = 1 To 20
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
                TempPixelList.Add(1)
                TrainData.Add(TempPixelList)
            Else
                TestData.Add(TempPixelList)
            End If
        Next
    End Sub

    Private Sub Draw_XandO(Results As List(Of Double))

        DrawingResults = True
        Dim g As Graphics
        g = Result_PictureBox.CreateGraphics()
        g.Clear(DefaultBackColor)
        Dim BlackBrush As Brush = Brushes.Black
        Dim WritingFont As New Font("Calibri", 13)
        Dim CenteredFormat As New StringFormat()
        CenteredFormat.Alignment = StringAlignment.Center
        For X = 0 To Results.Count - 1
            Dim ResultText As String
            If Results(X) < 0.5 Then
                ResultText = "X (" + Math.Round((1 - Results(X)) * 100, 2).ToString() + "%)"
            Else
                ResultText = "O (" + Math.Round(Results(X) * 100, 2).ToString() + "%)"
            End If
            If X + 1 <= Results.Count / 2 Then
                Dim DrawImage As New Bitmap(64, 64)
                Dim BitGraphics As Graphics = Graphics.FromImage(DrawImage)
                BitGraphics.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                BitGraphics.DrawImage(Image.FromFile("data_xando/drawingimages/x/" + (X + 1).ToString() + ".png"), 0, 0, 32, 32)
                g.DrawImage(DrawImage, CInt(Result_PictureBox.Width / 8), CInt(Result_PictureBox.Height / 6 * X))
                g.DrawString(ResultText, WritingFont, BlackBrush, CInt(Result_PictureBox.Width / 8 * 3), CInt(Result_PictureBox.Height / 6 * X), CenteredFormat)
            Else
                Dim DrawImage As New Bitmap(64, 64)
                Dim BitGraphics As Graphics = Graphics.FromImage(DrawImage)
                BitGraphics.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                BitGraphics.DrawImage(Image.FromFile("data_xando/drawingimages/o/" + (X - 5).ToString() + ".png"), 0, 0, 32, 32)
                g.DrawImage(DrawImage, CInt(Result_PictureBox.Width / 8 * 5), CInt(Result_PictureBox.Height / 6 * (X - Results.Count / 2)))
                g.DrawString(ResultText, WritingFont, BlackBrush, CInt(Result_PictureBox.Width / 8 * 7), CInt(Result_PictureBox.Height / 6 * (X - Results.Count / 2)), CenteredFormat)
            End If
        Next

    End Sub

    'Private Sub SetupNet_FootballScores()
    '    SetupData_FootballScores()
    '    NeuralNet = New Net("net_footballscores.txt")
    '    Epochs_TrackBar.Value = 1000000
    'End Sub

    'Private Sub SetupData_FootballScores()
    '    TrainData.Clear()
    '    TestData.Clear()
    'End Sub

    Private Sub SetupNet_XOR()
        SetupData_XOR()
        NeuralNet = New Net("net_xor.txt")
        Epochs_TrackBar.Value = 5000
    End Sub

    Private Sub SetupData_XOR()
        TrainData.Clear()
        TestData.Clear()
        TrainData.Add(New List(Of Double)({0, 0, 0}))
        TrainData.Add(New List(Of Double)({0, 1, 1}))
        TrainData.Add(New List(Of Double)({1, 0, 1}))
        TrainData.Add(New List(Of Double)({1, 1, 0}))
        TestData.Add(New List(Of Double)({0, 0}))
        TestData.Add(New List(Of Double)({0, 1}))
        TestData.Add(New List(Of Double)({1, 0}))
        TestData.Add(New List(Of Double)({1, 1}))
    End Sub

    Private Sub Draw_XOR(Results As List(Of Double))

        DrawingResults = True
        Dim g As Graphics
        g = Result_PictureBox.CreateGraphics()
        g.Clear(DefaultBackColor)
        Dim BlackBrush As Brush = Brushes.Black
        Dim BlackPen As New Drawing.Pen(Color.Black)
        Dim WritingFont As New Font("Calibri", 16)
        Dim WritingFont_B As New Font("Calibri", 16, FontStyle.Bold)
        Dim CenteredFormat As New StringFormat()
        CenteredFormat.Alignment = StringAlignment.Center
        g.DrawString("Inputs", WritingFont_B, BlackBrush, Result_PictureBox.Width / 12 * 3, Result_PictureBox.Height / 6 * 0.25, CenteredFormat)
        g.DrawString("Outputs", WritingFont_B, BlackBrush, Result_PictureBox.Width / 12 * 8, Result_PictureBox.Height / 6 * 0.25, CenteredFormat)
        g.DrawLine(BlackPen, CInt(Result_PictureBox.Width / 2 * 0.95), 0, CInt(Result_PictureBox.Width / 2 * 0.95), CInt(Result_PictureBox.Height / 6 * 4.75))
        For X = 0 To Results.Count - 1
            g.DrawLine(BlackPen, CInt(Result_PictureBox.Width / 12 * 1.25), CInt(Result_PictureBox.Height / 6 * (0.75 + X)), CInt(Result_PictureBox.Width / 12 * 10.25), CInt(Result_PictureBox.Height / 6 * (0.75 + X)))
            g.DrawString(Math.Round(TestData(X)(0)), WritingFont, BlackBrush, Result_PictureBox.Width / 6, Result_PictureBox.Height / 6 * (X + 1), CenteredFormat)
            g.DrawString(Math.Round(TestData(X)(1)), WritingFont, BlackBrush, Result_PictureBox.Width / 6 * 2, Result_PictureBox.Height / 6 * (X + 1), CenteredFormat)
            g.DrawString(Math.Round(Results(X), 4), WritingFont, BlackBrush, Result_PictureBox.Width / 6 * 4, Result_PictureBox.Height / 6 * (X + 1), CenteredFormat)
        Next

    End Sub

    Private Sub SetupNet_SineGraph()
        SetupData_SineGraph()
        NeuralNet = New Net("net_sinegraph.txt")
        Epochs_TrackBar.Value = 200000
    End Sub

    Private Sub SetupData_SineGraph()
        TrainData.Clear()
        TestData.Clear()
        For X = 0 To 360
            TrainData.Add(New List(Of Double)({X / 360, Map(Math.Sin(ToRadians(X)), -1, 1, 0, 1)}))
            TestData.Add(New List(Of Double)({X / 360}))
        Next
    End Sub

    Private Sub Draw_SineGraph(Results As List(Of Double))

        Dim TempResults As New List(Of Double)
        For X = 0 To Results.Count - 1
            TempResults.Add(Map(Results(X), 0, 1, -1, 1))
        Next

        DrawingResults = True
        Dim g As Graphics
        g = Result_PictureBox.CreateGraphics()
        g.Clear(DefaultBackColor)
        Dim BlackPen As New Drawing.Pen(Color.Black)
        Dim RedPen As New Drawing.Pen(Color.Red)
        g.DrawLine(BlackPen, 0, CInt(Result_PictureBox.Height / 2), Result_PictureBox.Width, CInt(Result_PictureBox.Height / 2))
        g.DrawLine(BlackPen, 0, 0, 0, Result_PictureBox.Height)

        For X = 0 To 359
            g.DrawLine(BlackPen, CInt(X * (Result_PictureBox.Width / 360)), CInt(Result_PictureBox.Height / 2 - ((Result_PictureBox.Height / 4) * Math.Sin(ToRadians(X)))), CInt((X + 1) * (Result_PictureBox.Width / 360)), CInt(Result_PictureBox.Height / 2 - ((Result_PictureBox.Height / 4) * Math.Sin(ToRadians(X + 1)))))
            g.DrawLine(RedPen, CInt(X * (Result_PictureBox.Width / 360)), CInt(Result_PictureBox.Height / 2 - (Result_PictureBox.Height / 4 * TempResults(X))), CInt((X + 1) * (Result_PictureBox.Width / 360)), CInt(Result_PictureBox.Height / 2 - (Result_PictureBox.Height / 4 * TempResults(X + 1))))
        Next

    End Sub

    Private Function ToRadians(Degrees As Double)
        Return Degrees * Math.PI / 180
    End Function

    Private Function Map(Value As Double, StartRange1 As Double, StopRange1 As Double, StartRange2 As Double, StopRange2 As Double) As Double
        Return ((Value - StartRange1) / (StopRange1 - StartRange1)) * (StopRange2 - StartRange2) + StartRange2
    End Function

    Private Sub Epochs_TrackBar_Scroll(sender As Object, e As EventArgs) Handles Epochs_TrackBar.Scroll
        Epochs_TrackBar.Value = Math.Max(1, Math.Round(Epochs_TrackBar.Value / 1000) * 1000)
        EpochNum_Label.Text = Epochs_TrackBar.Value
    End Sub

    Private Sub LearningRate_TrackBar_Scroll(sender As Object, e As EventArgs) Handles LearningRate_TrackBar.Scroll
        LearningRateNum_Label.Text = LearningRate_TrackBar.Value / 100
        If TypeOf sender IsNot Form Then
            NeuralNet.LearningRate = LearningRate_TrackBar.Value / 100
        End If
    End Sub

    Private Sub Momentum_TrackBar_Scroll(sender As Object, e As EventArgs) Handles Momentum_TrackBar.Scroll
        MomentumNum_Label.Text = Momentum_TrackBar.Value / 100
        If TypeOf sender IsNot Form Then
            NeuralNet.Momentum = Momentum_TrackBar.Value / 100
        End If
    End Sub

    Private Sub HiddenNeurons_TrackBar_Scroll(sender As Object, e As EventArgs) Handles HiddenNeurons_TrackBar.Scroll
        HiddenNeuronNum_Label.Text = HiddenNeurons_TrackBar.Value
        If TypeOf sender IsNot Form Then
            NeuralNet.UpdateHiddenLayerSize(HiddenNeurons_TrackBar.Value)
        End If
    End Sub

    Private Sub Train_Button_Click(sender As Object, e As EventArgs) Handles Train_Button.Click
        TrainNet()
    End Sub

    Private Sub Test_Button_Click(sender As Object, e As EventArgs) Handles Test_Button.Click
        TestNet()
    End Sub

    Private Sub Bias_CheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles Bias_CheckBox.CheckedChanged
        NeuralNet.BiasValue = Convert.ToInt16(Bias_CheckBox.Checked)
    End Sub

    Private Sub Reset_Button_Click(sender As Object, e As EventArgs) Handles Reset_Button.Click
        Templates_ComboBox_SelectedIndexChanged(Me, New EventArgs)
    End Sub
End Class