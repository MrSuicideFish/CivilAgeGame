using UnityEngine;
using System.Collections;

/// <summary>
/// The type of user occupying a SessionPlayer object.
/// </summary>
public enum PLAYER_TYPE
{
    USER,
    NPC_PLAYER,
    NPC_NATIVE,
    NPC_PIRATE
}

/// <summary>
/// Session Player
/// --------------
/// The SessionPlayer class contains all information on a player's status including
/// things like resources, planetary info, and diplomacy info.
/// 
/// </summary>
public class SessionPlayer : MonoBehaviour
{
    /*
     * INSTANCE
     */
    public string PlayerName { get; private set; }

    //Stats
    //--Financials
    public int TotalBalance { get; private set; }
    public int TotalRevenue { get; private set; }

    //--Citizenship
    public int TotalPopulation { get; private set; }

    //--Assets
    public int TotalPlanets { get; private set; }
    public int TotalShips { get; private set; }

    /*
     * STATIC
     */
    public static SessionPlayer CurrentPlayer { get; private set; }
    public static SessionPlayer[] AllPlayers { get; private set; }
}