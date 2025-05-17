using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Asteroids.SceneManage
{
    public class InitializerQueue : MonoBehaviour
    {
        [SerializeField] private List<MonoBehaviour> _initializeOrder;

        private void Awake()
        {
            foreach (var item in _initializeOrder)
                (item as IInitialize).Initialize();
        }

        private void OnValidate()
        {
            if (_initializeOrder != null && _initializeOrder.Count != 0)
            {
                List<MonoBehaviour> results = new();
                _initializeOrder.Last().GetComponents(results);

                results = results.Where(x => x is IInitialize).ToList();
                _initializeOrder.AddRange(results);

                _initializeOrder = _initializeOrder.Distinct().ToList();
                _initializeOrder = _initializeOrder.Where(x => x is IInitialize).ToList();
            }
        }
    }
}
