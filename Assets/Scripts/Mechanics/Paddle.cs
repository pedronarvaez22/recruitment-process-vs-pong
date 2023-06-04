using System;
using System.Collections;
using GameModesStructure;
using ScriptableObjects.Shots;
using UI;
using UnityEngine;

namespace Mechanics
{
    public class Paddle : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField]
        private float speed;
        public float movement;

        [Header("Rotation")]
        private const float MinRot = -50f;
        private const float MaxRot = 50f;
        public float rotationZ;
        public float sensitivityZ = 5;

        [Header("Shooting")]
        public GameObject arrow;
        [SerializeField]
        private Shot shotPrefab;
        public ShotModel myShotModel;
        [SerializeField]
        private GameObject muzzle;

        [Header("Player Animator")]
        [SerializeField]
        private Animator playerAnimator;

        [Header("Player RigidBody2D")]
        [SerializeField]
        private Rigidbody2D playerRigidBody2D;

        [Header("MISC Player Info")]
        public int playerHp;

        [SerializeField]
        private bool isPlayer1;

        public event Action<Shot> OnShot;
        private Master _master;
        private UIManager _uiManager;
        private static readonly int DamageHash = Animator.StringToHash("Damage");

        private void Start()
        {
            _uiManager = FindObjectOfType<UIManager>();
            _master = FindObjectOfType<Master>();
            _uiManager.HpRefresh(isPlayer1, playerHp);
        }

        private void Update()
        {
            if (_master.currentGameState == Master.GameStates.PlayerOneTurn || _master.currentGameState == Master.GameStates.PlayerTwoTurn)
            {
                Move();
                AdjustAngle();

                if (Input.GetKeyDown(KeyCode.Space) && arrow.gameObject.activeSelf && !_master.isPaused)
                    Shoot();
            }
        }

        private void Move()
        {
            if (isPlayer1)
            {
                movement = Input.GetAxisRaw("VerticalP1");
            }
            else
            {
                movement = Input.GetAxisRaw("VerticalP2");
            }

            playerRigidBody2D.velocity = new Vector2(playerRigidBody2D.velocity.x, movement * speed);
        }

        private void AdjustAngle()
        {
            if(arrow.gameObject.activeSelf)
            {
                rotationZ += (Input.GetAxis("Horizontal") * sensitivityZ * 10) * Time.deltaTime;
                rotationZ = Mathf.Clamp(rotationZ, MinRot, MaxRot);

                if (isPlayer1)
                {
                    arrow.transform.localEulerAngles = new Vector3(0, 0, -rotationZ);
                }
                else
                {
                    arrow.transform.localEulerAngles = new Vector3(0, 0, rotationZ);
                }
            }
        }

        private void Shoot()
        {
            var newShot = Instantiate(shotPrefab, muzzle.transform);
            newShot.transform.parent = null;
            newShot.PopulateShot(myShotModel);
            OnShot?.Invoke(newShot);

            arrow.SetActive(false);
        }

        public void Damage(int amount)
        {
            playerHp -= amount;
            playerAnimator.SetBool(DamageHash, true);

            if(playerHp <= 0)
            {
                playerHp = 0;
                _uiManager.HpRefresh(isPlayer1, playerHp);

                if (isPlayer1)
                {
                    StartCoroutine(_master.ChangeGameStates(Master.GameStates.PlayerTwoWin));
                }
                else
                {
                    StartCoroutine(_master.ChangeGameStates(Master.GameStates.PlayerOneWin));
                }
            }
            else
            {
                _uiManager.HpRefresh(isPlayer1, playerHp);
            }
        }
    }
}
