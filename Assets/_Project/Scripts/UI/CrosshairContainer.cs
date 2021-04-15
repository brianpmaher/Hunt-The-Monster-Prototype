﻿using HuntTheMonster.Environment;
using UnityEngine;
using UnityEngine.UI;

namespace HuntTheMonster.UI
{
    public class CrosshairContainer : MonoBehaviour
    {
        [SerializeField] public Camera playerCamera;
        [SerializeField] private Image crosshairImage;

        private Interactable _interactable;
        private Markable _markable;

        private bool IsLookingAtInteractable => _interactable != null;
        private bool ReleasedUseKey => Input.GetKeyUp(KeyCode.E);
        private bool IsLookingAtMarkable => _markable != null;
        private bool ReleasedMarkKey => Input.GetKeyUp(KeyCode.F);

        private void Update()
        {
            CheckIsLookingAtInteractable();
            CheckForInteraction();
            CheckIsLookingAtMarkable();
            CheckForMark();
        }

        private void CheckIsLookingAtInteractable()
        {
            var ray = playerCamera.ScreenPointToRay(crosshairImage.transform.position);

            if (Physics.Raycast(ray, out var hit))
            {
                _interactable = hit.collider.gameObject.GetComponent<Interactable>();
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

        private void CheckIsLookingAtMarkable()
        {
            var ray = playerCamera.ScreenPointToRay(crosshairImage.transform.position);

            if (Physics.Raycast(ray, out var hit))
            {
                _markable = hit.collider.gameObject.GetComponent<Markable>();
            }
        }

        private void CheckForMark()
        {
            if (IsLookingAtMarkable && ReleasedMarkKey)
            {
                _markable.Mark();
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