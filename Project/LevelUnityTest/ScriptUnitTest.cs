using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityStandardAssets.Characters.ThirdPerson;
using System.Runtime.InteropServices;

public class ScriptUnitTest {


	/// <summary>
	/// AICharacterControl_Test
	/// </summary>
	/// <returns>The character control test with enumerator passes.</returns>
	[UnityTest]
	public IEnumerator AICharacterControl_TestWithEnumeratorPasses() {
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Untitled");
		SetCharacter.istest = true;
		yield return null;
		GameObject enemy;
		enemy = SetCharacter.enemy;

		/// init GameObject test
		Assert.AreNotEqual (enemy, null);
		enemy.transform.position = new Vector3(12, 0, 15);
		enemy.transform.rotation = Quaternion.identity;

		/// init script test
		yield return null;
		AICharacterControl testcs = enemy.GetComponent<AICharacterControl> ();
		Assert.NotNull (testcs);
		Assert.IsFalse (testcs.active);
		Assert.NotNull (testcs.agent);

		/// move test
		/// not move than 0.5
		Vector3 pos = testcs.transform.position;
		yield return new WaitForFixedUpdate();
		Vector3 curpos = testcs.transform.position;
		Assert.Greater (0.5, Vector3.Magnitude (pos - curpos));

		/// CoolDown flush test
		testcs.CoolDownTime = 1.0f;
		yield return new WaitForFixedUpdate();
		Assert.Greater (1.0f, testcs.CoolDownTime);
		yield return null;
		SetCharacter.istest = false;
	}


	/// <summary>
	/// attribute
	/// </summary>
	/// <returns>The attribute_test with enumerator passes.</returns>
	[UnityTest]
	public IEnumerator attribute_Test_AttributeInit_WithEnumeratorPasses() {
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Untitled");
		SetCharacter.istest = true;
		yield return null;

		/// init GameObject test
		GameObject enemy = SetCharacter.enemy;
		Assert.AreNotEqual (enemy, null);
		enemy.transform.position = new Vector3(12, 0, 15);
		enemy.transform.rotation = Quaternion.identity;

		/// init script test
		yield return null;
		attribute testcs = enemy.GetComponent<attribute> ();
		Assert.IsTrue (testcs.ifAlive);

		SetCharacter.istest = false;
	}

	[UnityTest]
	public IEnumerator attribute_Test_update_EXP_WithEnumeratorPasses() {
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Untitled");
		SetCharacter.istest = true;
		yield return null;

		/// init GameObject test
		GameObject hero = SetCharacter.hero;
		Assert.AreNotEqual (hero, null);
		hero.transform.position = new Vector3(12, 0, 15);
		hero.transform.rotation = Quaternion.identity;

		/// init script test
		yield return null;
		attribute testcs = hero.GetComponent<attribute> ();
		Assert.IsTrue (testcs.ifAlive);

		/// test update_EXP
		testcs.EXP = 0;
		testcs.EXPForLevelUp = 100;
		testcs.Level = 1;
		testcs.update_EXP (99);
		Assert.GreaterOrEqual (1e-5, Math.Abs(testcs.EXP - 99));
		// level up
		testcs.update_EXP (15);
		Assert.AreEqual (testcs.Level, 2);
		// 99 + 15 - 100 = 14
		Assert.GreaterOrEqual (1e-5, Math.Abs(testcs.EXP - 14));
		testcs.update_EXP (1000);
		// 1000 + 14 --> 1014 --> up(10) + 14 --> level(12)
		Assert.AreEqual (testcs.Level, 12);
		// drop EXP
		testcs.update_EXP (-1000);
		// 14 - 1000 --> 0, level don't be changed
		Assert.AreEqual (testcs.Level, 12);
		Assert.AreEqual (testcs.EXP, 0);

		SetCharacter.istest = false;
	}

