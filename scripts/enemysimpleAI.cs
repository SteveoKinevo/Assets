using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemysimpleAI : MonoBehaviour {
    private Rigidbody rb;
    [SerializeField]
    private Transform startpos = null, endpos = null;
    public Transform startcol, endcol, startcol2, endcol2;
    bool collision2, side_coll;
    public float speed = 1f;
    public LayerMask detectWhat;
    bool move_right = true;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Move();
        ChangeDirection();
	}


    void Move()
    {
        //rb.velocity = new Vector2(transform.localScale.x, 0) * speed;
        if (move_right)      
            transform.Translate(Vector2.right * speed * Time.deltaTime);        
        else
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        
    }

    void ChangeDirection()
    {
        RaycastHit hitme, hitme2;
        collision2 = Physics.Linecast(startpos.position,endpos.position,out hitme, detectWhat);
        if (!collision2)
        {
            Vector3 temp = transform.localScale;
            temp.x *= -1;
            transform.localScale = temp;
            move_right = !move_right; 
        }
        //detect side collision, eg: player
        if (Physics.Linecast(startcol.position, endcol.position, out hitme2, detectWhat))
        {
            Player play = hitme2.collider.gameObject.GetComponent<Player>();
            play.JumpBack();            
            
        }
        //detect side collision, eg: player
        if (Physics.Linecast(startcol2.position, endcol2.position, out hitme2, detectWhat))
        {
            Player play = hitme2.collider.gameObject.GetComponent<Player>();
            play.JumpBack();

        }




    }

    void OnCollisionEnter(Collision coll)
    {
        Debug.Log("collider");
    }

    void OnTriggerEnter(Collider coll)
    {
        Debug.Log("trigger");
    }

    void OnDrawGizmos()
    {
        Debug.DrawLine(startpos.position, endpos.position);
        Debug.DrawLine(startcol.position, endcol.position);
        Debug.DrawLine(startcol2.position, endcol2.position);
    }
}

