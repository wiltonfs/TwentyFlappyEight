using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GliderScript : MonoBehaviour
{
    [SerializeField] private int speed = 10;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.left * speed;
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
