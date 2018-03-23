Imports SwinGame

''' <summary>
''' The DeploymentController controls the players actions
''' during the deployment phase.
''' </summary>
Module DeploymentController
    Private Const SHIPS_TOP As Integer = 98
    Private Const SHIPS_LEFT As Integer = 20
    Private Const SHIPS_HEIGHT As Integer = 90
    Private Const SHIPS_WIDTH As Integer = 300

    Private Const TOP_BUTTONS_TOP As Integer = 72
    Private Const TOP_BUTTONS_HEIGHT As Integer = 46

    Private Const PLAY_BUTTON_LEFT As Integer = 693
    Private Const PLAY_BUTTON_WIDTH As Integer = 80    

    Private Const DIR_BUTTONS_LEFT As Integer = 350
    Private Const UP_DOWN_BUTTON_LEFT As Integer = 410
    Private Const LEFT_RIGHT_BUTTON_LEFT As Integer = 350

    Private Const RANDOM_BUTTON_LEFT As Integer = 547
    Private Const RANDOM_BUTTON_WIDTH As Integer = 51

    Private Const DIR_BUTTONS_WIDTH As Integer = 47

    Private Const TEXT_OFFSET As Integer = 5

    Private _currentDirection As Direction = Direction.UpDown
    Private _selectedShip As ShipName = ShipName.Tug

    ''' <summary>
    ''' Handles user input for the Deployment phase of the game.
    ''' </summary>
    ''' <remarks>
    ''' Involves selecting the ships, deloying ships, changing the direction
    ''' of the ships to add, randomising deployment, end then ending
    ''' deployment
    ''' </remarks>
    Public Sub HandleDeploymentInput()
        If Input.WasKeyTyped(Keys.VK_ESCAPE) Then
            AddNewState(GameState.ViewingGameMenu)
        End If

        If Input.WasKeyTyped(Keys.VK_UP) Or Input.WasKeyTyped(Keys.VK_DOWN) Then
            _currentDirection = Direction.UpDown
        End If
        If Input.WasKeyTyped(Keys.VK_LEFT) Or Input.WasKeyTyped(Keys.VK_RIGHT) Then
            _currentDirection = Direction.LeftRight
        End If

        'TODO: Step 02: Add randomise on the R key being pressed

        If Input.MouseWasClicked(MouseButton.LeftButton) Then
            Dim selected As ShipName
            selected = GetShipMouseIsOver()
            If selected <> ShipName.None Then
                _selectedShip = selected
            Else
                DoDeployClick()
            End If

            If HumanPlayer.ReadyToDeploy And IsMouseInRectangle(PLAY_BUTTON_LEFT, TOP_BUTTONS_TOP, PLAY_BUTTON_WIDTH, TOP_BUTTONS_HEIGHT) Then
                'TODO: Step 02: End deployment here
            ElseIf IsMouseInRectangle(UP_DOWN_BUTTON_LEFT, TOP_BUTTONS_TOP, DIR_BUTTONS_WIDTH, TOP_BUTTONS_HEIGHT) Then
                'TODO: Step 02: Set _currentDirection to Direction.UpDown
            ElseIf IsMouseInRectangle(LEFT_RIGHT_BUTTON_LEFT, TOP_BUTTONS_TOP, DIR_BUTTONS_WIDTH, TOP_BUTTONS_HEIGHT) Then
                'TODO: Step 02: Set _currentDirection to Direction.LeftRight
            ElseIf IsMouseInRectangle(RANDOM_BUTTON_LEFT, TOP_BUTTONS_TOP, RANDOM_BUTTON_WIDTH, TOP_BUTTONS_HEIGHT) Then
                'TODO: Step 02: call HumanPlayer.RandomizeDeployment()
            End If
        End If
    End Sub

    ''' <summary>
    ''' The user has clicked somewhere on the screen, check if its is a deployment and deploy
    ''' the current ship if that is the case.
    ''' </summary>
    ''' <remarks>
    ''' If the click is in the grid it deploys to the selected location
    ''' with the indicated direction
    ''' </remarks>
    Private Sub DoDeployClick()
        Dim mouse As Point2D

        mouse = Input.GetMousePosition()

        'Calculate the row/col clicked
        Dim row, col As Integer
        row = Convert.ToInt32(Math.Floor((mouse.Y - FIELD_TOP) / (CELL_HEIGHT + CELL_GAP)))
        col = Convert.ToInt32(Math.Floor((mouse.X - FIELD_LEFT) / (CELL_WIDTH + CELL_GAP)))

        If row >= 0 And row < HumanPlayer.PlayerGrid.Height Then
            If col >= 0 And col < HumanPlayer.PlayerGrid.Width Then
                'if in the area try to deploy
                Try
                    HumanPlayer.PlayerGrid.MoveShip(row, col, _selectedShip, _currentDirection)
                    'TODO: Step 05: Play Deploy sound here
                    Message = "Ship deployed"
                Catch ex As Exception
                    'TODO: Step 05: Play Error sound here
                    Message = ex.Message
                End Try
            End If
        End If
    End Sub

    ''' <summary>
    ''' Draws the deployment screen showing the field and the ships
    ''' that the player can deploy.
    ''' </summary>
    Public Sub DrawDeployment()
        'Draw a large field (grid) showing the Player's ships - drawn in the lower right corner
        DrawField(HumanPlayer.PlayerGrid, HumanPlayer, True)

        'Draw the text message below the field - bottom left of field
        DrawMessage()

        'Draw the Randomise field button - middle top of field
        'TODO: Step 08: Change the following to draw button
        Text.DrawText("RF", Color.White, GameFont("Menu"), RANDOM_BUTTON_LEFT, TOP_BUTTONS_TOP)

        'Draw the Left/Right and Up/Down buttons - left top of field
        If _currentDirection = Direction.LeftRight Then
            'TODO: Step 08: Change the following to draw button
            Text.DrawText("U/D", Color.Gray, GameFont("Menu"), UP_DOWN_BUTTON_LEFT, TOP_BUTTONS_TOP)
            Text.DrawText("L/R", Color.White, GameFont("Menu"), LEFT_RIGHT_BUTTON_LEFT, TOP_BUTTONS_TOP)
        Else
            'TODO: Step 08: Change the following to draw button
            Text.DrawText("U/D", Color.White, GameFont("Menu"), UP_DOWN_BUTTON_LEFT, TOP_BUTTONS_TOP)
            Text.DrawText("L/R", Color.Gray, GameFont("Menu"), LEFT_RIGHT_BUTTON_LEFT, TOP_BUTTONS_TOP)
        End If

        'DrawShips to select
        For Each sn As ShipName In [Enum].GetValues(GetType(ShipName))
            Dim i As Integer
            i = Int(sn) - 1 'Get the ship's number and subtract 1

            If i >= 0 Then
                If sn = _selectedShip Then
                    'TODO: Step 08: Change the following to draw button
                    Graphics.FillRectangle(Color.LightBlue, SHIPS_LEFT, SHIPS_TOP + i * SHIPS_HEIGHT, SHIPS_WIDTH, SHIPS_HEIGHT)
                Else
                    'TODO: Step 08: Remove the following and the else above
                    Graphics.FillRectangle(Color.Gray, SHIPS_LEFT, SHIPS_TOP + i * SHIPS_HEIGHT, SHIPS_WIDTH, SHIPS_HEIGHT)
                End If

                'TODO: Step 08: Remove the following
                Text.DrawText(sn.ToString(), Color.Black, GameFont("Courier"), SHIPS_LEFT + TEXT_OFFSET, SHIPS_TOP + i * SHIPS_HEIGHT)
                Graphics.DrawRectangle(Color.Black, SHIPS_LEFT, SHIPS_TOP + i * SHIPS_HEIGHT, SHIPS_WIDTH, SHIPS_HEIGHT)
            End If
        Next

        'Draw the play button - top right of field
        If HumanPlayer.ReadyToDeploy Then
            'TODO: Step 08: Change the following to draw  a button
            Text.DrawText("PLAY", Color.White, GameFont("Menu"), PLAY_BUTTON_LEFT + TEXT_OFFSET, TOP_BUTTONS_TOP)
        End If
    End Sub

    ''' <summary>
    ''' Gets the ship that the mouse is currently over in the selection panel.
    ''' </summary>
    ''' <returns>The ship selected or none</returns>
    Private Function GetShipMouseIsOver() As ShipName
        For Each sn As ShipName In [Enum].GetValues(GetType(ShipName))
            Dim i As Integer
            i = Int(sn) - 1

            If IsMouseInRectangle(SHIPS_LEFT, SHIPS_TOP + i * SHIPS_HEIGHT, SHIPS_WIDTH, SHIPS_HEIGHT) Then
                Return sn
            End If
        Next

        Return ShipName.None
    End Function
End Module
