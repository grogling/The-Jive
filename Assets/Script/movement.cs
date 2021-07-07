using UnityEngine;
using System.Collections;
using DG.Tweening;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Diagnostics;
using System;

public class movement : MonoBehaviour {
    private float unitspeed = 15f;
    private Vector3 unitposition = new Vector3(0, 0, 0);
    public bool canMove = true , moving=false;
    public GameObject AU;
    public float movelength = 1f; // how far the unit move
    Rigidbody2D rb;

    void Start()
    {
    rb = GetComponent<Rigidbody2D>();
    }

    void Update ()
    {
    AU = GameManager.Instance.ActiveUnit;
    if (AU == gameObject) //gameObject is the object the script is linked to.
    {
        MoveExecution();
    }
    }

  private void Movefun()
  // This function translates directional keys into movement
  {
    bool upPress =  Input.GetKey(KeyCode.UpArrow);
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


    if(directionSum > 1)
    {
        moving = false;
    }
    else if (directionSum == 1)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f);
        if (hit.collider != null)
        {
            moving = false;
        }
        else
        {
            canMove = false;
            moving = true;
            unitposition += direction * movelength; // this is the position the unit will move to when you hit the directional key
        }
    }
  }



	public void MoveExecution()
  // This is the actual execution of the move function
	{
        if (canMove)
		{
            unitposition = transform.position;
			Movefun();
		}
		if (moving)
		{
			if (transform.position==unitposition)
            {
				moving=false;
            }
			transform.position = Vector3.MoveTowards (transform.position, unitposition, Time.deltaTime*unitspeed);
        }
	}
}
