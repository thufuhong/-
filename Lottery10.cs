using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lottery10 : MonoBehaviour {
    public float timer = -1f;
    public float timer_max = 5f;
    public float WaitingTime = 0.3f;
    public int[] itemid = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public int[] ItemLevel = { 0, 1, 2, 3, 0, 0, 0, 0, 0, 0 };
    public float speed = 10;


    public GameObject[] item;

    private Vector3 i1t;
    private GameObject[] _t = new GameObject[10];
    private bool[] state = { false, false, false, false, false, false, false, false, false, false};
    public bool flag = true;

    public void ClearChild()
    {
        for (int i = 0; i < 10; i++)
            Destroy(_t[i]);  
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
    void Start () {
        i1t = this.gameObject.transform.position + new Vector3(-450f, 60f, 0);
        //timer = timer_max;
        //GameObject t = Instantiate(item[itemid[0]], i1t, transform.rotation, transform);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if(timer>0)
        {

            if(flag)
            {
                flag = false;
                for (int i = 0; i < 10; i++)
                    state[i] = false;
                //transform.Find("Button_ok").gameObject.SetActive(false);
            }
            for(int i=0;i<10;i++)
            {
                if (timer < timer_max - i*WaitingTime)
                {
                    if (state[i] == false)
                    {
                        _t[i] = Instantiate(item[itemid[i]], transform.position, transform.rotation, transform);
                        if(itemid[i]>=3)
                        {
                            _t[i].transform.Find("value").gameObject.GetComponent<Text>().text = "+" + (ItemLevel[i] == 0 ? "15" : ItemLevel[i] == 1 ? "9" : ItemLevel[i] == 2 ? "6" : "3");
                        }
                        _t[i].transform.Find("GreatLight").localScale = ItemLevel[i] == 0 ? new Vector3(0.5f,0.5f,1f): ItemLevel[i] == 1 ? new Vector3(0.4f, 0.4f, 1f) : ItemLevel[i] == 2 ? new Vector3(0.3f, 0.3f, 1f) : new Vector3(0.2f, 0.2f, 1f);
                        RawImage[] _list = _t[i].transform.Find("GreatLight").gameObject.GetComponentsInChildren<RawImage>();
                        for (int j = 0; j < _list.Length; j++)
                        {
                            _list[j].color = ItemLevel[i] == 0 ? Color.red : ItemLevel[i] == 1 ? Color.blue : ItemLevel[i] == 2 ? Color.green : Color.white;
                            
                        }
                        state[i] = true;
                    }
                    _t[i].transform.position = Vector3.Lerp(_t[i].transform.position, transform.position + new Vector3(-200f + 100f * (i%5), i < 5 ? 60f : -60f, 0), speed * Time.fixedDeltaTime);   
                }
            }
            timer -= Time.fixedDeltaTime;
            if (timer<0)
            {
                transform.Find("Button_ok").gameObject.SetActive(true);
                //things to do when finished
            }
        }
	}
}
