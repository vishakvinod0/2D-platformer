using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public float fireRate = 0; //single burst weapon
    public float Damage = 10;
    public LayerMask whatToHit; //tells us what we want to hit

    public Transform BulletTrailPrefab;

    private float timeToFire = 0;
    Transform firePoint;
    
	// Use this for initialization
	void Awake () {
        firePoint = transform.Find("FirePoint");
        if (firePoint == null)
        {
            Debug.LogError("No fire point! WHAT?");
        }

	}

    // Update is called once per frame
    void Update() {
        if (fireRate == 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetButton("Fire1") && Time.time > timeToFire) //for burst weaponry for a later point in time
            {
                timeToFire = Time.time + 1 / fireRate; //speed=dist/time
                Shoot();
            }
        }
    }

        void Shoot()
        {
            //translate screen coords to world coords for raycasting
            Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
            RaycastHit2D Hit = Physics2D.Raycast(firePointPosition, mousePosition - firePointPosition, 100, whatToHit); //arguments are pt of origin, direction, float distance and LayerMask

            Effect();    

            Debug.DrawLine(firePointPosition, (mousePosition-firePointPosition)*100, Color.cyan);

            if(Hit.collider!=null)
            {
                Debug.DrawLine(firePointPosition, Hit.point, Color.red);
                Debug.Log("We hit " + Hit.collider.name + " and did " + Damage + "damage");
            }
        }
        
        void Effect()
        {
            Instantiate(BulletTrailPrefab, firePoint.position, firePoint.rotation);
        }
}
