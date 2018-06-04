using UnityEngine;

public class Generation : MonoBehaviour
{
    private GameObject level;
    public GameObject Level
    {
        get { return level; }
    }

    [Header("Size")]
    [SerializeField]
    [Range(6, 10)]
    private int width = 6;
    public int Width
    {
        get { return width; }
        set { width = value; }
    }
    [SerializeField]
    [Range(4, 6)]
    private int height = 6;
    public int Height
    {
        get { return height; }
        set { height = value; }
    }

    [SerializeField]
    [Range(0f, 1f)]
    private float frequency = 0.4f;
    public float Frequency
    {
        get { return frequency; }
        set { frequency = value; }
    }

    [Space]
    [SerializeField]
    private int amountEnemy = 1;
    public int AmountEnemy
    {
        get { return amountEnemy; }
        set { amountEnemy = value; }
    }

    public void CreateLevel()
    {
        level = Instantiate(Resources.Load("Level"), transform.position, Quaternion.identity) as GameObject;
        level.name = "Level";
        level.GetComponent<Level>().Initialization(width, height, frequency, amountEnemy);
    }

    public void DestroyLevel()
    {
        Destroy(level);
    }
}
