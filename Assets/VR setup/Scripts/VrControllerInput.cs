using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

namespace BigBoi.VR
{
    public class VrControllerInput : MonoBehaviour
    {
        private VrController controller;
        public VrController Controller => controller;

        #region Steam Input Actions
        [Header("Steam Actions")]
        [SerializeField] private SteamVR_Action_Vector2 touchpadAxis;
        [SerializeField] private SteamVR_Action_Boolean pointer, teleport, interact, grab;
        #endregion

        #region Unity Input Events
        [Header("Unity Events")]
        [SerializeField] private VrInputEvent onPointerPressed = new VrInputEvent();
        [SerializeField] private VrInputEvent onPointerReleased = new VrInputEvent();
        [SerializeField] private VrInputEvent onTeleportPressed = new VrInputEvent();
        [SerializeField] private VrInputEvent onTeleportReleased = new VrInputEvent();
        [SerializeField] private VrInputEvent onInteractPressed = new VrInputEvent();
        [SerializeField] private VrInputEvent onInteractReleased = new VrInputEvent();
        [SerializeField] private VrInputEvent onGrabPressed = new VrInputEvent();
        [SerializeField] private VrInputEvent onGrabReleased = new VrInputEvent();
        [SerializeField] private VrInputEvent onTouchpadAxisChanged = new VrInputEvent();

        public VrInputEvent OnPointerPressed => onPointerPressed;
        public VrInputEvent OnPointerReleased => onPointerReleased;
        public VrInputEvent OnTeleportPressed => onTeleportPressed;
        public VrInputEvent OnTeleportReleased => onTeleportReleased;
        public VrInputEvent OnInteractPressed => onInteractPressed;
        public VrInputEvent OnInteractReleased => onInteractReleased;
        public VrInputEvent OnGrabPressed => onGrabPressed;
        public VrInputEvent OnGrabReleased => onGrabReleased;
        public VrInputEvent OnTouchpadAxisChanged => onTouchpadAxisChanged;
        #endregion

        #region Steam VR Input Callbacks
        private void OnPointerDown(SteamVR_Action_Boolean _action, SteamVR_Input_Sources _source) => onPointerPressed.Invoke(GenerateArgs());
        private void OnPointerUp(SteamVR_Action_Boolean _action, SteamVR_Input_Sources _source) => onPointerReleased.Invoke(GenerateArgs());
        private void OnTeleportDown(SteamVR_Action_Boolean _action, SteamVR_Input_Sources _source) => onTeleportPressed.Invoke(GenerateArgs());
        private void OnTeleportUp(SteamVR_Action_Boolean _action, SteamVR_Input_Sources _source) => onTeleportReleased.Invoke(GenerateArgs());
        private void OnInteractDown(SteamVR_Action_Boolean _action, SteamVR_Input_Sources _source) => onInteractPressed.Invoke(GenerateArgs());
        private void OnInteractUp(SteamVR_Action_Boolean _action, SteamVR_Input_Sources _source) => onInteractReleased.Invoke(GenerateArgs());
        private void OnGrabDown(SteamVR_Action_Boolean _action, SteamVR_Input_Sources _source) => onGrabPressed.Invoke(GenerateArgs());
        private void OnGrabUp(SteamVR_Action_Boolean _action, SteamVR_Input_Sources _source) => onGrabReleased.Invoke(GenerateArgs());
        private void OnTouchpadChanged(SteamVR_Action_Vector2 _action, SteamVR_Input_Sources _source, Vector2 _axis, Vector2 _delta) => onTouchpadAxisChanged.Invoke(GenerateArgs());
        #endregion

        public void Initialise(VrController _controller)
        {
            controller = _controller;

            SteamVR_Input_Sources inputSource = controller.InputSource;

            #region delegate
            pointer.AddOnStateDownListener(OnPointerDown, inputSource);
            pointer.AddOnStateUpListener(OnPointerUp, inputSource);
            teleport.AddOnStateDownListener(OnTeleportDown, inputSource);
            teleport.AddOnStateUpListener(OnTeleportUp, inputSource);
            interact.AddOnStateDownListener(OnInteractDown, inputSource);
            interact.AddOnStateUpListener(OnInteractUp, inputSource);
            grab.AddOnStateDownListener(OnGrabDown, inputSource);
            grab.AddOnStateUpListener(OnGrabUp, inputSource);
            touchpadAxis.AddOnChangeListener(OnTouchpadChanged, inputSource);
            #endregion
        }

        private InputEventArgs GenerateArgs() => new InputEventArgs(controller, controller.InputSource, touchpadAxis.axis);
    }
}