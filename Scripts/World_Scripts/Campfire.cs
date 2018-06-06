using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campfire : MonoBehaviour
{

    [SerializeField] private GameObject Fire; //GameObject used to instantiate Fire Prefab.
    private bool FireLit = false; //Bool to declare if fire is currently lit.

    
    private void Start()
    {
        //Start corourtine to destroy GameObject after time.
        StartCoroutine(DestroyCampfire());
        
        //Transform y position by -1 (to appear placed on ground).
        transform.position = new Vector3(transform.position.x,transform.position.y-1 , transform.position.z);
    }

    void Update()
    {
        //If ActivateFire (from InteractionManager) is true, Start coroutine to light fire.
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<InteractionManager>().ActivateFire == true)
        {
            StartCoroutine(LightFire());
        }
    }

    //After 300 seconds (3 Minutes) destroy Campfire GameObject.
    IEnumerator DestroyCampfire()
    {
        yield return new WaitForSeconds(300.0f);
        Destroy(gameObject);
    }

    IEnumerator LightFire()
    {
        //Set ActivateFire (from InteractionManager) to false.
        GameObject.FindGameObjectWithTag("Player").GetComponent<InteractionManager>().ActivateFire = false;

        //If fire is not already lit.
        if (FireLit == false)
        {
            //Set FireLit to true. turn campfire into shelter, instantiate Fire Prefab, wait 60 seconds, destroy Fire Prefab, turn campfire back into campfire, set FireLit to false.
            FireLit = true;
            transform.gameObject.tag = "Shelter";
            GameObject FireObject = (GameObject)Instantiate(Fire, gameObject.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(30.0f);
            Destroy(FireObject);
            transform.gameObject.tag = "Campfire";
            FireLit = false;
        }
    }
}