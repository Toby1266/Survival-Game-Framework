using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveChar : MonoBehaviour {

    public float speed = 8.0f; //default player speed set to 8.
	
	void Update()
    {

        //if "shift" key is held, enable sprint mode, increasing speed from 8 to 16.
        if (Input.GetButton("Sprint"))
        {
            speed = 16.0f;
        }
        //if "shift" key not held, disable sprint mode, decreasing speed from 16 to 8.
        else
        {
            speed = 8.0f;
        }
        
        //calculate float variables for vertical and horizontal movement.
        float move = Input.GetAxis("Vertical") * speed *Time.deltaTime;
        float moveside = Input.GetAxis("Horizontal") * speed *Time.deltaTime;

        //Apply forces accordingly.
        transform.Translate(moveside, 0, move);
	}
}
