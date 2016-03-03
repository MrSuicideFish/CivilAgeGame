using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class PlayerInputController : MonoBehaviour
{
    //--Debug----------------
    Texture2D BoxTex;
    GameObject DebugSphereA,
                DebugSphereB,
                DebugSphereC,
                DebugSphereD,
                DebugSphereE;
    //------------------------

    private enum MouseDragMode
    {
        NONE                = -1,
        DRAG_SELECT         = 0,
        DRAG_CAMERA_ROTATE  = 1,
    }

    public static Vector3 MouseWorldPos { get; private set; }
    public static Ray MouseHoverRay { get; private set; }
    public static WorldActor[] SelectedActors { get; private set; }
    public static RaycastHit HoveredActor;

    public float MouseTraceDist = 2.0f;

    private Rect ScreenSelectionRect;

    private MouseDragMode DragMode = MouseDragMode.NONE;

    private float DragTimer = 0.0f;

    private bool IsDragging = false;

    private Vector2 MouseScreenPos,
                    DragStartPosition,
                    DragPosition,
                    DragEndPosition;

    private RectTransform ActionContextMenu;

    ///////////////////////
    // Initiation
    ///////////////////////
    void Awake( )
    {
        SessionGameManager.OnPauseToggled += OnGamePaused;
        SelectedActors = new WorldActor[0];
    }

    ///////////////////////
    // Event Callbacks
    ///////////////////////
    void OnGamePaused( bool enabled )
    {
        //Un-highlight hovered actor
        if ( HoveredActor.transform )
        {
            HoveredActor.transform.GetComponent<MeshRenderer>( ).material.SetFloat( "_OutlineWidth", 0.0f );
            HoveredActor = new RaycastHit( ); //to set to null
        }
    }

    void Update( )
    {
        if ( SessionGameManager.Instance.GameIsPaused ) return;

        //////////////////////
        // Mouse Hover
        /////////////////////
        //Convert mouse screen position to world
        MouseScreenPos = new Vector2( Input.mousePosition.x, Screen.height - Input.mousePosition.y );
        MouseWorldPos = Camera.main.ScreenToWorldPoint( new Vector3( MouseScreenPos.x, Screen.height - MouseScreenPos.y, MouseTraceDist ) );

        //Define ray
        MouseHoverRay = new Ray
            (
                MouseWorldPos, //Origin
                MouseWorldPos - CameraController.CurrentCamera.transform.position //Direction
            );

        //Cast
        var oldHoverActorHit = HoveredActor;
        Physics.Raycast( MouseHoverRay, out HoveredActor, Mathf.Infinity, 1 << 8 );

        //Unhighlight old
        if ( !oldHoverActorHit.Equals( HoveredActor ) && oldHoverActorHit.transform && !oldHoverActorHit.transform.GetComponent<WorldActor>( ).IsSelected )
        {
            oldHoverActorHit.transform.GetComponent<MeshRenderer>( ).material.SetFloat( "_OutlineWidth", 0.0f );
        }

        //Highlight actor
        if ( HoveredActor.transform && !HoveredActor.transform.GetComponent<WorldActor>().IsSelected )
        {
            HoveredActor.transform.GetComponent<MeshRenderer>( ).material.SetFloat( "_OutlineWidth", SessionGameManager.GlobalHighlightWidth );
            HoveredActor.transform.GetComponent<MeshRenderer>( ).material.SetColor( "_HighlightColor", CivColor.HoverHighlightColor.ToColor( ) );
        }

        //////////////////////
        // Mouse Drag
        /////////////////////
        
        // - Begin Drag Select
        if ( Input.GetMouseButtonDown( ( int )MouseDragMode.DRAG_SELECT ) )
        {
            if ( !UIContextMenu.Current || !UIContextMenu.Current.IsHovering )
            {
                ToggleMouseDragMode( MouseDragMode.DRAG_SELECT );

                if ( HoveredActor.transform )
                {
                    SelectedActors = new WorldActor[]
                    {
                        HoveredActor.transform.GetComponent<WorldActor>()
                    };

                    SelectedActors[0].Select( );
                }
            }
        }

        // - End Drag Select
        if ( Input.GetMouseButtonUp( ( int )MouseDragMode.DRAG_SELECT ) )
        {
            if ( !UIContextMenu.Current || !UIContextMenu.Current.IsHovering )
            {

                ToggleMouseDragMode( MouseDragMode.NONE );
            }
        }

        // - Begin Camera Rotate
        if ( Input.GetMouseButtonDown( ( int )MouseDragMode.DRAG_CAMERA_ROTATE ) )
        {
            ToggleMouseDragMode( MouseDragMode.DRAG_CAMERA_ROTATE );
        }

        // - End Camera Rotate
        if ( Input.GetMouseButtonUp( ( int )MouseDragMode.DRAG_CAMERA_ROTATE ) )
        {
            if ( IsDragging )
            {
                ToggleMouseDragMode( MouseDragMode.NONE );
            }
            else
            {
                //select hovered if nothing else
                if ( HoveredActor.transform && SelectedActors.Length == 0 )
                {
                    foreach(WorldActor actor in SelectedActors )
                    {
                        actor.Deselect( );
                    }

                    SelectedActors = new WorldActor[]
                    {
                        HoveredActor.transform.GetComponent<WorldActor>()
                    };

                    SelectedActors[0].Select( );
                }

                List<ContextMenuTarget> targets = new List<ContextMenuTarget>( );

                //create context content
                if ( SelectedActors.Length > 0 )
                {
                    ContextData contextInfo = new ContextData( );
                    contextInfo.SelectedObjects = SelectedActors;
                    contextInfo.TargetPosition  = MouseWorldPos;

                    if ( HoveredActor.transform )
                    {
                        contextInfo.TargetObject = HoveredActor.transform.GetComponent<WorldActor>( );
                    }

                    for ( int i = 0; i < SelectedActors.Length; i++ )
                    {
                        //create new target
                        ContextMenuTarget newTarget = new ContextMenuTarget( );
                        newTarget.Name          = SelectedActors[i].ToString( );
                        newTarget.Commands      = SelectedActors[i].GetComponent<WorldActor>( ).GetContextCommands( contextInfo );
                        newTarget.TargetActor   = SelectedActors[i];

                        //add target to list
                        targets.Add( newTarget );
                    }

                    UIContextMenu.Display( targets.ToArray( ), out ActionContextMenu );
                    ActionContextMenu.position = Input.mousePosition;
                }
            }
        }

        //Update drag position
        if ( DragMode != MouseDragMode.NONE )
        {
            DragPosition = MouseScreenPos;

            if(Vector2.Distance(DragStartPosition, DragPosition ) > 1 )
            {
                IsDragging = true;
            }
        }

        //Safety reset
        if(!Input.GetMouseButton( ( int )MouseDragMode.DRAG_CAMERA_ROTATE ) && !Input.GetMouseButton( ( int )MouseDragMode.DRAG_SELECT ) )
        {
            ToggleMouseDragMode( MouseDragMode.NONE );
        }

        //Mouse wheel scroll
        var _scrollValue = -Input.GetAxis( "Mouse ScrollWheel" );
        if ( _scrollValue != 0 )
        {
            CameraController.CurrentCamera.ZoomCamera( _scrollValue );
        }

        //////////////////////
        // Keys / Buttons
        /////////////////////
        if ( SelectedActors.Length != 0 && Input.GetButtonDown( "Focus Selection" ) )
        {
            CameraController.CurrentCamera.MoveToFocusSingle( SelectedActors[0].transform );
        }

        if ( !SessionGameManager.Instance.GameIsPaused && Input.GetKeyDown( KeyCode.Escape ) )
        {
            SessionGameManager.Instance.TogglePause( true );
        }

        ///////////////////////
        // Process Move
        ///////////////////////
        var moveDir = Vector3.zero;
        moveDir.x = Input.GetAxis( "Horizontal" );
        moveDir.z = Input.GetAxis( "Vertical" );

        if ( moveDir != Vector3.zero )
            CameraController.CurrentCamera.MoveCamera( moveDir, Input.GetKey( KeyCode.LeftShift ) ? 2 : 1 );
    }

    void FixedUpdate( )
    {
        ///////////////////////
        // Process Drag
        ///////////////////////
        switch ( DragMode )
        {
            case MouseDragMode.DRAG_SELECT:
                //Process rect
                Rect viewportRect = new Rect( 0, 0, Screen.width, Screen.height );

                //trace to world
                float dist = 80;

                //Top Left Pos
                var viewRectTopLeft = new Vector3( viewportRect.x, viewportRect.y, dist );
                viewRectTopLeft = Camera.main.ScreenToWorldPoint( viewRectTopLeft );

                //bottom left pos
                var viewRectBotLeft = new Vector3( viewportRect.x, viewportRect.height, dist );
                viewRectBotLeft = Camera.main.ScreenToWorldPoint( viewRectBotLeft );

                //bottom right pos
                var viewRectBotRight = new Vector3( viewportRect.width, viewportRect.height, dist );
                viewRectBotRight = Camera.main.ScreenToWorldPoint( viewRectBotRight );

                //top right
                var viewRectTopRight = new Vector3( viewportRect.width, viewportRect.y, dist );
                viewRectTopRight = Camera.main.ScreenToWorldPoint( viewRectTopRight );

                //Center
                var viewRectCenter = new Vector3( viewportRect.x + viewportRect.width / 2, viewportRect.y + viewportRect.height / 2, dist );
                viewRectCenter = Camera.main.ScreenToWorldPoint( viewRectCenter );

                RaycastHit[] ObjsInView = Physics.BoxCastAll( Camera.main.transform.position,
                    new Vector3( Vector3.Distance( viewRectTopLeft, viewRectTopRight ) / 2, Vector3.Distance( viewRectTopLeft, viewRectBotLeft ) / 2, dist ),
                    viewRectCenter - Camera.main.transform.position );

                SelectedActors = ObjsInView.Where( x => x.collider.GetComponent<WorldActor>( ) != null )
                    .Where( x => ScreenSelectionRect.Overlaps( CivUI.GetScreenRectByBounds( x.collider.bounds ), true ) )
                        .Select( x => x.collider.GetComponent<WorldActor>( ) ).ToArray( );

                break;

            case MouseDragMode.DRAG_CAMERA_ROTATE:

                var diff = ( DragPosition.x - DragStartPosition.x ) / Screen.width;
                CameraController.CurrentCamera.RotateCamera( diff );

            break;

            default:
                break;
        }
    }

    void ToggleMouseDragMode( MouseDragMode newMode )
    {
        switch ( newMode )
        {
            case MouseDragMode.DRAG_SELECT:
                //Began drag selection
                Array.ForEach( SelectedActors, x => x.Deselect( ) );
                break;

            case MouseDragMode.DRAG_CAMERA_ROTATE:
                break;

            case MouseDragMode.NONE:

                //Ended drag selection
                if(DragMode == MouseDragMode.DRAG_SELECT )
                {
                    Array.ForEach( SelectedActors, x => x.Select( ) );
                }

                //stop dragging
                IsDragging = false;

                break;

            default:
                break;
        }

        DragStartPosition = MouseScreenPos;
        DragMode = newMode;
    }

    void OnGUI( )
    {
        //Calculate selecton rect
        if ( DragMode == MouseDragMode.DRAG_SELECT )
        {
            //Note: move rect processing to Update() or FixedUpdate() and only draw in GUI
            var pos = new Vector2( DragStartPosition.x, DragStartPosition.y );
            var size = DragPosition - DragStartPosition;

            //Process rect
            ScreenSelectionRect = new Rect( pos, size );

            //Draw selection rect
            GUI.skin = SessionGameManager.Instance.DefaultGUISkin;

            Color rectColor = CivColor.HoverHighlightColor.ToColor( );
            rectColor.a = 0.2f;

            GUI.color = rectColor;

            if ( !BoxTex )
                BoxTex = Texture2D.whiteTexture;

            //GUI.DrawTexture( ScreenSelectionRect, BoxTex, ScaleMode.StretchToFill );
            GUI.Box( ScreenSelectionRect, "" );
        }
    }

}