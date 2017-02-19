using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    //Define required variables
    public GameObject Tile;     //The object prefab used as tiles
    public GameObject[,] tiles;        //An array of tiles

    //Define the layout and cost of the map
    public int[,] map;
    int gridSize;

    // Use this for initialization
    void Start()
    {
        //Initialise the size of the map, and the array that hold its coordinats and draw it.
        gridSize = 41;
        map = new int[gridSize, gridSize];

        //Initialise the size of the tile array
        tiles = new GameObject[gridSize, gridSize];

        for (int y = 0; y < gridSize; y++)
        {
            for (int x = 0; x < gridSize; x++)
            {
                map[x, y] = Random.Range(0, 6);
                //Instantiate a tile object at the proper position
                tiles[x, y] = Instantiate(Tile, new Vector3(x, 0, y), Quaternion.identity) as GameObject;
                tiles[x, y].transform.parent = gameObject.transform;

                switch (map[x, y])
                {
                    case 0:
                        tiles[x, y].GetComponent<Renderer>().material.color = new Vector4(0f, 0.8f, 0f, 0f); //Light green is grass
                        break;
                    case 1:
                        tiles[x, y].GetComponent<Renderer>().material.color = new Vector4(0f, 0.3f, 0f, 0f); //Dark green is forest
                        break;
                    case 2:
                        tiles[x, y].GetComponent<Renderer>().material.color = new Vector4(0.4f, 0.5f, 0f, 0f); //Greenish brown is hills
                        break;
                    case 3:
                        tiles[x, y].GetComponent<Renderer>().material.color = new Vector4(0f, 0f, 1f, 0f); //Light blue is for shallow water
                        break;
                    case 4:
                        tiles[x, y].GetComponent<Renderer>().material.color = new Vector4(0.2f, 0.2f, 0.2f, 0f); //Dark Gray is for mountain
                        break;
                    case 5:
                        tiles[x, y].GetComponent<Renderer>().material.color = new Vector4(0f, 0f, 0.3f, 0f); //Dark blue is for deep water
                        break;
                    default:
                        break;
                }
            }
        }

        //StartCoroutine(slowAnim());
    }

    // Update is called once per frame
    void Update () {
		
	}
}
