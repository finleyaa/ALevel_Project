Public Class Link

    Public Weight As Double
    Public Delta As Double

    Public Sub New(Rnd As Random)

        Weight = Rnd.Next(-1000, 1001) / 1000

    End Sub

End Class
