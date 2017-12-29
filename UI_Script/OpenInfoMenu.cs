using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenInfoMenu : MonoBehaviour
{

   public GameObject Info;
    public GameObject mask;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (Info.active == true)
            {
                Info.SetActive(false);
                Time.timeScale = 1 ;
                mask.SetActive(false);
            }
            else
            {
                Time.timeScale = 0;
                mask.SetActive(true);
                Info.SetActive(true);     
            }

        }
	}
    public void ButtonHead ()
    {
        Time.timeScale = 0;
        mask.SetActive(true);
        Info.SetActive(true); 
    }
}
