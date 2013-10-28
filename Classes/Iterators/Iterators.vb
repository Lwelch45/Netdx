Imports System.Threading

Public Class Iterators
#Region "bloop"
    Public Shared Sub idx_bloop1(Of T)(ByRef Src As idx(Of T), Act As Action(Of idxlooper(Of T), idx(Of T)))
        Dim Loop1 As New idxlooper(Of T)(Src, 0)
        While Loop1.notdone
            Loop1.next()
            Act(Loop1, Src)
        End While
    End Sub
    Public Shared Sub idx_bloop2(Of T, T2)(ByRef Src1 As idx(Of T), ByRef Src2 As idx(Of T2),
                    Act As Action(Of idxlooper(Of T), idx(Of T), idxlooper(Of T2), idx(Of T2)))
        Dim Loop1 As New idxlooper(Of T)(Src1, 0)
        Dim Loop2 As New idxlooper(Of T2)(Src2, 0)
        While Loop1.notdone
            Loop1.next() : Loop2.next()
            Act(Loop1, Src1, Loop2, Src2)
        End While
    End Sub
    Public Shared Sub idx_bloop3(Of T, T2, T3)(ByRef Src1 As idx(Of T), ByRef Src2 As idx(Of T2), ByRef Src3 As idx(Of T3),
                 Act As Action(Of idxlooper(Of T), idx(Of T), idxlooper(Of T2), idx(Of T2), idxlooper(Of T3), idx(Of T3)))
        Dim Loop1 As New idxlooper(Of T)(Src1, 0)
        Dim Loop2 As New idxlooper(Of T2)(Src2, 0)
        Dim Loop3 As New idxlooper(Of T3)(Src3, 0)
        While Loop1.notdone
            Loop1.next() : Loop2.next() : Loop3.next()
            Act(Loop1, Src1, Loop2, Src2, Loop3, Src3)
        End While
    End Sub
    Public Shared Sub idx_bloop4(Of T, T2, T3, T4)(ByRef Src1 As idx(Of T), ByRef Src2 As idx(Of T2),
                                                   ByRef Src3 As idx(Of T3), ByRef Src4 As idx(Of T4),
                    Act As Action(Of idxlooper(Of T), idx(Of T), idxlooper(Of T2), idx(Of T2), 
                                  idxlooper(Of T3), idx(Of T3), idxlooper(Of T4), idx(Of T4)))
        Dim Loop1 As New idxlooper(Of T)(Src1, 0) : Dim Loop2 As New idxlooper(Of T2)(Src2, 0)
        Dim Loop3 As New idxlooper(Of T3)(Src3, 0) : Dim Loop4 As New idxlooper(Of T4)(Src4, 0)
        While Loop1.notdone
            Loop1.next() : Loop2.next() : Loop3.next() : Loop4.next()
            Act(Loop1, Src1, Loop2, Src2, Loop3, Src3, Loop4, Src4)
        End While
    End Sub
#End Region

