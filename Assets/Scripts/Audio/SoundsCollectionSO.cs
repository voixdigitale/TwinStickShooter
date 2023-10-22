using UnityEngine;

[CreateAssetMenu()]
public class SoundsCollectionSO : ScriptableObject {
    [Header("Music")]
    public SoundSO[] Level1Music;


    [Header("SFX")]
    public SoundSO[] PlayerShoot;
    public SoundSO[] EnemyDeathSFX;
}
