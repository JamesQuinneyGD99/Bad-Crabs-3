using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// QUI16000158
// James Quinney
// 1. Makes the object fall when touched
// 2. Returns to original position after 5 seconds
public class DropOnTouch : MonoBehaviour
{
	[SerializeField] bool playerOnly = true; // Whether the player is the only object that can make this object fall
	[SerializeField] Rigidbody rb; // The rigidbody which is enabled when touched
	public Vector3 startPos; // This is the starting position of the object
	public Vector3 startAng; // This is the starting angle of the object
	public float startTime; // When the object started falling

	void Start(){
		// We store both the angles and position of the object when it is created
		startPos = transform.position;
		startAng = transform.eulerAngles;
	}

	void OnCollisionEnter(Collision collision){
		// Check if the object touching is allowed to break this object
		if(!playerOnly || collision.gameObject.tag == "Player"){
			rb.constraints = RigidbodyConstraints.None; // Unfreeze the rigidbody
			startTime = Time.time; // We specify the time the object started falling
		}
	}

	void Update(){
		// We check if the object is falling, and then whether it's been falling for more than 5 seconds
		if(startTime != 0.0f && Time.time - startTime > 5.0f){
			// Reset the fall time
			startTime = 0.0f;
			// Restore position and angle of the object
			transform.position = startPos;
			transform.eulerAngles = startAng;

			rb.constraints = RigidbodyConstraints.FreezeAll; // Freeze the rigidbody again
		}
	}
}
