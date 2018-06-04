using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private BoxCollider myCollider;

    [SerializeField]
    private int blastingForce = 1;
    [SerializeField]
    private float countdown = 2f;
    [SerializeField]
    private GameObject explosionEl;

    private Generation generation;

    private void Awake()
    {
        generation = FindObjectOfType<Generation>();
    }

    private void Start()
    {
        myCollider = GetComponent<BoxCollider>();
        StartCoroutine(Timer(countdown));
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) myCollider.isTrigger = false;
    }

    private void Explosion()
    {
        Instantiate(explosionEl, transform.position, Quaternion.identity, GameObject.Find("Level").transform);

        for (int i = 1; i <= blastingForce; i++)
        {
            Instantiate(explosionEl, new Vector3(transform.position.x + i, 0.75f, transform.position.z), Quaternion.identity, GameObject.Find("Level").transform); // right
            Instantiate(explosionEl, new Vector3(transform.position.x - i, 0.75f, transform.position.z), Quaternion.identity, GameObject.Find("Level").transform); // left
            Instantiate(explosionEl, new Vector3(transform.position.x, 0.75f, transform.position.z + i), Quaternion.identity, GameObject.Find("Level").transform); // up
            Instantiate(explosionEl, new Vector3(transform.position.x, 0.75f, transform.position.z - i), Quaternion.identity, GameObject.Find("Level").transform); // down
        }
    }

    private IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);
        Explosion();
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Level level = generation.Level.GetComponent<Level>();

        for (int x = 0; x <= generation.Width; x++)
        {
            for (int y = 0; y <= generation.Height; y++)
            {
                if (level.GetCell[x, y].transform.position.x == transform.position.x && level.GetCell[x, y].transform.position.z == transform.position.z)
                {
                    level.GetCell[x, y].GetComponent<Cell>().Type = CellType.NONE;
                }
            }
        }
    }
}
