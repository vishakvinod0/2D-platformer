using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]

public class Tiling : MonoBehaviour {

    public int offsetX = 2; //so as to avoid weird clipping errors

    //checking if we need to instantiate stuff
    public bool hasARightBuddy = false; 
    public bool hasALeftBuddy = false;

    public bool reverseScale = false; //used if object is not tileable
    private float spriteWidth = 0f; //width of our element
    private Camera cam; //ups performance
    private Transform myTransform; //ups performance

    void Awake()
    {
        cam = Camera.main;
        myTransform = transform; 
    }
    // Use this for initialization
    void Start () {
        SpriteRenderer sRend = GetComponent<SpriteRenderer>(); //attach SR from gameobject onto variable of type SR
        spriteWidth = sRend.sprite.bounds.size.x; //gets size of sprite
	}
	
	// Update is called once per frame
	void Update () {
		if(hasALeftBuddy == false || hasARightBuddy == false) //does it need Buddies? 
        {
            //calculate MainCamera's extent from centre in world coords
            float camHorizontalExtent = cam.orthographicSize * Screen.width / Screen.height;
            //calculate x position where camera can see edge of sprite
            float edgeVisiblePositionRight = (myTransform.position.x + spriteWidth / 2) - camHorizontalExtent;
            float edgeVisiblePositionLeft = (myTransform.position.x - spriteWidth / 2) + camHorizontalExtent;
            //checking if we can see element's edge and calling MakeNewBuddy if we can
            if(cam.transform.position.x >= edgeVisiblePositionRight - offsetX && hasARightBuddy == false)
            {
                MakeNewBuddy(1); //calls function that creates Buddy on the right side
                hasARightBuddy = true;         
            }
            else if (transform.position.x <= edgeVisiblePositionLeft + offsetX && hasALeftBuddy == false)
            {
                MakeNewBuddy(-1);
                hasALeftBuddy = true;
            }
        }
	}
    void MakeNewBuddy (int rightOrLeft) //function that creates a Buddy on the required side
    {
        //calculating new position for newly generated Buddy
        Vector3 newPosition = new Vector3(myTransform.position.x + (myTransform.localScale.x * spriteWidth * rightOrLeft), myTransform.position.y, myTransform.position.z);
        Transform newBuddy = Instantiate (myTransform, newPosition, myTransform.rotation) as Transform; //instantiating and casting as type Transform

        //if not tileable, reverse x size of object to get rid of ugly seams
        if(reverseScale == true)
        {
            newBuddy.localScale = new Vector3(newBuddy.localScale.x * -1, newBuddy.localScale.y, newBuddy.localScale.z); //invert size of mountains
        }

        newBuddy.parent = myTransform.parent;
        if(rightOrLeft>0) //rOL > 0 means we have already instantiated a newBuddy to the right of foreground dirt
        {
            newBuddy.GetComponent<Tiling>().hasALeftBuddy = true;
        }
        else if(rightOrLeft<0)
        {
            newBuddy.GetComponent<Tiling>().hasARightBuddy = true;
        }
    }
}
