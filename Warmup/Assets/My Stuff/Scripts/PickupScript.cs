using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
    public class PickupScript : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField]
        public int index = default(int);
        [SerializeField]
        List<GameObject> listOfModels;
        [SerializeField]
        FloatReference _rotationSpeed = default(FloatReference);

        void Start()
        {
            Instantiate(listOfModels[index],transform).SetActive(true);
        }
        private void Update()
        {
            float rotSpeed = _rotationSpeed.Value * Time.deltaTime;
            transform.RotateAroundLocal(new Vector3(0, 1, 0), rotSpeed);
        }

        public string returnPickup()
        {
            return listOfModels[index].name;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                if (other.GetType() == typeof(CapsuleCollider))
                {
                    other.gameObject.GetComponent<PlayerEquipment>().setPickupStatus(listOfModels[index].name);
                    Destroy(gameObject);
                }
            }
        }
        public void setToHPDrop()
        {
            index = 2;
        }
    }
}