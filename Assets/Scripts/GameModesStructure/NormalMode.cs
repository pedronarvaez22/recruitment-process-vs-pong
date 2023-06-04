using UnityEngine;

namespace GameModesStructure
{
    public class NormalMode : Master
    {
        public override void Init()
        {
            base.Init();

            int obstacleIndex = Random.Range(0, obstaclePrefabs.Length);
            Instantiate(obstaclePrefabs[obstacleIndex], ObstacleContainer.transform);
        }
    }
}
