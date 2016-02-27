using UnityEngine;
using System.Collections;

public class PauseInputController : MonoBehaviour
{

    void Start( )
    {

    }

    void Update( )
    {
        if ( Input.GetKeyDown( KeyCode.Escape ) )
        {
            PauseScreenController.Instance.Close( );
        }
    }
}