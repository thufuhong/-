using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawn : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    	
	}
	
    public void OnClick_Respown()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        //Application.LoadLevel(0);
    }

	// Update is called once per frame
	void Update () {
		
	}
}
