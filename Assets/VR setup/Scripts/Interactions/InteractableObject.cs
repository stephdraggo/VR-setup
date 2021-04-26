using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

using SteamVRInputSource = Valve.VR.SteamVR_Input_Sources; //bc im not a PLEB


namespace BigBoi.VR.Interaction
{
    [RequireComponent(typeof(Rigidbody))]
    public class InteractableObject : MonoBehaviour
    {
        public Rigidbody Rigidbody => rigidbody;
        public Collider Collider => collider;
        public Transform AttachPoint => attachPoint;

        [SerializeField] private bool isGrabbable = true;
        [SerializeField] private bool isTouchable = false;
        [SerializeField] private bool isUsable = false;
        [SerializeField] private SteamVRInputSource allowedSource = SteamVRInputSource.Any;

        [Space]

        [SerializeField, Tooltip("The part of the object we want to grab, if not set just grab the origin.")]
        private Transform attachPoint;

        [Space]

        public InteractionEvent onGrabbed = new InteractionEvent();
        public InteractionEvent onReleased = new InteractionEvent();
        public InteractionEvent onTouched = new InteractionEvent();
        public InteractionEvent onStopTouched = new InteractionEvent();
        public InteractionEvent onUsed = new InteractionEvent();
        public InteractionEvent onStopUsed = new InteractionEvent();

        private new Collider collider;
        private new Rigidbody rigidbody;

        void Start()
        {
            collider = gameObject.GetComponent<Collider>();
            if (collider == null)
            {
                collider = gameObject.AddComponent<BoxCollider>();
                Debug.LogError($"Object {name} does not have a collider, adding BoxCollider", gameObject);
            }
            rigidbody = gameObject.GetComponent<Rigidbody>();
        }

        private InteractEventArgs GenerateArgs(VrController _controller) => new InteractEventArgs(_controller, rigidbody, collider);

        #region OnObjectActioned methods
        public void OnObjectGrabbed(VrController _controller)
        {
            if (isGrabbable && (_controller.InputSource == allowedSource || allowedSource == SteamVRInputSource.Any))
            {
                onGrabbed.Invoke(GenerateArgs(_controller));
            }
        }

        public void OnObjectReleased(VrController _controller)
        {
            if (isGrabbable && (_controller.InputSource == allowedSource || allowedSource == SteamVRInputSource.Any))
            {
                onReleased.Invoke(GenerateArgs(_controller));
            }
        }

        public void OnObjectTouched(VrController _controller)
        {
            if (isTouchable && (_controller.InputSource == allowedSource || allowedSource == SteamVRInputSource.Any))
            {
                onTouched.Invoke(GenerateArgs(_controller));
            }
        }

        public void OnObjectStopTouched(VrController _controller)
        {
            if (isTouchable && (_controller.InputSource == allowedSource || allowedSource == SteamVRInputSource.Any))
            {
                onStopTouched.Invoke(GenerateArgs(_controller));
            }
        }

        public void OnObjectUsed(VrController _controller)
        {
            if (isUsable && (_controller.InputSource == allowedSource || allowedSource == SteamVRInputSource.Any))
            {
                onUsed.Invoke(GenerateArgs(_controller));
            }
        }

        public void OnObjectStopUsed(VrController _controller)
        {
            if (isUsable && (_controller.InputSource == allowedSource || allowedSource == SteamVRInputSource.Any))
            {
                onStopUsed.Invoke(GenerateArgs(_controller));
            }
        }
        #endregion

    }
}