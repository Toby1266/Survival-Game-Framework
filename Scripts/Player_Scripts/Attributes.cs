using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attributes : MonoBehaviour {

    //Initializing variables.
    //Starting Health points, Hunger points and Hydration.
    [SerializeField] public int CurrentHealth = 100;
    [SerializeField] public int CurrentHunger = 100;
    [SerializeField] public int CurrentHydration = 100;

    //Speed and Rate of Starvation and Dehydration.
    [SerializeField] private float StarvationSpeed = 7.5f;
    [SerializeField] private float DehydrationSpeed = 5f;
    private int StarvationRate = 2;
    private int DehydrationRate = 2;

    //Speed and Rate of Health Restoration.
    [SerializeField] private float HealthRestorationSpeed = 5.0f;
    [SerializeField] private int HealthRestorationRate = 5;

    //Speed and Rate of Hunger Depletion.
    [SerializeField] private float HungerDepletionSpeed = 15.0f;
    private int HungerDepletionRate = 2;

    //Speed and Rate of Hydration Depletion.
    [SerializeField] private float HydrationDepletionSpeed = 10f;
    private int HydrationDepletionRate = 2;

    //Speed and Rate of Cold Damage.
    [SerializeField] private float ColdDamageSpeed = 5.0f;
    [SerializeField] private int ColdDamageRate = 5;

    //Speed and Rate of Burning Damage.
    [SerializeField] private float BurningDamageSpeed = 0.25f;
    [SerializeField] private int BurningDamageRate = 2;

    //Bool used to alternate between GameplayMode and SurvivalMode.
    [SerializeField] private bool GameplayMode = true;

    //HUD text variables.
    public Text HealthDisplayUI;
    public Text HydrationDisplayUI;
    public Text HungerDisplayUI;
    public Text Temperature;
    public Text Mode;


    /*
     * This code is now obselete.
     * 
     * public bool Dehydrating = false;
     */



    void Start() {

        //On game start, begin coroutines .    
        StartCoroutine(RestoreHealth());
        StartCoroutine(DepleteHydration());
        StartCoroutine(DepleteHunger());
        StartCoroutine(Starvation());
        StartCoroutine(Dehydration());
        StartCoroutine(IsCold());
        StartCoroutine(IsBurning());
        StartCoroutine(IsSprinting());

        //Run functio to update all HUD information.
        UpdateStatsUI();
    }

    //Health restoration coroutine.
    IEnumerator RestoreHealth()
    {
        //constant loop.
        while (true)
        {
            //wait for time equal to health restoration speed.
            //after waiting, if current hunger and hydration are more than 80, and  current health is less than 95, and player is not cold or burning, restore health by 5.
            yield return new WaitForSeconds(HealthRestorationSpeed);
            if (CurrentHunger >= 80 && CurrentHydration >= 80 && CurrentHealth <= 95 && GetComponent<InteractionManager>().Cold == false && GetComponent<InteractionManager>().Burning == false)
            {
                CurrentHealth += HealthRestorationRate;
            }
         
        //Run function to update all HUD information.   
        UpdateStatsUI();
        }
    }


    //Deplete hydration coroutine.
    IEnumerator DepleteHydration()
    {
        //constant loop.
        while (true)
        {
            //Wait for time equal to hydration depletion speed then reduce current hydration by 2.
            yield return new WaitForSeconds(HydrationDepletionSpeed);           
            CurrentHydration -= HydrationDepletionRate;  
            
            /*
             * This code is now obselete.
             * 
             * if(Dehydrating == false && CurrentHydration <= 80)
            {
                Debug.Log("StartAgain");
                StartCoroutine(DepleteHealth());
            }
            */
            
            //Run function to update all HUD information.
            UpdateStatsUI();
        }
    }

    //Deplete hunger coroutine.
    IEnumerator DepleteHunger()
    {
        //constant loop.
        while (true)
        {
            //Wait for time equal to hunger depletion speed then reduce hunger by 2.
            yield return new WaitForSeconds(HungerDepletionSpeed);
            CurrentHunger -= HungerDepletionRate;

            //Run function to update all HUD information.
            UpdateStatsUI();
        }
    }

    //Health depletion due to starvation coroutine.
    IEnumerator Starvation()
    {
        //Constant loop.
        while (true)
        {
            //Wait for time equal to starvation speed.
            //after waiting, if hunger is less than 40, reduce health by 2.
            yield return new WaitForSeconds(StarvationSpeed);
            if (CurrentHunger <= 40)
            {
                CurrentHealth -= StarvationRate;
            }

            //Run function to update all HUD information.
            UpdateStatsUI();
        }
    }

    //Health depletion due to dehydration coroutine.
    IEnumerator Dehydration()
    {
        //Constant loop.
        while (true)
        {
            //Wait for time equal to dehydration speed.
            //after waiting, if current hydration is less than 40, reduce health by 2.
            yield return new WaitForSeconds(DehydrationSpeed);

            if (CurrentHydration <= 40)
            {
                CurrentHealth -= DehydrationRate;
            }

            //Run function to update all HUD information.
            UpdateStatsUI();
        }
    }

    //Coroutine for cold damage.
    IEnumerator IsCold()
    {
        //Constant loop.
        while (true)
        {
            //Wait for time equal to cold damage speed.
            //After waiting, if player is cold, reduce health by 2.
            yield return new WaitForSeconds(ColdDamageSpeed);
            if (GetComponent<InteractionManager>().Cold == true)
            {
                CurrentHealth -= ColdDamageRate;

                //Change temperature text to inform player that they are cold.
                Temperature.text = "Temperature = Too Cold!";
            }

            //Run function to update all HUD information.
            UpdateStatsUI();
        }
    }

    IEnumerator IsBurning()
    {
        //Constant loop.
        while (true)
        {
            //wait for time equal to burning damage speed.
            //After waiting, if player is burning, reduce health by 2.
            yield return new WaitForSeconds(BurningDamageSpeed);
            if (GetComponent<InteractionManager>().Burning == true)
            {
                CurrentHealth -= BurningDamageRate;

                //Change temperature text to inform player that they are burning.
                Temperature.text = "Temperature = Burning!";
            }

            //Run function to update all HUD information.
            UpdateStatsUI();
        }
    }

    //Sprinting coroutine
    IEnumerator IsSprinting()
    {
        //Constant loop.
        while (true)
        {
            //wait for 0.5 seconds.
            yield return new WaitForSeconds(0.5f);

            //if player speed is equal to 16.
            if (GetComponent<MoveChar>().speed == 16.0f)
            {
                //wait for time equal to half of hydration depletion speed, then reduce current hydration by 2.
                yield return new WaitForSeconds(HydrationDepletionSpeed/2);
                CurrentHydration -= HydrationDepletionRate;
            }
        }
    }

    /*
     * This code is now obselete.
     * 
     * IEnumerator DepleteHealth()
    {
        Dehydrating = true;

        while (true)
        {
            yield return new WaitForSeconds(HealthDepletionSpeed);
            CurrentHealth -= 1;

            if(CurrentHydration >= 81)
            {
                Debug.Log("StopHydrating");
                Dehydrating = false;
                StopCoroutine(DepleteHealth());
                yield break;
            }
        }
    }
    */

    private void Update()
    {
        //Clamp Health, Hunger and Hydration Hitpoints between minimum 0 and maximum 100.
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, 100);
        CurrentHunger = Mathf.Clamp(CurrentHunger, 0, 100);
        CurrentHydration = Mathf.Clamp(CurrentHydration, 0, 100);

        //if gameplay mode is true, set variables accordingly.
        if (GameplayMode == true)
        {
            HungerDepletionSpeed = 15.0f;
            HydrationDepletionSpeed = 10.0f;
            StarvationSpeed = 7.5f;
            DehydrationSpeed = 5.0f;
            Mode.text = "Gameplay Mode";
        }
        //if gameplay mode is not true, set variables according to survival mode.
        else if (GameplayMode == false)
        {
            HydrationDepletionSpeed = 21.6f;
            HungerDepletionSpeed = 151.2f;
            StarvationSpeed = 60.48f;
            DehydrationSpeed = 8.64f;
            Mode.text = "Survival Mode";
        }

        //If player is not cold and not hot, set temperature text to inform player their temperature is fine.
        if (GetComponent<InteractionManager>().Cold == false && GetComponent<InteractionManager>().Burning == false)
        {
            Temperature.text = "Temperature = Fine";
        }        

        //if the "9" key is pressed, alternate between gameplay mode and survival mode.
        if (Input.GetKeyDown("9"))
        {
            GameplayMode = !GameplayMode;
        }

    }

    //Function used to update HUD information.
    void UpdateStatsUI()
    {    
        HealthDisplayUI.text = "Health = " + CurrentHealth.ToString();
        HydrationDisplayUI.text = "Hydration = " + CurrentHydration.ToString();
        HungerDisplayUI.text = "Hunger = " + CurrentHunger.ToString();
    }

}
