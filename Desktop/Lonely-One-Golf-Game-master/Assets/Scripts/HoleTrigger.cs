using System.Collections;
using UnityEngine;

public class Hole : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            StartCoroutine(SuckBall(other.gameObject));
        }

    }

    private IEnumerator SuckBall(GameObject ball)
    {

        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;

        ball.GetComponent<Collider2D>().enabled = false;

        for (float t = 1; t > 0; t -= Time.deltaTime * 3)
        {
            ball.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            ball.transform.position = Vector3.Lerp(ball.transform.position, transform.position, 0.1f);
            yield return null;
        }

       
        ball.SetActive(false);

      
        yield return new WaitForSeconds(0.2f);

        Debug.Log("Level Complete!");

        FindAnyObjectByType<LevelManager>().LoadNextLevel();
    }
}
