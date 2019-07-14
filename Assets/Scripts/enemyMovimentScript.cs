using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovimentScript : MonoBehaviour
{

    Vector3 position, localScale;
    public float freq;
    public float mag;


    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
        localScale = transform.localScale;    
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position = position + transform.up* Mathf.Sin(Time.time* freq)*mag;
    }
}
