using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    private EnemyStateManager stateManager;
    private Collider2D thisCollider;

    private float sightDist = 6.8f;

    public float hangTime = 7f;

    [SerializeField]
    private LayerMask playerMask;

    int viewCounter;

    // Start is called before the first frame update
    void Start()
    {
        stateManager = GetComponentInParent<EnemyStateManager>();
        thisCollider = GetComponentInParent<Collider2D>();
        playerMask = LayerMask.GetMask("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void FixedUpdate()
    {
        EnemyState state = stateManager.getState();
        List<RaycastHit2D> hits = new List<RaycastHit2D>();
        for (float angle = -20f; angle <= 20f; angle += 4)
        {
            Vector3 vec = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.up;
            Debug.DrawRay(transform.position, transform.rotation * vec.normalized * sightDist, Color.cyan, 0.01f);

            hits.Add(Physics2D.Raycast(transform.position, transform.rotation * vec.normalized, 7, playerMask));
        }

        hits = hits.Where(hit => hit.collider != null).ToList();

        if (hits.Count > 0)
        {
            Debug.Log(hits[0].collider.gameObject);
            if (state != EnemyState.CAUGHT_PLAYER && state != EnemyState.STUNNED) { stateManager.setState(EnemyState.DISCOVERED_PLAYER); }
			stateManager.setPlayerPos(hits[0].collider.gameObject.transform);

			viewCounter++;
			Debug.Log(viewCounter / 60);

			if (viewCounter / 60 > 3)
			{
				stateManager.setState(EnemyState.CAUGHT_PLAYER);
			}
		}
		else { viewCounter = 0; stateManager.setState(EnemyState.PATROLLING); }
	}

    public Vector3 DirFromAngle(float angleInDegree, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegree += transform.eulerAngles.y;
        }

        return new Vector3(Mathf.Cos(angleInDegree * Mathf.Deg2Rad), Mathf.Sin(angleInDegree * Mathf.Deg2Rad), 0);
    }

   
}
