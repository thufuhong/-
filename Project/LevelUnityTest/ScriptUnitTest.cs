using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityStandardAssets.Characters.ThirdPerson;

public class ScriptUnitTest {

	/// <summary>
	/// AICharacterControl_Test
	/// </summary>
	/// <returns>The character control test with enumerator passes.</returns>
	[UnityTest]
	public IEnumerator AICharacterControl_TestWithEnumeratorPasses() {
		UnityEngine.SceneManagement.SceneManager.LoadScene("Untitled");
		yield return null;
		GameObject enemy = temp.enemy;
		/// init gamobject
		Assert.AreNotEqual (enemy, null);
		enemy.transform.position = new Vector3(12, 0, 15);
		enemy.transform.rotation = Quaternion.identity;
		yield return null;
		AICharacterControl testcs = enemy.GetComponent<AICharacterControl> ();
		Assert.NotNull (testcs);
		Assert.IsFalse (testcs.active);
		Assert.NotNull (testcs.agent);
		Vector3 pos = testcs.transform.position;
		yield return new WaitForFixedUpdate();
		/// not move than 0.5
		Vector3 curpos = testcs.transform.position;
		Assert.Greater (0.5, Vector3.Magnitude (pos - curpos));
		/// CoolDown flush
		testcs.CoolDownTime = 1.0f;
		yield return new WaitForFixedUpdate();
		Assert.Greater (1.0f, testcs.CoolDownTime);
		yield return null;
	}


	/// <summary>
	/// attribute_Test
	/// </summary>
	/// <returns>The character control test with enumerator passes.</returns>
	[UnityTest]
	public IEnumerator attribute_TestWithEnumeratorPasses() {
		UnityEngine.SceneManagement.SceneManager.LoadScene("Untitled");
		yield return null;
		GameObject enemy = temp.enemy;
		Assert.AreNotEqual (enemy, null);
		enemy.transform.position = new Vector3(12, 0, 15);
		enemy.transform.rotation = Quaternion.identity;
		yield return null;
		attribute testcs = enemy.GetComponent<attribute> ();
		Assert.IsTrue (testcs.ifAlive);
		/// test update_EXP

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
		yield return new WaitForSeconds(2);
		Debug.Log (testcs.HP);
		Debug.Log (testcs.ifAlive);
		Assert.IsTrue (enemy == null);
	}
}
