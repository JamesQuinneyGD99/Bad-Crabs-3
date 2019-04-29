using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// QUI16000158
// James Quinney
// 1. Offsets a texture based on the current time, only affects object it's attached to
public class TextureOffset : MonoBehaviour
{
	Renderer rend; // This is the renderer for the current object
	[SerializeField]
	Vector2 speed; // The speed the texture offsets itself

    // Start is called before the first frame update
    void Start()
    {
		rend = GetComponent<Renderer>(); // We store the renderer for quicker access in update
    }

    // Update is called once per frame
    void Update()
    {
		rend.material.SetTextureOffset("_MainTex",speed * Time.time);
    }
}
