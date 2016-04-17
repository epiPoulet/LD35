using UnityEngine;
using System.Collections;

public class WorldEventScript : MonoBehaviour {

	public float maxEventPerSec;
	public GameObject item;

	private GameObject[] map;
	private float[] spectrum;
	private float currentEvent = 0;

	public void update () {
		currentEvent += Time.deltaTime;
		if (currentEvent >= maxEventPerSec) {
			Instantiate(item, new Vector3(map[Random.Range (0, map.Length)].transform.position.x, 10, 0), item.transform.rotation);
			currentEvent = 0;
		}
	}

	public void sendData(ref GameObject[] _map, ref float[] _spectrum)
	{
		map = _map;
		spectrum = _spectrum;
	}
}
