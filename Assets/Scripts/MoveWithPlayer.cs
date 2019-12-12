using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithPlayer : MonoBehaviour
{
    private Transform pl;
    // Start is called before the first frame update
    void Start()
    {
        pl = GameObject.Find("Player").GetComponent<Transform>();
        Debug.Log("Move with Player startup");
    }
    private void FixedUpdate()
    {
        transform.position = pl.position; //Just copy the position
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
