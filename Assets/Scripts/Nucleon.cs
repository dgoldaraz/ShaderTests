using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nucleon : MonoBehaviour {


    public Rigidbody Body;
    public float AttractionForce = 10.0f;

    void Awake()
    {
        Body = GetComponent<Rigidbody>();
    }

	// Update is called once per frame
	void Update ()
    {
		//Apply attraction to the center of the scene (we are applying our poisition as a negative force)
        if(Body)
        {
            Body.AddForce(transform.localPosition * -AttractionForce);
        }
	}
}
