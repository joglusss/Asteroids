using Asteroids.Objects;
using UnityEngine;
using System.Collections.Generic;
using Asteroids.Visual;
using Asteroids.Score;

namespace Asteroids.SceneManage
{
    [System.Serializable]
    public class DependencyContainer
    {
        [field: SerializeField] public ObjectManager ObjectManagerLink { get; private set; }
        [field: SerializeField] public GameObject ShipLink { get; private set; }
        [field: SerializeField] public SceneContainerHandler SceneContainerHandlerLink { get; private set; }
        [field: SerializeField] public ShipStatView ShipStatViewLink { get; private set; }
        [field: SerializeField] public ScoreView ScoreViewLink { get; private set; }
        [field: SerializeField] public ScoreManager ScoreManagerLink { get; private set; }
        [field: SerializeField] public ScoreCounter ScoreCounterLink { get; private set; }

        public ShipStatModel ShipStatModelLink { get; private set; } = new();
        public ShipStatPresenter ShipStatPresenterLink { get; private set; } = new();

        public void InstantiatePrefabs()
        {
            ShipLink = Object.Instantiate(ShipLink);
        }

        public List<IInitialize> GetAllIInitialize()
        {
            List<IInitialize> returnList = new()
            {
                ObjectManagerLink,
                ShipStatPresenterLink,
                ScoreManagerLink,
                ScoreCounterLink
            };
            returnList.AddRange(ShipLink.GetComponents<IInitialize>());
            return returnList;
        }
    }
}