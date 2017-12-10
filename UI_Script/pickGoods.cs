using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickGoods : MonoBehaviour {
	public bool IfRotation = true;
	public float RotationSpeed = 60;
	public float value;
	public string type;
	void Start () {
		this.gameObject.GetComponentInChildren<TextMesh>().text = "+" + value.ToString();
	}

	public void OnTriggerEnter(Collider colli)
	{
		attribute colli_attribute = colli.gameObject.GetComponent<attribute> ();
		if (colli.gameObject == null || colli_attribute == null) {
			return;
		}
		// player can get the goods
		if (colli_attribute.if_Player && type == "HP+")
		{
			colli_attribute.update_HP(value);
			Destroy(this.gameObject);
		}
		if (colli_attribute.if_Player && type == "GOLD+") {
			colli_attribute.update_gold (value);
			Destroy (this.gameObject);
		}
	}

	// Update is called once per frame
	void Update () {
		if (IfRotation)
			transform.Rotate(0f, RotationSpeed * Time.deltaTime, 0f, Space.World);
	}
}
