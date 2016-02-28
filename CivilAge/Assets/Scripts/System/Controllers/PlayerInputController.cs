using UnityEngine;
using System;
using System.Linq;
using System.Collections;

public class PlayerInputController : MonoBehaviour
{
    private enum MouseDragMode
    {
        NONE                = -1,
        DRAG_SELECT         = 0,
        DRAG_CAMERA_ROTATE  = 1,
    }

    private Rect ScreenSelectionRect;

    private MouseDragMode DragMode = MouseDragMode.NONE;

    private Vector2 MouseScreenPos,
                    DragStartPosition,
                    DragPosition,
                    DragEndPosition;

    private Vector3 MouseWorldPos;

    private Ray MouseHoverRay;

    private RaycastHit HoveredActorHit;
    public WorldActor[] SelectedActors;

    public float MouseTraceDist = 2.0f;

    ///////////////////////
    // Initiation
    ///////////////////////
    void Awake( )
    {
        SessionGameManager.OnPauseToggled += OnGamePaused;
    }

    ///////////////////////
    // Event Callbacks
    ///////////////////////
    void OnGamePaused( bool enabled )
    {
        //Un-highlight hovered actor
        if ( HoveredActorHit.transform )
        {
            HoveredActorHit.transform.GetComponent<MeshRenderer>( ).material.SetFloat( "_OutlineWidth", 0.0f );
            HoveredActorHit = new RaycastHit( ); //to set to null
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
        var oldHoverActorHit = HoveredActorHit;
        Physics.Raycast( MouseHoverRay, out HoveredActorHit, Mathf.Infinity, 1 << 8 );

        //Unhighlight old
        if ( !oldHoverActorHit.Equals( HoveredActorHit ) && oldHoverActorHit.transform && !oldHoverActorHit.transform.GetComponent<WorldActor>( ).IsSelected )
        {
            oldHoverActorHit.transform.GetComponent<MeshRenderer>( ).material.SetFloat( "_OutlineWidth", 0.0f );
        }

        //Highlight actor
        if ( HoveredActorHit.transform && !HoveredActorHit.transform.GetComponent<WorldActor>().IsSelected )
        {
            HoveredActorHit.transform.GetComponent<MeshRenderer>( ).material.SetFloat( "_OutlineWidth", 0.2f );
            HoveredActorHit.transform.GetComponent<MeshRenderer>( ).material.SetColor( "_HighlightColor", SessionGameManager.HoveredActorHightlightColor );
        }

        //////////////////////
        // Mouse Drag
        /////////////////////
        // - Selection
        if ( Input.GetMouseButtonDown( ( int )MouseDragMode.DRAG_SELECT ) )
        {
            ToggleMouseDragMode( MouseDragMode.DRAG_SELECT );
        }

        if ( Input.GetMouseButtonUp( ( int )MouseDragMode.DRAG_SELECT ) )
        {
            ToggleMouseDragMode( MouseDragMode.NONE );

            if ( HoveredActorHit.transform )
            {
                SelectedActors = new WorldActor[]
                {
                    HoveredActorHit.transform.GetComponent<WorldActor>()
                };

                SelectedActors[0].Select( );
            }
        }

        // - Camera Rotate
        if ( Input.GetMouseButtonDown( ( int )MouseDragMode.DRAG_CAMERA_ROTATE ) )
            ToggleMouseDragMode( MouseDragMode.DRAG_CAMERA_ROTATE );

        if ( Input.GetMouseButtonUp( ( int )MouseDragMode.DRAG_CAMERA_ROTATE ) )
            ToggleMouseDragMode( MouseDragMode.NONE );

        //Update drag position
        if ( DragMode != MouseDragMode.NONE )
            DragPosition = MouseScreenPos;

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

                break;

            default:
                break;
        }

        DragStartPosition = MouseScreenPos;
        DragMode = newMode;
    }

    

    Texture2D BoxTex;
    GameObject DebugSphereA,
                DebugSphereB,
                DebugSphereC,
                DebugSphereD,
                DebugSphereE;
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
            GUI.color = new Color( 1, 1, 1, 0.05f );

            if ( !BoxTex )
                BoxTex = Texture2D.whiteTexture;

            GUI.DrawTexture( ScreenSelectionRect, BoxTex, ScaleMode.StretchToFill );
        }
    }

}