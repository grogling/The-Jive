using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;

public class activation_signal : MonoBehaviour {

    RaycastHit2D hit;
    private float velx = 0;
    private float vely = 0;
    private int PlayerMask = 0;
    private float turn_time;
    private float current_songPosition;
    private float time_delta = 0;
    private float destroyaftertime = 0;
    private Vector3 nextunit_POS = new Vector3(0, 0, 0);
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb;
    public GameObject NextUnit;
    


	void Start ()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        rb.mass = 0; //so it won't inact a force on the units
        NextUnit = TimeManager.instance.NextUnit;
        nextunit_POS = NextUnit.transform.position; //get next game object position
        time_delta = TimeManager.instance.turn_time/2; //two because its half a beat
        velx = (nextunit_POS.x - transform.position.x)/time_delta; //x velocity
        vely = (nextunit_POS.y - transform.position.y) / time_delta;//y velocity
        current_songPosition = TimeManager.instance.songPosition;
        turn_time = TimeManager.instance.turn_time/2;
        destroyaftertime = current_songPosition  + turn_time;
    
    }

	// Update is called once per frame
	void Update ()
    {
        rb.velocity = new Vector3 (velx, vely, 0);
        if (TimeManager.instance.songPosition>= destroyaftertime)
        {
            Destroy(gameObject);
        }


    }
}
