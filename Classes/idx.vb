Public Class idx(Of T)
    Implements Iidx(Of T)

    Private _Spec As idxspec
    Public Property Spec() As idxspec Implements Iidx(Of T).Spec
        Get
            Return _Spec
        End Get
        Set(ByVal value As idxspec)
            _Spec = value
        End Set
    End Property

    Private srg As Srg(Of T)
    Public Property Storage As IStorage(Of T) Implements Iidx(Of T).Storage
        Get
            Return srg
        End Get
        Set(value As IStorage(Of T))
            srg = value
        End Set
    End Property

    Private _pidxdim As idxd(Of Integer) ' allocated when get idxdim is called
    Public Property pidxdim() As idxd(Of Integer)
        Get
            Return _pidxdim
        End Get
        Set(ByVal value As idxd(Of Integer))
            _pidxdim = value
        End Set
    End Property



    Public Property Ptr(i As Integer) As T
        Get
            Return Storage.GetData(Offset() + i)
        End Get
        Set(ByVal value As T)
            Storage.SetData(Offset() + i, value)
        End Set
    End Property


#Region "Constructor"
    Sub New(other As idx(Of T))
        Spec = other.Spec
        Storage = other.Storage
    End Sub
    Sub New(t As Boolean)
        Spec = New idxspec
    End Sub
    Sub New()
        Spec = New idxspec(0)
        Storage = New Srg(Of T)()
        GrowStorage()
    End Sub
    Sub New(size0 As Integer)
        Spec = New idxspec(0, size0)
        Storage = New Srg(Of T)
        GrowStorage
    End Sub
    Sub New(size0 As Integer, size1 As Integer)
        Spec = New idxspec(0, size0, size1)
        Storage = New Srg(Of T)
        GrowStorage()

    End Sub
    Sub New(size0 As Integer, size1 As Integer, size2 As Integer)
        Spec = New idxspec(0, size0, size1, size2)
        Storage = New Srg(Of T)
        GrowStorage()
    End Sub
    Sub New(s0 As Integer, s1 As Integer, s2 As Integer, s3 As Integer, Optional s4 As Integer = 0, Optional s5 As Integer = 0, Optional s6 As Integer = 0, Optional s7 As Integer = 0)
        Spec = New idxspec(0, s0, s1, s2, s3, s4, s5, s6, s7)
        Storage = New Srg(Of T)
        GrowStorage()
    End Sub
    'Sub New(d As idxd(Of Integer))
    '    Spec = New idxspec(0, d)
    '    Storage = New Srg(Of T)
    '    GrowStorage()
    'End Sub
    Sub New(ByRef sg As Srg(Of T), s As idxspec)
        Spec = s
        If Not sg Is Nothing Then
            Storage = sg
        Else
            Storage = New Srg(Of T)
        End If
        GrowStorage()
    End Sub
    Sub New(ByRef sg As Srg(Of T), o As Integer, n As Integer, dims As Integer(), mods As Integer())
        Spec = New idxspec(IIf(Not sg Is Nothing, o, 0), n, dims, mods)
        If Not sg Is Nothing Then
            Storage = sg
        Else
            Storage = New Srg(Of T)
        End If
        GrowStorage
    End Sub
    Sub New(sg As Srg(Of T), o As Integer)
        If Not sg Is Nothing Then
            Spec = New idxspec(o)
        Else
            Spec = New idxspec(0)
        End If

        If Not sg Is Nothing Then
            Storage = sg
        Else
            Storage = New Srg(Of T)
        End If
        GrowStorage()
    End Sub
    Sub New(sg As Srg(Of T), o As Integer, size0 As Integer)
        Spec = New idxspec(IIf(Not sg Is Nothing, o, 0), size0)
        If Not sg Is Nothing Then
            Storage = sg
        Else
            Storage = New Srg(Of T)
        End If
        GrowStorage()
    End Sub
    Sub New(sg As Srg(Of T), o As Integer, size0 As Integer, size1 As Integer)
        Spec = New idxspec(IIf(Not sg Is Nothing, o, 0), size0, size1)
        If Not sg Is Nothing Then
            Storage = sg
        Else
            Storage = New Srg(Of T)
        End If
        GrowStorage()
    End Sub
    Sub New(sg As Srg(Of T), o As Integer, size0 As Integer, size1 As Integer, size2 As Integer)
        Spec = New idxspec(IIf(Not sg Is Nothing, o, 0), size0, size1, size2)
        If Not sg Is Nothing Then
            Storage = sg
        Else
            Storage = New Srg(Of T)
        End If
        GrowStorage()
    End Sub
    Sub New(sg As Srg(Of T), o As Integer, size0 As Integer, size1 As Integer, size2 As Integer, s3 As Integer, s4 As Integer, s5 As Integer, s6 As Integer, s7 As Integer)

        Spec = New idxspec(IIf(Not sg Is Nothing, o, 0), size0, size1, size2, s3, s4, s5, s6, s7)
        If Not sg Is Nothing Then
            Storage = sg
        Else
            Storage = New Srg(Of T)
        End If
        GrowStorage()
    End Sub
