using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine;

public class Boss : MonoBehaviour {

    void OnParticleCollision(GameObject colli)
    {

        if (colli.name == "Skill_4 Partical")
        {
            GameObject player = colli.transform.parent.gameObject;
            //float damage = player.GetComponent<ThirdPersonUserControl>().Skill_4_Value[0] + player.GetComponent<ThirdPersonUserControl>().Skill_4_Value[3] * player.GetComponent<attribute>().Skill_Level[3];
            float damage = (player.GetComponent<attribute>().ATK + player.GetComponent<attribute>().ATK_bouns) * (player.GetComponent<ThirdPersonUserControl>().Skill_4_Value[0] + player.GetComponent<ThirdPersonUserControl>().Skill_4_Value[3] * player.GetComponent<attribute>().Skill_Level[3]);
            float _t = this.gameObject.GetComponent<attribute>().HP;
            float _c = this.gameObject.GetComponent<attribute>().update_HP(Mathf.Min(-damage + this.gameObject.GetComponent<attribute>().DEF+ this.gameObject.GetComponent<attribute>().DEF_bouns, 0));
            if (_t >=0 && _c <= 0)
            {
                player.GetComponent<attribute>().update_EXP(this.gameObject.GetComponent<attribute>().DropEXP);
                // player.GetComponent<attribute>().gold += this.gameObject.GetComponent<attribute>().DropGold;
				// can't get gold directly, produce a gold coin
				this.gameObject.GetComponent<attribute>().dropGoods("coin");
            }
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
