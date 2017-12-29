using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.UI;

public class GameObjectGenerate : MonoBehaviour 
{
	public GameObject player;
	public GameObject enemy;
	public GameObject enemy_attack_area;
	public GameObject itemHP;
	public GameObject itemCoin;
    public GameObject Congratulation;

	public GameObject RockHill_1;
	public GameObject RockHill_2;
	public GameObject RockHill_3;
	public GameObject RockHill_4;
	public GameObject RockHill_5;

	public GameObject Moutain_1;
	public GameObject Moutain_2;
	public GameObject Moutain_3;
	public GameObject Moutain_4;
	public GameObject Moutain_5;

	public GameObject Tree_1;
	public GameObject Tree_2;
	public GameObject Tree_3;


	private float map_x = 500f;
	private float map_z = 500f;

	private float edge_width = 10f;

	private float times = 5f;

	private float tree_length = 4f;
	private float tree_width = 4f;
	private float rock_hill1_length = 2f;
	private float rock_hill1_width = 2f;
	private float rock_hill3_length = 5.5f;
	private float rock_hill3_width = 12f;
	private float rock_hill4_length = 9f;
	private float rock_hill4_width = 9f;

	private float moutain_length = 10f;
	private float moutain_width = 15f;
	private float enemy_length = 0.5f;
	private float enemy_width = 0.5f;
	private float HP_length = 0.4f;
	private float HP_width = 0.4f;

	private float rock_area_fraction_low = 1 / 6f;
	private float rock_area_fraction_high = 5 / 6f;

	public struct GameObjectNode
	{
		public float x;
		public float y;
		public float z;
		public float length;
		public float width;
		public float height;
		public GameObjectNode(float _x,float _y,float _z,float _length,float _width,float _height)
		{
			this.x = _x;
			this.y = _y;
			this.z = _z;
			this.length = _length;
			this.width = _width;
			this.height = _height;
		}
		public static bool operator==(GameObjectNode lnode,GameObjectNode rnode)
		{
			if (lnode.x == rnode.x && lnode.y == rnode.y && lnode.z == rnode.z 
				&& lnode.length == rnode.length && lnode.width == rnode.width && lnode.height == rnode.height)
				return true;
			else
				return false;
		}
		public static bool operator!=(GameObjectNode lnode,GameObjectNode rnode)
		{
			return !(lnode == rnode);
		}
	};

	LinkedList<GameObjectNode> GameObjectList = new LinkedList<GameObjectNode>();

	public bool IsCoincide(GameObjectNode temp)
	{
		LinkedListNode<GameObjectNode> current = GameObjectList.First;
		while (current != null)
		{
			if (Mathf.Abs (current.Value.x - temp.x) < (current.Value.length + temp.length) / 2
			   && Mathf.Abs (current.Value.z - temp.z) < (current.Value.length + temp.length) / 2)
				return true;
			current = current.Next;
		}
		return false;
	}

	void ListPrint()
	{
		LinkedListNode<GameObjectNode> current = GameObjectList.First;
		Debug.Log (GameObjectList.Count.ToString() + "\t");
		while (current != null)
		{
			Debug.Log (current.Value.x.ToString () + "\t" + current.Value.z.ToString ());
			current = current.Next;
		}
	}




