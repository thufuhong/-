using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_EscMenu : MonoBehaviour
{
    GameObject root;
    GameObject EscMenu;
    GameObject SettingMenu;
	// Use this for initialization
	void Start () {

        root = GameObject.Find("Canvas");
        EscMenu = root.transform.Find("EscMenu").gameObject;
        SettingMenu = root.transform.Find("SettingMenu").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
			if (EscMenu.active == true) {
                Button_Backgame();
				return;
            }
            if (SettingMenu.active == true)
            {
                Time.timeScale = 1;
                SettingMenu.SetActive(false);
            }  else {
				Time.timeScale = 0;          //暂停
				EscMenu.SetActive(true);     //显示菜单
			}

        }
    }
    public void Button_Backgame()
    {
        EscMenu.SetActive(false);       //隐藏菜单
        Time.timeScale = 1;             //继续游戏
    }
    public void Button_Setting()
    {
        EscMenu.SetActive(false);
        SettingMenu.SetActive(true);
    }
    public void Button_BackEscMenu()
    {
        EscMenu.SetActive(true);
        SettingMenu.SetActive(false);
    }
}
