using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedMachine : MonoBehaviour,IInteractable
{   

    [SerializeField] private ToolType _toolType;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private float _spawnRate;
    public ToolType ToolType { get => _toolType ; set => _toolType = value ; }
    public GeneralState State { get ; set ; }
    public IPlaceable PlacedObject { get; set; }

    float lastSpawnTime;
    Rigidbody _rb;

    void Awake() {
        _rb = GetComponent<Rigidbody>();
    }
    public GameObject Carry() {
        _rb.isKinematic = true;
        return this.gameObject;
    }

    public void Interact() {
        
        if(Time.time > lastSpawnTime + _spawnRate ){
            Instantiate(_prefab, _spawnPosition.position,Quaternion.identity);
            lastSpawnTime = Time.time;
        }
        
    }

    public GameObject ObjectReferance() {
        return this.gameObject;
    }

    
}
