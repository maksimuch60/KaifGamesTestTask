using System;
using System.Collections;
using Cube;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Platform
{
    public class CubeSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _cubePrefab;
        [SerializeField] private Transform _upperBoundPoint;
        [SerializeField] private Transform _lowerBoundPoint;
        [SerializeField] private int _cubesOnScene = 3;

        private int _currentCubesAmount;

        private void Awake()
        {
            for (int i = 0; i < _cubesOnScene; i++)
            {
                Spawn();
            }
        }

        private void Spawn()
        {
            GameObject instance = Instantiate(_cubePrefab, GetRandomPoint(), Quaternion.identity);
            instance.GetComponent<CubeMovement>().Construct(_upperBoundPoint, _lowerBoundPoint);
        }

        private Vector3 GetRandomPoint()
        {
            Vector3 lowerPosition = _lowerBoundPoint.position;
            Vector3 upperPosition = _upperBoundPoint.position;
            float x = Random.Range(lowerPosition.x, upperPosition.x);
            float z = Random.Range(lowerPosition.z, upperPosition.z);

            return new Vector3(x, 0.5f, z);
        }
    }
}