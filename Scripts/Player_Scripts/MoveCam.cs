using UnityEngine;
using System.Collections;

public class MoveCam  : MonoBehaviour {

    Vector2 mouse; //vector for mouse movement.
    Vector2 smooth; //vector applied for smooth mouse movement.
    GameObject Character; //Gameobject variable for the player.
    public float sensitivity = 4.0f; //declared default camera sensitivty.
    public float smoothness = 2.0f; //declared default camera movement.

    void Start()
    {
        Character = this.transform.parent.gameObject;

        //lock mouse coursor to center of screen and hide it.
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //Calculate mouse direction.
        var mdirection = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        mdirection = Vector2.Scale(mdirection, new Vector2(sensitivity * smoothness, sensitivity * smoothness));

        //apply smoothness to mouse movement.
        smooth.x = Mathf.Lerp(smooth.x, mdirection.x, 1.0f / smoothness);
        smooth.y = Mathf.Lerp(smooth.y, mdirection.y, 1.0f / smoothness);
        mouse += smooth;

        //clamp mouse movement on y-axis.
        mouse.y = Mathf.Clamp(mouse.y, -90.0f, 60.0f);


        //interpolate camera vector to camera position.
        transform.localRotation = Quaternion.AngleAxis(-mouse.y, Vector3.right);
        Character.transform.localRotation = Quaternion.AngleAxis(mouse.x, Character.transform.up);

        //if "escape" key is pressed, unlock and unhide mouse cursor from screen center.
        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
