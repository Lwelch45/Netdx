Imports Netdx
Public Class parameter(Of T)
    Inherits state(Of T)
    Dim deltax As idx(Of T) ' Average derivatives
    Dim epsilons As idx(Of T) ' individual learning rates
    Dim ddeltax As idx(Of T) 'average second-derivatives


End Class
