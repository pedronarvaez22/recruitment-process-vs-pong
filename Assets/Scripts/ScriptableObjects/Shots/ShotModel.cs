using UnityEngine;

namespace ScriptableObjects.Shots
{
    [CreateAssetMenu(fileName = "New Shot" , menuName = "Create New Shot")]
    public class ShotModel : ScriptableObject
    {
        public string shotName;
        public int shotDamage;
        public int shotRicochet;
        public int shotSpeed;
        public Sprite shotSprite;
    
        public enum PerkType
        {
            None,
            IncreaseSize,
            IncreaseDamage
        }

        public PerkType shotPerk;
        [TextArea(5, 5)]
        public string shotPerkDescription;
    }
}