#End Region

#Region "Get"
    Public Function [Get]() As T Implements Iidx(Of T).Get
        Return Storage.GetData(Spec.Offset)
    End Function

    Public Function [Get](i0 As Integer) As T Implements Iidx(Of T).Get
        Return Storage.GetData(Spec.Offset + i0 * Spec.mod(0))
    End Function

    Public Function [Get](i0 As Integer, i1 As Integer) As T Implements Iidx(Of T).Get
        Return Storage.GetData(Spec.Offset + i0 * Spec.mod(0) + i1 * Spec.mod(1))
    End Function

    Public Function [Get](i0 As Integer, i1 As Integer, i2 As Integer) As T Implements Iidx(Of T).Get
        Return Storage.GetData(Spec.Offset + (i0 * Spec.mod(0)) + (i1 * Spec.mod(1)) + (i2 * Spec.mod(2)))
    End Function

    Public Function [Get](i0 As Integer, i1 As Integer, i2 As Integer, i3 As Integer, Optional i4 As Integer = 0, Optional i5 As Integer = -1, Optional i6 As Integer = -1, Optional i7 As Integer = -1) As T Implements Iidx(Of T).Get
        Dim k As Integer = 0
        k += (i0 * Spec.mod(0)) + (i1 * Spec.mod(1)) + (i2 * Spec.mod(2))
        If Spec.ndim >= 4 Then
            k += i3 * Spec.mod(3)
        End If
        If Spec.ndim >= 5 Then
            k += i4 * Spec.mod(4)
        End If
        If Spec.ndim >= 6 Then
            k += i5 * Spec.mod(5)
        End If
        If Spec.ndim >= 7 Then
            k += i6 * Spec.mod(6)
        End If
        If Spec.ndim >= 8 Then
            k += i7 * Spec.mod(7)
        End If
        Return Storage.GetData(Spec.Offset + k)
    End Function

    Public Function GGet(Optional i0 As Integer = 0, Optional i1 As Integer = 0, Optional i2 As Integer = 0, Optional i3 As Integer = 0, Optional i4 As Integer = 0, Optional i5 As Integer = 0, Optional i6 As Integer = 0, Optional i7 As Integer = 0) As T Implements Iidx(Of T).GGet
        Return [Get](i0, i1, i2, i3, i4, i5, i6, i7)
    End Function

#End Region

