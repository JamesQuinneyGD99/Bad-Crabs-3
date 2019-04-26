using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOnTouch : MonoBehaviour
{
	[SerializeField] bool playerOnly = true; // Whether the player is the only object that can make this object fall
	[SerializeField] Rigidbody rb; // The rigidbody which is enabled when touched

	void OnCollisionEnter(Collision collision){
		// Check if the object touching is allowed to break this object
		if(!playerOnly || collision.gameObject.tag == "Player"){
			rb.constraints = RigidbodyConstraints.None; // Unfreeze the rigidbody
		}
	}
}
