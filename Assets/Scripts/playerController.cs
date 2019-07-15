using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{

    public Rigidbody2D rigidB;
    private Collision coll;
    public bool itsAlive = true;
    public int combo = 0;
    public int pontos = 0;
    //public Text pointsLabel;
    // public Text comboLabel;

    public Transform groundCheckTransform;
    private bool isGrounded;
    public LayerMask groundCheckLayerMask;

    public bool grounded;
    public bool toJump;
    public bool toDash;
    public bool toAttack;
    public bool isDashing;
    public bool canAttack = false;
    public bool gotKill = false;
    public bool isJump;
    public bool doubleJump;
    public bool extAttack;

    public float dist;
    private float distTimerCounter;

    private float jumpTimeCounter;
    public float jumpTime = 0.5f;
    private float timeAttack;
    public float startTimeAttack = 3f;
    public float speed = 10;
    public float jumpForce = 2;
    public float dashSpeed = 20;
    public float attackRadius = 1;

    public RaycastHit2D ray;

    public Transform attackPos;
    public LayerMask whatEnemies;
    public LayerMask whatGround;




    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collision>();
        rigidB = GetComponent<Rigidbody2D>();
        

    }

    // Update is called once per frame
    void Update()
    {
        toAttack = Input.GetButtonDown("Fire1");
        toDash = Input.GetButton("Fire1");
        toJump = Input.GetKeyDown(KeyCode.Space);

        if (itsAlive == true)
        {
            Vector2 newVelocity = rigidB.velocity;
            newVelocity.x = speed;
            rigidB.velocity = newVelocity;
            


            //Jump
            if ((grounded || gotKill) && Input.GetKeyDown(KeyCode.Space))
            {
                canAttack = true;
                isJump = true;
                gotKill = false;
                jumpTimeCounter = jumpTime;
                Vector2 jumpVelocity = rigidB.velocity;
                jumpVelocity.y = 1 * jumpForce;
                rigidB.velocity = jumpVelocity;
            }

            if (Input.GetKey(KeyCode.Space) && isJump == true)
            {
                if (jumpTimeCounter > 0)
                {
                    Vector2 jumpVelocity = rigidB.velocity;
                    jumpVelocity.y = 1 * jumpForce;
                    rigidB.velocity = jumpVelocity;
                    jumpTimeCounter -= Time.deltaTime;

                }
                else
                {
                    isJump = false;
                }
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                isJump = false;
            }

            //Attack
            if (!grounded && canAttack == true && Input.GetKeyDown(KeyCode.Return))
            {

                int i = 0;
                gotKill = false;
                canAttack = false;
                timeAttack = startTimeAttack;


                Collider2D[] enemiesToKill = Physics2D.OverlapCircleAll(attackPos.position, attackRadius, whatEnemies);

                if (enemiesToKill.Length > 0)
                {
                    for (i = 0; i < enemiesToKill.Length; i++)
                    {
                        Debug.Log(enemiesToKill[i]);
                        if (enemiesToKill[i].name != "Obstacle1")
                        {
                            enemiesToKill[i].GetComponent<enemyScript>().takeDamage();
                        }
                        isJump = false;
                        jumpTimeCounter = jumpTime;
                        combo += 1;
                        //comboLabel.text= "Combo: " + combo.ToString();
                        pontos += combo;
                        //pointsLabel.text = "Points: " + pontos.ToString();
                    }
                    gotKill = true;
                    canAttack = true;
                    //Debug.Log("Attakc");
                }

                timeAttack -= Time.deltaTime;
                


            }
            
            pontos += Mathf.RoundToInt( 25*Time.deltaTime);
            //pointsLabel.text = "Points: " + pontos.ToString();

            //Debug.Log(pontos);


            UpdateGroundedStatus();

        }
        else
        {
            this.gameObject.SetActive(false);
        }




    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRadius);
    }

    void UpdateGroundedStatus()
    {
        grounded = Physics2D.OverlapCircle(groundCheckTransform.position, 0.1f, groundCheckLayerMask);
        if (grounded) {
            combo = 0;
            //comboLabel.text= "Combo: " + combo.ToString();
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 9f, whatGround);
            if (hit)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation, 10f * Time.deltaTime);

            }
        } else {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.FromToRotation(transform.up, Vector2.up) * transform.rotation, 10f * Time.deltaTime);
        }





    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        hitByEnemy(collider);
    }

    void hitByEnemy(Collider2D EnemyCollider)
    {
        itsAlive = false;
    }

}
