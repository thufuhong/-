using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.UI;
using System.IO;
using System.Text;
using System.Security.Cryptography;

public class attribute : MonoBehaviour 
{
    public GameObject SkillUp_button;
    public GameObject Skill_1_Icon;
    public GameObject Skill_2_Icon;
    public GameObject Skill_3_Icon;
    public GameObject Skill_4_Icon;
    public float HP_max = 100f;
    public float BallisticSpeed = 10f;
    public float BallisticDamage = -34.0f;
    public float FireRate = 1.0f;
    public float MP_max = 100f;
    public float EXP = 0;
    public float Level = 1;
    public float ATKGrowth = 10f;
    public float DEFGrowth = 5f;
    public float HPGrowth = 20f;
    public float MPGrowth = 10;
    public float EXPForLevelUp = 1000f;
    public float gold = 0;
    public float DropGold = 100f;
    public float DropEXP = 600f;
    public float[] Skill_Level = { 0, 0, 0, 0 };
    public string NiCheng;
    public string ZhiYe;

    public float team = 0;
    public bool ifAlive = true;
    public Slider ui_HP;
    public Slider ui_EXP;
    public bool if_Player = false;
    public GameObject DieWindow;
    public GameObject ParticalLevelUp;
    public GameObject bag;
    public GameObject safe_area_sever;

    public float HP;
    public float MP;
    public float ATK = 20;
    public float ATK_bouns = 0;
    public float DEF = 10;
    public float DEF_bouns = 0;
    public float ForceBackPower = 1f;
    public float ForceBackCounterMax = 10;
    public float ForceBackCounter = -1;
    public Vector3 ForceBackDerection;

    public int skillUp_Num;


	public int level_num = 1;
	public string save_time;
	public bool IsInit = false;
    

    public float update_HP(float value)
    {
        if (ifAlive)
        {
			if (HP <= 0)
			{
				ifAlive = false;
				return HP;
			}
            HP += value;
            if(if_Player && ui_HP != null)
                ui_HP.value = Mathf.Max(HP, 0) / HP_max;
            if (HP >= HP_max)
                HP = HP_max;
            if (HP <= 0)
            {
                try
                {
                    Animator anim = GetComponent<Animator>();
                    anim.SetTrigger("death");
                    anim.ResetTrigger("stun");
                }
                catch { }
                try
                {
                    this.gameObject.transform.Find("Died Particle").gameObject.SetActive(true);
                }
                catch { }
                ifAlive = false;
                if(this.gameObject.tag == "Team1")
                    Destroy(this.gameObject, 1.5f);
				if (this.gameObject.tag == "Team0" && DieWindow != null)
                    DieWindow.SetActive(true);
                // other event when death....
            }
            //Debug.Log(this.name + "recieve" + value.ToString() + ", remain" + HP.ToString());
        }
        return HP;
    }

    public void update_EXP(float value)
    {
        if(ifAlive)
        {
            EXP += value;
			EXP = Mathf.Max (0, EXP);
        }
        while(EXP>= EXPForLevelUp)
        {
            EXP -= EXPForLevelUp;
            Level++;
            skillUp_Num++;
            Debug.Log("SkillUpNum: " + skillUp_Num.ToString());
			if(SkillUp_button != null)
				SkillUp_button.transform.Find("Skill_Up_Num").gameObject.GetComponent<Text>().text = skillUp_Num.ToString();
            ATK += ATKGrowth;
            DEF += DEFGrowth;
            HP_max += HPGrowth;
            MP_max += MPGrowth;
			if (ParticalLevelUp != null) ParticalLevelUp.SetActive(true);
			if (SkillUp_button != null) SkillUp_button.SetActive(true);
            
        }

		if (if_Player && ui_EXP!=null)
            ui_EXP.value = Mathf.Max(EXP, 0) / EXPForLevelUp;

    }

	// interface about goods droped when be killed
	public GameObject dropGoods(string type) {
		if (type == "coin") {
			GameObject coin = Instantiate (
				/*transform.Find ("/Terrain").GetComponent<GameObjectGenerate> ().itemCoin,*/
				Resources.Load("Prefabs/goldCoin") ,
				transform.position + new Vector3(0, (float) (GetComponent<CapsuleCollider>().bounds.size.y * 0.5) * 0 + 0.8f, 0),
				Quaternion.Euler (-90, 0, 0)
			) as GameObject;
			coin.GetComponent<pickGoods> ().value = DropGold;
			coin.GetComponent<pickGoods> ().type = "GOLD+";
			return coin;
			// Debug.Log ("coin" + coin.GetComponent<pickGoods> ().value + " cur:" + DropGold);
		}
		return null;
	}

