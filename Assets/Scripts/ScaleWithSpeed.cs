using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleWithSpeed : MonoBehaviour
{
    private Rigidbody rb;
    private PlanePilot pp;
    // Start is called before the first frame update
    void Start()
    {
        rb = GameObject.Find("Player").GetComponent<Rigidbody>();
        pp = GameObject.Find("Player").GetComponent<PlanePilot>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //float mt = rb.velocity.magnitude;
        //transform.localScale = new Vector3(2.0f * mt / pp.speed, 2.0f * mt / pp.speed, 2.0f * mt / pp.speed);
        
    }
}
