using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerScript : MonoBehaviour
{
    private PlayManager playManager;

    // Start is called before the first frame update
    void Start()
    {
        playManager = GameObject.Find("PlayManager").GetComponent<PlayManager>();

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        playManager.endGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
