using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float baseSpawnInterval;
    [SerializeField] private float randomSpawnInterval;
    [SerializeField] private Vector2 positionRange;
    [SerializeField] private GameObject child;
    [SerializeField] private bool preGen;

    private PlayManager playManager;

    private float timer;


    // Start is called before the first frame update
    void Start()
    {
        playManager = GameObject.Find("PlayManager").GetComponent<PlayManager>();
 
        if (preGen)
        {
            GameObject newChild = Instantiate(child, new Vector3(Random.Range(-50, 0), Random.Range(positionRange.x, positionRange.y), 0), Quaternion.Euler(0, 0, 0));
            newChild.transform.parent = gameObject.transform;
        }
        timer = 0.0f;   
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= timer)
        {
            if(preGen)
            {
                //Just keep spawning
                GameObject newChild = Instantiate(child, new Vector3(75, Random.Range(positionRange.x, positionRange.y), 0), Quaternion.Euler(0, 0, 0));
                newChild.transform.parent = gameObject.transform;

            } else if (playManager.playing())
            {
                GameObject newChild = Instantiate(child, new Vector3(75, Random.Range(positionRange.x, positionRange.y), 0), Quaternion.Euler(0, 0, 0));
                newChild.transform.parent = gameObject.transform;

            }

            timer += baseSpawnInterval + Random.Range(0, randomSpawnInterval);
        }
    }

    public void reset()
    {
        foreach (Transform child in this.transform) {
            GameObject.Destroy(child.gameObject);
        }
    }
}
