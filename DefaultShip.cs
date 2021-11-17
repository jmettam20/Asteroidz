using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultShip : MonoBehaviour
{
    private bool thrust;//thrusting?
    private float trurnDirection;//stores turn direction

    private Rigidbody2D rigidbody;

    public float thrustSpeed = 1f; 
    public float turnSpeed = 1f;

    public laserScript laser;

    public  Animator anim; 

   

    void Start() {
        rigidbody = GetComponent<Rigidbody2D>(); //get rigidbody 
        anim = GetComponent<Animator>();//get animator 
    }

    void Update()
    {
        movementControl();
        Shoot();

        //quit game
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

   

    void movementControl() {
        thrust = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);//if w or up pressed 

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {//go left if A or left arrow 
            trurnDirection = 1.0f;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {//go right if d or right arrow
            trurnDirection = -1.0f;
        }
        else {
            trurnDirection = 0f;//not turning 
        }

        //thruster functionality 
        if (thrust)
        {
            //move
            rigidbody.AddForce(this.transform.up * this.thrustSpeed);
            //aidio
            FindObjectOfType<audioManager>().Play("ThrusterSound");
            //animation 
            anim.SetBool("thrusting",true);
        }
       
        //tuning handler
        if (trurnDirection != 0f) {
            rigidbody.AddTorque(trurnDirection * this.turnSpeed);
        
        }

        //no thrust = stop animation + sound 
        if (!thrust) {
            FindObjectOfType<audioManager>().Stop("ThrusterSound");
            anim.SetBool("thrusting",false);
        }

    }

    //laser handler
    private void Shoot() {

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {//shoot if space or Lmouse press
            FindObjectOfType<audioManager>().Play("LazerSound");//play audio
            laserScript laser = Instantiate(this.laser, this.transform.position, this.transform.rotation);//spawn prefab
            laser.Project(this.transform.up);//move prefab
        }
    }

    //collision 
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Asteroid") {//if asteroid collides 
            rigidbody.velocity = Vector3.zero;//stop movement 
            rigidbody.angularVelocity = 0.0f;//stop movement 

            this.gameObject.SetActive(false); //deactivate player object

            FindObjectOfType<gameManager>().PlayerDeath(); //run player deth function from gameManager script
        }
    
    }
}
