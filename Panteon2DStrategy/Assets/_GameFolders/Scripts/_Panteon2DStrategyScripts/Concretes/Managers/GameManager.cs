using UnityEngine;

namespace Panteon2DStrategy.Managers
{
    public class GameManager : MonoBehaviour
    {
        void Awake()
        {
            Application.targetFrameRate = 60;
        }
    }    
}