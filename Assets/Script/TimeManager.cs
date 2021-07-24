using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{ 
    public float songBpm = 0; //Song beats per minute This is determined by the song you're trying to sync up to
    public float secPerBeat = 0; //The number of seconds for each song beat
    public float songPosition = 0; //Current song position, in seconds
    public float songPositionInBeats = 0;//Current song position, in beats
    public float dspSongTime = 0; //How many seconds have passed since the song started
    public AudioSource musicSource; //an AudioSource attached to this GameObject that will play the music.

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
