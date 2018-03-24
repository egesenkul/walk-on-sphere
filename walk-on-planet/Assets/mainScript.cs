using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainScript : MonoBehaviour {

	private Touch initialTouch = new Touch();
	private float distance = 0;
	private bool hasSwiped = false;
	private float speed = 5f;

	void FixedUpdate(){
		foreach (Touch t in Input.touches) {
			if (t.phase == TouchPhase.Began) {
				initialTouch = t;
			} else if (t.phase == TouchPhase.Moved && !hasSwiped) {
				float deltaX = initialTouch.position.x - t.position.x;
				float deltaY = initialTouch.position.x - t.position.y;
				bool SwipedSideways = Mathf.Abs (deltaX) > Mathf.Abs (deltaY);
				distance = Mathf.Sqrt ((deltaX * deltaX) + (deltaY * deltaY));
				if (distance > 100f) {
					if (SwipedSideways && deltaX > 0) {
						//swipe left
						this.transform.Translate(-speed*Time.deltaTime,0,0);
					} else if (SwipedSideways && deltaX <= 0) {
						//swipe rigth
						this.transform.Translate(speed*Time.deltaTime,0,0);
					} else if (SwipedSideways && deltaY > 0) {
						this.transform.Translate(0,-speed*Time.deltaTime,0);
						//swipe down
					} else if (SwipedSideways && deltaY <= 0) {
						this.transform.Translate(0,speed*Time.deltaTime,0);
						//swipe up
					}

					hasSwiped = true;
				}
			} else if (t.phase == TouchPhase.Ended) {
				initialTouch = new Touch ();
				hasSwiped = false;
			}
		}
	}
}
