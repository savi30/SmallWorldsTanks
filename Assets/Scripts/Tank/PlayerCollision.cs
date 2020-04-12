using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {

	public GameObject deathEffect;

	void OnCollisionEnter (Collision col)
	{
		if (col.collider.tag == "Meteor")
		{
			Instantiate(deathEffect, transform.position, transform.rotation);
			Debug.Log("Dead");
			Destroy(gameObject);
		}
	}

}
