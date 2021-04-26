using Serializable = System.SerializableAttribute;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



namespace BigBoi.VR.Interaction
{
    [Serializable]
    public class InteractionEvent : UnityEvent<InteractEventArgs> { }

    /// <summary>
    /// Heckenning
    /// </summary>
    [Serializable]
    public class InteractEventArgs
    {
        /// <summary>
        /// the controller that started this whole mess
        /// </summary>
        public VrController controller;
        /// <summary>
        /// the rigidbody we're interacting with
        /// </summary>
        public Rigidbody rigidbody;
        /// <summary>
        /// the collider we're interacting with
        /// </summary>
        public Collider collider;

        public InteractEventArgs(VrController _controller, Rigidbody _rigidbody, Collider _collider)
        {
            controller = _controller;
            rigidbody = _rigidbody;
            collider = _collider;
        }
    }
}