using TMPro;
using UnityEngine;

namespace Gisha.ZeroCollision.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText = default;

        public void UpdateScoreText(int score)
        {
            scoreText.text = score.ToString();
        }
    }
}