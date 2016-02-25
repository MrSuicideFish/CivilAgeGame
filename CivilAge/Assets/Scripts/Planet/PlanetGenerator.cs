using UnityEngine;
using System.Collections;

public class PlanetGenerator : MonoBehaviour {
    public int galaxySize;
    int numPlanets = 1;
    int maxAltitude;
	// Use this for initialization
	void Start () {
        Planet[] planets = new Planet[numPlanets];
        IcoSphere ico = new IcoSphere();
        for(int i = 0; i < numPlanets; i++)
        {
            GameObject planet = new GameObject("planet" + i);
            IcoSphere.Create(planet);
            //for(int i = 0; i < planet)
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
