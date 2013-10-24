' idxspec
Public Interface IDescription
    Property ndim() As Integer
    Property Offset() As Integer
    Property [dim]() As Integer()
    Property [mod]() As Integer()

    Function GetOffset() As Integer
    Sub AddOffset(off As Integer)
    Sub SetOffset(off As Integer)

    Function GetNDim() As Integer
    Function FootPrint() As Integer
    Function Nelements() As Integer
    Function TrueOrer() As Integer
    Sub Copy(spec As IDescription)
    Function contiguousp() As Boolean

    Function SetNDim(Dimension As Integer) As Integer
    Function SetNDim(n As Integer, ldim() As Integer, lmod() As Integer) As Integer

    Function Resize(Optional i0 As Integer = -1, Optional i1 As Integer = -1, Optional i2 As Integer = -1, Optional i3 As Integer = -1, Optional i4 As Integer = -1, Optional i5 As Integer = -1, Optional i6 As Integer = -1, Optional i7 As Integer = -1) As Integer
    Function Resize1(ndim As Integer, [size] As Integer) As Integer
    Function Resize(D As IExtractor(Of Integer)) As Integer

    Sub init_spec(o As Integer, s0 As Integer, s1 As Integer, s2 As Integer, s3 As Integer, s4 As Integer, s5 As Integer, s6 As Integer, s7 As Integer)
    Sub init_spec(o As Integer, s0 As Integer, s1 As Integer, s2 As Integer, s3 As Integer, s4 As Integer, s5 As Integer, s6 As Integer, s7 As Integer, n As Integer)
End Interface