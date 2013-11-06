Imports Netdx.DxError

Public Class idxspec
    Implements IDescription

    Private _offset As Integer


    Public Property Offset As Integer Implements IDescription.Offset
        Get
            Return _offset
        End Get
        Set(value As Integer)
            _offset = value
        End Set
    End Property

    Private _mod As Integer()
    Public Property [mod] As Integer() Implements IDescription.mod
        Get
            Return _mod
        End Get
        Set(value As Integer())
            _mod = value
        End Set
    End Property

    Private _dim As Integer()
    Public Property [dim] As Integer() Implements IDescription.dim
        Get
            Return _dim
        End Get
        Set(value As Integer())
            _dim = value
        End Set
    End Property

    Private _ndim As Integer
    Public Property ndim As Integer Implements IDescription.ndim
        Get
            Return _ndim
        End Get
        Set(value As Integer)
            _ndim = value
        End Set
    End Property

#Region "Constructors"
    Sub New()
        [dim] = Nothing
        ndim = -1

        [mod] = Nothing
        Offset = 0
    End Sub

    Sub New(src As idxspec)
        [dim] = Nothing
        ndim = -1
        [mod] = Nothing
        Copy(src)
    End Sub

    Sub New(o As Integer)
        ndim = 0
        Offset = o
        [dim] = Nothing
        [mod] = Nothing
    End Sub
    Sub New(o As Integer, d As idxd(Of Integer))
        init_spec(o, d.dims(0), d.dims(1), d.dims(2), d.dims(3), d.dims(4), _
         d.dims(5), d.dims(6), d.dims(7), d.Order())
    End Sub

    Sub New(ByVal o As Integer, ByVal s0 As Integer)
        If s0 < 0 Then Raise(Me.GetType, "Constructor", "trying to construct idx1 with negative dimension " & s0)
        ndim = -1
        Offset = o
        [dim] = Nothing
        [mod] = Nothing

        SetNDim(1)
        [dim](0) = s0
        [mod](0) = 1
    End Sub

    Sub New(ByVal o As Integer, ByVal s0 As Integer, ByVal s1 As Integer)
        If (s0 < 0 Or s1 < 0) Then Raise(Me.GetType, "Constructor", "trying to construct idx2 with negative dimensions: " & s0 & "x" & s1 & " offset: " & o)
        ndim = -1
        Offset = o
        [dim] = Nothing
        [mod] = Nothing

        SetNDim(2)
        [dim](0) = s0
        [mod](0) = s1
        [dim](1) = s1
        [mod](1) = 1
    End Sub

    Sub New(ByVal o As Integer, ByVal s0 As Integer, ByVal s1 As Integer, ByVal s2 As Integer)
        If (s0 < 0 Or s1 < 0 Or s2 < 0) Then Raise(Me.GetType, "Constructor", "trying to construct idx3 with negative dimensions: " & s0 & "x" & s1 & "x" & s2 & " offset: " & o)
        ndim = -1
        Offset = o
        [dim] = Nothing
        [mod] = Nothing

        SetNDim(3)
        [dim](0) = s0
        [mod](0) = s1 * s2
        [dim](1) = s1
        [mod](1) = s2
        [dim](2) = s2
        [mod](2) = 1

    End Sub

    Sub New(ByVal o As Integer, ByVal s0 As Integer, ByVal s1 As Integer, ByVal s2 As Integer, ByVal s3 As Integer, Optional ByVal s4 As Integer = -1, Optional ByVal s5 As Integer = -1, Optional ByVal s6 As Integer = -1, Optional ByVal s7 As Integer = -1)
        init_spec(o, s0, s1, s2, s3, s4, s5, s6, s7)
    End Sub

    Sub New(ByVal o As Integer, ByVal n As Integer, ByVal ldim As Integer(), ByVal lmod As Integer())
        ndim = -1
        Offset = o
        [dim] = Nothing
        [mod] = Nothing
        SetNDim(n)
        For i = 0 To n + 1
            [dim](i) = ldim(i) : [mod](i) = lmod(i)
        Next

    End Sub

    Public Sub init_spec(ByVal o As Integer, ByVal s0 As Integer, ByVal s1 As Integer, ByVal s2 As Integer, ByVal s3 As Integer, ByVal s4 As Integer, ByVal s5 As Integer, ByVal s6 As Integer, ByVal s7 As Integer) Implements IDescription.init_spec
        Dim ndimset As Boolean = False
        Dim md As Integer = 1
        [dim] = Nothing
        [mod] = Nothing
        Offset = 0
        ndim = -1

        If s7 > 0 Then
            If Not ndimset Then
                SetNDim(8) : ndimset = True
            End If
            [dim](7) = s7 : [mod](7) = md : md *= s7
        End If
        If s6 > 0 Then
            If Not ndimset Then
                SetNDim(7) : ndimset = True
            End If
            [dim](6) = s6 : [mod](6) = md : md *= s6
        End If
        If s5 > 0 Then
            If Not ndimset Then
                SetNDim(6) : ndimset = True
            End If
            [dim](5) = s5 : [mod](5) = md : md *= s5
        End If
        If s4 > 0 Then
            If Not ndimset Then
                SetNDim(5) : ndimset = True
            End If
            [dim](4) = s4 : [mod](4) = md : md *= s4
        End If
        If s3 > 0 Then
            If Not ndimset Then
                SetNDim(4) : ndimset = True
            End If
            [dim](3) = s3 : [mod](3) = md : md *= s3
        End If
        If s2 > 0 Then
            If Not ndimset Then
                SetNDim(3) : ndimset = True
            End If
            [dim](2) = s2 : [mod](2) = md : md *= s2
        End If
        If s1 > 0 Then
            If Not ndimset Then
                SetNDim(2) : ndimset = True
            End If
            [dim](1) = s1 : [mod](1) = md : md *= s1
        End If
        If s0 > 0 Then
            If Not ndimset Then
                SetNDim(1) : ndimset = True
            End If
            [dim](0) = s0 : [mod](0) = md : md *= s0
        End If
    End Sub

    Public Sub init_spec(ByVal o As Integer, ByVal s0 As Integer, ByVal s1 As Integer, ByVal s2 As Integer, ByVal s3 As Integer, ByVal s4 As Integer, ByVal s5 As Integer, ByVal s6 As Integer, ByVal s7 As Integer, ByVal n As Integer) Implements IDescription.init_spec

    End Sub
