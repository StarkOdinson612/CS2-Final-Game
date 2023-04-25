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

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("vis");
            StartCoroutine(HangTimeWait());

            if (thisCollider.IsTouching(collider))
            {
                Debug.Log("Caught ya!");
            }
        }
	}

    IEnumerator HangTimeWait()
    {
        yield return new WaitForSeconds(hangTime);
    }
}
