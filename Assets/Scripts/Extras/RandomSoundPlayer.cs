using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSoundPlayer : MonoBehaviour{
    [SerializeField]
    private List<AudioClip> audios;
    [SerializeField, Tooltip("Tiempo entre la reproducción de sonidos en un rango aleatorio")]
    private float minSeconds, maxSeconds;

    private AudioSource audioSource;
    private bool routineStarted = false;

    void Start() {
        audioSource = gameObject.GetComponent<AudioSource>();

        // Anti errores
        if(audios.Count < 1) {
            if(audioSource.clip){
                audios = new List<AudioClip>(){
                    audioSource.clip
                };
            }else {
                routineStarted = true;
            }
        }

        if(minSeconds < 0.0f) {
            minSeconds = 0.0f;
        }
        if(minSeconds <= maxSeconds) {
            maxSeconds += 1.0f;
        }
    }

    void Update(){
        if(!audioSource.isPlaying && !routineStarted) {
            routineStarted = true;
            StartCoroutine(PlaySoundAndWait());
        }
    }

    private IEnumerator PlaySoundAndWait() {
        yield return new WaitForSecondsRealtime(Random.value * (maxSeconds - minSeconds) + minSeconds);
        audioSource.clip = audios[(int) (Random.value * audios.Count)];
        audioSource.Play();
        routineStarted = false;
    }
}
