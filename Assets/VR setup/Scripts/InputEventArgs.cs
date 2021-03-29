using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR;

namespace BigBoi.VR
{
    [Serializable]
    public class VrInputEvent : UnityEvent<InputEventArgs> { }

    [Serializable]
    public class InputEventArgs
    {
        /// <summary>
        /// Controller firing this event.
        /// </summary>
        public VrController controller;

        /// <summary>
        /// Input source the event is coming from.
        /// </summary>
        public SteamVR_Input_Sources source;

        /// <summary>
        /// Position on touchpad being touched.
        /// </summary>
        public Vector2 touchpadAxis;

        /// <summary>
        /// Contruct this pls.
        /// </summary>
        public InputEventArgs(VrController _controller, SteamVR_Input_Sources _source, Vector2 _touchpadAxis)
        {
            controller = _controller;
            source = _source;
            touchpadAxis = _touchpadAxis;
        }
    }
}