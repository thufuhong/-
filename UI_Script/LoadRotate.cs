
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class LoadRotate : MonoBehaviour
{
	public GameObject LoadImage;
	public GameObject LoadText;
	private Text progress;
	private int progressval;
	public AsyncOperation async;
	public float speed;
	void Start () 
	{
		progress = LoadText.GetComponent<Text> ();
		progress.text = "0%";
		progressval = 0;
		speed = 1.0f;
	}

	void Update () 
	{
		LoadImage.transform.Rotate(new Vector3(0, 0, 1), speed); 
		speed = 1.0f;
		if (async != null) 
		{
			speed += progressval / 5.0f;
			/*
			if (async.progress >= 0.9f) 
			{
				progressval = 100;
			}
			*/
			if (progressval < async.progress * 100 / 0.9) 
			{
				progressval++;
			}
			progress.text = progressval + "%";
			if (progressval >= 99) 
			{
				async.allowSceneActivation = true;
			}
		}
	}
}