using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField]
	Transform exitNode; // This is where the player is teleported to when touching the portal

	// We check for when the portal touches something
	void OnTriggerEnter(Collider collider){
		// We check if the player is touching the portal
		if(collider.gameObject.tag == "Player"){
			player.main.transform.position = exitNode.position; // We move the player to the exit node
			player.main.GetComponent<player>().checkpoint = exitNode; // We change the checkpoint
		}
	}
}
