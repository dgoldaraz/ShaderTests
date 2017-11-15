using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NucleonSpawner : MonoBehaviour {

    public float TimeBetweenSpawns;

    public float SpawnDistance;

    public Nucleon[] NucleonPrefabs;

    float TimeSinceLastSpawn;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        TimeSinceLastSpawn += Time.deltaTime;
        if(TimeSinceLastSpawn > TimeBetweenSpawns)
        {
            TimeSinceLastSpawn = 0.0f;
            SpawnNucleon();
        }
	}

    void SpawnNucleon()
    {
        Nucleon Prefab = NucleonPrefabs[Random.Range(0, NucleonPrefabs.Length)];
        Nucleon SpawnN =  Instantiate<Nucleon>(Prefab);
        SpawnN.transform.localPosition = Random.onUnitSphere * SpawnDistance;
    }
}
