using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour {

    //Define camera movement speed
    public float speed;

    private Vector3 pos;

	// Use this for initialization
	void Start () {
        pos = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButton("W"))
        {
            pos.x -= speed;
            pos.z -= speed;
        }

        if (Input.GetButton("A"))
        {
            pos.x += speed;
            pos.z -= speed;
        }

        if (Input.GetButton("S"))
        {
            pos.x += speed;
            pos.z += speed;
        }

        if (Input.GetButton("D"))
        {
            pos.x -= speed;
            pos.z += speed;
        }

        if (pos.x < 13) pos.x = 13;
        if (pos.x > 47) pos.x = 47;
        if (pos.z < 13) pos.z = 13;
        if (pos.z > 47) pos.z = 47;

        gameObject.transform.position = pos;
    }
}
