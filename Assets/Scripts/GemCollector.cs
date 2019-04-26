﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// QUI16000158
// James Quinney
// Checks for collisions with gems, gives the player gems
public class GemCollector : MonoBehaviour
{
	// We check for collisions
    void OnTriggerEnter(Collider collider){
		// We check if the gem is touching the player
        if(collider.gameObject.tag == "Player"){
            player.gems += 1; // We increase the player's gems
            player.gemText.text = ""+player.gems; // We update the player's gem text
			// We check if the gem is attached to a light
			if(transform.parent == null){
				Destroy(gameObject); // We remove the gem
			}
			else{
				Destroy(transform.parent.gameObject); // We remove the parent of the gem (the glow effect)
			}
        }
    }
}
