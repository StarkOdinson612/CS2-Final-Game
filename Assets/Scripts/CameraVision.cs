using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class CameraVision : MonoBehaviour
{
    private CameraStateManager stateManager;

    private float sightDist = 6.8f;

    public float hangTime = 7f;

    [SerializeField]
    private LayerMask playerMask;

    public Transform origin;

    int viewCounter;

    // Start is called before the first frame update
    void Start()
    {
        stateManager = GetComponentInParent<CameraStateManager>();
        playerMask = LayerMask.GetMask("Player", "Default");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        CameraState state = stateManager.getState();
        List<RaycastHit2D> hits = new List<RaycastHit2D>();
        for (float angle = -20f; angle <= 20f; angle += 4)
        {
            Vector3 vec = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;
            Debug.DrawRay(origin.position, transform.localRotation * vec.normalized * sightDist, Color.cyan, 0.01f);

            hits.Add(Physics2D.Raycast(origin.position, transform.localRotation * vec.normalized, 7, playerMask));
        }

        hits = hits.Where(hit => hit.collider != null && hit.collider.gameObject.CompareTag("Player")).ToList();

        if (hits.Count > 0)
        {
            Debug.Log(hits[0].collider.gameObject);
            if (state != CameraState.CAUGHT_PLAYER) { stateManager.setState(CameraState.DISCOVERED_PLAYER); }
            stateManager.setPlayerPos(hits[0].collider.gameObject.transform);

            viewCounter++; 
            // Debug.Log(viewCounter / 60);

            if (viewCounter / 60 > 3)
            {
                stateManager.setState(CameraState.CAUGHT_PLAYER);
            }
        }
        else { viewCounter = 0; if (state != CameraState.CAUGHT_PLAYER) { stateManager.setState(CameraState.PATROLLING); } }
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
