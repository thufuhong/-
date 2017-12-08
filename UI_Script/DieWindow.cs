using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DieWindow : MonoBehaviour {

    public GameObject Die;
    public GameObject Fuwang;
    public Text MainText;
	// Use this for initialization
	void Start () {
        MainText.text =Fuwang.GetComponent<attribute>().NiCheng + "，您已死亡...";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Button_Renew()
    {
       
//         Time.timeScale = 1;
        SceneManager.LoadScene("LevelFirst", LoadSceneMode.Single);
//         Time.timeSinceLevelLoad("LevelFirst");
    }
    public void Button_Back()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
