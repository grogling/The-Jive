using System.Collections;
using UnityEngine;

public abstract class MovingObject : MonoBehaviour
{
    public float unitspeed = 15f;
    public float movelength = 1f;
    public bool canMove = true, moving = false;
    public Vector3 unitposition = new Vector3(0, 0, 0);

    private BoxCollider2D boxCollider;
    private Rigidbody2D rb2D;


    // Protected Virtual Functions can be overwritten by inheriting class
    protected virtual void Start()
    {

        Physics2D.queriesStartInColliders = false;
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    //Returns true if able to move
    protected virtual void AttemptMove (int xDir, int yDir, out RaycastHit2D hit)
    {
        Vector3 start = transform.position;
        Vector3 direction = new Vector3(xDir, yDir, 0);
        hit = Physics2D.Raycast(transform.position, direction, 1f);
                
        if (hit.collider != null)
        {
            moving = false;
            return;
        }
        else
        // if raycast doesn't hit
        {
            canMove = false;
            moving = true;
            unitposition += direction*movelength; 
            return;

        }
    }
}


