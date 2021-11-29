using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ScriptableObjectArchitecture
{
    public class RespawnPoint : MonoBehaviour
    {

        [SerializeField]
        Vector3Reference _spawnPointPosition = default(Vector3Reference);

        // Start is called before the first frame update
        void Start()
        {
            _spawnPointPosition.Value = transform.position;
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                other.gameObject.GetComponent<PlayerEquipment>().setSpawn(_spawnPointPosition.Value);

            }
        }
    }
}
