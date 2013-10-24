Imports Netdx.Iterators
Public Class idxops
    Public Shared Sub idx_clear(Of T)(m As idx(Of T))
        idx_aloop1(m, Sub(L, src)
                          src.Ptr(L.DataO) = DirectCast(0.0, Object)
                      End Sub)
    End Sub
    Public Shared Sub idx_fill(Of T)(m As idx(Of T), V As T)
        idx_aloop1(m, Sub(L, src)
                          src.Ptr(L.DataO) = V
                      End Sub)
    End Sub
End Class
