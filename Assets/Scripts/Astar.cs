using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astar : MonoBehaviour {

    //Define a list for open and burned nodes
    List<Node> Open;
    List<Node> Burned;

    //Define our start and goal node
    Node start;
    Node goal;

    //Cache the map script
    Grid map;

    //Define arrays of cost to a node, and heuristic cost for a node.
    int[,] costToNode;
    int[,] heuristic;

    // Use this for initialization
    void Start()
    {
        //Initialise the lists
        Open = new List<Node>();
        Burned = new List<Node>();

        costToNode = new int[41, 41];
        heuristic = new int[41, 41];

        //Reference the map script
        map = GameObject.Find("GridManager").GetComponent<Grid>();
    }

    public void StartPathfinding(Vector2 g)
    {
        //Clear the lists, so pathfinding can start anew
        Open.Clear();
        Burned.Clear();

        //Cache and reference the goal node
        goal = GameObject.Find("GridManager").GetComponent<Grid>().tiles[(int)g.x, (int)g.y].GetComponent<Node>();

        RaycastHit hitinfo;
        if(Physics.SphereCast(transform.position, 0.05f, new Vector3(0f, -1f, 0f), out hitinfo))
        {
            start = hitinfo.transform.GetComponent<Node>();
        }
        else
        {
            Debug.Log("No tile below");
            return;
        }

        Open.Add(start);

        Node current = Open[0];

        while (Open.Count > 0)
        {
            int x = (int)current.pos.x;
            int y = (int)current.pos.y;
            
            if(x - 1 >= 0 && !Burned.Contains(map.tiles[x - 1, y].GetComponent<Node>()))
            {
                Open.Add(map.tiles[x - 1, y].GetComponent<Node>());
                costToNode[x - 1, y] = costToNode[x, y] + map.map[x, y];
                heuristic[x - 1, y] = costToNode[x - 1, y] + (int)Mathf.Abs(g.x - x - 1 + g.y - y);
                Debug.Log("Heuristic for x - 1 " + heuristic[x - 1, y]);
            }
            if (x + 1 <= 40 && !Burned.Contains(map.tiles[x + 1, y].GetComponent<Node>()))
            {
                Open.Add(map.tiles[x + 1, y].GetComponent<Node>());
                costToNode[x + 1, y] = costToNode[x, y] + map.map[x, y];
                heuristic[x + 1, y] = costToNode[x + 1, y] + (int)Mathf.Abs(g.x - x + 1 + g.y - y);
                Debug.Log("Heuristic for x + 1 " + heuristic[x + 1, y]);
            }
            if (y - 1 >= 0 && !Burned.Contains(map.tiles[x, y - 1].GetComponent<Node>()))
            {
                Open.Add(map.tiles[x, y - 1].GetComponent<Node>());
                costToNode[x, y - 1] = costToNode[x, y] + map.map[x, y];
                heuristic[x, y - 1] = costToNode[x, y - 1] + (int)Mathf.Abs(g.x - x + g.y - y - 1);
                Debug.Log("Heuristic for y - 1 " + heuristic[x, y - 1]);
            }
            if (y + 1 <= 40 && !Burned.Contains(map.tiles[x, y + 1].GetComponent<Node>()))
            {
                Open.Add(map.tiles[x, y + 1].GetComponent<Node>());
                costToNode[x, y + 1] = costToNode[x, y] + map.map[x, y];
                heuristic[x, y + 1] = costToNode[x, y + 1] + (int)Mathf.Abs(g.x - x + g.y - y + 1);
                Debug.Log("Heuristic for y + 1 " + heuristic[x, y + 1]);
            }
            break;
        }
    }
}
