using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeTest : MonoBehaviour {
	public Swipe swipeControls;
	public Transform player;
	private Vector3 moveDirection;
	private Vector3 desiredPosition;
	// Update is called once per frame
	void Update () {
		if (swipeControls.swipeLeft) {
			desiredPosition += Vector3.left;
		}
		if (swipeControls.swipeRight) {
			desiredPosition += Vector3.right;
		}
		if (swipeControls.swipeUp) {
			desiredPosition += Vector3.up;
		}
		if (swipeControls.swipeDown) {
			desiredPosition += Vector3.down;
		}

		player.transform.position = Vector3.MoveTowards (player.transform.position, desiredPosition, 3f * Time.deltaTime);
	}
}
