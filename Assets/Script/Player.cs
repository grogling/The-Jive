using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Player : MovingObject
{
    public int wallDamage = 1;
    public int enemyDamage = 1;
    public GameObject ActiveUnit;

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
        ActiveUnit = TimeManager.instance.ActiveUnit;
        if (ActiveUnit == gameObject)
        {
            if (canMove)
            {
                unitposition = transform.position;
                Movefun();
            }
            if (moving)
            {
                if (transform.position == unitposition)
                {
                    moving = false;
                }
                transform.position = Vector3.MoveTowards(transform.position, unitposition, Time.deltaTime * unitspeed);
            }
        }
        
    }

    private void Movefun()
    // This function translates directional keys into movement
    {
        RaycastHit2D hit;

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
        Vector3 direction = new Vector3(rightInt - leftInt, upInt - downInt,0);
        if (directionSum > 1 )
        {
            moving = false;
        }
        else if (directionSum == 1)
        {
            AttemptMove(rightInt - leftInt, upInt - downInt, out hit);            
        }

        
    }

    private void OnDisable ()
    // This function is called when the behavior becomes inactive.
    {
        //reserve this to pass info to the gamemanager about unit upgrades
    }


}
