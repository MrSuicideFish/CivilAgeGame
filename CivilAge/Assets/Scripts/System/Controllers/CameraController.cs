using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraController : MonoBehaviour
{
    //Singleton
    public static CameraController CurrentCamera;

    private RectTransform selectionRect;
    private RectTransform SelectionRect
    {
        get
        {
            if ( selectionRect == null )
            {
                //initialize selection rectangle
                var rectObj = GameObject.FindGameObjectWithTag( "SelectionRect" );
                if ( rectObj == null )
                {
                    GameObject newRect = ( GameObject )Resources.Load( "SelectionRect" );
                    selectionRect = GameObject.Instantiate( newRect ).GetComponent<RectTransform>( );
                    selectionRect.SetParent( SessionGameManager.SessionCanvas.transform, false );
                }
                else
                {
                    selectionRect = rectObj.GetComponent<RectTransform>( );
                }
            }

            return selectionRect;
        }
    }

    //Flags
    public bool LockEnabled { get; private set; }

    //Locomotion
    private Vector3 TargetLocation;
    private Transform ViewTarget;

    public float CameraMoveSpeed { get; private set; }
    public float CameraRotationSpeed { get; private set; }
    public float CameraZoomSpeed { get; private set; }

    public float MinZoomDistance { get; private set; }
    public float ZoomDistance { get; private set; }
    public float MaxZoomDistance { get; private set; }

    public float RotationAngle { get; private set; }

    void Awake( )
    {
        //Camera
        CurrentCamera = this;

        //Disable selection rect by default
        SelectionRect.gameObject.SetActive( false );

        //Load in the player's settings
        CameraMoveSpeed = 0.4f;//PlayerPrefs.GetFloat( "CameraMovementSpeed", 10 );
        CameraRotationSpeed = 0.5f;//PlayerPrefs.GetFloat( "CameraMovementSpeed", 10 );

        //Zoom
        MinZoomDistance = 0;
        MaxZoomDistance = 30;
        CameraZoomSpeed = 10;

        //Debug
        ZoomDistance = 5;
        RotationAngle = 0;
    }

    private Vector3 DragStartPosition = Vector3.zero;
    private Vector3 DragPosition = Vector3.zero;
    private float TargetZoomDistance = 0;
    void Update( )
    {
        if ( !LockEnabled )
        {
            //Process camera positioning
            Vector3 _viewtargetPos = ViewTarget == null ?
                        TargetLocation :
                        ViewTarget.position;

            //Process Zoom
            TargetZoomDistance = Mathf.Lerp( TargetZoomDistance, ZoomDistance, 0.1f );

            //Process Orbit
            Vector3 newPos = new Vector3
            (
                _viewtargetPos.x + TargetZoomDistance * Mathf.Cos( RotationAngle + Mathf.PI / 180 ),
                _viewtargetPos.y + TargetZoomDistance,
                _viewtargetPos.z + TargetZoomDistance * Mathf.Sin( RotationAngle + Mathf.PI / 180 )
            );

            //Set final position
            transform.position = Vector3.Lerp( transform.position, newPos, 0.9f );
            transform.rotation = Quaternion.LookRotation( _viewtargetPos - transform.position );

        }
    }
    
    public static void ToggleLock(bool enabled)
    {
        CurrentCamera.LockEnabled = enabled;
    }

    public void RotateCamera( float amount )
    {
        CurrentCamera.RotationAngle += amount * CameraRotationSpeed;
    }

    float CurrentGridScale = 0.0f;
    public void ZoomCamera(float amount )
    {
        var _newZoom = CurrentCamera.ZoomDistance + ( amount * CameraZoomSpeed );
        if ( _newZoom > MinZoomDistance && _newZoom < MaxZoomDistance )
        {
            CurrentCamera.ZoomDistance = _newZoom;
        }
    }

    public void MoveToFocusSingle( Transform target )
    {
        TargetLocation = target.position;
    }

    public void MoveToFocusMultiple(Transform[] targets )
    {
        for(int i = 0; i < targets.Length; i++ )
        {

        }
    }

    public void MoveCamera(Vector3 direction, float speedModifier = 1.0f )
    {
        //Transform move direction and increment location
        direction = transform.TransformDirection( direction );

        TargetLocation.x += direction.x * CameraMoveSpeed * speedModifier;
        TargetLocation.z += direction.z * CameraMoveSpeed * speedModifier;
    }
}