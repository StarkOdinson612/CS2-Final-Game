using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    private EnemyStateManager stateManager;
    private Collider2D thisCollider;

    public float hangTime = 7f;

    // Start is called before the first frame update
    void Start()
    {
        stateManager = GetComponentInParent<EnemyStateManager>();
        thisCollider = GetComponentInParent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void FixedUpdate()
    {
        for (float angle = -30; angle <= 30; angle += 1)
        {
            Debug.DrawRay(transform.position, transform.position + DirFromAngle(angle, false), Color.cyan, 0.01f);
        }
    }

    public Vector3 DirFromAngle(float angleInDegree, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegree += transform.eulerAngles.y;
        }

        return new Vector3(Mathf.Sin(angleInDegree * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegree * Mathf.Deg2Rad));
    }
}
