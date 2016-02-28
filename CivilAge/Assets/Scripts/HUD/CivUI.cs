using UnityEngine;
using System.Collections;

public struct GameMenuScreen
{
    string MenuName;
    int MenuLayer;
}

public struct GameMenu
{
    GameMenuScreen[] ChildScreens;
}

public class CivUI : MonoBehaviour
{
    public static Rect GetScreenRectByBounds( Bounds bounds )
    {
        //Define bounds
        var centerPos = bounds.center;
        var x1y1z1 = new Vector3( centerPos.x - bounds.extents.x, centerPos.y - bounds.extents.y, centerPos.z + bounds.extents.z );
        var x2y1z1 = new Vector3( centerPos.x + bounds.extents.x, centerPos.y - bounds.extents.y, centerPos.z + bounds.extents.z );
        var x1y2z1 = new Vector3( centerPos.x - bounds.extents.x, centerPos.y + bounds.extents.y, centerPos.z + bounds.extents.z );
        var x2y2z1 = new Vector3( centerPos.x + bounds.extents.x, centerPos.y + bounds.extents.y, centerPos.z + bounds.extents.z );

        var x1y1z2 = new Vector3( centerPos.x - bounds.extents.x, centerPos.y - bounds.extents.y, centerPos.z - bounds.extents.z );
        var x2y1z2 = new Vector3( centerPos.x + bounds.extents.x, centerPos.y - bounds.extents.y, centerPos.z - bounds.extents.z );
        var x1y2z2 = new Vector3( centerPos.x - bounds.extents.x, centerPos.y + bounds.extents.y, centerPos.z - bounds.extents.z );
        var x2y2z2 = new Vector3( centerPos.x + bounds.extents.x, centerPos.y + bounds.extents.y, centerPos.z - bounds.extents.z );

        //Transform corners of bounds to screen-space
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
        Vector2 TopLeft = Vector2.zero,
                TopRight = Vector2.zero,
                BottomLeft = Vector2.zero,
                BottomRight = Vector2.zero;

        //Sort positions
        for ( int i = 0; i < projScrPositions.Length; i++ )
        {
            //Quick-default
            if ( TopLeft == Vector2.zero )
                TopLeft = projScrPositions[i];

            if ( TopRight == Vector2.zero )
                TopRight = projScrPositions[i];

            if ( BottomLeft == Vector2.zero )
                BottomLeft = projScrPositions[i];

            if ( BottomRight == Vector2.zero )
                BottomRight = projScrPositions[i];

            //Test top left
            if ( projScrPositions[i].x <= TopLeft.x &&
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
        TopLeft.y = Screen.height - TopLeft.y;
        BottomLeft.y = Screen.height - BottomLeft.y;

        return new Rect( TopLeft, new Vector2( TopRight.x - TopLeft.x, BottomLeft.y - TopLeft.y ) );
    }
}