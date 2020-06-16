using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public bool gameStarted;
    public bool SoundOn;

    public GameObject MenuUI;
    public GameObject GameUI;
    public GameObject GameOverUI;
    public AudioSource ClickSound;
    public AudioSource MenuMusic;
    public AudioSource GameMusic;
    public AudioSource TractorSound;
    public ParticleSystem TractorSmoke;
    public Text GameOverDistTxt;
    public Text MenuDistTxt;


    private int BestDistance;


    void Start()
    {
        gm = this;
        BestDistance = PlayerPrefs.GetInt("BestDistance");
        MenuDistTxt.text = BestDistance.ToString();

    }

    public void Play()
    {
        gameStarted = true;
        ClickSound.Play();
        MenuMusic.Stop();
        GameMusic.Play();
        TractorSound.Play();
        TractorSmoke.Play();
        MenuUI.SetActive(false);
        GameUI.SetActive(true);
    }


    public void GameOver()
    {
        gameStarted = false;
        MenuMusic.Play();
        GameMusic.Stop();
        TractorSound.Stop();
        TractorSmoke.Stop();
        GameUI.SetActive(false);
        GameOverUI.SetActive(true);

    }


    public void RestartScene()
    {
        ClickSound.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
