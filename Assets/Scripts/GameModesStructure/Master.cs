using System.Collections;
using Mechanics;
using UI;
using UnityEngine;

namespace GameModesStructure
{
    public class Master : MonoBehaviour
    {
        [Header("Current Game State")]
        public GameStates currentGameState;
        [Header("Game Over Bool")]
        public bool gameOver;
        [Header("All Obstacles Avaliable")]
        [SerializeField]
        protected GameObject[] obstaclePrefabs;
        protected GameObject ObstacleContainer;
        protected Paddle PlayerOne;
        protected Paddle PlayerTwo;
        protected UIManager UIManager;

        public bool isPaused;

        private Shot _shotPrefab;

        private static readonly int InfoAppear = Animator.StringToHash("InfoAppear");

        public enum GameStates
        {
            PlayerChoosingWeapons,
            PlayerOneTurn,
            PlayerTwoTurn,
            PlayerOneWin,
            PlayerTwoWin
        }

        private void Awake()
        {
            UIManager = FindObjectOfType<UIManager>();
        }

        private void Start()
        {
            Init();
        }

        public virtual void Init()
        {
            UIManager.SetMaster(this);
            gameOver = false;
            PlayerOne = UIManager.PlayerOne;
            PlayerTwo = UIManager.PlayerTwo;
            ObstacleContainer = UIManager.ObstacleContainer;
            PlayerOne.OnShot += PlayerShot;
            PlayerTwo.OnShot += PlayerShot;
            StartCoroutine(ChangeGameStates(GameStates.PlayerChoosingWeapons));

        }

        public virtual IEnumerator ChangeGameStates(GameStates newState)
        {
            currentGameState = newState;

            switch(currentGameState)
            {
                case GameStates.PlayerChoosingWeapons:
                    UIManager.infoLabelText.text = "Player 1 Picks";
                    UIManager.infoLabelAnim.SetBool(InfoAppear, true);
                    UIManager.PickYourShotPanel.SetActive(true);
                    yield return null;
                    break;
                case GameStates.PlayerOneTurn:
                    UIManager.infoLabelText.text = "Player 1 Turn";
                    UIManager.infoLabelAnim.SetBool(InfoAppear, true);
                    PlayerOne.arrow.SetActive(true);
                    break;
                case GameStates.PlayerTwoTurn:
                    UIManager.infoLabelText.text = "Player 2 Turn";
                    UIManager.infoLabelAnim.SetBool(InfoAppear, true);
                    PlayerTwo.arrow.SetActive(true);
                    break;
                case GameStates.PlayerOneWin:
                    UIManager.infoLabelText.text = "Player 1 Wins!";
                    UIManager.infoLabelAnim.SetBool(InfoAppear, true);
                    UIManager.WinPanel.SetActive(true);
                    break;
                case GameStates.PlayerTwoWin:
                    UIManager.infoLabelText.text = "Player 2 Wins!";
                    UIManager.infoLabelAnim.SetBool(InfoAppear, true);
                    UIManager.WinPanel.SetActive(true);
                    break;
            }
        }

        public void PlayerShot(Shot shot)
        {
            _shotPrefab = shot;
            _shotPrefab.OnShotDisable += ShotDestoyed;
            UIManager.infoLabelAnim.SetBool(InfoAppear, false);
        }

        public void ShotDestoyed()
        {
            if(currentGameState == GameStates.PlayerOneTurn)
            {
                StartCoroutine(ChangeGameStates(GameStates.PlayerTwoTurn));
            }
            else if( currentGameState == GameStates.PlayerTwoTurn)
            {
                StartCoroutine(ChangeGameStates(GameStates.PlayerOneTurn));
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isPaused = !isPaused;
                PauseGame();
            }
        }

        public void PauseGame()
        {
            if (isPaused)
            {
                UIManager.PausePanel.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
                UIManager.PausePanel.SetActive(false);
            }
        }
    }
}
