using System;
using ScriptableObjects.Shots;
using UnityEngine;

namespace Mechanics
{
    public class Shot : MonoBehaviour
    {
        [Header("Shot Info")]
        public int shotDamage;
        public float shotSpeed;
        public float shotRicochet;
        public ShotModel.PerkType shotPerk;
        public SpriteRenderer spriteRenderer;

        [Header("MISC Shot Information")]
        public Rigidbody2D rb;
        private Vector2 _direction;
        [SerializeField]
        private GameObject impactParticle;
        public float lifeTimeSpan;
        public event Action OnShotDisable;

        private void Start()
        {
            _direction = transform.right * shotSpeed;
            rb.velocity = _direction;
        }

        private void FixedUpdate()
        {
            _direction = rb.velocity;
        }
        private void Update()
        {
            LifeTimeSpan();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Obstacle"))
            {
                Bounce(collision.contacts[0].normal);
                shotRicochet--;
                Instantiate(impactParticle, transform.position, Quaternion.identity);
                PerkEffect(shotPerk);
                if(shotRicochet <= 0)
                {
                    OnShotDisable?.Invoke();
                    Destroy(gameObject);
                }
            }

            if(collision.gameObject.CompareTag("Player"))
            {
                OnShotDisable?.Invoke();
                Instantiate(impactParticle, transform.position, Quaternion.identity);
                collision.gameObject.GetComponent<Paddle>().Damage(shotDamage);
                Destroy(gameObject);
            }

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.CompareTag("Bounds"))
            {
                return;
            }

            OnShotDisable?.Invoke();
            Destroy(gameObject);
        }

        private void Bounce(Vector2 collisionNormal)
        {
            var speed = _direction.magnitude;
            var dir = Vector2.Reflect(_direction.normalized, collisionNormal);

            rb.velocity = dir * speed;
        }

        public void PopulateShot(ShotModel shotToPopulate)
        {
            spriteRenderer.sprite = shotToPopulate.shotSprite;
            shotDamage = shotToPopulate.shotDamage;
            shotSpeed = shotToPopulate.shotSpeed;
            shotRicochet = shotToPopulate.shotRicochet;
            shotPerk = shotToPopulate.shotPerk;
        }

        private void PerkEffect(ShotModel.PerkType shotPerkEffect)
        {
            switch(shotPerkEffect)
            {
                case ShotModel.PerkType.None:
                    break;
                case ShotModel.PerkType.IncreaseSize:
                    transform.localScale += new Vector3(.2f, .2f, .2f);
                    break;
                case ShotModel.PerkType.IncreaseDamage:
                    shotDamage++;
                    break;
            }
        }

        private void LifeTimeSpan()
        {
            lifeTimeSpan -= Time.deltaTime;
            if(lifeTimeSpan <= 0)
            {
                OnShotDisable?.Invoke();
                Destroy(gameObject);
            }
        }
    }
}
