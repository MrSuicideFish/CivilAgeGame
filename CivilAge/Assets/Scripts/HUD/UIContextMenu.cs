using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

public class UIContextMenu : MonoBehaviour
{
    public struct ContextMenuContent
    {
        public string Name;
        public Delegate Command;
    }

    private ContextMenuContent[] Content;

    public static void Display( ContextMenuContent[] newContent, out RectTransform contextMenuRect )
    {
        //Create empty panel
        var newPanel = GameObjectManager.GetObject( "EmptyPanel" );
        newPanel.transform.SetParent( SessionGameManager.SessionCanvas.transform );

        var newMenu = newPanel.AddComponent<UIContextMenu>( );
        newMenu.Content = newContent;

        contextMenuRect = newPanel.GetComponent<RectTransform>( );
    }

    void Start( )
    {

    }
}