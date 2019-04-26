using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// QUI16000158
// James Quinney
// Moves the player based on player input
public class player : MonoBehaviour
{
    public static bool onFloor; // This is whether the player is on the floor, it's static for ease of access in other scripts
	public static float isSliding; // The time the player started sliding
    public static int gems; // The number of gems the player has
    public static TextMesh gemText; // The gem counter on the top left
	public static GameObject main; // This is the player itself
	public Transform checkpoint; // This is where the player goes back to upon death
    [SerializeField]
    Transform body; // The player's player model transform
	[SerializeField]
	Rigidbody rb; // The rigidbody attached to the player
	[SerializeField]
	Transform playerModel; // This is the playermodels transform

    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity = new Vector3(0.0f, -55.0f,0.0f); // We set the gravity as the player spawns
		Cursor.visible = false; // We make the cursor invisible
		Cursor.lockState = CursorLockMode.Locked; // We lock the cursor so it wont leave the game
        gemText = GameObject.Find("Gem Counter").GetComponent<TextMesh>(); // We find the gem text and cache it, so we can easily update it later
		main = gameObject; // Set the main
    }

    // Update is called once per frame
    void Update()
    {
        float mult = 3 - (Mathf.Abs(Input.GetAxis("Speed")) + 1); // If the player is holding the speed key, we store a multiplier for their move speed
		float vertical = Input.GetAxis("Vertical"); // How fast the player should move forwards/backwards
		float horizontal = Input.GetAxis("Horizontal"); // This is how fast the player should move left/right

		// We check to see if the player is sliding
		if(isSliding != 0.0f){
			horizontal = 0.0f; // The player can't move horizontally when sliding
			mult = 7.5f; // We increase the speed multiplier whilst the player is sliding
			vertical = 1.0f; // The player is always moving whilst sliding
		}

		Transform camTrans = Camera.main.transform; // We store the cameras current transform
        Vector3 cameraAngles = camTrans.eulerAngles; // We store the cameras current angles

		// We check to see if the player is currently moving
		if(Mathf.Abs(vertical) > 0.1f || Mathf.Abs(horizontal) > 0.1f){
			body.rotation = Quaternion.LookRotation(new Vector3(horizontal,0.0f,vertical)); // We make the player look in the direction they are moving
			body.eulerAngles += new Vector3(0.0f,cameraAngles.y + 270.0f,0.0f); // (cont'd) We then factor in the camera's angles
		}
        float yVel = rb.velocity.y; // We store the player's Y velocity so it isn't overwritten

		camTrans.eulerAngles = new Vector3(0.0f,camTrans.eulerAngles.y,0.0f); // This ensures the player isn't pushed into the ground
		rb.velocity = camTrans.forward * vertical * 3.0f * mult + camTrans.right * horizontal * 3.0f * mult; // We make the player move, relative to the camera's rotation
		camTrans.eulerAngles = cameraAngles; // We restore our camera angles
		rb.velocity = new Vector3(rb.velocity.x,yVel,rb.velocity.z); // We restore the Y velocity

        // We check to see if the player is standing on top of anything
        if(FloorChecker.floor != null && FloorChecker.floor.gameObject.GetComponent<Rigidbody>()){
            Vector3 vel = FloorChecker.floor.gameObject.GetComponent<Rigidbody>().velocity; // We get the velocity of the object the player is standing on
            rb.velocity += new Vector3(vel.x,0.0f,vel.z); // We add the velocity of what the player is standing on top of too
        }

		// note, restoring Y velocity ensures the player doesn't walk along 3 axes

		// We check if the player is trying to jump, and is on the floor
        if(Input.GetButtonDown("Jump") && onFloor){
			onFloor = false;
            rb.AddForce(transform.up * 900.0f); // We throw the player in the air
        }

		// If the player holds the slide key, start sliding, if they let go, stop sliding
		if(Input.GetButtonDown("Slide")){
			// We check to see if the player just started sliding
			if(isSliding == 0.0f && onFloor){
				// We make them jump into the slide
				rb.AddForce(transform.up * 900.0f);
			}
			isSliding = Time.time; // This will allow the player to slowly rotate forwards when they start sliding
		}
		else if(Input.GetButtonUp("Slide")){
			isSliding = 0.0f; // isSliding being 0 means that the player is not sliding
		}

		// We check to see if the player is sliding, if they are we rotate them forwards
		if(isSliding != 0.0f){
			// This rotates the body forwards
			playerModel.eulerAngles = new Vector3(0,0,Mathf.Clamp(360.0f - Time.deltaTime * (Time.time - isSliding) * 18000.0f,270.0f,360.0f)) + body.eulerAngles;
		}
		else{
			// This puts the body back the way it was
			playerModel.eulerAngles = new Vector3(0,0,0.0f) + body.eulerAngles;
		}
    }
}
