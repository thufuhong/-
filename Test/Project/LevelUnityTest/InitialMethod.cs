using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Runtime.InteropServices;

public class InitialMethod
{
	[DllImport("user32.dll", EntryPoint = "Keybd_event")]
	public static extern void Keybd_event (
		byte bVk,
		byte bScan, // 0
		int dwFlags, // 0 : down, 1 : always down, 2 : release
		int dwExtraInfo // 0
	);

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