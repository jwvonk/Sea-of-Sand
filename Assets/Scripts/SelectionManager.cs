using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private string selectableTag = "Selectable";
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material defaultMaterial;

    private Transform fluidizableObj;
    private Transform _selection;
    private bool isSelected = false;

    private void Start()
    {
    }

    private void Update() {
        var mousePosition = Mouse.current.position.ReadValue();
        var ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)) {
            var selection = hit.transform;

            if (selection.CompareTag(selectableTag)) {
                if (Mouse.current.leftButton.wasPressedThisFrame) {
                    if (_selection != null) {
                        var lastSelectionRenderer = _selection.GetComponent<Renderer>();
                        if (lastSelectionRenderer != null && isSelected) {
                            lastSelectionRenderer.material = defaultMaterial;
                            isSelected = false;
                        }
                        else if (lastSelectionRenderer != null && !isSelected) {
                            lastSelectionRenderer.material = highlightMaterial;
                            isSelected = true;
                            fluidizableObj = selection;
                        }
                    }
                    _selection = selection;
                }
                // if (Keyboard.current.eKey.wasPressedThisFrame && isSelected) {
                //     var objrigidbody = selection.GetComponent<Rigidbody>();
                //     if (objrigidbody != null) {
                //         objrigidbody.useGravity = true;
                //     }
                // }
            }
        }
        // here
        if (Keyboard.current.eKey.wasPressedThisFrame && isSelected) {
            var objrigidbody = fluidizableObj.GetComponent<Rigidbody>();
            if (objrigidbody != null) {
                objrigidbody.useGravity = true;
            }
        }
    }
}
