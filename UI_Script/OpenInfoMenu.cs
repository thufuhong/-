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
                mask.GetComponent<MaskCount>().count--;
                if (mask.GetComponent<MaskCount>().count == 0)
                {
                    Time.timeScale = 1;
                    mask.SetActive(false);
                }
            }
            else
            {
                mask.GetComponent<MaskCount>().count++;
                Time.timeScale = 0;
                mask.SetActive(true);
                Info.SetActive(true);     
            }

        }
	}
    public void ButtonHead ()
    {
        mask.GetComponent<MaskCount>().count++;
        Time.timeScale = 0;
        mask.SetActive(true);
        Info.SetActive(true); 
    }
}
