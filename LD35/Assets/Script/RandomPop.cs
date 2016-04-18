using UnityEngine;
using System.Collections;

public class RandomPop : AEvent {

	public float popTimeMin;
	public float popTimeMax;

	private float popTime = 1;

	public override void update () {
		currentEvent += Time.deltaTime;
		if (currentEvent >= popTime && ActiveSound()) {
			Instantiate(item, new Vector3(map[Random.Range (0, map.Length)].transform.position.x, 10, 0), item.transform.rotation);
			currentEvent = 0;
			popTime = Random.Range (popTimeMin, popTimeMax);
		}
	}

	private bool ActiveSound() {
		for (int i = 0; i < spectrum.Length; ++i)
			if (spectrum [i] > 0.1f)
				return (true);
		return (false);
	}
}
