using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GliderScript : MonoBehaviour
{
    [SerializeField] private int speed = 10;
    [SerializeField] private bool randomSprite = false;
    [SerializeField] private Sprite[] sprites;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.left * speed;

        if (randomSprite)
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer != null && sprites.Length > 0)
            {
                int randomIndex = Random.Range(0, sprites.Length);
                spriteRenderer.sprite = sprites[randomIndex];
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < -75)
        {
            Destroy(gameObject);
        }
        
    }
}
