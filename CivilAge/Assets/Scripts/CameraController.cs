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
                    selectionRect.SetParent( SessionCanvas.transform, false );
                }
                else
                {
                    selectionRect = rectObj.GetComponent<RectTransform>( );
                }
            }

            return selectionRect;
        }
    }

    private Canvas sessionCanvas;
    private Canvas SessionCanvas
    {
        get
        {
            if(sessionCanvas == null )
            {
                var canvasObj = GameObject.FindGameObjectWithTag( "SessionCanvas" );
                if( !canvasObj )
                {
                    GameObject newCanvas = ( GameObject )Resources.Load( "SessionCanvas" );
                    sessionCanvas = GameObject.Instantiate( newCanvas ).GetComponent<Canvas>( );
                }
                else
                {
                    sessionCanvas = canvasObj.GetComponent<Canvas>( );
                }
            }


            return sessionCanvas;
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
        CameraMoveSpeed = 10;//PlayerPrefs.GetFloat( "CameraMovementSpeed", 10 );
        CameraRotationSpeed = 10;//PlayerPrefs.GetFloat( "CameraMovementSpeed", 10 );

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
    void Update( )
    {
        if ( !LockEnabled )
        {
            var _shiftModifier = Input.GetKey( KeyCode.LeftShift ) ? 2 : 1;

            //Process Movement
            var _moveDirection  = Vector3.zero;
            _moveDirection.x    = Input.GetAxis( "Horizontal" ) * ( CameraMoveSpeed * Time.deltaTime );
            _moveDirection.z    = Input.GetAxis( "Vertical" ) * ( CameraMoveSpeed * Time.deltaTime );

            //Mouse wheel scroll
            var _scrollValue = -Input.GetAxis( "Mouse ScrollWheel" ) * CameraZoomSpeed;

            if ( _scrollValue != 0 )
            {
                //zoom camera
                var _newZoom = ZoomDistance + _scrollValue;
                if( _newZoom > MinZoomDistance && _newZoom < MaxZoomDistance )
                {
                    ZoomDistance = _newZoom;
                }
            }

            //Mouse dragging stuff - Selection
            if ( Input.GetMouseButtonDown( 0 ) )
            {
                //record start position
                var _targetPos = new Vector2( Input.mousePosition.x, Input.mousePosition.y - Screen.height ) * SessionCanvas.scaleFactor;
                DragStartPosition = _targetPos;

                //Enable selection rect
                SelectionRect.gameObject.SetActive( true );
            }

            //do drag select
            if ( Input.GetMouseButton( 0 ) )
            {
                //Record drag position
                var _targetPos = new Vector2( Input.mousePosition.x, Input.mousePosition.y - Screen.height ) * SessionCanvas.scaleFactor;
                DragPosition = _targetPos;

                //Calculate selecton rect
                var _newWidth = -( DragStartPosition.x - DragPosition.x ) * SessionCanvas.scaleFactor * 2.45f;
                var _newHeight = ( DragStartPosition.y - DragPosition.y ) * SessionCanvas.scaleFactor * 2.45f;

                var xFlipped = _newWidth < 0;
                var yFlipped = _newHeight < 0;

                SelectionRect.localScale = new Vector3( xFlipped ? -1: 1, yFlipped ? -1 : 1, 1 );

                SelectionRect.anchoredPosition = new Vector2( DragStartPosition.x, DragStartPosition.y ) * SessionCanvas.scaleFactor * 2.45f;
                SelectionRect.SetSizeWithCurrentAnchors( RectTransform.Axis.Horizontal, Mathf.Abs(_newWidth) );
                SelectionRect.SetSizeWithCurrentAnchors( RectTransform.Axis.Vertical, Mathf.Abs( _newHeight ) );
            }

            //End drag select
            if ( Input.GetMouseButtonUp( 0 ) )
            {
                //Disable selection rect
                SelectionRect.gameObject.SetActive( false );
            }

            //Mouse dragging stuff - Rotation
            if ( Input.GetMouseButtonDown( 2 ) )
            {
                DragStartPosition = Input.mousePosition;
            }

            //process drag
            if ( Input.GetMouseButton( 2 ) )
            {
                DragPosition = Input.mousePosition;
                var diff = ( DragPosition.x - DragStartPosition.x ) / Screen.width;
                RotationAngle += diff * CameraRotationSpeed * Time.fixedDeltaTime;
            }

            //Transform move direction and increment location
            _moveDirection = transform.TransformDirection( _moveDirection );
            TargetLocation.x += _moveDirection.x * _shiftModifier;
            TargetLocation.z += _moveDirection.z * _shiftModifier;
        }

        //Process camera positioning
        Vector3 _viewtargetPos = ViewTarget == null ?
                    TargetLocation :
                    ViewTarget.position;

        //Process Orbit
        Vector3 newPos = new Vector3
        (
            _viewtargetPos.x + ZoomDistance * Mathf.Cos( RotationAngle + Mathf.PI / 180 ),
            _viewtargetPos.y + ZoomDistance,
            _viewtargetPos.z + ZoomDistance * Mathf.Sin( RotationAngle + Mathf.PI / 180 )
        );

        transform.position = Vector3.Lerp( transform.position, newPos, 0.9f );
        transform.rotation = Quaternion.LookRotation( _viewtargetPos - transform.position );
    }

    void FixedUpdate( )
    {

    }

    public static void ToggleLock(bool enabled)
    {
        CurrentCamera.LockEnabled = enabled;
    }
}