using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuManagerScript : MonoBehaviour {

    public Dropdown dropdown;

    private AudioClip[] clips;

	// Use this for initialization
	void Start ()
    {
        clips = Resources.LoadAll<AudioClip>("");
        dropdown.options.Clear();
        for (int i = 0; i < clips.Length; ++i)
        {
            dropdown.options.Add(new Dropdown.OptionData() { text = clips[i].name });
        }
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void SetSelectedClip()
    {
        DataKeeper.Clip = clips[dropdown.value];

        Application.LoadLevel("scene1");
    }
}