	public void update_gold(float value) {
		if (ifAlive) {
			gold += value;
			gold = Mathf.Max (0, gold);
		}
	}

    public void Update_Skill(int SkillNum)
    {
        Skill_Level[SkillNum] += 1;
        skillUp_Num--;
        if (skillUp_Num < 0) skillUp_Num = 0;
        SkillUp_button.transform.Find("Skill_Up_Num").gameObject.GetComponent<Text>().text = skillUp_Num.ToString();
        if (SkillNum == 0 && Skill_Level[SkillNum] == 1)
            Skill_1_Icon.GetComponent<Image>().fillAmount = 0;
        if (SkillNum == 1 && Skill_Level[SkillNum] == 1)
            Skill_2_Icon.GetComponent<Image>().fillAmount = 0;
        if (SkillNum == 2 && Skill_Level[SkillNum] == 1)
            Skill_3_Icon.GetComponent<Image>().fillAmount = 0;
        if (SkillNum == 3 && Skill_Level[SkillNum] == 1)
            Skill_4_Icon.GetComponent<Image>().fillAmount = 0;
        if (skillUp_Num <= 0)
            SkillUp_button.SetActive(false);
        
    } 
    
    // Use this for initialization
    void Start () {
        HP = HP_max;
        MP = MP_max;
        BallisticDamage = ATK;
        
        if (Level == 1 && if_Player)
            skillUp_Num++;
        if (skillUp_Num > 0)
        {
			if (SkillUp_button != null) {
				SkillUp_button.transform.Find ("Skill_Up_Num").gameObject.GetComponent<Text> ().text = skillUp_Num.ToString ();
				SkillUp_button.SetActive (true);
			}
        }
            

    }
	
	// Update is called once per frame
	void FixedUpdate () {
        try
        {
            if (Skill_Level[0] == 0)
                Skill_1_Icon.GetComponent<Image>().fillAmount = 1;
            if (Skill_Level[1] == 0)
                Skill_2_Icon.GetComponent<Image>().fillAmount = 1;
            if (Skill_Level[2] == 0)
                Skill_3_Icon.GetComponent<Image>().fillAmount = 1;
            if (Skill_Level[3] == 0)
                Skill_4_Icon.GetComponent<Image>().fillAmount = 1;
        }
        catch { }

        if (ForceBackCounter>=0)
        {
            this.gameObject.transform.position = this.gameObject.transform.position +
                ForceBackPower * ForceBackDerection.normalized / Mathf.Pow(2, (int)(1 + ForceBackCounterMax - ForceBackCounter));
            ForceBackCounter = ForceBackCounter - 1;
        }
		
	}

	public void SavePlayerAttribute()
	{
		PlayerPrefs.SetFloat ("HP_max", HP_max);
		PlayerPrefs.SetFloat ("BallisticSpeed", BallisticSpeed);
		PlayerPrefs.SetFloat ("BallisticDamage", BallisticDamage);
		PlayerPrefs.SetFloat ("FireRate", FireRate);
		PlayerPrefs.SetFloat ("MP_max", MP_max);
		PlayerPrefs.SetFloat ("EXP", EXP);
		PlayerPrefs.SetFloat ("Level", Level);
		PlayerPrefs.SetFloat ("ATKGrowth", ATKGrowth);
		PlayerPrefs.SetFloat ("DEFGrowth", DEFGrowth);
		PlayerPrefs.SetFloat ("HPGrowth", HPGrowth);
		PlayerPrefs.SetFloat ("MPGrowth", MPGrowth);
		PlayerPrefs.SetFloat ("EXPForLevelUp", EXPForLevelUp);
		PlayerPrefs.SetFloat ("gold", gold);
		PlayerPrefs.SetFloat ("DropGold", DropGold);
		PlayerPrefs.SetFloat ("team", team);
		//PlayerPrefs.SetFloat ("ifAlive", ifAlive);
		//PlayerPrefs.SetFloat ("if_Player", if_Player);
		PlayerPrefs.SetFloat ("HP", HP);
		PlayerPrefs.SetFloat ("MP", MP);
		PlayerPrefs.SetFloat ("ATK", ATK);
		PlayerPrefs.SetFloat ("DEF", DEF);
		PlayerPrefs.SetFloat ("ForceBackPower", ForceBackPower);
		PlayerPrefs.SetFloat ("ForceBackCounterMax", ForceBackCounterMax);
		PlayerPrefs.SetFloat ("ForceBackCounter", ForceBackCounter);
        

		PlayerPrefs.SetInt ("skillUp_Num", skillUp_Num);
		PlayerPrefs.SetFloat ("Skill_Level0", Skill_Level[0]);
		PlayerPrefs.SetFloat ("Skill_Level1", Skill_Level[1]);
		PlayerPrefs.SetFloat ("Skill_Level2", Skill_Level[2]);
		PlayerPrefs.SetFloat ("Skill_Level3", Skill_Level[3]);

        for(int _i=0;_i<12;_i++)
        {
            PlayerPrefs.SetInt("Item_num"+_i.ToString(), bag.GetComponent<bagManager>().ItemOwned[_i]);
        }
        


		PlayerPrefs.SetInt ("level_num", level_num);

		if(IsInit)
			PlayerPrefs.SetInt ("IsInit", 1);
		else
			PlayerPrefs.SetInt ("IsInit", 0);

	}

