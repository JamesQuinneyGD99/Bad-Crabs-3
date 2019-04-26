using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieOnTouch : MonoBehaviour
{
	// Check for collisions
	void OnTriggerEnter(Collider collider){
		// Check if the object being touched can die
		if(collider.gameObject.GetComponent<Alive>()){
			// Kill them
			collider.gameObject.GetComponent<Alive>().Kill();
		}
	}
}
