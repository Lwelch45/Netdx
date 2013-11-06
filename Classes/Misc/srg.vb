Namespace Misc
    Public Class Srg(Of T)
        Implements IStorage(Of T)
        Friend ID As Guid
        Private Data As T()

        Public Shared _id As Integer = 0

        Sub New(ByVal [Size] As Integer)
            ID = New Guid
            ' ID = GenID()
            ' Console.WriteLine("Storage ID: {0}", ID)
            Array.Resize(Data, [Size])
        End Sub

        Sub New()
            ID = New Guid
            ' ID = GenID()
            '  Console.WriteLine("Storage ID: {0}", _id)
            Array.Resize(Data, 0)
        End Sub

        Public Shared Function GenID() As Guid
            Return New Guid
        End Function

        Public Sub ChangeSize(ByVal S As Integer) Implements IStorage(Of T).ChangeSize
            Array.Resize(Data, S)
        End Sub

        Public Sub Clear() Implements IStorage(Of T).Clear
            Array.Clear(Data, 0, Data.Length)
            Array.Resize(Data, 1)
        End Sub

        Public Function Size() As Integer Implements IStorage(Of T).Size
            Return Data.Length
        End Function

        Public Function GetData(ByVal i As Integer) As T Implements IStorage(Of T).GetData
            Return Data(i)
        End Function

        Public Sub SetData(ByVal i As Integer, ByVal item As T) Implements IStorage(Of T).SetData
            Data(i) = item
        End Sub

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
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
End Namespace