	[UnityTest]
	public IEnumerator attribute_Test_update_HP_WithEnumeratorPasses() {
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Untitled");
		SetCharacter.istest = true;
		yield return null;

		/// init GameObject test
		GameObject hero = SetCharacter.hero;
		Assert.AreNotEqual (hero, null);
		hero.transform.position = new Vector3(12, 0, 15);
		hero.transform.rotation = Quaternion.identity;

		/// init script test
		yield return null;
		attribute testcs = hero.GetComponent<attribute> ();
		Assert.IsTrue (testcs.ifAlive);

		/// test hero
		/// test update_HP
		testcs.HP_max = testcs.HP = 100f;
		testcs.update_HP (-10f);
		// 100 - 10 = 90
		Assert.GreaterOrEqual (1e-5, Math.Abs(testcs.HP - 90f));
		testcs.update_HP (30f);
		// 90 + 30 = 120 (>HP_max) --> 100(HP_max)
		Assert.GreaterOrEqual (1e-5, Math.Abs(testcs.HP - 100f));
		// will dead
		testcs.update_HP (-110f);
		// wait it dead
		yield return new WaitForSeconds(1.5f);
		Debug.Log (testcs.HP);
		Debug.Log (testcs.ifAlive);
		Assert.IsTrue (hero != null);

		GameObject enemy = SetCharacter.enemy;
		Assert.AreNotEqual (enemy, null);
		enemy.transform.position = new Vector3(12, 0, 15);
		enemy.transform.rotation = Quaternion.identity;

		/// init script test
		yield return null;
		testcs = enemy.GetComponent<attribute> ();
		Assert.IsTrue (testcs.ifAlive);

		/// test enemy
		/// test update_HP
		testcs.HP_max = testcs.HP = 100f;
		testcs.update_HP (-10f);
		// 100 - 10 = 90
		Assert.GreaterOrEqual (1e-5, Math.Abs(testcs.HP - 90f));
		testcs.update_HP (30f);
		// 90 + 30 = 120 (>HP_max) --> 100(HP_max)
		Assert.GreaterOrEqual (1e-5, Math.Abs(testcs.HP - 100f));
		// will dead
		testcs.update_HP (-110f);
		// wait it dead
		yield return new WaitForSeconds(1.5f);
		Debug.Log (testcs.HP);
		Debug.Log (testcs.ifAlive);
		Assert.IsTrue (enemy == null);

		SetCharacter.istest = false;
	}

	[UnityTest]
	public IEnumerator attribute_Test_dropGoods_WithEnumeratorPasses() {
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Untitled");
		SetCharacter.istest = true;
		yield return null;

		/// init GameObject
		GameObject enemy = SetCharacter.enemy;
		Assert.AreNotEqual (enemy, null);
		enemy.transform.position = new Vector3(12, 0, 15);
		enemy.transform.rotation = Quaternion.identity;

		/// init script test
		yield return null;
		attribute testcs = enemy.GetComponent<attribute> ();
		Assert.IsTrue (testcs.ifAlive);

		/// test dropGood - coin
		testcs.DropGold = 100f;
		GameObject coin = testcs.dropGoods ("coin");
		Assert.NotNull (coin);
		Assert.NotNull (coin.GetComponent<pickGoods> ());
		Assert.GreaterOrEqual (1e-5, Math.Abs (coin.GetComponent<pickGoods> ().value - testcs.DropGold));

		SetCharacter.istest = false;
	}

	[UnityTest]
	public IEnumerator attribute_Test_update_gold_WithEnumeratorPasses() {
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Untitled");
		SetCharacter.istest = true;
		yield return null;

		/// init GameObject
		GameObject enemy = SetCharacter.enemy;
		Assert.AreNotEqual (enemy, null);
		enemy.transform.position = new Vector3(12, 0, 15);
		enemy.transform.rotation = Quaternion.identity;

		/// init script test
		yield return null;
		attribute testcs = enemy.GetComponent<attribute> ();
		Assert.IsTrue (testcs.ifAlive);

		/// test update_gold
		testcs.gold = 100f;
		testcs.update_gold (100f);
		Assert.GreaterOrEqual (1e-5, Math.Abs(testcs.gold - 200f));
		testcs.update_gold (-3f);
		Assert.GreaterOrEqual (1e-5, Math.Abs(testcs.gold - 197f));
		testcs.update_gold (-300f);
		Assert.GreaterOrEqual (1e-5, Math.Abs(testcs.gold));

		SetCharacter.istest = false;
	}

