using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class activation_signal : MonoBehaviour {

    private float velx = 0;
    private float vely = 0;
    private float time_delta = 0;
    private Vector3 nextunit_POS = new Vector3(0, 0, 0);
    //private float _magnitude;


    Rigidbody2D rb;


    public GameObject NextUnit;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.mass = 0; //so it won't inact a force on the units
        NextUnit = TimeManager.instance.NextUnit;
        nextunit_POS = NextUnit.transform.position; //get next game object position
        time_delta = TimeManager.instance.turn_time/2; //two because its half a beat
// old way
        velx = (nextunit_POS.x - transform.position.x)/time_delta;
        vely = (nextunit_POS.y - transform.position.y)/time_delta;

	}

	// Update is called once per frame
	void Update ()
    {
        rb.velocity = new Vector3 (velx, vely, 0); // takes 2 seconds to activate each unit

        if (transform.position == nextunit_POS)
        {
            Destroy(gameObject);
        }


    }
}
