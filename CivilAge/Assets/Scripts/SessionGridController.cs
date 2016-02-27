using UnityEngine;
using System.Collections;

public class SessionGridController : MonoBehaviour
{
    private CameraController CameraParent;
    private Material GridMaterial;

    void Awake( )
    {
        GridMaterial = GetComponent<MeshRenderer>( ).material;
    }

    void FixedUpdate( )
    {
        if(!CameraParent )
        {
            CameraParent = CameraController.CurrentCamera;
        }

        //Keep grid under camera
        transform.position = new Vector3( CameraParent.transform.position.x, 0, CameraParent.transform.position.z );

        //Set grid tiling
        var newGridTiling = Mathf.Lerp( 6, 0f, ( CameraParent.ZoomDistance / CameraParent.MaxZoomDistance ));
        var newAnisScale = Mathf.Lerp( 5, 80, ( CameraParent.ZoomDistance / CameraParent.MaxZoomDistance ) );

        GridMaterial.SetTextureScale( "_GridAlpha", new Vector2( newGridTiling, newGridTiling ) );
        GridMaterial.SetFloat( "_Anis", newAnisScale );
    }
}