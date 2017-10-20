Public Class Neuron

    Private Value As Double
    Private InputConnections As New Dictionary(Of Neuron, Link)
    Private WeightChange As New List(Of Double)
    Private PreviousWeightChange As New List(Of Double)

    Public Sub SetValue(NewValue As Double)
        Value = NewValue
    End Sub

    Public Function GetValue() As Double
        Return Value
    End Function

    Public Sub AddToValue(AddValue As Double)
        Value += AddValue
    End Sub

    Public Sub SigmoidVal()
        Value = 1 / (1 + Math.Exp(-Value))
    End Sub

    Public Function SigmoidDerivVal() As Double
        Return Value * (1 - Value)
    End Function

    Public Sub NewInputConnection(NewNeuron As Neuron, NewWeight As Link)
        InputConnections.Add(NewNeuron, NewWeight)
    End Sub

    Public Sub FeedForward()
        For Each Connection In InputConnections
            Value += Connection.Key.GetValue() * Connection.Value.Weight
        Next
        If InputConnections.Count > 0 Then
            SigmoidVal()
        End If
    End Sub

    Public Sub CalculateBackProp(OutputNeuron As Neuron, OutputError As Double, OutputDelta As Double, IsHiddenNeuron As Boolean)

        WeightChange.Clear()

        For Each Connection In InputConnections
            If IsHiddenNeuron Then
                Dim OutputWeight As Double = OutputNeuron.InputConnections.ElementAt(OutputNeuron.InputConnections.Keys.ToList().IndexOf(Me)).Value.Weight
                WeightChange.Add(OutputError * OutputDelta * Connection.Key.GetValue() * OutputWeight * SigmoidDerivVal())
            Else
                WeightChange.Add(OutputError * OutputDelta * Connection.Key.GetValue())
            End If
        Next

    End Sub

    Public Sub UpdateWeights(LearningRate As Double, Momentum As Double)
        For X = 0 To InputConnections.Count - 1
            If PreviousWeightChange.Count > 0 Then
                InputConnections.ElementAt(X).Value.Weight -= LearningRate * WeightChange(X) + Momentum * PreviousWeightChange(X)
            Else
                InputConnections.ElementAt(X).Value.Weight -= LearningRate * WeightChange(X)
            End If
        Next
        PreviousWeightChange.Clear()
        PreviousWeightChange.AddRange(WeightChange)
    End Sub

End Class
