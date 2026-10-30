using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Star"))
        {
            audioSource.Play();

            ScoreManager.Instance.AddScore();

            Destroy(other.gameObject);
        }
    }
}