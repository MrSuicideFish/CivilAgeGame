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

    //Instance initiation
    void Start( ) { }
    void Awake( ) { }

    //Locomotion Methods
    void GoToPosition(Vector2 newPosition ) { }
    void FollowShip(Transform shipTransform ) { }

    //Logistics
    public void Rename(string newName )
    {
        ShipName = newName;
    }
}
