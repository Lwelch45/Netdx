Imports RandomGen
Namespace Misc
    Public Class Random
        Private Gen As MersenneTwister
        Sub New()
            Gen = New MersenneTwister
        End Sub
        Sub New(seed As Integer)
            Gen = New MersenneTwister(seed)
        End Sub

        Public Function drand() As Double
            Return Gen.NextDoublePositive
        End Function

        ''' <summary>
        ''' Random distribution: [-v,v]
        ''' </summary>
        ''' <param name="V"></param>
        ''' <returns>random double within range</returns>
        Public Function drand(V As Double) As Double
            Return V * 2 * drand - V
        End Function

        ''' <summary>
        ''' Random number generator. Return a random number drawn from a uniform distribution over [v0,v1].
        ''' </summary>
        ''' <param name="v0"></param>
        ''' <param name="v1"></param>
        ''' <returns>random double between [v0,v1]</returns>
        Public Function drand(v0 As Double, v1 As Double) As Double
            Return Gen.Next(v0, v1)
        End Function
    End Class
End Namespace