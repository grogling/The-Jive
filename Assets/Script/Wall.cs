using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public int hp = 1;

    void Awake()
    {
        
    }

    // Update is called once per frame
    public void DamageWall(int loss)
    {
        hp -= loss;
        if (hp <= 0)
            gameObject.SetActive(false);
    }
}
