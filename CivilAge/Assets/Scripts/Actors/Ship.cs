using UnityEngine;
using System.Collections;

public class Ship : WorldActor
{
    //Public
    //--General
    public string ShipName { get; private set; }

    //--Combat
    public int TotalHealth { get; private set; }
    public int TotalEnergy { get; private set; }
    public int TotalDamage { get; private set; }
    public int TotalArmor { get; private set; }

    //Private
    //--Locomotion
    private Vector2 TargetPosition;

    //--Internal Components
    private MeshRenderer MeshRenderer;
    private BoxCollider BoxCollider;

    //--External Components
    private GameObject HealthBar;

    //--Actor Commands
    delegate void GoTo( );

    //Instance initiation
    public virtual void Awake( )
    {
        //Init internal components
        MeshRenderer = GetComponent<MeshRenderer>( );
        BoxCollider = GetComponent<BoxCollider>( );

        //Setup context commands
        ContextCommands = new ContextMenuCommand[1];
        ContextCommands[0] = new ContextMenuCommand( );
        ContextCommands[0].Name = "Go Here";
    }

    public virtual void Start( )
    {
        //Init health bar
        HealthBar = GameObjectManager.GetObject( "ShipHealthbar" );
        HealthBar.transform.SetParent( SessionGameManager.SessionCanvas.transform, false );
        HealthBar.transform.SetSiblingIndex( 0 );
    }

    //Locomotion Methods
    void GoToPosition( ) { }
    void FollowShip( ) { }

    //Logistics
    public void Rename( string newName )
    {
        ShipName = newName;
    }

    //Selection
    public override void Select( )
    {
        base.Select( );

        HealthBar.SetActive( true );
    }
    public override void Deselect( )
    {
        base.Deselect( );

        HealthBar.SetActive( false );
    }

    //Update
    public virtual void Update( )
    {
        if ( IsSelected )
        {
            var healthBarRect = HealthBar.GetComponent<RectTransform>( );
            var newPos = Camera.main.WorldToViewportPoint( transform.position );

            //Move to center
            //newPos.x -= healthBarRect.rect.width / 2;
            newPos.x -= 0.035f;
            newPos.y += 0.075f;

            healthBarRect.anchorMin = newPos;
            healthBarRect.anchorMax = newPos;
        }
    }

    public override ContextMenuCommand[] GetContextCommands( WorldActor[] selectedActor, WorldActor targetActor )
    {
        ContextCommands = new ContextMenuCommand[1];
        ContextCommands[0] = new ContextMenuCommand( "Move To..", new GoTo( GoToPosition ) );

        return ContextCommands;
    }
}
