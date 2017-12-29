using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCharacter : MonoBehaviour {
	public static GameObject enemy = null;
	public static GameObject hero = null;
	public static bool istest = false;

	// Use this for initialization
	void Start () {
		Debug.Log ("awake");
		reset ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!istest)
			reset ();
	}

	public static void reset()
	{
		if (enemy == null) enemy = setEnemy ();
		if (hero == null) hero = setHero ();
	}

	public static GameObject setEnemy()
	{
		return InitialMethod.getGameObjectByPrefab ("crystal_maiden_econ enemy");
	}

	public static GameObject setHero()
	{
		return InitialMethod.getGameObjectByPrefab ("axe_econ");
	}
}
