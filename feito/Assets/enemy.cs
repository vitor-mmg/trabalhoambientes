using UnityEngine;
using System.Collections;

public class enemy : MonoBehaviour
{
    Animator anim;
    GameObject player;
    Transform ppp;
    public float speed = 2.0f;
    public float damage = 10.0f;
    public float hp = 40.0f;

    //atatck stuff
    float timer;
    public float timeBetweenAttacks;

    //state machine
    bool isAttacking;
    bool isDead;

    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        ppp = player.transform;
        isAttacking = false;
        isDead = false;
        timer = 0.0f;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (!isDead)
        {
            //move zombu
            Move();
            if (isAttacking)
            {
                if(timeBetweenAttacks <= timer)
                    Attack();
            }
        }
    }

    //move
    //the zombu will follow the player around
    void Move()
    {
        Vector3 move = ppp.position - transform.position;
        move.Normalize();
        transform.position += move * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider pl)
    {
        if (pl.CompareTag("Player"))
        {
            isAttacking = true;
            anim.SetBool("attack_zombu", isAttacking);
            anim.SetBool("move_zombu", false);
            Debug.Log("asdasd");
        }
    }

    void OnTriggerExit(Collider pl)
    {
        if (pl.CompareTag("Player"))
        {
            isAttacking = false;
            anim.SetBool("attack_zombu", isAttacking);
            anim.SetBool("move_zombu", true);
        }
    }

    //cause damage to the player
    void Attack()
    {
        timer = 0.0f;
        UnityStandardAssets.Characters.FirstPerson.FirstPersonController plc = player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
        plc.TakeDamage(damage);
        Debug.Log("AhAH I attakked u prayer");
    }

    //die
    void Die()
    {
        isDead = true;
        isAttacking = false;
        anim.SetBool("Die_Zombie_die", isDead);
        Destroy(gameObject, 0.1f);
        UnityStandardAssets.Characters.FirstPerson.FirstPersonController plc = player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
        plc.ScoringLikeAMan(1);
        

    }

    //take damage
    public void TakeDamage(float d)
    {
        hp -= d;
        if (hp <= 0.0f)
        {
            Die();
        }
    }
	
}
