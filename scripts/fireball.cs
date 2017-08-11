using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireball : MonoBehaviour
{
	//this is a gihub test
    public float rocketSpeed = 8f;
    public bool kick_ball;
    Player play;
    Rigidbody rb;
    // Use this for initializatio	
    // Update is called once per frame

    void Awake()
    {
        //Debug.Log(kick_ball);
        rb = GetComponent<Rigidbody>();
        play = GameObject.Find("Player").GetComponent<Player>();
    }

    void Start()
    {
        if (!kick_ball)
            rb.AddForce(new Vector3((play.facingRight ? 1 : -1), 0) * 8f, ForceMode.Impulse);
    }

    
    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            Target target = coll.gameObject.GetComponent<Target>();
            target.TakeDamage(5);
            Destroy(gameObject);

        }
        

    }

}
