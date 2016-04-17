using UnityEngine;
using System.Collections;

public class LinePop : AEvent {

	public float neededPower;
	public int number;

	public float popTimeMin;
	public float popTimeMax;

	private float popTime = 1;

	public override void update () {
		currentEvent += Time.deltaTime;
		if (currentEvent >= popTime && checkSoundPower()) {
			int randItem = Random.Range (0, map.Length);
			for (int i = 0; randItem + i < map.Length && i < 4; ++i) {
				Instantiate (item, new Vector3 (map [randItem + i].transform.position.x, 10, 0), item.transform.rotation);
			}
			popTime = Random.Range (popTimeMin, popTimeMax);
			currentEvent = 0;
		}
	}

	private bool checkSoundPower() {
		int nbOverPower = 0;

		for (int i = 0; i < spectrum.Length; ++i)
			if (spectrum [i] > neededPower)
				++nbOverPower;
		if (nbOverPower >= number) {
			return true;
		}
		return false;
	}
}
