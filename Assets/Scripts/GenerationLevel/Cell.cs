using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField]
    private CellType type;
    public CellType Type
    {
        get { return type; }
        set { type = value; }
    }

    private void Start()
    {
        type = CellType.NONE;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Wall")) Type = CellType.WALL;
        else if (other.CompareTag("Box")) Type = CellType.BOX;
        else if (other.CompareTag("Bomb")) Type = CellType.BOMB;
        else if (other.CompareTag("Player")) Type = CellType.PLAYER;
        else if (other.CompareTag("EnemyLow")) Type = CellType.ENEMY_LOW;
        else if (other.CompareTag("EnemyNormal")) Type = CellType.ENEMY_NORMAL;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Wall") || other.CompareTag("Box") ||
            other.CompareTag("Bomb") || other.CompareTag("Player") ||
            other.CompareTag("EnemyLow") || other.CompareTag("EnemyNormal")) Type = CellType.NONE;
    }
}