	public void ReadPlayerAttribute()
	{
		HP_max = PlayerPrefs.GetFloat ("HP_max", 100);
		BallisticSpeed = PlayerPrefs.GetFloat ("BallisticSpeed", 10);
		BallisticDamage = PlayerPrefs.GetFloat ("BallisticDamage", -34);
		FireRate = PlayerPrefs.GetFloat ("FireRate", 1);
		MP_max = PlayerPrefs.GetFloat ("MP_max", 100);
		EXP = PlayerPrefs.GetFloat ("EXP", 0);
		Level = PlayerPrefs.GetFloat ("Level", 1);
		ATKGrowth = PlayerPrefs.GetFloat ("ATKGrowth", 10);
		DEFGrowth = PlayerPrefs.GetFloat ("DEFGrowth", 5);
		HPGrowth = PlayerPrefs.GetFloat ("HPGrowth", 20);
		MPGrowth = PlayerPrefs.GetFloat ("MPGrowth", 10);
		EXPForLevelUp = PlayerPrefs.GetFloat ("EXPForLevelUp", 1000);
		gold = PlayerPrefs.GetFloat ("gold", 0);
		DropGold = PlayerPrefs.GetFloat ("DropGold", 100);
		team = PlayerPrefs.GetFloat ("team", 0);


		ifAlive = true;
		if_Player = true;
		ForceBackDerection = new Vector3 (0, 0, 0);
		//ifAlive = PlayerPrefs.GetFloat ("ifAlive", 100);
		//if_Player = PlayerPrefs.GetFloat ("if_Player", 100);

		HP = PlayerPrefs.GetFloat ("HP", 100);
		MP = PlayerPrefs.GetFloat ("MP", 100);
		ATK = PlayerPrefs.GetFloat ("ATK", 90);
		DEF = PlayerPrefs.GetFloat ("DEF", 10);
		ForceBackPower = PlayerPrefs.GetFloat ("ForceBackPower", 1);
		ForceBackCounterMax = PlayerPrefs.GetFloat ("ForceBackCounterMax", 10);
		ForceBackCounter = PlayerPrefs.GetFloat ("ForceBackCounter", -1);

		skillUp_Num = PlayerPrefs.GetInt ("skillUp_Num", 0);
		Skill_Level[0] = PlayerPrefs.GetFloat ("Skill_Level0", 0);
		Skill_Level[1] = PlayerPrefs.GetFloat ("Skill_Level1", 0);
		Skill_Level[2] = PlayerPrefs.GetFloat ("Skill_Level2", 0);
		Skill_Level[3] = PlayerPrefs.GetFloat ("Skill_Level3", 0);

        for (int _i = 0; _i < 12; _i++)
        {
            bag.GetComponent<bagManager>().ItemOwned[_i] = PlayerPrefs.GetInt("Item_num" + _i.ToString(), 0);
        }

        level_num = PlayerPrefs.GetInt ("level_num", 1);

		if (PlayerPrefs.GetInt ("IsInit", 1) == 1)
			IsInit = true;
		else
			IsInit = false;

		//ui_EXP.value = Mathf.Max(EXP, 0) / EXPForLevelUp;
		//ui_HP.value = Mathf.Max(HP, 0) / HP_max;
	}

