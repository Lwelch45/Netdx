Imports Netdx.Iterators
Public Class idxops
    Public Shared Function RO(Of T)(item As T) As Object
        Return DirectCast(item, Object)
    End Function

    Public Shared Sub idx_clear(Of T)(m As idx(Of T))
        idx_aloop1(m, Sub(L, src) src.Ptr(L.DataO) = DirectCast(0.0, Object))
    End Sub

    Public Shared Sub idx_fill(Of T)(m As idx(Of T), V As T)
        idx_aloop1(m, Sub(L, src) src.Ptr(L.DataO) = V)
    End Sub

    Public Shared Sub idx_copy(Of T)(m As idx(Of T), dst As idx(Of T))
        'Loop and Copy
        If m.Order = 0 And dst.Order = 0 Then
            dst.Set(m.Get)
        Else
            'Slow by hand
            idx_aloop2(m, dst, Sub(itr0, m1, itr1, dst2) dst.Ptr(itr0.DataO) = m1.Ptr(itr0.DataO))
        End If
    End Sub
    Public Shared Function idx_copy(Of T)(src As idx(Of T))
        Dim dst = New idx(Of T)(src.get_idxdim)
        idx_copy(src, dst)
        Return dst
    End Function

    Public Shared Sub idx_minus(Of T)(m As idx(Of T), dst As idx(Of T))
        idx_aloop2(m, dst, Sub(itr0, m1, itr1, dst2) dst.Ptr(itr0.DataO) = -DirectCast(m1.Ptr(itr1.DataO), Object))
    End Sub
    Public Shared Sub idx_minus_acc(Of T)(m As idx(Of T), dst As idx(Of T))
        idx_aloop2(m, dst, Sub(itr0, m1, itr1, dst2) dst.Ptr(itr0.DataO) += -DirectCast(m1.Ptr(itr1.DataO), Object))
    End Sub

    Public Shared Sub idx_inv(Of T)(m As idx(Of T), dst As idx(Of T))
        idx_aloop2(m, dst, Sub(itr0, m1, itr1, dst2) dst.Ptr(itr0.DataO) = 1 / DirectCast(m1.Ptr(itr1.DataO), Object))
    End Sub

    Public Shared Sub idx_add(Of T)(m As idx(Of T), dst As idx(Of T))
        idx_aloop2(m, dst, Sub(itr0, m1, itr1, dst2) dst.Ptr(itr0.DataO) = DirectCast(m1.Ptr(itr1.DataO), Object) + DirectCast(dst.Ptr(itr0.DataO), Object))
    End Sub
    Public Shared Sub idx_add(Of T)(m As idx(Of T), n As idx(Of T), dst As idx(Of T))
        idx_aloop3(m, n, dst, Sub(itr0, m1, ii, n1, itr1, dst2) dst.Ptr(itr0.DataO) = DirectCast(m1.Ptr(itr1.DataO), Object) + DirectCast(n1.Ptr(ii.DataO), Object))
    End Sub

    Public Shared Sub idx_sub(Of T)(m As idx(Of T), dst As idx(Of T))
        idx_aloop2(m, dst, Sub(itr0, m1, itr1, dst2) m.Ptr(itr0.DataO) = DirectCast(m1.Ptr(itr1.DataO), Object) - DirectCast(dst.Ptr(itr0.DataO), Object))
    End Sub
    Public Shared Sub idx_sub(Of T)(m As idx(Of T), n As idx(Of T), dst As idx(Of T))
        idx_aloop3(m, n, dst, Sub(itr0, m1, ii, n1, itr1, dst2) dst.Ptr(itr0.DataO) = DirectCast(m1.Ptr(itr1.DataO), Object) - DirectCast(n1.Ptr(ii.DataO), Object))
    End Sub
    Public Shared Sub idx_subacc(Of T)(m As idx(Of T), n As idx(Of T), dst As idx(Of T))
        idx_aloop3(m, n, dst, Sub(itr0, m1, ii, n1, itr1, dst2) dst.Ptr(itr0.DataO) += DirectCast(m1.Ptr(itr1.DataO), Object) - DirectCast(n1.Ptr(ii.DataO), Object))
    End Sub

    Public Shared Sub idx_mul(Of T)(m As idx(Of T), n As idx(Of T), dst As idx(Of T))
        idx_aloop3(m, n, dst, Sub(itr0, m1, ii, n1, itr1, dst2) dst.Ptr(itr0.DataO) = DirectCast(m1.Ptr(itr1.DataO), Object) * DirectCast(n1.Ptr(ii.DataO), Object))
    End Sub
    Public Shared Sub idx_mulacc(Of T)(m As idx(Of T), n As idx(Of T), dst As idx(Of T))
        idx_aloop3(m, n, dst, Sub(itr0, m1, ii, n1, itr1, dst2) dst.Ptr(itr0.DataO) += DirectCast(m1.Ptr(itr1.DataO), Object) * DirectCast(n1.Ptr(ii.DataO), Object))
    End Sub

    Public Shared Sub idx_div(Of T)(m As idx(Of T), n As idx(Of T), dst As idx(Of T))
        idx_aloop3(m, n, dst, Sub(itr0, m1, ii, n1, itr1, dst2)
                                  Try
                                      If DirectCast(n1.Ptr(ii.DataO), Object) = 0 Then
                                          Throw New Exception("Cannot divide by 0")
                                      End If
                                      dst.Ptr(itr0.DataO) = DirectCast(m1.Ptr(itr1.DataO), Object) / DirectCast(n1.Ptr(ii.DataO), Object)
                                  Catch ex As Exception
                                      Console.WriteLine(ex.ToString)
                                  End Try
                              End Sub)
    End Sub

    Public Shared Sub idx_addc(Of T)(m As idx(Of T), V As T)
        idx_aloop1(m, Sub(L, src) src.Ptr(L.DataO) += DirectCast(V, Object))
    End Sub
    Public Shared Sub idx_addc(Of T)(m As idx(Of T), V As T, dst As idx(Of T))
        idx_aloop2(m, dst, Sub(itr0, m1, itr1, dst2) dst.Ptr(itr0.DataO) = DirectCast(m1.Ptr(itr1.DataO), Object) + DirectCast(V, Object))
    End Sub
    Public Shared Sub idx_addcacc(Of T)(m As idx(Of T), V As T, dst As idx(Of T))
        idx_aloop2(m, dst, Sub(itr0, m1, itr1, dst2) dst.Ptr(itr0.DataO) += DirectCast(m1.Ptr(itr1.DataO), Object) + DirectCast(V, Object))
    End Sub

    Public Shared Sub idx_dotc(Of T, T2)(m As idx(Of T), V As T2, dst As idx(Of T))
        idx_aloop2(m, dst, Sub(itr0, m1, itr1, dst2) dst.Ptr(itr0.DataO) = RO(m1.Ptr(itr1.DataO)) * RO(V))
    End Sub
    Public Shared Sub idx_dotcacc(Of T, T2)(m As idx(Of T), V As T2, dst As idx(Of T))
        idx_aloop2(m, dst, Sub(itr0, m1, itr1, dst2) dst.Ptr(itr0.DataO) += RO(m1.Ptr(itr1.DataO)) * RO(V))
    End Sub

    Public Shared Sub idx_signdotc(Of T, T2)(m As idx(Of T), V As T2, dst As idx(Of T))
        idx_aloop2(m, dst, Sub(itr0, m1, itr1, dst2) dst.Ptr(itr0.DataO) = IIf(RO(m1.Ptr(itr1.DataO)) < 0, -RO(V), RO(V)))
    End Sub
    Public Shared Sub idx_signdotcacc(Of T, T2)(m As idx(Of T), V As T2, dst As idx(Of T))
        idx_aloop2(m, dst, Sub(itr0, m1, itr1, dst2) dst.Ptr(itr0.DataO) += IIf(RO(m1.Ptr(itr1.DataO)) < 0, -RO(V), RO(V)))
    End Sub

    Public Shared Sub idx_subsquare(Of T)(m As idx(Of T), n As idx(Of T), dst As idx(Of T))
        idx_aloop3(m, n, dst, Sub(itr0, m1, ii, n1, itr1, dst2)
                                  Dim D As T = RO(m1.Ptr(itr1.DataO)) + RO(n1.Ptr(ii.DataO))
                                  dst.Ptr(itr0.DataO) = RO(D) * RO(D)
                              End Sub)
    End Sub
    Public Shared Sub idx_subsquareacc(Of T)(m As idx(Of T), n As idx(Of T), dst As idx(Of T))
        idx_aloop3(m, n, dst, Sub(itr0, m1, ii, n1, itr1, dst2)
                                  Dim D As T = RO(m1.Ptr(itr1.DataO)) + RO(n1.Ptr(ii.DataO))
                                  dst.Ptr(itr0.DataO) += RO(D) * RO(D)
                              End Sub)
    End Sub


    Public Shared Sub idx_m2dotm1(Of T)(i1 As idx(Of T), i2 As idx(Of T), o1 As idx(Of T))
        Dim C1_m1 = i1.mod(1) : Dim c2_m0 = i2.mod(0)
        Dim jmax = i2.dim(0)
        Dim imax = o1.dim(0)
        Dim c1_m0 = i1.mod(0) : Dim d1_m0 = o1.mod(0)

        Dim f As T ' = RO(0)
        Dim C1_0 As Integer = i1.Offset
        Dim ker As Integer = i2.Offset
        Dim d1 As Integer = o1.Offset + 1

        For i = 0 To imax - 1
            f = RO(0)
            Dim c1 = C1_0
            Dim c2 = ker
            If ((C1_m1 = 1) And (c2_m0 = 1)) Then
                For j = 0 To jmax - 1
                    c1 += 1 : c2 += 1
                    f += (RO(i1.Ptr(c1)) * RO(i2.Ptr(c2)))
                Next
            Else
                For j = 0 To jmax - 1
                    f += (RO(i1.Ptr(c1)) * RO(i2.Ptr(c2)))
                    c1 += C1_m1 : c2 += c2_m0
                Next
            End If
            o1.Ptr(d1) = RO(f)
            d1 += d1_m0
            C1_0 += c1_m0
        Next
    End Sub
    Public Shared Sub idx_m2dotm1acc(Of T)(i1 As idx(Of T), i2 As idx(Of T), o1 As idx(Of T))
        Dim C1_m1 = i1.mod(1) : Dim c2_m0 = i2.mod(0)
        Dim jmax = i2.dim(0)
        Dim imax = o1.dim(0)
        Dim c1_m0 = i1.mod(0) : Dim d1_m0 = o1.mod(0)

        Dim f As T
        Dim C1_0 As Integer = i1.Offset
        Dim ker As Integer = i2.Offset
        Dim d1 As Integer = o1.Offset + 1

        For i = 0 To imax - 1
            f = RO(d1)
            Dim c1 = C1_0
            Dim c2 = ker
            If ((C1_m1 = 1) And (c2_m0 = 1)) Then
                For j = 0 To jmax - 1
                    c1 += 1 : c2 += 1
                    f += (RO(i1.Ptr(c1)) * RO(i2.Ptr(c2)))
                Next
            Else
                For j = 0 To jmax - 1
                    f += (RO(i1.Ptr(c1)) * RO(i2.Ptr(c2)))
                    c1 += C1_m1 : c2 += c2_m0
                Next
            End If
            o1.Ptr(d1) = RO(f)
            d1 += d1_m0
            C1_0 += c1_m0
        Next
    End Sub
    Public Shared Sub idx_m2dotm3(Of T)(i1 As idx(Of T), i2 As idx(Of T), o1 As idx(Of T))
        idx_bloop2(i2, o1, Sub(itr2, src2, itr1, src1)
                               idx_bloop2(itr2, itr1, Sub(itr21, itr11, src22, src11)
                                                          idx_m2dotm1(i1, itr21, src11)
                                                      End Sub)
                           End Sub)

    End Sub

    Public Shared Function Concat(Of T)(m1 As idx(Of T), m2 As idx(Of T), Optional [dim] As Integer = 0)
        Dim d = New idxd(Of Integer)(m1)

        d.SetDim([dim], m1.dim([dim]) + m2.dim([dim]))
        Dim m3 As New idx(Of T)(d)
        Dim temp = m3.narrow([dim], m1.dim([dim]), 0)
        idx_copy(m1, temp)
        temp = m3.narrow([dim], m2.dim([dim]), m1.dim([dim]))
        idx_copy(m2, temp)
        m3.Storage = temp.Storage
        Return m3
    End Function

    Public Shared Sub lin_comb(Of T)(m As idx(Of T), k1 As T, n As idx(Of T), k2 As T, dst As idx(Of T))
        idx_aloop3(m, n, dst, Sub(it1, m1, it2, n1, it3, dst1) dst.Ptr(it3.DataO) = RO(m1.Ptr(it1.DataO)) * k1 + RO(n1.Ptr(it2.DataO)) * k2)
    End Sub
End Class