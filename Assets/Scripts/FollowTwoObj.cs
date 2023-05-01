using DigitalRuby.LightningBolt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTwoObj : MonoBehaviour
{
    public GameObject obj;
    public GameObject target;
    private LightningBoltScript boltScript;

    // Start is called before the first frame update
    void Start()
    {
        boltScript = GetComponent<LightningBoltScript>();
    }

    // Update is called once per frame
    void Update()
    {
        boltScript.StartObject = obj;
        boltScript.EndObject = target;
    }
}
