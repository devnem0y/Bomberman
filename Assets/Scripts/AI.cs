using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    private List<Vector3> waypoints = new List<Vector3>();
    private Vector3[] points = new Vector3[4];

    private bool isMove;
    private float count = 0.3f;
    private Vector3 point = Vector3.zero;

    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private bool isStepWallked;

    private Generation generation;
    private GameStateManager gsm;

    private void Awake()
    {
        generation = FindObjectOfType<Generation>();
        gsm = FindObjectOfType<GameStateManager>();
    }

    private void Start()
    {
        float count = 0.3f;

        count -= Time.deltaTime;

        if (count <= 0f) NextPoint();
    }

    private void Update()
    {
        count -= Time.deltaTime;

        if (isMove) Move(point);
        else
        {
            if (count <= 0f) NextPoint();
        }
    }

    private void Move(Vector3 waypoint)
    {
        transform.position = Vector3.MoveTowards(transform.position, waypoint, speed * Time.deltaTime);
        if (transform.position == waypoint) isMove = false;
    }

    private void NextPoint()
    {
        Level level = generation.Level.GetComponent<Level>();

        points[0] = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
        points[1] = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
        points[2] = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
        points[3] = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);

        
        for (int i = 0; i < points.Length; i++)
        {
            for (int x = 0; x <= generation.Width; x++)
            {
                for (int y = 0; y <= generation.Height; y++)
                {
                    if (points[i].x >= (generation.Width / 2) * -1 && points[i].x <= generation.Width / 2 && points[i].z >= (generation.Height / 2) * -1 && points[i].z <= generation.Height / 2)
                    {
                        if (isStepWallked)
                        {
                            if (level.GetCell[x, y].transform.position.x == points[i].x && level.GetCell[x, y].transform.position.z == points[i].z &&
                            ((level.GetCell[x, y].GetComponent<Cell>().Type == CellType.NONE ||
                                level.GetCell[x, y].GetComponent<Cell>().Type == CellType.PLAYER ||
                                level.GetCell[x, y].GetComponent<Cell>().Type == CellType.ENEMY_LOW ||
                                level.GetCell[x, y].GetComponent<Cell>().Type == CellType.ENEMY_NORMAL ||
                                level.GetCell[x, y].GetComponent<Cell>().Type == CellType.BOX)))
                            {
                                waypoints.Add(points[i]);
                            }
                        }
                        else
                        {
                            if (level.GetCell[x, y].transform.position.x == points[i].x && level.GetCell[x, y].transform.position.z == points[i].z &&
                                ((level.GetCell[x, y].GetComponent<Cell>().Type == CellType.NONE ||
                                level.GetCell[x, y].GetComponent<Cell>().Type == CellType.PLAYER ||
                                level.GetCell[x, y].GetComponent<Cell>().Type == CellType.ENEMY_LOW)))
                            {
                                waypoints.Add(points[i]);
                            }
                        }
                    }
                }
            }
        }
            
            if (waypoints.Count != 0)
            {
                point = waypoints[Random.Range(0, waypoints.Count)];
                waypoints.Clear();
                count = 0.3f;
                isMove = true;
            } else
            {
                waypoints.Clear();
                isMove = false;
            }
        }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Explosion")) Destroy(gameObject);

        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
            gsm.GameOver();
        }
    }
}
