using UnityEngine;
using System.Collections;
using DG.Tweening;

public class GameManager : MonoBehaviour {


    public static GameManager instance = null;
    public BoardManager boardScript;
    public TimeManager timeScript;

    
    void Awake ()
    {
       // if (instance = null)
       //     instance = this;
      //  else if (instance != this)
       //     Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        boardScript = GetComponent<BoardManager>();
        timeScript = GetComponent<TimeManager>();
        InitializeGame();
    }

    void Update()
    {
        timeScript.initializeTurn();
        
    }

    void InitializeGame()
    {
        boardScript.SetupScene();
        timeScript.initializeMusic();
    }
}




