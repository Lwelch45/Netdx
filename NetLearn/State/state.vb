Imports Netdx
Public Class state(Of T)
    Inherits idx(Of T)

    Protected Friend x As state_forwardvector(Of idx(Of T)) ' Forward tensors
    Protected Friend dx As svector(Of idx(Of T)) ' Backward tensors
    Protected Friend ddx As svector(Of idx(Of T)) ' Second order backward tensors
    Private forward_only As Boolean

    Sub New()
        MyBase.new()
        init()
    End Sub

    Sub New(s0 As Integer, Optional P As parameter(Of T) = Nothing)
        init()
        Dim d As New idxd(Of Integer)(s0)
        allocate(d, P)
    End Sub





    Sub init()

    End Sub

    Sub allocate(D As idxd(Of Integer), ByRef P As parameter(Of T))

    End Sub
End Class
