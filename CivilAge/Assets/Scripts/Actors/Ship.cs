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

    //--External Components
    private GameObject HealthBar;

    //Instance initiation
    public virtual void Awake( )
    {
        //Init internal components
        MeshRenderer = GetComponent<MeshRenderer>( );
    }

    public virtual void Start( )
    {
        //Init health bar
        HealthBar = GameObjectManager.GetObject( "ShipHealthbar" );
        HealthBar.transform.SetParent( SessionGameManager.SessionCanvas.transform, false );
        HealthBar.transform.SetSiblingIndex( 0 );
    }

    //Locomotion Methods
    void GoToPosition( Vector2 newPosition ) { }
    void FollowShip( Transform shipTransform ) { }

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
    public virtual void FixedUpdate( )
    {
        if ( IsSelected )
        {
            var newPos = CivUI.GetScreenRectByBounds( MeshRenderer.bounds ).position;
            HealthBar.GetComponent<RectTransform>( ).anchoredPosition = newPos;
        }
    }
}
