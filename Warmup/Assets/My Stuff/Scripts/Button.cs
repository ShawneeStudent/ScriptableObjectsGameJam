using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ScriptableObjectArchitecture
{
    public class Button : MonoBehaviour
    {
        [SerializeField]
        GameEvent _buttonPressed = default(GameEvent); 
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                transform.GetComponent<AudioSource>().Play();
                _buttonPressed.Raise();
            }
        }
    }
}
