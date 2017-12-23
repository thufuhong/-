
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class LoadRotate : MonoBehaviour
{
	public GameObject LoadImage;
	public GameObject LoadText;
	private Text progress;
	public AsyncOperation async;
	public float speed;
	void Start () 
	{
		progress = LoadText.GetComponent<Text> ();
		progress.text = "0%";
		speed = 1.0f;
	}

	void Update () 
	{
		LoadImage.transform.Rotate(new Vector3(0, 0, 1), speed); 
		speed = async.progress * 10 + 3.0f;
		progress.text = (int) (async.progress * 100) + "%";
	}
}