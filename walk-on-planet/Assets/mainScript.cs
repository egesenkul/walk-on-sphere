using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainScript : MonoBehaviour {

	public float maxTime=0.5f;
	public float minSwipeDist=50;

	float startTime;
	float endTime;

	Vector3 startPos;
	Vector3 endPos;

	float swipeDistance;
	float swipeTime;

	void Update(){
	
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

				if (swipeTime < maxTime && swipeDistance > minSwipeDist) {
					funSwipe ();
				}
			}
		}
	}
	void funSwipe(){

		Vector2 distance = endPos - startPos;
		if (Mathf.Abs (distance.x) > Mathf.Abs (distance.y)) {
			//Horizontal swipe 
			if(distance.x>0){
				//Right swipe
				transform.Translate(distance.x,0,0);
			}
			if (distance.x < 0) {
				//Left swipe
				transform.Translate(-distance.x*Time.deltaTime,0,0);
			}
		}
		else if (Mathf.Abs (distance.x) < Mathf.Abs (distance.y)) {
			//Vertical swipe 
			if(distance.y>0){
				//Up swipe
				transform.Translate(distance.y * Time.deltaTime, Space.World);
			}
			if (distance.y < 0) {
				//Down swipe
				transform.Translate(distance.y * Time.deltaTime, GameObject.FindGameObjectWithTag("world"));
			}
		}
	}
}
