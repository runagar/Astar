using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

    //Cache the grid script
    Grid map;
    SpawnFlockBuddies spawnScript;

    //Vectors for own position and parent posistion
    public Vector2 pos;


    void Start()
    {
        map = GameObject.Find("GridManager").GetComponent<Grid>();
        spawnScript = GameObject.Find("UnitManager").GetComponent<SpawnFlockBuddies>();

        this.pos = new Vector2(transform.position.x, transform.position.z);
    }

    private void OnMouseDown()
    {
        spawnScript.StartPathfinding(pos);   
    }

}
