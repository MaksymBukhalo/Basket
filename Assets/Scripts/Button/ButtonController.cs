using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private MainMenu mainMenu;
    [SerializeField] private GameObject gameMenu;
    [SerializeField] private GameObject pauseGameMenu;
    [SerializeField] private GameObject resultMenu;
    [SerializeField] private Transform canvas;
    [SerializeField] private GameObject game;
    [SerializeField] private Text scoreText;
    [SerializeField] private LifeAndScoreController scoreNumber;

    private int endScore;


    private bool isPauseGame = false;

	private void Start()
	{
        canvas = GameObject.Find("Canvas").GetComponent<Transform>();
        mainMenu = GameObject.Find("MainMenu").GetComponent<MainMenu>();

    }
	public void StartGame()
    {
        LoadGame();
        mainMenu.DeactiveMenu();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void PauseMenuStart()
    {
        if (!isPauseGame)
        {
            pauseGameMenu.SetActive(true);
            game.SetActive(false);
            Time.timeScale = 0f;
            isPauseGame = true;
            scoreText.text = "Score: " + scoreNumber.numberOfScore;
        }
        else
        {
            pauseGameMenu.SetActive(false);
            game.SetActive(true);
            Time.timeScale = 1f;
            isPauseGame = false;
        }
    }

    public void LoadGame()
    {
        gameMenu.name = "Game";
        Instantiate(gameMenu,canvas);
    }

    public void ResultGame()
    {
		resultMenu.SetActive(true);
        game.SetActive(false);
        Time.timeScale = 0f;
        GameObject.Find("ScoreText").GetComponent<Text>().text = "Score: " + scoreNumber.numberOfScore;
    }

    public void ReturnMainMenu()
    {
        GameObject destroyObbject = GameObject.Find(@"Game(Clone)");
        mainMenu.ActiveMenu();
        PauseMenuStart();
        CheckAndSaveScore();
        Destroy(destroyObbject);
    }

    public void CheckAndSaveScore()
    {
        endScore = scoreNumber.numberOfScore;
        canvas.GetComponent<SaveJson>().CheckScore(endScore);
    }
}
