Imports Netdx
Imports Netdx.idxops
Imports Netdx.idxIO
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
        set_forward_only
        idx_clear(deltax)
        idx_clear(epsilons)
        idx_clear(ddeltax)
        Resize_paramater(0)

    End Sub

    Sub New(param_filename As String)
        MyBase.new(1)
        set_forward_only
        deltax = New idx(Of T)(1)
        ddeltax = New idx(Of T)(1)
        epsilons = New idx(Of T)(1)
    End Sub

    Public Sub Resize_paramater(s0 As Integer)
        x(0).resize(s0)
        If Not dx.Count < 0 Then dx(0).resize(s0)
        If Not ddx.Count < 0 Then ddx(0).resize(s0)
        deltax.resize(s0)
        epsilons.resize(s0)
        ddeltax.resize(s0)
        idx_clear(deltax)
        idx_clear(epsilons)
        idx_clear(ddeltax)
    End Sub

    Public Function load_x(files As List(Of String)) As Boolean
        Try
            If files.Count = 0 Then Throw New Exception("was expecting at least 1 file to load")
            Dim w As idx(Of T) = load_matrix(Of T)(files(0))
            For i = 1 To files.Count - 1
                Dim tmp As idx(Of T) = load_matrix(Of T)(files(i))
                w = Concat(w, tmp)
            Next
            load_x(w)
        Catch ex As Exception
            Console.WriteLine(ex.ToString)
            Return False
        End Try
        Return True
    End Function
    Public Function load_x(File As String) As Boolean
        Try
            Dim w As idx(Of T) = load_matrix(Of T)(File)
            Resize_paramater(w.dim(0))
            idx_copy(w, Me)

        Catch ex As Exception
            Console.WriteLine(ex.ToString)
            Return False
        End Try
        Return True
    End Function
    Public Function load_x(M As idx(Of T))
        Try
            Resize_paramater(M.dim(0))
            idx_copy(M, Me)
        Catch ex As Exception
            Console.WriteLine(ex.ToString)
            Return False
        End Try
        Return True
    End Function

    Public Function save_x(File As String)
        Throw New NotImplementedException("saving not implemented yet")
    End Function

    Public Sub permute_x(ByRef blocks As List(Of Integer), ByRef permutations As List(Of Integer))
        Throw New NotImplementedException("permutation not implemented yet")
    End Sub

    Public Sub clear_deltax()
        idx_clear(deltax)
    End Sub
    Public Sub clear_ddeltax()
        idx_clear(ddeltax)
    End Sub
    Public Sub compute_epsilons(mu As T)
        idx_addc(ddeltax, mu, epsilons)
        idx_inv(epsilons, epsilons)
    End Sub
    Public Sub set_epsilon(m As T)
        idx_fill(epsilons, m)
    End Sub
    Public Sub update(arg As gd_param)
        update_weights(arg)
    End Sub

    Public Sub update_weights(arg As gd_param)
        Try
            If dx.Count = 0 Then Throw New Exception("gradient tensors not found")
            If arg.decay_l2 > 0 Then idx_dotcacc(Of T, T)(x(0), RO(arg.decay_l2), dx(0))
            If arg.decay_l2 > 0 Then idx_signdotcacc(Of T, T)(x(0), RO(arg.decay_l1), dx(0))
            If arg.inertia = 0 Then
                idx_mul(dx(0), epsilons, dx(0))
                idx_dotcacc(dx(0), -arg.eta, x(0))
            Else
                update_deltax(RO(1 - arg.inertia), RO(arg.inertia))
                idx_mul(dx(0), epsilons, dx(0))
                idx_dotcacc(dx(0), -arg.eta, x(0))
            End If
        Catch ex As Exception
            Console.WriteLine(ex.ToString)
        End Try
    End Sub
    Public Sub update_deltax(knew As T, kold As T)
        Try
            lin_comb(dx(0), knew, deltax, kold, deltax)
        Catch ex As Exception
            Console.WriteLine(ex.ToString)
        End Try
    End Sub
End Class