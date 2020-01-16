using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSystem : MonoBehaviour
{
    public float points = 0;
    public float combo = 0;
    private float combotimer;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //Physics.IgnoreCollision(GameObject.Find("Player").GetComponent<Collider>(), GetComponent<Collider>());
        Debug.Log("PointSystem Startup");
    }
    void FixedUpdate()
    {
        
    }
    private void OnCollisionStay(Collision collision)
    {
        combo = 0;
        combotimer = 0.0f;
    }
    private void OnTriggerStay(Collider other)
    {
        if (rb.velocity.magnitude <= 15.0f)
        {

        }
        else
        {
            combo += 1.0f * rb.velocity.magnitude/15.0f / 10.0f;
            combotimer = 0.0f;
        }
        Debug.Log(rb.velocity.magnitude);
    }
    private void OnTriggerExit(Collider other)
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        combotimer += Time.deltaTime;
        if (combotimer >= 5.0f)
        {
            points += combo;
            combo = 0;
        }
    }
}
