using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityReciever : MonoBehaviour {

	public Gravity gravity;
	// Use this for initialization
	void Start () {
		Rigidbody rb = GetComponent<Rigidbody> ();
		rb.useGravity = false;
		rb.freezeRotation = true;
	}
	
	// Update is called once per frame
	void Update () {
		gravity.ApplyGravity (transform);
	}
}
