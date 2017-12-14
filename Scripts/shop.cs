using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shop : MonoBehaviour {
    public GameObject player;
    public GameObject ShopObject;
    public Text mainHelper;
	public attribute player_attribute;

    public bool isPlayerNearBy = false;
    private bool shopOpened = false;
	private bool next_level = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        if (!isPlayerNearBy && (this.gameObject.transform.position - player.transform.position).sqrMagnitude < 500)
        {
            isPlayerNearBy = true;
            mainHelper.text = "按 E 来显示商店。\n按 N 来进入下一关卡。";
            mainHelper.gameObject.transform.parent.gameObject.SetActive(true);
        }
        if (isPlayerNearBy)
        {
            if (shopOpened && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape)))
            {
                ShopObject.SetActive(false);
                shopOpened = false;
                return;
            }
            if (!shopOpened && Input.GetKeyDown(KeyCode.E))
            {
                ShopObject.SetActive(true);
                ShopObject.GetComponent<bagManager>().RefreshBag();
                shopOpened = true;
                //Put SHOP code here
            }

			if (Input.GetKeyDown (KeyCode.N) && !next_level)
			{
				//next level
				next_level = true;
				Debug.Log(player_attribute.level_num);
				player_attribute.level_num++;
				Debug.Log(player_attribute.level_num);
				player_attribute.SavePlayerAttribute ();
				player_attribute.SaveAttributeInFile ();
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
