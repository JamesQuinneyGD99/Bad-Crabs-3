using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// QUI16000158
// James Quinney
// 1. Destroys another object when this object is touched
public class DestroyOtherOnTouch : MonoBehaviour
{
	[SerializeField]
	bool playerOnly = true; // Whether only the player can trigger the destruction
	[SerializeField]
	GameObject toDestroy; // This is the object that's destroyed

	void OnCollisionEnter(Collision collision){
		// If we're only allowing players, then ensure we're touching the player before proceding
		if(!playerOnly || collision.gameObject == player.main){
			// We make sure the object is still valid
			if(toDestroy != null){
				Destroy(toDestroy); // We destroy the other object
			}
		}
	}

	void OnTriggerEnter(Collider collision){
		// If we're only allowing players, then ensure we're touching the player before proceding
		if(!playerOnly || collision.gameObject == player.main){
			// We make sure the object is still valid
			if(toDestroy != null){
				Destroy(toDestroy); // We destroy the other object
			}
		}
	}
}
