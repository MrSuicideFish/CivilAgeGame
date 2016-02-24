using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    /// <summary>
    /// Singleton
    /// </summary>
    public static CameraController CurrentCamera;

    //Flags
    public bool LockEnabled { get; private set; }

    //Locomotion
    private Vector3 TargetLocation;
    private Transform ViewTarget;

    public float CameraMoveSpeed { get; private set; }
    public float CameraRotationSpeed { get; private set; }

    public float ZoomDistance { get; private set; }
    public float RotationAngle { get; private set; }

    void Awake( )
    {
        //Assign singleton
        CurrentCamera = this;

        //Load in the player's settings
        CameraMoveSpeed = 10;//PlayerPrefs.GetFloat( "CameraMovementSpeed", 10 );
        CameraRotationSpeed = 10;//PlayerPrefs.GetFloat( "CameraMovementSpeed", 10 );

        //Debug
        ZoomDistance = 5;
        RotationAngle = 90;
    }

    void Update( )
    {
        if ( !LockEnabled )
        {
            //Process Movement
            var _moveDirection  = Vector3.zero;
            _moveDirection.x    = Input.GetAxis( "Horizontal" ) * ( CameraMoveSpeed * Time.deltaTime );
            _moveDirection.z    = Input.GetAxis( "Vertical" ) * ( CameraMoveSpeed * Time.deltaTime );

            //Transform move direction and increment location
            _moveDirection = transform.InverseTransformVector( _moveDirection );
            TargetLocation.x += _moveDirection.x;
            TargetLocation.z += _moveDirection.z;
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

        transform.position = Vector3.Lerp( transform.position, newPos, CameraMoveSpeed * Time.deltaTime );
        transform.rotation = Quaternion.Lerp( transform.rotation, Quaternion.LookRotation( _viewtargetPos - transform.position ), 0.2f );
    }

    void FixedUpdate( )
    {

    }

    public static void ToggleLock(bool enabled)
    {
        CurrentCamera.LockEnabled = enabled;
    }
}