Imports System.IO ' For reading and writing to files

' This is the Net class, it handles the network layout and contains a list of Neurons and all the network parameters
' It controls the order of feed-forward and back-propagation procedures
Public Class Net

    Public Neurons As New List(Of Neuron) ' Collection of the network's neurons
    Public BiasValue As Double ' The bais value, 1 = bias used, 0 = no bias (defined in the .txt)
    Private Rnd As New Random ' Passed into the link initialisers
    Public LearningRate As Double ' The learning rate value (defined in the .txt)
    Public Momentum As Double ' The momentum value (defined in the .txt)
    Private InputLayerSize, OutputLayerSize As Integer ' The size of the input and output layers (defined in the .txt)
    Public HiddenLayerSize As Integer ' The size of the hidden layer (defined in the .txt). Must be Public so that the form can read and write to it

    Public Sub New(ByRef InputFile As String) ' Used when a new Net object is initialised

        Dim Reader As StreamReader
        Reader = New StreamReader(InputFile) ' Reads the specified file (network layout and parameters)
        Dim Layout As String() = Reader.ReadLine().Split(",") ' File is CSV
        InputLayerSize = Layout(0)
        HiddenLayerSize = Layout(1)
        OutputLayerSize = Layout(2)

        ' Convert.ToDouble() because parameters will be read from .txt and therefore be a string
        LearningRate = Convert.ToDouble(Reader.ReadLine())
        Momentum = Convert.ToDouble(Reader.ReadLine())
        BiasValue = Convert.ToDouble(Reader.ReadLine())
        Reader.Close()

        UpdateHiddenLayer(HiddenLayerSize) ' Create the network with the specified hidden layer size

    End Sub

    Public Function NewWeights(Weights As String()) ' Set's the weights to the specified weights

        If Weights.Length = ((InputLayerSize + 1) * HiddenLayerSize) + ((HiddenLayerSize + 1) * OutputLayerSize) Then ' If there are the correct amount of weights in the array
            Dim WeightCounter As Integer = 0 ' Which weight to use from the array

            For X = 0 To Neurons.Count - 1 ' Loop through each neuron

                Dim NewInputConnections As New Dictionary(Of Neuron, Link) ' The input connections with the new weights
                For I = 0 To Neurons(X).InputConnections.Keys.Count - 1 ' Loop through all of the neuron's input connections
                    ' Create a new link with the weight at WeightCounter
                    Dim NewLink As New Link(Rnd) With {
                        .Weight = Convert.ToDouble(Weights(WeightCounter))
                    }
                    NewInputConnections.Add(Neurons(X).InputConnections.Keys(I), NewLink) ' Add the weight and neuron to the new input connections
                    WeightCounter += 1 ' Move to the next item in the array
                Next
                Neurons(X).InputConnections = NewInputConnections ' Update the neuron's input connections
            Next
            Return True
        Else ' Otherwise, return false
            Return False
        End If

    End Function

    Public Sub UpdateHiddenLayer(NewSize As Integer) ' When the trackbar is changed on the form, this sub will be called

        HiddenLayerSize = NewSize ' Set the HiddenLayerSize to the specified size

        Neurons.Clear() ' Clear the list of Neurons

        For X = 0 To InputLayerSize + 1 + HiddenLayerSize + 1 + OutputLayerSize - 1 ' Cycle through the size of the network (+1 for bias neuron)
            Neurons.Add(New Neuron()) ' Add a neuron to the network's list
        Next

        For X = InputLayerSize + 1 To InputLayerSize + 1 + HiddenLayerSize + 1 - 2 ' Loop through all the neurons in the hidden layer and create the input connections
            For Y = 0 To InputLayerSize ' Cycle through all the input neurons
                Neurons(X).NewInputConnection(Neurons(Y), New Link(Rnd)) ' Add an input connection referring to an input neuron and a new link
            Next
        Next

        For X = InputLayerSize + 1 + HiddenLayerSize + 1 To InputLayerSize + 1 + HiddenLayerSize + 1 + OutputLayerSize - 1 ' Loop through all the neurons in the output layer and create the input connections
            For Y = InputLayerSize + 1 To InputLayerSize + 1 + HiddenLayerSize ' Cycle through all the hidden layer neurons
                Neurons(X).NewInputConnection(Neurons(Y), New Link(Rnd)) ' Create an input connection for the output neuron with a link and hidden layer neuron
            Next
        Next

    End Sub

    ' This function will train the network by taking a set of data, amount of epochs and some other parameters
    Public Function Train(TrainingData As List(Of List(Of Double)), Epochs As Integer, ReturnErrors As Boolean, Optional ByRef ProgressBar As ProgressBar = Nothing, Optional ByRef ProgressLabel As Label = Nothing) As List(Of Double)

        Dim Rnd As New Random ' Used for selecting a random piece of training data
        Dim SelectedValue As New List(Of Double) ' The currently selected training data
        Dim Result As Double ' The output of the current neural network
        Dim TrainVals As New List(Of Double) ' The values on which to train the neural network in the current iteration
        Dim Target As Double ' The network's target output (for back-propagation)
        Dim Errors As New List(Of Double) ' The training errors

        Dim StartTime As DateTime = DateTime.Now ' Used for epochs per second count

        For Num = 1 To Epochs ' Train for the specified amount of epochs

            TrainVals.Clear() ' Clear the selected training data

            SelectedValue = TrainingData(Math.Round(Rnd.Next(TrainingData.Count))) ' Choose a random piece of training data
            TrainVals.AddRange(SelectedValue) ' Add the selected data to the TrainVals
            TrainVals.RemoveAt(TrainVals.Count - 1) ' Remove the target from the training data
            Target = SelectedValue(SelectedValue.Count - 1) ' Set the target to it's value

            Result = Run(TrainVals) ' Run the neural network with the selected training data and return the output
            BackProp(Result, Target) ' Back-propagate the network using the network's target and it's actual output
            Errors.Add(Math.Abs(Result - Target)) ' Add the absolute value of the network's error to the list of errors

            If Not IsNothing(ProgressBar) Then ' If a progress bar has been passed to the function as a parameter then update it's value
                ProgressBar.Value = Num / Epochs * 100 ' % of epochs completed
            End If
            If Not IsNothing(ProgressLabel) And Num Mod 100 = 0 Then ' If a progress label has been specified then update it
                ProgressLabel.Text = "Training " & Math.Round(Num / (DateTime.Now.Subtract(StartTime).TotalSeconds)) & "/sec" ' Amount of completed epochs divided by the time taken
                ProgressLabel.Refresh() ' Refresh the label to update it on the form
            End If

        Next

        If ReturnErrors Then ' If the Function has been run to return the errors, then do so
            Return Errors
        Else ' Otherwise, return a list containing one 0
            Return New List(Of Double)({0})
        End If

    End Function

    ' This Function is used to test current network on a set of test data
    Public Function Test(TestData As List(Of List(Of Double)), Optional ByRef ProgressBar As ProgressBar = Nothing) As List(Of Double)

        Dim Results As New List(Of Double) ' A list of each test's result

        For Each Values In TestData ' Loop through each piece of data in the data set
            Results.Add(Run(Values)) ' Forward-propagate the network and add the result to the list
            If Not IsNothing(ProgressBar) Then ' If a progress bar is specified then update it
                ProgressBar.Value = (TestData.IndexOf(Values) + 1) / TestData.Count * 100 ' % of tests completed
            End If
        Next

        Return Results ' Return the list of results

    End Function

    Private Function Run(Values) As Double ' Controls the forward-propagation of the network

        For Each Neuron In Neurons ' Loop through each neuron
            Neuron.Value = 0 ' Reset the neuron's value to 0
        Next
        Neurons(InputLayerSize).Value = BiasValue ' Set the bias input neuron's value to the bias value
        Neurons(InputLayerSize + 1 + HiddenLayerSize).Value = BiasValue ' Set the bias hidden neuron's value to the bias value

        Dim Result As Double ' The network's output
        For X = 0 To InputLayerSize - 1 ' Cycle through each input neuron
            Neurons(X).Value = Values(X) ' Set the input neuron's value to the data value
        Next

        For Each Neuron In Neurons
            Neuron.FeedForward() ' Feed forward through each neuron
        Next

        Result = Neurons(Neurons.Count - 1).Value ' The network's output

        Return Result ' Return the output

    End Function

    Private Sub BackProp(ActualValue As Double, TargetValue As Double) ' Controls the network's back-propagation procedure

        Dim OutputErr As Double = ActualValue - TargetValue ' Calculate the total output error
        Dim OutputDelta As Double = Neurons(Neurons.Count - 1).Value * (1 - Neurons(Neurons.Count - 1).Value) ' Calculate the partial derivative of the output neuron's value

        For X = Neurons.Count - 1 To InputLayerSize + 1 Step -1 ' Cycle thought each neuron backwards
            If X < Neurons.Count - OutputLayerSize - 1 Then ' If the neuron is within the hidden layer then handle the back-propagation differently
                Neurons(X).CalculateBackProp(Neurons(Neurons.Count - 1), OutputErr, OutputDelta, True) ' Back-propagate through the specified hidden layer neuron
            ElseIf X >= Neurons.Count - OutputLayerSize Then ' Otherwise, treat it normally
                Neurons(X).CalculateBackProp(Neurons(Neurons.Count - 1), OutputErr, OutputDelta, False) ' Back-propagate through the specified neuron
            End If
        Next

        For Each Neuron In Neurons ' Update each neuron's input connection weights
            Neuron.UpdateWeights(LearningRate, Momentum)
        Next

    End Sub

End Class
