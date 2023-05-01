using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] GameObject window;
    [SerializeField] TMP_Text score;

    void Start()
    {
        Game.Instance.OnGameOver += Game_onGameOver;     
    }

    private void Game_onGameOver()
    {
        score.text = "Score: " + Game.Instance.score;
        window.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
