Imports NetLearn.Global
Imports Netdx

Public MustInherit Class module_1_1(Of T)
    Inherits [module]

    Dim outdims As idxd(Of Integer)
    Public bresize, memoptimized, bmstate_input, bmstate_output As Boolean
    Dim ninputs, noutputs As Integer
    Sub New(Optional n As String = "module_1_1", Optional bresize As Boolean = True)
        MyBase.new(n)
        Me.bresize = bresize
        memoptimized = False
        bmstate_input = False
        bmstate_output = False
        ninputs = 1
        noutputs = 1
    End Sub

#Region "single propagation"
    Public Sub fprop1(ByRef [in] As idx(Of T), ByRef [out] As idx(Of T))
        eblerror("not implemented , " & Me.describe)
    End Sub
    Public Sub bprop1(ByRef [in] As state(Of T), ByRef [out] As state(Of T))
        eblerror("not implemented , " & Me.describe)
    End Sub
    Public Sub bbprop1(ByRef [in] As state(Of T), ByRef [out] As state(Of T))
        eblerror("not implemented , " & Me.describe)
    End Sub
#End Region

#Region "full state propagation"
    Public Sub fprop(ByRef [in] As state(Of T), ByRef [out] As state(Of T))
        If [in].x.Count = 0 Then eblerror("input tensor should have at least 1 input tensor")
        '''TODO Resize output orders
        For x = 0 To [in].x.Count - 1
            fprop1([in].x(x), out.x(x))
        Next
        ninputs = [in].x.Count
        noutputs = out.x.Count
    End Sub
    Public Sub bprop(ByRef [in] As state(Of T), ByRef [out] As state(Of T))
        For i = [in].dx.Count - 1 To 0 Step -1
            Dim sin As state(Of T) = [in].get_dx(i)
            Dim sout As state(Of T) = [out].get_dx(i)
            bprop1(sin, sout)
        Next
    End Sub
    Public Sub bbprop(ByRef [in] As state(Of T), ByRef [out] As state(Of T))
        For i = [in].ddx.Count - 1 To 0 Step -1
            Dim sin As state(Of T) = [in].get_ddx(i)
            Dim sout As state(Of T) = [out].get_ddx(i)
            bbprop1(sin, sout)
        Next
    End Sub
#End Region

#Region "Dumping"
    Sub fprop1_dump(ByRef [in] As idx(Of T), ByRef [out] As idx(Of T))
        fprop1([in], out)
    End Sub
    Sub fprop_dump(ByRef [in] As state(Of T), ByRef [out] As state(Of T))
        If [in].x.Count = 0 Then eblerror("input tensor should have at least 1 input tensor")
        '''TODO Resize output orders
        For x = 0 To [in].x.Count - 1
            fprop1_dump([in].x(x), out.x(x))
        Next
        ninputs = [in].x.Count
        noutputs = out.x.Count
    End Sub
#End Region

#Region "Weight manipulation"
    Sub forget(ByRef fp As forget_param_linear)

    End Sub
    Sub normalize()

    End Sub
    Sub zero_dx()

    End Sub
    Sub zero_ddx()

    End Sub
    Function replicable_order() As Integer
        Return -1
    End Function
    Function ignored1(ByRef [in] As idx(Of T), ByRef [out] As idx(Of T)) As Boolean
        If Me.enabled Then Return False
        idxops.idx_copy([in], out)
        Return True
    End Function
#End Region

#Region "Resizing"
    Public Sub resize_output_orders(ByRef [in] As state(Of T), ByRef [out] As state(Of T))
        out.resize_forward_orders([in], 1)
    End Sub
    Public Function resize_output(ByRef [in] As idx(Of T), ByRef [out] As idx(Of T), Optional ByRef d As idxd(Of Integer) = Nothing) As Boolean
        If [in].get_idxdim = out.get_idxdim Then
            outdims = out.get_idxdim
            Return False
        End If
        If Not d Is Nothing Then
            outdims = d
            If Not d.Order = out.Order Then
                out = (New idx(Of T)(d))
            ElseIf Not d = out.get_idxdim Then
                out.resize(d)
            Else
                Return False
            End If
        Else
            outdims = [in].get_idxdim
            If Not d.Order = out.Order Or Not out.contiguousp = True Then
                out = (New idx(Of T)([in].get_idxdim))
            ElseIf Not d = out.get_idxdim Then
                out.resize([in].get_idxdim)
            Else
                Return False
            End If
        End If
        Return True
    End Function
    Function optimize_fprop(ByRef [in] As state(Of T), ByRef out As state(Of T)) As Boolean
        Return True
    End Function
    Public Sub load_x(weights As idx(Of T))
        eblerror("not implemented")
    End Sub
    Public Function last_module() As module_1_1(Of T)
        Return Me
    End Function
    Public Function mstate_input() As Boolean
        Return bmstate_input
    End Function
    Public Function mstate_output() As Boolean
        Return bmstate_output
    End Function
    Public Function get_ninputs() As Integer
        Return ninputs
    End Function
    Public Function get_noutputs() As Integer
        Return noutputs
    End Function
    Public Function get_outdims() As idxd(Of Integer)
        Return outdims
    End Function
    Public Sub update_outdims([out] As idx(Of T))
        outdims = out.get_idxdim
    End Sub



#End Region
End Class

