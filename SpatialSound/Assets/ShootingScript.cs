using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour {
	private ParticleSystem bullets;
	// Use this for initialization
	void Start () {
		bullets = this.GetComponent<ParticleSystem>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartShooting(){
		if (!bullets.isPlaying) {
			bullets.Play ();
		}
	}

	public void StopShooting(){
		if (bullets.isPlaying) {
			bullets.Stop ();
		}
	}
}
