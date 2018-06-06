using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionManager : MonoBehaviour {

    public bool ActivateFire = false; //Bool for lighting fire declared false.
    private float Range = 6; //Declared float for raycast range.

    public GameObject CF;//GameObject variable for "Campfire".    
    public GameObject Sun; //GameObject variable for "Sun".
    private GameObject ActiveCF; //GamObject variable for any existing Campfire's in scene.
    private GameObject ActiveFire; // GameObject variable for any existing fire's in scene.

    RaycastHit Detect; //Declared variable name for raycast collision detection.

    [SerializeField] private int Food = 0; //Declared starting integer for food at 0.
    [SerializeField] private int Water = 0; //Declared starting integer for water at 0.
    [SerializeField] private int Wood = 0; //Declared starting integer for wood at 0.

    [SerializeField] private bool Axe = false; //Bool for axe declared false, ensures player does not start with axe in inventory.
    [SerializeField] private bool Sheltered = false; //Bool for sheltered declared false.
    [SerializeField] public bool Cold = false; //Bool for temperature declared false. True being cold, false being fine.
    [SerializeField] public bool Burning = false; //Bool for touching fire declared false.

    public Text FoodDisplayUI;
    public Text WaterDisplayUI;
    public Text WoodDisplayUI;
    public Text AxeDisplayUI;

    void Start()
    {
        UpdateStatsUI();
    }

    void Update ()
    {
        //Detect if interact button "e" has been pressed.
        if (Input.GetButtonDown("Interact"))
        {            
            //If interact button "e" has been pressed, draw raycast from camera direction by distance "Range".
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out Detect, Range))
            {

                //If ray collides with food, destory food game object and increment water integer by 1.
                if (Detect.collider.gameObject.tag == "Food")
                {
                    Destroy(Detect.transform.gameObject);
                    Food++;
                    UpdateStatsUI();
                }

                //If ray collides with bottle, destroy bottle game object and increment water integer by 1.
                if (Detect.collider.gameObject.tag == "Bottle")
                {
                    Destroy(Detect.transform.gameObject);
                    Water++;
                    UpdateStatsUI();
                }

                //If ray collides with axe, destroy axe game object and set axe bool to true.
                //This is how we know the axe has been collected.
                if (Detect.collider.gameObject.tag == "Axe")
                {
                    Destroy(Detect.transform.gameObject);
                    Axe = true;
                    AxeDisplayUI.text = "- Axe";
                }
                if (Detect.collider.gameObject.tag == "AxeHead")
                {
                    Destroy(Detect.transform.gameObject);
                    Destroy(Detect.transform.parent.gameObject);
                    Axe = true;
                    AxeDisplayUI.text = "- Axe";
                }
                
                //If player has collected axe, and ray collides with tree, destory tree game object and increment wood integer by 1.
                if (Axe == true && Detect.collider.gameObject.tag == "Tree")
                {
                    Destroy(Detect.transform.gameObject);
                    Wood++;
                    UpdateStatsUI();
                }

                //If ray collides with Campfire, set activate fire to true.
                if (Detect.collider.gameObject.tag == "Campfire")
                {
                    ActivateFire = true;
                }
            }
        }

        //if key "1" is pressed and player has 1 or more Food, increase Hunger Hitpoints and decrease Food count by 1.
        if (Input.GetKeyDown("1") && Food >= 1)
        {
            GetComponent<Attributes>().CurrentHunger += 20;
            Food -= 1;
            UpdateStatsUI();
        }
        //if key "2" is pressed and player has 1 or more Water, increase Hydration Hitpoints and decrease Water count by 1.
        if (Input.GetKeyDown("2") && Water>= 1)
        {
            GetComponent<Attributes>().CurrentHydration += 20;
            Water -= 1;
            UpdateStatsUI();
        }

        //if key "3" is pressed and player has 1 or more Wood, destroy existing campfire and fire, instantiate new campfire and reduce wood by 1.
        if (Input.GetKeyDown("3") && Wood >= 1)
        {
            ActiveCF = GameObject.Find("Campfire(Clone)");
            ActiveFire = GameObject.FindGameObjectWithTag("Fire");
            Destroy(ActiveCF);
            Destroy(ActiveFire);
            GameObject Campfire = (GameObject)Instantiate(CF, transform.position, Quaternion.identity);
            Wood -= 1;
            UpdateStatsUI();
        }
        
        //If player is not "Sheltered" and y position of "Sun" is below 50.0f, player is "Cold". Else, player is not "Cold".
        if (!Sheltered && Sun.transform.position.y <= -50.0f)
        {
            Cold = true;
        }
        else
        {
            Cold = false;
        }
    }

    //Function for entering triggers.
    private void OnTriggerStay(Collider Collision)
    {
        //When player enters shelter or is near campfire, set Sheltered to true.
        if (Collision.gameObject.tag == "Shelter")
        {
            Sheltered = true;
        }
        //When player is touching fire, set Burning to true.
        if (Collision.gameObject.tag == "Fire")
        {
            Burning = true;
        }
    }

    //Function for exiting triggers.
    private void OnTriggerExit(Collider Collision)
    {
        //When player exits shelter or is not near campfire, set Sheltered to false.
        if (Collision.gameObject.tag == "Shelter")
        {
            Sheltered = false;
        }

        //When player is not touching fire, setr burning to false.
        if (Collision.gameObject.tag == "Fire")
        {
            Burning = false;
        }
    }

    void UpdateStatsUI()
    {
        FoodDisplayUI.text = "Food = " + Food.ToString();
        WaterDisplayUI.text = "Water = " + Water.ToString();
        WoodDisplayUI.text = "Wood = " + Wood.ToString();
    }
}
