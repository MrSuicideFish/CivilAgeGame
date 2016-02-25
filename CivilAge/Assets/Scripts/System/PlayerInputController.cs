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

                var pos = new Vector2( DragStartPosition.x, DragStartPosition.y );
                var size = DragPosition - DragStartPosition;

                //Process rect
                Rect selectionRect = new Rect( pos, size );

                //trace to world
                float dist = 10f;

                //Top Left Pos
                var rectTopLeft = new Vector3( selectionRect.x, Screen.height - selectionRect.y, dist );
                rectTopLeft = Camera.main.ScreenToWorldPoint( rectTopLeft );

                //bottom left pos
                var rectBotLeft = new Vector3( selectionRect.x, Screen.height - ( selectionRect.y + selectionRect.height ), dist );
                rectBotLeft = Camera.main.ScreenToWorldPoint( rectBotLeft );

                //bottom right pos
                var rectBotRight = new Vector3( selectionRect.x + selectionRect.width, Screen.height - ( selectionRect.y + selectionRect.height ), dist );
                rectBotRight = Camera.main.ScreenToWorldPoint( rectBotRight );

                //top right
                var rectTopRight = new Vector3( selectionRect.x + selectionRect.width, Screen.height - selectionRect.y, dist );
                rectTopRight = Camera.main.ScreenToWorldPoint( rectTopRight );

                //Center
                var rectCenter = new Vector3( selectionRect.x + (selectionRect.width / 2), Screen.height - ( selectionRect.y + selectionRect.height / 2 ), dist );
                rectCenter = Camera.main.ScreenToWorldPoint( rectCenter );

                var width = Vector3.Distance( rectTopLeft, rectTopRight ) * 6;
                var height = Vector3.Distance( rectTopLeft, rectBotLeft ) * 6;

                RaycastHit[] hitObj = Physics.BoxCastAll( rectCenter, new Vector3( width * 0.5f, height * 0.5f, Camera.main.nearClipPlane ), rectCenter - Camera.main.transform.position, Camera.main.transform.rotation );

                foreach(RaycastHit hit in hitObj )
                {
                    hit.collider.gameObject.GetComponent<MeshRenderer>( ).material.color = Color.red;
                }

                if ( !DebugSphereA )
                {
                    float scale = 0.2f;

                    DebugSphereA = GameObject.CreatePrimitive( PrimitiveType.Cube );
                    DebugSphereA.transform.localScale = Vector3.one * scale;

                    DebugSphereB = GameObject.CreatePrimitive( PrimitiveType.Cube );
                    DebugSphereB.transform.localScale = Vector3.one * scale;

                    DebugSphereC = GameObject.CreatePrimitive( PrimitiveType.Cube );
                    DebugSphereC.transform.localScale = Vector3.one * scale;

                    DebugSphereD = GameObject.CreatePrimitive( PrimitiveType.Cube );
                    DebugSphereD.transform.localScale = Vector3.one * scale;

                    DebugSphereE = GameObject.CreatePrimitive( PrimitiveType.Cube );
                    DebugSphereE.transform.localScale = Vector3.one * scale;
                }

                DebugSphereA.transform.position = rectTopLeft;
                DebugSphereB.transform.position = rectBotLeft;
                DebugSphereC.transform.position = rectBotRight;
                DebugSphereD.transform.position = rectTopRight;
                DebugSphereE.transform.position = rectCenter;

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
            Rect selectionRect = new Rect( pos, size );

            //Draw selection rect
            GUI.skin = SessionGameManager.Instance.DefaultGUISkin;
            GUI.color = new Color( 1, 1, 1, 0.05f );

            if ( !BoxTex )
                BoxTex = Texture2D.whiteTexture;

            GUI.DrawTexture( selectionRect, BoxTex, ScaleMode.StretchToFill );
        }
    }
}