#Region "Set"
    Public Function [Set](Val As T) As T Implements Iidx(Of T).Set
        Storage.SetData(Spec.Offset, Val)
        Return Val
    End Function

    Public Function [Set](Val As T, i0 As Integer) As T Implements Iidx(Of T).Set
        Storage.SetData(Spec.Offset + i0 * Spec.mod(0), Val)
        Return Val
    End Function

    Public Function [Set](Val As T, i0 As Integer, i1 As Integer) As T Implements Iidx(Of T).Set
        Storage.SetData(Spec.Offset + i0 * Spec.mod(0) + i1 * Spec.mod(1), Val)
        Return Val
    End Function

    Public Function [Set](Val As T, i0 As Integer, i1 As Integer, i2 As Integer) As T Implements Iidx(Of T).Set
        Storage.SetData(Spec.Offset + (i0 * Spec.mod(0)) + (i1 * Spec.mod(1)) + (i2 * Spec.mod(2)), Val)
        Return Val
    End Function

    Public Function [Set](Val As T, i0 As Integer, i1 As Integer, i2 As Integer, i3 As Integer, Optional i4 As Integer = 0, Optional i5 As Integer = 0, Optional i6 As Integer = 0, Optional i7 As Integer = 0) As T Implements Iidx(Of T).Set
        Dim k As Integer = 0
        k += (i0 * Spec.mod(0)) + (i1 * Spec.mod(1)) + (i2 * Spec.mod(2))
        If Spec.ndim >= 4 Then
            k += i3 * Spec.mod(3)
        End If
        If Spec.ndim >= 5 Then
            k += i4 * Spec.mod(4)
        End If
        If Spec.ndim >= 6 Then
            k += i5 * Spec.mod(5)
        End If
        If Spec.ndim >= 7 Then
            k += i6 * Spec.mod(6)
        End If
        If Spec.ndim >= 8 Then
            k += i7 * Spec.mod(7)
        End If
        Storage.SetData(Spec.Offset + k, Val)
        Return Val
    End Function

    Public Function SSet(Val As T, Optional i0 As Integer = 0, Optional i1 As Integer = 0, Optional i2 As Integer = 0, Optional i3 As Integer = 0, Optional i4 As Integer = 0, Optional i5 As Integer = 0, Optional i6 As Integer = 0, Optional i7 As Integer = 0) As T Implements Iidx(Of T).SSet
        [Set](Val, i0, i1, i2, i3, i4, i5, i6, i7)
        Return Val
    End Function

#End Region

#Region "Offset"
    Public Function SetOffset(off As Integer) As Integer Implements Iidx(Of T).SetOffset
        If off > Spec.Offset Then
            Spec.SetOffset(off)
            GrowStorage()
        Else
            Spec.SetOffset(off)
        End If
        Return Spec.GetOffset()
    End Function

    Public Sub AddOffset(off As Integer) Implements Iidx(Of T).AddOffset
        Spec.AddOffset(off)
    End Sub

    Public Function Offset() As Integer Implements Iidx(Of T).Offset
        Return Spec.Offset
    End Function
#End Region

#Region "Misc"

    Public Function [dim](d As Integer) As Integer
        Return Spec.dim(d)
    End Function

    Public Function [mod](d As Integer) As Integer
        Return Spec.mod(d)
    End Function

    Public Function mods() As Integer()
        Return Spec.mod
    End Function

    Public Function dims() As Integer()
        Return Spec.dim
    End Function

    Public Function contiguousp() As Boolean
        Return Spec.contiguousp
    End Function

    Public Function Order() As Integer
        Return Spec.ndim
    End Function

    Public Sub GrowStorage()
        Storage.ChangeSize(Spec.FootPrint)
    End Sub

    Public Sub GrowStorageChunck(ByVal C As Integer)
        Storage.ChangeSize(Spec.FootPrint + C)
    End Sub

    Public Function TrueOrder() As Integer Implements Iidx(Of T).TrueOrer
        Return Spec.TrueOrer
    End Function

    Public Function GetStorage() As IStorage(Of T) Implements Iidx(Of T).GetStorage
        Return Storage
    End Function

    Public Function Nelements() As Integer Implements Iidx(Of T).Nelements
        Return Spec.Nelements
    End Function

    Public Function FootPrint() As Integer Implements Iidx(Of T).FootPrint
        Return Spec.FootPrint
    End Function

    Public Function get_idxdim() As idxd(Of Integer)
        Dim d As New idxd(Of Integer)
        d.SetDims(Spec)
        Return d
    End Function

    Public Overrides Function ToString() As String
        Dim Str As String = ""
        Str += "idx: pointing to storage: " & srg.ID.ToString & vbNewLine
        Str += "(storage size=" & Storage.Size() & ")"
        Str += vbNewLine
        Return Str & Spec.ToString
    End Function