	// Use this for initialization
	void Start () 
	{
        int game_level = player.GetComponent<attribute>().level_num;
        if (game_level == 5)
            Congratulation.SetActive(true);
        //generate gameobject
        for (int j = 0; j < 3; j++)
		{
			GenetateEnemy (60);
			GenetateHP (10);
            GenetateCoins(15);

            GenerateTree (2);
			GenerateRockHill (3);
			GenerateMoutain (3);
		}


		/*
		{
			//test of the LinkedList,you can ignore it

			LinkedListNode<GameObjectNode> node1 = new LinkedListNode<GameObjectNode> (new GameObjectNode (20, 20, 0, 2, 0, 2));
			LinkedListNode<GameObjectNode> node2 = new LinkedListNode<GameObjectNode> (new GameObjectNode (30, 30, 0, 2, 0, 2));
			LinkedListNode<GameObjectNode> node3 = new LinkedListNode<GameObjectNode> (new GameObjectNode (40, 40, 0, 2, 0, 2));
			LinkedListNode<GameObjectNode> node4 = new LinkedListNode<GameObjectNode> (new GameObjectNode (50, 50, 0, 2, 0, 2));
			LinkedListNode<GameObjectNode> node5 = new LinkedListNode<GameObjectNode> (new GameObjectNode (60, 60, 0, 2, 0, 2));
			LinkedListNode<GameObjectNode> node6 = new LinkedListNode<GameObjectNode> (new GameObjectNode (70, 70, 0, 2, 0, 2));

			GameObjectList.AddLast (node1);
			GameObjectList.AddLast (node2);
			GameObjectList.AddLast (node3);
			GameObjectList.AddLast (node4);
			GameObjectList.AddLast (node5);
			GameObjectList.AddLast (node6);

			ListPrint ();

			LinkedListNode<GameObjectNode> test_node1 = new LinkedListNode<GameObjectNode> (new GameObjectNode (20, 20, 0, 2, 0, 2));
			LinkedListNode<GameObjectNode> test_node2 = new LinkedListNode<GameObjectNode> (new GameObjectNode (30, 30, 0, 2, 0, 2));
			LinkedListNode<GameObjectNode> test_node3 = new LinkedListNode<GameObjectNode> (new GameObjectNode (40, 40, 0, 2, 0, 2));
			LinkedListNode<GameObjectNode> test_node4 = new LinkedListNode<GameObjectNode> (new GameObjectNode (50, 50, 0, 2, 0, 2));
			LinkedListNode<GameObjectNode> test_node5 = new LinkedListNode<GameObjectNode> (new GameObjectNode (60, 60, 0, 2, 0, 2));
			LinkedListNode<GameObjectNode> test_node6 = new LinkedListNode<GameObjectNode> (new GameObjectNode (70, 70, 0, 2, 0, 2));

			Debug.Log (IsCoincide (new GameObjectNode (20, 20, 0, 2, 0, 2)));
			GameObjectList.Remove (new GameObjectNode (20, 20, 0, 2, 0, 2));
			Debug.Log (IsCoincide (new GameObjectNode (20, 20, 0, 2, 0, 2)));

			ListPrint ();
		}
		*/

		//
		transform.Find ("/Canvas").gameObject.GetComponent<EnterWindow> ().InitFunction ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	//generate count enemys
	void GenetateEnemy(int count)
	{
		int game_level = player.GetComponent<attribute> ().level_num;
		Vector3 t;
		for (int i = 0; i < count; i++)
		{
			while (true)
			{
				t = new Vector3 (UnityEngine.Random.Range (edge_width, map_x - edge_width), 
					0, UnityEngine.Random.Range (edge_width, map_x - edge_width));
				if (!IsCoincide (new GameObjectNode (t.x, t.y, t.z, enemy_length, enemy_width, 0)))
				{
					GameObjectList.AddLast (new GameObjectNode (t.x, t.y, t.z, enemy_length, enemy_width, 0));
					break;
				}
			}
			GameObject temp_enemy = Instantiate (enemy, t, new Quaternion ());
			temp_enemy.GetComponent<AICharacterControl> ().target = player.transform;
			temp_enemy.GetComponent<attribute> ().Level = game_level;
			temp_enemy.GetComponent<attribute> ().EnemyUpdate ();

			/*
			temp_enemy.GetComponent<attribute> ().ATK = 20f * game_level + 5f;
			temp_enemy.GetComponent<attribute> ().DEF = 10f * game_level + 5f;
			temp_enemy.GetComponent<attribute> ().HP = temp_enemy.GetComponent<attribute> ().HP_max = 100f * game_level + 30f;
			temp_enemy.GetComponent<attribute> ().DropGold = 10f * game_level + 10f;
			temp_enemy.GetComponent<attribute> ().DropEXP = 300f * game_level + 200f;
			temp_enemy.GetComponent<attribute> ().FireRate = 0.2f * game_level + 1f;
			*/

			GameObject temp_range = Instantiate (enemy_attack_area, t, new Quaternion ());
			temp_range.GetComponent<attack_range> ().Target = temp_enemy;
		}
	}

	//generate count HPs
	void GenetateHP(int count)
	{
        int game_level = player.GetComponent<attribute>().level_num;
        Vector3 t;
		for (int i = 0; i < count; i++)
		{
			while (true)
			{
				t = new Vector3 (UnityEngine.Random.Range (edge_width, map_x - edge_width), 
					0.6f, UnityEngine.Random.Range (edge_width, map_x - edge_width));
				if (!IsCoincide (new GameObjectNode (t.x, t.y, t.z, HP_length, HP_width, 0)))
				{
					GameObjectList.AddLast (new GameObjectNode (t.x, t.y, t.z, HP_length, HP_width, 0));
					break;
				}
			}
			GameObject temp_HP = Instantiate (itemHP, t, Quaternion.Euler(-90, 0, 0));
			temp_HP.GetComponent<pickGoods> ().value = (int)UnityEngine.Random.Range (-5+ game_level * 15, 25+ game_level * 25);
		}
	}

    void GenetateCoins(int count)
    {
        int game_level = player.GetComponent<attribute>().level_num;
        Vector3 t;
        for (int i = 0; i < count; i++)
        {
            while (true)
            {
                t = new Vector3(UnityEngine.Random.Range(edge_width, map_x - edge_width),
                    1f, UnityEngine.Random.Range(edge_width, map_x - edge_width));
                if (!IsCoincide(new GameObjectNode(t.x, t.y, t.z, HP_length, HP_width, 0)))
                {
                    GameObjectList.AddLast(new GameObjectNode(t.x, t.y, t.z, HP_length, HP_width, 0));
                    break;
                }
            }
            GameObject temp_HP = Instantiate(itemCoin, t, Quaternion.Euler(-90, 0, 0));
            temp_HP.GetComponent<pickGoods>().value = (int)UnityEngine.Random.Range(-10+ game_level * 20, 30 + game_level * 20);
        }
    }

    //it will generate 2*3*count trees
    void GenerateTree(int count)
	{
		GameObject[] tree = new GameObject[3];
		GameObject[] temp_tree = new GameObject[3];
		tree [0] = Tree_1;
		tree [1] = Tree_2;
		tree [2] = Tree_3;
		Vector3 t;

		//completely random
		for (int j = 0; j < count; j++)
		{
			for (int i = 0; i < 3; i++)
			{
				while (true)
				{
					t = new Vector3 (UnityEngine.Random.Range (edge_width, map_x - edge_width), 
						0, UnityEngine.Random.Range (edge_width, map_x - edge_width));
					if (!IsCoincide (new GameObjectNode (t.x, t.y, t.z, tree_length, tree_width, 0)))
					{
						GameObjectList.AddLast (new GameObjectNode (t.x, t.y, t.z, tree_length, tree_width, 0));
						break;
					}
				}
				temp_tree [i] = Instantiate (tree [i], t, new Quaternion ());
			}
		}

		//generate trees together 
		while (true)
		{
			t = new Vector3 (UnityEngine.Random.Range (edge_width + tree_length * 3 * count, map_x - edge_width - tree_length * 3 * count), 
				0, UnityEngine.Random.Range (edge_width + tree_length * 3 * count, map_x - edge_width - tree_length * 3 * count));
			if (!IsCoincide (new GameObjectNode (t.x, t.y, t.z, tree_length * 3 * count, tree_width, 0)) 
				||!IsCoincide (new GameObjectNode (t.x, t.y, t.z, tree_length, tree_width * 3 * count, 0)) )
				break;
		}
		for (int j = 0; j < count; j++)
		{
			for (int i = 0; i < 3; i++)
			{
				while (true)
				{
					t = new Vector3 (t.x + UnityEngine.Random.Range (0, tree_length / 2), 
						0, t.z + UnityEngine.Random.Range (0, tree_width / 2));
					if (!IsCoincide (new GameObjectNode (t.x, t.y, t.z, tree_length, tree_width, 0)))
					{
						GameObjectList.AddLast (new GameObjectNode (t.x, t.y, t.z, tree_length, tree_width, 0));
						break;
					}
				}
				temp_tree [i] = Instantiate (tree [i], t, new Quaternion ());
			}
		}
	}

	//generate 5*count rockhills
	void GenerateRockHill (int count)
	{
		GameObject[] rock = new GameObject[5];
		GameObject[] temp_rock = new GameObject[5];
		rock [0] = RockHill_1;
		rock [1] = RockHill_2;
		rock [2] = RockHill_3;
		rock [3] = RockHill_4;
		rock [4] = RockHill_5;
		Vector3 t;
		for (int j = 0; j < count; j++)
		{
			for (int i = 0; i < 2; i++)
			{
				while (true)
				{
					t = new Vector3 (UnityEngine.Random.Range (edge_width + rock_area_fraction_low * map_x, map_x * rock_area_fraction_high - edge_width),
						0, UnityEngine.Random.Range (edge_width + map_x * rock_area_fraction_low, map_x * rock_area_fraction_high - edge_width));
					if (!IsCoincide (new GameObjectNode (t.x, t.y, t.z, rock_hill1_length, rock_hill1_width, 0)))
					{
						GameObjectList.AddLast (new GameObjectNode (t.x, t.y, t.z, rock_hill1_length, rock_hill1_width, 0));
						break;
					}
				}
				temp_rock [i] = Instantiate (rock [i], t, new Quaternion ());
			}
			while (true)
			{
				t = new Vector3 (UnityEngine.Random.Range (edge_width + map_x * rock_area_fraction_low, map_x * rock_area_fraction_high - edge_width), 
					0, UnityEngine.Random.Range (edge_width + map_x * rock_area_fraction_low, map_x * rock_area_fraction_high - edge_width));
				if (!IsCoincide (new GameObjectNode (t.x, t.y, t.z, rock_hill3_length, rock_hill3_width, 0)))
				{
					GameObjectList.AddLast (new GameObjectNode (t.x, t.y, t.z, rock_hill3_length, rock_hill3_width, 0));
					break;
				}
				temp_rock [2] = Instantiate (rock [2], t, new Quaternion ());
			}
			for (int i = 3; i < 5; i++)
			{
				while (true)
				{
					t = new Vector3 (UnityEngine.Random.Range (edge_width + map_x * rock_area_fraction_low, map_x * rock_area_fraction_high - edge_width),
						0, UnityEngine.Random.Range (edge_width + map_x * rock_area_fraction_low, map_x * rock_area_fraction_high - edge_width));
					if (!IsCoincide (new GameObjectNode (t.x, t.y, t.z, rock_hill4_length, rock_hill4_width, 0)))
					{
						GameObjectList.AddLast (new GameObjectNode (t.x, t.y, t.z, rock_hill4_length, rock_hill4_width, 0));
						break;
					}
				}
				temp_rock [i] = Instantiate (rock [i], t, new Quaternion ());
			}
		}
	}

	//generate 5*count moutain
	void GenerateMoutain(int count)
	{
		GameObject[] moutain = new GameObject[5];
		GameObject[] temp_moutain = new GameObject[5];
		moutain [0] = Moutain_1;
		moutain [1] = Moutain_2;
		moutain [2] = Moutain_3;
		moutain [3] = Moutain_4;
		moutain [4] = Moutain_5;
		Vector3 t;
		for (int j = 0; j < count; j++)
		{
			for (int i = 0; i < 5; i++)
			{
				while (true)
				{
					t = new Vector3 (UnityEngine.Random.Range (edge_width + map_x * rock_area_fraction_low, map_x * rock_area_fraction_high - edge_width),
						0, UnityEngine.Random.Range (edge_width + map_x * rock_area_fraction_low, map_x * rock_area_fraction_high - edge_width));
					if (!IsCoincide (new GameObjectNode (t.x, t.y, t.z, moutain_length, moutain_width, 0)))
					{
						GameObjectList.AddLast (new GameObjectNode (t.x, t.y, t.z, moutain_length, moutain_width, 0));
						break;
					}
				}
				temp_moutain [i] = Instantiate (moutain [i], t, new Quaternion ());
			}
		}
	}
}
