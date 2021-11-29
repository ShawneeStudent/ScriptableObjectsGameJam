using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ScriptableObjectArchitecture
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField]
        FloatReference _enemyMaxHealth = default(FloatReference);
        [SerializeField]
        FloatReference _enemyDamage = default(FloatReference);
        [SerializeField]
        Vector3Reference _playerPosition = default(Vector3Reference);
        [SerializeField]
        private float _detectionRadius = default(float);
        [SerializeField]
        private GameObject _castPostion = default(GameObject);
        [SerializeField]
        private float _rotationSpeed = default(float);
        [SerializeField]
        private float _walkSpeed = default(float);
        [SerializeField]
        private float _damageTimerMax = default(float);

        private float currentDamageTimer = 0;
        private float health = 0;
        private Vector3 lastKnowLocation = default(Vector3);
        private bool canSeePlayer = false;
        private GameObject muzzleFlash = default(GameObject);

        // Start is called before the first frame update
        void Start()
        {
            health = _enemyMaxHealth.Value;
            currentDamageTimer = _damageTimerMax;
            muzzleFlash = transform.GetChild(2).gameObject;
        }

        void beenHit(float dmgAmnt)
        {
            health -= dmgAmnt;
            StartCoroutine("showHit");
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (currentDamageTimer < _damageTimerMax)
                currentDamageTimer += Time.deltaTime;
            if (Vector3.Distance(_playerPosition.Value, transform.position) < _detectionRadius)
            {
                Vector3 playerDir = (_playerPosition.Value - _castPostion.transform.position).normalized;
                RaycastHit hit;
                if (Physics.SphereCast(_castPostion.transform.position, 0.1f, playerDir, out hit))
                {
                    if (hit.transform.gameObject.tag == "Player")
                    {
                        canSeePlayer = true;
                        float singleStep = _rotationSpeed * Time.deltaTime;
                        Vector3 newDir = Vector3.RotateTowards(transform.forward, playerDir, singleStep, 0.0f);
                        transform.rotation = Quaternion.LookRotation(newDir);
                        lastKnowLocation = hit.transform.position;
                        if (!(Vector3.Distance(transform.position, lastKnowLocation) < 0.001f))
                        {
                            float step = _walkSpeed * Time.deltaTime;
                            transform.position = Vector3.MoveTowards(transform.position, lastKnowLocation, step);
                        }
                        if (muzzleFlash != null && currentDamageTimer >= _damageTimerMax && canSeePlayer)
                        {
                            UnitHealth targetHealth = hit.transform.gameObject.GetComponent<UnitHealth>();
                            currentDamageTimer = 0;
                            DealDamage(targetHealth);
                            StartCoroutine("showFire");
                        }
                    }
                    else
                        canSeePlayer = false;

                }
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                UnitHealth targetHealth = other.gameObject.GetComponent<UnitHealth>();
                if (targetHealth != null && currentDamageTimer >= _damageTimerMax && canSeePlayer)
                {
                    currentDamageTimer = 0;
                    DealDamage(targetHealth);
                }
            }

        }
        protected virtual void DealDamage(UnitHealth target)
        {
            target.Health.Value -= _enemyDamage.Value;
        }

        IEnumerator showHit()
        {
            transform.GetChild(1).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            transform.GetChild(1).gameObject.SetActive(false);
        }
        IEnumerator showFire()
        {
            transform.GetChild(2).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            transform.GetChild(2).gameObject.SetActive(false);
        }

    }
}
