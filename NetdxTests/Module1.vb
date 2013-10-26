Imports Netdx
Imports Netdx.Iterators
Module Module1

    Sub Main()
        'TestidxGet()
        'TestStorage()
        'TestIdxspec()
        'TestidxSelect()
        'TestebLoop()
        'Testaloop
        'TestRandom
        Dim Value As Integer = Val("&H" & "7fffffff")
        Print(Value)
        Print(idxIO.MAGIC_FLOAT_MATRIX)
        Console.Read
    End Sub
    Sub TestRandom()
        Dim r As New RandomGen.MersenneTwister
        For x = 0 To 99
            'Print(r.Next(0.1, 0.9))
            Print(r.NextDoublePositive)
            'Print(0.1 <= r.Next(0.1, 0.9) <= 0.9)
        Next
    End Sub

    Sub Testaloop()
        Dim i = New idxiter
        Dim T2 As New idx(Of Double)(2, 2)
        Dim T3 As New idx(Of Double)(2, 2, 2)
        Dim qq = New idx(Of Double)(4, 5)
        Dim m = New idx(Of Double)(3, 4)
        Dim p = New idx(Of Double)(4, 3)

        Print(T2)
        Print(T3)
        Print(qq)
        Print(m)
        Print(p)

        i.init(T2)
        While i.notdone
            Print(i.next(T2))
        End While

        i.init(T3)
        While i.notdone
            Print(i.next(T3))
        End While

        'Fill M
        'Very bad way use aloop
        For i0 = 0 To 2
            For i1 = 0 To 3
                m.Set(10 * i0 + i1, i0, i1)
            Next
        Next

        Dim ii As Integer = 0
        idx_aloop1(qq, Sub(d, src)
                           ii += 1
                           src.Ptr(d.DataO) = 2 * ii
                       End Sub)

        ii = 0
        idx_aloop1(qq, Sub(d, src)
                           ii += 1
                           Print(src.Ptr(d.DataO) = 2 * ii)
                       End Sub)

        idx_aloop2(p, m, Sub(p1, src1, m1, src2)
                             src1.Ptr(p1.DataO) = src2.Ptr(m1.DataO)
                         End Sub)

        idx_aloop1(p, Sub(it1, src)
                          Print(src.Ptr(it1.DataO) = m.Ptr(it1.DataO))
                      End Sub)
    End Sub

    Sub TestebLoop()
        Console.WriteLine("Testing eloop and bloop")
        Dim m As New idx(Of Double)(3, 4)
        Dim p As New idx(Of Double)(3, 5)

        Dim i As Integer = 0
        idx_bloop1(m, Sub(lm, mm)
                          idx_bloop1(lm, Sub(llm, lmm)
                                             i += 1
                                             llm.Set(i)
                                         End Sub)
                      End Sub)
        i = 0
        idx_bloop1(m, Sub(lm, mm)
                          idx_bloop1(lm, Sub(llm, lmm)
                                             i += 1
                                             Print(llm.Get = i)
                                         End Sub)
                      End Sub)

        Dim i8 As New idx(Of Double)(2, 2, 2, 2, 2, 2, 2, 2)
        Dim i8_1 = i8.flat
        Dim cnt As Int16 = 0
        idx_bloop1(i8_1, Sub(ii, iii)
                             cnt += 1
                         End Sub)
        Print(cnt = 256)
        Console.WriteLine()
    End Sub

    Sub TestidxSelect()
        Console.WriteLine("Testing Idx-Select")
        Dim ma = New idx(Of Double)(3, 10, 20)
        Dim ms = ma.Select(0, 1)
        Print(ms.Order = 2)
        Print(ms.Offset = 200)
        Print(ms.contiguousp = True)
        Print(ms.dim(0) = 10)
        Print(ms.dim(1) = 20)
        Print(ms.mod(0) = 20)
        Print(ms.mod(1) = 1)
        Console.WriteLine()
    End Sub

    Sub TestidxGet()
        Console.WriteLine("Testing Idx-Get")
        Dim T0 As New idx(Of Double)()
        Dim T1 As New idx(Of Double)(2)
        Dim T2 As New idx(Of Double)(2, 2)
        Dim T3 As New idx(Of Double)(2, 2, 2)
        Dim T4 As New idx(Of Double)(2, 2, 2, 2)
        Dim T5 As New idx(Of Double)(2, 2, 2, 2, 2)
        Dim T6 As New idx(Of Double)(2, 2, 2, 2, 2, 2)
        Dim T7 As New idx(Of Double)(2, 2, 2, 2, 2, 2, 2)
        Dim T8 As New idx(Of Double)(9, 2, 8, 4, 5, 7, 6, 3)

        T0.Set(1)
        T1.Set(1, 0)
        T2.Set(1, 0, 1)
        T3.Set(1, 0, 1, 0)
        T4.Set(1, 0, 1, 0, 1)
        T5.Set(1, 0, 1, 0, 1, 0)
        T6.Set(1, 0, 1, 0, 1, 0, 1)
        T7.Set(1, 0, 1, 0, 1, 0, 1, 0)
        T8.Set(5, 0, 1, 0, 1, 0, 1, 0, 1)

        Print(T0.Get())
        Print(T1.Get(0))
        Print(T2.Get(0, 1))
        Print(T3.Get(0, 1, 0))
        Print(T4.Get(0, 1, 0, 1))
        Print(T5.Get(0, 1, 0, 1, 0))
        Print(T6.Get(0, 1, 0, 1, 0, 1))
        Print(T7.Get(0, 1, 0, 1, 0, 1, 0))
        Print(T8.Get(0, 1, 0, 1, 0, 1, 0, 1))

        T8.Set(14645645, 1, 0, 0, 0, 0, 0, 0)
        Print(T8.ToString)
        Dim Looper As New idxlooper(Of Double)(T8, 0)
        Print(Looper.i)
        Print(Looper.dimd)
        Print(Looper.modd)

        Print(Looper.ToString)

        Do While Looper.notdone
            Print(Looper.next)
            'looper.next
        Loop
        Console.WriteLine()
    End Sub

    Sub TestStorage()
        Console.WriteLine("Testing Storage")
        Dim s = New Srg(Of Double)(10)
        Print(s.Size = 10)
        s.SetData(3, 42)
        Print(s.GetData(3) = 42)
        s.ChangeSize(8)
        Print(s.Size = 8)
        Console.WriteLine()
    End Sub

    Sub TestIdxspec()
        Console.WriteLine("Testing Idxspec")
        Dim sp = New idxspec(5, 4, 3)
        Print(sp.GetNDim = 2)
        Print(sp.GetOffset = 5)
        Print(sp.select(1, 0).Nelements = 4)
        Print(sp.select(0, 0).Nelements = 3)

        Dim spec1 = New idxspec(0, 5, 6)
        Print(spec1.GetNDim = 2)
        Print(spec1.GetOffset = 0)
        Print(spec1.select(1, 0).Nelements = 5)
        Print(spec1.select(0, 0).Nelements = 6)

        Dim spec2 = New idxspec(0, 10)
        Print(spec2.GetNDim = 1)
        Print(spec2.GetOffset = 0)
        Print(spec2.Nelements = 10)

        ' Print(spec1.select(0, 2).GetOffset)
        spec1.select_into(spec2, 0, 2)
        Print(1 = spec2.GetNDim)
        Print(spec2.Offset = 12)
        Print(spec2.Nelements = 6)

        spec1.select_into(spec2, 1, 4)
        Print(1 = spec2.GetNDim)
        Print(spec2.Offset = 4)  'This is not working properly
        Print(spec2.Nelements = 5)

        spec1.transpose_into(spec2, 0, 1)
        Print(2 = spec2.GetNDim)
        Print(0 = spec2.GetOffset)
        Print(spec2.select(1, 0).Nelements() = 6)
        Print(spec2.select(0, 0).Nelements() = 5)

        spec1.unfold_into(spec2, 1, 3, 1)
        Print(3 = spec2.GetNDim())
        Print(0 = spec2.GetOffset())
        Print(5 = spec2.select(1, 0).select(1, 0).Nelements())
        Print(4 = spec2.select(0, 0).select(1, 0).Nelements())
        Print(3 = spec2.select(0, 0).select(0, 0).Nelements())


        Console.WriteLine()
    End Sub

    Public Sub Print(Obj As Object)
        Console.WriteLine(Obj)
    End Sub
End Module
