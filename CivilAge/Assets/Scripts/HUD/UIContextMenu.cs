using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;
using System.Collections;

public struct ContextMenutTargetGroup
{
    public string Name;
    public ContextMenuCommand GlobalCommand;

}
public struct ContextMenuTarget
{
    public string Name;
    public Texture2D Icon;
    public ContextMenuCommand[] Commands;
    public WorldActor TargetActor;

    public ContextMenuTarget( string name, WorldActor targetActor )
    {
        Name = name;
        Icon = null;
        Commands = null;
        TargetActor = targetActor;
    }

    public ContextMenuTarget( string name, Texture2D icon, WorldActor targetActor )
    {
        Name = name;
        Icon = icon;
        Commands = null;
        TargetActor = targetActor;
    }

    public ContextMenuTarget( string name, Texture2D icon, ContextMenuCommand[] commands, WorldActor targetActor )
    {
        Name = name;
        Icon = icon;
        Commands = commands;
        TargetActor = targetActor;
    }
}

public struct ContextMenuCommand
{
    public string Name;
    public Delegate Command;

    public ContextMenuCommand( string name, Delegate command )
    {
        Name = name;
        Command = command;
    }
}

public enum ContextTarget
{
    ACTOR,
    POSITION
}

public sealed class UIContextMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private ContextMenuTarget[] Targets;
    private ContextMenuItem[] ContextMenuItems;

    private RectTransform   Rect,
                            SelectionRect;

    private UIContextMenu   Parent,
                            Child;

    private int SelectedIdx = 0;

    private float EdgePadding = 10.0f;

    public static UIContextMenu Current;
    public static bool IsHovering { get; private set; }

    public static void Display( ContextMenuTarget[] targets, out RectTransform contextMenuRect, ContextTarget targetType = ContextTarget.ACTOR )
    {
        //Close existing panels
        if ( Current )
        {
            Close( );
        }

        //Create empty panel
        var newPanel = GameObjectManager.GetObject( "EmptyPanel" );
        newPanel.transform.SetParent( SessionGameManager.SessionCanvas.transform, false );
        newPanel.SetActive( true );

        var newMenu = newPanel.AddComponent<UIContextMenu>( );
        newMenu.Targets = targets;

        contextMenuRect = newPanel.GetComponent<RectTransform>( );

        Current = newMenu;
        Current.InitMenu( );
    }

    public static void Close( )
    {
        if(Current != null )
        {
            GameObject.Destroy( Current.gameObject );
        }
    }

    private void InitMenu( )
    {
        //Calculate groups
        List<List<ContextMenuTarget>> targetGroups = new List<List<ContextMenuTarget>>( );

        foreach(ContextMenuTarget target in Targets )
        {
            if(targetGroups.Count <= 0 )
            {
                var newGroup = new List<ContextMenuTarget>( );
                newGroup.Add( target );

                targetGroups.Add( newGroup );
            }
            else
            {
                bool foundGroup = false;
                for ( int i = 0; i < targetGroups.Count; i++ )
                {
                    if(targetGroups[i][0].TargetActor.GetType() == target.TargetActor.GetType( ) )
                    {
                        if ( !targetGroups[i].Contains( target ) )
                        {
                            targetGroups[i].Add( target );
                        }

                        foundGroup = true;
                    }
                }

                if ( !foundGroup )
                {
                    var newGroup = new List<ContextMenuTarget>( );
                    newGroup.Add( target );

                    targetGroups.Add( newGroup );
                }
            }
        }

        ContextMenuItems = new ContextMenuItem[targetGroups.Count];

        Rect = GetComponent<RectTransform>( );
        Rect.SetSizeWithCurrentAnchors( RectTransform.Axis.Horizontal, 250 + ( EdgePadding * 2 ) );
        Rect.SetSizeWithCurrentAnchors( RectTransform.Axis.Vertical, ( targetGroups.Count * 50 ) + ( EdgePadding * 2 ) );

        SelectionRect = GameObjectManager.GetObject( "EmptyImage" ).GetComponent<RectTransform>( );
        SelectionRect.transform.SetParent( Rect, false );

        if ( targetGroups.Count == 1 )
        {
            //Add menu item for each valid target[]
            for ( int i = 0; i < targetGroups.Count; i++ )
            {
                ContextMenuItem newTargetBtn = GameObjectManager.GetObject( "EmptyText" ).AddComponent<ContextMenuItem>( );
                newTargetBtn.transform.SetParent( Rect, false );
                newTargetBtn.GetComponent<RectTransform>( ).anchoredPosition = new Vector2( 0, -( ( i * 50 ) + EdgePadding ) );
                newTargetBtn.GetComponent<Text>( ).text = targetGroups[i][0].TargetActor.GetType( ).ToString( );
                newTargetBtn.GetComponent<Text>( ).fontSize = 32;

                newTargetBtn.gameObject.SetActive( true );
                ContextMenuItems[i] = newTargetBtn;
            }
        }
        else
        {
            //Add menu item for each valid target[]
            for ( int i = 0; i < targetGroups.Count; i++ )
            {
                ContextMenuItem newTargetBtn = GameObjectManager.GetObject( "EmptyText" ).AddComponent<ContextMenuItem>( );
                newTargetBtn.transform.SetParent( Rect, false );
                newTargetBtn.GetComponent<RectTransform>( ).anchoredPosition = new Vector2( 0, -( ( i * 50 ) + EdgePadding ) );
                newTargetBtn.GetComponent<Text>( ).text = targetGroups[i][0].TargetActor.GetType( ).ToString( );
                newTargetBtn.GetComponent<Text>( ).fontSize = 32;

                newTargetBtn.gameObject.SetActive( true );
                ContextMenuItems[i] = newTargetBtn;
            }
        }
    }

    private void AddChild( ) { }
    private void RemoveChild( ) { }
    private void SelectTarget( ) { }
    private void SelectCommand( ) { }

    void Update( )
    {
        if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown( 1 ) )
        {
            if ( !IsHovering )
            {
                Close( );
            }
        }

        if ( IsHovering )
        {
            SelectionRect.gameObject.SetActive( false );
            for ( int i = 0; i < ContextMenuItems.Length; i++ )
            {
                ContextMenuItems[i].GetComponent<Text>( ).color = ContextMenuItems[i].IsHovering ? SessionGameManager.HoveredActorHightlightColor : Color.white;
                if ( ContextMenuItems[i].IsHovering )
                {
                    SelectionRect.gameObject.SetActive( true );
                    SelectionRect.SetParent( ContextMenuItems[i].transform, false );
                }
            }
        }
        else
        {
            SelectionRect.gameObject.SetActive( false );
        }
    }

    public void OnPointerEnter( PointerEventData eventData )
    {
        IsHovering = true;
    }

    public void OnPointerExit( PointerEventData eventData )
    {
        IsHovering = false;
    }
}