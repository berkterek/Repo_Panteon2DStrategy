using Panteon2DStrategy.Abstracts.Controllers;
using Panteon2DStrategy.Abstracts.Inputs;
using Panteon2DStrategy.Abstracts.Movements;
using Panteon2DStrategy.Enums;
using Panteon2DStrategy.Managers;
using Panteon2DStrategy.Movements;
using Panteon2DStrategy.ViewModels;
using Panteon2DStrategyScripts.Helpers;
using UnityEngine;

namespace Panteon2DStrategy.Controllers
{
    public class PlayerController : MonoBehaviour, IPlayerController
    {
        [SerializeField] Transform _transform;
        [SerializeField] Transform _moverObject;
        [SerializeField] Transform _moverCamera;

        public Transform Transform => _transform;
        public IInputService InputManager { get; set; }
        public IMovementService MoveManager { get; private set; }
        public Camera MainCamera { get; private set; }

        void Awake()
        {
            InputManager = PlayerInputManager.CreateSingleton(InputType.OldInput);
            MainCamera = Camera.main;
            this.GetReference<Transform>(ref _transform);
            var moveViewModel = new PlayerMovementViewModel();
            moveViewModel.PlayerController = this;
            moveViewModel.MoverDalArray = new IMoverDal[]
                { new MoveWithMousePosition(_moverObject), new MoveWithTransformDal(_moverObject) };
            MoveManager = new PlayerMovementManager(moveViewModel);
        }

        void OnValidate()
        {
            this.GetReference<Transform>(ref _transform);
        }

        void Update()
        {
            MoveManager.Tick();
        }

        void FixedUpdate()
        {
            MoveManager.FixedTick();
        }
    }
}