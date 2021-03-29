using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

namespace BigBoi.VR
{
    [RequireComponent(typeof(SteamVR_Behaviour_Pose))]
    [RequireComponent(typeof(VrControllerInput))]
    public class VrController : MonoBehaviour
    {
        
        private VrControllerInput input;
        public VrControllerInput Input => input;

        private SteamVR_Behaviour_Pose pose;
        public SteamVR_Input_Sources InputSource => pose.inputSource;
        /// <summary>
        /// Movement speed of controller.
        /// </summary>
        public Vector3 Velocity => pose.GetVelocity();
        /// <summary>
        /// Rotational speed of controller.
        /// </summary>
        public Vector3 AngularVelocity => pose.GetAngularVelocity();

        public void Initialise()
        {
            pose = gameObject.GetComponent<SteamVR_Behaviour_Pose>();
            input = gameObject.GetComponent<VrControllerInput>();

            input.Initialise(this);
        }
    }
}