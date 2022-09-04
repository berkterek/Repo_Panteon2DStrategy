﻿using Panteon2DStrategy.Abstracts.Animations;
using Panteon2DStrategy.Abstracts.Movements;
using Panteon2DStrategy.Enums;
using Panteon2DStrategy.ScriptableObjects;
using UnityEngine;

namespace Panteon2DStrategy.Abstracts.Controllers
{
    public interface ISoldierController : IEntityController,ICanSelectableController,IHealthController,IAttackerController
    {
        Transform Target { get; set; }
        Vector3 TargetPosition { get; set; }
        ISoldierStats Stats { get; }
        ISoldierAnimationService AnimationManager { get; }
        IMovementService MovementManager { get; }
        void SetDestination(Vector3 position);
        void SetPlayer(PlayerType playerType);
    }
}