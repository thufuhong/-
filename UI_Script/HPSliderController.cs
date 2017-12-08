using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPSliderController : MonoBehaviour {


	private Slider HPSlider;
	private Image HPColor;
	private GameObject parent;
	private float parent_height;
	private attribute HpValue;
	// Use this for initialization
	void Start () {
		HPSlider = transform.Find ("Slider").GetComponent<Slider> ();
		HPColor = HPSlider.fillRect.GetComponent<Image> ();
		parent = transform.parent.gameObject;
		HpValue = parent.GetComponent<attribute> ();
		// parent_height = (parent.GetComponent<MeshFilter>().mesh.bounds.size.y) * (parent.transform.lossyScale.y);
		parent_height = parent.GetComponent<CapsuleCollider>().bounds.size.y;
		HPColor.color = Color.green;
	}
	
	// Update is called once per frame
	void Update () {
		transform.rotation = Camera.main.transform.rotation;
		HPSlider.gameObject.transform.position = new Vector3 (
			parent.transform.position.x,
			parent.transform.position.y + parent_height + 0.1f,
			parent.transform.position.z);
		HPSlider.value = Mathf.Max(HpValue.HP, 0) / HpValue.HP_max;
		if (HPSlider.value < 0.3) {
			HPColor.color = Color.red;
		}
	}
}
