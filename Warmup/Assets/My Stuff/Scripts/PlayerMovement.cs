using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        private FloatReference _moveSpeed = default(FloatReference);
        [SerializeField]
        private FloatReference _jumpForce = default(FloatReference);
        [SerializeField]
        private FloatReference _mouseSens = default(FloatReference);
        [SerializeField]
        private GameEvent _shootEvent = default(GameEvent);
        [SerializeField]
        private Vector3Reference _playerPosition = default(Vector3Reference);
        [SerializeField]
        private BoolReference _playerHasGun = default(BoolReference);

        private bool onGround = true;
        private Rigidbody rig;
        private Vector3 jump_dir;
        private bool canJump = true;

        private void Start()
        {
            rig = this.GetComponent<Rigidbody>();
            jump_dir = (Vector3.up - Vector3.forward).normalized;
        }

        private void FixedUpdate()
        {
            //check for on ground
            RaycastHit hit;
            onGround = Physics.SphereCast(transform.position, 0.05f, Vector3.down, out hit, 1.01f) ? true : false;
        }

        // Update is called once per frame
        void Update()
        {
            _playerPosition.Value = transform.position;
            if (Input.GetKey(KeyCode.W))
                transform.position += transform.forward * _moveSpeed.Value;
            if (Input.GetKey(KeyCode.S))
                transform.position -= transform.forward * _moveSpeed.Value;
            if (Input.GetKey(KeyCode.A))
                transform.position -= transform.right * _moveSpeed.Value;
            if (Input.GetKey(KeyCode.D))
                transform.position += transform.right * _moveSpeed.Value;

            float h = Input.GetAxis("Mouse X") * _mouseSens.Value;
            transform.Rotate(0, h, 0);
            if (onGround && canJump)
                if (Input.GetKey(KeyCode.Space))
                {
                    rig.AddForce(Vector3.up * _jumpForce.Value);
                }
                canJump = false;
            if (canJump == false && onGround == true)
                canJump = true;
            if (Input.GetButtonDown("Fire1") && _playerHasGun.Value)
            {
                _shootEvent.Raise();
            }
        }
    }
}
