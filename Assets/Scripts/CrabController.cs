using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// QUI16000158
// James Quinney
// 1. Rotates randomly, it's used to make the crab walk around the player in the last level
// 2. Fires projectiles at the player
public class CrabController : MonoBehaviour
{
	float nextSwitch = 0.0f; // This is the time the direction will change again
	[SerializeField]
	float rotateSpeed = 1.0f; // The speed of rotation
	int dir = 1; // The direction of rotation, -1 = backwards, 1 = forwards
	[SerializeField]
	GameObject projPrefab; // The prefab for the projectile

    // Update is called once per frame
    void Update()
    {
		// Check if it's time to change direction
		if(Time.time > nextSwitch){
			nextSwitch = Time.time + Random.Range(1.0f,10.0f); // Set the amount of time before the next direction change
			dir = -dir; // Change direction

			GameObject projectile = Instantiate(projPrefab,transform.GetChild(0).position + Vector3.up * 4.0f,transform.GetChild(0).rotation); // Create the projectile
			Physics.IgnoreCollision(projectile.GetComponent<Collider>(),transform.GetChild(0).GetComponent<Collider>()); // Make sure the projectile doesn't hit the crab
			projectile.GetComponent<Rigidbody>().AddForce(transform.GetChild(0).forward * Random.Range(500.0f,1000.0f) + Vector3.up * Random.Range(200.0f,600.0f)); // Shoot the projectile diagonally forwards
		}
		else{
			Vector3 curRot = transform.eulerAngles; // Store the object's current angles
			curRot += new Vector3(0.0f,rotateSpeed * Time.deltaTime * dir,0.0f); // Rotate by the speed of the object
			transform.eulerAngles = curRot; // Update the rotation of our object
		}
    }
}
