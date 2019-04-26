using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    int dir = 1; // This is the directional multiplier

    // Start is called before the first frame update
    void Start()
    {
        // We only want to store the origin if we didn't modify it in the editor
        if(startPosition == new Vector3(0,0,0)){
            startPosition = transform.position; // We store the origin when the object is created
        }
        transform.position += new Vector3(Random.Range(-moveDistance.x,moveDistance.x),Random.Range(-moveDistance.y,moveDistance.y),Random.Range(-moveDistance.z,moveDistance.z));

        if(Random.Range(0,2) == 1){
            dir = -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = ((moveDistance*dir + startPosition) - transform.position) * moveSpeed; // We work out the current speed of the object

        Vector3 savedVelocity = rb.velocity;

        if(savedVelocity.x != 0.0f && Mathf.Abs(savedVelocity.x) < moveSpeed){
            if(savedVelocity.x < 0.0f){
                savedVelocity.x = -moveSpeed;
            }
            else{
                savedVelocity.x = moveSpeed;
            }
        }

        if(savedVelocity.y != 0.0f && Mathf.Abs(savedVelocity.y) < moveSpeed){
            if(savedVelocity.y < 0.0f){
                savedVelocity.y = -moveSpeed;
            }
            else{
                savedVelocity.y = moveSpeed;
            }
        }

        if(savedVelocity.z != 0.0f && Mathf.Abs(savedVelocity.z) < moveSpeed){
            if(savedVelocity.z < 0.0f){
                savedVelocity.z = -moveSpeed;
            }
            else{
                savedVelocity.z = moveSpeed;
            }
        }

        rb.velocity = savedVelocity;

        // We work out whether the object needs to change directions
        if(Vector3.Distance(transform.position, moveDistance*dir + startPosition) < 0.1f){
            dir = -dir; // We change the direction of the object
        }
    }
}
