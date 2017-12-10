using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class attack_range : MonoBehaviour {
    public GameObject Target;

    
	// Use this for initialization
	void Start () {
        
	}
	
    public void OnTriggerEnter(Collider colli)
    {
        if (colli.tag == "Team0")
            Target.GetComponent<AICharacterControl>().active = true;
    }


    public void OnTriggerExit(Collider colli)
    {
        if (colli.tag == "Team0")
        {
            Vector3 t = colli.transform.position;
            t.Scale(new Vector3(1f, 0, 1f));
            if (!(this.gameObject.GetComponent<Collider>().bounds.Contains(colli.transform.position)))
                Target.GetComponent<AICharacterControl>().active = false;
        }
            
    }

    // Update is called once per frame
    void Update () {
		
	}
}
