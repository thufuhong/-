using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.UI;
using System.IO;
using System.Text;

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

    public float HP;
    public float MP;
    public float ATK = 20;
    public float DEF = 10;
    public float ForceBackPower = 1f;
    public float ForceBackCounterMax = 10;
    public float ForceBackCounter = -1;
    public Vector3 ForceBackDerection;

    private int skillUp_Num = 0;


	public int level_num = 1;
	public string save_time;

    

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
            if(if_Player)
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
                if (this.gameObject.tag == "Team0")
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
        }
        
        while(EXP>= EXPForLevelUp)
        {
            EXP -= EXPForLevelUp;
            Level++;
            skillUp_Num++;
            ATK += ATKGrowth;
            DEF += DEFGrowth;
            HP_max += HPGrowth;
            MP_max += MPGrowth;
            ParticalLevelUp.SetActive(true);
            SkillUp_button.SetActive(true);
        }

        if (if_Player)
            ui_EXP.value = Mathf.Max(EXP, 0) / EXPForLevelUp;

    }

	// interface about goods droped when be killed
	public void dropGoods(string type) {
		if (type == "coin") {
			GameObject coin = Instantiate (transform.Find ("/Terrain").GetComponent<GameObjectGenerate> ().itemCoin,
				transform.position + new Vector3(0, (float) (GetComponent<CapsuleCollider>().bounds.size.y * 0.5), 0),
				Quaternion.Euler (-90, 0, 0)
			);
			coin.GetComponent<pickGoods> ().value = DropGold;
			// Debug.Log ("coin" + coin.GetComponent<pickGoods> ().value + " cur:" + DropGold);
		}
	}

	public void update_gold(float value) {
		if (ifAlive) {
			gold += value;
		}
	}

    public void Update_Skill(int SkillNum)
    {
        Skill_Level[SkillNum] += 1;
        skillUp_Num--;
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
        if (Skill_Level[0] >= 1)
            Skill_1_Icon.GetComponent<Image>().fillAmount = 0;
        if (Skill_Level[1] >= 1)
            Skill_2_Icon.GetComponent<Image>().fillAmount = 0;
        if (Skill_Level[2] >= 1)
            Skill_3_Icon.GetComponent<Image>().fillAmount = 0;
        if (Skill_Level[3] >= 1)
            Skill_4_Icon.GetComponent<Image>().fillAmount = 0;
        if (Level == 1)
            skillUp_Num++;
        if (skillUp_Num > 0)
            SkillUp_button.SetActive(true);

    }
	
	// Update is called once per frame
	void FixedUpdate () {


        if(ForceBackCounter>=0)
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

		PlayerPrefs.SetInt ("level_num", level_num);



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
		team = PlayerPrefs.GetFloat ("team", 600);

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

		level_num = PlayerPrefs.GetInt ("level_num", 1);



		ui_EXP.value = Mathf.Max(EXP, 0) / EXPForLevelUp;
		ui_HP.value = Mathf.Max(HP, 0) / HP_max;
	}

	public void EnemyUpdate()
	{
		HP = HP_max = level_num * 50f;
		ATK = level_num * 10;
		DEF = level_num * 5;
		DropGold = level_num * 300;
		DropEXP = level_num * 50;
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
		gold = 0;
		DropGold = 100;
		team = 600;

		ifAlive = true;
		if_Player = true;
		ForceBackDerection = new Vector3 (0, 0, 0);
		//ifAlive = PlayerPrefs.GetFloat ("ifAlive", 100);
		//if_Player = PlayerPrefs.GetFloat ("if_Player", 100);

		HP = 100;
		MP = 100;
		//ATK = PlayerPrefs.GetFloat ("ATK", 90);
		//DEF = PlayerPrefs.GetFloat ("DEF", 10);
		ForceBackPower = 1;
		ForceBackCounterMax = 10;
		ForceBackCounter = -1;

		skillUp_Num = 0;
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
		using (StreamWriter sw = new StreamWriter (fs))
		{
			sw.WriteLine ("HP_max " + HP_max.ToString ());
			sw.WriteLine ("BallisticSpeed " + BallisticSpeed.ToString ());
			sw.WriteLine ("BallisticDamage " + BallisticDamage.ToString ());
			sw.WriteLine ("FireRate " + FireRate.ToString ());
			sw.WriteLine ("MP_max " + MP_max.ToString ());
			sw.WriteLine ("EXPmax " + EXP.ToString ());
			sw.WriteLine ("Level " + Level.ToString ());
			sw.WriteLine ("ATKGrowth " + ATKGrowth.ToString ());
			sw.WriteLine ("DEFGrowth " + DEFGrowth.ToString ());
			sw.WriteLine ("HPGrowth " + HPGrowth.ToString ());
			sw.WriteLine ("MPGrowth " + MPGrowth.ToString ());
			sw.WriteLine ("EXPForLevelUp " + EXPForLevelUp.ToString ());
			sw.WriteLine ("gold " + gold.ToString ());
			sw.WriteLine ("DropGold " + DropGold.ToString ());
			sw.WriteLine ("team " + team.ToString ());
			sw.WriteLine ("HP " + HP.ToString ());
			sw.WriteLine ("MP " + MP.ToString ());
			sw.WriteLine ("ATK " + ATK.ToString ());
			sw.WriteLine ("DEF " + DEF.ToString ());
			sw.WriteLine ("ForceBackPower " + ForceBackPower.ToString ());
			sw.WriteLine ("ForceBackCounterMax " + ForceBackCounterMax.ToString ());
			sw.WriteLine ("ForceBackCounter " + ForceBackCounter.ToString ());
			sw.WriteLine ("skillUp_Num " + skillUp_Num.ToString ());
			sw.WriteLine ("Skill_Level0 " + Skill_Level [0].ToString ());
			sw.WriteLine ("Skill_Level1 " + Skill_Level [1].ToString ());
			sw.WriteLine ("Skill_Level2 " + Skill_Level [2].ToString ());
			sw.WriteLine ("Skill_Level3 " + Skill_Level [3].ToString ());
			sw.WriteLine ("level_num " + level_num.ToString ());
			save_time = System.DateTime.Now.ToString ();
			sw.WriteLine ("save_time " + save_time);
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
			string str = sr.ReadLine ();
			string[] strArray = str.Split (' ');
			HP_max = float.Parse (strArray [1]);

			str = sr.ReadLine ();
			strArray = str.Split (' ');
			BallisticSpeed = float.Parse (strArray [1]);
			str = sr.ReadLine ();
			strArray = str.Split (' ');
			BallisticDamage = float.Parse (strArray [1]);
			str = sr.ReadLine ();
			strArray = str.Split (' ');
			FireRate = float.Parse (strArray [1]);
			str = sr.ReadLine ();
			strArray = str.Split (' ');
			MP_max = float.Parse (strArray [1]);
			str = sr.ReadLine ();
			strArray = str.Split (' ');
			EXP = float.Parse (strArray [1]);

			str = sr.ReadLine ();
			strArray = str.Split (' ');
			Level = float.Parse (strArray [1]);
			str = sr.ReadLine ();
			strArray = str.Split (' ');
			ATKGrowth = float.Parse (strArray [1]);
			str = sr.ReadLine ();
			strArray = str.Split (' ');
			DEFGrowth = float.Parse (strArray [1]);
			str = sr.ReadLine ();
			strArray = str.Split (' ');
			HPGrowth = float.Parse (strArray [1]);
			str = sr.ReadLine ();
			strArray = str.Split (' ');
			MPGrowth = float.Parse (strArray [1]);
			str = sr.ReadLine ();
			strArray = str.Split (' ');
			EXPForLevelUp = float.Parse (strArray [1]);

			str = sr.ReadLine ();
			strArray = str.Split (' ');
			gold = float.Parse (strArray [1]);
			str = sr.ReadLine ();
			strArray = str.Split (' ');
			DropGold = float.Parse (strArray [1]);
			str = sr.ReadLine ();
			strArray = str.Split (' ');
			team = float.Parse (strArray [1]);
			str = sr.ReadLine ();
			strArray = str.Split (' ');
			HP = float.Parse (strArray [1]);
			str = sr.ReadLine ();
			strArray = str.Split (' ');
			MP = float.Parse (strArray [1]);
			str = sr.ReadLine ();
			strArray = str.Split (' ');
			ATK = float.Parse (strArray [1]);

			str = sr.ReadLine ();
			strArray = str.Split (' ');
			DEF = float.Parse (strArray [1]);
			str = sr.ReadLine ();
			strArray = str.Split (' ');
			ForceBackPower = float.Parse (strArray [1]);
			str = sr.ReadLine ();
			strArray = str.Split (' ');
			ForceBackCounterMax = float.Parse (strArray [1]);
			str = sr.ReadLine ();
			strArray = str.Split (' ');
			ForceBackCounter = float.Parse (strArray [1]);
			str = sr.ReadLine ();
			strArray = str.Split (' ');
			skillUp_Num = int.Parse (strArray [1]);
			str = sr.ReadLine ();
			strArray = str.Split (' ');
			Skill_Level [0] = float.Parse (strArray [1]);

			str = sr.ReadLine ();
			strArray = str.Split (' ');
			Skill_Level [1] = float.Parse (strArray [1]);
			str = sr.ReadLine ();
			strArray = str.Split (' ');
			Skill_Level [2] = float.Parse (strArray [1]);
			str = sr.ReadLine ();
			strArray = str.Split (' ');
			Skill_Level [3] = float.Parse (strArray [1]);
			str = sr.ReadLine ();
			strArray = str.Split (' ');
			level_num = int.Parse (strArray [1]);
			str = sr.ReadLine ();
			strArray = str.Split (' ');
			save_time = strArray [1];


			ifAlive = true;
			if_Player = true;
			ForceBackDerection = new Vector3 (0, 0, 0);
		}
		fs.Close ();
		ui_EXP.value = Mathf.Max(EXP, 0) / EXPForLevelUp;
		ui_HP.value = Mathf.Max(HP, 0) / HP_max;
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
			string str = sr.ReadToEnd ();
			string[] strArray = str.Split (' ', '\n');
			for (int i = 0; i < str.Length; i++)
			{
				if (strArray [i] == _attribute)
				{
					if (_attribute == "save_time")
						return strArray [i + 1] + strArray [i + 2] + strArray [i + 3];	
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


	public int GetLevelOfGameFromFile(int num_save = 0)
	{
		return int.Parse(GetAttributeFromFile ("level_num", num_save));
	}


	public string GetSaveTimeFromFile(int num_save = 0)
	{
		return GetAttributeFromFile ("save_time", num_save);
	}

}
