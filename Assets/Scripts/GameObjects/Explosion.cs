using UnityEngine;

public class Explosion : MonoBehaviour
{
    private Generation generation;

    private void Awake()
    {
        generation = FindObjectOfType<Generation>();
    }

    private void Start()
    {
        transform.GetChild(0).transform.position = transform.position;

        if (transform.position.x > generation.Width / 2 || transform.position.x < (generation.Width / 2) * -1||
            transform.position.z > generation.Height / 2|| transform.position.z < (generation.Height / 2) * -1)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject, 0.45f);
            if (other.CompareTag("Box")) Destroy(other.gameObject);
        }
    }
}
