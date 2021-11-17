using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class gameManager : MonoBehaviour
{
    public DefaultShip player;

    public int lives = 3;

    public int maxLives = 3; 

    public float respawnTime = 3.0f;

    public float invulnrabilityTime = 3.0f;

    public ParticleSystem explosion;

    public ParticleSystem HealthPackPulse;

    public int score = 0;


    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;


    public Slider slider;

   

    public GameObject HealthPack;

    public bool gotHealthPack = false;


    void Start() {
        highScoreText.text = PlayerPrefs.GetInt("HighScore",0).ToString();
    }

    void Update() {
        slider.value = lives;
        keepScore();

        if (lives >= 3)
        {
            lives = 3;
        }
    }

    public void keepScore() {
        scoreText.SetText(score.ToString());
      
        if (score > PlayerPrefs.GetInt("HighScore", 0)) {
            PlayerPrefs.SetInt("HighScore", score);
            highScoreText.text = score.ToString(); 
        }
       
    }

    public void SetLives() {
        slider.maxValue = maxLives;
        slider.value = lives;

       

        if (lives == 1) {
            FindObjectOfType<audioManager>().Play("LowHealthSound");
        }
    }

    public void asteroidDestroyed(asteroidScript asteroid) {

        FindObjectOfType<audioManager>().Play("ExplosionSound");

        //HEALTH PACK
        int num = Random.Range(1, 10);

        if (num == 1)
        {
            this.HealthPackPulse.transform.position = asteroid.transform.position;
            this.HealthPackPulse.Play();


            Vector2 position = asteroid.transform.position;
            position += Random.insideUnitCircle * 0.5f;
            Instantiate(HealthPack, position, HealthPack.transform.rotation);
        }
        


       


        
        

        /////


        this.explosion.transform.position = asteroid.transform.position;
        this.explosion.Play();



       

        if (asteroid.size < 7.5f) {
            score += 100;
        } else if (asteroid.size < 10f) {
            score += 50;
        }else{
            score += 25;

        }
    }


    public void PlayerDeath() {
        this.explosion.transform.position = this.player.transform.position; 
        this.explosion.Play(); 
        this.lives--;

        FindObjectOfType<audioManager>().Play("ExplosionSound"); 
        

        if (this.lives <= 0)
        {
            FindObjectOfType<audioManager>().Play("ExplosionSound");
            gameOver();
        } else {

            Invoke(nameof(Respawn), this.respawnTime);
                 }
           
    }

    private void Respawn() {
        this.player.transform.position = Vector3.zero;
        this.player.gameObject.layer = LayerMask.NameToLayer("Invunrability");
        this.player.gameObject.SetActive(true);
       Invoke(nameof(TurnOnCollisions), invulnrabilityTime);
    }

    private void TurnOnCollisions() {
        this.player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    public void gameOver() {

        
        Application.LoadLevel("GameOver");
    }

    public void mainMenu() {

        Application.LoadLevel("Menu");
    }

    public void quit() {
        Application.Quit();
    }

    public void startGame() {
        Application.LoadLevel("Main");
    }
}
