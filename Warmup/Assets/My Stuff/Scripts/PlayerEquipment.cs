using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjectArchitecture
{

    public class PlayerEquipment : MonoBehaviour
    {
        [SerializeField]
        FloatReference _playerDamage = default(FloatReference);
        [SerializeField]
        FloatReference playerHealth = default(FloatReference);
        [SerializeField]
        FloatReference _maxPlayerHealth = default(FloatReference);
        [SerializeField]
        FloatReference _healAmount = default(FloatReference);
        [SerializeField]
        BoolReference _Gun = default(BoolReference);
        [SerializeField]
        BoolReference _Shotgun = default(BoolReference);
        [SerializeField]
        BoolReference _KeyBlue = default(BoolReference);
        [SerializeField]
        BoolReference _KeyGreen = default(BoolReference);
        [SerializeField]
        BoolReference _KeyRed = default(BoolReference);
        
        bool playerHasGun = false;
        bool playerHasShotGun = false;
        string activeGun = "Gun";
        GameObject gun = null;
        GameObject Shotgun = null;
        Vector3 respawnPoint = default(Vector3);

        // Start is called before the first frame update
        void Start()
        {
            gun = transform.GetChild(1).gameObject;
            Shotgun = transform.GetChild(2).gameObject;
            _Gun.Value = false;
            _Shotgun.Value = false;
            _KeyBlue.Value = false;
            _KeyGreen.Value = false;
            _KeyRed.Value = false;
        }

        public void setSpawn(Vector3 point)
        {
            respawnPoint = point;
        }

        // Update is called once per frame
        void Update()
        {
            if (playerHealth.Value <= 0 && respawnPoint != null)
            {
                playerHealth.Value = _maxPlayerHealth.Value;
                transform.position = respawnPoint;
            }
            float inpt = Input.GetAxisRaw("Mouse ScrollWheel");
            if (inpt != 0)
            {
                if (playerHasGun && playerHasShotGun)
                {
                    if (activeGun == "Gun")
                    {
                        Shotgun.SetActive(true);
                        gun.SetActive(false);
                        activeGun = "Shotgun";
                    }
                    else 
                    {
                        gun.SetActive(true);
                        Shotgun.SetActive(false);
                        activeGun = "Gun";
                    }
                }

            }
        }

        public void setPickupStatus(string pickup)
        {
            if (pickup == "Gun 1")
            {
                _Gun.Value = true;
                playerHasGun = true;
                gun.SetActive(true);
            }
            if (pickup == "Shotgun 1")
            {
                _Shotgun.Value = true;
                playerHasShotGun = true;
            }
            if (pickup == "Health")
            {
                if (playerHealth.Value < _maxPlayerHealth.Value)
                    playerHealth.Value += _healAmount.Value;
            }
            if (pickup == "BlueKey")
                _KeyBlue.Value = true;
            if (pickup == "GreenKey")
                _KeyGreen.Value = true;
            if (pickup == "RedKey")
                _KeyRed.Value = true;
        }

    }
}