Public Class idxd(Of T)
    Implements IExtractor(Of T)
    Private _ndim As Integer = 0
    Public Property ndim As Integer Implements IExtractor(Of T).ndim
        Get
            Return _ndim
        End Get
        Set(value As Integer)
            _ndim = value
        End Set
    End Property

    Private _dims As Integer() = New Integer() {-1, -1, -1, -1, -1, -1, -1, -1}
    Public Property dims As Integer() Implements IExtractor(Of T).dims
        Get
            Return _dims
        End Get
        Set(value As Integer())
            _dims = value
        End Set
    End Property

    Private _offsets as T()
    Public Property Offsets As T() Implements IExtractor(Of T).Offsets
        Get
            Return _offsets
        End Get
        Set(value As T())
            _offsets = value
        End Set
    End Property

#Region "Constructor"
    Sub New()
        Offsets = Nothing
        ndim = -1
        dims = New Integer() {-1, -1, -1, -1, -1, -1, -1, -1}
    End Sub
    Sub New(i As idx(Of Object))
        Offsets = Nothing
        SetDims(i.Spec)
    End Sub
    Sub New(o As Object)
        Offsets = Nothing
        SetDims(o.Spec)
    End Sub
    Sub New(i As idxd(Of Object))
        Offsets = Nothing
        SetDims(i)
    End Sub
    Sub New(spec As idxspec)
        Offsets = Nothing
        SetDims(spec)
    End Sub
    Sub New(i As idxd(Of T))
        Offsets = Nothing
        SetDims(i)
    End Sub
    Sub New(s0 As Integer, Optional s1 As Integer = -1, Optional s2 As Integer = -1, Optional s3 As Integer = -1,
            Optional s4 As Integer = -1, Optional s5 As Integer = -1, Optional s6 As Integer = -1, Optional s7 As Integer = -1)
        dims(0) = s0
        dims(1) = s1
        dims(2) = s2
        dims(3) = s3
        dims(4) = s4
        dims(5) = s5
        dims(6) = s6
        dims(7) = s7
        ndim = 0
        For i = 0 To 7
            If (dims(i) >= 0) Then ndim += 1
        Next
    End Sub
#End Region

#Region "Dims"
    Public Function [Dim](d As Integer) As T Implements IExtractor(Of T).Dim
        Return DirectCast(dims(d), Object)
    End Function

    Public Sub SetDim(dimn As Integer, size As T) Implements IExtractor(Of T).SetDim
        dims(dimn) = DirectCast(size, Object)
    End Sub

    Public Sub SetDims(index As IDescription) Implements IExtractor(Of T).SetDims
        ndim = index.ndim
        dims = index.dim
    End Sub

    Public Sub SetDims(s As IExtractor(Of T)) Implements IExtractor(Of T).SetDims
        ndim = s.Order
        dims = s.dims
        Offsets = s.Offsets
    End Sub

    Public Sub SetDims(s As IExtractor(Of Object)) Implements IExtractor(Of T).SetDims
        ndim = s.Order
        dims = s.dims
        Offsets = s.Offsets
    End Sub

    Public Sub SetDims(index As Iidx(Of T)) Implements IExtractor(Of T).SetDims
        SetDims(index.Spec)
    End Sub

    Public Sub SetDims(n As T) Implements IExtractor(Of T).SetDims
        For i = 0 To ndim - 1
            dims(i) = DirectCast(n, Object)
        Next

    End Sub

    Public Sub InsertDim(pos As Integer, dim_size As T) Implements IExtractor(Of T).InsertDim

    End Sub

    Public Function MaxDim() As T Implements IExtractor(Of T).MaxDim
        Dim m As Integer = 0
        For i = 0 To ndim - 1
            If m < dims(i) Then m = dims(i)
        Next
        Return DirectCast(m, Object)
    End Function

    Public Function RemoveDim(pos As Integer) As T Implements IExtractor(Of T).RemoveDim
        Dim rdim = [Dim](pos)
        For i = pos To ndim - 2
            dims(i) = dims(i + 1)
        Next
        dims(ndim - 1) = -1
        If Not Offsets Is Nothing Then
            For i = pos To ndim - 2
                Offsets(i) = Offsets(i + 1)
            Next
            Offsets(ndim - 1) = DirectCast(-1, Object)
        End If
        ndim -= 1
        Return rdim
    End Function

    Public Sub RemoveTrailingDims() Implements IExtractor(Of T).RemoveTrailingDims
        For i = ndim - 1 To 0 Step -1
            If [Dim](i) = DirectCast(1, Object) Then RemoveDim(i) Else Exit For
        Next
    End Sub

    Public Sub ShiftDim(d As Integer, pos As Integer) Implements IExtractor(Of T).ShiftDim

    End Sub
#End Region

#Region "Offset"
    Public Sub SetOffset(dimn As Integer, offset As T) Implements IExtractor(Of T).SetOffset
        If Offsets Is Nothing Then
            Offsets = New T() {DirectCast(0, Object), DirectCast(0, Object), DirectCast(0, Object), DirectCast(0, Object), DirectCast(0, Object), DirectCast(0, Object), DirectCast(0, Object), DirectCast(0, Object)}
        End If
        Offsets(dimn) = offset
    End Sub

    Public Function Offset(d As Integer) As T Implements IExtractor(Of T).Offset
        If Offsets Is Nothing Then
            Return DirectCast(0, Object)
        Else
            Return Offsets(d)
        End If
    End Function

    Public Function HasOffsets() As Boolean Implements IExtractor(Of T).HasOffsets
        Return Not Offsets Is Nothing
    End Function
#End Region

#Region "Misc"
    Public Shared Operator =(ByVal other As idxd(Of T), ByVal i As idxd(Of T)) As Boolean
        If other.ndim <> i.ndim Then Return False
        For ind = 0 To i.ndim - 1
            If Not DirectCast(other.Dim(ind), Object) = DirectCast(i.Dim(ind), Object) Then
                Return False
            End If
        Next
        Return True
    End Operator
    Public Shared Operator <>(ByVal other As idxd(Of T), ByVal i As idxd(Of T)) As Boolean
        If Not other = i Then Return True
        Return False
    End Operator
    Public Function Empty() As Boolean Implements IExtractor(Of T).Empty
        Return ndim = -1
    End Function
    Public Function Nelements() As Integer Implements IExtractor(Of T).Nelements
        Dim total = 1
        For i = 0 To ndim - 1
            total *= DirectCast([Dim](i), Object)
        Next
        Return total
    End Function
    Public Function Order() As Integer Implements IExtractor(Of T).Order
        Return ndim
    End Function
    Public Sub SetMax(other As IExtractor(Of T)) Implements IExtractor(Of T).SetMax
        For i = 0 To ndim - 1
            dims(i) = IIf(dims(i) > other.dims(i), dims(i), other.Dim(i))
        Next
    End Sub
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
