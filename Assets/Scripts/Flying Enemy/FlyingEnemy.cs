using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FlyingEnemy : MonoBehaviour
{
    [SerializeField] float range;
    [SerializeField] float Speed;
    [SerializeField] GameObject player;
    [SerializeField] AudioClip sfx_Death;

    public AIPath myAiPathReference;
    Animator myAnimator;
    HealthComponent myHealthComponent;
    bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myHealthComponent = GetComponent<HealthComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckIfDead();
    }

    void CheckIfDead()
    {
        if(myHealthComponent.GetHealth() <= 0)
        {
            myAiPathReference.maxSpeed = 0.0f;
            if (!isDead)
                AudioSource.PlayClipAtPoint(sfx_Death, Camera.main.transform.position);
            isDead = true;
            
        }
    }

    void Move()
    {
        if (Physics2D.OverlapCircle(transform.position, range, LayerMask.GetMask("Player")) != null)
        {
            //myAnimator.SetBool("isMoving", true);
            myAiPathReference.maxSpeed = Speed;

            if(myAiPathReference.desiredVelocity.x >= 0.01f)
            {
                transform.localScale = new Vector3(-4f, 4f, 4f);
            }
            else if(myAiPathReference.desiredVelocity.x <= -0.01f)
            {
                transform.localScale = new Vector3(4f, 4f, 4f);
            }
        }
        else
        {
            myAiPathReference.maxSpeed = 0.0f;
        }

      
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 1f, 1f, 0.5f);
        Gizmos.DrawSphere(transform.position, range);
        Gizmos.DrawLine(transform.position, player.transform.position);
    }

    public bool GetIsDead()
    {
        return isDead;
    }
}

