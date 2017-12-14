using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameButton : MonoBehaviour {

    GameObject Button_Restart;
    
	// Use this for initialization
	void Start () {
        Button_Restart = GameObject.Find("Button_Restart");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    public void StartGame()
    {
        Time.timeScale = 1;
    }
    public void ShowGame()
    {
        //if (Button_Restart.active == true)
       // {
           Button_Restart.SetActive(false);

      //   }
       /*  else if (Button_Restart.active == false)
        {
             Button_Restart.SetActive(true);
        }*/
    }

}
