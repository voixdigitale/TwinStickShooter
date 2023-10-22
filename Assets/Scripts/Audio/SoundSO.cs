using UnityEngine;

[CreateAssetMenu()]
public class SoundSO : ScriptableObject {
    public enum AudioTypes {
        SFX,
        Music
    }

    public AudioTypes AudioType;
    public AudioClip Clip;
    public bool Loop = false;
    [Range(.1f, 2f)]
    public float Volume = 1f;
}
