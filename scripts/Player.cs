using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    CharacterController controller;
    Vector3 moveVector, lastMotion;
    float inpDirection, verticalVelocity;
    public float gravity = 25f;
    public float speed = 8f;
    public float jumpForce = 10f;
    public float fireRate = 0f;
    public float fireRange = 30f;
    public GameObject jumpEffect;
    Transform chld;
    bool doubleJump = false;
    public bool facingRight;
    public bool shootOption = false;
    float nextFire = 0.6f;
    UnityEngine.Object fireb;

    // Use this for initialization
    void Start () {
        controller = GetComponent<CharacterController>();
        chld = transform.GetChild(0);
        fireb = Resources.Load("fireball", typeof(GameObject));
        facingRight = true;
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 pos = transform.localScale;
        pos.x *= -1;
        transform.localScale = pos;
    }
	
	// Update is called once per frame
	void Update () {
        moveVector = Vector3.zero;
        inpDirection = Input.GetAxis("Horizontal") * speed;

        if (inpDirection > 0 && !facingRight)
            Flip();		
        else if (inpDirection < 0 && facingRight)
            Flip();

        if (isGrounded() == 2) //both raycasts on the ground
        {
            verticalVelocity = 0f;

            if (Input.GetKey(KeyCode.Space))
            {
                verticalVelocity = jumpForce;
                doubleJump = true;
            }
 
            //for firing
            if (Input.GetKeyDown(KeyCode.C) && Time.time >= nextFire)
            {
                Shoot(shootOption);
                //Debug.Log("shoot");
                nextFire = Time.time + fireRate;
            }

            moveVector.x = inpDirection;
        }else{
            if (Input.GetKeyDown(KeyCode.Space) && doubleJump)
            {
                verticalVelocity = jumpForce;
                doubleJump = false;
                Instantiate(jumpEffect, new Vector3(transform.position.x,
                    transform.position.y - 0.4f), transform.rotation);
            }

            verticalVelocity -= gravity * Time.deltaTime;
            moveVector.x = lastMotion.x;
        }

        moveVector.y = verticalVelocity;
        controller.Move(moveVector*Time.deltaTime);
        lastMotion = moveVector;
    }

    private void Shoot(bool kick_ball)
    {
       /* RaycastHit hit;
        Debug.Log(chld.transform.name);
        //Debug.Log(inpDirection.ToString());
        if (Physics.Raycast(chld.position,
            new Vector3((facingRight?1:-1),0,0), 
            out hit,fireRange))
        {
            //Debug.DrawLine(chld.position, hit.point,Color.red, 3f);
            if (hit.collider.tag == "Enemy")
            {
                Debug.Log("fsdgdgdf");
                
                /*Target target = hit.transform.GetComponent<Target>();
                if (target != null)
                {
                  target.TakeDamage(5);
                }*/

            //}


        //Quaternion rot = chld.localRotation;
        //rot.z = (facingRight ? 0f : 180f);
        GameObject eff2 = Instantiate(fireb, chld.position, chld.rotation) as GameObject;
        fireball ball = eff2.GetComponent<fireball>();
        ball.kick_ball = kick_ball;
        
    }

    public void JumpBack()
    {
        Debug.Log("attacked");
    }

    private int isGrounded()
    {
        int onedge = 0;
        Vector3 leftStart, rightStart;
        leftStart = controller.bounds.center;
        rightStart = controller.bounds.center;
        leftStart.x -= controller.bounds.extents.x;
        rightStart.x += controller.bounds.extents.x;

        if (Physics.Raycast(leftStart, Vector3.down, (controller.height / 2) + 0.1f))       
            onedge=2;        

        if (Physics.Raycast(rightStart, Vector3.down, (controller.height / 2) + 0.1f))        
            onedge=2;
                
        Debug.DrawRay(leftStart, Vector3.down, Color.red);
        Debug.DrawRay(rightStart, Vector3.down, Color.red);
        return onedge;
    }

    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (controller.collisionFlags == CollisionFlags.Sides)
        {
            if (hit.transform.tag == "Enemy")
            {
                Debug.Log("hitter");
                moveVector = hit.normal * speed;
                verticalVelocity = jumpForce * 1.0f;
            }
                //wall jump stuff
            if (Input.GetKey(KeyCode.Space)){
                moveVector = hit.normal * speed;
                verticalVelocity = jumpForce;
                doubleJump = false;
            }
        }

        if (controller.collisionFlags == CollisionFlags.Below)
        {
            if (hit.transform.tag == "spikes")
            {
                Target trg = hit.gameObject.GetComponent<Target>();
                PlayerHealth plyH = GetComponent<PlayerHealth>();
                plyH.TakeDamage(trg.damageToPlayer);
                verticalVelocity = jumpForce * 1.5f;
            }
            else if (hit.transform.tag == "Enemy")
            {
                verticalVelocity = jumpForce * 1.0f;
                //moveVector = hit.normal * -speed;
                Target trg = hit.gameObject.GetComponent<Target>();
                trg.TakeDamage(trg.playerDamage);
            }
        }


        switch (hit.gameObject.tag)
        {
            case "Coin":
                {
                    LevelManager.Instance.CoinUp();
                    Destroy(hit.gameObject);
                    break;
                }
            case "jumppad":
                {
                    verticalVelocity = jumpForce * 2.2f;
                    break;
                }
            case "teleport":
                {
                    transform.position = hit.transform.GetChild(0).position;
                    break;
                }
            case "WinCheckpoint":
                {
                    LevelManager.Instance.Win();
                    break;
                }
        }
    }
}
