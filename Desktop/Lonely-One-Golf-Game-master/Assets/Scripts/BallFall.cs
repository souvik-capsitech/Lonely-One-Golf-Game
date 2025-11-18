using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallFallDetector : MonoBehaviour
{
    public float fallLimit = -15f;
    public GameObject restartPanel;

    void Update()
    {
        if (transform.position.y < fallLimit)
        {
            StartCoroutine(GameOverAfterDelay());
        }
    }

    IEnumerator GameOverAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        restartPanel.SetActive(true);
        Time.timeScale = 0f;
    }
}
