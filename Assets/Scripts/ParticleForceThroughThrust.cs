using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleForceThroughThrust : MonoBehaviour
{
    ParticleSystem ps;
    float velocityStatic = 30.0f;
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        ps.startSpeed = velocityStatic * Input.GetAxis("Boost");
    }
}