#Region "eloop"
    Public Shared Sub idx_eloop1(Of T)(Src As idx(Of T), Act As Action(Of idxlooper(Of T), idx(Of T)))
        Dim Loop1 As New idxlooper(Of T)(Src, Src.Order - 1)
        While Loop1.notdone
            Loop1.next()
            Act(Loop1, Src)
        End While
    End Sub
    Public Shared Sub idx_eloop2(Of T, T2)(Src1 As idx(Of T), Src2 As idx(Of T2),
                                       Act As Action(Of idxlooper(Of T), idx(Of T), idxlooper(Of T2), idx(Of T2)))
        Dim Loop1 As New idxlooper(Of T)(Src1, Src1.Order - 1)
        Dim Loop2 As New idxlooper(Of T2)(Src2, Src2.Order - 1)
        While Loop1.notdone
            Loop1.next() : Loop2.next()
            Act(Loop1, Src1, Loop2, Src2)
        End While
    End Sub
    Public Shared Sub idx_eloop3(Of T, T2, T3)(ByRef Src1 As idx(Of T), ByRef Src2 As idx(Of T2), ByRef Src3 As idx(Of T3),
                     Act As Action(Of idxlooper(Of T), idx(Of T), idxlooper(Of T2), idx(Of T2), idxlooper(Of T3), idx(Of T3)))
        Dim Loop1 As New idxlooper(Of T)(Src1, Src1.Order - 1)
        Dim Loop2 As New idxlooper(Of T2)(Src2, Src2.Order - 1)
        Dim Loop3 As New idxlooper(Of T3)(Src3, Src3.Order - 1)
        While Loop1.notdone
            Loop1.next() : Loop2.next() : Loop3.next()
            Act(Loop1, Src1, Loop2, Src2, Loop3, Src3)
        End While
    End Sub
    Public Shared Sub idx_eloop4(Of T, T2, T3, T4)(ByRef Src1 As idx(Of T), ByRef Src2 As idx(Of T2),
                                                   ByRef Src3 As idx(Of T3), ByRef Src4 As idx(Of T4),
                    Act As Action(Of idxlooper(Of T), idx(Of T), idxlooper(Of T2), idx(Of T2), 
                                  idxlooper(Of T3), idx(Of T3), idxlooper(Of T4), idx(Of T4)))
        Dim Loop1 As New idxlooper(Of T)(Src1, Src1.Order - 1) : Dim Loop2 As New idxlooper(Of T2)(Src2, Src2.Order - 1)
        Dim Loop3 As New idxlooper(Of T3)(Src3, Src3.Order - 1) : Dim Loop4 As New idxlooper(Of T4)(Src4, Src4.Order - 1)
        While Loop1.notdone
            Loop1.next() : Loop2.next() : Loop3.next() : Loop4.next()
            Act(Loop1, Src1, Loop2, Src2, Loop3, Src3, Loop4, Src4)
        End While
    End Sub
#End Region

#Region "aloop_on"
    Public Shared Sub idx_aloop1_on(Of T)(ByRef itr0 As idxiter, ByRef Src0 As idx(Of T), Act As Action(Of idxiter, idx(Of T)))
        itr0.init(Src0) ' : If itr0.notdone Then Act(itr0, Src0)
        Do While itr0.notdone
            itr0.next(Src0)
            Act(itr0, Src0)
        Loop
    End Sub
#End Region

#Region "aloop"
    Public Shared Sub idx_aloop1(Of T)(ByRef Src0 As idx(Of T), Act As Action(Of idxiter, idx(Of T)))
        idx_aloop1_on(New idxiter, Src0, Act)
    End Sub
    Public Shared Sub idx_aloop2(Of T, T2)(ByRef Src0 As idx(Of T), ByRef Src1 As idx(Of T2), Act As Action(Of idxiter, idx(Of T), idxiter, idx(Of T2)))
        Dim itr0 = New idxiter
        Dim itr1 = New idxiter
        itr0.init(Src0) : itr1.init(Src1)
        '   If itr0.notdone Then Act(itr0, Src0, itr1, Src1)
        While itr0.notdone
            itr0.next(Src0) : itr1.next(Src1)
            Act(itr0, Src0, itr1, Src1)
        End While
    End Sub
    Public Shared Sub idx_aloop3(Of T, T2, T3)(ByRef Src0 As idx(Of T), ByRef Src1 As idx(Of T2), ByRef Src2 As idx(Of T3),
                                               Act As Action(Of idxiter, idx(Of T), idxiter, idx(Of T2), idxiter, idx(Of T3)))
        Dim itr0 = New idxiter
        Dim itr1 = New idxiter
        Dim itr2 = New idxiter
        itr0.init(Src0) : itr1.init(Src1) : itr2.init(Src2)
        'If itr0.notdone Then Act(itr0, Src0, itr1, Src1, itr2, Src2)
        While itr0.notdone
            itr0.next(Src0) : itr1.next(Src1) : itr2.next(Src2)
            Act(itr0, Src0, itr1, Src1, itr2, Src2)
        End While
    End Sub
#End Region
End Class
