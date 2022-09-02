using System.Linq;
using Panteon2DStrategy.Abstracts.Helpers;
using Panteon2DStrategy.Controllers;
using Panteon2DStrategy.Enums;
using UnityEngine;

namespace Panteon2DStrategy.Managers
{
    public class PlayerManager : SingletonDestroyObject<PlayerManager>
    {
        [SerializeField] PlayerManagerInspector[] _players;

        public PlayerManagerInspector[] Players => _players;
        
        void Awake()
        {
            SetSingleton(this);
        }

        public PlayerManagerInspector GetPlayer(PlayerType playerType)
        {
            return _players.FirstOrDefault(x => x.PlayerType == playerType);
        }
    }

    [System.Serializable]
    public class PlayerManagerInspector
    {
        [SerializeField] PlayerType _playerType;
        [SerializeField] PlayerController _playerController;

        public PlayerType PlayerType => _playerType;
        public PlayerController PlayerController => _playerController;
    }
}