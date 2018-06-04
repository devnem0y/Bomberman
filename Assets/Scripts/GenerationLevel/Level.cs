using UnityEngine;

public class Level : MonoBehaviour
{
    private bool playerSpawned;
    private bool portalCreating;

    private GameObject[,] grid;
    public GameObject[,] GetCell
    {
        get { return grid; }
    }

    public void Initialization(int width, int height, float frequency, int amountEnemy)
    {
        CreateGrid(width, height);
        GenerationLevel(width, height, frequency, amountEnemy);
    }

    private void GenerationLevel(int width, int height, float frequency, int amountEnemy)
    {
        GameObject platform = Instantiate(Resources.Load("Platform"), new Vector3(0f, 0.03f, 0f), Quaternion.identity, transform) as GameObject;
        platform.transform.localScale = new Vector3(width + 1.15f, platform.transform.localScale.y, height + 1.15f);
        GameObject ground = Instantiate(Resources.Load("Ground"), new Vector3(0f, 0.19f, 0f), Quaternion.identity, transform) as GameObject;
        ground.transform.localScale = new Vector3(width + 1, platform.transform.localScale.y, height + 1);

        CreateWall(width, height);
        CreateBox(width, height, frequency);
        CreatePortal(width, height);
        SpawnEnemy(width, height, amountEnemy);
        SpawnPlayer(width, height);
    }

    private void CreateGrid(int sizeW, int sizeH)
    {
        GameObject field = transform.GetChild(0).gameObject;
        grid = new GameObject[sizeW + 1, sizeH + 1];
        int minW = (sizeW / 2) * -1;
        int minH = (sizeH / 2) * -1;

        for (int x = 0; x <= sizeW; x++)
        {
            for (int y = 0; y <= sizeH; y++)
            {
                grid[x, y] = Instantiate(Resources.Load("Cell"), new Vector3(minW + x, 0f, minH + y), Quaternion.identity, field.transform) as GameObject;
            }
        }
    }

    private void CreateBox(int width, int height, float frequency)
    {
        for (int x = 1; x <= width; x++)
        {
            for (int y = 0; y <= height; y++)
            {
                if (grid[x, y].GetComponent<Cell>().Type == CellType.NONE && Random.value < frequency)
                {
                    Vector3 pos = grid[x, y].transform.position;
                    Instantiate(Resources.Load("Box"), new Vector3(pos.x, 0.5f, pos.z), Quaternion.identity, transform);
                    grid[x, y].GetComponent<Cell>().Type = CellType.BOX;
                }
            }
        }
    }

    private void CreatePortal(int width, int height)
    {
        while (!portalCreating)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (grid[x, y].GetComponent<Cell>().Type == CellType.BOX && Random.value > 0.75f)
                    {
                        Vector3 pos = grid[x, y].transform.position;
                        if (!portalCreating)
                        {
                            Instantiate(Resources.Load("Portal"), new Vector3(pos.x, 0.7f, pos.z), Quaternion.identity, transform);
                            portalCreating = true;
                        }
                    }
                }
            }
        }
    }

    private void CreateWall(int width, int height)
    {
        for (int x = 1; x < width; x += 2)
        {
            for (int y = 1; y < height; y += 2)
            {
                Vector3 pos = grid[x, y].transform.position;
                Instantiate(Resources.Load("Wall"), new Vector3(pos.x, 0.5f, pos.z), Quaternion.identity, transform);
                grid[x, y].GetComponent<Cell>().Type = CellType.WALL;
            }
        }
    }

    private void SpawnEnemy(int width, int height, int amountEnemy)
    {
        int currEnemy = 0;

        while (currEnemy < amountEnemy)
        {
            for (int x = 1; x < width - 1; x++)
            {
                for (int y = 1; y < height - 1; y++)
                {
                    if ((grid[x, y].GetComponent<Cell>().Type == CellType.NONE || grid[x, y].GetComponent<Cell>().Type == CellType.ENEMY_LOW || 
                        grid[x, y].GetComponent<Cell>().Type == CellType.ENEMY_NORMAL) && Random.value > 0.7f)
                    {
                        if (currEnemy < amountEnemy)
                        {
                            Vector3 pos = grid[x, y].transform.position;
                            Instantiate(Resources.Load("EnemyLow"), new Vector3(pos.x, 0.64f, pos.z), Quaternion.identity, transform);
                            currEnemy++;
                            grid[x, y].GetComponent<Cell>().Type = CellType.ENEMY_LOW;
                        }
                    }
                    else if ((grid[x, y].GetComponent<Cell>().Type == CellType.NONE || grid[x, y].GetComponent<Cell>().Type == CellType.ENEMY_LOW || 
                        grid[x, y].GetComponent<Cell>().Type == CellType.ENEMY_NORMAL || grid[x, y].GetComponent<Cell>().Type == CellType.BOX) && Random.value > 0.85f)
                    {
                        if (currEnemy < amountEnemy)
                        {
                            Vector3 pos = grid[x, y].transform.position;
                            Instantiate(Resources.Load("EnemyNormal"), new Vector3(pos.x, 0.64f, pos.z), Quaternion.identity, transform);
                            currEnemy++;
                            grid[x, y].GetComponent<Cell>().Type = CellType.ENEMY_NORMAL;
                        }
                    }
                }
            }
        }
    }

    private void SpawnPlayer(int width, int height)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (grid[x, y].GetComponent<Cell>().Type == CellType.NONE && Random.value > 0.3f)
                {
                    if (!playerSpawned)
                    {
                        Vector3 pos = grid[x, y].transform.position;
                        Instantiate(Resources.Load("Character"), new Vector3(pos.x, 0.64f, pos.z), Quaternion.identity, transform);
                        grid[x, y].GetComponent<Cell>().Type = CellType.PLAYER;
                        playerSpawned = true;
                    }
                }
            }
        }
    }
}
