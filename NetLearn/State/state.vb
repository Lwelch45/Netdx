Imports Netdx
Imports Netdx.idxops

Public Class state(Of T)
    Inherits idx(Of T)

    Protected Friend x As state_forwardvector(Of idx(Of T)) ' Forward tensors
    Protected Friend dx As svector(Of idx(Of T)) ' Backward tensors
    Protected Friend ddx As svector(Of idx(Of T)) ' Second order backward tensors
    Public forward_only As Boolean
#Region "Constructor"

    Sub New()
        MyBase.new()
        init()
    End Sub
    Sub New(s0 As Integer, Optional P As parameter(Of T) = Nothing)
        MyBase.new()
        init()
        Dim d As New idxd(Of Integer)(s0)
        allocate(d, P)
    End Sub
    Sub New(s0 As Integer, s1 As Integer, Optional p As parameter(Of T) = Nothing)
        MyBase.new()
        init()
        Dim d As New idxd(Of Integer)(s0, s1)
        allocate(d, p)
    End Sub
    Sub New(s0 As Integer, s1 As Integer, s2 As Integer, Optional p As parameter(Of T) = Nothing)
        MyBase.new()
        init()
        Dim d As New idxd(Of Integer)(s0, s1, s2)
        allocate(d, p)
    End Sub
    Sub New(s0 As Integer, s1 As Integer, s2 As Integer, s3 As Integer, Optional p As parameter(Of T) = Nothing)
        MyBase.new()
        init()
        Dim d As New idxd(Of Integer)(s0, s1, s2, s3)
        allocate(d, p)
    End Sub
    Sub New(s0 As Integer, s1 As Integer, s2 As Integer, s3 As Integer, s4 As Integer, s5 As Integer, s6 As Integer, s7 As Integer, Optional p As parameter(Of T) = Nothing)
        MyBase.new()
        init()
        Dim d As New idxd(Of Integer)(s0, s1, s2, s3, s4, s5, s6, s7)
        allocate(d, p)
    End Sub
    Sub New(d As idxd(Of Integer), Optional p As parameter(Of T) = Nothing)
        MyBase.New()
        init()
        allocate(d, p)
    End Sub
    Sub New(s As state(Of T))
        init()
        forward_only = s.forward_only
        x.Clear()
        ' forward
        For i As UInteger = 0 To s.x.Count - 1
            s.x(i) = New idx(Of T)(s.x(i))
        Next
        ' backward
        For i As UInteger = 0 To s.dx.Count - 1
            s.dx(i) = New idx(Of T)(s.dx(i))
        Next
        ' bbackward
        For i As UInteger = 0 To s.ddx.Count() - 1
            s.ddx(i) = New idx(Of T)(s.ddx(i))
        Next
    End Sub
    Sub New(s As state(Of T), dim_size As Integer, n As Integer)
        MyBase.new()
        init()
        forward_only = s.forward_only
        reset_tensors(s.x, x, dim_size, n)
        If Not s.dx.Count = 0 Then reset_tensors(s.dx, dx, dim_size, n)
        If Not s.ddx.Count = 0 Then reset_tensors(s.ddx, ddx, dim_size, n)
        link_f0()
        zero_all
    End Sub
    Sub New(ft As idx(Of T))
        init()
        x.Clear()
        add_x(ft)
    End Sub
    Sub New(ft As idx(Of T), bt As idx(Of T))
        init()
        x.Clear()
        add_x(ft)
        dx.Add(bt)
    End Sub
    Sub New(ft As idx(Of T), dxt As idx(Of T), ddxt As idx(Of T))
        init()
        x.Clear()
        add_x(ft)
        dx.Add(dxt)
        ddx.Add(ddxt)
    End Sub
