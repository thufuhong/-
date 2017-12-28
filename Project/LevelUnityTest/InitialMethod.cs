using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class InitialMethod
{
	private static UnityEngine.Object getPrefab(string name)
	{
		return Resources.Load ("Prefabs/" + name);
	}
	public static GameObject getGameObjectByPrefab(string name)
	{
		UnityEngine.Object prefab = getPrefab (name);
		GameObject cur = GameObject.Instantiate (prefab, Vector3.zero, Quaternion.identity) as GameObject;
		cur.name = name + "Test";
		/*
		foreach (var component in cur.GetComponents<MonoBehaviour>()) {
			var startMethod = component.GetType().GetMethod("Start", BindingFlags.NonPublic | BindingFlags.Instance);
			// Debug.Log (startMethod);
			if (startMethod != null) startMethod.Invoke (component, null);
		}
		*/
		return cur;
	}
}