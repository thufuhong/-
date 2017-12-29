using System;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class ThirdPersonUserControl : MonoBehaviour
    {
        private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object                  // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward;             // The current forward direction of the camera
        private Vector3 m_Move;
        private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.
        Animator m_Animator;
        public Transform attaction_generate_position;
        public GameObject attack_object;
        public GameObject ParticalHPUp;
        public Safe_area_sever Sa;
        public float Skill_1_CoolDownTime_Max = 10.0f;
        public float Skill_1_Duration = 5.0f;
        public float[] Skill_1_Value = { 1.1f, 1.2f, 2.0f, 0.1f, 0.2f, 0.0f}; //Base Value1, Base Vlaue2, ..., Update Value1, Update Value2, ...
        public GameObject Skill_1_Icon;
        public float Skill_2_CoolDownTime_Max = 10.0f;
        public float Skill_2_Duration = 5.0f;
        public float[] Skill_2_Value = { 30, 5, 10 ,1 };
        public GameObject Skill_2_Icon;
        public float Skill_3_CoolDownTime_Max = 10.0f;
        public float Skill_3_Duration = 5.0f;
        public float[] Skill_3_Value = { 10, 2 };
        public GameObject Skill_3_Icon;
        public float Skill_4_CoolDownTime_Max = 10.0f;
        public float Skill_4_Duration = 5.0f;
        public float[] Skill_4_Value = { 1.0f, 10, 1, 0.02f, 1, 0.07f };
        public GameObject Skill_4_Icon;
        public float ATKTime_max = 20.0f;
        public float DEFTime_max = 20.0f;
        public bagManager Bag;
        public GameObject MainScreenItem;
        public GameObject ShopObject;
        public GameObject StateIcon;
        public GameObject boss;

        private attribute _attri;
        private float CoolDownTime = -1.0f;
        private float Skill_1_CoolDownTime = -1.0f;
        private bool Skill_1_active;
        private float Skill_2_CoolDownTime = -1.0f;
        private bool Skill_2_active;
        private float Skill_3_CoolDownTime = -1.0f;
        private bool Skill_3_active;
        private float Skill_4_CoolDownTime = -1.0f;
        private bool Skill_4_active;
        private float Safe_area_damage_persecond_record;
        private float ATKTime = -1f;
        private float ATK_multiplier;
        private float DEFTime = -1f;
        private float DEF_multiplier;
        private float item_cooldown = -1.0f; //内置按键冷却时间0.5s，防止重复判定


        private void Start()
        {
            m_Animator = GetComponent<Animator>();
            _attri = GetComponent<attribute>();

            // get the third person character ( this should never be null due to require component )
            m_Character = GetComponent<ThirdPersonCharacter>();
        }


        private void Update()
        {
        }


        void OnParticleCollision(GameObject colli)
        {
            if(colli.name == "Boss Partical")
            // Boss ATK: 20, And 50% of Player's DEF is ignored.
                this.gameObject.GetComponent<attribute>().update_HP(Mathf.Min(-boss.GetComponent<attribute>().ATK+ 0.7f*(this.gameObject.GetComponent<attribute>().DEF+ this.gameObject.GetComponent<attribute>().DEF_bouns),0));

        }

        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            attribute _attr = this.gameObject.GetComponent<attribute>();
            if (CoolDownTime > 0)
                CoolDownTime -= Time.fixedDeltaTime;
            if (Skill_1_CoolDownTime > 0)
            {
                Skill_1_CoolDownTime -= Time.fixedDeltaTime;
				if (Skill_1_Icon != null) {
					Skill_1_Icon.GetComponent<Image> ().fillAmount = Mathf.Max (0, Skill_1_CoolDownTime) / Skill_1_CoolDownTime_Max;
					StateIcon.transform.Find ("Skill1").gameObject.GetComponent<Image> ().fillAmount = Mathf.Max (1 - (Skill_1_CoolDownTime_Max - Skill_1_CoolDownTime) / Skill_1_Duration, 0.0f);
				}
                if (Skill_1_CoolDownTime_Max - Skill_1_CoolDownTime >= Skill_1_Duration && Skill_1_active)
                {
                    Skill_1_active = false;
                    Renderer[] list;
                    list = this.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
                    for (int i = 0; i < list.Length; i++)
                    {
                        list[i].materials[0].color = Color.white;
                    }
                    _attr.ATK_bouns -= _attr.ATK * (Skill_1_Value[0] + Skill_1_Value[3] * _attr.Skill_Level[0] - 1);
                    //_attr.DEF_bouns -= _attr.DEF * (Skill_1_Value[1] + Skill_1_Value[4] * _attr.Skill_Level[0] - 1);
                    //_attr.ATK /= Skill_1_Value[0] + Skill_1_Value[3] * _attr.Skill_Level[0];
                    //_attr.DEF /= Skill_1_Value[1] + Skill_1_Value[4] * _attr.Skill_Level[0];
                    this.gameObject.GetComponent<ThirdPersonCharacter>().m_MoveSpeedMultiplier /= Skill_1_Value[2] + Skill_1_Value[5] * _attr.Skill_Level[0];
                }
                
            }
            if (Skill_2_CoolDownTime > 0)
            {
                Skill_2_CoolDownTime -= Time.fixedDeltaTime;
				if (Skill_2_Icon != null) {
					Skill_2_Icon.GetComponent<Image>().fillAmount = Mathf.Max(0, Skill_2_CoolDownTime) / Skill_2_CoolDownTime_Max;
					StateIcon.transform.Find("Skill2").gameObject.GetComponent<Image>().fillAmount = Mathf.Max(1 - (Skill_2_CoolDownTime_Max - Skill_2_CoolDownTime) / Skill_2_Duration, 0.0f);
				}
                if (Skill_2_active)
                    this.gameObject.GetComponent<attribute>().update_HP((Skill_2_Value[1] + Skill_2_Value[3] * this.gameObject.GetComponent<attribute>().Skill_Level[1]) * Time.fixedDeltaTime);
                if (Skill_2_CoolDownTime_Max - Skill_2_CoolDownTime >= Skill_2_Duration && Skill_2_active)
                {
                    Skill_2_active = false;
                }
            }
            if (Skill_3_CoolDownTime > 0)
            {
                Skill_3_CoolDownTime -= Time.fixedDeltaTime;
				if (Skill_3_Icon != null) {
					Skill_3_Icon.GetComponent<Image>().fillAmount = Mathf.Max(0, Skill_3_CoolDownTime) / Skill_3_CoolDownTime_Max;
					StateIcon.transform.Find("Skill3").gameObject.GetComponent<Image>().fillAmount = Mathf.Max(1 - (Skill_3_CoolDownTime_Max - Skill_3_CoolDownTime) / (Skill_3_Value[0] + Skill_3_Value[1] * _attr.Skill_Level[2]), 0.0f);
				}
                if (Skill_3_CoolDownTime_Max - Skill_3_CoolDownTime >= Skill_3_Value[0] + Skill_3_Value[1]*_attr.Skill_Level[2] && Skill_3_active)
                {
                    Skill_3_active = false;
                    this.gameObject.transform.Find("Skill_3_shelter").gameObject.SetActive(false);
                    Sa.DamagePerSecond = Safe_area_damage_persecond_record;
                    _attr.DEF_bouns -= _attr.DEF * (Skill_1_Value[1] + Skill_1_Value[4] * _attr.Skill_Level[2] - 1);
                }
            }
            if (Skill_4_CoolDownTime > 0)
            {
                Skill_4_CoolDownTime -= Time.fixedDeltaTime;
				if (Skill_4_Icon != null) {
					Skill_4_Icon.GetComponent<Image> ().fillAmount = Mathf.Max (0, Skill_4_CoolDownTime) / Skill_4_CoolDownTime_Max;
				}
            }
            if (ATKTime > 0)
            {
                ATKTime -= Time.fixedDeltaTime;
                StateIcon.transform.Find("ATK").gameObject.GetComponent<Image>().fillAmount = Mathf.Max(ATKTime / ATKTime_max, 0.0f);
                if (ATKTime <= 0)
                    _attr.ATK_bouns -= _attr.ATK * (ATK_multiplier - 1);
            }
            if (DEFTime > 0)
            {
                DEFTime -= Time.fixedDeltaTime;
                StateIcon.transform.Find("DEF").gameObject.GetComponent<Image>().fillAmount = Mathf.Max(DEFTime / DEFTime_max, 0.0f);
                if (DEFTime <= 0)
                    _attr.DEF_bouns -= _attr.DEF * (DEF_multiplier - 1);
            }
            if (item_cooldown > 0)
                item_cooldown -= Time.fixedDeltaTime;

            if (!(this.gameObject.GetComponent<attribute>().ifAlive))
                return;
            
            // read inputs
            float h = Input.GetAxis("Horizontal"); // left or right arrow
            float v = Input.GetAxis("Vertical"); // up or down arrow


			Operation_LRUD (h, v);

            int state_now = m_Animator.GetCurrentAnimatorStateInfo(0).shortNameHash;
            if (state_now == Animator.StringToHash("Grounded") && Input.GetKeyDown(KeyCode.J) && CoolDownTime <= 0) //If use mouse, change to Input.GetButtonDown("Fire1")
            {
				Operation_J ();
            }
            if (state_now == Animator.StringToHash("Grounded") && Input.GetKeyDown(KeyCode.U) && Skill_1_CoolDownTime <= 0 && _attr.Skill_Level[0] > 0)
            {
				Operation_U ();
            }
            if (state_now == Animator.StringToHash("Grounded") && Input.GetKeyDown(KeyCode.I) && Skill_2_CoolDownTime <= 0 && _attr.Skill_Level[1] > 0)
            {
				Operation_I ();
            }
            if (state_now == Animator.StringToHash("Grounded") && Input.GetKeyDown(KeyCode.O) && Skill_3_CoolDownTime <= 0 && _attr.Skill_Level[2] > 0)
            {
				Operation_O ();
            }
            if (state_now == Animator.StringToHash("Grounded") && Input.GetKeyDown(KeyCode.P) && Skill_4_CoolDownTime <= 0 && _attr.Skill_Level[3] > 0)
            {
				Operation_P ();
            }
            if (Input.GetKeyDown(KeyCode.E) && item_cooldown < 0)
            {
				Operation_E ();
            }
            if (Input.GetKeyDown(KeyCode.F) && item_cooldown<0)
            {
				Operation_F ();
            }

            // pass all parameters to the character control script
            m_Character.Move(m_Move, false, m_Jump);
            m_Jump = false;
        }

		public void Operation_LRUD(float h, float v, bool dirMove = false)
		{
			if (v < 0) v = 0;
			m_Move = 3f * v * this.transform.forward + 1f * h * this.transform.right;
			if (dirMove)
				m_Character.Move (m_Move, false, false);
		}

		public void Operation_E()
		{
			item_cooldown = 0.5f;
			if (ShopObject.activeInHierarchy)
				ShopObject.SetActive(false);
			else
			{
				ShopObject.SetActive(true);
				ShopObject.GetComponent<bagManager>().RefreshBag();
			} 
		}

		public void Operation_P()
		{
			attribute _attr = this.gameObject.GetComponent<attribute>();
			Skill_4_active = true;
			Skill_4_CoolDownTime = Skill_4_CoolDownTime_Max;
			try {
				ParticleSystem.MainModule _m = this.gameObject.transform.Find("Skill_4 Partical").gameObject.GetComponent<ParticleSystem>().main;
				ParticleSystem.EmissionModule _e = this.gameObject.transform.Find("Skill_4 Partical").gameObject.GetComponent<ParticleSystem>().emission;
				_m.duration = 0.5f * (Skill_4_Value[2] + Skill_4_Value[5] * _attr.Skill_Level[3]);
				_e.rateOverTime = (Skill_4_Value[1] + Skill_4_Value[4] * _attr.Skill_Level[3]);
				this.gameObject.transform.Find("Skill_4 Partical").gameObject.SetActive(true);
			} catch {
			}

		}

		public void Operation_O()
		{
			attribute _attr = this.gameObject.GetComponent<attribute>();
			Skill_3_active = true;
			Skill_3_CoolDownTime = Skill_3_CoolDownTime_Max;
			if (this.gameObject.transform.Find("Skill_3_shelter") != null)
				this.gameObject.transform.Find("Skill_3_shelter").gameObject.SetActive(true);
			if (Sa != null) {
				Safe_area_damage_persecond_record = Sa.DamagePerSecond;
				Sa.DamagePerSecond = 0;
			}
			_attr.DEF_bouns += _attr.DEF * (Skill_1_Value[1] + Skill_1_Value[4] * _attr.Skill_Level[2] - 1);
		}

		public void Operation_I()
		{
			attribute _attr = this.gameObject.GetComponent<attribute>();
			Skill_2_active = true;
			Skill_2_CoolDownTime = Skill_2_CoolDownTime_Max;
			if (ParticalHPUp != null) {
				ParticalHPUp.SetActive (true);
			}
			_attr.update_HP(Skill_2_Value[0] + Skill_1_Value[2] * _attr.Skill_Level[1]);
		}

		public void Operation_U()
		{
			attribute _attr = this.gameObject.GetComponent<attribute>();
			Skill_1_active = true;
			Skill_1_CoolDownTime = Skill_1_CoolDownTime_Max;
			Renderer[] list;
			list = this.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();

			for(int i=0;i<list.Length;i++)
			{
				list[i].materials[0].color = Color.red;
			}

			_attr.ATK_bouns += _attr.ATK * (Skill_1_Value[0] + Skill_1_Value[3] * _attr.Skill_Level[0] - 1);
			//_attr.DEF_bouns += _attr.DEF * (Skill_1_Value[1] + Skill_1_Value[4] * _attr.Skill_Level[0] - 1);
			//_attr.ATK *= Skill_1_Value[0] + Skill_1_Value[3] * _attr.Skill_Level[0];
			//_attr.DEF *= Skill_1_Value[1] + Skill_1_Value[4] * _attr.Skill_Level[0]; 
			this.gameObject.GetComponent<ThirdPersonCharacter>().m_MoveSpeedMultiplier *= Skill_1_Value[2] + Skill_1_Value[5] * _attr.Skill_Level[0];
		}

		public void Operation_J()
		{
			CoolDownTime = _attri.FireRate;
			m_Animator.SetFloat("Random", UnityEngine.Random.Range(0f, 1f));
			m_Animator.SetTrigger("attack");
			GameObject temp = Instantiate(attack_object, attaction_generate_position.position, attaction_generate_position.rotation);
			temp.GetComponent<ballistic>().speed = _attri.BallisticSpeed;
			temp.GetComponent<ballistic>().side = _attri.team;
			temp.GetComponent<ballistic>().damage = _attri.ATK + _attri.ATK_bouns + UnityEngine.Random.Range(-3, 3);
			temp.GetComponent<ballistic>().From = this.gameObject;
			temp.GetComponent<ballistic>().duration = this.gameObject.GetComponent<attribute>().ZhiYe == "warrior" ? 1.0f : 3.0f;
			temp.GetComponent<Rigidbody>().velocity = (attaction_generate_position.forward).normalized * _attri.BallisticSpeed;
			temp.tag = "Team0";
		}
		public void Operation_F()
		{
			attribute _attr = this.gameObject.GetComponent<attribute>();
			if(MainScreenItem.transform.Find("Icon").gameObject.GetComponent<RawImage>().texture.Equals(Bag.nan) == false)
			{
				float _percentage = 0.0f;
				item_cooldown = 0.5f;
				int _x = Bag._x;
				int _y = Bag._y;
				if (_x == 0)
				{
					_percentage = _y == 0 ? 0.2f : _y == 1 ? 0.4f : _y == 2 ? 0.6f : 1.0f;
					_attr.update_HP(_attr.HP_max * _percentage);
					_attr.gameObject.transform.Find("HPup").gameObject.SetActive(true);
					Bag.ItemOwned[4 * _x + _y]--;
					Bag.RefreshBag();
					MainScreenItem.transform.Find("Number").gameObject.GetComponent<Text>().text = Bag.ItemOwned[4 * _x + _y].ToString();
					if (Bag.ItemOwned[4 * _x + _y] == 0)
					{
						MainScreenItem.transform.Find("Icon").gameObject.GetComponent<RawImage>().texture = Bag.nan;
						Bag.ItemGameObject[4 * _x + _y].transform.Find("Choosen").gameObject.SetActive(false);
					}
				}
				if (_x == 1)
				{
					_percentage = _y == 0 ? 0.3f : _y == 1 ? 0.6f : _y == 2 ? 0.9f : 1.5f;
					if(ATKTime<0)
					{
						ATKTime = ATKTime_max;
						ATK_multiplier = 1 + _percentage;
						_attr.ATK_bouns += _attr.ATK * _percentage;
						//_attr.ATK *=  1 + _percentage;
					}
					else
					{
						if(1 + _percentage == ATK_multiplier)
							ATKTime = ATKTime_max;
						else
						{
							ATKTime = ATKTime_max;
							_attr.ATK_bouns -= _attr.ATK * (ATK_multiplier - 1);
							//_attr.ATK /= ATK_multiplier;
							ATK_multiplier = 1 + _percentage;
							_attr.ATK_bouns += _attr.ATK * _percentage;
							//_attr.ATK *= 1 + _percentage;
						}
					}

					Bag.ItemOwned[4 * _x + _y]--;
					Bag.RefreshBag();
					MainScreenItem.transform.Find("Number").gameObject.GetComponent<Text>().text = Bag.ItemOwned[4 * _x + _y].ToString();
					if (Bag.ItemOwned[4 * _x + _y] == 0)
					{
						MainScreenItem.transform.Find("Icon").gameObject.GetComponent<RawImage>().texture = Bag.nan;
						Bag.ItemGameObject[4 * _x + _y].transform.Find("Choosen").gameObject.SetActive(false);
					}
				}
				if (_x == 2)
				{
					_percentage = _y == 0 ? 0.3f : _y == 1 ? 0.6f : _y == 2 ? 0.9f : 1.5f;
					if (DEFTime < 0)
					{
						DEFTime = DEFTime_max;
						DEF_multiplier = 1 + _percentage;
						_attr.DEF_bouns += _attr.DEF * _percentage;
						//_attr.DEF *= 1 + _percentage;
					}
					else
					{
						if (1 + _percentage == DEF_multiplier)
							DEFTime = DEFTime_max;
						else
						{
							DEFTime = DEFTime_max;
							//_attr.DEF /= DEF_multiplier;
							_attr.DEF_bouns -= _attr.DEF * (DEF_multiplier-1);
							DEF_multiplier = 1 + _percentage;
							//_attr.DEF *= 1 + _percentage;
							_attr.DEF_bouns += _attr.DEF * _percentage;
						}
					}

					Bag.ItemOwned[4 * _x + _y]--;
					Bag.RefreshBag();
					MainScreenItem.transform.Find("Number").gameObject.GetComponent<Text>().text = Bag.ItemOwned[4 * _x + _y].ToString();
					if (Bag.ItemOwned[4 * _x + _y] == 0)
					{
						MainScreenItem.transform.Find("Icon").gameObject.GetComponent<RawImage>().texture = Bag.nan;
						Bag.ItemGameObject[4 * _x + _y].transform.Find("Choosen").gameObject.SetActive(false);
					}
				}
			}
		}
    }
}