	[UnityTest]
	public IEnumerator attribute_Test_rw_file_WithEnumeratorPasses() {
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Untitled");
		SetCharacter.istest = true;
		yield return null;

		/// init GameObject
		GameObject hero = SetCharacter.hero;
		Assert.AreNotEqual (hero, null);
		hero.transform.position = new Vector3(12, 0, 15);
		hero.transform.rotation = Quaternion.identity;

		/// init script test
		yield return null;
		attribute testcs = hero.GetComponent<attribute> ();
		Assert.IsTrue (testcs.ifAlive);

		/// test SaveAttributeInFile
		/// test ReadAttributeFromFile
		testcs.gold = 99999f;
		testcs.SaveAttributeInFile ();
		testcs.gold = 0f;
		Assert.GreaterOrEqual (1e-5, Math.Abs(testcs.gold - 0f));
		testcs.ReadAttributeFromFile ();
		Assert.GreaterOrEqual (1e-5, Math.Abs(testcs.gold - 99999f));

		/// test GetLevelOfPlayerFromFile
		/// test GetGoldOfPlayerFromFile
		/// test GetTypeOfPlayerFromFile
		/// test GetLevelOfGameFromFile
		testcs.Level = 99999f;
		testcs.gold = 99999f;
		testcs.ZhiYe = "xx";
		testcs.level_num = 99999;
		testcs.SaveAttributeInFile ();
		testcs.Level = 0f;
		testcs.gold = 0f;
		testcs.ZhiYe = "000";
		testcs.level_num = 0;
		Assert.GreaterOrEqual (1e-5, Math.Abs(testcs.GetLevelOfPlayerFromFile () - 99999));
		Assert.GreaterOrEqual (1e-5, Math.Abs(testcs.GetGoldOfPlayerFromFile () - 99999));
		Assert.IsTrue ("xx".Equals (testcs.GetTypeOfPlayerFromFile ()));
		Assert.GreaterOrEqual (1e-5, Math.Abs(testcs.GetLevelOfGameFromFile () - 99999));

		/// init
		testcs.AttributeInit ();
		/// cover
		testcs.SaveAttributeInFile ();
		SetCharacter.istest = false;
	}

	/// <summary>
	/// ThirdPersonCharacter && AICharacterControl
	/// </summary>
	/// <returns>The test rw file with enumerator passes.</returns>
	[UnityTest]
	public IEnumerator ThirdPersonCharacter_AICharacterControl_AutoMove_WithEnumeratorPasses() {
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Untitled");
		SetCharacter.istest = true;
		yield return null;

		/// init GameObject
		GameObject enemy = SetCharacter.enemy;
		Assert.AreNotEqual (enemy, null);
		enemy.transform.position = new Vector3(12, 0, 15);
		enemy.transform.rotation = Quaternion.identity;

		/// init script test
		yield return null;
		ThirdPersonCharacter testcs = enemy.GetComponent<ThirdPersonCharacter> ();
		AICharacterControl ai = enemy.GetComponent<AICharacterControl> ();
		// unit vector uesd to test
		// init the pos of hero is near with enemy
		SetCharacter.hero.transform.LookAt (enemy.transform);
		Vector3 prepos = enemy.transform.position;
		Vector3 dir = SetCharacter.hero.transform.position - prepos;
		yield return new WaitForSeconds (1);
		// Debug.Log (enemy.transform.position);
		Vector3 dpos = enemy.transform.position - prepos;
		float length = Vector3.Magnitude (dpos);
		float dot = Vector3.Dot (dpos, dir.normalized);
		// line walking
		Assert.GreaterOrEqual (length * 1e-3, Math.Abs (length - dot));
		SetCharacter.istest = false;
	}

