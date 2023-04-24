using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] points;
    private int currentDestPoint;
    private bool hasStopped;

    public Vector3 dir;

    public float timeDelay = 1f;

    // Start is called before the first frame update
    void Start()
    {
        if (points.Length != 0)
        {
            currentDestPoint = 0;
        }
        else { return; }

        hasStopped = false;
        transform.up = points[currentDestPoint].position - transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
