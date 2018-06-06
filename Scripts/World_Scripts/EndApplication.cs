using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndApplication : MonoBehaviour {

	//Disabled on test scene.

	void Update () {

        //End game if CurrentHealth <= 0;
        if (GetComponent<Attributes>().CurrentHealth <= 0)
        {
            Application.Quit();
        }
    }
}
