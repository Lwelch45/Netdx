Public Class infer_param

End Class

'' gradient parameters
'' A class that contains all the parameters
'' for a stochastic gradient descent update,
'' including step sizes, regularizer coefficients...
Public Class gd_param
    Inherits infer_param

    '! global step size
    Public eta As Double
    '! stopping criterion threshold
    Public n As Double
    '! L1 regularizer coefficient
    Public decay_l1 As Double
    '! L2 regularizer coefficient
    Public decay_l2 As Double
    '! Time (in number of training samples) after which to decay values
    Public decay_time As Integer
    '! momentum term
    Public inertia As Double
    '! annealing coefficient for the learning rate
    Public anneal_value As Double
    '! Number of training samples beetween two annealings.
    Public anneal_period As Integer
    '! threshold on square norm of gradient for stopping
    Public gradient_threshold As Double
    '! for debugging purpose
    Public niter_done As Integer

    Sub New()
        eta = 0.0
        n = 0
        decay_time = 0
        decay_l2 = 0.0
        decay_l1 = 0.0
        inertia = 0.0
        anneal_value = 0.0
        anneal_period = 0
        gradient_threshold = 0.0
        niter_done = 0
    End Sub

    Private Sub New(leta As Double, ln As Double, l1 As Double, l2 As Double,
                    dtime As Integer, iner As Double,
                        a_v As Double, a_p As Integer, g_t As Double)
        eta = leta
        n = ln
        decay_time = dtime
        decay_l2 = l2
        decay_l1 = l1
        inertia = iner
        anneal_value = a_v
        anneal_period = a_p
        gradient_threshold = g_t
        niter_done = 0
    End Sub

    Public Overrides Function ToString() As String
        Dim str As String = ""
        str += "Gradient parameters: eta " & eta & " stopping threshold " & n
        str += " decay_l1 " & decay_l1 & " decay_l2 " & decay_l2 & " decay_time " & decay_time
        str += " inertia " & inertia & " anneal_value " & anneal_value
        str+= " anneal_period " & anneal_period & " gradient threshold " & gradient_threshold
        Return str
    End Function
End Class

