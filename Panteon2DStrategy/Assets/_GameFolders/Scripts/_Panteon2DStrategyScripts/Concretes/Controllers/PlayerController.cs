using Panteon2DStrategy.Abstracts.Controllers;
using Panteon2DStrategy.Abstracts.Inputs;
using Panteon2DStrategy.Abstracts.Movements;
using Panteon2DStrategy.Enums;
using Panteon2DStrategy.Factories;
using Panteon2DStrategy.Managers;
using Panteon2DStrategyScripts.Helpers;
using UnityEngine;

namespace Panteon2DStrategy.Controllers
{
    public class PlayerController : MonoBehaviour, IPlayerController
    {
        [SerializeField] Transform _transform;
        [SerializeField] Transform _moverObject;
        
        IMovementService _moveManager;

        public Transform Transform => _transform;
        public IInputService InputManager { get; set; }
        public Camera MainCamera { get; private set; }
        public Transform TargetMover => _moverObject;

        void Awake()
        {
            InputManager = PlayerInputManager.CreateSingleton(InputType.OldInput);
            MainCamera = Camera.main;
            this.GetReference<Transform>(ref _transform);
            _moveManager = new PlayerMovementManager(PlayerMovementFactory.Create(this));
        }

        void OnValidate()
        {
            this.GetReference<Transform>(ref _transform);
        }

        void Update()
        {
            _moveManager.Tick();
        }

        void FixedUpdate()
        {
            _moveManager.FixedTick();
        }
    }
}