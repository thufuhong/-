using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.UI;

public class Safe_area_sever : MonoBehaviour 
{
    public GameObject quad1;
    public GameObject quad2;
    public GameObject quad3;
    public GameObject quad4;
    public GameObject player;
    public GameObject Boss;
    public GameObject shop;
    public Text ui_text;
    public GameObject Boss_Slider;

    public Avatar axe;
    public Avatar crystal;
    public GameObject attack_axe;
    public GameObject attack_crystal;

	public GameObjectGenerate gameobject_generate;
	public attribute player_attribute;

    public float map_scale_x = 500;
    public float map_scale_z = 500;
    public float wait_time = 5f;
    public float Base_transfer_time = 10f;
    public float Additional_transfer_time = 50f;
    public float TotalRounds = 3;
    public float DamagePerSecond = 10f;

    public Vector3 SafeCenter;
    public float SafeScale = 1;
    public float scale_factor = 3;
    public float timer = -1f;
    public bool IsWaiting = false;
    public float rounds = 0;

    private float DeltaQuad1;
    private float DeltaQuad2;
    private float DeltaQuad3;
    private float DeltaQuad4;
    private float boss_transition_timer = -1.0f;
    private bool WinFlag = false;

	private float boss_length = 2.2f;
	private float boss_width = 2.2f;

    // Use this for initialization
    void Start () 
	{
		//read the attribute of player
		//player_attribute.ReadPlayerAttribute ();



		//player_attribute.SaveAttributeInFile ();

		//player_attribute.ReadAttributeFromFile ();

		Debug.Log (player_attribute.GetSaveTimeFromFile ());
		Debug.Log (player_attribute.GetLevelOfPlayerFromFile ().ToString ());
		Debug.Log (player_attribute.GetLevelOfGameFromFile ().ToString ());
		Debug.Log (player_attribute.GetGoldOfPlayerFromFile ().ToString ());
		Debug.Log(player_attribute.GetAttributeFromFile ("gold"));
		Debug.Log(player_attribute.GetAttributeFromFile ("Level",0));
		Debug.Log(player_attribute.GetAttributeFromFile ("level_num"));

        try
        {
            int _charac = PlayerPrefs.GetInt("Character");
            if (_charac == 1)
            {
                player.transform.Find("axe_armor").gameObject.SetActive(false);
                player.transform.Find("axe_base").gameObject.SetActive(false);
                player.transform.Find("axe_belt").gameObject.SetActive(false);
                player.transform.Find("axe_hair").gameObject.SetActive(false);
                player.transform.Find("root").gameObject.SetActive(false);

                player.transform.Find("crystal_maiden_arms").gameObject.SetActive(true);
                player.transform.Find("crystal_maiden_back").gameObject.SetActive(true);
                player.transform.Find("crystal_maiden_base").gameObject.SetActive(true);
                player.transform.Find("crystal_maiden_head").gameObject.SetActive(true);
                player.transform.Find("crystal_maiden_shoulder").gameObject.SetActive(true);
                player.transform.Find("crystal_maiden_shoulder_bow").gameObject.SetActive(true);
                player.transform.Find("Root_0").gameObject.SetActive(true);

                player.GetComponent<Animator>().avatar = crystal;
                player.transform.Find("Attack_area").gameObject.SetActive(false);
                player.GetComponent<ThirdPersonUserControl>().attack_object = attack_crystal;
                player.GetComponent<attribute>().ZhiYe = "法师";
				if(player_attribute.level_num == 1)
				{
                	player.GetComponent<attribute>().ATK = 50 * 5;
                	player.GetComponent<attribute>().DEF = 5 * 5;
                	player.GetComponent<attribute>().ATKGrowth = 3f * 5f;
                	player.GetComponent<attribute>().DEFGrowth = 0.2f * 5f;
                	player.GetComponent<attribute>().HPGrowth = 10f;
                	player.GetComponent<attribute>().FireRate = 0.8f;
                	player.GetComponent<ThirdPersonCharacter>().m_MoveSpeedMultiplier = 1.2f;
					player_attribute.AttributeInit();
				}

            }
            if (_charac == 0)
            {
                player.transform.Find("axe_armor").gameObject.SetActive(true);
                player.transform.Find("axe_base").gameObject.SetActive(true);
                player.transform.Find("axe_belt").gameObject.SetActive(true);
                player.transform.Find("axe_hair").gameObject.SetActive(true);
                player.transform.Find("root").gameObject.SetActive(true);

                player.transform.Find("crystal_maiden_arms").gameObject.SetActive(false);
                player.transform.Find("crystal_maiden_back").gameObject.SetActive(false);
                player.transform.Find("crystal_maiden_base").gameObject.SetActive(false);
                player.transform.Find("crystal_maiden_head").gameObject.SetActive(false);
                player.transform.Find("crystal_maiden_shoulder").gameObject.SetActive(false);
                player.transform.Find("crystal_maiden_shoulder_bow").gameObject.SetActive(false);
                player.transform.Find("Root_0").gameObject.SetActive(false);

                player.GetComponent<Animator>().avatar = axe;
                player.transform.Find("Attack_area").gameObject.SetActive(true);
                player.GetComponent<ThirdPersonUserControl>().attack_object = attack_axe;
                player.GetComponent<attribute>().ZhiYe = "战士";
				if(player_attribute.level_num == 1)
				{
					player.GetComponent<attribute>().ATK = 30 * 5;
                	player.GetComponent<attribute>().DEF = 10 * 5;
                	player.GetComponent<attribute>().ATKGrowth = 1.5f * 5f;
                	player.GetComponent<attribute>().DEFGrowth = 1f * 5f;
                	player.GetComponent<attribute>().HPGrowth = 20f;
                	player.GetComponent<attribute>().FireRate = 0.6f;
                	player.GetComponent<ThirdPersonCharacter>().m_MoveSpeedMultiplier = 1.5f;
					player_attribute.AttributeInit();
				}

            }
        }
        catch { }

        quad1.transform.position = new Vector3(0f, 0.02f, map_scale_z / 2);
        quad2.transform.position = new Vector3(map_scale_x / 2, 0.02f, 0f);
        quad3.transform.position = new Vector3(map_scale_x - 0f, 0.02f, map_scale_z / 2);
        quad4.transform.position = new Vector3(map_scale_x / 2, 0.02f, map_scale_z - 0f);

        quad1.transform.localScale = new Vector3(0f, map_scale_z, 1f);
        quad2.transform.localScale = new Vector3(0f, map_scale_x, 1f);
        quad3.transform.localScale = new Vector3(0f, map_scale_z, 1f);
        quad4.transform.localScale = new Vector3(0f, map_scale_x, 1f);

    }
	void Update() { }

