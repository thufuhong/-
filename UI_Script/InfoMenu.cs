using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoMenu : MonoBehaviour
{


    public GameObject Fuwang;
    public GameObject Info;
    public Text nicheng;
    public Text zhiye;
    public Text dengji;
    public Text atk;
    public Text def;
    public Text hp;
    public Text mp;
    public Text money;
    public Text exp;

        // Use this for initialization
	void Start () {


       
	}
	
	// Update is called once per frame
	void Update () {
        nicheng.text = Fuwang.GetComponent<attribute>().NiCheng;
        zhiye.text = Fuwang.GetComponent<attribute>().ZhiYe;
        dengji.text = ((int)Fuwang.GetComponent<attribute>().Level).ToString();
        atk.text = ((int)Fuwang.GetComponent<attribute>().ATK).ToString();
        def.text = ((int)Fuwang.GetComponent<attribute>().DEF).ToString();
        hp.text = (((int)Fuwang.GetComponent<attribute>().HP).ToString() + '/' + ((int)Fuwang.GetComponent<attribute>().HP_max).ToString());
        mp.text = (((int)Fuwang.GetComponent<attribute>().MP).ToString() + '/' + ((int)Fuwang.GetComponent<attribute>().MP_max).ToString());
        exp.text = (((int)Fuwang.GetComponent<attribute>().EXP).ToString() + '/' + ((int)Fuwang.GetComponent<attribute>().EXPForLevelUp).ToString());
        money.text = ((int)Fuwang.GetComponent<attribute>().gold).ToString();
    }
    public void CloseInfo()
    {
        Info.SetActive(false);
        Time.timeScale = 1;
    }
}
