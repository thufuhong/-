
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class LoadRotate : MonoBehaviour
{
	public GameObject LoadImage;
	public AsyncOperation async = null;
	public float speed;
	void Start () 
	{
		
		speed = 1.0f;
	}

	void Update () 
	{
		LoadImage.transform.Rotate(new Vector3(0, 0, 1), speed); 
		if (async != null) 
		{
			speed = async.progress * 10;
		}
	}
}