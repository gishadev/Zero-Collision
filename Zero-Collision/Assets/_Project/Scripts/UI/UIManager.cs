using TMPro;
using UnityEngine;

namespace Gisha.ZeroCollision.UI
{
    public class UIManager : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private TMP_Text currentScoreText = default;
        [SerializeField] private TMP_Text maxScoreText = default;
        [SerializeField] private GameObject pressToPlayPanel = default;

        [SerializeField] private Animator scoreAnimator = default;


        public void OnPlay()
        {
            pressToPlayPanel.SetActive(false);
            scoreAnimator.SetTrigger("HideBottom");
        }

        public void UpdateScoreText(int score, int maxScore)
        {
            currentScoreText.text = score.ToString();
            maxScoreText.text = maxScore.ToString();
        }
    }
}