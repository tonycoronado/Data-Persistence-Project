using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public static MainManager instance;

    public Brick BrickPrefab;
    public int LineCount = 6;
    public int BestScore;
    public Rigidbody Ball;
    public string PlayerName;

    public Text BestScoreText;
    public Text ScoreText;
    public GameObject GameOverText;
    public GameObject MenuButton;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    private void Awake()
    {
        
        if (instance != null)
        {
            Destroy(instance);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); //Singleton

    }

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            DataManager.instance.LoadBestScoreData();
            const float step = 0.6f;
            int perLine = Mathf.FloorToInt(4.0f / step);

            int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
            for (int i = 0; i < LineCount; ++i)
            {
                for (int x = 0; x < perLine; ++x)
                {
                    Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                    var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                    brick.PointValue = pointCountArray[i];
                    brick.onDestroyed.AddListener(AddPoint);
                }
            }

            return;
        }
        
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);

            }
        }
        else if (m_GameOver)
        {
            CheckBestScore(m_Points, PlayerName);
            UpdateBestScore();
            

            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                DataManager.instance.LoadBestScoreData();
            }
        }
    }


    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        MenuButton.SetActive(true);
        DataManager.instance.SaveBestScoreData();
    }

    private void UpdateBestScore()
    {
        BestScoreText.text = "Best Score: " + PlayerName + " " + BestScore;
    }

    private void CheckBestScore(int p_Points, string p_playerName)
    {
        if (BestScore < p_Points)
        {
            BestScore = p_Points;
            PlayerName = DataManager.instance.playerName;
        }
    }

    
}
