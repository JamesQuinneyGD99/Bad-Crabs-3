using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// QUI16000158
// James Quinney
// 1. Kills anything alive that touches it
public class DieOnTouch : MonoBehaviour
{
	// Check for triggers
	void OnTriggerEnter(Collider collider){
		// Make sure it doesn't try to collide with itself
		if(collider.gameObject != gameObject){
			// Check if the object being touched can die
			if(collider.gameObject.GetComponent<Alive>()){
				// Kill them
				collider.gameObject.GetComponent<Alive>().Kill();
			}
		}
	}

	// Check for collisions
	void OnCollisionEnter(Collision collider){
		// Make sure it doesn't try to collide with itself
		if(collider.gameObject != gameObject){
			// Check if the object being touched can die
			if(collider.gameObject.GetComponent<Alive>()){
				// Kill them
				collider.gameObject.GetComponent<Alive>().Kill();
			}
		}
	}
}
