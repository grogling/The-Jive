using UnityEngine;
using System.Collections;
using DG.Tweening;

public class GameManager : MonoBehaviour {


    public static GameManager instance = null;
    // Use this for initialization
    void Awake ()
    {
        if (instance = null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

    }

	void Update () 
    {
		
    }
}
