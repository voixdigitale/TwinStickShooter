using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Pool;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private float _masterVolume = 1f;
    [SerializeField] private SoundsCollectionSO _soundsCollectionSO;
    [SerializeField] private AudioMixerGroup _sfxMixerGroup;
    [SerializeField] private AudioMixerGroup _musicMixerGroup;

    private ObjectPool<GameObject> _soundObjectPool;

    private void Start() {
        CreateSoundObjectPool();
    }

    private void OnEnable() {
        Shooting.OnShoot += Shooting_OnShoot;
    }

    private void OnDisable() {
        Shooting.OnShoot -= Shooting_OnShoot;
    }

    private void CreateSoundObjectPool() {
        _soundObjectPool = new ObjectPool<GameObject>(() => {
            return CreateSoundObject();              // Fonction de création
        }, soundObject => {
            soundObject.gameObject.SetActive(true);  // OnGet
        }, soundObject => {
            soundObject.gameObject.SetActive(false); // OnRelease
        }, soundObject => {
            Destroy(soundObject);                    // OnDestroy
        }, false,                                    // Erreurs si on "Release" un objet dans la collection
        5,                                          // Taille du tableau pour éviter recréations
        10                                          // Taille maximale du tableau
        );
    }

    private GameObject CreateSoundObject() {
        GameObject soundObject = Instantiate(new GameObject("Pooled Audio Source"));
        soundObject.AddComponent<AudioSource>();

        return soundObject;
    }

    private IEnumerator ReleaseSoundFromPool(GameObject soundObject, float clipLength) {
        yield return new WaitForSeconds(clipLength);
        _soundObjectPool.Release(soundObject);
    }

    private void PlayRandomSound(SoundSO[] sounds) {
        if (sounds != null && sounds.Length > 0) {
            SoundSO soundSO = sounds[Random.Range(0, sounds.Length)];
            SoundToPlay(soundSO);
        }
    }

    private void SoundToPlay(SoundSO soundSO) {
        AudioClip clip = soundSO.Clip;
        float volume = soundSO.Volume * _masterVolume;
        bool loop = soundSO.Loop;
        AudioMixerGroup audioMixerGroup;

        audioMixerGroup = DetermineAudioMixerGroup(soundSO);

        PlaySound(clip, volume, loop, audioMixerGroup);
    }

    private AudioMixerGroup DetermineAudioMixerGroup(SoundSO soundSO) => soundSO.AudioType switch {
        SoundSO.AudioTypes.SFX => _sfxMixerGroup,
        SoundSO.AudioTypes.Music => _musicMixerGroup,
        _ => null,
    };

    private void PlaySound(AudioClip clip, float volume, bool loop, AudioMixerGroup audioMixerGroup) {
        GameObject soundObject = _soundObjectPool.Get();
        AudioSource audioSource = soundObject.GetComponent<AudioSource>();

        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.loop = loop;
        audioSource.outputAudioMixerGroup = audioMixerGroup;
        audioSource.Play();

        if (!loop) { StartCoroutine(ReleaseSoundFromPool(soundObject, clip.length)); }
    }

    private void Shooting_OnShoot() {
        PlayRandomSound(_soundsCollectionSO.PlayerShoot);
    }
}
