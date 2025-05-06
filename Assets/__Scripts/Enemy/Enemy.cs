using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {

    [SerializeField] private float _speed;
    [SerializeField] private float _health;
    public TurretArea CurrentTurretArea;
    GameObject _player;
    Vector3 _directionToPlayer;
    Rigidbody _rb;
    void Awake() {
        _rb = GetComponent<Rigidbody>();
        var player = FindAnyObjectByType<PlayerMovementController>();
        if (player != null)
            _player = player.gameObject;
    }
    void Update() {
        _directionToPlayer = (_player.transform.position - transform.position).normalized;
    }

    void FixedUpdate() {
        _rb.velocity = _directionToPlayer * _speed;
    }

    void OnCollisionEnter(Collision collision) {
        IFarmProduct bullet = collision.gameObject.GetComponent<IFarmProduct>();
        if (bullet == null)
            return;

        if (bullet.State == GeneralState.CanAttack) {
            _health -= bullet.Damage;
            Destroy(collision.gameObject, .4f);
            if (_health <= 0){
                if(CurrentTurretArea != null)
                    CurrentTurretArea.MainTurret.Target = null;
                Destroy(gameObject);

            }
        }


    }



}
