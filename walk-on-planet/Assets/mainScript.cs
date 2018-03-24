using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mainScript : MonoBehaviour {

	public float maxTime=0.5f;
	public float minSwipeDist=50;

	public float moveSpeed=1;
	private Vector3 moveDirection;

	public Text controlText;

	float startTime;
	float endTime;

	Vector3 startPos;
	Vector3 endPos;

	float swipeDistance;
	float swipeTime;

	void Update(){
		Debug.Log (transform.position);
		Vector2 distance = new Vector2(1,0);
		if (Input.GetKeyDown (KeyCode.Space)) {
			controlText.text = "EGE";
			transform.Translate (new Vector3 (1, 0, 0), Space.Self);
		} else {
			controlText.text = "right";
		}
	/*	if (Input.touchCount > 0) {
			Touch touch = Input.GetTouch (0);

			if (touch.phase == TouchPhase.Began) {
				startTime = Time.time;
				startPos = touch.position;
			
			} else if (touch.phase == TouchPhase.Ended) {
				endTime = Time.time;
				endPos = touch.position;

				swipeDistance = (endPos - startPos).magnitude;
				swipeTime = endTime - startTime;

				if (swipeTime < maxTime && swipeDistance > minSwipeDist) {
					funSwipe ();
				}
			}
		}
		*/
	}

	void FixedUpdate()
	{
		GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + transform.TransformDirection(moveDirection) * moveSpeed * Time.deltaTime);
	}

	void funSwipe(){

		Vector2 distance = endPos - startPos;
		if (Mathf.Abs (distance.x) > Mathf.Abs (distance.y)) {
			//Horizontal swipe 
			if(distance.x>0){
				//Right swipe
				controlText.text = "right";
				moveDirection = new Vector3(distance.x, 0, 0).normalized;
			}
			if (distance.x < 0) {
				//Left swipe
				controlText.text = "left";
				moveDirection = new Vector3(-distance.x, 0, 0).normalized;
			}
		}
		else if (Mathf.Abs (distance.x) < Mathf.Abs (distance.y)) {
			//Vertical swipe 
			if(distance.y>0){
				//Up swipe
				controlText.text = "up";
				moveDirection = new Vector3(0,distance.y, 0).normalized;
			}
			if (distance.y < 0) {
				//Down swipe
				controlText.text = "down";
				moveDirection = new Vector3(0,-distance.y, 0).normalized;
			}
		}
	}
}
