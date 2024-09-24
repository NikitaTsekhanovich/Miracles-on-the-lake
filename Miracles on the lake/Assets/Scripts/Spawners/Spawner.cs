using System.Collections;
using System.Collections.Generic;
using AbilityControllers;
using GameEntities;
using GameEntities.Types;
using GameEntities.TypesPhysicsMovement;
using PlayerInterface;
using UnityEngine;

namespace Spawners
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private Transform _wormContainer;
        [SerializeField] private Transform _eagleContainer;
        [SerializeField] private List<Transform> _spawnPointsWorm = new();
        [SerializeField] private List<Transform> _spawnPointsEagle = new();
        [SerializeField] private Worm _worm;
        [SerializeField] private Eagle _eagle;

        private float _currentTime;
        private float _delay = 5f;
        private float _firstStage = 35f;
        private float _secondStage = 90f;

        private bool _wasEagle = true;
        private bool _canSpawnEagle = true;

        private void OnEnable()
        {
            AbilityController.OnOffEagles += DoOffEagles;
            PlayerInterfaceController.OnRestartSpawner += RestartSpawner;
        }

        private void OnDisable()
        {
            AbilityController.OnOffEagles -= DoOffEagles;
            PlayerInterfaceController.OnRestartSpawner -= RestartSpawner;
        }

        public void StartSpawn()
        {
            StartCoroutine(SpawnEntity());
        }

        private IEnumerator SpawnEntity()
        {
            yield return new WaitForSeconds(2f);
            while (true)
            {
                if (_currentTime <= _firstStage)
                    DoFirstStage();
                else if (_currentTime <= _secondStage) 
                    DoSecondStage();
                else 
                    DoThirdStage();

                yield return new WaitForSeconds(_delay);
                _currentTime += _delay;
            }
        }

        private void RestartSpawner()
        {
            _currentTime = 0;
            _delay = 5f;
            _wasEagle = true;
            _canSpawnEagle = true;

            while (_wormContainer.transform.childCount > 0) 
                DestroyImmediate(_wormContainer.transform.GetChild(0).gameObject);

            while (_eagleContainer.transform.childCount > 0) 
                DestroyImmediate(_eagleContainer.transform.GetChild(0).gameObject);
        }

        private void ChooseSpawnWorm(int indexSpawnPoint)
        {
            var worm = GetInstantiateGameEntity(_spawnPointsWorm[indexSpawnPoint], _worm, _wormContainer);
            worm.PhysicsMovementWorm.ChooseForce(indexSpawnPoint);

            _wasEagle = false;
        }

        private void ChooseSpawnEagle(int indexSpawnPoint)
        {
            if (!_canSpawnEagle)
                return;

            var eagle = GetInstantiateGameEntity(_spawnPointsEagle[indexSpawnPoint], _eagle, _eagleContainer);
            eagle.PhysicsMovementEagle.DoFly();

            _wasEagle = true;
        }

        private void DoFirstStage()
        {
            if (_wasEagle || !_canSpawnEagle)
            {
                ChooseSpawnWorm(Random.Range(0, _spawnPointsWorm.Count));
            }
            else 
            {
                ChooseSpawnEagle(Random.Range(0, _spawnPointsEagle.Count));
            }
        }

        private void DoSecondStage()
        {
            _delay = 4f;

            if (_wasEagle || !_canSpawnEagle)
            {
                ChooseSpawnWorm(Random.Range(0, _spawnPointsWorm.Count));
            }
            else 
            {
                ChooseSpawnWorm(Random.Range(0, _spawnPointsWorm.Count));
                ChooseSpawnEagle(Random.Range(0, _spawnPointsEagle.Count));
            }
        }

        private void DoThirdStage()
        {
            var randomSpawnLotWorms = Random.Range(0, 5);
            var randomSpawnLotEagle = Random.Range(0, 5);

            if (randomSpawnLotWorms == 0)
            {
                ChooseSpawnWorm(Random.Range(0, 2)); 
                ChooseSpawnWorm(Random.Range(2, 4));
                ChooseSpawnWorm(Random.Range(4, 6));
                ChooseSpawnWorm(Random.Range(6, 8));
            }

            if (randomSpawnLotEagle == 0)
            {
                ChooseSpawnEagle(Random.Range(0, 2));
                ChooseSpawnEagle(Random.Range(2, 4));
                ChooseSpawnEagle(Random.Range(4, 6)); 
                ChooseSpawnEagle(Random.Range(6, 8));
            }

            if (_wasEagle || !_canSpawnEagle)
            {
                ChooseSpawnWorm(Random.Range(0, 4));
                ChooseSpawnWorm(Random.Range(4, 8));
            }
            else 
            {
                ChooseSpawnWorm(Random.Range(0, 4)); 
                ChooseSpawnWorm(Random.Range(4, 8));
                ChooseSpawnEagle(Random.Range(0, 4)); 
                ChooseSpawnEagle(Random.Range(4, 8));
            }
        }

        private TEntity GetInstantiateGameEntity<TEntity>(Transform spawnPoint, TEntity entity, Transform container)
            where TEntity : Entity
        {
            var spawnPosition = new Vector3(
                spawnPoint.transform.position.x,
                spawnPoint.transform.position.y,
                0
            );

            return Instantiate(
                entity, 
                spawnPosition, 
                Quaternion.identity, 
                container);
        }

        private void DoOffEagles()
        {
            _canSpawnEagle = false;

            for (var i = 0; i < _eagleContainer.transform.childCount; i++)
            {
                _eagleContainer.transform.GetChild(i).GetComponent<PhysicsMovementEagle>().WillTurnAround();
            }                

            StartCoroutine(DoWeak());
        }

        private IEnumerator DoWeak()
        {
            yield return new WaitForSeconds(20f);
            _canSpawnEagle = true;
        }
    }
}

