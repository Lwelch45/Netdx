Imports Netdx

'A trainable set of parameter.
Public Class dparameter(Of T)
    Inherits parameter(Of T)

    Sub New(Optional initial_size As Integer = 100)
        'initialize backward tensors
        MyBase.New(initial_size)
        Me.dx.Add(New idx(Of T)(Me.get_idxdim))
        Me.Resize_paramater(0)
    End Sub

    Sub New(filename As String)
        MyBase.new(filename)

        Me.dx.Add(New idx(Of T)(Me.get_idxdim))
        forward_only = True
    End Sub
End Class
