using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bird : MonoBehaviour
{
    private PlayManager playManager;
    private Rigidbody2D myRigidbody;

    [SerializeField] private float flapVelocity;
    private float flapDelay;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = gameObject.GetComponent<Rigidbody2D>();
        flapDelay = 2f * flapVelocity / (9.81f * myRigidbody.gravityScale);
        timer = flapDelay * 0.5f;
        playManager = GameObject.Find("PlayManager").GetComponent<PlayManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playManager.menu())
        {
            updateRot();
            //Autoflap
            if (Time.time >= timer)
            {
                flap();
                timer = Time.time + flapDelay;
            }
            
        } else if (playManager.playing())
        {
            updateRot();

            if (Input.GetKeyDown("space"))
            {
                flap();
            }

        } else
        {
            //ragdoll
        }
    }

    public void flap()
    {
        myRigidbody.velocity = Vector2.up * flapVelocity;
    }

    public void reset()
    {
        //Reset bird
        myRigidbody.velocity = Vector2.zero;
        myRigidbody.position = Vector2.zero;
        myRigidbody.angularVelocity = 0;
        myRigidbody.rotation = 0;

    }

    private void updateRot()
    {
        myRigidbody.rotation = myRigidbody.velocity.y;
    }
}
