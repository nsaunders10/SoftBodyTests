﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastBoom : MonoBehaviour {

	public Transform home;
	public float radius = 5.0F;
	public float power = 10.0F;
	public GameObject[] cubes;

	public bool gravityOn;
	public bool kinematic;
	public bool goHome;


	void Start () {
		
	}

	void Update () {

		for(int i=0; i<cubes.Length; i++){

			if (gravityOn) {
				cubes [i].GetComponent<Rigidbody> ().useGravity = true;
			}
			else cubes [i].GetComponent<Rigidbody> ().useGravity = false;

			if (kinematic) {
				cubes [i].GetComponent<Rigidbody> ().isKinematic = true;
			}
			else cubes [i].GetComponent<Rigidbody> ().isKinematic = false;

			if (goHome) {
				cubes [i].transform.position = home.position;
			}
		}

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Input.GetMouseButton(0)) {
			if (Physics.Raycast (ray, out hit, 1000)) {
				Debug.DrawLine (ray.origin, hit.point); 
				Collider[] colliders = Physics.OverlapSphere(hit.point, radius);
				foreach (Collider col in colliders)
				{
					Rigidbody rb = col.gameObject.GetComponent<Rigidbody> ();

					if (rb != null)
						rb.AddExplosionForce(power, hit.point, radius);
				}
			}
		}
	}
}
