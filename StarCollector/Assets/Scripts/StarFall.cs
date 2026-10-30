using UnityEngine;

public class StarFall : MonoBehaviour
{
    public float fallSpeed = 1f;

    void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);

        if (transform.position.y < -3.5f)
        {
            LivesManager.Instance.LoseLife();

            Destroy(gameObject);
        }
    }
}