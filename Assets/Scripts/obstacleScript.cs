using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacleScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Collider2D ObstacleCollider;
    private SpriteRenderer ObstacleRenderer;
    void Start()
    {
        ObstacleCollider = gameObject.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
