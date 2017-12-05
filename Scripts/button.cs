using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
		
	}
    public void OnClick_Respown()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
    public void OnClick_Exit()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        //Application.Quit();
    }
    // Update is called once per frame
    void Update () {
		
	}
}
