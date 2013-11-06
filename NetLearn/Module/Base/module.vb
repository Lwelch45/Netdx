Imports System.IO
Imports NetLearn.Global
Public Class [module]
    Implements IDisposable

    Public silent As Boolean = True

    Private _enabled As Boolean
    Public Property enabled() As Boolean
        Get
            Return _enabled
        End Get
        Set(ByVal value As Boolean)
            _enabled = value
        End Set
    End Property

    Private _name As String
    Public Property name() As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
        End Set
    End Property

    Private Shared _merr As TextWriter
    Public Property merr() As TextWriter
        Get
            Return _merr
        End Get
        Set(ByVal value As TextWriter)
            _merr = value
        End Set
    End Property

    Private Shared _mout As TextWriter
    Public Property mout() As TextWriter
        Get
            Return _mout
        End Get
        Set(ByVal value As TextWriter)
            _mout = value
        End Set
    End Property

    Sub New(Optional n As String = "module")
        silent = False
        name = n
        enabled = True
        mout = Console.Out
        merr = Console.Out

    End Sub

    Public Sub SetOutputTextWriters(ByRef Err As TextWriter, ByRef [out] As TextWriter)
        merr = Err
        mout = [out]
    End Sub

    Public Function describe() As String
        Return name '' default, just return the module's name
    End Function

    Public Sub enable()
        enabled = True
        Print("Module " & name & " is enabled")
    End Sub

    Public Sub disable()
        enabled = False
        Print("Module " & name & " is disabled")
    End Sub

    Public Function describe_indent(indent As Integer) As String
        Dim str As String = ""
        For x = 0 To indent : str += "\t" : Next
        Return str & describe()
    End Function


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