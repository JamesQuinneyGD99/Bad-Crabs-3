using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// QUI16000158
// James Quinney
// Includes options for continuing the game after death, and exiting
public class DeathScreen : MonoBehaviour
{
	public static GameObject deathScreenObject; // This makes it easier to access the death screen from other scripts
    // Start is called before the first frame update
    void Start()
    {
		deathScreenObject = gameObject;
		gameObject.SetActive(false);
    }

	public void Enable(){
		deathScreenObject.SetActive(true);
	}

	public void Disable(){
		deathScreenObject.SetActive(false);
		Cursor.visible = false; // We make the cursor invisible
		Cursor.lockState = CursorLockMode.Locked; // We lock the cursor so it wont leave the game
	}

	public void Continue(){
		player.main.transform.position = player.main.GetComponent<player>().checkpoint.position;
		player.main.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation; // Unfreeze the player
		Disable();
	}

	public void QuitGame(){
		Application.Quit();
	}
}
