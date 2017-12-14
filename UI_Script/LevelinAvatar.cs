using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelinAvatar : MonoBehaviour {

    public GameObject Fuwang;
    public Text ui_Level;
	// Use this for initialization
	void Start () {

        ui_Level.text = ((int)Fuwang.GetComponent<attribute>().Level).ToString();
	}
	
	// Update is called once per frame
	void Update () {
        ui_Level.text = ((int)Fuwang.GetComponent<attribute>().Level).ToString();
	}
}
