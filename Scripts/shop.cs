using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shop : MonoBehaviour {
    public GameObject player;
    public GameObject ShopObject;
    public Text mainHelper;

    private bool isPlayerNearBy = false;
    private bool shopOpened = false;

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
            mainHelper.text = "按 E 来显示商店。";
            mainHelper.gameObject.transform.parent.gameObject.SetActive(true);
        }
        if (isPlayerNearBy)
        {
            if(!shopOpened && Input.GetKeyDown(KeyCode.E))
            {
                ShopObject.SetActive(true);
                //Put SHOP code here
            }
            if (shopOpened && (Input.GetKeyDown(KeyCode.E)|| Input.GetKeyDown(KeyCode.Escape)))
            {
                ShopObject.SetActive(false);
            }
        }
        if (isPlayerNearBy && (this.gameObject.transform.position - player.transform.position).sqrMagnitude >600)
        {
            isPlayerNearBy = false;
            mainHelper.gameObject.transform.parent.gameObject.SetActive(false);
        }

    }
}
