using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] GameManager game;
    [SerializeField] GameObject pauseMenu;
    private string StartMenu = "StartMenu";
    private string SelectMenu = "SelectMenu";

    private string LevelKorsakov = "LevelKorsakov";
    private string LevelVivaldi = "LevelVivaldi";
    private string LevelBeethoven = "LevelBeethoven";

    private bool isPaused = false;

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadPreviousScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Pause()
    {
        if (isPaused == false && !pauseMenu.activeInHierarchy)
        {
            isPaused = true;
            Time.timeScale = 0;
            game.track.Pause();
            pauseMenu.SetActive(true);
        }
    }

    public void unPause()
    {
        if (isPaused == true && pauseMenu.activeInHierarchy)
        {
            isPaused = false;
            Time.timeScale = 1;
            game.track.Play();
            pauseMenu.SetActive(false);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void LoadStartMenu()
    {
        SceneManager.LoadScene("SelectMenu");
    }

    public void LoadSelectMenu()
    {
        SceneManager.LoadScene("SelectMenu");
    }

    public void Flight()
    {
        StartCoroutine(CO_LoadBuffer());
        SceneManager.LoadScene("LevelKorsakov");
    }

    public void Seasons() //helloworld
    {
        StartCoroutine(CO_LoadBuffer());
        SceneManager.LoadScene("LevelKorsakov");
    }
    public void FurElise() //helloworld
    {
        StartCoroutine(CO_LoadBuffer());
        SceneManager.GetSceneByBuildIndex(4);
    }

    private IEnumerator CO_LoadBuffer()
    {
        yield return new WaitForSeconds(2f);
    }
}
