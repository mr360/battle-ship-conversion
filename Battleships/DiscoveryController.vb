Imports SwinGame

''' <summary>
''' The battle phase is handled by the DiscoveryController.
''' </summary>
Module DiscoveryController

    ''' <summary>
    ''' Handles input during the discovery phase of the game.
    ''' </summary>
    ''' <remarks>
    ''' Escape opens the game menu. Clicking the mouse will
    ''' attack a location.
    ''' </remarks>
    Public Sub HandleDiscoveryInput()
        If Input.WasKeyTyped(Keys.VK_ESCAPE) Then
            AddNewState(GameState.ViewingGameMenu)
        End If

        'TODO: Step 03: Add code here to DoAttack() when the mouse is clicked
    End Sub

    ''' <summary>
    ''' Attack the location that the mouse if over.
    ''' </summary>
    Private Sub DoAttack()
        Dim mouse As Point2D

        mouse = Input.GetMousePosition()

        'Calculate the row/col clicked
        Dim row, col As Integer

        'TODO: Step 3: Add code to perform an attack
    End Sub

    ''' <summary>
    ''' Draws the game during the attack phase.
    ''' </summary>s
    Public Sub DrawDiscovery()
        Const SCORES_LEFT As Integer = 172
        Const SHOTS_TOP As Integer = 157
        Const HITS_TOP As Integer = 206
        Const SPLASH_TOP As Integer = 256

        'TODO: Step 03: Draw the field here

        'TODO: Step 07: Add score drawing code to this location
    End Sub

End Module
