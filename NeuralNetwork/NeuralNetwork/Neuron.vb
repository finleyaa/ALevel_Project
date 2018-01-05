' This class is the neuron class, it contains a value and also knows all of it's input weights and neurons
' It carries out the feed-forward And back-propagation procedure on itself.
Public Class Neuron

    Public Value As Double ' Value stored within the neuron
    Public InputConnections As New Dictionary(Of Neuron, Link) ' All the input weights and their corresponding neurons
    Private WeightChange As New List(Of Double) ' The list to store the calculated weight changes
    Private PreviousWeightChange As New List(Of Double) ' The list to store the previous calculated weight changes

    Public Sub AddToValue(AddValue As Double) ' Adds the specified value to the value stored within the neuron
        Value += AddValue
    End Sub

    Public Sub SigmoidVal() ' Carries out the sigmoid function on the neuron's value
        Value = 1 / (1 + Math.Exp(-Value))
    End Sub

    Public Function SigmoidDerivVal() As Double ' Returns the partial sigmoid derivative of the neuron's value
        Return Value * (1 - Value)
    End Function

    Public Sub NewInputConnection(NewNeuron As Neuron, NewWeight As Link) ' Creates a new input link between the current neuron and the specified neuron
        InputConnections.Add(NewNeuron, NewWeight)
    End Sub

    Public Sub FeedForward() ' Carries out the feed-forward procedure
        ' Cycle through all the input connections and multiply the associated input by it's weight, then add it to the neuron's current value
        For Each Connection In InputConnections
            Value += Connection.Key.Value * Connection.Value.Weight
        Next
        ' Sigmoid the value
        If InputConnections.Count > 0 Then ' Do not sigmoid if the neuron is an input neuron
            SigmoidVal()
        End If
    End Sub

    Public Sub CalculateBackProp(OutputNeuron As Neuron, OutputError As Double, OutputDelta As Double, IsHiddenNeuron As Boolean) ' Carries out the back-propagation algorithm

        WeightChange.Clear()

        ' Cycle through each input connection and calcuate the required change in weights
        For Each Connection In InputConnections
            If IsHiddenNeuron Then ' Hidden and input neuron back-propagation is different to output neuron
                Dim OutputWeight As Double = OutputNeuron.InputConnections.ElementAt(OutputNeuron.InputConnections.Keys.ToList().IndexOf(Me)).Value.Weight
                ' error * partial derivative of output * input value * output weight * partial derivative of value
                WeightChange.Add(OutputError * OutputDelta * Connection.Key.Value * OutputWeight * SigmoidDerivVal())
            Else ' Output neuron back-propagation
                ' error * partial derivative of output * input value
                WeightChange.Add(OutputError * OutputDelta * Connection.Key.Value)
            End If
        Next

    End Sub

    Public Sub UpdateWeights(LearningRate As Double, Momentum As Double) ' Change the weights according to the back-propagation algorithm
        For X = 0 To InputConnections.Count - 1
            If PreviousWeightChange.Count > 0 Then ' If this is the first weight update we won't have any values for momentum
                ' Update the weights by multiplying the learning rate and the calculated weight change, along with the momentum value and the previous calculated weight change
                InputConnections.ElementAt(X).Value.Weight -= LearningRate * WeightChange(X) + Momentum * PreviousWeightChange(X)
            Else
                ' Same as above but without momentum
                InputConnections.ElementAt(X).Value.Weight -= LearningRate * WeightChange(X)
            End If
        Next
        PreviousWeightChange.Clear()
        PreviousWeightChange.AddRange(WeightChange)
    End Sub

End Class
