Imports Netdx.Iterators
Imports System.Runtime.InteropServices
Public Class idxIO
    'Standard
    Public Shared MAGIC_FLOAT_MATRIX As Integer = &H1E3D4C51
    Public Shared MAGIC_PACKED_MATRIX As Integer = &H1E3D4C52
    Public Shared MAGIC_DOUBLE_MATRIX As Integer = &H1E3D4C53
    Public Shared MAGIC_INTEGER_MATRIX As Integer = &H1E3D4C54
    Public Shared MAGIC_BYTE_MATRIX As Integer = &H1E3D4C55
    Public Shared MAGIC_SHORT_MATRIX As Integer = &H1E3D4C56
    Public Shared MAGIC_SHORT8_MATRIX As Integer = &H1E3D4C57
    Public Shared MAGIC_LONG_MATRIX As Integer = &H1E3D4C58
    Public Shared MAGIC_ASCII_MATRIX As Integer = &H2E4D4154
    'Non-Standard
    Public Shared MAGIC_UINT_MATRIX As Integer = &H1E3D4C59
    Public Shared MAGIC_UINT64_MATRIX As Integer = &H1E3D4C5A
    Public Shared MAGIC_INT64_MATRIX As Integer = &H1E3D4C5B
    Sub New()
    End Sub

#Region "helper functions"
    Public Shared Function is_matrix(filename As String) As Boolean
        Dim Fp As File
        Try
            Fp = New File(filename, True)
            Dim magic As Integer
            Dim d As idxd(Of Integer)
            d = read_matrix_header(Fp, magic)
        Catch ex As Exception
            Console.WriteLine(ex.ToString)
            Return False
        End Try
        Return True
    End Function

    Public Shared Function is_magic(magic As Integer) As Boolean
        Select Case (magic)
            Case Is = MAGIC_FLOAT_MATRIX Or MAGIC_PACKED_MATRIX Or MAGIC_DOUBLE_MATRIX
                Return True
            Case Is = MAGIC_INTEGER_MATRIX Or MAGIC_BYTE_MATRIX Or MAGIC_SHORT_MATRIX
                Return True
            Case Is = MAGIC_SHORT8_MATRIX Or MAGIC_LONG_MATRIX Or MAGIC_ASCII_MATRIX
                Return True
            Case Is = MAGIC_UINT_MATRIX Or MAGIC_UINT64_MATRIX Or MAGIC_INT64_MATRIX
                Return True
            Case Else
                Return False
        End Select

    End Function

    Public Shared Function get_matrix_type(filename As String) As Integer
        Dim Fp As File
        Try
            Fp = New File(filename, True)
        Catch ex As Exception
            Console.WriteLine(ex.ToString)
            Return Nothing
        End Try
        Dim magic As Integer
        Dim d As idxd(Of Integer)
        Try
            d = read_matrix_header(Fp, magic)
        Catch ex As Exception
            Console.WriteLine(ex.ToString)
            Return Nothing
        End Try
        Return magic
    End Function

    Public Shared Function get_matrix_dims(filename As String) As idxd(Of Integer)
        Dim Fp As File
        Try
            Fp = New File(filename, True)
        Catch ex As Exception
            Console.WriteLine(ex.ToString)
            Return Nothing
        End Try
        Dim magic As Integer
        Dim d As idxd(Of Integer)
        Try
            d = read_matrix_header(Fp, magic)
        Catch ex As Exception
            Console.WriteLine(ex.ToString)
            Return Nothing
        End Try
        Return d
    End Function

    Public Shared Function has_multiple_matrices(filename As String) As Boolean
        Dim Fp As File
        Try
            Fp = New File(filename, True)
        Catch ex As Exception
            Console.WriteLine(ex.ToString)
            Return False
        End Try

        Dim magic As Integer
        Dim d As idxd(Of Integer)
        Try
            d = read_matrix_header(Fp, magic)
            Dim size As Integer = d.Nelements
            Select Case magic
                Case Is = MAGIC_BYTE_MATRIX
                    size *= 1
                    Exit Select
                Case Is = MAGIC_INTEGER_MATRIX
                    size *= 4
                    Exit Select
                Case Is = MAGIC_FLOAT_MATRIX
                    size *= 4
                    Exit Select
                Case Is = MAGIC_DOUBLE_MATRIX
                    size *= 8
                    Exit Select
                Case Is = MAGIC_LONG_MATRIX
                    size *= 8
                    Exit Select
            End Select
            Fp.AddLength(size)

        Catch ex As Exception
            Console.WriteLine(ex.ToString)
        End Try
        If Fp.Length - 1 <> Fp.Offset Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function read_matrix_header(ByRef F As File, ByRef Magic As Integer) As idxd(Of Integer)
        Dim ndim, v As Integer
        Dim dims As idxd(Of Integer)
        Magic = F.ReadInt()

        If is_magic(Magic) Then
            ndim = F.ReadInt
        Else
            'Not implemented
        End If

        dims = New idxd(Of Integer)()
        Try
            For x = 0 To ndim - 1
                v = F.ReadInt
                If x < ndim Then
                    If v = 0 Or v < 0 Then
                        Throw New Exception("dimension is negative or zero")
                    End If
                End If
                dims.InsertDim(x, v)
            Next
        Catch ex As Exception
            Console.WriteLine(ex.ToString)
        End Try
        Return dims
    End Function
#End Region

#Region "loading"

    Public Shared Sub read_matrix_body_integer(ByRef fp As File, ByRef m As idx(Of Integer))
        Dim fpp = fp
        idx_aloop1(m, Sub(itr0, src)
                          src.Ptr(itr0.DataO) = DirectCast(fpp.ReadInt, Object)
                      End Sub)
        fp = fpp
    End Sub
    Public Shared Sub read_matrix_body_double(ByRef fp As File, ByRef m As idx(Of Double))
        Dim fpp = fp
        idx_aloop1(m, Sub(itr0, src)
                          src.Ptr(itr0.DataO) = fpp.ReadDouble
                      End Sub)
        fp = fpp
    End Sub

    Public Shared Sub load_matrix(Of T)(ByRef m As idx(Of T), filename As String)
        Dim File = New File(filename, True)
        m = load_matrix(Of T)(File)
    End Sub
    Public Shared Function load_matrix(Of T)(ByRef fp As File, Optional ByRef out_ As idx(Of T) = Nothing)
        Dim magic As Integer
        Dim dims = read_matrix_header(fp, magic)
        '  Dim pout As idx(Of T)
        If out_ Is Nothing Then 'if no input matrix, allocate new one
            '  pout = New idx(Of T)(dims)
        Else
            '    pout = out_
        End If
        Select Case magic
            Case Is = MAGIC_DOUBLE_MATRIX
                Dim pout = New idx(Of Double)(dims)
                read_matrix_body_double(fp, pout)
                Return pout
            Case Is = MAGIC_INTEGER_MATRIX
                Dim pout = New idx(Of Integer)(dims)
                read_matrix_body_integer(fp, pout)
                Return pout
            Case Else
                DxError.Raise("Not implemented")
        End Select

    End Function
#End Region
End Class
