Public Interface Iidx(Of T)
    Inherits IDisposable
    Property Spec As idxspec
    Property Storage As IStorage(Of T)

    Sub AddOffset(off As Int32)
    Function SetOffset(off As Int32) As Integer

    Function GetStorage() As IStorage(Of T)
    Function Offset() As Integer
    Function Nelements() As Integer
    Function FootPrint() As Integer
    Function TrueOrer() As Integer

    Function [Get]() As T
    Function [Get](i0 As Integer) As T
    Function [Get](i0 As Integer, i1 As Integer) As T
    Function [Get](i0 As Integer, i1 As Integer, i2 As Integer) As T
    Function [Get](i0 As Integer, i1 As Integer, i2 As Integer, i3 As Integer, Optional i4 As Integer = 0, Optional i5 As Integer = -1, Optional i6 As Integer = -1, Optional i7 As Integer = -1) As T
    Function [GGet](Optional i0 As Integer = 0, Optional i1 As Integer = 0, Optional i2 As Integer = 0, Optional i3 As Integer = 0, Optional i4 As Integer = 0, Optional i5 As Integer = 0, Optional i6 As Integer = 0, Optional i7 As Integer = 0) As T

    Function [Set](Val As T) As T
    Function [Set](Val As T, i0 As Integer) As T
    Function [Set](Val As T, i0 As Integer, i1 As Integer) As T
    Function [Set](Val As T, i0 As Integer, i1 As Integer, i2 As Integer) As T
    Function [Set](Val As T, i0 As Integer, i1 As Integer, i2 As Integer, i3 As Integer, Optional i4 As Integer = 0, Optional i5 As Integer = 0, Optional i6 As Integer = 0, Optional i7 As Integer = 0) As T
    Function [SSet](Val As T, Optional i0 As Integer = 0, Optional i1 As Integer = 0, Optional i2 As Integer = 0, Optional i3 As Integer = 0, Optional i4 As Integer = 0, Optional i5 As Integer = 0, Optional i6 As Integer = 0, Optional i7 As Integer = 0) As T


End Interface
