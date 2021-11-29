using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
    public class AimBox : MonoBehaviour
    {
        [SerializeField]
        private FloatReference _boxWidth = default(FloatReference);
        [SerializeField]
        private FloatReference _boxLength = default(FloatReference);

        // Start is called before the first frame update
        void Update()
        {
            transform.localScale = new Vector3(_boxWidth.Value, 10, _boxLength.Value);
        }

    }
}
