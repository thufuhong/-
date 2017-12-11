using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu_Sever : MonoBehaviour {
    public Transform BackGroundTransform;
    public Vector3 DeltaCenter = new Vector3(25, 0, 0);
    public Vector3 DiffCenter = new Vector3(-2, 0, 0);
    public int counter = 0;
    public GameObject choosePlayer;

    private Vector3 BackgtoundTarget;
    private Vector3 CameraTarget;
    // Use this for initialization
    void Start () {
        BackgtoundTarget = BackGroundTransform.position;
        CameraTarget = Camera.main.transform.position;
        

    }
    public void ChooseCharacter(int x)
    {
        PlayerPrefs.SetInt("Character", x);
		PlayerPrefs.SetInt ("level_num", 1);
        BackgtoundTarget = BackgtoundTarget + DeltaCenter + DiffCenter;
        CameraTarget = CameraTarget + DeltaCenter;
        Invoke("LoadFirstScene", 2);

    }
    void LoadFirstScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelFirst");
    }
	// Update is called once per frame
	void FixedUpdate () {
        counter = counter + 1;
        if (counter % 50 == 0)
            Debug.Log("Sever Alive.");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        GameObject t = hit.collider.gameObject;
        Debug.Log("Mouse Target: " + t.name);
        if (Input.GetButtonDown("Fire2") && BackgtoundTarget.x > 10.0f)
        {
            BackgtoundTarget = BackgtoundTarget - DeltaCenter - DiffCenter;
            CameraTarget = CameraTarget - DeltaCenter;
        }
        if (t.tag == "MainMenuButton")
        {
            // TODO: some feed back when mouse floating on button
            if (Input.GetButtonDown("Fire1"))
            {
                switch(t.name)
                {
                    case "M1_Exit":
                        Application.Quit();
                        break;
                    case "M2_NewGame":
                        //BackgtoundTarget = BackgtoundTarget + DeltaCenter + DiffCenter;
                        //CameraTarget = CameraTarget + DeltaCenter;
                        //Invoke("LoadFirstScene", 2);
                        choosePlayer.SetActive(true);
                        break;
                    default:
                        BackgtoundTarget = BackgtoundTarget + DeltaCenter + DiffCenter;
                        CameraTarget = CameraTarget + DeltaCenter;
                        break;
                }
                
            }
            
        }
    }

    void Update()
    {
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, CameraTarget, 3 * Time.fixedDeltaTime);
        BackGroundTransform.position = Vector3.Lerp(BackGroundTransform.position, BackgtoundTarget, 3 * Time.fixedDeltaTime);
    }
}
