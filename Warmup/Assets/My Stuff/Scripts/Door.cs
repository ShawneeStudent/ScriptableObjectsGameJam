using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ScriptableObjectArchitecture
{
    public class Door : MonoBehaviour
    {
        [SerializeField]
        BoolReference _requiredKeyBool = default(BoolReference);

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.gameObject.tag == "Player")
                if (_requiredKeyBool.Value)
                    Destroy(gameObject);
        }
    }

}
