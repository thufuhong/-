﻿using System.Collections;
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
        MainText.text = Fuwang.GetComponent<attribute>().NiCheng + "，您已死亡。\n点击“买活”可以花费1000个金币原地复活。\n点击“重新开始”回退到本关开始。";
        gameObject.transform.Find("goldowned").gameObject.GetComponent<Text>().text = Fuwang.GetComponent<attribute>().gold.ToString();
        gameObject.transform.Find("goldowned").gameObject.GetComponent<Text>().color = Fuwang.GetComponent<attribute>().gold >= 1000 ? Color.black : Color.red;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Button_Renew()
    {
        if(Fuwang.GetComponent<attribute>().gold>=1000)
        {
            Fuwang.GetComponent<attribute>().gold -= 1000;
            Fuwang.GetComponent<attribute>().HP = Fuwang.GetComponent<attribute>().HP_max;
            Fuwang.GetComponent<attribute>().ifAlive = true;
            Fuwang.GetComponent<Animator>().SetTrigger("reborn");
            this.gameObject.SetActive(false);
            Time.timeScale = 1;
        }

		// Time.timeScale = 1;
        // SceneManager.LoadScene("LevelFirst", LoadSceneMode.Single);
		// SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
		// // Time.timeSinceLevelLoad("LevelFirst");
		// Time.timeSinceLevelLoad(SceneManager.GetActiveScene().name);
    }
    public void Button_Back()
    {
        //UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
}
