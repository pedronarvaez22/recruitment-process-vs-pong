using UnityEngine;

namespace GameModesStructure
{
    public class InitGameMode : MonoBehaviour
    {
        [Header("All Game Modes Avaliable")]
        [SerializeField]
        //Index: 0 - NormalBattle / 1 - RogueBattle
        private GameObject[] gameModesPrefabs;
        [Header("Container Reference")]
        [SerializeField]
        private Transform gameManagerContainer;

        private void Awake()
        {
            switch(PlayerPrefs.GetString("PPGameMode"))
            {
                case "NormalBattle":
                    Instantiate(gameModesPrefabs[0], gameManagerContainer);
                    break;
                case "RogueBattle":
                    Instantiate(gameModesPrefabs[1], gameManagerContainer);
                    break;
            }
        }
    }
}
