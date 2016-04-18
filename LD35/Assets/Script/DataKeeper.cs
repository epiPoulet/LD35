using UnityEngine;
using System.Collections;

public static class DataKeeper
{
    private static AudioClip clip;

    public static AudioClip Clip
    {
        get
        {
            return clip;
        }

        set
        {
            clip = value;
        }
    }
}