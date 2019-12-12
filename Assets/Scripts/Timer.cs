using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeLeft = 60.0f;
    public PointSystem ps;
    public Text highscore;
    public Text timeLeftText;
    // Start is called before the first frame update
    void Start()
    {
       
    }
    void FixedUpdate()
    {
        timeLeftText.text = "TIME " + (int)timeLeft;
        if (timeLeft <= 0.0f)
        {
            
            highscore.fontSize = 25;
            highscore.text = "HIGHSCORE: " + ps.points;
            Time.timeScale = 0.0f; //Freeze Game

        }
    }
    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
    }
}
