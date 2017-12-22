using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu_Sever : MonoBehaviour {
    public Transform BackGroundTransform;
    public Vector3 DeltaCenter = new Vector3(25, 0, 0);
    public Vector3 DiffCenter = new Vector3(-2, 0, 0);
    public int counter = 0;
    public GameObject choosePlayer;
    public GameObject loadgame;
    public GameObject developer;

	public GameObject loadSprite;

    private Vector3 BackgtoundTarget;
    private Vector3 CameraTarget;
    private int _x;
    private bool isUIOpen = false;
    // Use this for initialization
    void Start () 
	{
        BackgtoundTarget = BackGroundTransform.position;
        CameraTarget = Camera.main.transform.position;
    }

    public void ChooseCharacter(int x)
    {
        PlayerPrefs.SetInt("Character", x);
		PlayerPrefs.SetInt ("level_num", 1);
		GetComponent<attribute> ().IsInit = true;
		GetComponent<attribute> ().SavePlayerAttribute ();
        BackgtoundTarget = BackgtoundTarget + DeltaCenter + DiffCenter;
        CameraTarget = CameraTarget + DeltaCenter;
        Invoke("LoadFirstScene", 2);
    }
    public void ButtonLoad(int x)
    {

        loadgame.SetActive(false);
        BackgtoundTarget = BackgtoundTarget + DeltaCenter + DiffCenter;
        CameraTarget = CameraTarget + DeltaCenter;
        Invoke("LoadArchive2", 2);
    }

    /*
	interface for save and read archive:

	void LoadFirstScene()
	void LoadArchive(int num)
	void SaveArchive(int num)
	int GetGameLevel(int num)
	float GetPlayerLevel(int num)
	float GetPlayerGold(int num)
	string GetSaveTime(int num)
	
	num=0 means the default archive

	 */

    void LoadFirstScene()
    {
        // UnityEngine.SceneManagement.SceneManager.LoadScene("LevelFirst");
		StartCoroutine(loadScene());
    }

	void LoadArchive(int num)
	{
		GetComponent<attribute> ().ReadAttributeFromFile (num);
		GetComponent<attribute> ().SavePlayerAttribute ();
		// use AsyncOperation Loading
		// UnityEngine.SceneManagement.SceneManager.LoadScene("LevelFirst");
		LoadFirstScene();
	}

	IEnumerator loadScene()
	{
		AsyncOperation async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync ("LevelFirst");
		if (loadSprite != null) 
		{
			loadSprite.GetComponent<LoadRotate> ().async = async;
		}
		yield return async;
	}

    void LoadArchive2()
    {
        int num = _x;
        // GetComponent<attribute>().ReadAttributeFromFile(num);
        // GetComponent<attribute>().SavePlayerAttribute();
        // UnityEngine.SceneManagement.SceneManager.LoadScene("LevelFirst");
		LoadArchive(num);
    }

    void SaveArchive(int num)
	{
		GetComponent<attribute> ().ReadPlayerAttribute ();
		GetComponent<attribute> ().SaveAttributeInFile (num);
	}

	int GetGameLevel(int num)
	{
		return GetComponent<attribute> ().GetLevelOfGameFromFile (num);
	}

	float GetPlayerLevel(int num)
	{
		return GetComponent<attribute> ().GetLevelOfPlayerFromFile (num);
	}

	float GetPlayerGold(int num)
	{
		return GetComponent<attribute> ().GetGoldOfPlayerFromFile (num);
	}

    string GetPlayerType(int num)
    {
        return GetComponent<attribute>().GetTypeOfPlayerFromFile(num);
    }

	string GetSaveTime(int num)
	{
		return GetComponent<attribute> ().GetSaveTimeFromFile (num);
	}

	// Update is called once per frame
	void FixedUpdate () {
        counter = counter + 1;
        //if (counter % 50 == 0)
            //Debug.Log("Sever Alive.");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        GameObject t = hit.collider.gameObject;
        //Debug.Log("Mouse Target: " + t.name);
        if (Input.GetButtonDown("Fire2") && BackgtoundTarget.x > 10.0f)
        {
            BackgtoundTarget = BackgtoundTarget - DeltaCenter - DiffCenter;
            CameraTarget = CameraTarget - DeltaCenter;
        }
        if (t.tag == "MainMenuButton")
        {
            // TODO: some feed back when mouse floating on button
            Debug.Log(t.name);
            if (Input.GetButtonDown("Fire1"))
            {
                
                switch(t.name)
                {
                case "M1_DoublePlayer":
                    {
                        developer.SetActive(true);
                        break;
                    }
				case "M1_Exit":
					{    
						Application.Quit ();
						break;
					}
				case "M2_NewGame":
                        
					{
						choosePlayer.SetActive (true);
						break;
					}
                        
				case "M2_LoadGame":
					{

						//LoadArchive (3);

						//Debug.Log ("LoadGame");
						//SaveArchive (6);
						//Debug.Log(GetSaveTime (0));
						//Debug.Log(GetGameLevel (0));
						//Debug.Log(GetPlayerLevel (0));
						//Debug.Log(GetPlayerGold (0));
						//Debug.Log(GetSaveTime (0));
      //                  Debug.Log(GetPlayerType(0));
                            Transform _t;

                            if(GetSaveTime(0)==" ")
                            {
                                loadgame.transform.Find("Save0").Find("Button0").gameObject.SetActive(false);
                                loadgame.transform.Find("Save0").Find("Button1").gameObject.SetActive(false);
                                loadgame.transform.Find("Save0").Find("Empty").gameObject.SetActive(true);
                            }
                            else
                            {
                                loadgame.transform.Find("Save0").Find("Empty").gameObject.SetActive(false);
                                if (GetPlayerType(0) == "warrior")
                                {
                                    _t = loadgame.transform.Find("Save0").Find("Button0");
                                    loadgame.transform.Find("Save0").Find("Button0").gameObject.SetActive(true);
                                    loadgame.transform.Find("Save0").Find("Button1").gameObject.SetActive(false);
                                }
                                else
                                {
                                    _t = loadgame.transform.Find("Save0").Find("Button1");
                                    loadgame.transform.Find("Save0").Find("Button0").gameObject.SetActive(false);
                                    loadgame.transform.Find("Save0").Find("Button1").gameObject.SetActive(true);
                                }

                                _t.Find("Text").Find("GameLevel").gameObject.GetComponent<Text>().text = GetGameLevel(0).ToString();
                                _t.Find("Text").Find("PlayerLevel").gameObject.GetComponent<Text>().text = GetPlayerLevel(0).ToString();
                                _t.Find("Text").Find("gold").gameObject.GetComponent<Text>().text = GetPlayerGold(0).ToString();
                                _t.Find("Text").Find("time").gameObject.GetComponent<Text>().text = GetSaveTime(0).ToString();
                            }

                            if (GetSaveTime(1) == " ")
                            {
                                loadgame.transform.Find("Save1").Find("Button0").gameObject.SetActive(false);
                                loadgame.transform.Find("Save1").Find("Button1").gameObject.SetActive(false);
                                loadgame.transform.Find("Save1").Find("Empty").gameObject.SetActive(true);
                            }
                            else
                            {
                                loadgame.transform.Find("Save1").Find("Empty").gameObject.SetActive(false);
                                if (GetPlayerType(1) == "warrior")
                                {
                                    _t = loadgame.transform.Find("Save1").Find("Button0");
                                    loadgame.transform.Find("Save1").Find("Button0").gameObject.SetActive(true);
                                    loadgame.transform.Find("Save1").Find("Button1").gameObject.SetActive(false);
                                }
                                else
                                {
                                    _t = loadgame.transform.Find("Save1").Find("Button1");
                                    loadgame.transform.Find("Save1").Find("Button0").gameObject.SetActive(false);
                                    loadgame.transform.Find("Save1").Find("Button1").gameObject.SetActive(true);
                                }

                                _t.Find("Text").Find("GameLevel").gameObject.GetComponent<Text>().text = GetGameLevel(1).ToString();
                                _t.Find("Text").Find("PlayerLevel").gameObject.GetComponent<Text>().text = GetPlayerLevel(1).ToString();
                                _t.Find("Text").Find("gold").gameObject.GetComponent<Text>().text = GetPlayerGold(1).ToString();
                                _t.Find("Text").Find("time").gameObject.GetComponent<Text>().text = GetSaveTime(1).ToString();
                            }

                            if (GetSaveTime(2) == " ")
                            {
                                loadgame.transform.Find("Save2").Find("Button0").gameObject.SetActive(false);
                                loadgame.transform.Find("Save2").Find("Button1").gameObject.SetActive(false);
                                loadgame.transform.Find("Save2").Find("Empty").gameObject.SetActive(true);
                            }
                            else
                            {
                                loadgame.transform.Find("Save2").Find("Empty").gameObject.SetActive(false);
                                if (GetPlayerType(2) == "warrior")
                                {
                                    _t = loadgame.transform.Find("Save2").Find("Button0");
                                    loadgame.transform.Find("Save2").Find("Button0").gameObject.SetActive(true);
                                    loadgame.transform.Find("Save2").Find("Button1").gameObject.SetActive(false);
                                }
                                else
                                {
                                    _t = loadgame.transform.Find("Save2").Find("Button1");
                                    loadgame.transform.Find("Save2").Find("Button0").gameObject.SetActive(false);
                                    loadgame.transform.Find("Save2").Find("Button1").gameObject.SetActive(true);
                                }

                                _t.Find("Text").Find("GameLevel").gameObject.GetComponent<Text>().text = GetGameLevel(2).ToString();
                                _t.Find("Text").Find("PlayerLevel").gameObject.GetComponent<Text>().text = GetPlayerLevel(2).ToString();
                                _t.Find("Text").Find("gold").gameObject.GetComponent<Text>().text = GetPlayerGold(2).ToString();
                                _t.Find("Text").Find("time").gameObject.GetComponent<Text>().text = GetSaveTime(2).ToString();
                            }


                            loadgame.SetActive(true);
                        break;
					}
					
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
