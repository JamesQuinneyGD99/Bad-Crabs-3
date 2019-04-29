using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// QUI16000158
// James Quinney
// 1. This moves an object along a path by a certain distance, it then returns
public class BoxMover : MonoBehaviour
{
    [SerializeField]
    Vector3 moveDistance = new Vector3(0.0f,0.0f,2.0f); // This is the distance from origin the box will move to
    [SerializeField]
    Rigidbody rb; // This is the rigidbody attached to the box
    [SerializeField]
    Vector3 startPosition; // This is the origin of the box
    [SerializeField]
    float moveSpeed = 0.1f; // This is how fast the box will move
	[SerializeField]
    int dir = 1; // This is the directional multiplier

    // Start is called before the first frame update
    void Start()
    {
        // We only want to store the origin if we didn't modify it in the editor
        if(startPosition == new Vector3(0,0,0)){
            startPosition = transform.position; // We store the origin when the object is created
        }
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = ((moveDistance*dir + startPosition) - transform.position) * moveSpeed; // We work out the current speed of the object

        Vector3 savedVelocity = rb.velocity; // We save the current velocity

		// We check to make sure the object is moving along the x axis, we then check to see if the current velocity is below the move speed
        if(savedVelocity.x != 0.0f && Mathf.Abs(savedVelocity.x) < moveSpeed){
			// If we are moving backwards we negate the move speed
            if(savedVelocity.x < 0.0f){
                savedVelocity.x = -moveSpeed;
            }
			// If we are moving forwards we add the move speed
            else{
                savedVelocity.x = moveSpeed;
            }
        }

		// We check to make sure the object is moving along the y axis, we then check to see if the current velocity is below the move speed
        if(savedVelocity.y != 0.0f && Mathf.Abs(savedVelocity.y) < moveSpeed){
			// If we are moving backwards we negate the move speed
            if(savedVelocity.y < 0.0f){
                savedVelocity.y = -moveSpeed;
            }
			// If we are moving forwards we add the move speed
            else{
                savedVelocity.y = moveSpeed;
            }
        }

		// We check to make sure the object is moving along the z axis, we then check to see if the current velocity is below the move speed
        if(savedVelocity.z != 0.0f && Mathf.Abs(savedVelocity.z) < moveSpeed){
			// If we are moving backwards we negate the move speed
            if(savedVelocity.z < 0.0f){
                savedVelocity.z = -moveSpeed;
            }
			// If we are moving forwards we add the move speed
            else{
                savedVelocity.z = moveSpeed;
            }
        }

        rb.velocity = savedVelocity; // We set the velocity to the modified velocity

        // We work out whether the object needs to change directions
        if(Vector3.Distance(transform.position, moveDistance*dir + startPosition) < 0.1f){
            dir = -dir; // We change the direction of the object
        }
    }
}
