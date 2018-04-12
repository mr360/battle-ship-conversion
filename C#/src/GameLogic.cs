// VBConversions Note: VB project level imports
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using SwinGameSDK;

sealed class GameLogic
{
    public static void Main()
    {
      
        SwinGame.OpenGraphicsWindow("Battle Ships", 800, 600);
        
        GameResources.LoadResources();
        
        SwinGame.PlayMusic(GameResources.GameMusic("Background"));
        
   
        do
        {
            GameController.HandleUserInput();
            GameController.DrawScreen();
        } while (!(SwinGame.WindowCloseRequested() == true || GameController.CurrentState == GameState.Quitting));
        
        SwinGame.StopMusic();
        
        //Free Resources and Close Audio, to end the program.
        GameResources.FreeResources();
    }
}


