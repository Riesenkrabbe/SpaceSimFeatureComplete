using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanePilot : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    public float liftStatic = 3.0f;
    //public float railsFactor = 0.2f;
    public float glideFactor = 0.5f;
    public float rudder = 10.0f;
    public float alteron = 10.0f;
    public float elevator = 10.0f;
    public AnimationCurve axis;
    private float counter;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Plane Pilot Script startup");
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        
        float angle = Vector3.Angle(transform.forward, rb.velocity.normalized);//Are we looking into the slipstream or not

        //Old System
        /*rb.velocity =     Mathf.Abs(Mathf.Sin(angle)) * rb.velocity * 0.15f  //Realistic
                        + rb.velocity * 0.75f
                        + transform.forward * speed * Input.GetAxis("Boost") * 0.10f  //Arcady
                        + transform.up * 5 * lift * (rb.velocity.magnitude / speed) * (Mathf.Abs(Mathf.Sin(angle))); //Lift
        */


        //Lift
        Vector3 lift = Vector3.Project(rb.velocity.normalized, transform.up);   //Only use forward velocity for lift
        lift = lift * rb.velocity.magnitude / speed                             //the faster we go the faster our lift
                    * liftStatic;                                               //How much lift do our wings produce?

        //Glide

        Vector3 glide = transform.forward * rb.velocity.magnitude * Mathf.Cos(Vector3.Angle(transform.forward, rb.velocity) * Mathf.Deg2Rad) * glideFactor - rb.velocity * glideFactor; //* 0.0174533f is degree to radians
        
        //Drag
        Vector3 drag = rb.velocity * (-1) * Mathf.Sin(angle * Mathf.Deg2Rad) * liftStatic;//If our plane is at 90 degrees into the slipstream the drag is highest

        
        //Rails
        //Vector3 rails = Vector3.Project(rb.velocity.normalized, rb.transform.forward)*rb.velocity.magnitude;
        //rb.velocity = rails *railsFactor - rb.velocity * (1.0f - railsFactor);
        
        //Rails
        /*float mag = rb.velocity.magnitude;
        rb.velocity = rb.transform.forward  * railsFactor + rb.velocity.normalized * (1.0f - railsFactor); // simple arcade feeling
        rb.velocity = rb.velocity.normalized * mag;
        */
        
        
        //Thrust
        Vector3 thrust = transform.forward * speed * Input.GetAxis("Boost");

        //Rails
        /*Debug.DrawLine(transform.position, transform.position + rb.velocity);
        Vector3 rails = Vector3.Scale(rb.velocity.normalized, rb.transform.forward.normalized);

        Debug.DrawLine(transform.position, transform.position + Vector3.Scale(transform.forward * rb.velocity.magnitude, rails) * railsFactor);
        Debug.DrawLine(transform.position, transform.position + rb.velocity * (1.0f - railsFactor));
        rails = Vector3.Scale(transform.forward * rb.velocity.magnitude, rails) *railsFactor //How much velocity change is done 
                + rb.velocity * (1.0f - railsFactor); //How much velocity it keeps

        rb.velocity = rails;
        */
        //Apply Forces
        
        rb.AddForce(lift + drag + thrust +glide);// + glide);//+ rails);


        //rb.drag = Mathf.Abs(Mathf.Sin(angle * 0.0174533f * 2));
        //rb.AddForce(thrust);

        //Turn
        float verticalAxis; float horizontalAxis; float yawAxis;
        verticalAxis   = Input.GetAxis("Vertical");
        horizontalAxis = Input.GetAxis("Horizontal");
        yawAxis = Input.GetAxis("Yaw");
        verticalAxis = axis.Evaluate(verticalAxis);
        horizontalAxis = axis.Evaluate(horizontalAxis);
        yawAxis = axis.Evaluate(yawAxis);
        Vector3 rot = new Vector3(verticalAxis * elevator, yawAxis * rudder, -horizontalAxis * alteron);
        //* rb.velocity.magnitude / speed * Mathf.Cos(angle * Mathf.Deg2Rad); //Turn rate based on wethever we are looking into the slipstream or not   and its higher if we go fast
        
        //rot = rot +  rb.velocity.magnitude / speed; //Old System
        rb.AddRelativeTorque(rot);
        

    }



    // Update is called once per frame
    void Update()
    {
        
        
        //float floorHeightWhereWeAre = Terrain.activeTerrain.SampleHeight(transform.position);
        /*if (floorHeightWhereWeAre > transform.position.y) // we are under our terrain
        {
            transform.position = new Vector3(transform.position.x,
                                            floorHeightWhereWeAre,
                                            transform.position.z);
        }*/
    }
    void LateUpdate()
    {
       

    }
}
