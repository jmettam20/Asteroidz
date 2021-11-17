using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserScript : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    public float laserSpeed = 100f;
    public float maxLifetime = 10.00f; 

    private void Awake() {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Project(Vector2 direction){
        rigidbody.AddForce(direction * laserSpeed);//direction of laser
        Destroy(this.gameObject, this.maxLifetime); //despawn after max lifetime expires
}
    //destroy if collides w anything 
    private void OnCollisionEnter2D(Collision2D collision) {
        Destroy(this.gameObject); 
    }

}
