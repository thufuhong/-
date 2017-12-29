using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lottery1 : MonoBehaviour {

    public float timer = -1f;
    public float timer_max = 5f;
    public float WaitingTime = 0.3f;
    public int itemid = 0;
    public int ItemLevel = 0;
    public float speed = 10;


    public GameObject[] item;

    private Vector3 i1t;
    private GameObject _t;
    private bool state = false;
    public bool flag = true;

    public void ClearChild()
    {
        Destroy(_t);
    }

    public void BlingBling()
    {
        if (timer < 0)
            timer = timer_max;
    }

    public void SetFlag()
    {
        flag = true;
    }

    // Use this for initialization
    void Start()
    {
        i1t = this.gameObject.transform.position + new Vector3(-450f, 60f, 0);
        //timer = timer_max;
        //GameObject t = Instantiate(item[itemid[0]], i1t, transform.rotation, transform);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timer > 0)
        {

            if (flag)
            {
                flag = false;
                state = false;
                //transform.Find("Button_ok").gameObject.SetActive(false);
            }


            if (timer < timer_max - WaitingTime)
            {
                if (state == false)
                {
                    _t = Instantiate(item[itemid], transform.position, transform.rotation, transform);
                    if (itemid >= 3)
                    {
                        _t.transform.Find("value").gameObject.GetComponent<Text>().text = "+" + (ItemLevel == 0 ? "15" : ItemLevel == 1 ? "9" : ItemLevel == 2 ? "6" : "3");
                    }
                    _t.transform.Find("GreatLight").localScale = ItemLevel == 0 ? new Vector3(0.5f, 0.5f, 1f) : ItemLevel == 1 ? new Vector3(0.4f, 0.4f, 1f) : ItemLevel == 2 ? new Vector3(0.3f, 0.3f, 1f) : new Vector3(0.2f, 0.2f, 1f);
                    RawImage[] _list = _t.transform.Find("GreatLight").gameObject.GetComponentsInChildren<RawImage>();
                    for (int j = 0; j < _list.Length; j++)
                    {
                        _list[j].color = ItemLevel == 0 ? Color.red : ItemLevel == 1 ? Color.blue : ItemLevel == 2 ? Color.green : Color.white;
                    }
                    state = true;
                }
            }
            timer -= Time.fixedDeltaTime;
            if (timer < 0)
            {
                transform.Find("Button_ok").gameObject.SetActive(true);
                //things to do when finished
            }
        }
    }
}
