using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFlockBuddies : MonoBehaviour {

    //Define the variable for which unit should be spawned, how many should spawn, and a list that contains them
    public GameObject unit;
    public int unitCount;
    public GameObject[] unitList;
    Astar[] astarList;

	// Use this for initialization
	void Start () {

        //Initialise the size of the array
        unitList = new GameObject[unitCount];
        astarList = new Astar[unitCount];

        //Spawn units
        for (int i = 0; i < unitCount; i++)
        {
            int randX = Random.Range(0, 41);
            int randZ = Random.Range(0, 41);


            unitList[i] = Instantiate(unit, new Vector3(randX, 0.65f, randZ), Quaternion.identity) as GameObject;
            unitList[i].transform.parent = gameObject.transform;

            astarList[i] = unitList[i].GetComponent<Astar>();
        }
    }

    public void StartPathfinding(Vector2 goal)
    {
        foreach(Astar a in astarList)
        {
            a.StartPathfinding(goal);
        }
    }
}
