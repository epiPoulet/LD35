using UnityEngine;
using System.Collections;

public abstract class AEvent : MonoBehaviour {

	public GameObject item;

	protected GameObject[] map;
	protected float[] spectrum;
	protected float currentEvent = 0;

	public abstract void update ();

	public void sendData(ref GameObject[] _map, ref float[] _spectrum)
	{
		map = _map;
		spectrum = _spectrum;
	}
}