    // 玩家每回合大致获得80点总属性，怪物每回合获得90点属性
	public void EnemyUpdate()
	{
		HP = HP_max = 60f * Level + 50f;
		ATK = 55f * Level - 10f;
		DEF = 35f * Level - 10f;
		DropGold = 30f * Level + 10f;
        //DropEXP = 200f * Level + 200f;
        DropEXP = 400f;
        FireRate = 0.5f / Level + 0.5f;
	}

	public void BossUpdate()
	{
		HP = HP_max = 1000f * Level + 200f;
		ATK = 55f * Level + 5f;
		DEF = 35f * Level - 5f;
		DropGold = 300f * Level + 10f;
        //DropEXP = 600f * Level + 100f;
        DropEXP = 1000f;

        safe_area_sever.GetComponent<Safe_area_sever>().DamagePerSecond = 4f*Level - 2f;
	}

	private void PrintAttribute()
	{
		//just for debug
		Debug.Log ("ATK Growth:"+ATKGrowth.ToString());
		Debug.Log ("DEF Growth:"+DEFGrowth.ToString());
		Debug.Log ("ATK:"+ATK.ToString());
		Debug.Log ("DEF:"+DEF.ToString());
	}

	public void AttributeInit()
	{
		HP_max = 100;
		BallisticSpeed = 10;
		BallisticDamage = -34;
		//FireRate = PlayerPrefs.GetFloat ("FireRate", 1);
		MP_max = 100;
		EXP = 0;
		Level = 1;
		//ATKGrowth = PlayerPrefs.GetFloat ("ATKGrowth", 10);
		//DEFGrowth = PlayerPrefs.GetFloat ("DEFGrowth", 5);
		//HPGrowth = PlayerPrefs.GetFloat ("HPGrowth", 20);
		//MPGrowth = PlayerPrefs.GetFloat ("MPGrowth", 10);
		EXPForLevelUp = 1000;
		gold = 1500;
		DropGold = 100;
		team = 600;

		ifAlive = true;
		if_Player = true;
		ForceBackDerection = new Vector3 (0, 0, 0);
		HP = 100;
		MP = 100;
		//ATK = PlayerPrefs.GetFloat ("ATK", 90);
		//DEF = PlayerPrefs.GetFloat ("DEF", 10);
		ForceBackPower = 1;
		ForceBackCounterMax = 10;
		ForceBackCounter = -1;

		//skillUp_Num = 1;
		Skill_Level[0] = 0;
		Skill_Level[1] = 0;
		Skill_Level[2] = 0;
		Skill_Level[3] = 0;

		level_num = 1;
	}

	public void SaveAttributeInFile(int num_save = 0)
	{
		string path = Application.dataPath;
		string dir = "/save/";
		string name = "GameSave";
		string format = ".mygame";
		if (!Directory.Exists (path + dir))
			Directory.CreateDirectory (path + dir);
		string num_str = num_save.ToString ();
		if(num_save > 0)
			name = name + num_str;
		FileStream fs = new FileStream(path + dir + name + format,FileMode.OpenOrCreate);
		string tempString = "";
		using (StreamWriter sw = new StreamWriter (fs))
		{
			tempString += ("HP_max " + HP_max.ToString () + "\n");
			tempString += ("BallisticSpeed " + BallisticSpeed.ToString () + "\n");
			tempString += ("BallisticDamage " + BallisticDamage.ToString () + "\n");
			tempString += ("FireRate " + FireRate.ToString () + "\n");
			tempString += ("MP_max " + MP_max.ToString () + "\n");
			tempString += ("EXPmax " + EXP.ToString () + "\n");
			tempString += ("Level " + Level.ToString () + "\n");
			tempString += ("ATKGrowth " + ATKGrowth.ToString () + "\n");
			tempString += ("DEFGrowth " + DEFGrowth.ToString () + "\n");
			tempString += ("HPGrowth " + HPGrowth.ToString () + "\n");
			tempString += ("MPGrowth " + MPGrowth.ToString () + "\n");
			tempString += ("EXPForLevelUp " + EXPForLevelUp.ToString () + "\n");
			tempString += ("gold " + gold.ToString () + "\n");
			tempString += ("DropGold " + DropGold.ToString () + "\n");
			tempString += ("team " + team.ToString () + "\n");
			tempString += ("HP " + HP.ToString () + "\n");
			tempString += ("MP " + MP.ToString () + "\n");
			tempString += ("ATK " + ATK.ToString () + "\n");
			tempString += ("DEF " + DEF.ToString () + "\n");
			tempString += ("ForceBackPower " + ForceBackPower.ToString () + "\n");
			tempString += ("ForceBackCounterMax " + ForceBackCounterMax.ToString () + "\n");
			tempString += ("ForceBackCounter " + ForceBackCounter.ToString () + "\n");
			tempString += ("skillUp_Num " + skillUp_Num.ToString () + "\n");
			tempString += ("Skill_Level0 " + Skill_Level [0].ToString () + "\n");
			tempString += ("Skill_Level1 " + Skill_Level [1].ToString () + "\n");
			tempString += ("Skill_Level2 " + Skill_Level [2].ToString () + "\n");
			tempString += ("Skill_Level3 " + Skill_Level [3].ToString () + "\n");
			tempString += ("level_num " + (level_num + 1).ToString () + "\n");
			if (bag != null) {
				for (int _i = 0; _i < 12; _i++) {
					tempString += ("Item_num" + _i.ToString () + " " + bag.GetComponent<bagManager> ().ItemOwned [_i].ToString () + "\n");
				}
			}
            tempString += ("ZhiYe " + ZhiYe +"\n");
            save_time = System.DateTime.Now.ToString ();
			tempString += ("save_time " + save_time);

			string encryptString = Encrypt (tempString);
			sw.Write (encryptString);
		}
		fs.Close ();
	}

