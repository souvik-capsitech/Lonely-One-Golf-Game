using System.Collections;
using UnityEngine;

public class Hole : MonoBehaviour
{
    public Transform coin;
    public bool zoomThisLevel = false;
    private CameraZoom camZoom;

     void Start()
    {
        camZoom=  Camera.main.GetComponent<CameraZoom>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            if (zoomThisLevel)
            {
                CameraZoom cz = Camera.main.GetComponent<CameraZoom>();

                cz.TriggerZoom(transform.position);
            }

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

        
        ScoreManager.instance.AddDirectShot();

        yield return new WaitForSeconds(0.1f);
        yield return StartCoroutine(CoinPop());

        if (zoomThisLevel && camZoom != null)
            camZoom.ResetZoom();
        FindAnyObjectByType<LevelManager>().LoadNextLevel();

    }


    private IEnumerator CoinPop()
    {
        if (coin == null)
            yield break;

      
        coin.gameObject.SetActive(true);
        coin.localScale = new Vector3(0.033f, 0.033f, 1f);

      
        Animator anim = coin.GetComponent<Animator>();
        if (anim != null)
            anim.Play("CoinFlip");

      
        Vector3 startPos = coin.localPosition;
        Vector3 endPos = startPos + new Vector3(0, 0.6f, 0);

        float duration = 0.5f;     
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            coin.localPosition = Vector3.Lerp(startPos, endPos, t / duration);
            yield return null;
        }

       
        yield return new WaitForSeconds(0.2f);
    }

}
