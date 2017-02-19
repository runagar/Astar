using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steering : MonoBehaviour {

    List<Vector3> path;
    int currentWaypoint;
    int     maxSpeed;
    Vector3 velocity;

    float   pathWeight,
            seperationWeight,
            cohesionWeight,
            alignmentWeight,
            avoidanceWeight;

    bool    receivedFirstCommand;   

    // Use this for initialization
    void Start () {

        maxSpeed = 4;
        velocity = Vector3.zero;
        path = new List<Vector3>();

        pathWeight = 1;
        seperationWeight = 0.15f;
        cohesionWeight = 0.4f;
        alignmentWeight = 0.1f;
        //avoidanceWeight = 1f;



        receivedFirstCommand = false;
    }
	
    public void giveMoveOrder(List<Vector3> p)
    {
        path = p;
        currentWaypoint = 0;
        receivedFirstCommand = true;
    }

	// Update is called once per frame
	void Update () {

        //Don't move if you haven't received your first move order (so they don't move to (0, 0, 0) as the application starts)
        if (!receivedFirstCommand || currentWaypoint == path.Count)
        {
            if (path.Count > 0 && Vector3.Distance(path[currentWaypoint - 1], transform.position) > 0.2f)
            {
                currentWaypoint--;
            }
            else return;
        }

        //Set the velocity to go towards the next waypoint
        velocity = (path[currentWaypoint] - transform.position).normalized * pathWeight;

        //Define variables and list needed for cohesion, seperation and alignment
        Vector3 averageLocation = Vector3.zero;
        Vector3 averageDirection = Vector3.zero;
        int numberOfBuddies = 0;
        Collider[] nearbyBuddies = Physics.OverlapSphere(transform.position, 2f);

        for (int i = 0; i < nearbyBuddies.Length; i++)
        {
            if (nearbyBuddies[i].name.Contains("Buddy"))
            {
                numberOfBuddies++;

                //Add up all the positions and directions of the nearby buddies
                averageLocation += nearbyBuddies[i].transform.position;
                averageDirection += nearbyBuddies[i].transform.forward;

                //Offset the velocity with the seperation, so they will not move in a single line
                Vector3 seperationOffset = nearbyBuddies[i].transform.position - transform.position;
                velocity += (seperationOffset / -seperationOffset.sqrMagnitude).normalized * seperationWeight;

            }
        }

        //Offset velocity based on cohesion and alignment, if there are any nearby buddies
        if (numberOfBuddies > 0)
        {
            //Divide by total number of buddies to get average
            averageLocation /= numberOfBuddies;
            averageDirection /= numberOfBuddies;

            velocity += (averageLocation - transform.position).normalized * cohesionWeight;
            velocity += averageDirection.normalized * alignmentWeight;
        }


        //Cap movement speed to max speed
        velocity = velocity.normalized * maxSpeed;

        //Move him in the direction of the velocity
        transform.position += velocity * Time.deltaTime;

        //Face him in the correct direction
        if (velocity != Vector3.zero) transform.forward = velocity;

        //Make sure they are on the ground
        transform.position = new Vector3(transform.position.x, 0.65f, transform.position.z);

        //Find the next point on the path, if the current point has been reached
        if (Vector3.Distance(path[currentWaypoint], transform.position ) < 0.2f) currentWaypoint++;
	}
}
