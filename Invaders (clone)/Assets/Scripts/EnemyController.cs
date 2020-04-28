using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Shape
{
    public event Action<int> EnemyKilled;
    public GameObject explosion;
    public AudioSource explosionSFX;
    public ProjectileController projectilePrefab;
    public Transform projectileSpawnPoint;
    public AudioSource projectileSoundFX;
    public Powerup powerupPrefab;
    public float oddsToGetPowerup;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        StartCoroutine(ShootBack());
    }

    // Update is called once per frame
    void Update()
    {
        MoveEnemy();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(EnemyKilled != null)
        {
            EnemyKilled(10);//Add points
            Instantiate(explosion, transform.position, Quaternion.identity);
            float getPower = UnityEngine.Random.Range(0f, 1f);
            Debug.Log(getPower + " " + (getPower <= oddsToGetPowerup));
            if(getPower <= oddsToGetPowerup)//random range max is exclusive
            {
                Powerup p = Instantiate(powerupPrefab, transform.position, Quaternion.identity);
                p.moveDirection = Vector2.down;
                p.moveSpeed = 3;
            }
            if(explosionSFX != null)
            {
                Debug.Log("PlaySFX");
                explosionSFX.Play();
            }
            Destroy(gameObject);//enemy
        }
    }

    private IEnumerator ShootBack()
    {

        while (true)
        {
            WaitForSeconds wait = new WaitForSeconds(UnityEngine.Random.Range(1f,2f));
            ProjectileController projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);

            projectile.projectileSpeed = 5;
            projectile.projectileDirection = Vector2.down;


            if (projectileSoundFX != null)
            {
                projectileSoundFX.Play();
            }

            yield return wait;
        }
    }
        private void MoveEnemy()
    {
        transform.Translate(Vector2.down * Time.deltaTime, Space.World);

        float bottom = transform.position.y - halfHeight;

        if(bottom <= -gameSceneController.screenBounds.y)
        {
            Destroy(gameObject);
        }
    }
}
