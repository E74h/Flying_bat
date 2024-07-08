using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatMovement1 : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public float flapStrength;
    public LogicScript logic;
    public bool batIsAlive = true;
    public float falling;
    public float overFly = 17.6f;
    public AudioClip wind;
    public Animator anim;
    public float speed;

    // Start is called before the first frame update

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        AudioSource.PlayClipAtPoint(wind, transform.position);
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame

    void Update()
    {
        //AudioSource.PlayClipAtPoint(wind, transform.position);
        if (Input.GetButtonDown("Jump") && batIsAlive)
        {
            myRigidbody.velocity = Vector2.up * flapStrength;
            anim.SetBool("Flop", true);



        }
        fallandOverFlop();
        if (Input.GetButtonUp("Jump"))
        {
            new WaitForSeconds(0.3f);
            anim.SetBool("Flop", false);
        }

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                myRigidbody.velocity = Vector2.up * flapStrength;
                anim.SetBool("Flop", true);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                new WaitForSeconds(0.3f);
                anim.SetBool("Flop", false);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        logic.gameOver();
        batIsAlive = false;
    }
    private void fallandOverFlop()
    {
        float birdPosition = myRigidbody.position.y;
        if (birdPosition <= falling || birdPosition >= overFly)
        {
            logic.gameOver();
            batIsAlive = false;
        }
    }


}