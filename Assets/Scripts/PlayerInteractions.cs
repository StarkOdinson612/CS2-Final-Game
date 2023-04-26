using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField]
    private bool hasKeyCard;

    public GameObject keyCardUIParent;

    // Start is called before the first frame update
    void Start()
    {
        hasKeyCard = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("KeyCard"))
        {
            keyCardUIParent.SetActive(true);


        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("KeyCard"))
        {
            StartCoroutine(PlayerInteractions.waitForTimeCustom(0.3f));
            keyCardUIParent.SetActive(false);
        }
    }

    public static IEnumerator waitForTimeCustom(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
