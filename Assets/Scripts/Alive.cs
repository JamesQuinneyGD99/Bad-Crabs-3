using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alive : MonoBehaviour
{
	public void Kill(){
		// Check if the thing dying is the player
		if(gameObject == player.main){
			DeathScreen.deathScreenObject.SetActive(true);
			Cursor.visible = true; // We make the cursor invisible
			Cursor.lockState = CursorLockMode.None; // We lock the cursor so it wont leave the game
		}
		else{
			Destroy(gameObject);
		}
	}
}
