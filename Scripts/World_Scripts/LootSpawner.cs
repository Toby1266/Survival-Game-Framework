using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSpawner : MonoBehaviour {    
    
    [SerializeField] private GameObject Food; //Gameobject prefab for food object.
    [SerializeField] private GameObject Water; //Gameobject prefab for water object.
    [SerializeField] private float SpawnRate; //Spawn rate for loot, TBD in inspector to allow varying speeds for different locations.
    

    /*
     * This variable is now obselete.
     * 
    //Bool used to declare if loot is spawned or not.
    private bool AlreadySpawned = false;
    */

    void Start()
    {
        //Start loot spawning coroutine.
        StartCoroutine(SpawnLoot());
    }

    /*
     * This segment is now obselete.
     * 
    //Check if loot is already spawned. Used later to determine if loot needs to be destroyed, in order to spawn new loot.
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Food" || collision.gameObject.tag == "Water")
        {
            AlreadySpawned = false;
        }        
    }
    void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Food" || collision.gameObject.tag == "Water")
        {
            AlreadySpawned = true;
        }
    }
    */

    IEnumerator SpawnLoot()
    {
        //Constant loop.
        while (true)
        {
            //Instantiate water gameobject.
            GameObject WaterObject = (GameObject) Instantiate(Water, transform.position, Quaternion.identity); 
            //Wait for time equal to spawn rate.
            yield return new WaitForSeconds(SpawnRate);            
            //Destroy instantiated water gameobject.
            Destroy(WaterObject);

            //instantiate food gameobject.
            GameObject FoodObject = (GameObject)Instantiate(Food, transform.position, Quaternion.identity);
            //Waite for time equal to spawn rate.
            yield return new WaitForSeconds(SpawnRate);
            //Destroy instantiated food gameobject.
            Destroy(FoodObject);
        }        
    }
}
