using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vent : MonoBehaviour
{
    [SerializeField]
    private float health = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void beenHit(float dmgAmnt)
    {
        health -= dmgAmnt;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
