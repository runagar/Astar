using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astar : MonoBehaviour {

    //Define a list for open and burned nodes, as well as the path
    List<Vector2> Open;
    List<Vector2> Burned;
    List<Vector3> Path;

    //Define our start and goal node
    Vector2 start;
    Vector2 goal;

    //Cache the map script, and the steering script
    Grid map;
    Steering steeringScript;

    //Define arrays of cost to a node, and heuristic cost for a node, as well as an array of the parents for every node
    Vector2[,] parents;
    int[,] costToNode;
    int[,] heuristic;

    // Use this for initialization
    void Start()
    {
        //Initialise the lists
        Open = new List<Vector2>();
        Burned = new List<Vector2>();
        Path = new List<Vector3>();

        parents = new Vector2[41, 41];
        costToNode = new int[41, 41];
        heuristic = new int[41, 41];

        //Reference the map and steering scripts
        map = GameObject.Find("GridManager").GetComponent<Grid>();
        steeringScript = gameObject.GetComponent<Steering>();
    }

    public void StartPathfinding(Vector2 g)
    {
        //Clear the lists, so pathfinding can start anew
        Open.Clear();
        Burned.Clear();
        Path.Clear();

        //Cache and reference the goal node
        goal = g;

        //Find which square the unit is standing on and save that position as the start pos
        RaycastHit hitinfo;
        if(Physics.SphereCast(transform.position, 0.05f, new Vector3(0f, -1f, 0f), out hitinfo))
        {
            start = hitinfo.transform.GetComponent<Node>().pos;
        }
        else
        {
            Debug.Log("No tile below");
            return;
        }

        Open.Add(start);

        //Variable for the pos we're working with
        Vector2 current = Open[0];

        //Where the actul pathfinding happens
        while (Open.Count > 0)
        {

            if (current == goal)
            {
                break;
            }

            //Save x and y coordinates of the current pos for ease of reference
            int x = (int)current.x;
            int y = (int)current.y;

            //In the following four if-statements, go through all the neighbours to the current pos.
            if(x - 1 >= 0 && !Burned.Contains(map.tiles[x - 1, y].GetComponent<Node>().pos))
            {
                Vector2 temp = map.tiles[x - 1, y].GetComponent<Node>().pos;

                //If the pos is already in the open list, only work on it if we found a shorter route here
                if (Open.Contains(temp) && costToNode[x - 1, y] > costToNode[x, y] + map.map[x - 1, y])
                {
                    costToNode[x - 1, y] = costToNode[x, y] + map.map[x - 1, y];
                    heuristic[x - 1, y] = costToNode[x - 1, y] + (int)Mathf.Abs(goal.x - x - 1 + goal.y - y);
                    parents[x - 1, y] = current;
                }
                //Else, if it is not in the open list, calculate all relevant information on it, and add it to the open list
                else if (!Open.Contains(temp))
                {
                    Open.Add(map.tiles[x - 1, y].GetComponent<Node>().pos);
                    costToNode[x - 1, y] = costToNode[x, y] + map.map[x - 1, y];
                    heuristic[x - 1, y] = costToNode[x - 1, y] + (int)Mathf.Abs(goal.x - x - 1 + goal.y - y);
                    parents[x - 1, y] = current;
                }
            }
            if (x + 1 <= 40 && !Burned.Contains(map.tiles[x + 1, y].GetComponent<Node>().pos))
            {
                Vector2 temp = map.tiles[x + 1, y].GetComponent<Node>().pos;
                if (Open.Contains(temp) && costToNode[x + 1, y] > costToNode[x, y] + map.map[x + 1, y])
                {
                    costToNode[x + 1, y] = costToNode[x, y] + map.map[x + 1, y];
                    heuristic[x + 1, y] = costToNode[x + 1, y] + (int)Mathf.Abs(goal.x - x + 1 + goal.y - y);
                    parents[x + 1, y] = current;
                }
                else if (!Open.Contains(temp))
                {
                    Open.Add(map.tiles[x + 1, y].GetComponent<Node>().pos);
                    costToNode[x + 1, y] = costToNode[x, y] + map.map[x + 1, y];
                    heuristic[x + 1, y] = costToNode[x + 1, y] + (int)Mathf.Abs(goal.x - x + 1 + goal.y - y);
                    parents[x + 1, y] = current;
                }
            }
            if (y - 1 >= 0 && !Burned.Contains(map.tiles[x, y - 1].GetComponent<Node>().pos))
            {
                Vector2 temp = map.tiles[x, y - 1].GetComponent<Node>().pos;
                if (Open.Contains(temp) && costToNode[x, y - 1] > costToNode[x, y] + map.map[x, y - 1])
                {
                    costToNode[x, y - 1] = costToNode[x, y] + map.map[x, y - 1];
                    heuristic[x, y - 1] = costToNode[x, y - 1] + (int)Mathf.Abs(goal.x - x + goal.y - y - 1);
                    parents[x, y - 1] = current;
                }
                else if (!Open.Contains(temp))
                {
                    Open.Add(map.tiles[x, y - 1].GetComponent<Node>().pos);
                    costToNode[x , y - 1] = costToNode[x, y] + map.map[x, y - 1];
                    heuristic[x, y - 1] = costToNode[x, y - 1] + (int)Mathf.Abs(goal.x - x + goal.y - y - 1);
                    parents[x, y - 1] = current;
                }
            }
            if (y + 1 <= 40 && !Burned.Contains(map.tiles[x, y + 1].GetComponent<Node>().pos))
            {
                Vector2 temp = map.tiles[x, y + 1].GetComponent<Node>().pos;
                if (Open.Contains(temp) && costToNode[x, y + 1] > costToNode[x, y] + map.map[x, y + 1])
                {
                    costToNode[x, y + 1] = costToNode[x, y] + map.map[x, y + 1];
                    heuristic[x, y + 1] = costToNode[x, y + 1] + (int)Mathf.Abs(goal.x - x + goal.y - y + 1);
                    parents[x, y + 1] = current;
                }
                else if (!Open.Contains(temp))
                {
                    Open.Add(map.tiles[x, y + 1].GetComponent<Node>().pos);
                    costToNode[x, y + 1] = costToNode[x, y] + map.map[x, y + 1];
                    heuristic[x, y + 1] = costToNode[x, y + 1] + (int)Mathf.Abs(goal.x - x + goal.y - y + 1);
                    parents[x, y + 1] = current;
                }

            }

            //Burn the current node
            Open.Remove(current);
            Burned.Add(current);

            //Variable for the lowest heuristic 
            int lowestHeuristic = int.MaxValue;

            //Go through the Open list
            foreach(Vector2 v in Open)
            {
                //If an item in the list has a lower heuristic cost than the previous, make it the next item to be worked on and adjust the lowest heuristic variable
                if (heuristic[(int)v.x, (int)v.y] < lowestHeuristic)
                {
                    lowestHeuristic = heuristic[(int)v.x, (int)v.y];
                    current = v;
                }
            }
        }

        //Once the goal has been found, follow the parents backwards from the goal, adding each step to a list that is our path
        while (current != start)
        {
            Path.Insert(0, new Vector3(current.x, 0.65f, current.y));
            current = parents[(int)current.x, (int)current.y];
        }

        //Start the steering of the unit
        steeringScript.giveMoveOrder(Path);
    }
}
