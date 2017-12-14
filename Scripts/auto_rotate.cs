using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class auto_rotate : MonoBehaviour {
    public float speed = 10;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.gameObject.transform.Rotate(0, 0, speed * Time.fixedDeltaTime);
        
	}
}
