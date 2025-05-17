using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Asteroids.SceneManage
{
    public class Initializer : MonoBehaviour
    {
        [SerializeField] private DependencyContainer _dependencyContainer;
        [SerializeField] private List<MonoBehaviour> _initializeOrder;

        private void Awake()
        {
            _dependencyContainer.InstantiatePrefabs();

            foreach (IInitialize item in _dependencyContainer.GetAllIInitialize())
            {
                if(item is MonoBehaviour)
                    _initializeOrder.Remove(item as MonoBehaviour);
                item.Initialize(_dependencyContainer);
            }


            foreach (var item in _initializeOrder)
                (item as IInitialize).Initialize(_dependencyContainer);
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
