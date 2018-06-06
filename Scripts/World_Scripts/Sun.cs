using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour {

    public GameObject Orbit; //Gameobject for Sun to orbit around.
    public float speed; //Speed orbit, TBD in inspector for easier testing.
	
	void Update ()
    {
        //Function for rotation of Sun, to be run once every frame.
        DayCycle();
    }

    void DayCycle()
    {
        //rotate Sun around GameObject "Orbit". 
        transform.RotateAround(Orbit.transform.position, Vector3.forward, speed * Time.deltaTime);
    }
}