	// Update is called once per frame
	void FixedUpdate () 
	{
        if (WinFlag)
            return;
        
        if ((Boss.transform.position - player.transform.position).sqrMagnitude < 10000)
            Boss_Slider.SetActive(true);
        else
            Boss_Slider.SetActive(false);
        Boss.transform.Find("Boss Partical").Rotate(0,0, 20 * Time.fixedDeltaTime);
       // Boss.transform.Find("Particle System").Rotate()
        if (player.transform.position.x < 2 * quad1.transform.position.x ||
                player.transform.position.x > 2 * quad3.transform.position.x - map_scale_x ||
                player.transform.position.z < 2 * quad2.transform.position.z ||
                player.transform.position.z > 2 * quad4.transform.position.z - map_scale_z)
            player.GetComponent<attribute>().update_HP(-DamagePerSecond * Time.deltaTime);
        if (timer > 0)
            timer -= Time.fixedDeltaTime;
        if (boss_transition_timer > 0)
        {
            boss_transition_timer -= Time.fixedDeltaTime;
            if(boss_transition_timer>3)
            {
                Boss.transform.Find("Boss Partical").gameObject.SetActive(false);
                Boss.GetComponent<Collider>().enabled = false;
                Boss.GetComponent<MeshRenderer>().materials[0].color =
                    Color.Lerp(Boss.GetComponent<MeshRenderer>().material.color, new Color(Boss.GetComponent<MeshRenderer>().material.color.r,Boss.GetComponent<MeshRenderer>().material.color.g, Boss.GetComponent<MeshRenderer>().material.color.b, 0f), Time.fixedDeltaTime);
            }  
            if ( boss_transition_timer <3)
            {
                //Boss.transform.position = SafeCenter + new Vector3(0f, 3.9f, 0f);

				Vector3 t = new Vector3(SafeCenter.x,3.9f,SafeCenter.z);
				while (gameobject_generate.IsCoincide (new GameObjectGenerate.GameObjectNode (t.x, t.y, t.z, boss_length, boss_width, 0)))
				{
					t = t + new Vector3 (UnityEngine.Random.Range (-4f * boss_length, 4f * boss_length), 0, UnityEngine.Random.Range (-4f * boss_width, 4f * boss_width));
				}
				Boss.transform.position = t;



                Boss.GetComponent<MeshRenderer>().materials[0].color =
                    Color.Lerp(Boss.GetComponent<MeshRenderer>().material.color, new Color(Boss.GetComponent<MeshRenderer>().material.color.r, Boss.GetComponent<MeshRenderer>().material.color.g, Boss.GetComponent<MeshRenderer>().material.color.b, 1f), Time.fixedDeltaTime);
            }
            if (boss_transition_timer < 0)
            {
                Boss.transform.Find("Boss Partical").gameObject.SetActive(true);
                Boss.GetComponent<Collider>().enabled = true;
            }
        }
        if (Boss.GetComponent<attribute>().ifAlive == false)
        {
            WinFlag = true;
            ui_text.text = "You Win";
            Boss_Slider.SetActive(false);
            //Boss.transform.Find("Boss Partical").gameObject.SetActive(false);
            //Boss.transform.Find("Died Partical").gameObject.SetActive(true);
            quad1.transform.position = new Vector3(0f, 0.02f, map_scale_z / 2);
            quad2.transform.position = new Vector3(map_scale_x / 2, 0.02f, 0f);
            quad3.transform.position = new Vector3(map_scale_x - 0f, 0.02f, map_scale_z / 2);
            quad4.transform.position = new Vector3(map_scale_x / 2, 0.02f, map_scale_z - 0f);

            quad1.transform.localScale = new Vector3(0f, map_scale_z, 1f);
            quad2.transform.localScale = new Vector3(0f, map_scale_x, 1f);
            quad3.transform.localScale = new Vector3(0f, map_scale_z, 1f);
            quad4.transform.localScale = new Vector3(0f, map_scale_x, 1f);

            player.GetComponent<attribute>().update_HP(player.GetComponent<attribute>().HP_max);
            shop.transform.position = Boss.transform.position;
            shop.SetActive(true);

            //event when success

			//player_attribute.SavePlayerAttribute();

            return;
        }

        else
        {
            if(timer<=0)
            {
                if(!IsWaiting)
                {
                    rounds++;
                    SafeCenter = new Vector3(UnityEngine.Random.Range(2* quad1.transform.position.x, 2*quad3.transform.position.x - map_scale_x),
                        0,
                        UnityEngine.Random.Range(2 * quad2.transform.position.z, 2 * quad4.transform.position.z - map_scale_z));
                    SafeScale *= 2;
                    float _totalframes = (Base_transfer_time+Additional_transfer_time/rounds) / Time.deltaTime;
                    DeltaQuad1 = (SafeCenter.x - 2 * quad1.transform.position.x) / _totalframes / scale_factor;
                    DeltaQuad2 = (SafeCenter.z - 2 * quad2.transform.position.z) / _totalframes / scale_factor;
                    DeltaQuad3 = (2 * quad3.transform.position.x - map_scale_x - SafeCenter.x) / _totalframes / scale_factor;
                    DeltaQuad4 = (2 * quad4.transform.position.z - map_scale_z - SafeCenter.z) / _totalframes / scale_factor;
                    timer = wait_time;
                    IsWaiting = true;

                    // Transition of Boss
                    boss_transition_timer = 6f;
                }
                else
                {
                    timer = (Base_transfer_time + Additional_transfer_time / rounds);
                    IsWaiting = false;
                }
            }
            // TBD: else here?
            ui_text.text = (IsWaiting ? "Safe Area Will Reduce in " : "Safe Area Reductin Ends in ") + ((int)timer + 1).ToString();
            if (!IsWaiting)
            {
                quad1.transform.position = quad1.transform.position + new Vector3(DeltaQuad1/2, 0f, 0f);
                quad2.transform.position = quad2.transform.position + new Vector3(0f, 0f, DeltaQuad2/2);
                quad3.transform.position = quad3.transform.position + new Vector3(-DeltaQuad3/2, 0f, 0f);
                quad4.transform.position = quad4.transform.position + new Vector3(0f, 0f, -DeltaQuad4/2);

                quad1.transform.localScale = quad1.transform.localScale + new Vector3(DeltaQuad1, 0f, 0f);
                quad2.transform.localScale = quad2.transform.localScale + new Vector3(DeltaQuad2, 0f, 0f);
                quad3.transform.localScale = quad3.transform.localScale + new Vector3(DeltaQuad3, 0f, 0f);
                quad4.transform.localScale = quad4.transform.localScale + new Vector3(DeltaQuad4, 0f, 0f);

            }
        }
	}
}
