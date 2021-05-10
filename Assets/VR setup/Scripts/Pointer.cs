using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BigBoi.VR
{
    public class Pointer : MonoBehaviour
    {
        private const float TracerWidth = 0.025f;

        public Vector3 endPoint { get; private set; } = Vector3.zero;
        public bool Active { get; private set; } = false;
        public VrController controller;

        private Transform cursor, tracer;
        private Renderer cursorRender, tracerRender;

        [SerializeField] private float cursorScaleFactor = 0.1f;
        [SerializeField] private Color invalid = Color.red, valid = Color.green;

        private void Start()
        {
            controller.Input.OnPointerPressed.AddListener(_args => { Activate(true); });
            controller.Input.OnPointerReleased.AddListener(_args => { Activate(false); });

            CreatePointer();
            Activate(false);
        }

        private void Activate(bool _active)
        {
            Active = _active;
            cursor.gameObject.SetActive(_active);
            tracer.gameObject.SetActive(_active);
        }

        private void Update()
        {
            if (Active)
            {
                bool didHit = Physics.Raycast(controller.transform.position, controller.transform.forward, out RaycastHit hit);
                endPoint = didHit ? hit.point : Vector3.zero;
                UpdateScalePos(hit, didHit);
                SetValid(didHit);
            }
        }

        private void CreatePointer()
        {
            GameObject tracerObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            GameObject cursorObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            tracer = tracerObj.transform;
            cursor = cursorObj.transform;

            tracer.SetParent(controller.transform);
            cursor.SetParent(controller.transform);

            tracerRender = tracer.GetComponent<Renderer>();
            cursorRender = cursor.GetComponent<Renderer>();

            SetValid(false);
        }

        public void SetValid(bool _valid)
        {
            cursorRender.material.color = _valid ? valid : invalid;
            tracerRender.material.color = _valid ? valid : invalid;
        }

        private void UpdateScalePos(RaycastHit _hit, bool _didHit)
        {
            if (_didHit)
            {
                CalculateDirAndDist(controller.transform.position, _hit.point,
                    out Vector3 dir, out float distance);

                Vector3 midPoint = Vector3.Lerp(controller.transform.position,
                    controller.transform.position + _hit.point, 0.5f);

                tracer.position = midPoint;
                tracer.localScale = new Vector3(TracerWidth, TracerWidth, distance);

                cursor.position = _hit.point;
                cursor.localScale = Vector3.zero * cursorScaleFactor;
            }
            else
            {
                //set cursor and tracer to arbitrary position
                CalculateDirAndDist(controller.transform.position,
                    controller.transform.forward * 100 + controller.transform.position,
                    out Vector3 dir, out float distance);

                Vector3 midPoint = Vector3.Lerp(controller.transform.position,
                    controller.transform.position + dir * distance, 0.5f);

                tracer.position = midPoint;
                tracer.localScale = new Vector3(TracerWidth, TracerWidth, distance);

                cursor.position = controller.transform.position + controller.transform.forward * 100;
                cursor.localScale = Vector3.zero * cursorScaleFactor;
            }
        }

        private void CalculateDirAndDist(Vector3 _start, Vector3 _end, out Vector3 _dir, out float _dist)
        {
            Vector3 heading = _end - _start;
            _dist = heading.magnitude;
            _dir = heading / _dist;
        }
    }
}