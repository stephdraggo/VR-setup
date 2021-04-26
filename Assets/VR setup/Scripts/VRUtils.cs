using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


namespace BigBoi.VR
{
    public static class VRUtils
    {
        private static List<XRInputSubsystem> subSystems = new List<XRInputSubsystem>();

        public static void SetVREnabled(bool _enabled)
        {
            SubsystemManager.GetInstances(subSystems);
            //hecc
            foreach (XRInputSubsystem _subsystem in subSystems)
            {
                if (_enabled)
                {
                    _subsystem.Start();
                }
                else
                {
                    _subsystem.Stop();
                }
            }
        }

        public static bool IsVREnabled()
        {
            SubsystemManager.GetInstances(subSystems);

            foreach (XRInputSubsystem _subsystem in subSystems)
            {
                if (_subsystem.running)
                {
                    return true;
                }
            }
            return false;
        }
    }
}