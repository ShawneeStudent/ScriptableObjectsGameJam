using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjectArchitecture 
{ 
    public class GunFiring : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
    
        }
    
        // Update is called once per frame
        void Update()
        {
    
        }
    
        IEnumerator showMuzzleFlash()
        {
            transform.GetChild(0).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            transform.GetChild(0).gameObject.SetActive(false);
        }
    
    
    }
}
