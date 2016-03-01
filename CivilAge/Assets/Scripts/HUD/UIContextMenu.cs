/// <summary>
/// UI CONTEXT MENU
/// ---------------------------------------------
/// The UIContextMenu class handles the single-instanced right-click context menu.
/// The primary purpose of the menu is to handle the intiation and display of the window
/// as well as the initiaion of it's child objects.
/// 
/// DO NOT USE CONTEXT MENU TO:
/// 
/// 1. Add WorldActor Logic.
/// 2. Override Input Logic.
/// 3. Override UI Logic.
/// </summary>

//.NET Namespaces
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

//Unity3D Namespaces
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// ContextData contains all of the information about the event that led up
/// to the display of the ContextWindow. Such as the object that was right-clicked,
/// the position of the mouse, and the selected objects in question.
/// </summary>
public struct ContextData
{
    public WorldActor[] SelectedObjects;
    public WorldActor TargetObject;
    public Vector3 TargetPosition;
}

public struct ContextMenuTargetGroup
{
    public string Name;
    public ContextMenuCommand[] GlobalCommands;
    public List<WorldActor> TargetActors;
    public List<ContextMenuTarget> Targets;
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
    public static UIContextMenu Current         { get; private set; }
    public static ContextData   CurrentEvent    { get; private set; }
    public static bool          IsHovering      { get; private set; }

    private int SelectedIdx = 0;

    private float   EdgePadding = 4.0f,
                    HoverTimer = 0.0f;

    private ContextMenuTarget[] Targets;
    private ContextMenuItem[] ContextMenuItems;

    private ContextMenuItem CurrentHoverItem,
                            LastHoverItem;

    private UIContextMenu ChildMenu;

    private RectTransform   Rect,
                            SelectionRect;

    private UIContextMenu   Parent,
                            Child;

    //Initialization
    private void InitMenu( )
    {
        //Calculate groups
        List<ContextMenuTargetGroup> targetGroups = new List<ContextMenuTargetGroup>( );

        foreach ( ContextMenuTarget target in Targets )
        {
            if ( targetGroups.Count <= 0 )
            {
                var newGroup = new ContextMenuTargetGroup( );
                newGroup.Name = target.Name;

                //add commands from target
                newGroup.GlobalCommands = target.Commands;

                //create group->target actors list
                newGroup.TargetActors = new List<WorldActor>( );
                newGroup.TargetActors.Add( target.TargetActor );

                newGroup.Targets = Targets.ToList( );

                //add new group to list
                targetGroups.Add( newGroup );
            }
            else
            {
                bool foundGroup = false;
                for ( int i = 0; i < targetGroups.Count; i++ )
                {
                    if ( targetGroups[i].TargetActors[0].GetType( ) == target.TargetActor.GetType( ) )
                    {
                        if ( !targetGroups[i].TargetActors.Contains( target.TargetActor ) )
                        {
                            targetGroups[i].TargetActors.Add( target.TargetActor );
                            targetGroups[i].Targets.Add( target );
                        }

                        foundGroup = true;
                    }
                }

                if ( !foundGroup )
                {
                    var newGroup = new ContextMenuTargetGroup( );
                    newGroup.Name = target.Name;

                    //add commands from target
                    newGroup.GlobalCommands = target.Commands;

                    newGroup.TargetActors = new List<WorldActor>( );
                    newGroup.TargetActors.Add( target.TargetActor );

                    newGroup.Targets = new List<ContextMenuTarget>( );
                    newGroup.Targets.Add( target );

                    targetGroups.Add( newGroup );
                }
            }
        }

        ContextMenuItems = new ContextMenuItem[targetGroups.Count];

        Rect = GetComponent<RectTransform>( );
        Rect.SetSizeWithCurrentAnchors( RectTransform.Axis.Horizontal, 250 + ( EdgePadding * 2 ) );

        if ( targetGroups.Count != 1 )
            Rect.SetSizeWithCurrentAnchors( RectTransform.Axis.Vertical, ( targetGroups.Count * 50 ) + ( EdgePadding * 2 ) );
        else
            Rect.SetSizeWithCurrentAnchors( RectTransform.Axis.Vertical, ( targetGroups[0].GlobalCommands.Length * 50 ) + ( EdgePadding * 2 ) );

        SelectionRect = GameObjectManager.GetObject( "EmptyImage" ).GetComponent<RectTransform>( );
        SelectionRect.transform.SetParent( Rect, false );

        if ( targetGroups.Count == 1 )
        {
            ContextMenuItems = new ContextMenuItem[targetGroups[0].GlobalCommands.Length];

            //Add commands from group
            for ( int i = 0; i < targetGroups[0].GlobalCommands.Length; i++ )
            {
                //Create and initialize button data
                ContextMenuItem newTargetBtn = GameObjectManager.GetObject( "EmptyText" ).AddComponent<ContextMenuItem>( );
                newTargetBtn.ItemType = ContextMenuItem.ContextMenuItemType.COMMAND;
                newTargetBtn.TargetCommand = targetGroups[0].GlobalCommands[i].Command;

                newTargetBtn.transform.SetParent( Rect, false );

                //Position button
                newTargetBtn.GetComponent<RectTransform>( ).anchoredPosition = new Vector2( 0, -( ( i * 50 ) + EdgePadding ) );

                //Set button text
                newTargetBtn.GetComponent<Text>( ).text = targetGroups[0].GlobalCommands[i].Name;
                newTargetBtn.GetComponent<Text>( ).fontSize = 16;

                //Show
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
                newTargetBtn.ItemType = ContextMenuItem.ContextMenuItemType.TARGET;
                newTargetBtn.Targets = targetGroups[i].Targets.ToArray( );

                newTargetBtn.transform.SetParent( Rect, false );
                newTargetBtn.GetComponent<RectTransform>( ).anchoredPosition = new Vector2( 0, -( ( i * 50 ) + EdgePadding ) );
                newTargetBtn.GetComponent<Text>( ).text = targetGroups[i].Name;
                newTargetBtn.GetComponent<Text>( ).fontSize = 16;
                newTargetBtn.gameObject.SetActive( true );

                //add continue arrow
                var arrow = GameObjectManager.GetObject( "EmptyImage" ).GetComponent<RectTransform>( );
                arrow.SetParent( newTargetBtn.GetComponent<RectTransform>( ), false );
                arrow.gameObject.SetActive( true );
                arrow.GetComponent<Image>( ).color = Color.magenta;

                //Reposition arrow
                arrow.anchorMin = new Vector2( 1, 0.5f );
                arrow.anchorMax = new Vector2( 1, 0.5f );
                arrow.pivot = new Vector2( 1, 0.5f );
                arrow.anchoredPosition = Vector2.zero;
                arrow.SetSizeWithCurrentAnchors( RectTransform.Axis.Horizontal, 30 );
                arrow.SetSizeWithCurrentAnchors( RectTransform.Axis.Vertical, 30 );

                ContextMenuItems[i] = newTargetBtn;
            }
        }


    }

