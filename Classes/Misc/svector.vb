Public Class svector(Of T)
    Implements IList(Of T), IDisposable


    Protected Friend L As New List(Of T)
    Public Sub New()
        L = New List(Of T)

    End Sub

    Public Sub New(n As Integer)
        L = New List(Of T)(n)
    End Sub

    Public Sub Add(item As T) Implements System.Collections.Generic.ICollection(Of T).Add
        L.Add(item)
    End Sub

    Public Overridable Sub Clear() Implements System.Collections.Generic.ICollection(Of T).Clear
        L.Clear()
    End Sub

    Public Function Contains(item As T) As Boolean Implements System.Collections.Generic.ICollection(Of T).Contains
        Return L.Contains(item)
    End Function

    Public Sub CopyTo(array() As T, arrayIndex As Integer) Implements System.Collections.Generic.ICollection(Of T).CopyTo
        L.CopyTo(array, arrayIndex)
    End Sub

    Public ReadOnly Property Count As Integer Implements System.Collections.Generic.ICollection(Of T).Count
        Get
            Return L.Count
        End Get
    End Property

    Public ReadOnly Property IsReadOnly As Boolean Implements System.Collections.Generic.ICollection(Of T).IsReadOnly
        Get
            Return False
        End Get
    End Property

    Public Function Remove(item As T) As Boolean Implements System.Collections.Generic.ICollection(Of T).Remove
        Return L.Remove(item)
    End Function

    Public Function GetEnumerator() As System.Collections.Generic.IEnumerator(Of T) Implements System.Collections.Generic.IEnumerable(Of T).GetEnumerator
        Return L.GetEnumerator
    End Function

    Public Function IndexOf(item As T) As Integer Implements System.Collections.Generic.IList(Of T).IndexOf
        Return L.IndexOf(item)
    End Function

    Public Sub Insert(index As Integer, item As T) Implements System.Collections.Generic.IList(Of T).Insert
        L.Insert(index, item)
    End Sub

    Default Public Property Item(index As Integer) As T Implements System.Collections.Generic.IList(Of T).Item
        Get
            Return L.Item(index)
        End Get
        Set(value As T)
            L.Item(index) = value
        End Set
    End Property

    Public Sub RemoveAt(index As Integer) Implements System.Collections.Generic.IList(Of T).RemoveAt
        L.RemoveAt(index)
    End Sub

    Public Function GetEnumerator1() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        Return GetEnumerator()
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                L = Nothing
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