#End Region

#Region "Resize"
    Sub resize(s0 As Integer, s1 As Integer, s2 As Integer, s3 As Integer, s4 As Integer, s5 As Integer, s6 As Integer, s7 As Integer)
        Spec.Resize(s0, s1, s2, s3, s4, s5, s6, s7)
        GrowStorage()
    End Sub
    Sub resize(d As idxd(Of Integer))
        Spec.Resize(d)
        GrowStorage()
    End Sub
    Sub resize1(dinm As Integer, size As Integer)
        Spec.Resize1(dinm, size)
        GrowStorage()
    End Sub
    Sub resize_chunk(chunk As Integer, s0 As Integer, s1 As Integer, s2 As Integer, s3 As Integer, s4 As Integer, s5 As Integer, s6 As Integer, s7 As Integer)
        Spec.Resize(s0, s1, s2, s3, s4, s5, s6, s7)
        GrowStorageChunck(chunk)
    End Sub
#End Region

#Region "Manipulation"
    Public Function [Select](D As Integer, i As Integer) As idx(Of T)
        Dim R As New idx(Of T)(Storage, Spec.GetOffset)
        Spec.select_into(R.Spec, D, i)
        Return R
    End Function
    Public Function narrow(d As Integer, s As Integer, o As Integer) As idx(Of T)
        Dim R As New idx(Of T)(Storage, Spec.GetOffset)
        Spec.narrow_into(R.Spec, d, s, o)
        Return R
    End Function
    Public Function transpose(d1 As Integer, d2 As Integer) As idx(Of T)
        Dim R As New idx(Of T)(Storage, Spec.GetOffset)
        Spec.transpose_into(R.Spec, d1, d2)
        Return R
    End Function
    Public Function unfold(d As Integer, k As Integer, s As Integer) As idx(Of T)
        Dim R As New idx(Of T)(Storage, Spec.GetOffset)
        Spec.unfold_into(R.Spec, d, k, s)
        Return R
    End Function
    Public Function view_as_order(n As Integer) As idx(Of T)
        If n = Spec.ndim Then
            Return Me
        Else
            If (n = 1) And (Spec.ndim = 1) Then
                Return New idx(Of T)(Me)
            ElseIf n = 1 Then
                If Spec.contiguousp Then
                    Return New idx(Of T)(GetStorage, 0, Spec.Nelements)
                End If
            ElseIf n > Spec.ndim Then
                Dim ldim = New Integer(n) {}
                Dim lmod = New Integer(n) {}

                Array.Copy(Spec.dim, ldim, Spec.ndim)
                Array.Copy(Spec.mod, lmod, Spec.ndim)

                For i = Spec.ndim To n - 1
                    ldim(i) = 1
                    lmod(i) = 1
                Next
                Return New idx(Of T)(Storage, Spec.GetOffset, n, ldim, lmod)
            Else
                Return New idx(Of T)(Me)
            End If

        End If
        Return Me
    End Function
    Public Function flat() As idx(Of T)
        Return view_as_order(1)
    End Function
    'Public Function shift_dim(d As Integer, pos As Integer)
    '    Dim tr = New Integer(8) {}
    '    Dim j As Integer = 0
    '    For i = 0 To Spec.ndim - 1
    '        If i = pos Then
    '            tr(i) = d
    '        Else
    '            If j = d Then
    '                j += 1
    '            End If
    '            j += 1
    '            tr(i) = j
    '        End If
    '    Next
    '    Return transpose(tr)
    'End Function
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                'srg = Nothing
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
