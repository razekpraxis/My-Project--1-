using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    

    [SerializeField]
    private GameObject _itemToSpawn;
    [SerializeField]
    private Transform _spawnPoint;

    private void Start()
    {
        Vector3 spawnPosition = _spawnPoint.position;
        Instantiate(_itemToSpawn, spawnPosition, Quaternion.identity);
        Destroy(gameObject);
    }
}