#End Region

#Region "Offset"
    Public Function GetOffset() As Integer Implements IDescription.GetOffset
        Return Offset
    End Function

    Public Sub AddOffset(ByVal off As Integer) Implements IDescription.AddOffset
        Offset += off
    End Sub

    Public Sub SetOffset(ByVal off As Integer) Implements IDescription.SetOffset
        Offset = off
    End Sub
#End Region

#Region "Resize"
    Public Function Resize(Optional ByVal s0 As Integer = -1, Optional ByVal s1 As Integer = -1, Optional ByVal s2 As Integer = -1, Optional ByVal s3 As Integer = -1, Optional ByVal s4 As Integer = -1, Optional ByVal s5 As Integer = -1, Optional ByVal s6 As Integer = -1, Optional ByVal s7 As Integer = -1) As Integer Implements IDescription.Resize
        Dim md As Integer = 1
        If s7 > 0 Then
            [dim](7) = s7 : [mod](7) = md : md *= s7
        End If
        If s6 > 0 Then
            [dim](6) = s6 : [mod](6) = md : md *= s6
        End If
        If s5 > 0 Then
            [dim](5) = s5 : [mod](5) = md : md *= s5
        End If
        If s4 > 0 Then
            [dim](4) = s4 : [mod](4) = md : md *= s4
        End If
        If s3 > 0 Then
            [dim](3) = s3 : [mod](3) = md : md *= s3
        End If
        If s2 > 0 Then
            [dim](2) = s2 : [mod](2) = md : md *= s2
        End If
        If s1 > 0 Then
            [dim](1) = s1 : [mod](1) = md : md *= s1
        End If
        If s0 > 0 Then
            [dim](0) = s0 : [mod](0) = md : md *= s0
        End If
        Return md + FootPrint()
    End Function

    Public Function Resize(ByVal D As IExtractor(Of Integer)) As Integer Implements IDescription.Resize
        Return Resize(D.dims(0), D.dims(1), D.dims(2), D.dims(3), D.dims(4), D.dims(5), D.dims(6), D.dims(7))
    End Function

    Public Function Resize1(ByVal ndim As Integer, ByVal size As Integer) As Integer Implements IDescription.Resize1
        Raise(Me.GetType, "Resize1", "Not Implemented")
        Return 0
    End Function
#End Region

#Region "Dim"
    Public Function SetNDim(ByVal D As Integer) As Integer Implements IDescription.SetNDim
        If D = 0 Or D > ndim Then
            If Not [dim] Is Nothing Then [dim] = Nothing
            If Not [mod] Is Nothing Then [mod] = Nothing
        End If
        If D > 0 Then
            If [dim] Is Nothing Then [dim] = New Integer(D) {}
            If [mod] Is Nothing Then [mod] = New Integer(D) {}
        End If
        ndim = D
        Return ndim
    End Function

    Public Function SetNDim(ByVal n As Integer, ByVal ldim() As Integer, ByVal lmod() As Integer) As Integer Implements IDescription.SetNDim
        If Not [dim] Is Nothing Then [dim] = Nothing
        If Not [mod] Is Nothing Then [mod] = Nothing

        [dim] = ldim
        [mod] = lmod
        ndim = n
        Return ndim
    End Function
#End Region