	/// <summary>
	/// ThirdPersonUserControl
	/// </summary>
	/// <returns></returns>
	[UnityTest]
	public IEnumerator ThirdPersonUserControl_Move_Test_WithEnumeratorPasses() {
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Untitled");
		SetCharacter.istest = true;
		yield return null;

		/// init GameObject
		GameObject hero = SetCharacter.hero;
		Assert.AreNotEqual (hero, null);
		hero.transform.position = new Vector3(12, 0, 15);
		hero.transform.rotation = Quaternion.identity;

		/// init script test
		yield return null;
		ThirdPersonUserControl testcs = hero.GetComponent<ThirdPersonUserControl> ();
		/// move -> forward
		Vector3 pos = hero.transform.position;
		// up arrow
		testcs.Operation_LRUD (0f, 100.0f, true);
		yield return null;yield return null;
		yield return null;yield return null;
		Assert.Greater(Vector3.Dot (hero.transform.forward, hero.transform.position - pos), 0.0);
		/// move -> rot 
		Vector3 forw = hero.transform.forward;
		testcs.Operation_LRUD (10.0f, 0.0f, true);
		Assert.Greater (Vector3.Cross (forw, hero.transform.forward).y, 0.0);

		SetCharacter.istest = false;
	}

	[UnityTest]
	public IEnumerator ThirdPersonUserControl_Attack_Test_WithEnumeratorPasses() {
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Untitled");
		SetCharacter.istest = true;
		yield return null;

		/// init GameObject
		GameObject hero = SetCharacter.hero;
		Assert.AreNotEqual (hero, null);
		hero.transform.position = new Vector3(12, 0, 15);
		hero.transform.rotation = Quaternion.identity;

		GameObject enemy = SetCharacter.enemy;
		Assert.AreNotEqual (enemy, null);
		enemy.transform.position = new Vector3(14, 0, 15);
		enemy.transform.rotation = Quaternion.identity;

		hero.transform.LookAt (enemy.transform);
		/// init script test
		yield return null;
		ThirdPersonUserControl testcs = hero.GetComponent<ThirdPersonUserControl> ();
		float hp = enemy.GetComponent<attribute> ().HP;
		Debug.Log (enemy.GetComponent<attribute> ().HP);
		testcs.Operation_J ();
		yield return null;yield return null;
		yield return null;yield return null;
		yield return null;yield return null;
		// damage
		Assert.Greater (hp, enemy.GetComponent<attribute> ().HP);

		SetCharacter.istest = false;
	}

	[UnityTest]
	public IEnumerator ThirdPersonUserControl_Skill_U_Test_WithEnumeratorPasses() {
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Untitled");
		SetCharacter.istest = true;
		yield return null;

		/// init GameObject
		GameObject hero = SetCharacter.hero;
		Assert.AreNotEqual (hero, null);
		hero.transform.position = new Vector3(12, 0, 15);
		hero.transform.rotation = Quaternion.identity;

		GameObject enemy = SetCharacter.enemy;
		Assert.AreNotEqual (enemy, null);
		enemy.transform.position = new Vector3(14, 0, 15);
		enemy.transform.rotation = Quaternion.identity;

		hero.transform.LookAt (enemy.transform);
		/// init script test
		yield return null;
		ThirdPersonUserControl testcs = hero.GetComponent<ThirdPersonUserControl> ();
		ThirdPersonCharacter messagea = hero.GetComponent<ThirdPersonCharacter> ();
		attribute messageb = hero.GetComponent<attribute> ();

		float presp = messagea.m_MoveSpeedMultiplier;
		/// test Skill1
		messageb.Skill_Level [0] = 10;
		testcs.Operation_U ();
		yield return null;
		Assert.Greater (messagea.m_MoveSpeedMultiplier, presp);

		SetCharacter.istest = false;
	}

