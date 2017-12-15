using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class bagManager : MonoBehaviour {

    public Texture hp;
    public Texture atk;
    public Texture def;
    public Texture nan;
    public float[] ShopPrice = { 50, 50, 50, 100, 900 };
    public int[] ItemOwned = { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; //十二个回血/加攻/加防物品的数量
    public GameObject[] ItemGameObject;
    public attribute player;
    public GameObject LotterySingle;
    public GameObject Lottery10;
    public GameObject MainScreenItem;
    public GameObject shopPosition;

    public int _x;
    public int _y;

    public void changetexture()
    {
        this.gameObject.GetComponent<RawImage>().texture = atk;
    }
    public void RefreshBag()
    {
        // hide shop when player not nearby
        if(shopPosition.GetComponent<shop>().isPlayerNearBy)
        {
            transform.Find("ShopFake").gameObject.SetActive(false);
            transform.Find("Shop").gameObject.SetActive(true);
        }
        else
        {
            transform.Find("ShopFake").gameObject.SetActive(true);
            transform.Find("Shop").gameObject.SetActive(false);
        }
        // Refresh Shop
        transform.Find("Shop").Find("Item1").Find("Price").gameObject.GetComponent<Text>().text = ShopPrice[0].ToString();
        if(player.gold < ShopPrice[0])
        {
            transform.Find("Shop").Find("Item1").Find("Price").gameObject.GetComponent<Text>().color = Color.red;
            transform.Find("Shop").Find("Item1").Find("Button").gameObject.GetComponent<Button>().interactable = false;
        }
        else
        {
            transform.Find("Shop").Find("Item1").Find("Price").gameObject.GetComponent<Text>().color = Color.black;
            transform.Find("Shop").Find("Item1").Find("Button").gameObject.GetComponent<Button>().interactable = true;
        }
        transform.Find("Shop").Find("Item2").Find("Price").gameObject.GetComponent<Text>().text = ShopPrice[1].ToString();
        if (player.gold < ShopPrice[1])
        {
            transform.Find("Shop").Find("Item2").Find("Price").gameObject.GetComponent<Text>().color = Color.red;
            transform.Find("Shop").Find("Item2").Find("Button").gameObject.GetComponent<Button>().interactable = false;
        }
        else
        {
            transform.Find("Shop").Find("Item2").Find("Price").gameObject.GetComponent<Text>().color = Color.black;
            transform.Find("Shop").Find("Item2").Find("Button").gameObject.GetComponent<Button>().interactable = true;
        }
        transform.Find("Shop").Find("Item3").Find("Price").gameObject.GetComponent<Text>().text = ShopPrice[2].ToString();
        if (player.gold < ShopPrice[2])
        {
            transform.Find("Shop").Find("Item3").Find("Price").gameObject.GetComponent<Text>().color = Color.red;
            transform.Find("Shop").Find("Item3").Find("Button").gameObject.GetComponent<Button>().interactable = false;
        }
        else
        {
            transform.Find("Shop").Find("Item3").Find("Price").gameObject.GetComponent<Text>().color = Color.black;
            transform.Find("Shop").Find("Item3").Find("Button").gameObject.GetComponent<Button>().interactable = true;
        }
        transform.Find("Shop").Find("1lian").Find("Price").gameObject.GetComponent<Text>().text = ShopPrice[3].ToString();
        if (player.gold < ShopPrice[3])
        {
            transform.Find("Shop").Find("1lian").Find("Price").gameObject.GetComponent<Text>().color = Color.red;
            transform.Find("Shop").Find("1lian").gameObject.GetComponent<Button>().interactable = false;
        }
        else
        {
            transform.Find("Shop").Find("1lian").Find("Price").gameObject.GetComponent<Text>().color = Color.black;
            transform.Find("Shop").Find("1lian").gameObject.GetComponent<Button>().interactable = true;
        }
        transform.Find("Shop").Find("10lian").Find("Price").gameObject.GetComponent<Text>().text = ShopPrice[4].ToString();
        if (player.gold < ShopPrice[4])
        {
            transform.Find("Shop").Find("10lian").Find("Price").gameObject.GetComponent<Text>().color = Color.red;
            transform.Find("Shop").Find("10lian").gameObject.GetComponent<Button>().interactable = false;
        }
        else
        {
            transform.Find("Shop").Find("10lian").Find("Price").gameObject.GetComponent<Text>().color = Color.black;
            transform.Find("Shop").Find("10lian").gameObject.GetComponent<Button>().interactable = true;
        }
        // Refresh Bag
        for (int i =0;i<12;i++)
        {
            if(ItemOwned[i] == 0)
            {
                ItemGameObject[i].transform.Find("Button").gameObject.GetComponent<Image>().color = Color.black - new Color(0, 0, 0, 0.5f);
                ItemGameObject[i].transform.Find("Button").gameObject.GetComponent<Button>().interactable = false;
                ItemGameObject[i].transform.Find("Number").gameObject.GetComponent<Text>().text = ItemOwned[i].ToString();
            }
            else
            {
                ItemGameObject[i].transform.Find("Button").gameObject.GetComponent<Image>().color = Color.white;
                ItemGameObject[i].transform.Find("Button").gameObject.GetComponent<Button>().interactable = true;
                ItemGameObject[i].transform.Find("Number").gameObject.GetComponent<Text>().text = ItemOwned[i].ToString();
            }
        }
        transform.Find("Bag").Find("goldOwned").Find("Text2").gameObject.GetComponent<Text>().text = player.gold.ToString();
    }
    public void purchase(int x)
    {
        //x = 0/1/2 ... purchase hp/atk/def
        if (ShopPrice[x] > player.gold)
            return;
        else
        {
            player.gold -= ShopPrice[x];
            ItemOwned[4 * x]++;
        }
        RefreshBag();
    }

    public void lottery(int x)
    {
        if(x==1)
        {
            if (ShopPrice[3] > player.gold)
                return;
            else
            {
                player.gold -= ShopPrice[3];
                float _rand1 = Random.Range(0f, 1f);
                float _rand2 = Random.Range(0f, 1f);
                int _kind1 = _rand1 < 0.28f ? 0 : _rand1 < 0.49f ? 1 : _rand1 < 0.7f ? 2 : _rand1 < 0.85f ? 3 : 4; //Probality of hp/atk/def/Attribute_atk/Attribute_def ... 0.28/0.21/0.21/0.15/0.15
                int _kind2 = _rand2 < 0.5f ? 0 : _rand2 < 0.8f ? 1 : _rand2 < 0.95f ? 2 : 3; //Probality of white/green/blue/red ... 0.5/0.3/0.15/0.05
                if (_kind1<3)
                    ItemOwned[4 * _kind1 + _kind2]++;
                else
                {
                    if (_kind1 == 3)
                        player.ATK += _kind2 == 0 ? 3 : _kind2 == 1 ? 6 : _kind2 == 2 ? 9 : 15;
                    if (_kind1 == 4)
                        player.DEF += _kind2 == 0 ? 3 : _kind2 == 1 ? 6 : _kind2 == 2 ? 9 : 15;
                }
                LotterySingle.GetComponent<Lottery1>().itemid = _kind1;
                LotterySingle.GetComponent<Lottery1>().ItemLevel = 3 - _kind2;
                LotterySingle.SetActive(true);
                LotterySingle.GetComponent<Lottery1>().BlingBling();
            }
        }
        if (x == 10)
        {
            if (ShopPrice[4] > player.gold)
                return;
            else
            {
                player.gold -= ShopPrice[4];

                float _rand1 = Random.Range(0f, 1f);
                float _rand2 = 1.0f; //十连必爆红色品质
                int _kind1 = _rand1 < 0.28f ? 0 : _rand1 < 0.49f ? 1 : _rand1 < 0.7f ? 2 : _rand1 < 0.85f ? 3 : 4; //Probality of hp/atk/def/Attribute_atk/Attribute_def ... 0.28/0.21/0.21/0.15/0.15
                int _kind2 = _rand2 < 0.5f ? 0 : _rand2 < 0.8f ? 1 : _rand2 < 0.95f ? 2 : 3; //Probality of white/green/blue/red ... 0.5/0.3/0.15/0.05
                if (_kind1 < 3)
                    ItemOwned[4 * _kind1 + _kind2]++;
                else
                {
                    if (_kind1 == 3)
                        player.ATK += _kind2 == 0 ? 3 : _kind2 == 1 ? 6 : _kind2 == 2 ? 9 : 15;
                    if (_kind1 == 4)
                        player.DEF += _kind2 == 0 ? 3 : _kind2 == 1 ? 6 : _kind2 == 2 ? 9 : 15;
                }
                Lottery10.GetComponent<Lottery10>().itemid[0] = _kind1;
                Lottery10.GetComponent<Lottery10>().ItemLevel[0] = 3 - _kind2;

                for(int i=1;i<10;i++)
                {
                    _rand1 = Random.Range(0f, 1f);
                    _rand2 = Random.Range(0f, 1f);
                    _kind1 = _rand1 < 0.28f ? 0 : _rand1 < 0.49f ? 1 : _rand1 < 0.7f ? 2 : _rand1 < 0.85f ? 3 : 4; //Probality of hp/atk/def/Attribute_atk/Attribute_def ... 0.28/0.21/0.21/0.15/0.15
                    _kind2 = _rand2 < 0.5f ? 0 : _rand2 < 0.8f ? 1 : _rand2 < 0.95f ? 2 : 3; //Probality of white/green/blue/red ... 0.5/0.3/0.15/0.05
                    if (_kind1 < 3)
                        ItemOwned[4 * _kind1 + _kind2]++;
                    else
                    {
                        if (_kind1 == 3)
                            player.ATK += _kind2 == 0 ? 3 : _kind2 == 1 ? 6 : _kind2 == 2 ? 9 : 15;
                        if (_kind1 == 4)
                            player.DEF += _kind2 == 0 ? 3 : _kind2 == 1 ? 6 : _kind2 == 2 ? 9 : 15;
                    }
                    Lottery10.GetComponent<Lottery10>().itemid[i] = _kind1;
                    Lottery10.GetComponent<Lottery10>().ItemLevel[i] = 3 - _kind2;
                }

                Lottery10.SetActive(true);
                Lottery10.GetComponent<Lottery10>().BlingBling();

            }
        }
        RefreshBag();
    }

    public void ChangeChoose(int x)
    {
        ItemGameObject[4 * _x + _y].transform.Find("Choosen").gameObject.SetActive(false);
        _x = x/4;
        _y = x%4;
        ItemGameObject[4 * _x + _y].transform.Find("Choosen").gameObject.SetActive(true);
        Texture _t = _x == 0 ? hp : _x == 1 ? atk : _x == 2 ? def : nan;
        MainScreenItem.transform.Find("Icon").gameObject.GetComponent<RawImage>().texture = _t;
        MainScreenItem.transform.Find("Number").gameObject.GetComponent<Text>().text = ItemOwned[4 * _x + _y].ToString();
    }

    public void UseItem()
    {
        if (MainScreenItem.transform.Find("Icon").gameObject.GetComponent<RawImage>().texture.Equals(nan))
            return;
        if (_x==0)
        {
            float _percentage = _y == 0 ? 0.2f : _y == 1 ? 0.4f : _y == 2 ? 0.6f : 1.0f;
            player.update_HP(player.HP_max * _percentage);
            player.gameObject.transform.Find("HPup").gameObject.SetActive(true);
            ItemOwned[4 * _x + _y]--;
            MainScreenItem.transform.Find("Number").gameObject.GetComponent<Text>().text = ItemOwned[4 * _x + _y].ToString();
            if (ItemOwned[4 * _x + _y] == 0)
            {
                MainScreenItem.transform.Find("Icon").gameObject.GetComponent<RawImage>().texture = nan;

            }
        }
    }

	// Use this for initialization
	void Start () {
        RefreshBag();
    }
	
	// Update is called once per frame
	void Update () {
        //RefreshBag();

    }
}
