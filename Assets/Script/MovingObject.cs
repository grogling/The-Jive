using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEngine;

public abstract class MovingObject : MonoBehaviour
{
    public float unitspeed = 15f;
    public float movelength = 1f;
    public bool canMove = true, moving = false;

    private BoxCollider2D boxCollider;
    private Rigidbody2D rb2D;


    // Protected Virtual Functions can be overwritten by inheriting class
    protected virtual void Start()
    {
        
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    //Returns true if able to move
    protected bool Move (int xDir, int yDir, out RaycastHit2D hit)
    {
        Vector3 start = transform.position;
        Vector3 end = start + new Vector3(xDir, yDir, 0);
        Vector3 direction = end - start;
        hit = Physics2D.Raycast(transform.position, direction, 1f);
                
        if (hit.collider ==null)
        //check if raycast does not hit, move
        {
            transform.position = Vector3.MoveTowards(start, end, Time.deltaTime * unitspeed);
            return true;
        }
        else
        // if raycast hits can't move
        {
            moving = false;
            return false;
        }
    }

    
    protected virtual void AttemptMove (int xDir, int yDir)
    {
        RaycastHit2D hit;

        bool canMove = Move(xDir, yDir, out hit);

        if (hit.collider == null) return;
    }
}


