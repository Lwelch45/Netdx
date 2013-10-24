'srg
Public Interface IStorage(Of T)
    Inherits IDisposable

    Sub Clear()
    Sub ChangeSize(S As Integer) 'Take the current storage and copy it into a larger/smaller array
    Function GetData(i As Int32) As T
    Sub SetData(ByVal i As Int32, ByVal item As T)

    Function Size() As Integer
End Interface
