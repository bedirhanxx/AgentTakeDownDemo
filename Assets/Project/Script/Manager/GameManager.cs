using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    [Header("GameObjects")]
    public GameObject player;
    public AudioSource[] sound;
    public GameObject[] levelHolder;
    public GameObject drawArea;

    [Header("UI Panel")]    
    public GameObject finishPanel;
    public GameObject gameOverPanel;
    public GameObject menuPanel;
    public GameObject gamePanel;
    public Text levelText;
    public Text coinText;
    private int level;
    private int coin;

    [Header(("Perfect Sprites"))] 
    public Image amazing;
    public Image perfect;

    [Header(("Levels"))]
    public int LevelNumber = 0;
    private int textLevelNumber;

    [Header(("Control"))]
    public bool gameisStarted;
    public int TotalLevel;
    public bool winLevel;
    public int savedLevel;
    private int randomLevel;


    // ---------- START
    public override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        gamePanel.SetActive(false);
        GetDatas();
        LevelGenerator();       
    }

    #region GAME EVENTS
    // ---------- GAME EVENTS

    // ---------- GAME EVENTS

    // ---------- GAME EVENTS
    public void FinishLevel()
    {
        
        gameisStarted = false;
        drawArea.SetActive(false);
        LevelNumberManager.instance.LevelNumber++;
        StartCoroutine("FinishPanel", 0.5f);
    }
    public void GameOver()
    {
        gameisStarted = false;
        drawArea.SetActive(false);
        StartCoroutine("OverPanel", 0.5f);
    }

    IEnumerator FinishPanel()
    {
        yield return new WaitForSeconds(1f);
        finishPanel.SetActive(true);
        AddCoin(50);
        
    }

    IEnumerator OverPanel()
    {
        yield return new WaitForSeconds(1f);
        gameOverPanel.SetActive(true);
        
    }
    #endregion

    #region SAVE AND LEVEL SETUP

    private void LevelGenerator()
    {
        coinText.text = coin.ToString();
        levelText.text = "LEVEL " + textLevelNumber.ToString();
              
            if (LevelNumberManager.instance.LevelNumber > GameManager.Instance.levelHolder.Length - 1)
            {
                if (LevelNumberManager.instance.gameRestarted)
                {
                savedLevel = randomLevel;
                }
                else
                {
                randomLevel = Random.RandomRange(0, levelHolder.Length);
                }
                
                

                for (int i = 0; i < levelHolder.Length; i++)
                {
                    levelHolder[i].SetActive(false);
                    levelHolder[randomLevel].SetActive(true);
                }
            }
            else
            {
                for (int i = 0; i < levelHolder.Length; i++)
                {
                    levelHolder[i].SetActive(false);
                    savedLevel = LevelNumberManager.instance.LevelNumber;
                    levelHolder[LevelNumberManager.instance.LevelNumber].SetActive(true);
                }
            }

    }

    public void SceneLoad()
    {
        SceneManager.LoadScene(0);
    }



    public void getNextLevel()
    {
        if (LevelNumberManager.instance.LevelNumber == levelHolder.Length)
        {
            level = Random.RandomRange(0, levelHolder.Length);
            LevelGenerator();
            SceneLoad();
            Debug.Log("Load Random Level");
        }
        else
        {
            level = LevelNumber;
            LevelGenerator();
            SceneLoad();
            Debug.Log("Load Next Level");
        }
    }

    public void saveLevel()
    {
        LevelNumber = LevelNumberManager.instance.LevelNumber;
        PlayerPrefs.SetInt("level", LevelNumber);
        level = LevelNumber;
    }


    public void GetDatas()
    {
        // LEVEL
        if (PlayerPrefs.HasKey("level"))
        {
            level = LevelNumber;
            level = PlayerPrefs.GetInt("level");
            LevelNumberManager.instance.LevelNumber = level;

            LevelNumber = level;
            textLevelNumber = LevelNumber + 1;
        }
        else
        {
            PlayerPrefs.SetInt("level", 0);
            textLevelNumber = 1;
        }

        // GEM
        if (PlayerPrefs.HasKey("coin"))
        {
            coin = PlayerPrefs.GetInt("coin");
        }
        else
        {
            PlayerPrefs.SetInt("coin", coin);
        }

        // SOUND
        if (!PlayerPrefs.HasKey("sound"))
        {
            PlayerPrefs.SetInt("sound", 1);
        }
    }

    public void AddCoin(int newCoin)
    {
        int prevCoin = PlayerPrefs.GetInt("coin");
        PlayerPrefs.SetInt("coin", prevCoin + newCoin);
        coin = newCoin;
    }

    #endregion

    #region UI SETUP
    // ---------- UI BUTTON
    public void StartButton()
    {
        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
        drawArea.SetActive(true);
        gameisStarted = true;

    }

    public void RestartButton()
    {
        LevelNumberManager.instance.gameRestarted = true;
        SceneLoad();
        
        //FindObjectOfType<AdManager>().ShowAdmobInterstitial();
    }
    
    // PERFECT SYTSTEM
    public void Perfector()
    {
        perfect.gameObject.SetActive(true);
        perfect.transform.DOScale(5, 0.5f).SetEase(Ease.OutBounce).OnComplete(() =>
        {
            perfect.transform.DOScale(1, 0);
            perfect.gameObject.SetActive(false);
        });
    }

    public void Amazer()
    {
        amazing.gameObject.SetActive(true);
        amazing.transform.DOScale(5, 0.5f).SetEase(Ease.OutBounce).OnComplete(() =>
        {
            amazing.transform.DOScale(1, 0);
            amazing.gameObject.SetActive(false);
        });
    }

    #endregion


}