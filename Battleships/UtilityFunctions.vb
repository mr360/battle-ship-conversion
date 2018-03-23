''' <summary>
''' This includes a number of utility methods for
''' drawing and interacting with the Mouse.
''' </summary>
Module UtilityFunctions
    Public Const FIELD_TOP As Integer = 122
    Public Const FIELD_LEFT As Integer = 349
    Public Const FIELD_WIDTH As Integer = 418
    Public Const FIELD_HEIGHT As Integer = 418
    Public Const CELL_WIDTH As Integer = 40
    Public Const CELL_HEIGHT As Integer = 40
    Public Const CELL_GAP As Integer = 2

    Private Const SMALL_FIELD_LEFT As Integer = 39
    Private Const SMALL_FIELD_TOP As Integer = 373
    Private Const SMALL_FIELD_WIDTH As Integer = 166
    Private Const SMALL_FIELD_HEIGHT As Integer = 166
    Private Const SMALL_FIELD_CELL_WIDTH As Integer = 13
    Private Const SMALL_FIELD_CELL_HEIGHT As Integer = 13
    Private Const SMALL_FIELD_CELL_GAP As Integer = 4

    Public Const SHIP_GAP As Integer = 3

    Private ReadOnly MISS As Color = Color.FromArgb(255, 1, 147, 220)
    Private ReadOnly HIT As Color = Color.FromArgb(255, 169, 24, 37)
    Private ReadOnly SEA As Color = Color.FromArgb(255, 6, 60, 94)
    Private ReadOnly SHIP As Color = Color.Gray

    Private ReadOnly OUTLINE_COLOR As Color = Color.FromArgb(255, 5, 55, 88)
    Private ReadOnly SHIP_FILL_COLOR As Color = Color.Gray
    Private ReadOnly SHIP_OUTLINE_COLOR As Color = Color.White
    Private ReadOnly MESSAGE_COLOR As Color = Color.FromArgb(255, 2, 167, 252)

    Public Const MESSAGE_TOP As Integer = 548

    Public Const ANIMATION_CELLS As Integer = 7
    Public Const FRAMES_PER_CELL As Integer = 3

    ''' <summary>
    ''' Determines if the mouse is in a given rectangle.
    ''' </summary>
    ''' <param name="x">the x location to check</param>
    ''' <param name="y">the y location to check</param>
    ''' <param name="w">the width to check</param>
    ''' <param name="h">the height to check</param>
    ''' <returns>true if the mouse is in the area checked</returns>
    Public Function IsMouseInRectangle(ByVal x As Integer, ByVal y As Integer, ByVal w As Integer, ByVal h As Integer) As Boolean
        Dim mouse As Point2D
        Dim result As Boolean = False

        mouse = Input.GetMousePosition()

        'if the mouse is inline with the button horizontally
        If mouse.X >= x And mouse.X <= x + w Then
            'Check vertical position
            If mouse.Y >= y And mouse.Y <= y + h Then
                result = True
            End If
        End If

        Return result
    End Function

    ''' <summary>
    ''' Draws a large field using the grid and the indicated player's ships.
    ''' </summary>
    ''' <param name="grid">the grid to draw</param>
    ''' <param name="thePlayer">the players ships to show</param>
    ''' <param name="showShips">indicates if the ships should be shown</param>
    Public Sub DrawField(ByVal grid As ISeaGrid, ByVal thePlayer As Player, ByVal showShips As Boolean)
        DrawCustomField(grid, thePlayer, False)
        If showShips Then
            DrawShips(thePlayer, False)
        End If
    End Sub

    ''' <summary>
    ''' Draws a small field, showing the attacks made and the locations of the player's ships
    ''' </summary>
    ''' <param name="grid">the grid to show</param>
    ''' <param name="thePlayer">the player to show the ships of</param>
    Public Sub DrawSmallField(ByVal grid As ISeaGrid, ByVal thePlayer As Player)
        DrawCustomField(grid, thePlayer, True)
        DrawShips(thePlayer, True)
    End Sub

    ''' <summary>
    ''' Set the dimensions of the different aspects of the grid
    ''' </summary>
    ''' <param name="small">is it the small grid?</param>
    ''' <param name="left">the left position to set</param>
    ''' <param name="top">the top position to set</param>
    ''' <param name="width">the width position to set</param>
    ''' <param name="height">the height position to set</param>
    ''' <param name="cellWidth">the cellWidth position to set</param>
    ''' <param name="cellHeight">the cellHeight position to set</param>
    ''' <param name="cellGap">the cellGap position to set</param>
    Private Sub SetDimensions(ByVal small As Boolean, ByRef left As Integer, ByRef top As Integer, ByRef width As Integer, ByRef height As Integer, ByRef cellWidth As Integer, ByRef cellHeight As Integer, ByRef cellGap As Integer)
        If small Then
            Left = SMALL_FIELD_LEFT
            top = SMALL_FIELD_TOP
            width = SMALL_FIELD_WIDTH
            height = SMALL_FIELD_HEIGHT
            cellWidth = SMALL_FIELD_CELL_WIDTH
            cellHeight = SMALL_FIELD_CELL_HEIGHT
            cellGap = SMALL_FIELD_CELL_GAP
        Else
            Left = FIELD_LEFT
            top = FIELD_TOP
            width = FIELD_WIDTH
            height = FIELD_HEIGHT
            cellWidth = CELL_WIDTH
            cellHeight = CELL_HEIGHT
            cellGap = CELL_GAP
        End If
    End Sub

    ''' <summary>
    ''' Draws the player's grid and ships.
    ''' </summary>
    ''' <param name="grid">the grid to show</param>
    ''' <param name="thePlayer">the player to show the ships of</param>
    ''' <param name="small">true if the small grid is shown</param>
    Private Sub DrawCustomField(ByVal grid As ISeaGrid, ByVal thePlayer As Player, ByVal small As Boolean)
        Dim left As Integer, top As Integer, width As Integer, height As Integer
        Dim cellWidth As Integer, cellHeight As Integer, cellGap As Integer

        Dim rowTop As Integer
        Dim colLeft As Integer

        SetDimensions(small, left, top, width, height, cellWidth, cellHeight, cellGap)

        'TODO: Step 07: remove the following line of code.
        Graphics.FillRectangle(Color.Blue, left, top, width, height)

        'Draw the grid
        For row As Integer = 0 To 9
            rowTop = top + (cellGap + cellHeight) * row

            For col As Integer = 0 To 9
                colLeft = left + (cellGap + cellWidth) * col

                Dim fillColor As Color
                Dim draw As Boolean

                draw = True

                Select Case grid.Item(row, col)
                    Case TileView.Miss
                        fillColor = MISS
                    Case TileView.Hit
                        fillColor = HIT
                    Case TileView.Sea, TileView.Ship
                        If small Then fillColor = SEA Else draw = False
                End Select

                If draw Then
                    Graphics.FillRectangle(fillColor, colLeft, rowTop, cellWidth, cellHeight)
                End If
                'TODO: Step 07: Remove the following line of code
                Graphics.DrawRectangle(OUTLINE_COLOR, colLeft, rowTop, cellWidth, cellHeight)
            Next
        Next
    End Sub

    ''' <summary>
    ''' Draws the ships over a given field.
    ''' </summary>
    ''' <param name="thePlayer">the player whos ships are to be drawn</param>
    ''' <param name="small">indicates if this is being drawn to the small grid</param>
    ''' <remarks></remarks>
    Private Sub DrawShips(ByVal thePlayer As Player, ByVal small As Boolean)
        Dim left As Integer, top As Integer, width As Integer, height As Integer
        Dim cellWidth As Integer, cellHeight As Integer, cellGap As Integer

        Dim rowTop As Integer
        Dim colLeft As Integer

        Dim shipHeight, shipWidth As Integer
        Dim shipName As String

        SetDimensions(small, left, top, width, height, cellWidth, cellHeight, cellGap)

        'Draw the ships
        For Each s As Ship In thePlayer
            If s Is Nothing OrElse Not s.IsDeployed Then Continue For

            rowTop = top + (cellGap + cellHeight) * s.Row + SHIP_GAP
            colLeft = left + (cellGap + cellWidth) * s.Column + SHIP_GAP

            If s.Direction = Direction.LeftRight Then
                shipName = "ShipLR" & s.Size
                shipHeight = cellHeight - (SHIP_GAP * 2)
                shipWidth = (cellWidth + cellGap) * s.Size - (SHIP_GAP * 2) - cellGap
            Else
                'Up down
                shipName = "ShipUD" & s.Size
                shipHeight = (cellHeight + cellGap) * s.Size - (SHIP_GAP * 2) - cellGap
                shipWidth = cellWidth - (SHIP_GAP * 2)
            End If

            'TODO: Step 09: Add code around the following two lines to draw the ship images
            Graphics.FillRectangle(SHIP_FILL_COLOR, colLeft, rowTop, shipWidth, shipHeight)
            Graphics.DrawRectangle(SHIP_OUTLINE_COLOR, colLeft, rowTop, shipWidth, shipHeight)
        Next
    End Sub

    Private _message As String

    ''' <summary>
    ''' The message to display
    ''' </summary>
    ''' <value>The message to display</value>
    ''' <returns>The message to display</returns>
    Public Property Message() As String
        Get
            Return _message
        End Get
        Set(ByVal value As String)
            _message = value
        End Set
    End Property

    ''' <summary>
    ''' Draws the message to the screen
    ''' </summary>
    Public Sub DrawMessage()
        Text.DrawText(Message, MESSAGE_COLOR, GameFont("Courier"), FIELD_LEFT, MESSAGE_TOP)
    End Sub

    ''' <summary>
    ''' Draws the background for the current state of the game
    ''' </summary>
    Public Sub DrawBackground()
        'TODO: Step 07: Draw background images here
        Graphics.ClearScreen()

        Text.DrawFramerate(675, 585, GameFont("CourierSmall"))
    End Sub

    ''' <summary>
    ''' Add an explosion to the game.
    ''' </summary>
    ''' <param name="row">the row location of the explosion</param>
    ''' <param name="col">the column location of the explosion</param>
    Public Sub AddExplosion(ByVal row As Integer, ByVal col As Integer)
        AddAnimation(row, col, "Explosion")
    End Sub

    ''' <summary>
    ''' Add an splash to the game.
    ''' </summary>
    ''' <param name="row">the row location of the splash</param>
    ''' <param name="col">the column location of the splash</param>
    Public Sub AddSplash(ByVal row As Integer, ByVal col As Integer)
        AddAnimation(row, col, "Splash")
    End Sub

    Private _Animations As New List(Of Sprite)()

    ''' <summary>
    ''' Adds a animation to the list of animations.
    ''' </summary>
    ''' <param name="row">the row of the animation</param>
    ''' <param name="col">the column of the animation</param>
    ''' <param name="image">the animation's image</param>
    Private Sub AddAnimation(ByVal row As Integer, ByVal col As Integer, ByVal image As String)
        'TODO: Step 11: Create the sprite and add it to the list of sprites
    End Sub

    ''' <summary>
    ''' Updates all of the animations in _animations, removing those that have ended.
    ''' </summary>
    Public Sub UpdateAnimations()
        Dim ended As New List(Of Sprite)()
        For Each s As Sprite In _Animations
            Graphics.UpdateSprite(s)
            If s.hasEnded Then
                ended.Add(s)
            End If
        Next

        For Each s As Sprite In ended
            _Animations.Remove(s)
            Graphics.FreeSprite(s)
        Next
    End Sub

    ''' <summary>
    ''' Draws all of the animations in _animations
    ''' </summary>
    Public Sub DrawAnimations()
        'TODO: Step 11: Draw all animations
    End Sub

    ''' <summary>
    ''' Draw an entire animation sequence.
    ''' </summary>
    Public Sub DrawAnimationSequence()
        For i As Integer = 1 To ANIMATION_CELLS * FRAMES_PER_CELL
            UpdateAnimations()
            DrawScreen()
        Next
    End Sub
End Module
