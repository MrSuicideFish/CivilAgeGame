using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public enum VirtualTimeScale
{
    X1 = 1,
    X2 = 2,
    X3 = 3
}

public sealed class CivColor
{
    public float    R, 
                    G, 
                    B, 
                    A;

    public CivColor( ) { }
    public CivColor( float r, float g, float b )
    {
        R = r;
        G = g;
        B = b;
        A = 1;
    }
    public CivColor( float r, float g, float b, float a ) { }

    public static CivColor HoverHighlightColor { get { return new CivColor( 0.8f, 0.56f, 0.2f ); } }
    public static CivColor SelectedHighlightColor { get { return new CivColor( 0f, 1f, 0f ); } }

    public Color ToColor( )
    {
        return new Color( R, G, B, A );
    }
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

    private static GameObject worldGridObj;
    public static GameObject WorldGridObject
    {
        get
        {
            if ( worldGridObj == null )
                worldGridObj = GameObject.FindGameObjectWithTag( "GridPlane" );

            return worldGridObj;
        }
    }

    private static PauseScreenController pauseScreen;
    public static PauseScreenController PauseScreen
    {
        get
        {
            if(pauseScreen == null )
            {
                pauseScreen = GameObject.Instantiate( Resources.Load<GameObject>( "PAUSE_SCREEN" ) ).GetComponent<PauseScreenController>( );
                PauseScreen.gameObject.transform.SetParent( SessionCanvas.transform, false );
            }

            return pauseScreen;
        }
    }

    //Pre-Loaded
    public GUISkin SystemGUISkin;
    public GUISkin DefaultGUISkin;

    //Pausing
    public bool GameIsPaused { get; private set; }

    //Game flow
    VirtualTimeScale CurrentGameSpeed = VirtualTimeScale.X1;
    public UnityEvent<VirtualTimeScale> OnGameSpeedChanged;

    //GAME EVENTS
    public delegate void GamePauseEvent( bool enabled );
    public static event GamePauseEvent OnPauseToggled;

    //Colors
    public static float GlobalHighlightWidth = 0.1f;

    /// <summary>
    /// On the start of the game session
    /// </summary>
    void Awake( )
    {
    }

    public void TogglePause( bool enabled )
    {
        if ( enabled == GameIsPaused ) return;

        GameIsPaused = enabled;

        //Show / hide screen
        PauseScreen.gameObject.SetActive( GameIsPaused );

        if(OnPauseToggled != null )
        {
            OnPauseToggled( GameIsPaused );
        }
    }

    public void SetGameSpeed(VirtualTimeScale speedMode )
    {
        if ( speedMode == CurrentGameSpeed ) return;

        CurrentGameSpeed = speedMode;
        OnGameSpeedChanged.Invoke( CurrentGameSpeed );
    }
}