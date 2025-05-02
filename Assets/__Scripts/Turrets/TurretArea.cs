using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretArea : MonoBehaviour {
    [SerializeField] private Turret _mainTurret;

    Enemy currentEnemy;

    void Update() {
        transform.position = _mainTurret.transform.position;
    }
    void OnTriggerEnter(Collider other) {
        Enemy EnemyTarget = other.GetComponent<Enemy>();
        if (EnemyTarget != null && currentEnemy == null) {
            currentEnemy = EnemyTarget;
            _mainTurret.Target = EnemyTarget;
        }
    }

    void OnTriggerExit(Collider other) {
        Enemy EnemyTarget = other.GetComponent<Enemy>();
        if (EnemyTarget != null && currentEnemy == EnemyTarget) {
            currentEnemy = null;
            _mainTurret.Target = null;
        }

    }
}
