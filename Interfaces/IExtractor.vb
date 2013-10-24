'idxd
Public Interface IExtractor(Of T)
    Inherits IDisposable

#Region "Properties"
    Property ndim() As Integer
    Property Offsets() As T()
    Property dims() As Integer()
#End Region


    Sub SetDim(dimn As Integer, [size] As T)
    Sub SetDims(index As Iidx(Of T))
    Sub SetDims(s As IExtractor(Of T))
    Sub SetDims(s As IExtractor(Of Object))
    Sub SetDims(index As IDescription)
    Sub SetDims(n As T)

    Function Empty() As Boolean
    Sub InsertDim(pos As Integer, dim_size As T)
    Function RemoveDim(pos As Integer) As T
    Sub ShiftDim(d As Integer, pos As Integer)
    Function [Dim]([d] As Integer) As T
    Sub RemoveTrailingDims()

    Sub SetOffset(dimn As Integer, offset As T)
    Function HasOffsets() As Boolean
    Function [Offset]([d] As Integer) As T

    Sub SetMax(other As IExtractor(Of T))
    Function Nelements() As Integer
    Function Order() As Integer
    Function MaxDim() As T

End Interface