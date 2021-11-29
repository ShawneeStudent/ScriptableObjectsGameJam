using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
    public class AimBoxTargeting : MonoBehaviour
    {
        [SerializeField]
        private GameObject _componentParent = default(GameObject);
        [SerializeField]
        private FloatReference _damageAmount = default(FloatReference);

        private float closestDistanceSoFar = Mathf.Infinity;
        private GameObject closestEnemy = default(GameObject);

        private void onFirePress()
        {
            RaycastHit hit;
            if (Physics.SphereCast(_componentParent.transform.position, 0.1f, _componentParent.transform.forward, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Action")))
            {
                Debug.Log(hit.transform.name);
                if (hit.transform.gameObject.tag == "Enemy")
                    hit.transform.SendMessage("beenHit", _damageAmount.Value);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.tag == "Enemy")
            {
                float disBetween = Vector3.Distance(other.transform.position, _componentParent.transform.position);
                if (disBetween < closestDistanceSoFar)
                {
                    closestDistanceSoFar = disBetween;
                    closestEnemy = other.gameObject;
                }
            }
            if (closestEnemy == null)
            {
                closestEnemy = default(GameObject);
                closestDistanceSoFar = Mathf.Infinity;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Enemy")
            { 
                if(other.gameObject == closestEnemy)
                {
                    closestEnemy = default(GameObject);
                    closestDistanceSoFar = Mathf.Infinity;
                }
            }    
        }
    }
}
