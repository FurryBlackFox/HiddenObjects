using System;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Ingame
{
    public class InputService : MonoBehaviour
    {
        [SerializeField] private InputServiceSettings inputSettings;

        private Camera _mainCamera;
        
        [Inject]
        private void Construct(Camera mainCamera)
        {
            _mainCamera = mainCamera;
        }
        
        private void Update()
        {
            if(Input.GetMouseButtonDown(0))
                ProcessInput(Input.mousePosition);    
        }

        private void ProcessInput(Vector3 screenPosition)
        {
            var isOverUI = EventSystem.current.IsPointerOverGameObject();
            if(isOverUI)
                return;
            
            var worldPos = _mainCamera.ScreenToWorldPoint(screenPosition);
            var result = Physics2D.Raycast(worldPos, Vector3.forward,
                inputSettings.RaycastDistance, inputSettings.SupportedLayers);
            
            if(!result)
                return;

            var clickableObject = result.transform.GetComponentInChildren<ClickableObject.ClickableObject>();
            
            if(clickableObject == null)
                return;
            
            clickableObject.OnClick();
        }
    }
}