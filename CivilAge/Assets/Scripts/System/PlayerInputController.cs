using UnityEngine;
using System.Collections;

public class PlayerInputController : MonoBehaviour
{
    private enum MouseDragMode
    {
        NONE                = -1,
        DRAG_SELECT         = 0,
        DRAG_CAMERA_ROTATE  = 1,
    }

    private MouseDragMode DragMode = MouseDragMode.NONE;
    private Vector2 DragStartPosition,
                    DragPosition,
                    DragEndPosition;

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

    Texture2D BoxTex;
    void OnGUI( )
    {
        //Calculate selecton rect
        if ( DragMode == MouseDragMode.DRAG_SELECT )
        {
            var pos = new Vector2( DragStartPosition.x, DragStartPosition.y );
            var size = DragPosition - DragStartPosition;

            //Process rect
            Rect selectionRect = new Rect( pos, size );

            //Draw selection rect
            GUI.skin = SessionGameManager.Instance.DefaultGUISkin;
            GUI.color = new Color( 1, 1, 1, 0.05f );

            if ( !BoxTex )
            {
                BoxTex = Texture2D.whiteTexture;
            }

            GUI.DrawTexture( selectionRect, BoxTex, ScaleMode.StretchToFill );
        }
    }
}