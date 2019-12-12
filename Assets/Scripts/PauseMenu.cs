using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject menu; // Assign in inspector
    private bool isShowing = false;
    // Start is called before the first frame update
    void Start()
    {
        menu.GetComponent<MenuSettings>().StartUp();
        menu.SetActive(isShowing);
    }
   
    float ConvertBoolToFloat(bool bl)
    {
        if (bl)
        {
            return 1.0f;
        }
        else
        {
            return 0.0f;
        }
    }
    void Update()
    {
        if (Input.GetButtonDown("Menu")) //Activates or deactivates menu
        {
            isShowing = !isShowing;
            menu.SetActive(isShowing);
            Time.timeScale = 1.0f - ConvertBoolToFloat(isShowing); //  freeze all game physics
        }
       
    }
}
