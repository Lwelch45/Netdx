
Namespace Misc
    Public Class DxError
        Public Shared Sub Raise([Class] As Type, method As String, Message As String)
            Console.WriteLine("Error {0}::{1} : {2}", [Class].Name, method, Message)
            Throw New Exception(String.Format("Error {0}::{1} : {2}", [Class].Name, method, Message))
        End Sub

        Public Shared Sub Raise([Class] As Type, Message As String)
            Console.WriteLine("Error in Class {0} : {0}", [Class].Name, Message)
            Throw New Exception(String.Format("Error in Class {0} : {0}", [Class].Name, Message))
        End Sub

        Public Shared Sub Raise(method As String, Message As String)
            Console.WriteLine("Error in method {0} : {0}", method, Message)
            Throw New Exception(String.Format("Error in method {0} : {0}", method, Message))
        End Sub

        Public Shared Sub Raise(Message As String)
            Console.WriteLine("Error: {0}", Message)
            Throw New Exception(String.Format("Error: {0}", Message))
        End Sub
    End Class
End Namespace