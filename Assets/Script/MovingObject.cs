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
            StartCoroutine(Moving(end));
            return true;
        }
        else
        // if raycast hits can't move
        {
            return false;
        }
    }

    protected IEnumerator Moving (Vector3 end)
    {
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;
        while(sqrRemainingDistance >float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(rb2D.position, end, Time.deltaTime * unitspeed);
            rb2D.MovePosition(newPosition);
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;
            yield return null;
        }
    }

}


