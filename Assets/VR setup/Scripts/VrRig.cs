using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BigBoi.VR
{
    public class VrRig : MonoBehaviour
    {

        [SerializeField]
        private Transform leftController, rightController, headset, playArea;
        public Transform LeftController => leftController;
        public Transform RightController => rightController;
        public Transform Headset => headset;
        public Transform PlayArea => playArea;

        private VrController left, right;

        #region instance
        public static VrRig instance = null;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
                return;
            }
        }
        #endregion

        void Start()
        {
            //Validate everything
            ValidateComponent(leftController);
            ValidateComponent(rightController);
            ValidateComponent(headset);
            ValidateComponent(playArea);

            //get the controller components
            left = leftController.GetComponent<VrController>();
            right = rightController.GetComponent<VrController>();

            //Initialise the controllers
            left.Initialise();
            right.Initialise();
        }

        private void OnValidate()
        {
            //make sure you can't connect non-controllers to the controller references
            if (leftController != null && leftController.GetComponent<VrController>() == null)
            {
                leftController = null;
                Debug.LogWarning("The object you are trying to set to the left controller does not have a VrController component on it.");
            }
            if (rightController != null && rightController.GetComponent<VrController>() == null)
            {
                rightController = null;
                Debug.LogWarning("The object you are trying to set to the right controller does not have a VrController component on it.");
            }


        }

        /// <summary>
        /// Check that we have references to controllers, headset etc.
        /// If null, logs error then quit.
        /// </summary>
        private void ValidateComponent<T>(T _component) where T : Component
        {
            if (_component == null)
            {
                Debug.LogError($"Component {nameof(_component)} is null. This must be set.");
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
                Application.Quit();
            }
        }
    }
}