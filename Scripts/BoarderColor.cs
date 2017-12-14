using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarderColor : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		Color temp = new Color ();
		temp.a = 0f;
		this.GetComponent<Renderer> ().material.color = temp;
		Debug.Log (this.GetComponent<Renderer> ().material.color.a);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
