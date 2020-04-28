using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : Shape
{
    public Vector2 moveDirection;
    public float moveSpeed;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        movePowerUp();
    }

      private void movePowerUp()
    {
        transform.Translate(moveDirection * Time.deltaTime * moveSpeed, Space.World);

        float top = transform.position.y + halfHeight;
        float bottom = transform.position.y - halfHeight;

        if (top >= gameSceneController.screenBounds.y || bottom <= -gameSceneController.screenBounds.y)
        {
            Destroy(gameObject);
        }
    }
}
