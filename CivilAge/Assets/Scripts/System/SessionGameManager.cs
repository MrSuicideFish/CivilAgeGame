using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public enum GameSpeedMode
{
    X1 = 1,
    X2 = 2,
    X3 = 3
}

public class SessionGameManager : MonoBehaviour
{
    //Singleton
    private static SessionGameManager instance;
    public static SessionGameManager Instance
    {
        get
        {
            if(!instance)
            {
                instance = GameObject.FindGameObjectWithTag( "GameController" ).GetComponent<SessionGameManager>( );
            }

            return instance;
        }
    }

    private static CameraController gameCamera;
    public static CameraController GameCamera
    {
        get
        {
            if ( !gameCamera )
            {
                gameCamera = Camera.main.GetComponent<CameraController>( );
            }

            return gameCamera;
        }
    }

    private static Canvas sessionCanvas;
    public static Canvas SessionCanvas
    {
        get
        {
            if ( sessionCanvas == null )
            {
                var canvasObj = GameObject.FindGameObjectWithTag( "SessionCanvas" );
                if ( !canvasObj )
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

    //Pre-Loaded
    public GUISkin SystemGUISkin;
    public GUISkin DefaultGUISkin;

    //Pausing
    public bool GameIsPaused { get; private set; }
    public UnityEvent<bool> OnPauseToggled;

    //Game flow
    GameSpeedMode CurrentGameSpeed = GameSpeedMode.X1;
    public UnityEvent<GameSpeedMode> OnGameSpeedChanged;

    public void TogglePause( bool enabled )
    {
        if ( enabled == GameIsPaused ) return;

        GameIsPaused = enabled;
        OnPauseToggled.Invoke( GameIsPaused );
    }

    public void SetGameSpeed(GameSpeedMode speedMode )
    {
        if ( speedMode == CurrentGameSpeed ) return;

        Time.timeScale = speedMode.GetHashCode( );

        CurrentGameSpeed = speedMode;
        OnGameSpeedChanged.Invoke( CurrentGameSpeed );
    }
}