    //Visibility
    public static void Display( ContextMenuTarget[] targets, out RectTransform contextMenuRect )
    {
        //Close existing panels
        if ( Current )
        {
            Close( );
        }

        //Create empty panel
        var newPanel = GameObjectManager.GetObject( "EmptyPanel" ).AddComponent<UIContextMenu>( );
        newPanel.GetComponent<RectTransform>( ).SetParent( SessionGameManager.SessionCanvas.transform, false );
        newPanel.gameObject.SetActive( true );
        newPanel.Targets = targets;

        contextMenuRect = newPanel.GetComponent<RectTransform>( );

        //Create Context Data
        var newEv = new ContextData( );
        newEv.SelectedObjects = PlayerInputController.SelectedActors;
        newEv.TargetPosition = PlayerInputController.MouseWorldPos;

        if ( PlayerInputController.HoveredActor.transform )
        {
            newEv.TargetObject = PlayerInputController.HoveredActor.transform.GetComponent<WorldActor>( );
        }

        CurrentEvent    = newEv;
        Current         = newPanel;

        Current.InitMenu( );
    }

    public static void Close( )
    {
        if ( Current != null )
        {
            IsHovering = false;
            GameObject.Destroy( Current.gameObject );
        }
    }

    //Window Collapse/Minimize
    private void DisplayChild( ContextMenuTarget[] targets, Vector2 windowPos )
    {
        //Create empty panel
        var childMenu = ChildMenu ? ChildMenu : GameObjectManager.GetObject( "EmptyPanel" ).AddComponent<UIContextMenu>( );
        childMenu.gameObject.SetActive( true );
        childMenu.Targets = targets;

        RectTransform childMenuRect = childMenu.GetComponent<RectTransform>( );
        childMenuRect.anchoredPosition = windowPos;
        childMenuRect.SetParent( Current.transform, false );

        //Create Context Data
        //var newEv = new ContextData( );
        //newEv.SelectedObjects   = CurrentEvent.SelectedObjects;
        //newEv.TargetPosition    = CurrentEvent.TargetPosition;
        //newEv.TargetObject      = CurrentEvent.TargetObject;

        ChildMenu = childMenu;
        ChildMenu.InitMenu( );
    }

    //Click Events
    public void OnPointerEnter( PointerEventData eventData )
    {
        IsHovering = true;
    }
    public void OnPointerExit( PointerEventData eventData )
    {
        IsHovering = false;
    }

    //Tick
    void Update( )
    {
        if( (Input.GetMouseButton( 0 ) || 
            Input.GetMouseButton( 1 )) &&
            !IsHovering )
        {
            Close( );
        }

        var ChildIsValid = false;

        LastHoverItem = null;
        if ( IsHovering )
        {
            SelectionRect.gameObject.SetActive( false );
            for ( int i = 0; i < ContextMenuItems.Length; i++ )
            {
                ContextMenuItems[i].GetComponent<Text>( ).color = ContextMenuItems[i].IsHovering ? CivColor.HoverHighlightColor.ToColor( ) : Color.white;
                if ( ContextMenuItems[i].IsHovering )
                {
                    SelectionRect.gameObject.SetActive( true );
                    SelectionRect.SetParent( ContextMenuItems[i].transform, false );

                    if ( ContextMenuItems[i].ItemType == ContextMenuItem.ContextMenuItemType.TARGET )
                    {
                        LastHoverItem = CurrentHoverItem;
                        CurrentHoverItem = ContextMenuItems[i];

                        if ( CurrentHoverItem == LastHoverItem )
                        {
                            HoverTimer += Time.fixedDeltaTime;
                            ChildIsValid = true;
                        }
                        else
                        {
                            HoverTimer = 0;
                        }
                    }
                }
            }

            if ( !ChildIsValid )
            {
                if ( ChildMenu )
                {
                    GameObject.Destroy( ChildMenu );
                }
            }

            //Open next context
            if ( HoverTimer >= 2 && ChildMenu == null )
            {
                print( "Opening next window" );
                DisplayChild( CurrentHoverItem.Targets, CurrentHoverItem.transform.position );
            }
        }
        else
        {
            SelectionRect.gameObject.SetActive( false );

            if ( ChildMenu )
            {
                GameObject.Destroy( ChildMenu );
            }

            HoverTimer = 0;
        }
    }
}