#End Region



    Sub allocate(D As idxd(Of Integer), ByRef P As parameter(Of T))
        If Not P Is Nothing Then
            Me.Storage = P.GetStorage
            Me.Spec = P.Spec
        Else
            Me.Storage = New Srg(Of T)
        End If
        'allocate dx and ddx tensors if present in p
        If Not P Is Nothing Then
            If P.dx.Count > 0 Then
                dx.Add(New idx(Of T)(P.dx(0).GetStorage(), P.dx(0).FootPrint(), D))
            End If
            If P.ddx.Count > 0 Then
                ddx.Add(New idx(Of T)(P.ddx(0).GetStorage(), P.ddx(0).FootPrint(), D))
            End If
            P.Resize_paramater(P.FootPrint + Nelements())
        End If

    End Sub

    Sub init()
        forward_only = False
        x.parent = Me
        x.Add(Me)
    End Sub

    Sub set_forward_only()
        forward_only = True
    End Sub

    Sub zero_all()
        zero_x()
        zero_dx()
        zero_ddx()
    End Sub
    Sub zero_x()
        For Each id In x
            idx_clear(id)
        Next
    End Sub
    Sub zero_dx()
        For Each id In dx
            idx_clear(id)
        Next
    End Sub
    Sub zero_ddx()
        For Each id In ddx
            idx_clear(id)
        Next
    End Sub
    Sub clear_all()
        x.Clear()
        dx.Clear()
        ddx.Clear()
    End Sub

    Public Function get_x(i As Integer) As state(Of T)
        Return New state(Of T)(x(i)) With {.forward_only = forward_only}
    End Function
    Public Function get_dx(i As Integer) As state(Of T)
        Return New state(Of T)(x(i), dx(i)) With {.forward_only = forward_only}
    End Function
    Public Function get_ddx(i As Integer) As state(Of T)
        Return New state(Of T)(x(i), dx(i), ddx(i)) With {.forward_only = forward_only}
    End Function

    Sub add_x(m As idx(Of T))
        x.Add(m)
        If x.Count = 1 Then link_f0()
    End Sub
    Sub add_x(m As svector(Of idx(Of T)))
        For Each item In m
            add_x(item)
        Next
    End Sub

    Sub link_f0()
        If Not x(0) Is Me Then
            link_main_to_f0()
        End If
    End Sub
    Sub link_main_to_f0()
        x.Item(0) = Me 'f0is a reference to the main tensor
    End Sub

    Sub resize_dx()
        resize_vidx_as(x, dx)
    End Sub
    Sub resize_ddx()
        resize_vidx_as(x, ddx)
    End Sub
    Sub resize_vidx_as([in] As svector(Of idx(Of T)), ByRef out As svector(Of idx(Of T)))
        Dim reset = False : Dim resize = False
        If [in].Count <> out.Count Then reset = True

        If [in].Count = out.Count Then
            For i = 0 To [in].Count - 1
                If [in](i).Order <> out(i).Order Then reset = True
                For d = 0 To [in](i).dims.Count - 1
                    If [in](i).dim(d) <> out(i).dim(d) Then resize = True
                Next
            Next
        End If
        If reset Then
            reset_tensors([in], out)
        ElseIf resize Then
            For i = 0 To [in].Count - 1
                out(i).resize([in](i).get_idxdim)
            Next
        End If
    End Sub
    Sub resize_vidx_orders([in] As svector(Of idx(Of T)), ByRef out As svector(Of idx(Of T)), Optional dim_size As Integer = 0, Optional n As Integer = 0)
        Dim reset = False
        Try
            If n < 0 Then n = [in].Count
            If [in].Count > n And out.Count < n Then reset = True
            If [in].Count <= n And Not out.Count = [in].Count Then reset = True
            For i = 0 To [in].Count - 1
                If Not [in](i).Order = out(i).Order Then reset = True
            Next
            If reset Then reset_tensors([in], out, dim_size, n)
        Catch ex As Exception
            Console.WriteLine(ex.ToString)
        End Try
    End Sub
    Sub reset_tensors([in] As svector(Of idx(Of T)), ByRef out As svector(Of idx(Of T)), Optional dim_size As Integer = 0, Optional n As Integer = 0)
        Try
            If n < 0 Then n = [in].Count
            Dim d As idxd(Of Integer)
            out.Clear()
            For i = 0 To n - 1
                If i < [in].Count Then
                    d = [in](i).get_idxdim
                    If dim_size > 0 Then d.SetDims(dim_size)
                End If
                out.Add(New idx(Of T)(d))
            Next
        Catch ex As Exception
            Console.WriteLine(ex.ToString)
        End Try
    End Sub
End Class
