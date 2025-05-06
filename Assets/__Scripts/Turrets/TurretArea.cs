using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretArea : MonoBehaviour {
    public Turret MainTurret;

    Enemy _currentEnemy;

    void Update() {
        transform.position = MainTurret.transform.position;
    }
    void OnTriggerEnter(Collider other) {
        FindEnemy(other);
    }

    private void FindEnemy(Collider other) {
        Enemy EnemyTarget = other.GetComponent<Enemy>();
        if (EnemyTarget != null && _currentEnemy == null) {
            EnemyTarget.CurrentTurretArea = this;
            _currentEnemy = EnemyTarget;
            MainTurret.Target = EnemyTarget;
        }
    }

    private void OnTriggerStay(Collider other) {
        FindEnemy(other);
    }

    void OnTriggerExit(Collider other) {
        Enemy EnemyTarget = other.GetComponent<Enemy>();
        if (EnemyTarget != null && _currentEnemy == EnemyTarget) {
            _currentEnemy = null;
            MainTurret.Target = null;
        }

    }
}
