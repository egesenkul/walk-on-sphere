using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float maxTime=0.5f;
	public float minSwipeDist=50;

	public Text controlText;

	bool swipeLeft=false,swipeRight=false,swipeUp=false,swipeDown=false;

	float startTime;
	float endTime;

	Vector3 startPos;
	Vector3 endPos;

	float swipeDistance;
	float swipeTime;

	float speed = 10;
	float jumpPower = 500;
	bool isJump = false;
	Rigidbody rb;
	void Start(){
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Jump")) {
			isJump = true;
		}
		if (Input.touchCount > 0) {
			Touch touch = Input.GetTouch (0);

			if (touch.phase == TouchPhase.Began) {
				startTime = Time.time;
				startPos = touch.position;

			} else if (touch.phase == TouchPhase.Ended) {
				endTime = Time.time;
				endPos = touch.position;

				swipeDistance = (endPos - startPos).magnitude;
				swipeTime = endTime - startTime;

				if (swipeDistance > minSwipeDist) {
					funSwipe ();
				}
			}
		}

	}

	void funSwipe(){

		Vector2 distance = endPos - startPos;
		if (Mathf.Abs (distance.x) > Mathf.Abs (distance.y)) {
			//Horizontal swipe 
			if (distance.x > 0) {
				//Right swipe
				swipeRight=true;
				controlText.text = "right";
			}
			if (distance.x < 0) {
				//Left swipe
				swipeLeft=true;
				controlText.text = "left";
			}
		} else if (Mathf.Abs (distance.x) < Mathf.Abs (distance.y)) {
			//Vertical swipe 
			if (distance.y > 0) {
				//Up swipe
				controlText.text = "up";
				swipeUp = true;
			}
			if (distance.y < 0) {
				//Down swipe
				swipeDown=true;
				controlText.text = "down";
			}
		}
	}


	void FixedUpdate(){
		Vector3 dir = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
		if (swipeLeft) {
			dir = new Vector3 (-1, 0, 0);
		}
		else if (swipeRight) {
			dir = new Vector3 (1, 0, 0);
		}
		else if (swipeUp) {
			dir = new Vector3 (0, 0, 1);
		}
		else if (swipeDown) {
			dir = new Vector3 (0, 0, -1);
		}
		rb.MovePosition (rb.position + transform.TransformDirection (dir) * speed * Time.deltaTime);
		swipeLeft = swipeRight = swipeDown = swipeUp = false;
		if (isJump) {
			rb.AddForce (transform.up * jumpPower);
			isJump = false;
		}
	}
}
