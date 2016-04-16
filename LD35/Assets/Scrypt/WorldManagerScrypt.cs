using UnityEngine;
using System.Collections;

public class WorldManagerScrypt : MonoBehaviour {
	
	public GameObject pillar;

	private GameObject[] map;
	private int nbPillar = 128;

	void Start () {
		float pillarScale = 20.55f / nbPillar;
		map = new GameObject[nbPillar];
		for (int i = 0; i < nbPillar; ++i) {
			map [i] = (GameObject)Instantiate (pillar, new Vector3(i * pillarScale, -8, 0), pillar.transform.rotation);
			map[i].transform.localScale -= new Vector3 (1 - pillarScale, 0, 0);
		}
	}

	void Update () {

	}
}