#Region "Misc"
    Public Overrides Function ToString() As String
        Dim str As String = ""
        str += "ndim= " & ndim & vbNewLine
        str += "offset= " & Offset & vbNewLine

        If ndim > 0 Then
            str += "  dim=["
            For i = 0 To ndim - 2
                str += [dim](i) & ", "
            Next
            str += [dim](ndim - 1) & "]" & vbNewLine

            str += "  mod=["
            For i = 0 To ndim - 2
                str += [mod](i) & ", "
            Next
            str += [mod](ndim - 1) & "]" & vbNewLine
        Else
        End If
        str += "footprint= " & FootPrint() & vbNewLine
        str += "contiguous= " & IIf(contiguousp, "yes", "no") & vbNewLine
        Return str
    End Function

    Public Function contiguousp() As Boolean Implements IDescription.contiguousp
        Dim size As Integer = 1 : Dim r As Boolean = True
        For i = ndim - 1 To 0 Step -1
            If Not size = [mod](i) Then
                r = False
            End If
            size *= [dim](i)
        Next
        Return r
    End Function

    Public Sub Copy(spec As IDescription) Implements IDescription.Copy
        Offset = spec.Offset
        SetNDim(spec.ndim)
        [dim] = spec.dim
        [mod] = spec.mod
    End Sub

    Public Function FootPrint() As Integer Implements IDescription.FootPrint
        Dim r As Integer = Offset + 1
        For i = 0 To ndim - 1
            r += [mod](i) * [dim](i)
        Next
        Return r
    End Function

    Public Function GetNDim() As Integer Implements IDescription.GetNDim
        Return ndim
    End Function

    Public Function Nelements() As Integer Implements IDescription.Nelements
        Dim r As Integer = 1
        For i = 0 To ndim - 1
            r *= [dim](i)
        Next
        Return r
    End Function

    Public Function TrueOrer() As Integer Implements IDescription.TrueOrer
        Dim order As Integer = 0
        For i = 0 To ndim - 1
            If ([dim](i) > 1) Then order = i + 1
        Next
        Return order
    End Function
#End Region

#Region "Manipulation"
    Public Function [select](d As Integer, i As Integer) As idxspec
        Dim r = New idxspec()
        select_into(r, d, i)
        Return r
    End Function
    Public Sub select_inplace(D As Integer, n As Integer)
        select_into(Me, D, n)
    End Sub
    Public Sub select_into(ByRef dst As idxspec, d As Integer, n As Integer)
        dst.SetNDim(ndim - 1)
        dst.Offset = Offset + n * [mod](d)
        If ndim - 1 > 0 Then
            For j = 0 To d - 1
                dst.dim(j) = [dim](j)
                dst.mod(j) = [mod](j)
            Next
            For j = d To ndim - 1
                dst.dim(j) = [dim](j + 1)
                dst.mod(j) = [mod](j + 1)
            Next
        End If
    End Sub

    Public Sub narrow_into(ByRef dst As idxspec, d As Integer, s As Integer, o As Integer)
        dst.SetNDim(ndim)
        dst.Offset = Offset + 0 * [mod](d)
        For j = 0 To ndim - 1
            dst.dim(j) = [dim](j)
            dst.mod(j) = [mod](j)
        Next
        dst.dim(d) = s
    End Sub
    Public Sub narrow_inplace(d As Integer, s As Integer, o As Integer)
        narrow_into(Me, d, s, o)
    End Sub
    Public Function narrow(d As Integer, s As Integer, O As Integer)
        Dim r = New idxspec
        narrow_into(r, d, s, O)
        Return r
    End Function

    Public Sub transpose_into(ByRef dst As idxspec, d1 As Integer, d2 As Integer)
        dst.SetNDim(ndim)
        dst.Offset = Offset

        For j = 0 To ndim - 1
            dst.dim(j) = [dim](j)
            dst.mod(j) = [mod](j)
        Next
        Dim tmp As Integer = 0

        ' dst->dim[d2]=tmp
        tmp = [dim](d1) : dst.dim(d1) = [dim](d2) : dst.dim(d2) = tmp
        tmp = [mod](d1) : dst.mod(d1) = [mod](d2) : dst.mod(d2) = tmp
    End Sub
    Public Sub transpose_inplace(d1 As Integer, d2 As Integer)
        transpose_into(Me, d1, d2)
    End Sub
    Public Function transpose(d1 As Integer, d2 As Integer) As idxspec
        Dim r = New idxspec
        transpose_into(r, d1, d2)
        Return r
    End Function

    ' d: dimension; k: kernel size; s: stride
    Public Sub unfold_into(ByRef dst As idxspec, d As Integer, k As Integer, s As Integer)
        Dim ns As Integer = 1 + ([dim](d) - k) / s
        dst.SetNDim(ndim + 1)
        dst.Offset = Offset
        For j = 0 To ndim - 1
            dst.dim(j) = [dim](j)
            dst.mod(j) = [mod](j)
        Next
        dst.dim(ndim) = k
        dst.mod(ndim) = [mod](d)
        dst.dim(d) = ns
        dst.mod(d) = [mod](d) * s
    End Sub
    Public Sub unfold_inplace(d As Integer, k As Integer, s As Integer)
        unfold_into(Me, d, k, s)
    End Sub
    Public Function unfold(d As Integer, k As Integer, s As Integer) As idxspec
        Dim r As New idxspec
        unfold_into(r, d, k, s)
        Return r
    End Function

#End Region

End Class
