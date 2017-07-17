using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {

	private static GameObject gun;
	public GameObject spawnPoint;
	public GameObject bullet;
	private bool isShooting;
	public AudioSource[] AudioClips = null;

	// Use this for initialization
	void Start () {

		gun = gameObject.transform.GetChild (0).gameObject;
		spawnPoint = gun.transform.GetChild (0).gameObject;

		isShooting = false;
	}

	//Shoot function is IEnumerator so we can delay for seconds
	IEnumerator Shoot() {
		isShooting = true;
		bullet = Instantiate(Resources.Load("Bullet", typeof(GameObject))) as GameObject;
		Rigidbody rb = bullet.GetComponent<Rigidbody>();
		bullet.transform.rotation = spawnPoint.transform.rotation;
		bullet.transform.position = spawnPoint.transform.position;
		rb.AddForce(spawnPoint.transform.forward * 500f);
		if (!AudioClips[0].isPlaying)
		{
			AudioClips[0].Play();
		}
		gun.GetComponent<Animation>().Play ("MachineGin_shoot");
		Destroy (bullet, 1);
		//wait for 1 second and set isShooting to false so we can shoot again
		yield return new WaitForSeconds (1f);
		isShooting = false;
	}

	// Update is called once per frame
	void Update () {

		RaycastHit hit;
		//draw the ray for debuging purposes (will only show up in scene view)
		Debug.DrawRay(spawnPoint.transform.position, spawnPoint.transform.forward, Color.green);

		//cast a ray from the spawnpoint in the direction of its forward vector
		if (Physics.Raycast(spawnPoint.transform.position, spawnPoint.transform.forward, out hit, 100)){

			//if the raycast hits any game object where its name contains "zombie" and we aren't already shooting we will start the shooting coroutine
			if (hit.collider.gameObject.tag == "zombie") {
				gun.GetComponent<Animation>().Play ("MachineGin_shoot");
				if (!isShooting) {
					StartCoroutine ("Shoot");
				}

			}

		}

	}

}