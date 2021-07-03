using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activation_signal : MonoBehaviour {

    private float velx = 0;
    private float vely = 0;
    private Vector3 activeunit_POS = new Vector3(0, 0, 0);
    //private float _magnitude;
   

    Rigidbody2D rb;
 

    public GameObject AU;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.mass = 0; //so it won't inact a force on the units
        AU = GameManager.Instance.NextUnit; //instaniate active game object
        activeunit_POS = AU.transform.position; //get active game object position
// old way
        velx = activeunit_POS.x - transform.position.x;
        vely = activeunit_POS.y - transform.position.y;

        // end old way

       // _magnitude = Mathf.Sqrt(Mathf.Pow((AU.transform.position.x - transform.position.x),2) + Mathf.Pow((AU.transform.position.y - transform.position.y),2));
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        rb.velocity = new Vector3 (velx*2, vely*2, 0); // takes 2 seconds to activate each unit
        
        
    }
}
