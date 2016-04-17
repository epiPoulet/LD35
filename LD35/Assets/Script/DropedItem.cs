using UnityEngine;
using System.Collections;

public class DropedItem : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D coll) {
		Destroy (this.gameObject);
	}
}
