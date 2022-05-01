using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    //Speed of the fireball
    public float speed = 5f;
    //Rigid body of the fireball
    public Rigidbody2D fireballRigidBody;
    //The fireball hit effect prefab
    public GameObject fireballHitPrefab;
    //The amount of things the fireball has hit
    private int hitCount = 0;
    //Fireball damage
    private int fireballDamage = 100;

    // Start is called before the first frame update
    void Start()
    {
        //Get the fireball hit prefab
        fireballHitPrefab = (GameObject)Resources.Load("Prefabs/FireballHit");
        //Get the rigidbody of the fireball
        fireballRigidBody = GetComponent<Rigidbody2D>();
        //Set the velocity to transform.right (1, 0, 0) and multiply by the speed
        fireballRigidBody.velocity = transform.right * speed;

    }

    //Called when the fireball collides with something
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            //Kill the enemy
            collision.GetComponent<EnemyAI>().EnemyDeath();
        }
        else if (collision.CompareTag("Boss"))
        {
            //Damage the boss
            collision.GetComponent<BossAI>().BossTakeDamage(fireballDamage);
        }
        //Get the position of the enemy so the hit effect can be spawned there
        Transform collisionPos = collision.GetComponent<Transform>();
        //Incrememnt the hit count
        hitCount++;
        //If the fireball has hit 5 things 
        if (hitCount == 5)
        {
            //Destroy the fireball
            Destroy(gameObject);
            //Create a clone of the fireball hit prefab.
            Instantiate(fireballHitPrefab, collisionPos.position, collisionPos.rotation);
        }
    }
}
