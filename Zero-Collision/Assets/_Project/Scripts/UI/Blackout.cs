using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gisha.ZeroCollision.Game
{
    public class Blackout : MonoBehaviour
    {
        #region Singleton
        public static Blackout Instance { get; private set; }
        #endregion

        Animator _animator;

        private void Awake()
        {
            CreateInstance();
            _animator = GetComponent<Animator>();
        }

        void CreateInstance()
        {
            DontDestroyOnLoad(transform.parent.gameObject);

            if (Instance == null)
                Instance = this;
            else
            {
                if (Instance != this)
                    Destroy(transform.parent.gameObject);
            }
        }

        public void EnterBlackout()
        {
            _animator.SetTrigger("Enter");
        }

        private void OnLeaveBlackout(Scene arg0, LoadSceneMode arg1)
        {
            _animator.SetTrigger("Leave");
            SceneManager.sceneLoaded -= OnLeaveBlackout;
        }

        public void ReloadScene()
        {
            SceneManager.LoadScene(0);
            SceneManager.sceneLoaded += OnLeaveBlackout;
        }

    }
}