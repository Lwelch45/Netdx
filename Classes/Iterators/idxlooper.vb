Imports Netdx.DxError
Public Class idxlooper(Of T)
    Inherits idx(Of T)

    Public i As Integer
    Public dimd As Integer
    Public modd As Integer

    Sub New(src As idx(Of T), ld As Integer)
        MyBase.new(True)
        If ld >= src.Order Then Raise(Me.GetType, "Constructor", "Check ld argument")
        If src.Order = 0 Then Raise(Me.GetType, "Consructor", "cannot loop on idx with order 0 ")
        i = 0
        dimd = src.dim(ld)
        modd = src.mod(ld)

        src.Spec.select_into(Me.Spec, ld, i)
        Me.Storage = src.Storage
    End Sub

    Public Function [next]() As T
        i += 1
        Me.AddOffset(modd)
        'Console.WriteLine(Me.Offset)
        If Me.Offset > Storage.Size Then Return Nothing
        Return IIf(Not Storage.GetData(Me.Offset) Is Nothing, Storage.GetData(Me.Offset), "NOTHING")
    End Function

    Public Function notdone() As Boolean
        Return (i < dimd)
    End Function
End Class
