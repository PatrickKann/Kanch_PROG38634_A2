using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTreasure : MonoBehaviour
{
    public GameObject _treasurePrefab;

    private Transform[] _spawnPoints;

    void Start()
    {
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("TreasurePoint");

        _spawnPoints = new Transform[spawnPoints.Length];
        for (int i = 0; i < spawnPoints.Length; i++)
            _spawnPoints[i] = spawnPoints[i].transform;

        SpawnTreasureAtRandomPos();
    }

    void Update()
    {
        
    }

    public void SpawnTreasureAtRandomPos()
    {
        //Get Random Loc
        System.Random rand = new System.Random();
        int randIndex = rand.Next(_spawnPoints.Length);
        Transform nextLocation = _spawnPoints[randIndex];

        //Instanciate treasure
        GameObject treas = Instantiate(_treasurePrefab, nextLocation.position, new Quaternion());
        treas.AddComponent<treasure>();
        Debug.Log("Created at " + treas.transform.position);
    }

    public bool IsTreasureCreated()
    {
        return (GameObject.FindGameObjectWithTag("Treasure") != null) ? true : false;
    }
}
