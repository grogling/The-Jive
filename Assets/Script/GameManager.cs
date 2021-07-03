using UnityEngine;
using System.Collections;
using DG.Tweening;

public class GameManager : MonoBehaviour {

	public GameObject[] Units = new GameObject[4];
    public GameObject NextUnit; // index used to keep track of the unit after the current unit
	public GameObject ActiveUnit; // the unit currently allowed to move
    public GameObject deadpulser; // The queen
    public GameObject activation_signal; // once the signal reaches a unit, it should be able to move
    public float activation_frequency = 60;
    public float activation_period = 0;


    private int _activeUnitIndex; // the index of the active unit
    private int _nextUnitIndex = 1; // the index of the next unit set equal to one to always be 1 ahead of the active index
    private GameObject signalClone;
    

    public AudioClip _beat;
    public AudioSource _beatsource;

    private float _currentInterval = 0; // the intitiation of the timing
    private float _deadpulse = 0; // be one second off the unit control

    public static GameManager Instance; 

    // Use this for initialization
    void Awake ()
    {
        activation_period = 1 / (activation_frequency / 60);
        _deadpulse = 0 - activation_period;
        ActiveUnit = Units [0]; //starts with the first unit in the array
        NextUnit = Units[1];
        Instance = this;

	}

	void Update () {

		_currentInterval += Time.deltaTime; //records the time passed
        _deadpulse += Time.deltaTime; //records an offset time passed
        

		if (_currentInterval >= activation_period)
        {
            
            _activeUnitIndex++;
            _nextUnitIndex++;
			if (_activeUnitIndex == this.Units.Length) //Once you get through all the units restart
            {
				_activeUnitIndex = 0;
               // _beatsource.Play();
			}
            if (_nextUnitIndex == this.Units.Length) //Next unit index has to go back to 0 once the active unit index reaches the max
            {
                _nextUnitIndex = 0;
            }
            ActiveUnit = Units[_activeUnitIndex];
            NextUnit = Units[_nextUnitIndex];

     
			_currentInterval = 0;

            // pop the unit when selected
           // _beatsource.Play();
            ActiveUnit.transform.DOScale(1.5f, 0.1f).OnComplete(() =>
            {
                ActiveUnit.transform.localScale = new Vector3(1, 1, 1);
                Destroy(signalClone); // remove signal clone
            });
        
		}
        if (_deadpulse >= activation_period/2) // it has to twice the activation frequency
        {
            deadpulser.transform.DOScale(1.5f, 0.1f).OnComplete(() =>
            {
                deadpulser.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f); // using .5 instead of 1 since the queen sprite is too large. The initial object is .5 scale
            });
            _deadpulse = 0-(activation_period/2); // it has to be offset by .5 seconds
            signalClone = Instantiate(activation_signal, deadpulser.transform.position, Quaternion.identity); //creates the signal to the units at the location of the queen sprite
           
        }



    }
}
