Module GameLogic
    Public Sub Main()
        'Opens a new Graphics Window
        Core.OpenGraphicsWindow("Battle Ships", 800, 600)

        'Open Audio Device
        Audio.OpenAudio()

        'Load Resources
        LoadResources()

        'TODO: Step 10: Play music here

        'Game Loop
        Do
            'TODO: Step 01: Handle User Input and Draw Screen
            Core.ProcessEvents() 'TODO: Step 01: Remove this code
        Loop Until SwinGame.Core.WindowCloseRequested() = True Or CurrentState = GameState.Quitting

        'TODO: Step 10: Stop playing music here

        'Free Resources and Close Audio, to end the program.
        FreeResources()
        Audio.CloseAudio()
    End Sub
End Module