	public void ReadAttributeFromFile(int num_save = 0)
	{
		string path = Application.dataPath;
		string dir = "/save/";
		string name = "GameSave";
		string format = ".mygame";
		string num_str = num_save.ToString ();
		if(num_save > 0)
			name = name + num_str;
		if (!Directory.Exists (path + dir))
			Directory.CreateDirectory (path + dir);
		FileInfo t = new FileInfo (path + dir + name + format);
		if (!t.Exists)
		{
			
		}
		FileStream fs = new FileStream(path + dir + name + format,FileMode.OpenOrCreate);
		using (StreamReader sr = new StreamReader (fs))
		{
			int index = 0;
			string readStr = sr.ReadToEnd ();
			string result = Decrypt (readStr);
			string[] allArray = result.Split ('\n');
			string[] strArray = allArray [index++].Split (' ');
			HP_max = float.Parse (strArray [1]);

			strArray = allArray [index++].Split (' ');
			BallisticSpeed = float.Parse (strArray [1]);
			
			strArray = allArray [index++].Split (' ');
			BallisticDamage = float.Parse (strArray [1]);
			
			strArray = allArray [index++].Split (' ');
			FireRate = float.Parse (strArray [1]);
			
			strArray = allArray [index++].Split (' ');
			MP_max = float.Parse (strArray [1]);
			
			strArray = allArray [index++].Split (' ');
			EXP = float.Parse (strArray [1]);

			
			strArray = allArray [index++].Split (' ');
			Level = float.Parse (strArray [1]);
			
			strArray = allArray [index++].Split (' ');
			ATKGrowth = float.Parse (strArray [1]);
			
			strArray = allArray [index++].Split (' ');
			DEFGrowth = float.Parse (strArray [1]);
			
			strArray = allArray [index++].Split (' ');
			HPGrowth = float.Parse (strArray [1]);
			
			strArray = allArray [index++].Split (' ');
			MPGrowth = float.Parse (strArray [1]);
			
			strArray = allArray [index++].Split (' ');
			EXPForLevelUp = float.Parse (strArray [1]);

			
			strArray = allArray [index++].Split (' ');
			gold = float.Parse (strArray [1]);
			
			strArray = allArray [index++].Split (' ');
			DropGold = float.Parse (strArray [1]);
			
			strArray = allArray [index++].Split (' ');
			team = float.Parse (strArray [1]);
			
			strArray = allArray [index++].Split (' ');
			HP = float.Parse (strArray [1]);
			
			strArray = allArray [index++].Split (' ');
			MP = float.Parse (strArray [1]);
			
			strArray = allArray [index++].Split (' ');
			ATK = float.Parse (strArray [1]);

			
			strArray = allArray [index++].Split (' ');
			DEF = float.Parse (strArray [1]);
			
			strArray = allArray [index++].Split (' ');
			ForceBackPower = float.Parse (strArray [1]);
			
			strArray = allArray [index++].Split (' ');
			ForceBackCounterMax = float.Parse (strArray [1]);
			
			strArray = allArray [index++].Split (' ');
			ForceBackCounter = float.Parse (strArray [1]);
			
			strArray = allArray [index++].Split (' ');
			skillUp_Num = int.Parse (strArray [1]);
			
			strArray = allArray [index++].Split (' ');
			Skill_Level [0] = float.Parse (strArray [1]);

			
			strArray = allArray [index++].Split (' ');
			Skill_Level [1] = float.Parse (strArray [1]);
			
			strArray = allArray [index++].Split (' ');
			Skill_Level [2] = float.Parse (strArray [1]);
			
			strArray = allArray [index++].Split (' ');
			Skill_Level [3] = float.Parse (strArray [1]);
			
			strArray = allArray [index++].Split (' ');
			// save it as level_num +1
			// read it should be level_num -1
			level_num = int.Parse (strArray [1]) - 1;

			if (bag != null) {
				for (int _i = 0; _i < 12; _i++) {
					strArray = allArray [index++].Split (' ');
					bag.GetComponent<bagManager> ().ItemOwned [_i] = int.Parse (strArray [1]);
				}
			}

            strArray = allArray[index++].Split(' ');
            ZhiYe = strArray[1];

            strArray = allArray [index++].Split (' ');
			save_time = strArray [1] + " " + strArray [2] + " " + strArray [3];


			ifAlive = true;
			if_Player = true;
			ForceBackDerection = new Vector3 (0, 0, 0);
		}
		fs.Close ();
		//ui_EXP.value = Mathf.Max(EXP, 0) / EXPForLevelUp;
		//ui_HP.value = Mathf.Max(HP, 0) / HP_max;
	}

