using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// QUI16000158
// James Quinney
// 1. Checks for whether the player is on the floor
// 2. Makes the player bounce if they touch a red box
public class FloorChecker : MonoBehaviour
{
    [SerializeField]
    Rigidbody rb; // This is the rigidbody attached to the player
    public static List<Collider> colliders; // This is everything the player is currently standing on
    Dictionary<GameObject, float> reloadFloor; // This is all of the boxes which need to respawn
    [SerializeField]
    Transform playerTransform; // This is the player's transform
	public static Transform floor; // This is the object the player is standing on

    void Start(){
        colliders = new List<Collider>(); // We create a list to store everything the player is touching
        reloadFloor = new Dictionary<GameObject, float>(); // We create a dictionary to store respawn times for boxes
    }

    // Called when the player lands on something
    void OnTriggerEnter(Collider collider){
        // We check to make sure the floor checker doesn't recognise the player as the floor
        if(collider.gameObject.tag != "Player"){
            player.onFloor = true; // We store that the player is on the floor
            colliders.Add(collider); // We add the current collider to the list of objects the player is touching

            // We check to see if the player has landed on a red box (Other boxes don't need tags)
            if(collider.gameObject.tag == "Box"){
                reloadFloor[collider.gameObject] = Time.time; // We tell the box when it needs to respawn
                collider.gameObject.SetActive(false); // We deactivate the box
                player.onFloor = false; // We tell the player they are no longer on the floor
                colliders = new List<Collider>(); // We clear the list of things the player is standing on
                // We check to see if the player is holding the jump key when landing on a box
                if(Input.GetButton("Jump")){
                    rb.velocity = new Vector3(rb.velocity.x,32.5f,rb.velocity.z); // If they are we send them a bit higher
                }
                else{
                    rb.velocity = new Vector3(rb.velocity.x,30.0f,rb.velocity.z); // than if they weren't holding the key
                }
            }
            // We check to see if the player has landed on something other than a box/the player
            else{
                floor = collider.transform; // We attach the player to this object
            }
        }
    }

    void Update(){
        List<GameObject> toReload = new List<GameObject>(); // We create a list of all objects that will reload this frame

        // We loop through each box which needs to be respawned
        foreach(KeyValuePair<GameObject, float> boxes in reloadFloor){
            // We check if 10 seconds have passed since the box was spawned
            if(boxes.Value + 10 < Time.time){
                toReload.Add(boxes.Key); // We add the box to the list of boxes which will be respawned
            }
        }

        // We loop through each box which is to be respawned
        foreach(GameObject box in toReload){
            box.SetActive(true); // We reactivate it
            reloadFloor.Remove(box); // We remove it from the list of boxes which need to be respawned
        }
    }

    // This is called when the player leaves an object
    void OnTriggerExit(Collider collider){
        // We check to make sure it wasn't the player it was touching
        if(collider.gameObject.tag != "Player"){
            colliders.Remove(collider); // We remove the object from the list of objects the player is touching
            // We check to see if the player isn't standing on anything
            if(colliders.Count == 0){
                player.onFloor = false; // We tell the player they are no longer on the floor
            }

            // We check to see if the player has left the object he is attached to
            if(collider.transform == playerTransform.parent){
                // We check to see if there is anything else the player is standing on
                if(colliders.Count>0){
                    floor = colliders[colliders.Count-1].transform; // We attach them to that instead
                }
                // If the player isn't standing on anything else
                else{
                    floor = null; // We detach them from everything
                }
            }
        }
    }
}
