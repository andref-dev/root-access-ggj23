using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{

    private BoxCollider2D _collider;
    public BoxCollider2D Collider
    {
        get
        {
            if (_collider == null)
                _collider = GetComponent<BoxCollider2D>();
            return _collider;
        }
    }

    private SpriteRenderer _renderer;
    public SpriteRenderer Renderer
    {
        get
        {
            if (_renderer == null)
                _renderer = GetComponent<SpriteRenderer>();
            return _renderer;
        }
    }

    public bool IsWall;
    public void Setup(bool wall, Color color)
    {
        IsWall = wall;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().color = color;
    }
}
