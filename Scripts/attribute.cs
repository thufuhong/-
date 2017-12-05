using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.UI;

public class attribute : MonoBehaviour {
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
    public float ATKGrowth = 2;
    public float DEFGrowth = 2;
    public float HPGrowth = 20;
    public float MPGrowth = 10;
    public float EXPForLevelUp = 1000f;
    public float gold = 0;
    public float DropGold = 100f;
    public float DropEXP = 600f;
    public float[] Skill_Level = { 0, 0, 0, 0 };


    public float team = 0;
    public bool ifAlive = true;
    public Slider ui_HP;
    public Slider ui_EXP;
    public Text ui_level;
    public bool if_Player = false;
    public GameObject respawn_button;
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
    

    public float update_HP(float value)
    {
        if (ifAlive)
        {
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
                ifAlive = false;
                if(this.gameObject.tag == "Team1")
                    Destroy(this.gameObject, 1.0f);
                if (this.gameObject.tag == "Team0")
                    respawn_button.SetActive(true);
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
            ui_level.text = ((int)Level).ToString();
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
        if(if_Player)
            ui_level.text = ((int)Level).ToString();
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
}
