using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// QUI16000158
// James Quinney
// 1. Makes objects alive/killable
public class Alive : MonoBehaviour
{
	public void Kill(){
		// Check if the thing dying is the player
		if(gameObject == player.main){
			gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll; // Ensure the player doesn't fall endlessly
			DeathScreen.deathScreenObject.SetActive(true);
			Cursor.visible = true; // We make the cursor invisible
			Cursor.lockState = CursorLockMode.None; // We lock the cursor so it wont leave the game
		}
		else{
			Destroy(gameObject);
		}
	}
}
