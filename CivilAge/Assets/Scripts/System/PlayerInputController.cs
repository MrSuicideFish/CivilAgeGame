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
    private Vector2 DragStartPosition,
                    DragPosition,
                    DragEndPosition;

    public WorldActor[] SelectedActors;

    void Update( )
    {
        //////////////////////
        // Mouse Drag
        /////////////////////

        // - Selection
        if ( Input.GetMouseButtonDown( ( int )MouseDragMode.DRAG_SELECT ) )
            ToggleMouseDragMode( MouseDragMode.DRAG_SELECT );

        if ( Input.GetMouseButtonUp( (int)MouseDragMode.DRAG_SELECT) )
            ToggleMouseDragMode( MouseDragMode.NONE );

        // - Camera Rotate
        if ( Input.GetMouseButtonDown( ( int )MouseDragMode.DRAG_CAMERA_ROTATE ) )
            ToggleMouseDragMode( MouseDragMode.DRAG_CAMERA_ROTATE );

        if ( Input.GetMouseButtonUp( ( int )MouseDragMode.DRAG_CAMERA_ROTATE ) )
            ToggleMouseDragMode( MouseDragMode.NONE );

        //Update drag position
        if (DragMode != MouseDragMode.NONE )
            DragPosition = new Vector2( Input.mousePosition.x, Screen.height - Input.mousePosition.y );

        ///////////////////////
        // Other mouse controls
        ///////////////////////
        switch ( DragMode )
        {
            case MouseDragMode.DRAG_SELECT:

                var pos = new Vector2( DragStartPosition.x, DragStartPosition.y );
                var size = DragPosition - DragStartPosition;

                //Process rect
                Rect viewportRect = new Rect( 0, 0, Screen.width, Screen.height );

                //trace to world
                float dist = 15;

                //Top Left Pos
                var rectTopLeft = new Vector3( viewportRect.x, viewportRect.y, dist );
                rectTopLeft = Camera.main.ScreenToWorldPoint( rectTopLeft );

                //bottom left pos
                var rectBotLeft = new Vector3( viewportRect.x, viewportRect.height, dist );
                rectBotLeft = Camera.main.ScreenToWorldPoint( rectBotLeft );

                //bottom right pos
                var rectBotRight = new Vector3( viewportRect.width, viewportRect.height, dist );
                rectBotRight = Camera.main.ScreenToWorldPoint( rectBotRight );

                //top right
                var rectTopRight = new Vector3( viewportRect.width, viewportRect.y, dist );
                rectTopRight = Camera.main.ScreenToWorldPoint( rectTopRight );

                //Center
                var rectCenter = new Vector3( viewportRect.x + viewportRect.width / 2, viewportRect.y + viewportRect.height / 2, dist );
                rectCenter = Camera.main.ScreenToWorldPoint( rectCenter );

                //Top Left Pos
                var virtualTopLeft = new Vector3( viewportRect.x, Screen.height - viewportRect.y, dist );
                virtualTopLeft = Camera.main.ScreenToWorldPoint( virtualTopLeft );

                RaycastHit[] ObjsInView = Physics.BoxCastAll( Camera.main.transform.position,
                    new Vector3( Vector3.Distance( rectTopLeft, rectTopRight ) / 2, Vector3.Distance( rectTopLeft, rectBotLeft ) / 2, dist ),
                    rectCenter - Camera.main.transform.position );

                foreach(RaycastHit obj in ObjsInView )
                {
                    if ( obj.transform.GetComponent<WorldActor>() == null ) continue;

                    //Translate obj bounds to screen rect
                    if ( ScreenSelectionRect.Overlaps( GetScreenRectByBounds( obj.collider.bounds ), true ) )
                    {
                        obj.collider.GetComponent<MeshRenderer>( ).material.color = Color.red;
                    }
                }

                //Gets all 'WorldActors' from array of hit objects and returns array of worldActors
                //SelectedActors = hitObjs.Where( x => x.collider.gameObject.GetComponent<WorldActor>( ) != null ).Select( x => x.collider.GetComponent<WorldActor>( ) ).ToArray( );

                //if ( !DebugSphereA )
                //{
                //    float scale = 0.05f;

                //    DebugSphereA = GameObject.CreatePrimitive( PrimitiveType.Cube );
                //    DebugSphereA.transform.localScale = Vector3.one * scale;

                //    DebugSphereB = GameObject.CreatePrimitive( PrimitiveType.Cube );
                //    DebugSphereB.transform.localScale = Vector3.one * scale;

                //    DebugSphereC = GameObject.CreatePrimitive( PrimitiveType.Cube );
                //    DebugSphereC.transform.localScale = Vector3.one * scale;

                //    DebugSphereD = GameObject.CreatePrimitive( PrimitiveType.Cube );
                //    DebugSphereD.transform.localScale = Vector3.one * scale;

                //    DebugSphereE = GameObject.CreatePrimitive( PrimitiveType.Cube );
                //    DebugSphereE.transform.localScale = Vector3.one * scale;
                //}

                //DebugSphereA.transform.position = rectTopLeft;
                //DebugSphereB.transform.position = rectBotLeft;
                //DebugSphereC.transform.position = rectBotRight;
                //DebugSphereD.transform.position = rectTopRight;
                //DebugSphereE.transform.position = rectCenter;

                break;

            case MouseDragMode.DRAG_CAMERA_ROTATE:

                var diff = ( DragPosition.x - DragStartPosition.x ) / Screen.width;
                CameraController.CurrentCamera.RotateCamera( diff );
            break;

            default:
                break;
        }

        //Mouse wheel scroll
        var _scrollValue = -Input.GetAxis( "Mouse ScrollWheel" );
        if ( _scrollValue != 0 )
        {
            CameraController.CurrentCamera.ZoomCamera( _scrollValue );
        }

        ///////////////////////
        // Process Move
        ///////////////////////
        var moveDir = Vector3.zero;
        moveDir.x = Input.GetAxis( "Horizontal" );
        moveDir.z = Input.GetAxis( "Vertical" );

        if( moveDir != Vector3.zero)
            CameraController.CurrentCamera.MoveCamera(moveDir, Input.GetKey(KeyCode.LeftShift) ? 2 : 1);
    }

    void ToggleMouseDragMode( MouseDragMode newMode )
    {
        switch ( newMode )
        {
            case MouseDragMode.DRAG_SELECT:
                break;

            case MouseDragMode.DRAG_CAMERA_ROTATE:
                break;

            default:
                break;
        }

        DragStartPosition = new Vector2( Input.mousePosition.x, Screen.height - Input.mousePosition.y );
        DragMode = newMode;
    }

    Rect GetScreenRectByBounds( Bounds bounds )
    {
        //Transform corner of bounds to screen-space
        var centerPos = bounds.center;

        var x1y1z1 = new Vector3( centerPos.x - bounds.extents.x, centerPos.y - bounds.extents.y, centerPos.z + bounds.extents.z );
        var x2y1z1 = new Vector3( centerPos.x + bounds.extents.x, centerPos.y - bounds.extents.y, centerPos.z + bounds.extents.z );
        var x1y2z1 = new Vector3( centerPos.x - bounds.extents.x, centerPos.y + bounds.extents.y, centerPos.z + bounds.extents.z );
        var x2y2z1 = new Vector3( centerPos.x + bounds.extents.x, centerPos.y + bounds.extents.y, centerPos.z + bounds.extents.z );

        var x1y1z2 = new Vector3( centerPos.x - bounds.extents.x, centerPos.y - bounds.extents.y, centerPos.z - bounds.extents.z );
        var x2y1z2 = new Vector3( centerPos.x + bounds.extents.x, centerPos.y - bounds.extents.y, centerPos.z - bounds.extents.z );
        var x1y2z2 = new Vector3( centerPos.x - bounds.extents.x, centerPos.y + bounds.extents.y, centerPos.z - bounds.extents.z );
        var x2y2z2 = new Vector3( centerPos.x + bounds.extents.x, centerPos.y + bounds.extents.y, centerPos.z - bounds.extents.z );

        //Raytrace bound positions to screen
        Vector2[] projScrPositions = new Vector2[]
        {
            Camera.main.WorldToScreenPoint( x1y1z1 ),
            Camera.main.WorldToScreenPoint( x2y1z1 ),
            Camera.main.WorldToScreenPoint( x1y2z1 ),
            Camera.main.WorldToScreenPoint( x2y2z1 ),

            Camera.main.WorldToScreenPoint( x1y1z2 ),
            Camera.main.WorldToScreenPoint( x2y1z2 ),
            Camera.main.WorldToScreenPoint( x1y2z2 ),
            Camera.main.WorldToScreenPoint( x2y2z2 )
        };

        //Bubble-sort positions
        //-TopLeft most
        Vector2 TopLeft     = Vector2.zero,
                TopRight    = Vector2.zero,
                BottomLeft  = Vector2.zero,
                BottomRight = Vector2.zero;

        //Sort positions
        for(int i = 0; i < projScrPositions.Length; i++ )
        {
            //Setup defaults
            if ( TopLeft == Vector2.zero )
                TopLeft = projScrPositions[i];

            if ( TopRight == Vector2.zero )
                TopRight = projScrPositions[i];

            if ( BottomLeft == Vector2.zero )
                BottomLeft = projScrPositions[i];

            if ( BottomRight == Vector2.zero )
                BottomRight = projScrPositions[i];

            //Test top left
            if (projScrPositions[i].x <= TopLeft.x &&
                projScrPositions[i].y >= TopLeft.y )
            {
                TopLeft = projScrPositions[i];
            }

            //Test top right
            if ( projScrPositions[i].x >= TopRight.x &&
                projScrPositions[i].y >= TopRight.y )
            {
                TopRight = projScrPositions[i];
            }

            //Test bottom left
            if ( projScrPositions[i].x <= BottomLeft.x &&
                projScrPositions[i].y <= BottomLeft.y )
            {
                BottomLeft = projScrPositions[i];
            }

            //Test bottom right
            if ( projScrPositions[i].x >= BottomRight.x &&
                projScrPositions[i].y <= BottomRight.y )
            {
                BottomRight = projScrPositions[i];
            }
        }

        //Screen height offset
        TopLeft.y       = Screen.height - TopLeft.y;
        BottomLeft.y    = Screen.height - BottomLeft.y;

        return new Rect( TopLeft, new Vector2( TopRight.x - TopLeft.x, BottomLeft.y - TopLeft.y ) );
    }

    private enum RectExtentDirection
    {
        TopLeft = 0,
        TopRight = 1,
        BottomLeft = 2,
        BottomRight = 3
    }

    void ExchangeArrayIdx( Vector2[] positions, int from, int to)
    {
        Vector2 temporary;

        temporary = positions[from];
        positions[from] = positions[to];
        positions[to] = temporary;
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