using System.Collections;
using UnityEngine;

namespace GameModesStructure
{
    public class RogueMode : Master
    {
        private static readonly int InfoAppear = Animator.StringToHash("InfoAppear");

        public override IEnumerator ChangeGameStates(GameStates newState)
        {
            currentGameState = newState;

            switch (currentGameState)
            {
                case GameStates.PlayerChoosingWeapons:
                    UIManager.infoLabelText.text = "Player 1 Picks";
                    UIManager.infoLabelAnim.SetBool(InfoAppear, true);
                    UIManager.PickYourShotPanel.SetActive(true);
                    yield return null;
                    break;
                case GameStates.PlayerOneTurn:
                    CreateNewObstacles();
                    UIManager.infoLabelText.text = "Player 1 Turn";
                    UIManager.infoLabelAnim.SetBool(InfoAppear, true);
                    PlayerOne.arrow.SetActive(true);
                    break;
                case GameStates.PlayerTwoTurn:
                    CreateNewObstacles();
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

        private void CreateNewObstacles()
        {
            foreach (Transform child in ObstacleContainer.transform)
            {
                Destroy(child.gameObject);
            }

            int obstacleIndex = Random.Range(0, obstaclePrefabs.Length);
            Instantiate(obstaclePrefabs[obstacleIndex], ObstacleContainer.transform);

        }
    }
}
