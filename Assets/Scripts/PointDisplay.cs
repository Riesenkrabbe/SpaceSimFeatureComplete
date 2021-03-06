﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointDisplay : MonoBehaviour
{
    public Text Points;
    public Text Combo;
    private PointSystem ps;
    // Start is called before the first frame update
    void Start()
    {
        ps = GameObject.Find("Player").GetComponent<PointSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        Combo.text = "Combo: " + Mathf.RoundToInt(ps.combo);
        Combo.fontSize = (int)(ps.combo / 20.0f);
        if (Combo.fontSize > 30)
        {
            Combo.fontSize = 30;
        }
        Points.text = "POINTS: " + Mathf.RoundToInt(ps.points);
    }
}
