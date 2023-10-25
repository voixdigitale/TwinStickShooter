using UnityEngine;

[CreateAssetMenu()]
public class SoundsCollectionSO : ScriptableObject {
    [Header("Music")]
    public SoundSO[] Level1Music;


    [Header("SFX")]
    public SoundSO[] PlayerShoot;
    public SoundSO[] PlayerDeath;
    public SoundSO[] PlayerHit;
    public SoundSO[] EnemyDeath;
    public SoundSO[] EnemyHit;
    public SoundSO[] EnemyShoot;
}
