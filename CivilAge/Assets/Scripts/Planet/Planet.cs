using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour
{
    int galaxyScale = 100;
    int PlanetScaleFactor = 100;
    void start()
    {
        //Create randomly sized planet, with set of tiles covering at most half the planet
        int PlanetSize = Random.Range(2, 20) * PlanetScaleFactor;
        Vector3 Position = new Vector3(Random.Range(0, galaxyScale * 10000), Random.Range(0, galaxyScale * 10000), Random.Range(0, galaxyScale * 10000));
    }

}