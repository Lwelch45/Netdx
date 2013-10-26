Imports Netdx
Imports Netdx.idxops
Public Class parameter(Of T)
    Inherits state(Of T)
    Dim deltax As idx(Of T) ' Average derivatives
    Dim epsilons As idx(Of T) ' individual learning rates
    Dim ddeltax As idx(Of T) 'average second-derivatives

    Sub New(Optional init_size As Integer = 100)
        MyBase.new(init_size)
        deltax = New idx(Of T)(init_size)
        ddeltax = New idx(Of T)(init_size)
        epsilons = New idx(Of T)(init_size)
        idx_clear(deltax)
        idx_clear(epsilons)
        idx_clear(ddeltax)
        Resize_paramater(0)

    End Sub

    Sub New(param_filename As String)
        MyBase.new(1)
        deltax = New idx(Of T)(1)
        ddeltax = New idx(Of T)(1)
        epsilons = New idx(Of T)(1)
    End Sub

    Public Sub Resize_paramater(s0 As Integer)

    End Sub
End Class
