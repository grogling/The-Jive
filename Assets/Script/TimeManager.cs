using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Runtime.InteropServices;
using System;
using System.Diagnostics;
using System.CodeDom;

public class TimeManager : MonoBehaviour
{
    public GameObject[] Units = new GameObject[4];
    public GameObject NextUnit; // index used to keep track of the unit after the current unit
    public GameObject ActiveUnit; // the unit currently allowed to move
    public GameObject Queen; // 
    public GameObject activation_signal; // once the signal reaches a unit, it should be able to move
    public float songBpm = 60; //Song beats per minute This is determined by the song you're trying to sync up to
    public float songPosition = 0; //Current song position, in seconds
    public float songPosInBeats = 0;
    public float secPerBeat = 0;
    public float dspSongTime = 0; //How many seconds have passed since the song started
    public AudioSource musicSource; //an AudioSource attached to this GameObject that will play the music.
    public float turn_time = 0;
    public float offbeat_time;
    public float turn_clock = 0;
    public float offbeat_clock = 0;
    public int _activeUnitIndex; // the index of the active unit
    public int _nextUnitIndex = 1; // the index of the next unit set equal to one to always be 1 ahead of the active index
    public float[] SongPosRef = new float [2];
    public float SongPosRefindex = 0;
    public static TimeManager instance;


    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        ActiveUnit = Units[0]; //starts with the first unit in the array
        NextUnit = Units[1];

        musicSource = GetComponent<AudioSource>();
        dspSongTime = (float)AudioSettings.dspTime;
        secPerBeat = 60F / songBpm;
        musicSource.Play();
        turn_time = 1 / (songBpm / 60);
        offbeat_time = 0 - turn_time;
}

    // Update is called once per frame
    void Update()
    {
        songPosition = (float)(AudioSettings.dspTime - dspSongTime);
        songPosInBeats = songPosition / secPerBeat;
        turn_clock = SongPosRef[1] - SongPosRef[0];
        offbeat_clock += Time.deltaTime;
        if (SongPosRefindex==0)
        {
         SongPosRef[0] = songPosition;
         SongPosRefindex++;
        }
        if (SongPosRefindex>=1)
        {
         SongPosRef[1] = songPosition;
         SongPosRefindex++;
            if (SongPosRef[1]-SongPosRef[0] >= turn_time)
            {
                SongPosRef[0] = SongPosRef[1];
                SongPosRefindex = 0;
                _activeUnitIndex++;
                _nextUnitIndex++;


                if (_activeUnitIndex == this.Units.Length) //Once you get through all the units restart
                {
                    _activeUnitIndex = 0;

                }
                if (_nextUnitIndex == this.Units.Length) //Next unit index has to go back to 0 once the active unit index reaches the max
                {
                    _nextUnitIndex = 0;
                }
                ActiveUnit = Units[_activeUnitIndex];
                ActiveUnit.GetComponent<Player>().canMove = true;
                NextUnit = Units[_nextUnitIndex];
                turn_clock = 0;
                ActiveUnit.transform.DOScale(1.5f, 0.1f).OnComplete(() =>
                {
                    ActiveUnit.transform.localScale = new Vector3(1, 1, 1);
                });

            }
        }

        //if (offbeat_clock >= turn_time / 4) // Occurs in between turns
        //{
          //  Queen.transform.DOScale(1.5f, 0.1f).OnComplete(() =>
            //{
              //  Queen.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f); // using .5 instead of 1 since the queen sprite is too large. The initial object is .5 scale
            //});
            //offbeat_clock = 0 - (turn_time / 2); // it has to be offset by .5 seconds
            //Instantiate(activation_signal, Queen.transform.position, Quaternion.identity); //creates the signal to the units at the location of the queen sprite

        }
    }

