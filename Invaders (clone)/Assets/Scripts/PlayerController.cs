using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : Shape
{
    public GameObject explosion;
    public GameObject shieldAnimation;
    public ProjectileController projectilePrefab;
    public GameObject projectileSpawnPoint;
    public AudioSource projectileShoot;
    public AudioSource shieldHit1Audio;
    public AudioSource powerUpAudio;
    public int health;
    public HealthDisplay healthDisplay;

    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            FireProjectile();
        }
    }

    public void MovePlayer()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");

        if (Mathf.Abs(horizontalMovement) > Mathf.Epsilon)
        {
            horizontalMovement = horizontalMovement * Time.deltaTime * gameSceneController.playerSpeed;
            horizontalMovement += transform.position.x;

            float right = gameSceneController.screenBounds.x - halfWidth;
            float left = -right;

            float limit = Mathf.Clamp(horizontalMovement, left, right);

            transform.position = new Vector2(limit, transform.position.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name.Contains("powerup"))
        {
            if(health < 4)
            {
                health++;
                healthDisplay.AddOneToHealth();
                powerUpAudio.Play();
                Destroy(collision.gameObject);
            }
            return;
        }
        health--;

        if (health <= 0)
        {
            GameObject.Find("GameController").GetComponent<GameSceneController>().StopAllCoroutines();
            Instantiate(explosion, transform.position, Quaternion.identity);
            GameObject.Find("GameController").GetComponent<GameSceneController>().StartBtn.SetActive(true);
            Destroy(this.gameObject);
        }
        else
        {
            //Play shield effect
            GameObject s = Instantiate(shieldAnimation, transform.position, Quaternion.identity);
            s.transform.SetParent(this.gameObject.transform);
            Destroy(s, .25f);

            //update display
            healthDisplay.removeOneFromHealth();

            //play random sound
            shieldHit1Audio.Play();
        }
    }

    private void FireProjectile()
    {
        Vector2 spawnPosition = projectileSpawnPoint.transform.position;

        ProjectileController projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);

        projectile.projectileSpeed = 5;
        projectile.projectileDirection = Vector2.up;

        if(projectileShoot != null)
        {
            projectileShoot.Play();
        }
    }
}
