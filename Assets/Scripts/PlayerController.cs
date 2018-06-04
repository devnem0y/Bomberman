using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    private Rigidbody body;

    [SerializeField]
    private GameObject bomb;
    [SerializeField]
    private float resBomb = 0f;
    private bool isBomber = true;

    private Generation generation;
    private GameStateManager gsm;

    private void Awake()
    {
        generation = FindObjectOfType<Generation>();
        gsm = FindObjectOfType<GameStateManager>();
    }

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        body.drag = 7.5f;
    }

    private void Update()
    {
        if (gsm.IsGame)
        {
            Move();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Level level = generation.Level.GetComponent<Level>();

                if (isBomber)
                {
                    Vector3 pos = Vector3.zero;
                    for (int x = 0; x <= generation.Width; x++)
                    {
                        for (int y = 0; y <= generation.Height; y++)
                        {
                            if (level.GetCell[x, y].GetComponent<Cell>().Type == CellType.PLAYER)
                            {
                                pos = level.GetCell[x, y].transform.position;
                            }
                        }
                    }
                    Instantiate(bomb, new Vector3(pos.x, transform.position.y, pos.z), Quaternion.identity, GameObject.Find("Level").transform);
                    StartCoroutine(TimerResBomb(resBomb));
                    isBomber = false;
                }
            }
        }
    }

    private IEnumerator TimerResBomb(float time)
    {
        yield return new WaitForSeconds(time);
        isBomber = true;
    }

    private void Move()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(hAxis, 0f, vAxis) * speed * Time.deltaTime;

        body.MovePosition(transform.position + movement);

        if (transform.position.x <= (generation.Width / 2) * -1) transform.position = new Vector3((generation.Width / 2) * -1, transform.position.y, transform.position.z);
        else if (transform.position.x >= generation.Width / 2) transform.position = new Vector3(generation.Width / 2, transform.position.y, transform.position.z);

        if (transform.position.z <= (generation.Height / 2) * -1) transform.position = new Vector3(transform.position.x, transform.position.y, (generation.Height / 2) * -1);
        else if (transform.position.z >= generation.Height / 2) transform.position = new Vector3(transform.position.x, transform.position.y, generation.Height / 2);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Explosion"))
        {
            Destroy(gameObject);
            gsm.GameOver();
        }
    }
}