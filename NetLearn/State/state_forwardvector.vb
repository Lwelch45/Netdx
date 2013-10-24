Imports Netdx
Public Class state_forwardvector(Of T)
    Inherits svector(Of T)
    Dim parent As T

    Sub New()
        MyBase.new()
        parent = Nothing
    End Sub

    Sub New(other As state_forwardvector(Of T))
        Me.L = other.L
    End Sub

    Sub New(n As Integer)
        MyBase.New(n)
        parent = Nothing
    End Sub

    Overrides Sub Clear()
        If Not parent Is Nothing Then
            For i = 1 To Me.Count - 1
                RemoveAt(i)
            Next
        Else
            For i = 0 To Me.Count - 1
                RemoveAt(i)
            Next
        End If
        
    End Sub

    Shadows Sub Clear(n As Integer)
        RemoveAt(n)
    End Sub

End Class
