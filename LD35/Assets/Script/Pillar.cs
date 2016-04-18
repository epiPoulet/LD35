using UnityEngine;
using System.Collections;

public class Pillar : MonoBehaviour {
	
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Event") {
			this.gameObject.GetComponentInChildren<Renderer> ().material.color = Color.red;
		}
	}
}
