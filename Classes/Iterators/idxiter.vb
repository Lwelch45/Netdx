Imports Netdx
Imports Netdx.DxError

Public Class idxiter
    Public DataO As Integer 'Offset to Data for use with idx.ptr
    Protected i As Integer
    Protected n As Integer
    Protected j As Integer
    Protected d As Integer() = New Integer(8) {}
    Sub New()
    End Sub

    Public Function init(Of T)(ByRef itera As idx(Of T)) As Integer

        i = 0
        j = itera.Spec.ndim
        DataO = DirectCast(itera.Spec.Offset, Object)
        n = itera.Spec.Nelements
        If itera.Spec.contiguousp Then
            d(0) = -1
        Else
            For ii = 0 To itera.Spec.ndim - 1
                d(ii) = 0
            Next
        End If
        Return DataO
    End Function

    Public Function [next](Of T)(ByRef m As idx(Of T)) As Integer
        i += 1
        If (d(0) < 0) Then
            'Contiguous idx
            DataO += 1
        Else
            Do While (j < m.Spec.dim(j))
                If (j < 0) Then Exit Do

                If d(j) + 1 < m.Spec.dim(j) Then
                    DataO += m.Spec.mod(j)
                    j += 1
                Else
                    DataO -= m.Spec.mod(j) * m.Spec.dim(j)
                    j -= 1
                    d(j) = -1
                End If
            Loop
        End If
        Return DataO
    End Function

    Public Function notdone() As Boolean
        Return i < n
    End Function
End Class
