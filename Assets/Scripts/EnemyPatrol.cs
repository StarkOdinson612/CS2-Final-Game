using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyPatrol : MonoBehaviour
{
    public Transform[] points;
    private int currentDestPoint;

    private EnemyStateManager stateManager;

    public Vector3 dir;

    public float timeDelay = 1f;

    [SerializeField]
    private float moveSpeed = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        if (points.Length != 0)
        {
            currentDestPoint = 0;
        }
        else { return; }

        transform.up = points[currentDestPoint].position - transform.position;

        stateManager = GetComponent<EnemyStateManager>();
        stateManager.setState(EnemyState.PATROLLING);
    }

    // Update is called once per frame
    void Update()
    {
        EnemyState state = stateManager.getState();
        if (state == EnemyState.PATROLLING)
        {
            dir = points[currentDestPoint].position - transform.position;
            float dirAngle = GetAngleFromVectorFloat(dir);

            Quaternion rotDir = Quaternion.Euler(new Vector3(0, 0, dirAngle - transform.rotation.z - 90));
            transform.rotation = Quaternion.Slerp(transform.rotation, rotDir, 4 * Time.deltaTime);
            
            if (Vector2.Distance(transform.position, points[currentDestPoint].position) < 0.2f)
            {
                StartCoroutine(NextTargetLocation());
            }
            
            if (stateManager.getState() == EnemyState.PATROLLING)
            {
                transform.position = Vector2.MoveTowards(transform.position, points[currentDestPoint].position, moveSpeed * Time.deltaTime);
            }
        }
        else if (state == EnemyState.DISCOVERED_PLAYER || state == EnemyState.CAUGHT_PLAYER)
        {

			float dirAngle = GetAngleFromVectorFloat(stateManager.getPlayerPos().position - transform.position);

			Quaternion rotDir = Quaternion.Euler(new Vector3(0, 0, dirAngle - transform.rotation.z - 90));
			transform.rotation = Quaternion.Slerp(transform.rotation, rotDir, 1 * Time.deltaTime);
		}
    }

    IEnumerator NextTargetLocation()
    {
        stateManager.setState(EnemyState.STOPPED);

        if (currentDestPoint < points.Length - 1)
        {
            currentDestPoint++;
        }
        else if (currentDestPoint == points.Length - 1) 
        {
            currentDestPoint = 0;
        }
        else
        {
            yield break;
        }

        yield return new WaitForSeconds(timeDelay);

        stateManager.setState(EnemyState.PATROLLING);

    }

	public static float GetAngleFromVectorFloat(Vector3 dir)
	{
		dir = dir.normalized;
		float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

		if (n < 0)
		{
			n += 360;
		}

		return n;
	}
}
