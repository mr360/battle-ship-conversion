//Not finished still working on it


using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using SwinGameSDK;

public class PopUpHit : ResultOfAttack
{

  public PopupHit PopupHit { get; set; }

  myPopup.AllowsTransparency = true;
  
  public event PopupEventHandler Popup
  {
    private void ToolTip1_Popup(Object sender, PopupEventArgs e) 
    {
    System.Text.StringBuilder messageBoxCS = new System.Text.StringBuilder();
    messageBoxCS.AppendFormat("{0} = {1}", "Hit", e.Hit );
    messageBoxCS.AppendLine();
    MessageBox.Show(messageBoxCS.ToString(), "Popup Event" );
    }
  }
  
  myTextBlockPopup.PopupHit = PopupHit.Fade;
  
  }

