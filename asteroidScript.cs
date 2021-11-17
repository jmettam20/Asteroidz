using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroidScript : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidbody;
    public float size = 1.0f; 
    public float minSize = 5f; 
    public float maxSize = 15f; 

    public float asteroidSpeed = 10f; 

        public float maxLifetime = 30.0f;


    

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
    }
    
    private void Start()
    {
        this.transform.eulerAngles = new Vector3(0.0f,0.0f,Random.value * 360.0f); 
        this.transform.localScale = Vector3.one * this.size;

        rigidbody.mass = this.size; 
    }

    public void SetTrajectory(Vector2 direction) {

        rigidbody.AddForce(direction * this.asteroidSpeed);

       
        Destroy(this.gameObject, this.maxLifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Laser") {
            if (this.size * 0.5f >= this.minSize) {
                CreateSplit(); 
                CreateSplit(); 

            }
            FindObjectOfType<gameManager>().asteroidDestroyed(this);
            Destroy(this.gameObject); 
        }
    
    }

   

    private void CreateSplit() {
        Vector2 position = this.transform.position;
        position += Random.insideUnitCircle * 0.5f;
        asteroidScript half = Instantiate(this, position, this.transform.rotation);
        half.size = this.size * 0.5f;

        half.SetTrajectory(Random.insideUnitCircle.normalized * 20);

    }
}
  