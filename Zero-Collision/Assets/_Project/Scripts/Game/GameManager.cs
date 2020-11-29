using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gisha.ZeroCollision.Game
{
    public class GameManager : MonoBehaviour
    {
        public void Lose()
        {
            Debug.Log("You Lose!");
            SceneManager.LoadScene(0);
        }
    }
}