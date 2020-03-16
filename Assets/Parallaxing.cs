using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour {

    public Transform[] backgrounds; //Array of all backgrounds and foreground to be parallaxed
    private float[] parallaxScales; //Proportion by which the MainCamera moves the backgrounds  
    public float smoothing = 1f; //How smooth the parallaxing will be; make sure to set it >0
    private Transform cam; //Reference to MainCamera's transform
    private Vector3 prevCamPos; //Stores position of camera in previous frame
    
   //Called before Start() but after gameobjects are set up. Apt for references
    void Awake()
    {
        //Set up the camera reference
        cam = Camera.main.transform;
    }


    //Use this for initialization
    void Start()
    {
        //Assign previous frame to current frame's CamPos
        prevCamPos = cam.position;

        //Assigning corresponding parallaxScales
        parallaxScales = new float[backgrounds.Length];
        for(int i = 0; i<backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z * -1;

        }
	}
	
	//Update is called once per frame
	void Update()
    {
        //for each background
        for(int i = 0; i<backgrounds.Length; i++)
        {
            //The parallax is the opposite of camera movement because previous frame is multiplied by the scale
            float parallax = (prevCamPos.x - cam.position.x) * parallaxScales[i];

            //Set a target x position, i.e, current position plus parallax
            float backgroundTargetPosX = backgrounds[i].position.x + parallax;

            //Create a target position, i.e, background's current position with its target x position
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            //Fade between current position and target position using Lerp
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        //Set prevCamPos to the camera's position at the end of the frame
        prevCamPos = cam.position;
	}
}
