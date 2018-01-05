' The Link class contains a weight and a delta value
' It creates a random weight value when it is initialised
Public Class Link

    Public Weight As Double ' The weight value
    Public Delta As Double ' The weight's partial derivative

    Public Sub New(Rnd As Random)

        Weight = Rnd.Next(-1000, 1001) / 1000 ' Sets the weight value to random number between -1.0 and 1.0

    End Sub

End Class
