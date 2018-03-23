using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour {
	public bool tap,swipeLeft,swipeRight,swipeUp,swipeDown;
	public Vector2 startTouch,swipeDelta;
	public bool isDragging = false; // is our finger in the screen
	 
	private void Update(){
		tap = swipeUp = swipeRight = swipeLeft = swipeDown = false;

		if (Input.GetMouseButtonDown (0)) {
			//Mouse Left click
			tap = isDragging= true; //ekrana bastı olarak assume ettik 
			startTouch = Input.mousePosition; //tap ettiğimiz yeri vektörün başlangıç yeri olarak assign ettik
		} 
		else if (Input.GetMouseButtonUp (0)) {
			//Mousedan parmağını çektiği zaman 
			isDragging=false;
			Reset();
		}

		#region for mobile

		if(Input.touches.Length >0){
			if(Input.touches[0].phase == TouchPhase.Began){
				//basmaya başladığı ilk nokta 0. indexi
				tap = isDragging= true;
				startTouch = Input.touches[0].position; //basmaya ilk başladığımız yeri başlangıç noktası olarak kabul ettik
			}
			else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled){
				isDragging=false;
				Reset();
			}
		}

		#endregion

		//in that time we can resume touching for that calculate the distance
		swipeDelta= Vector2.zero;
		if (isDragging) {
			if (Input.touches.Length > 0) {
				swipeDelta = Input.touches [0].position - startTouch;
			} else if (Input.GetMouseButton (0)) {
				swipeDelta = (Vector2)Input.mousePosition - startTouch;
			}
		}
		//did we exit the deadzone
		if(swipeDelta.magnitude > 150){
			float x = swipeDelta.x;
			float y = swipeDelta.y;
			//hangi yöne swipe oluyor sağa sola mı yukarı aşağı mı 
			if (Mathf.Abs (x) > Mathf.Abs (y)) {
				if (x < 0) {
					swipeLeft = true;
				} else {
					swipeRight = true;
				}
			} else {
				if (y < 0) {
					swipeDown = true;
				} else {
					swipeUp = true;
				}
			
			}
			Reset ();
		}
	}

	private void Reset(){
		startTouch = swipeDelta = Vector2.zero;
		isDragging = false;
	}
}
