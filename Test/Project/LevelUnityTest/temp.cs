using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temp : MonoBehaviour {
	public static GameObject enemy;
	public static GameObject hero;
	// Use this for initialization
	void Awake () {
		enemy = setEnemy ();
		hero = setHero ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public GameObject setEnemy()
	{
		return InitialMethod.getGameObjectByPrefab ("crystal_maiden_econ enemy");
	}

	public GameObject setHero()
	{
		return InitialMethod.getGameObjectByPrefab ("axe_econ");
	}
}
