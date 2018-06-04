using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public GameObject panelG, panelM;

    private float step = 2f;
    public float sizeW = 6f;
    public float sizeH = 4f;
    public float frequency = 0.4f;
    public float currentEnemy = 1f;

    public Text timer, width, height, frequ, enemy;

    private void Start()
    {
        width.text = (sizeW + 1).ToString();
        height.text = (sizeH + 1).ToString();
        frequ.text = frequency.ToString();
        enemy.text = currentEnemy.ToString();
    }

    public void SliderWidth(float value)
    {
        sizeW = (int)(value * step);
        width.text = (sizeW + 1).ToString();
    }

    public void SliderHeight(float value)
    {
        sizeH = (int)(value * step);
        height.text = (sizeH + 1).ToString();
    }

    public void SliderFrequency(float value)
    {
        frequency = value;
        frequ.text = frequency.ToString();
    }

    public void SliderEnemy(float value)
    {
        currentEnemy = (int)value;
        enemy.text = currentEnemy.ToString();
    }
}
