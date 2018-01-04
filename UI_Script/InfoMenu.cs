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
    public GameObject mask;

        // Use this for initialization
	void Start () {


       
	}
	
	// Update is called once per frame
	void Update () {
        nicheng.text = Fuwang.GetComponent<attribute>().NiCheng;
        zhiye.text = Fuwang.GetComponent<attribute>().ZhiYe == "warrior"?"战士":"法师";
        if(Fuwang.GetComponent<attribute>().ZhiYe == "warrior")
        {
            gameObject.transform.Find("Head").gameObject.SetActive(true);
            gameObject.transform.Find("Head2").gameObject.SetActive(false);
        }
        else
        {
            gameObject.transform.Find("Head").gameObject.SetActive(false);
            gameObject.transform.Find("Head2").gameObject.SetActive(true);
        }
        dengji.text = ((int)Fuwang.GetComponent<attribute>().Level).ToString();
        atk.text = ((int)Fuwang.GetComponent<attribute>().ATK).ToString()+( (int)Fuwang.GetComponent<attribute>().ATK_bouns >0? "<color=green>+" + ((int)Fuwang.GetComponent<attribute>().ATK_bouns).ToString()+"</color>":"");
        def.text = ((int)Fuwang.GetComponent<attribute>().DEF).ToString() + ((int)Fuwang.GetComponent<attribute>().DEF_bouns > 0 ? "<color=green>+" + ((int)Fuwang.GetComponent<attribute>().DEF_bouns).ToString() + "</color>" : "");
        //((int)Fuwang.GetComponent<attribute>().DEF+ (int)Fuwang.GetComponent<attribute>().DEF_bouns).ToString();
        hp.text = ((Mathf.Max(0,(int)Fuwang.GetComponent<attribute>().HP)).ToString() + '/' + ((int)Fuwang.GetComponent<attribute>().HP_max).ToString());
        mp.text = (((int)Fuwang.GetComponent<attribute>().MP).ToString() + '/' + ((int)Fuwang.GetComponent<attribute>().MP_max).ToString());
        exp.text = (((int)Fuwang.GetComponent<attribute>().EXP).ToString() + '/' + ((int)Fuwang.GetComponent<attribute>().EXPForLevelUp).ToString());
        money.text = ((int)Fuwang.GetComponent<attribute>().gold).ToString();
    }
    public void CloseInfo()
    {
        Info.SetActive(false);
        mask.GetComponent<MaskCount>().count--;
        if (mask.GetComponent<MaskCount>().count == 0)
        {
            Time.timeScale = 1;
            mask.SetActive(false);
        }
    }
}
