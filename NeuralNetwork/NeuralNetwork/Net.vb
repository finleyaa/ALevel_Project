Imports System.IO

Public Class Net

    Private Neurons As New List(Of Neuron)
    Public BiasValue As New Double
    Private Rnd As New Random
    Public LearningRate As Double
    Private PreviousWeightUpdateIH As New List(Of Double)
    Private PreviousWeightUpdateHO As New List(Of Double)
    Public Momentum As Double
    Private InputLayerSize, OutputLayerSize As Integer
    Public HiddenLayerSize As Integer

    Public Sub New(ByRef InputFile As String)

        Dim Reader As StreamReader
        Reader = New StreamReader(InputFile)
        Dim Layout As String() = Reader.ReadLine().Split(",")
        InputLayerSize = Layout(0)
        HiddenLayerSize = Layout(1)
        OutputLayerSize = Layout(2)

        LearningRate = Convert.ToDouble(Reader.ReadLine())
        Momentum = Convert.ToDouble(Reader.ReadLine())
        BiasValue = Convert.ToDouble(Reader.ReadLine())
        Reader.Close()

        Neurons.Clear()

        For X = 0 To InputLayerSize + 1 + HiddenLayerSize + 1 + OutputLayerSize - 1 ' Do +1 for bias
            Neurons.Add(New Neuron())
        Next

        For X = InputLayerSize + 1 To InputLayerSize + 1 + HiddenLayerSize + 1 - 2
            For Y = 0 To InputLayerSize
                Neurons(X).NewInputConnection(Neurons(Y), New Link(Rnd))
            Next
        Next

        For X = InputLayerSize + 1 + HiddenLayerSize + 1 To InputLayerSize + 1 + HiddenLayerSize + 1 + OutputLayerSize - 1
            For Y = InputLayerSize + 1 To InputLayerSize + 1 + HiddenLayerSize
                Neurons(X).NewInputConnection(Neurons(Y), New Link(Rnd))
            Next
        Next

    End Sub

    Public Sub UpdateHiddenLayerSize(NewSize As Integer)

        HiddenLayerSize = NewSize

        Neurons.Clear()

        For X = 0 To InputLayerSize + 1 + HiddenLayerSize + 1 + OutputLayerSize - 1 ' Do +1 for bias
            Neurons.Add(New Neuron())
        Next

        For X = InputLayerSize + 1 To InputLayerSize + 1 + HiddenLayerSize + 1 - 2
            For Y = 0 To InputLayerSize
                Neurons(X).NewInputConnection(Neurons(Y), New Link(Rnd))
            Next
        Next

        For X = InputLayerSize + 1 + HiddenLayerSize + 1 To InputLayerSize + 1 + HiddenLayerSize + 1 + OutputLayerSize - 1
            For Y = InputLayerSize + 1 To InputLayerSize + 1 + HiddenLayerSize
                Neurons(X).NewInputConnection(Neurons(Y), New Link(Rnd))
            Next
        Next

    End Sub

    Public Function Train(TrainingData As List(Of List(Of Double)), Epochs As Integer, ReturnErrors As Boolean, Optional ByRef ProgressBar As ProgressBar = Nothing) As List(Of Double)

        Dim Rnd As New Random
        Dim SelectedValue As New List(Of Double)
        Dim Result As Double
        Dim TrainVals As New List(Of Double)
        Dim Target As Double
        Dim Errors As New List(Of Double)

        For Num = 1 To Epochs

            TrainVals.Clear()

            SelectedValue = TrainingData(Math.Round(Rnd.Next(TrainingData.Count)))
            TrainVals.AddRange(SelectedValue)
            TrainVals.RemoveAt(TrainVals.Count - 1)
            Target = SelectedValue(SelectedValue.Count - 1)

            Result = Run(TrainVals)
            BackProp(Result, Target)
            Errors.Add(Math.Abs(Result - Target))

            If Not IsNothing(ProgressBar) Then
                ProgressBar.Value = Num / Epochs * 100
            End If

        Next

        If ReturnErrors Then
            Return Errors
        Else
            Return New List(Of Double)({0})
        End If

    End Function

    Public Function Test(TestData As List(Of List(Of Double)), Optional ByRef ProgressBar As ProgressBar = Nothing) As List(Of Double)

        Dim Results As New List(Of Double)

        For Each Values In TestData
            Results.Add(Run(Values))
            If Not IsNothing(ProgressBar) Then
                ProgressBar.Value = (TestData.IndexOf(Values) + 1) / TestData.Count * 100
            End If
        Next

        Return Results

    End Function

    Private Function Run(Values) As Double

        ' Reset all layers
        For Each Neuron In Neurons
            Neuron.SetValue(0)
        Next
        Neurons(InputLayerSize).SetValue(BiasValue)
        Neurons(InputLayerSize + 1 + HiddenLayerSize).SetValue(BiasValue)

        Dim Result As Double
        For X = 0 To InputLayerSize - 1
            Neurons(X).SetValue(Values(X))
        Next

        For Each Neuron In Neurons
            Neuron.FeedForward()
        Next

        Result = Neurons(Neurons.Count - 1).GetValue()

        Return Result

    End Function

    Private Sub BackProp(ActualValue As Double, TargetValue As Double)

        Dim OutputErr As Double = ActualValue - TargetValue
        Dim OutputDelta As Double = SigmoidDeriv(Neurons(Neurons.Count - 1).GetValue())

        For X = Neurons.Count - 1 To InputLayerSize + 1 Step -1
            If X < Neurons.Count - OutputLayerSize - 1 Then ' -1 to skip bias neuron
                Neurons(X).CalculateBackProp(Neurons(Neurons.Count - 1), OutputErr, OutputDelta, True)
            ElseIf X >= Neurons.Count - OutputLayerSize Then
                Neurons(X).CalculateBackProp(Neurons(Neurons.Count - 1), OutputErr, OutputDelta, False)
            End If
        Next

        For Each Neuron In Neurons
            Neuron.UpdateWeights(LearningRate, Momentum)
        Next

    End Sub

    Private Function Sigmoid(ValToSig As Double) As Double
        Return 1 / (1 + Math.Exp(-ValToSig))
    End Function

    Private Function SigmoidDeriv(ValToSigDeriv As Double) As Double
        Return ValToSigDeriv * (1 - ValToSigDeriv)
    End Function

End Class
