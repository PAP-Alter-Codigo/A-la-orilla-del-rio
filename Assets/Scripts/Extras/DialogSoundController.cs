using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogSoundController : MonoBehaviour {
    private WriterAudio writerAudio;

    // Los sonidos por defecto del objeto
    private List<AudioClip> defaultAudioClips;

    private void Start() {
        writerAudio = GetComponent<WriterAudio>();
        if (writerAudio == null ) {
            Debug.LogError("No WriterAudio component attached to this object");
            enabled = false;
            return;
        }
        defaultAudioClips = new(writerAudio.beepSounds);
    }

    public void onCharacterChanged(Character character) {
        if (character != null && character is Personaje personaje) {
            writerAudio.beepSounds = personaje.beepSounds;
        } else {
            writerAudio.beepSounds = defaultAudioClips;
        }
    }
}
