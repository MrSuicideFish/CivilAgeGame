using UnityEngine;
using System.Collections;

public class PlayerInputController : MonoBehaviour
{
    private enum MouseDragMode
    {
        NONE,
        DRAG_SELECT,
        DRAG_CAMERA_ROTATE,
        DRAG_CAMERA_PAN
    }

    private MouseDragMode DragMode = MouseDragMode.NONE;
    private Vector2 DragStartPosition,
                    DragPosition,
                    DragEndPosition;

    void Update( )
    {
        //Mouse - Selection
        if ( Input.GetMouseButtonDown( 0 ) )
            ToggleMouseDragMode( MouseDragMode.DRAG_SELECT );

        if ( Input.GetMouseButtonUp( 0 ) )
            ToggleMouseDragMode( MouseDragMode.NONE );

        //Mouse - Camera Rotate
        if ( Input.GetMouseButtonDown( 1 ) )
            ToggleMouseDragMode( MouseDragMode.DRAG_CAMERA_ROTATE );

        if ( Input.GetMouseButtonUp( 1 ) )
            ToggleMouseDragMode( MouseDragMode.NONE );

        //Mouse - Camera Pan
        if ( Input.GetMouseButtonDown( 2 ) )
            ToggleMouseDragMode( MouseDragMode.DRAG_CAMERA_PAN );

        if ( Input.GetMouseButtonUp( 2 ) )
            ToggleMouseDragMode( MouseDragMode.NONE );

        if(DragMode != MouseDragMode.NONE )
        {
            DragPosition = new Vector2( Input.mousePosition.x, Screen.height - Input.mousePosition.y  );
        }
    }

    void ToggleMouseDragMode( MouseDragMode newMode )
    {
        switch ( newMode )
        {
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