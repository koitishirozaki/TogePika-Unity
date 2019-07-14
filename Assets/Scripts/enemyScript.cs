using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Collider2D EnemyCollider;
    private SpriteRenderer EnemyRenderer;
    void Start()
    {
        EnemyCollider = gameObject.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(){
        Debug.Log("DANO");
        Destroy(gameObject);

    }
}
