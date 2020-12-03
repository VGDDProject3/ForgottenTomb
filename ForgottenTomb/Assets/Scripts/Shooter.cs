using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    #region Editor Variables
    [SerializeField]
    private GameObject projectile;

    [SerializeField]
	private float timeToReload;

    [SerializeField]
	private float fireSpeed;

    [SerializeField]
    // Time for bullet to exist before despawn
	private float bulletTime = 5;

    [SerializeField]
    [Tooltip("Shoot right (1), left (-1), neither (0)")]
	private int xdir = 1;

    [SerializeField]
    [Tooltip("Shoot up (1), down (-1), neither (0)")]
	private int ydir = 0;
    #endregion

    float reloadTimer = 0;

    // Update is called once per frame
    void Update() {
		// check if shooter is reloaded
		if (reloadTimer > timeToReload) {
            reloadTimer = 0;
            //calculate the trajectory vector
            Vector2 FireDirection = new Vector2 (xdir, ydir);
            FireDirection = FireDirection.normalized * fireSpeed;

            // Create the projectile and Access its Rigidbody to add force
            GameObject newProjectile = (GameObject) Instantiate (projectile, transform.position, transform.rotation);
            newProjectile.GetComponent<Rigidbody2D> ().velocity = FireDirection;

            //If the projectile exists after bulletTime seconds, destroy it
            Destroy (newProjectile, bulletTime);
		} else {
			reloadTimer += Time.deltaTime;
		}
	}
}
