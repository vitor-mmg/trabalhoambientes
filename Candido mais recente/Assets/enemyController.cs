using UnityEngine;
using System.Collections;
public class enemyController : MonoBehaviour
{
    Animator anim;
    public GameObject player;
    public Transform plc;
    public float speed;
    public float damage;
    public float health;
    bool isAttacking;
    bool isDead;
    bool isKicking;


    public float timebetwenatacks;
    float timer;


    void Start()
    {
        anim = GetComponent<Animator>();
        isAttacking = false;
        isDead = false;
    }
    //move
    //the zombu will follow the player around
    void Update()
    {
        if (!isDead)
        {
            Move();
            if (isAttacking)
            {
                if (timebetwenatacks >= timer)

                    Attack();
            }
        }
    }
    void Move()
    {
        Vector3 move = plc.position - transform.position;
        move.Normalize();
        transform.position += move * speed * Time.deltaTime;
    }

    void onTriggerEnter(Collider pl)
    {
        if (pl.CompareTag("Player"))
        {
            isAttacking = true;
        }
    }
    void onTriggerExit(Collider pl)
    {
        if (pl.CompareTag("Player"))
        {
            isAttacking = false;
        }
    }
    //cause damage to the player
    void Attack()
    {
        CharacterController plc = player.GetComponent<CharacterController>();
        //plc.TakeDamage(damage);
        timer = 0.0f;
        Debug.Log("ahahahahahha gg");
    }
    //die
    void DieZombuDie()
    {
        isDead = true;
        isAttacking = false;
        isDead = false;
        //Destroy(TakeDamage, 1f);

    }
    //take damage
    public void TakeDamage(float d)
    {
        health -= d;
        if (health <= 0.0f)
            DieZombuDie();
    }
}