	[UnityTest]
	public IEnumerator ThirdPersonUserControl_Skill_I_Test_WithEnumeratorPasses() {
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Untitled");
		SetCharacter.istest = true;
		yield return null;

		/// init GameObject
		GameObject hero = SetCharacter.hero;
		Assert.AreNotEqual (hero, null);
		hero.transform.position = new Vector3(12, 0, 15);
		hero.transform.rotation = Quaternion.identity;

		GameObject enemy = SetCharacter.enemy;
		Assert.AreNotEqual (enemy, null);
		enemy.transform.position = new Vector3(14, 0, 15);
		enemy.transform.rotation = Quaternion.identity;

		hero.transform.LookAt (enemy.transform);
		/// init script test
		yield return null;
		ThirdPersonUserControl testcs = hero.GetComponent<ThirdPersonUserControl> ();
		attribute message = hero.GetComponent<attribute> ();

		message.HP_max = 100f;
		message.HP = 20f;
		/// test Skill2
		message.Skill_Level [1] = 10;
		testcs.Operation_I ();
		yield return null;yield return null;
		yield return null;yield return null;
		yield return null;yield return null;
		Assert.Greater (message.HP, 20f);

		SetCharacter.istest = false;
	}

	[UnityTest]
	public IEnumerator ThirdPersonUserControl_Skill_O_Test_WithEnumeratorPasses() {
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Untitled");
		SetCharacter.istest = true;
		yield return null;

		/// init GameObject
		GameObject hero = SetCharacter.hero;
		Assert.AreNotEqual (hero, null);
		hero.transform.position = new Vector3(12, 0, 15);
		hero.transform.rotation = Quaternion.identity;

		GameObject enemy = SetCharacter.enemy;
		Assert.AreNotEqual (enemy, null);
		enemy.transform.position = new Vector3(14, 0, 15);
		enemy.transform.rotation = Quaternion.identity;

		hero.transform.LookAt (enemy.transform);
		/// init script test
		yield return null;
		ThirdPersonUserControl testcs = hero.GetComponent<ThirdPersonUserControl> ();
		attribute message = hero.GetComponent<attribute> ();
		float predef = message.DEF_bouns;
		/// test Skill3
		message.Skill_Level [2] = 10;
		testcs.Operation_O ();
		yield return null;yield return null;
		yield return null;yield return null;
		yield return null;yield return null;
		Assert.Greater (message.DEF_bouns, predef);

		SetCharacter.istest = false;
	}

	[UnityTest]
	public IEnumerator ThirdPersonUserControl_Skill_P_Test_WithEnumeratorPasses() {
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Untitled");
		SetCharacter.istest = true;
		yield return null;

		/// init GameObject
		GameObject hero = SetCharacter.hero;
		Assert.AreNotEqual (hero, null);
		hero.transform.position = new Vector3(12, 0, 15);
		hero.transform.rotation = Quaternion.identity;

		GameObject enemy = SetCharacter.enemy;
		Assert.AreNotEqual (enemy, null);
		enemy.transform.position = new Vector3(14, 0, 15);
		enemy.transform.rotation = Quaternion.identity;

		hero.transform.LookAt (enemy.transform);
		/// init script test
		yield return null;
		ThirdPersonUserControl testcs = hero.GetComponent<ThirdPersonUserControl> ();
		attribute message = enemy.GetComponent<attribute> ();
		float prehp = message.HP = message.HP_max = 100f;
		/// test Skill4
		message.Skill_Level [3] = 100;
		testcs.Operation_P ();
		yield return new WaitForSeconds (1.5f);
		Assert.Greater (prehp, message.HP);

		SetCharacter.istest = false;
	}
}
