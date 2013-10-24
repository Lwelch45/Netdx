
Public Class forget_param

End Class

Public Class forget_param_linear
    Inherits forget_param

    Dim exp As Double
    Dim v As Double
    Dim Gen As Netdx.Random

    Sub New(v As Double, e As Double)
        Me.v = v
        exp = e
        Gen = New Netdx.Random
    End Sub

End Class
