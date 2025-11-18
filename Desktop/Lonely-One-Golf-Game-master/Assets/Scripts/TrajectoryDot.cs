using UnityEngine;

public class TrajectoryDot : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Transform ball;
    public float hideDist = 0.2f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (ball == null) return;

        if (ball.position.x > transform.position.x)
        {
            gameObject.SetActive(false);
        }


    }
}
