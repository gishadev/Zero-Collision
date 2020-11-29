using Gisha.ZeroCollision.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gisha.ZeroCollision.Game
{
    public class GameManager : MonoBehaviour
    {
        int Score { get; set; }

        UIManager _UI;

        private void Awake()
        {
            _UI = FindObjectOfType<UIManager>();
        }

        public void Lose()
        {
            Debug.Log("You Lose!");
            SceneManager.LoadScene(0);
        }

        public void AddScore()
        {
            Score++;

            _UI.UpdateScoreText(Score);
        }
    }
}