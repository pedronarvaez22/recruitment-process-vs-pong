using GameModesStructure;
using Mechanics;
using ScriptableObjects.Shots;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [Header("Info Label Reference")]
        public Text infoLabelText;
        public Animator infoLabelAnim;

        [Header("Players HP Text")]
        [SerializeField]
        private Text playerOneHpText;
        [SerializeField]
        private Text playerTwoHpText;

        [Header("Panels References")]
        [SerializeField]
        private GameObject pickYourShotPanel;
        public GameObject PickYourShotPanel => pickYourShotPanel;
        [SerializeField]
        private GameObject winPanel;
        public GameObject WinPanel => winPanel;
        [SerializeField]
        private GameObject pausePanel;
        public GameObject PausePanel => pausePanel;

        [Header("Populate Pick Your Shot Panel")]
        [SerializeField]
        private Image shotSprite;
        [SerializeField]
        private Text shotNameText;
        [SerializeField]
        private Text powerText;
        [SerializeField]
        private Text speedText;
        [SerializeField]
        private Text ricochetText;
        [SerializeField]
        private Text perkText;

        [Header("ShotModel References")]
        [SerializeField]
        private ShotModel circleShotModel;
        [SerializeField]
        private ShotModel octagonShotModel;
        [SerializeField]
        private ShotModel hexagonShotModel;
        [SerializeField]
        private ShotModel shotSelected;

        [SerializeField]
        [Header("Players References")]
        private Paddle playerOne;
        public Paddle PlayerOne => playerOne;
        [SerializeField]
        private Paddle playerTwo;
        public Paddle PlayerTwo => playerTwo;

        [Header("Obstacle Container Reference")]
        [SerializeField]
        private GameObject obstacleContainer;
        public GameObject ObstacleContainer => obstacleContainer;

        private Master _master;
        public Master Master => _master;

        public void HpRefresh(bool isPlayer1, int hpAmount)
        {
            if (isPlayer1)
            {
                playerOneHpText.text = hpAmount.ToString();
            }
            else
            {
                playerTwoHpText.text = hpAmount.ToString();
            }
        }

        public void DisplayOnShotPanel(string shotName)
        {
            switch(shotName)
            {
                case "Circle Shot":
                    PopulateShotArea(circleShotModel);
                    break;
                case "Hexagon Shot":
                    PopulateShotArea(hexagonShotModel);
                    break;
                case "Octagon Shot":
                    PopulateShotArea(octagonShotModel);
                    break;
            }

        }

        private void PopulateShotArea(ShotModel shotModelToPopulate)
        {
            shotSelected = shotModelToPopulate;
            shotSprite.sprite = shotModelToPopulate.shotSprite;
            shotNameText.text = shotModelToPopulate.shotName;
            powerText.text = shotModelToPopulate.shotDamage.ToString();
            speedText.text = shotModelToPopulate.shotSpeed.ToString();
            ricochetText.text = shotModelToPopulate.shotRicochet.ToString();
            perkText.text = shotModelToPopulate.shotPerkDescription;
        }

        public void PlayerChooseShot(bool isPlayer1)
        {
            if(isPlayer1)
            {
                playerOne.myShotModel = shotSelected;
                infoLabelText.text = "Player 2 Picks";
            }
            else
            {
                playerTwo.myShotModel = shotSelected;
                StartCoroutine(_master.ChangeGameStates(Master.GameStates.PlayerOneTurn));
            }
        }

        public void ButtonIsPressed(string buttonName)
        {
            switch(buttonName)
            {
                case "Try Again":
                    SceneManager.LoadScene("GameScene");
                    Time.timeScale = 1;
                    break;
                case "Main Menu":
                    SceneManager.LoadScene("MainMenu");
                    Time.timeScale = 1;
                    break;
                case "Resume":
                    _master.isPaused = !_master.isPaused;
                    _master.PauseGame();
                    break;
            }
        }

        public void SetMaster(Master master)
        {
            _master = master;
        }
    }
}