	//method to get the attribute from file
	public string GetAttributeFromFile(string _attribute,int num_save = 0)
	{
		string path = Application.dataPath;
		string dir = "/save/";
		string name = "GameSave";
		string format = ".mygame";
		string num_str = num_save.ToString ();
		if(num_save > 0)
			name = name + num_str;
		if (!Directory.Exists (path + dir))
			Directory.CreateDirectory (path + dir);
		FileInfo t = new FileInfo (path + dir + name + format);
		if (!t.Exists)
		{
			return " ";
		}
		FileStream fs = new FileStream(path + dir + name + format,FileMode.Open);
		using (StreamReader sr = new StreamReader (fs))
		{
			string decryptString = sr.ReadToEnd ();	
			string str = Decrypt (decryptString);
			string[] strArray = str.Split (' ', '\n');
			for (int i = 0; i < str.Length; i++)
			{
				if (strArray [i] == _attribute)
				{
					if (_attribute == "save_time")
						return strArray [i + 1] + " " + strArray [i + 2] + " " + strArray [i + 3];	
					fs.Close ();
					return strArray [i + 1];
				}
			}
		}
		fs.Close ();
		return " ";
	}


	public float GetLevelOfPlayerFromFile(int num_save = 0)
	{
		return float.Parse(GetAttributeFromFile ("Level", num_save));
	}


	public float GetGoldOfPlayerFromFile(int num_save = 0)
	{
		return float.Parse(GetAttributeFromFile ("gold", num_save));
	}
    public string GetTypeOfPlayerFromFile(int num_save = 0)
    {
        return GetAttributeFromFile("ZhiYe", num_save);
    }

    public int GetLevelOfGameFromFile(int num_save = 0)
	{
		// save it as level_num +1
		// read it should be level_num -1
		return int.Parse(GetAttributeFromFile ("level_num", num_save)) - 1;
	}


	public string GetSaveTimeFromFile(int num_save = 0)
	{
		return GetAttributeFromFile ("save_time", num_save);
	}

	private string Keys = "abcdefghabcdefgh";
	public string Key = "abcdefgh";

	public string EncryptDES(string encryptString,string encryptKey)
	{
		try
		{
			
			byte[] byteKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0,8));
			byte[] byteKeys = Encoding.UTF8.GetBytes(Keys);
			byte[] byteString = Encoding.UTF8.GetBytes(encryptString);

