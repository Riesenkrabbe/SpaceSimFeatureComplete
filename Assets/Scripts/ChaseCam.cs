using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseCam : MonoBehaviour
{
    public float bias = 0.4f;
    Transform pl;
    // Start is called before the first frame update
    void Start()
    {
        pl = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 moveCamTo = pl.position - pl.forward * 6.0f + pl.up * 2.0f;//Chasecam
        

        transform.position = Vector3.Lerp(transform.position, 
                                        moveCamTo,
                                          bias *Time.deltaTime/Time.fixedDeltaTime);


        //transform.position = transform.position * bias + moveCamTo * (1.0f - bias);




        //Camera.main.transform.rotation;// Quaternion.Slerp(Camera.main.transform.rotation, rot, counter);
        

        
        transform.LookAt (pl.position + pl.forward * 25.0f, Vector3.Slerp(transform.up,pl.up,0.05f)); //look 25m infront of the plane
        //Vector3 relativePos = pl.position - transform.position;
        //Quaternion rot = Quaternion.LookRotation(relativePos, pl.up);
        
        
    }   

}
