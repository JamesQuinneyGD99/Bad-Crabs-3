using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// QUI16000158
// James Quinney
// Rotates the player's legs based on player input
public class LegMover : MonoBehaviour
{
	[SerializeField]
	bool isPlayer = true; // Whether to take player input or not
    Vector3 defaultRot; // This is the rotation when the player spawns
    [SerializeField]
    int angle = 1; // This rotation of the player's legs
	[SerializeField]
	float speed = 20.0f; // The speed of the rotation
	[SerializeField]
	Transform body; // The body that the rotation moves with
	[SerializeField]
	string axis = "z";
    float totalMove = 0; // This increases as the player moves
    // Start is called before the first frame update
    void Start()
    {
		defaultRot = transform.eulerAngles; // We store the player's starting rotation, with the offset of their starting rotation
    }

    // Update is called once per frame
    void Update()
    {
		// We check if the player is on the floor
        if(!isPlayer || player.onFloor){
			if(isPlayer){
	            totalMove += Mathf.Abs(Input.GetAxis("Horizontal")) * Time.deltaTime * speed; // We increase the movement of the leg by the player's input
	            totalMove += Mathf.Abs(Input.GetAxis("Vertical")) * Time.deltaTime * speed; // We do the same along both axis
			}
			else{
				totalMove += Time.deltaTime * speed;
			}
			switch(axis){ // Check the axis that is rotating
				case "x":
					transform.eulerAngles = new Vector3(defaultRot.x + Mathf.Sin(totalMove) * angle,defaultRot.y,defaultRot.z); // We update the leg's rotation
					break;
				case "y":
					transform.eulerAngles = new Vector3(defaultRot.x,defaultRot.y + Mathf.Sin(totalMove) * angle,defaultRot.z); // We update the leg's rotation
					break;
				default:
					transform.eulerAngles = new Vector3(defaultRot.x,defaultRot.y,defaultRot.z + Mathf.Sin(totalMove) * angle); // We update the leg's rotation
					break;
			}
			transform.eulerAngles += body.eulerAngles;
        }
		// This is if the player isn't on the floor
        //else{
        //    transform.localRotation = Quaternion.Euler(0.0f,speed * 3.0f,defaultRot.z); // We put the player's legs on the sids of their body
        //}
    }
}
