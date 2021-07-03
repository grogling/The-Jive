using UnityEngine;
using System.Collections;
using DG.Tweening;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Diagnostics;
using System;

public class movement : MonoBehaviour {

    private float unitspeed = 15f;
    private Vector3 unitposition= new Vector3(0,0,0);
	private bool canMove = true , moving=false;
    public GameObject AU;
    public float movelength = 2f; // how far the unit moves
    private float cooldowntime =1f; // cooldown on moving
    private float nextmovetime = 0f; // the next time you are able to move

    
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }
    void Update ()
    {
        AU = GameManager.Instance.ActiveUnit;
        
        if (AU == gameObject)
        {
            MoveExecution();
        }


    }

    // This function translates directional keys into movement
    private void Movefun() 
	{
        if (Time.time > nextmovetime)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 1f, 1 << 8);
                if (hit.collider != null)
                {
                    canMove = true;
                    moving = false;
                }
                else
                {
                    canMove = false;
                    moving = true;
                    unitposition += Vector3.up * movelength; // this is the position the unit will move to when you hit the directional key
                    nextmovetime = Time.time + cooldowntime; // this let's you only input one movement command per cycle
                }
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, 1 << 8);
                if (hit.collider != null)
                {
                    canMove = true;
                    moving = false;
                }
                else
                {
                    canMove = false;
                    moving = true;
                    unitposition += Vector3.down * movelength;
                    nextmovetime = Time.time + cooldowntime;
                }
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 1f, 1 << 8);
                if (hit.collider != null)
                {
                    canMove = true;
                    moving = false;
                }
                else
                {
                    canMove = false;
                    moving = true;
                    unitposition += Vector3.right * movelength;
                    nextmovetime = Time.time + cooldowntime;
                }
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, 1f, 1 << 8);
                if (hit.collider != null)
                {
                    canMove = true;
                    moving = false;
                }
                else
                {
                    canMove = false;
                    moving = true;
                    unitposition += Vector3.left * movelength;
                    nextmovetime = Time.time + cooldowntime;
                }
            }
        }
	}
    // This is the actual execution of the move function
	public void MoveExecution() 
	{
        
		if (canMove) 
		{
			
            unitposition = transform.position;
			Movefun ();
			
		}
		if (moving) 
		{
			if (transform.position==unitposition)
			{
				moving=false;
				canMove= true;			
				Movefun ();
			}
			transform.position = Vector3.MoveTowards (transform.position, unitposition, Time.deltaTime*unitspeed);
            
        }
	}
}
