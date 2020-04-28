using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    public string Name;
    protected GameSceneController gameSceneController;
    protected float halfWidth;
    protected float halfHeight;

    private SpriteRenderer spriteRenderer;

    protected virtual void Start()
    {
        gameSceneController = FindObjectOfType<GameSceneController>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        halfWidth = spriteRenderer.bounds.extents.x;
        halfHeight = spriteRenderer.bounds.extents.y;
    }

    public void SetColor(Color c)
    {
        spriteRenderer.color = c;
    }
}
