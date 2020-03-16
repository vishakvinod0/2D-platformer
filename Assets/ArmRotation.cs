using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Can just use transform.lookat(), but it does not give enough control, i.e, like rotating only on the XY plane which is our intention
public class ArmRotation : MonoBehaviour {

    public int rotOffset = 90;
	
	// Update is called once per frame
	void Update ()
    {
        //Subtracting position of PlayerOne from mousePosition
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position; 
        difference.Normalize(); //Normalizing the vector; i.e, sum of the components of vector is = 1

        //Atan2 calculates distance between origin and PlayerOne and calculates angle of rotation. Rad2Deg because degrees easier to work with
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + rotOffset);

	}
}
