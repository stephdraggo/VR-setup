using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BigBoi.VR
{
    [RequireComponent(typeof(Pointer))]
    public class Teleporter : MonoBehaviour
    {
        [SerializeField, HideInInspector] private Pointer pointer;


        private void OnValidate()
        {
            pointer = gameObject.GetComponent<Pointer>();
        }

        void Start()
        {
            if (pointer == null)
                pointer=gameObject.GetComponent<Pointer>();

            pointer.controller.Input.OnTeleportPressed.AddListener(_args =>
            {
                if (pointer.endPoint != Vector3.zero)
                {
                    VrRig.instance.PlayArea.position = pointer.endPoint;
                }
            });
        }
    }
}