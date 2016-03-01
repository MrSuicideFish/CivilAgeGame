using UnityEngine;
using System.Collections.Generic;
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
    protected delegate void Command( ContextData contextInfo );

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
        //Create health bar
        HealthBar = GameObjectManager.GetObject( "ShipHealthbar" );
        HealthBar.transform.SetParent( SessionGameManager.SessionCanvas.transform, false );
        HealthBar.transform.SetSiblingIndex( 0 );
    }

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
            newPos.x -= 0.035f;
            newPos.y += 0.075f;

            healthBarRect.anchorMin = newPos;
            healthBarRect.anchorMax = newPos;
        }
    }

    public override ContextMenuCommand[] GetContextCommands( ContextData data )
    {
        List<ContextMenuCommand> commandsList = new List<ContextMenuCommand>( );

        commandsList.Add( new ContextMenuCommand( "Move To..", new Command( MoveTo ) ) );
        commandsList.Add( new ContextMenuCommand( "Set Patrol Route", new Command( SetPatrolRoute ) ) );

        if ( data.TargetObject && data.TargetObject != this )
        {
            commandsList.Add( new ContextMenuCommand( "Follow Ship", new Command( FollowShip ) ) );
        }

        ContextCommands = commandsList.ToArray( );

        return ContextCommands;
    }

    ///-------------------------------
    /// COMMANDS
    ///-------------------------------
    void MoveTo( ContextData data )
    {
        print( "Command MoveTo " + data.TargetPosition );
    }

    void FollowShip( ContextData data )
    {
        if ( data.TargetObject )
        {
            print( "Command FollowShip " + data.TargetObject );
        }
        else
            print( "Command FollowShip " );
    }

    void SetPatrolRoute( ContextData data )
    {
        print( "Command SetPatrolRoute " + data.TargetPosition );
    }
}
