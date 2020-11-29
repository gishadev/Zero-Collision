using Gisha.ZeroCollision.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gisha.ZeroCollision.Game
{
    public class GameManager : MonoBehaviour
    {
        #region Singleton 
        public static GameManager Instance { get; private set; }
        #endregion

        public bool IsPlaying { get; private set; } = false;

        int Score { get; set; }
        int MaxScore
        {
            get
            {
                if (PlayerPrefs.HasKey("MaxScore"))
                    return PlayerPrefs.GetInt("MaxScore");
                else
                {
                    PlayerPrefs.SetInt("MaxScore", Score);
                    return Score;
                }
            }

            set => PlayerPrefs.SetInt("MaxScore", value);
        }

        UIManager _UI;

        private void Awake()
        {
            Instance = this;

            _UI = FindObjectOfType<UIManager>();
        }

        private void Start()
        {
            _UI.UpdateScoreText(Score, MaxScore);
        }

        private void Update()
        {
            if (IsPlaying)
                return;

            if (Input.anyKeyDown)
                Play();
        }

        public void Play()
        {
            IsPlaying = true;

            _UI.OnPlay();
        }

        public void Lose()
        {
            Debug.Log("You Lose!");
            SceneManager.LoadScene(0);
        }

        public void AddScore()
        {
            Score++;

            if (Score > MaxScore)
                MaxScore = Score;

            _UI.UpdateScoreText(Score, MaxScore);
        }
    }
}