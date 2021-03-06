﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float minSwipeDist=50;

	public Text controlText;

	public Camera camera;

	bool swipeLeft=false,swipeRight=false,swipeUp=false,swipeDown=false;
	bool PrevswipeLeft=false,PrevswipeRight=false,PrevswipeUp=false,PrevswipeDown=false;

	Vector3 dir2;

	Vector3 startPos;
	Vector3 endPos;

	float swipeDistance;

	float speed = 10;
	Rigidbody rb;
	void Start(){
		rb = GetComponent<Rigidbody> ();
		dir2 = new Vector3 (0, 0, 1);
	}

	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0) {
			Touch touch = Input.GetTouch (0);

			if (touch.phase == TouchPhase.Began) {
				startPos = touch.position;

			}  else if (touch.phase == TouchPhase.Ended) {
				endPos = touch.position;

				swipeDistance = (endPos - startPos).magnitude;

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
		}  
		else {
			//Vertical swipe 
			if (distance.y > 0) {
				//Up swipe
				swipeUp = true;
				controlText.text = "up";
			}
			if (distance.y < 0) {
				//Down swipe
				swipeDown=true;
				controlText.text = "down";
			}
		}
	}


	void FixedUpdate(){
		if (swipeLeft && !PrevswipeRight) {
			
			PrevswipeLeft = true;
			PrevswipeUp = false;
			PrevswipeRight = false;
			PrevswipeDown = false;
		}
		else if (swipeRight && !PrevswipeLeft) {
			PrevswipeLeft = false;
			PrevswipeUp = false;
			PrevswipeRight = true;
			PrevswipeDown = false;
		}
		else if (swipeUp && !PrevswipeDown) {
			PrevswipeLeft = false;
			PrevswipeUp = true;
			PrevswipeRight = false;
			PrevswipeDown = false;
		}
		else if (swipeDown && !PrevswipeUp) {
			PrevswipeLeft = false;
			PrevswipeUp = false;
			PrevswipeRight = false;
			PrevswipeDown = true;
		}

		if (PrevswipeUp) {
			dir2 = new Vector3 (0, 0, 1);
		}
		else if (PrevswipeDown) {
			dir2 = new Vector3 (0, 0, -1);
		}
		else if (PrevswipeRight) {
			dir2 = new Vector3 (1, 0, 0);
		}
		else if (PrevswipeLeft) {
			dir2 = new Vector3 (-1, 0, 0);
		}
		rb.MovePosition (rb.position + transform.TransformDirection (dir2) * speed * Time.deltaTime);
		swipeLeft = swipeRight = swipeDown = swipeUp = false;
		speed += 0.0002f;
	}
}

