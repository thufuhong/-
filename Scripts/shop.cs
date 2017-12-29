using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shop : MonoBehaviour {
    public GameObject player;
    public GameObject ShopObject;
    public GameObject SaveObject;
    public Text mainHelper;
	public attribute player_attribute;

    public bool isPlayerNearBy = false;
    private bool shopOpened = false;
    private bool SaveOpened = false;
    private bool next_level = false;

    private float button_cooldown = -1;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        if (button_cooldown > 0)
            button_cooldown -= Time.fixedDeltaTime;
        if (!isPlayerNearBy && (this.gameObject.transform.position - player.transform.position).sqrMagnitude < 500)
        {
            isPlayerNearBy = true;
            mainHelper.text = "按 E 来显示商店。\n按 N 来进入下一关卡。\n按 R 来手动保存游戏。";
            mainHelper.gameObject.transform.parent.gameObject.SetActive(true);
        }
        if (isPlayerNearBy)
        {
            //if (shopOpened && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape)))
            //{
            //    ShopObject.SetActive(false);
            //    shopOpened = false;
            //    return;
            //}
            //if (!shopOpened && Input.GetKeyDown(KeyCode.E))
            //{
            //    ShopObject.SetActive(true);
            //    ShopObject.GetComponent<bagManager>().RefreshBag();
            //    shopOpened = true;
            //    //Put SHOP code here
            //}
            
            if (button_cooldown<0 && SaveObject.activeInHierarchy && (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Escape)))
            {
                //Debug.Log(SaveObject.activeInHierarchy.ToString() + "0" + Input.GetKeyDown(KeyCode.R).ToString());
                button_cooldown = 0.2f;
                SaveObject.SetActive(false);
                SaveOpened = false;
                return;
            }
            if (button_cooldown < 0 && !(SaveObject.activeInHierarchy) && Input.GetKeyDown(KeyCode.R))
            {
                //Debug.Log(SaveObject.activeInHierarchy.ToString() + "1" + Input.GetKeyDown(KeyCode.R).ToString());
                button_cooldown = 0.2f;
                SaveObject.SetActive(true);
                Transform _t;
                if (player.GetComponent<attribute>().GetSaveTimeFromFile(0) == " ")
                {
                    SaveObject.transform.Find("Save0").Find("Button0").gameObject.SetActive(false);
                    SaveObject.transform.Find("Save0").Find("Button1").gameObject.SetActive(false);
                    SaveObject.transform.Find("Save0").Find("Empty").gameObject.SetActive(true);
                }
                else
                {
                    SaveObject.transform.Find("Save0").Find("Empty").gameObject.SetActive(false);
                    if (player.GetComponent<attribute>().GetTypeOfPlayerFromFile(0) == "warrior")
                    {
                        _t = SaveObject.transform.Find("Save0").Find("Button0");
                        SaveObject.transform.Find("Save0").Find("Button0").gameObject.SetActive(true);
                        SaveObject.transform.Find("Save0").Find("Button1").gameObject.SetActive(false);
                    }
                    else
                    {
                        _t = SaveObject.transform.Find("Save0").Find("Button1");
                        SaveObject.transform.Find("Save0").Find("Button0").gameObject.SetActive(false);
                        SaveObject.transform.Find("Save0").Find("Button1").gameObject.SetActive(true);
                    }

                    _t.Find("Text").Find("GameLevel").gameObject.GetComponent<Text>().text = player.GetComponent<attribute>().GetLevelOfGameFromFile(0).ToString();
                    _t.Find("Text").Find("PlayerLevel").gameObject.GetComponent<Text>().text = player.GetComponent<attribute>().GetLevelOfPlayerFromFile(0).ToString();
                    _t.Find("Text").Find("gold").gameObject.GetComponent<Text>().text = player.GetComponent<attribute>().GetGoldOfPlayerFromFile(0).ToString();
                    _t.Find("Text").Find("time").gameObject.GetComponent<Text>().text = player.GetComponent<attribute>().GetSaveTimeFromFile(0).ToString();
                }

                if (player.GetComponent<attribute>().GetSaveTimeFromFile(1) == " ")
                {
                    SaveObject.transform.Find("Save1").Find("Button0").gameObject.SetActive(false);
                    SaveObject.transform.Find("Save1").Find("Button1").gameObject.SetActive(false);
                    SaveObject.transform.Find("Save1").Find("Empty").gameObject.SetActive(true);
                }
                else
                {
                    SaveObject.transform.Find("Save1").Find("Empty").gameObject.SetActive(false);
                    if (player.GetComponent<attribute>().GetTypeOfPlayerFromFile(1) == "warrior")
                    {
                        _t = SaveObject.transform.Find("Save1").Find("Button0");
                        SaveObject.transform.Find("Save1").Find("Button0").gameObject.SetActive(true);
                        SaveObject.transform.Find("Save1").Find("Button1").gameObject.SetActive(false);
                    }
                    else
                    {
                        _t = SaveObject.transform.Find("Save1").Find("Button1");
                        SaveObject.transform.Find("Save1").Find("Button0").gameObject.SetActive(false);
                        SaveObject.transform.Find("Save1").Find("Button1").gameObject.SetActive(true);
                    }

                    _t.Find("Text").Find("GameLevel").gameObject.GetComponent<Text>().text = player.GetComponent<attribute>().GetLevelOfGameFromFile(1).ToString();
                    _t.Find("Text").Find("PlayerLevel").gameObject.GetComponent<Text>().text = player.GetComponent<attribute>().GetLevelOfPlayerFromFile(1).ToString();
                    _t.Find("Text").Find("gold").gameObject.GetComponent<Text>().text = player.GetComponent<attribute>().GetGoldOfPlayerFromFile(1).ToString();
                    _t.Find("Text").Find("time").gameObject.GetComponent<Text>().text = player.GetComponent<attribute>().GetSaveTimeFromFile(1).ToString();
                }

                if (player.GetComponent<attribute>().GetSaveTimeFromFile(2) == " ")
                {
                    SaveObject.transform.Find("Save2").Find("Button0").gameObject.SetActive(false);
                    SaveObject.transform.Find("Save2").Find("Button1").gameObject.SetActive(false);
                    SaveObject.transform.Find("Save2").Find("Empty").gameObject.SetActive(true);
                }
                else
                {
                    SaveObject.transform.Find("Save2").Find("Empty").gameObject.SetActive(false);
                    if (player.GetComponent<attribute>().GetTypeOfPlayerFromFile(2) == "warrior")
                    {
                        _t = SaveObject.transform.Find("Save2").Find("Button0");
                        SaveObject.transform.Find("Save2").Find("Button0").gameObject.SetActive(true);
                        SaveObject.transform.Find("Save2").Find("Button1").gameObject.SetActive(false);
                    }
                    else
                    {
                        _t = SaveObject.transform.Find("Save2").Find("Button1");
                        SaveObject.transform.Find("Save2").Find("Button0").gameObject.SetActive(false);
                        SaveObject.transform.Find("Save2").Find("Button1").gameObject.SetActive(true);
                    }

                    _t.Find("Text").Find("GameLevel").gameObject.GetComponent<Text>().text = player.GetComponent<attribute>().GetLevelOfGameFromFile(2).ToString();
                    _t.Find("Text").Find("PlayerLevel").gameObject.GetComponent<Text>().text = player.GetComponent<attribute>().GetLevelOfPlayerFromFile(2).ToString();
                    _t.Find("Text").Find("gold").gameObject.GetComponent<Text>().text = player.GetComponent<attribute>().GetGoldOfPlayerFromFile(2).ToString();
                    _t.Find("Text").Find("time").gameObject.GetComponent<Text>().text = player.GetComponent<attribute>().GetSaveTimeFromFile(2).ToString();
                }
                SaveOpened = true;
            }

            if (Input.GetKeyDown (KeyCode.N) && !next_level)
			{
				//next level
				next_level = true;
				Debug.Log(player_attribute.level_num);
				player_attribute.level_num++;
				Debug.Log(player_attribute.level_num);
                player_attribute.SavePlayerAttribute();
                //player_attribute.SaveAttributeInFile();
                UnityEngine.SceneManagement.SceneManager.LoadScene ("LevelFirst");
			}
            

        }
        if (isPlayerNearBy && (this.gameObject.transform.position - player.transform.position).sqrMagnitude >600)
        {
            isPlayerNearBy = false;
            mainHelper.gameObject.transform.parent.gameObject.SetActive(false);
        }

    }
}
