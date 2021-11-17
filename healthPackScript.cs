using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthPackScript : MonoBehaviour
{
    public GameObject player;

    public GameObject gameMnager;

    int maxLifetime = 3;

    void Awake()
    {
        //get player + game manager
        player = GameObject.Find("Player");

        gameMnager = GameObject.Find("GameManager");


        Destroy(this.gameObject, this.maxLifetime);//destroy after max lifetime
    }

    
   


    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")//if collides w player
        {

            Destroy(this.gameObject);//destroy self 

            FindObjectOfType<audioManager>().Play("PickupSound");//play sound 

            gameMnager.GetComponent<gameManager>().lives++;//add 1 life

            
        }

       
    }
}