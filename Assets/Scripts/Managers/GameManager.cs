using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using Cinemachine;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    #region Bools

    [Header("-- Bools --")]

    public static bool isGameStarted = false;
    public static bool isGameEnded = false;
    public static bool isGameRestarted = false;
    public bool isSpawnAuto = false;
    #endregion

    #region GameObject and Lists
    [Header("-- Lists --")]
    public List<GameObject> Levels;
    public GameObject Player;
    [Space]

    [Header("-- Cams --")]
    public CinemachineVirtualCamera vCamGame;
    [Space]
    #endregion

    public int levelCount = 0;
    public int nextLevel = 0;

    private Volume _volume;
    private void Awake()
    {
        #region Instance Jobs
        if (instance == null)
            instance = this;
        #endregion

    }

    private IEnumerator Start()
    {

        #region Bool Reset
        isGameStarted = false;
        isGameEnded = true;
        isGameRestarted = false;
        #endregion

        #region Add Listener Jobs

        UIManager.instance.NextLevelButton.onClick.AddListener(() => NextLevelFunc());
        UIManager.instance.RestartButton.onClick.AddListener(() => RestartLevelFunc());

        #endregion

        StartGame();

        UIManager.instance.LevelsText.text = "Level " +  (nextLevel + 1).ToString();

        _volume = GetComponent<Volume>();

        yield return new WaitForSeconds(0.1f);

        //If action is required a certain time after the game has started.

        #region Player Jobs

        Player = GameObject.FindGameObjectWithTag("Player");
        vCamGame.m_Follow = Player.gameObject.transform;
        vCamGame.m_LookAt = Player.gameObject.transform;

        #endregion

        #region Spawn Enemies Pool
       
        if (isSpawnAuto)
        {
            LevelManager.instance.SetSpawnObjectsPool();
        }

        #endregion

    }
    public void StartGame()
    {
        if (isGameRestarted)
        {
            UIManager.instance.MainMenu.SetActive(false);

        }

        levelCount = PlayerPrefs.GetInt("levelCount", levelCount);
        nextLevel = PlayerPrefs.GetInt("nextLevel", nextLevel);

        if (levelCount < 0 || levelCount >= Levels.Count)
        {
            levelCount = 0;
            nextLevel = 0;
            PlayerPrefs.SetInt("levelCount", levelCount);
        }
        CreateLevel(levelCount);
    }

    /// <summary>
    /// Create Level.
    /// </summary>
    /// <param name="Levelindex"> Level index from levelCount</param>
    public void CreateLevel(int Levelindex)
    {
        Instantiate(Levels[Levelindex], new Vector3(0, 0, 0), Levels[Levelindex].transform.rotation);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isGameEnded && UIManager.instance.MainMenu.activeInHierarchy)
        {

            UIManager.instance.MainMenu.SetActive(false);
            UIManager.instance.GameMenu.SetActive(true);

            isGameStarted = true;
            isGameEnded = false;
            _volume.enabled = false;

            #region Spawn Auto and Move

            if (isSpawnAuto)
            {
                LevelManager.instance.StartSpawn();
            }

            #endregion
        }

    }

    public void OnLevelCompleted()
    {
        StartCoroutine(WaitForFinish(0.5f));
    }

    //if Level Failed
    public void OnLevelFailed()
    {
        UIManager.instance.LoseMenu.SetActive(true);
        UIManager.instance.GameMenu.SetActive(false);

        isGameEnded = true;
        isGameStarted = false;
        _volume.enabled = true;
    }

    // Next Level Button
    public void NextLevelFunc()
    {
        #region Next Level Jobs

        isGameEnded = false;
        isGameRestarted = true;
        isGameStarted = true;

        levelCount++;
        nextLevel++;

        #endregion

        SetPlayerPrefs();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    // Rest.
    public void RestartLevelFunc()
    {
        isGameRestarted = true;
        isGameStarted = true;
        isGameEnded = false;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator WaitForFinish(float _waitTime)
    {
        isGameEnded = true;

        yield return new WaitForSeconds(_waitTime);

        UIManager.instance.WinMenu.SetActive(true);
        UIManager.instance.GameMenu.SetActive(false);
        _volume.enabled = true;
    }

    private void SetPlayerPrefs()
    {
        PlayerPrefs.SetInt("levelCount", levelCount);
        PlayerPrefs.SetInt("nextLevel", nextLevel);
    }
}