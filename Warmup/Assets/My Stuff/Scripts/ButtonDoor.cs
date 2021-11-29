using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ScriptableObjectArchitecture
{
    public class ButtonDoor : MonoBehaviour
    {
        private Quaternion _openPosition = default(Quaternion);
        private Quaternion _closePosition = default(Quaternion);
        private BoolReference _openDoorBool = default(BoolReference);
        private BoolReference _closeDoorBool = default(BoolReference);

        public void openDoor()
        {
            transform.rotation = _openPosition;
        }

        void closeDoor()
        {
            transform.rotation = _closePosition;
        }
    }
}