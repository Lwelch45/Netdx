Imports Netdx.DxError
Public Class File
    Dim Reader As Reader

    Public ReadOnly Property Length() As Integer
        Get
            Return Reader.Packet.Length
        End Get
    End Property
    Public ReadOnly Property Offset() As Integer
        Get
            Return Reader.Offset
        End Get
    End Property
#Region "Misc"
    Sub New(filename As String, Read As Boolean)
        '  Dim fs = IO.File.Open(filename, IO.FileMode.OpenOrCreate)
        If Not IO.File.Exists(filename) Then Raise(Me.GetType, "Constructor", "File dosent exist")
        If Read = True Then
            Reader = New Reader(IO.File.ReadAllBytes(filename))
        Else
            Raise(Me.GetType, "Constructor", "Writing to file not implemented")
        End If
    End Sub

    Public Sub SetLength(L As Integer)
        Reader.Offset = L
    End Sub

    Public Sub AddLength(L As Integer)
        Reader.Offset += L
    End Sub
#End Region

#Region "Read"
    Public Function ReadInt() As Integer
        Return Reader.ReadInteger
    End Function

    Public Function ReadDouble() As Double
        Return Reader.ReadDouble
    End Function

    Public Function Read3Int() As Integer
        Return Reader.ReadThreeByteInteger
    End Function
#End Region

End Class
Class Reader
    Private _Packet As Byte()
    Private _Offset As Integer

    Public Sub New(packet As Byte())
        _Packet = packet
        _Offset = 0
    End Sub

    Protected Overrides Sub Finalize()
        Try
            _Offset = -1
            _Packet = Nothing
        Finally
            MyBase.Finalize()
        End Try
    End Sub

    Public Property Offset() As Integer
        Get
            Return _Offset
        End Get
        Set(value As Integer)
            _Offset = value
        End Set
    End Property

    Public Function ReadInteger() As Integer
        Dim result As Integer = BitConverter.ToInt32(_Packet, _Offset)
        _Offset += 4
        Return result
    End Function

    ''' <summary>
    ''' A integer with 3 bytes not 4
    ''' </summary>
    Public Function ReadThreeByteInteger() As Integer
        Return CInt(ReadByte()) Or ReadByte() << 8 Or ReadByte() << 16
    End Function

    Public Function ReadUInteger() As UInteger
        Dim result As UInteger = BitConverter.ToUInt32(_Packet, _Offset)
        _Offset += 4
        Return result
    End Function

    Public Function ReadByte() As Byte
        Dim result As Byte = _Packet(_Offset)
        _Offset += 1
        Return result
    End Function

    Public Function ReadBytes(Length As Integer) As Byte()
        Dim result As Byte() = New Byte(Length - 1) {}
        Array.Copy(_Packet, _Offset, result, 0, Length)
        _Offset += Length
        Return result
    End Function

    Public Function ReadShort() As Short
        Dim result As Short = BitConverter.ToInt16(_Packet, _Offset)
        _Offset += 2
        Return result
    End Function

    Public Function ReadUShort() As UShort
        Dim result As UShort = BitConverter.ToUInt16(_Packet, _Offset)
        _Offset += 2
        Return result
    End Function

    Public Function ReadDouble() As Double
        Dim result As Double = BitConverter.ToDouble(_Packet, _Offset)
        _Offset += 8
        Return result
    End Function

    Public Function ReadLong() As Long
        Dim result As Long = BitConverter.ToInt64(_Packet, _Offset)
        _Offset += 8
        Return result
    End Function

    Public Function ReadULong() As ULong
        Dim result As ULong = BitConverter.ToUInt64(_Packet, _Offset)
        _Offset += 8
        Return result
    End Function

    Public Function ReadString() As String
        Dim result As String = ""
        Try
            result = System.Text.Encoding.Unicode.GetString(_Packet, _Offset, _Packet.Length - _Offset)
            Dim idx As Integer = result.IndexOf(ChrW(&H0))
            If Not (idx = -1) Then
                result = result.Substring(0, idx)
            End If
            _Offset += (result.Length * 2) + 2
        Catch ex As Exception
            Throw New Exception(ex.StackTrace + vbCr & vbLf + ex.Message)
        End Try
        Return result
    End Function

    Public ReadOnly Property Packet() As Byte()
        Get
            Return _Packet
        End Get
    End Property
End Class