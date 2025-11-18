using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject[] levels;
    public GameObject ball;

    private GameObject currLevel;
    private int currIdx = 0;

    void Start()
    {
        LoadLevel(currIdx);
    }


    public void LoadLevel(int idx)
    {
        if (currLevel != null)
        {
            Destroy(currLevel);
        }

        currLevel = Instantiate(levels[idx]);

        Transform spawn = currLevel.transform.Find("SpawnPoint");

        if(spawn != null) 
            ball.transform.position= spawn.position;

        if (spawn == null)
        {
            Debug.LogError("SpawnPoint not found in level " + idx);
            return;
        }
        ball.transform.position = spawn.position;
     
        ball.SetActive(true);

        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.bodyType = RigidbodyType2D.Dynamic;
        ball.GetComponent<Collider2D>().enabled = true;
    }

    public void LoadNextLevel()
    {
        currIdx++;
        if(currIdx >= levels.Length)
            currIdx = 0;

        LoadLevel(currIdx);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
