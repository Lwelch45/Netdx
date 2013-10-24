Imports Netdx
Public Class state(Of T)
    Inherits idx(Of T)

    Protected Friend x As state_forwardvector(Of idx(Of T)) ' Forward tensors
    Protected Friend dx As svector(Of idx(Of T)) ' Backward tensors
    Protected Friend ddx As svector(Of idx(Of T)) ' Second order backward tensors
    Private forward_only As Boolean
End Class
