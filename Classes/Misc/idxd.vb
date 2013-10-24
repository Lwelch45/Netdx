Public Class idxd(Of T)
    Implements IExtractor(Of T)

    Public Property ndim As Integer Implements IExtractor(Of T).ndim
        Get

        End Get
        Set(value As Integer)

        End Set
    End Property

    Public Property dims As Integer() Implements IExtractor(Of T).dims
        Get

        End Get
        Set(value As Integer())

        End Set
    End Property
    Public Property Offsets As T() Implements IExtractor(Of T).Offsets
        Get

        End Get
        Set(value As T())

        End Set
    End Property

#Region "Constructor"
    Sub New()
        Offsets = Nothing
        ndim = -1
        dims = New Integer(8) {}
    End Sub

    Sub New(i As idx(Of Type))
        Offsets = Nothing
        SetDims(i.Spec)
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

    End Function

    Public Sub SetDim(dimn As Integer, size As T) Implements IExtractor(Of T).SetDim
        dims(dimn) = DirectCast(size, Object)
    End Sub

    Public Sub SetDims(index As IDescription) Implements IExtractor(Of T).SetDims

    End Sub

    Public Sub SetDims(s As IExtractor(Of T)) Implements IExtractor(Of T).SetDims

    End Sub

    Public Sub SetDims(s As IExtractor(Of Object)) Implements IExtractor(Of T).SetDims

    End Sub

    Public Sub SetDims(index As Iidx(Of T)) Implements IExtractor(Of T).SetDims
        SetDims(index.Spec)
    End Sub

    Public Sub SetDims(n As T) Implements IExtractor(Of T).SetDims

    End Sub

    Public Sub InsertDim(pos As Integer, dim_size As T) Implements IExtractor(Of T).InsertDim

    End Sub

    Public Function MaxDim() As T Implements IExtractor(Of T).MaxDim

    End Function

    Public Function RemoveDim(pos As Integer) As T Implements IExtractor(Of T).RemoveDim

    End Function

    Public Sub RemoveTrailingDims() Implements IExtractor(Of T).RemoveTrailingDims

    End Sub

    Public Sub ShiftDim(d As Integer, pos As Integer) Implements IExtractor(Of T).ShiftDim

    End Sub
#End Region

#Region "Offset"
    Public Sub SetOffset(dimn As Integer, offset As T) Implements IExtractor(Of T).SetOffset

    End Sub

    Public Function Offset(d As Integer) As T Implements IExtractor(Of T).Offset

    End Function

    Public Function HasOffsets() As Boolean Implements IExtractor(Of T).HasOffsets

    End Function
#End Region

#Region "Misc"
    Public Function Empty() As Boolean Implements IExtractor(Of T).Empty

    End Function
    Public Function Nelements() As Integer Implements IExtractor(Of T).Nelements

    End Function
    Public Function Order() As Integer Implements IExtractor(Of T).Order

    End Function
    Public Sub SetMax(other As IExtractor(Of T)) Implements IExtractor(Of T).SetMax

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
