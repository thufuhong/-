using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup : MonoBehaviour {
    public bool IfRotation = true;
    public float RotationSpeed = 60;
    public float value = 20;
    public string type = "HP+";

	// Use this for initialization
	void Start () {
        this.gameObject.GetComponentInChildren<TextMesh>().text = "+" + value.ToString();
	}

    public void OnTriggerEnter(Collider colli)
    {
        if(type == "HP+")
        {
            colli.gameObject.GetComponent<attribute>().update_HP(value);
            Destroy(this.gameObject);
        }
    }

	// Update is called once per frame
	void Update () {
        if (IfRotation)
            transform.Rotate(0f, RotationSpeed * Time.deltaTime, 0f, Space.World);
    }
}
