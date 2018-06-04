using UnityEngine;

public class Box : MonoBehaviour
{
    private Generation generation;

    private void Awake()
    {
        generation = FindObjectOfType<Generation>();
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