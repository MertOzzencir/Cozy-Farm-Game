
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public abstract class Turret : MonoBehaviour, IPlaceable, IPickable {

    [SerializeField] public Transform ShootPosition;
    [SerializeField] private float _shootCooldown;
    [SerializeField] private float _shootPower;


    public Enemy Target;
    public List<IFarmProduct> Bullets = new List<IFarmProduct>();
    public bool CanPlaceOnIt { get; set; }
    public GeneralState State { get; set; }
    public IPlaceable PlacedObject { get; set; }

    Rigidbody _rb;
    float _timer;

    void Awake() {
        _rb = GetComponent<Rigidbody>();
    }
    public GameObject GMReference() {
        return this.gameObject;
    }

    void Update() {
        if (Target != null && Bullets.Count != 0) {
            _timer += Time.deltaTime;
            Vector3 target = (Target.transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(target);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 0.2f);
            Shoot(_timer);
        }
    }

    private void Shoot(float timer) {
        if (timer > _shootCooldown) {
            IFarmProduct currentBullet = Bullets[Bullets.Count - 1];
            currentBullet.ObjectReferance().SetActive(true);
            currentBullet.ObjectReferance().transform.parent = null;
            currentBullet.ObjectReferance().GetComponent<Rigidbody>().AddForce(transform.forward * _shootPower, ForceMode.Impulse);
            _timer = 0;
            Bullets.Remove(Bullets[Bullets.Count - 1]);
        }

    }

    public void PlaceOnPlaceable(IPickable objectToPlace) {
        IFarmProduct bullet = objectToPlace.ObjectReferance().GetComponent<IFarmProduct>();
        if (bullet == null)
            return;
        GameObject currentBullet = objectToPlace.ObjectReferance();
        objectToPlace.State = GeneralState.None;
        Bullets.Add(bullet);
        currentBullet.transform.position = ShootPosition.position;
        currentBullet.transform.parent = ShootPosition;
        Transform bulletTransform = currentBullet.transform;
        Vector3 negativeUpTarget = ShootPosition.forward;
        Vector3 fakeForward = Vector3.Cross(negativeUpTarget, bulletTransform.right);
        currentBullet.transform.rotation = Quaternion.LookRotation(fakeForward, -negativeUpTarget);
        objectToPlace.State = GeneralState.CanAttack;
        currentBullet.SetActive(false);
    }



    public GameObject Carry() {
        _rb.isKinematic = true;
        return this.gameObject;
    }

    public GameObject ObjectReferance() {
        return this.gameObject;
    }
    public void OnEnable() {
        CanPlaceOnIt = true;
        PlacedObject = this;
        State = GeneralState.CanPick;
    }
}
