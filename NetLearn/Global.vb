Imports Netdx.DxError
Public Class [Global]

    Public Shared Sub Print(O As Object)
        Console.WriteLine(O)
    End Sub
    Public Shared Sub eblerror(msg As String)
        Raise(msg)
    End Sub

End Class
