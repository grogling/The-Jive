using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovingObject
{
    public int wallDamage = 1;
    public int enemyDamage = 1;
    public GameObject AU;
    public bool canMove = true;

    private Animator animator;
    private int health;

    protected override void Start()
    {
        animator = GetComponent<Animator>();
        //upgrade = GameManager.instance.upgrades

        //call the start function of MovingObject Class
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        AU = GameManager.Instance.ActiveUnit;
        if (AU == gameObject || canMove == true)
        {
            canMove = false;
            Movefun(); //gameObject is the object the script is linked to.
            canMove = false;
        }
        
    }

    private void Movefun()
    // This function translates directional keys into movement
    {
        bool upPress = Input.GetKey(KeyCode.UpArrow);
        bool downPress = Input.GetKey(KeyCode.DownArrow);
        bool leftPress = Input.GetKey(KeyCode.LeftArrow);
        bool rightPress = Input.GetKey(KeyCode.RightArrow);
        int upInt, downInt, leftInt, rightInt;

        upInt = upPress ? 1 : 0;
        downInt = downPress ? 1 : 0;
        leftInt = leftPress ? 1 : 0;
        rightInt = rightPress ? 1 : 0;

        int directionSum = upInt + downInt + leftInt + rightInt;
        Vector3 direction = new Vector3(rightInt - leftInt, upInt - downInt, 0);
        if (directionSum == 1 || canMove == true)
        {
            canMove = false;
            AttemptMove(rightInt - leftInt, upInt - downInt);
        }
        
    }

    private void OnDisable ()
    // This function is called when the behavior becomes inactive.
    {
        //reserve this to pass info to the gamemanager about unit upgrades
    }


}
