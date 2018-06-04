using UnityEngine;

public class Portal : MonoBehaviour
{
    private GameStateManager gsm;

    private void Awake()
    {
        gsm = FindObjectOfType<GameStateManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Explosion"))
        {
            GetComponent<MeshRenderer>().enabled = true;
        }
        else if (other.CompareTag("Player"))
        {
            gsm.NextLevel();
        }
    }
}
