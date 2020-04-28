using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : Shape
{
    public ProjectileController projectilePrefab;

    public Vector2 projectileDirection;
    public float projectileSpeed;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        moveProjectile();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Projectile"))
        {
            //more like continue and do nothing
            return;
        }
        else if (collision.gameObject.name.Contains("powerup"))
        {
            return;
        }
        else
        {
            Debug.Log("I hit " + collision.gameObject.name);
            Destroy(this.gameObject);
        }
    }

    private void moveProjectile()
    {
        transform.Translate(projectileDirection*Time.deltaTime*projectileSpeed, Space.World);

        float top = transform.position.y + halfHeight;
        float bottom = transform.position.y - halfHeight;

        if(top >= gameSceneController.screenBounds.y || bottom <= -gameSceneController.screenBounds.y)
        {
            Destroy(gameObject);
        }
    }
}
