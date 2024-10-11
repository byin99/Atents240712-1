using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airplane_241010 : MonoBehaviour
{
    public float rotateSpeed = 720.0f;
    public float moveSpeed = 5.0f;

    Transform[] waypoints;
    Transform propeller;
    int targetIndex = 0;

    private void Awake()
    {
        waypoints = new Transform[2];
        propeller = transform.GetChild(4);
    }

    private void Start()
    {
        waypoints[0] = GameObject.Find("Waypoint1").transform;
        waypoints[1] = GameObject.Find("Waypoint2").transform;
    }

    private void Update()
    {
        transform.Translate(Time.deltaTime * moveSpeed * transform.forward, Space.World);
        propeller.Rotate(0, 0, Time.deltaTime * rotateSpeed);

        if( (transform.position - waypoints[targetIndex].position).sqrMagnitude < 0.0025f )
        {
            transform.position = waypoints[targetIndex].position;
            GoNextWaypoint();
        }
    }

    void GoNextWaypoint()
    {
        targetIndex++;
        targetIndex %= waypoints.Length;
        
        transform.LookAt(waypoints[targetIndex].position);
    }

}