			/*
			RijndaelManaged rDel = new RijndaelManaged();
			rDel.Key = byteKeys;
			rDel.BlockSize = 128;
			rDel.Mode = CipherMode.CBC;
			rDel.IV = rDel.Key;
			rDel.Padding = PaddingMode.ISO10126;
			ICryptoTransform cTransform = rDel.CreateEncryptor();
			byte [] resultArray = cTransform.TransformFinalBlock(byteString,0,byteString.Length);
			return Encoding.UTF8.GetString(resultArray);
			*/


			string str = null;
			DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
			using (MemoryStream mStream = new MemoryStream())
			{
			
				using(CryptoStream cStream = new CryptoStream(mStream,dCSP.CreateEncryptor(byteKey,byteKeys),CryptoStreamMode.Write))
				{
				cStream.Write(byteString,0,byteString.Length);
				cStream.FlushFinalBlock();
				//cStream.Close();
			
			Debug.Log("string before En");
			Debug.Log(encryptString.Length);
			Debug.Log(encryptString);
			Debug.Log("byte before En");
			Debug.Log(byteString.Length);
			Debug.Log(byteString);
			Debug.Log("En byte array");
			Debug.Log(mStream.ToArray().Length);
			Debug.Log(mStream.ToArray());
			Debug.Log("string after En");
					str = Encoding.UTF8.GetString(mStream.ToArray());
			Debug.Log(Encoding.UTF8.GetString(mStream.ToArray()).Length);
			Debug.Log(Encoding.UTF8.GetString(mStream.ToArray()));
			
				//return Encoding.UTF8.GetString(mStream.ToArray());
				}
			}
			return str;

		}
		catch
		{
			Debug.Log("EN Fail!!!!!!!!!!!!!!!");
			return encryptString;
		}
	}

	public string DecryptDES(string decryptString,string decryptKey)
	{
		
		try
		{
			byte[] byteKey = Encoding.UTF8.GetBytes(decryptKey.Substring(0,8));
			byte[] byteKeys = Encoding.UTF8.GetBytes(Keys);
			byte[] byteString = Encoding.UTF8.GetBytes(decryptString);

			string str = null;

			DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();

			using(MemoryStream mStream = new MemoryStream())
			{
				using(CryptoStream cStream = new CryptoStream(mStream,dCSP.CreateDecryptor(byteKey,byteKeys),CryptoStreamMode.Write))
				{
			cStream.Write(byteString,0,byteString.Length);
			Debug.Log("De byte array before De");
			Debug.Log(byteString.Length);
			Debug.Log(byteString);
			//cStream.Flush();
			cStream.FlushFinalBlock();
			//cStream.Close();
			Debug.Log("string after De");
					str = Encoding.UTF8.GetString(mStream.ToArray());
			Debug.Log(Encoding.UTF8.GetString(mStream.ToArray()).Length);
			Debug.Log(Encoding.UTF8.GetString(mStream.ToArray()));
			//return Encoding.UTF8.GetString(mStream.ToArray());
				}
			}

			return str;
			/*
			RijndaelManaged rDel = new RijndaelManaged();
			rDel.Key = byteKeys;
			rDel.BlockSize = 128;
			rDel.Mode = CipherMode.CBC;
			rDel.IV = rDel.Key;
			rDel.Padding = PaddingMode.ISO10126;
			ICryptoTransform cTransform = rDel.CreateDecryptor();
			byte [] resultArray = cTransform.TransformFinalBlock(byteString,0,byteString.Length);
			return Encoding.UTF8.GetString(resultArray);
			*/
		}
		catch (System.Exception ex)
		{
			Debug.Log("DE Fail!!!!!!!!!!!!!!!");
			Debug.Log (ex);
			return decryptString;
		}
	}


	public string Encrypt(string enString)
	{
		byte[] byteString = Encoding.ASCII.GetBytes (enString);
		for (int i = 0; i < byteString.Length; i++)
		{
			if (byteString [i] < 0x40)
				byteString [i] -= 10;
			else
				byteString [i] += 7;
		}
		string result = Encoding.ASCII.GetString (byteString);
		return result;
	}

	public string Decrypt(string deString)
	{
		byte[] byteString = Encoding.ASCII.GetBytes (deString);
		for (int i = 0; i < byteString.Length; i++)
		{
			if (byteString [i] < 0x40 - 10)
				byteString [i] += 10;
			else
				byteString [i] -= 7;
		}
		string result = Encoding.ASCII.GetString (byteString);
		return result;
	}

}
