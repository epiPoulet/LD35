using UnityEngine;
using System.Collections;
using System;

public class WorldManagerScript : MonoBehaviour {

	public GameObject pillar; // pillar display object
	public GameObject[] Event;
	public int spectrumRate; // "1 / spectrumRate" give the number of spectrum refresh per second 
	public float maxAditionalHeight;
	public int elevationStrenght;

	private GameObject[] map; // array of pillar
	private int spectreSize = 64; // number for fft of audioAnalyse
	private int nbPillar = 32; //number of pillar making the ground
	private int heightPillar = -8; //pillar position in world

	private AudioSource audioSound; // use for stop/play the sound.
	private AudioSource audioAnalyse; // this variable can take the sound at T + 1 (T = 1 / spectrumRate)
	private float[] spectrum; // spectre of audioAnalyse
	private float[] lastPos; //previous position af all 

	private float currentTime; // current time betwen 0 and "1 / spectrumRate"
	private float timePosition;  // give % of current time betwen 0 and "1 / spectrumRate"

	void Start () {
		audioSound = GetComponents<AudioSource>()[0];
        audioSound.clip = DataKeeper.Clip;
		audioSound.Pause ();
		audioAnalyse = GetComponents<AudioSource>()[1];
        audioAnalyse.clip = DataKeeper.Clip;
        audioAnalyse.Play ();
		spectrum = new float[spectreSize];
		lastPos = new float[nbPillar];
		currentTime = 0;

		float pillarScale = 20.60f / nbPillar;

		map = new GameObject[nbPillar];
		for (int i = 0; i < nbPillar; ++i) {
			map [i] = (GameObject)Instantiate (pillar, new Vector3(i * pillarScale, heightPillar, 0), pillar.transform.rotation);
			map[i].transform.localScale -= new Vector3 (1 - pillarScale, 0, 0);
		}
		for (int i = 0; i < Event.Length; ++i)
			Event [i].GetComponent<AEvent> ().sendData
			(ref map, ref spectrum);
	}

	void Update () {
		if (currentTime >= (float)1 / spectrumRate) {
			if (!audioSound.isPlaying)
				audioSound.Play ();
			audioAnalyse.GetSpectrumData (spectrum, 0, FFTWindow.BlackmanHarris);
			for (int i = 0; i < nbPillar; ++i)
				lastPos[i] = map [i].transform.position.y;
			currentTime = 0;
		} else {
			currentTime += Time.deltaTime;
		}
		if (audioSound.isPlaying) {
			if (currentTime > (float)1 / spectrumRate)
				currentTime = (float)1 / spectrumRate;
			timePosition = currentTime / ((float)1 / spectrumRate) ;
			ground1 ();
		}
		for (int i = 0; i < Event.Length; ++i)
			Event [i].GetComponent<AEvent> ().update ();
	}

	// ground moving function

	private void ground0()
	{
		for (int i = 0; i < nbPillar; ++i) {
			map [i].transform.position = new Vector3 (map [i].transform.position.x, getYposition0(i), 0);
		}
	}

	private void ground1()
	{
		for (int i = nbPillar / 2 - 1; i >= 0; --i) {
			map [i].transform.position = new Vector3 (map [i].transform.position.x, getYposition1(i), 0);
		}
		for (int i = nbPillar / 2; i < nbPillar; ++i)
			map [i].transform.position = new Vector3 (map [i].transform.position.x, map[(nbPillar / 2 - 1) - (i - (nbPillar / 2))].transform.position.y, 0);
	}

	// manage the y position of pillar with respect to refresh time

	private float getYposition0(int i)
	{
		float a;
		a = (heightPillar + ((1 - Mathf.Exp (-spectrum [i] * elevationStrenght)) * maxAditionalHeight)) - lastPos [i];
		return (lastPos [i] + a * timePosition);
		//return (heightPillar + (timePosition * ((1 - Mathf.Exp (-spectrum [i] * elevationStrenght)) * maxAditionalHeight)) );
	}

	private float getYposition1(int i)
	{
		float a;
		a = (heightPillar + ((1 - Mathf.Exp (-spectrum [(nbPillar / 2 - 1) - i] * elevationStrenght)) * maxAditionalHeight)) - lastPos [i];
		return (lastPos [i] + a * timePosition);
		//return (heightPillar + (timePosition * ((1 - Mathf.Exp (-spectrum [i] * elevationStrenght)) * maxAditionalHeight)) );
	}
}
