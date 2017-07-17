using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine.UI;

public class ZombieScript : MonoBehaviour {
	private Transform goal;
	private NavMeshAgent agent;
	public GvrAudioSource[] AudioClips = null;

	// Use this for initialization
	void Start () {

		goal = Camera.main.transform;
		agent = GetComponent<NavMeshAgent>();
		agent.destination = goal.position;

		GetComponent<Animation>().Play ("walk");
		if (!AudioClips[0].isPlaying)
		{
			AudioClips[0].Play();
		}
	}


	//for this to work both need colliders, one must have rigid body, and the zombie must have is trigger checked.
	void OnTriggerEnter (Collider col)
	{
		//first disable the zombie's collider so multiple collisions cannot occur
		GetComponent<CapsuleCollider>().enabled = false;
		Destroy(col.gameObject);
		//stop the zombie from moving forward by setting its destination to it's current position
		agent.destination = gameObject.transform.position;
		//stop the walking animation and play the falling back animation
		GetComponent<Animation>().Stop ();
		GetComponent<Animation>().Play ("back_fall");
		if (!AudioClips[1].isPlaying)
		{
			AudioClips[1].Play();
		}
		Destroy (gameObject, 6);
		//instantiate a new zombie
		GameObject zombie = Instantiate(Resources.Load("Zombie", typeof(GameObject))) as GameObject;

		//set the coordinates for a new vector 3
		float randomX = UnityEngine.Random.Range (-12f,12f);
		float constantY = .01f;
		float randomZ = UnityEngine.Random.Range (-13f,13f);
		//set the zombies position equal to these new coordinates
		zombie.transform.position = new Vector3 (randomX, constantY, randomZ);

		//if the zombie gets positioned less than or equal to 3 scene units away from the camera we won't be able to shoot it
		//so keep repositioning the zombie until it is greater than 3 scene units away. 
		while (Vector3.Distance (zombie.transform.position, Camera.main.transform.position) <= 3) {

			randomX = UnityEngine.Random.Range (-12f,12f);
			randomZ = UnityEngine.Random.Range (-13f,13f);

			zombie.transform.position = new Vector3 (randomX, constantY, randomZ);
		}

	}

	public void Die(){
		
		agent.destination = gameObject.transform.position;
		GetComponent<Animation>().Stop ();
		GetComponent<Animation>().Play ("back_fall");
		if (!AudioClips[1].isPlaying)
		{
			AudioClips[1].Play();
		}
		Destroy (gameObject, 6);

		GameObject zombie = Instantiate(Resources.Load("Zombie", typeof(GameObject))) as GameObject;

		//set the coordinates for a new vector 3
		float randomX = UnityEngine.Random.Range (-12f,12f);
		float constantY = .01f;
		float randomZ = UnityEngine.Random.Range (-13f,13f);
		//set the zombies position equal to these new coordinates
		zombie.transform.position = new Vector3 (randomX, constantY, randomZ);

		//if the zombie gets positioned less than or equal to 3 scene units away from the camera we won't be able to shoot it
		//so keep repositioning the zombie until it is greater than 3 scene units away. 
		while (Vector3.Distance (zombie.transform.position, Camera.main.transform.position) <= 3) {

			randomX = UnityEngine.Random.Range (-12f,12f);
			randomZ = UnityEngine.Random.Range (-13f,13f);

			zombie.transform.position = new Vector3 (randomX, constantY, randomZ);
		}
	}    

}