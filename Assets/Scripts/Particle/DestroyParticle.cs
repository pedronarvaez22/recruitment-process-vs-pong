using UnityEngine;

namespace Particle
{
    public class DestroyParticle : MonoBehaviour
    {
        private void Start()
        {
            Destroy(gameObject, 1f);
        }
    }
}
