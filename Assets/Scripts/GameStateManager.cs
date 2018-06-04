using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    private UI ui;
    private Generation generation;

    private MyTimer timer = new MyTimer();
    [SerializeField]
    private float counterGames = 30f;

    private bool isGame;
    public bool IsGame
    {
        get { return isGame; }
    }

    private void Awake()
    {
        ui = FindObjectOfType<UI>();
        generation = FindObjectOfType<Generation>();
    }

    private void Start()
    {
        ui.sizeW = generation.Width;
        ui.sizeH = generation.Height;
        ui.frequency = generation.Frequency;
        ui.currentEnemy = generation.AmountEnemy;

        Menu();
    }

    private void Update()
    {
        if (timer.Seconds < 10f) ui.timer.text = "0" + (int)timer.Seconds;
        else ui.timer.text = " " + (int)timer.Seconds;

        if (isGame && timer.Stop) GameOver();
        else if (isGame) timer.Update();
    }

    private void InitSettings()
    {
        generation.Width = (int)ui.sizeW;
        generation.Height = (int)ui.sizeH;
        generation.Frequency = ui.frequency;
        generation.AmountEnemy = (int)ui.currentEnemy;
    }

    private void Menu()
    {
        ui.panelM.SetActive(true);
        ui.panelG.SetActive(false);
        ui.timer.gameObject.SetActive(false);
    }

    public void GameOver()
    {
        ui.panelM.SetActive(false);
        ui.panelG.SetActive(true);
        ui.timer.gameObject.SetActive(false);
        isGame = false;
    }

    public void NextLevel()
    {
        ui.timer.gameObject.SetActive(true);
        generation.DestroyLevel();
        generation.CreateLevel();
        timer.Starting(counterGames);
        isGame = true;
    }

    public void Play()
    {
        ui.panelM.SetActive(false);
        ui.panelG.SetActive(false);
        ui.timer.gameObject.SetActive(true);
        InitSettings();
        generation.CreateLevel();
        timer.Starting(counterGames);
        isGame = true;
    }

    public void RestartLevel()
    {
        ui.panelG.SetActive(false);
        NextLevel();
    }

    public void ReturnMenu()
    {
        generation.DestroyLevel();
        Menu();
    }

    public void Exit()
    {
        Application.Quit();
    }
}
