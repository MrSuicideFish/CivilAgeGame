using UnityEngine;
using System.Collections;

public class PauseScreenController : MonoBehaviour
{
    public static PauseScreenController Instance;

    void Awake( )
    {
        Instance = this;
    }

    public void GoToMenu(string newMenu )
    {

    }

    public void Close( )
    {
        SessionGameManager.Instance.TogglePause( false );
    }

    public void Open( )
    {
        SessionGameManager.Instance.TogglePause( true );
    }
}