﻿using UnityEngine;
using UnityEngine.UI;

namespace HuntTheMonster
{
    public class UICrosshairContainerBehavior : MonoBehaviour
    {
        [SerializeField] private Camera playerCamera;
        [SerializeField] private Image crosshairImage;

        private InteractableBehavior _interactable;

        private bool IsLookingAtInteractable => _interactable != null;
        private bool ReleasedUseKey => Input.GetKeyUp(KeyCode.E);

        private void Update()
        {
            CheckIsLookingAtInteractable();
            CheckForInteraction();
        }

        private void CheckIsLookingAtInteractable()
        {
            var ray = playerCamera.ScreenPointToRay(crosshairImage.transform.position);

            if (Physics.Raycast(ray, out var hit))
            {
                _interactable = hit.collider.gameObject.GetComponent<InteractableBehavior>();
                if (_interactable != null)
                {
                    SetInteractableCrosshair();
                }
                else
                {
                    SetNoInteractableCrosshair();
                }
            }
            else
            {
                SetNoInteractableCrosshair();
            }
        }

        private void CheckForInteraction()
        {
            if (IsLookingAtInteractable && ReleasedUseKey)
            {
                _interactable.Interact();
            }
        }
        
        private void SetInteractableCrosshair()
        {
            crosshairImage.color = Color.green;
        }

        private void SetNoInteractableCrosshair()
        {
            crosshairImage.color = Color.white;
        }
    }
}