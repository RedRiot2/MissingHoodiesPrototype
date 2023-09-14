using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickUp : MonoBehaviour
{
    [SerializeField]
    private Transform playerCameraTransform;

    [SerializeField]
    private Transform pickableLayerMask;

    [SerializeField]
    private GameObject pickUpUI;

    [SerializeField]
    private GameObject doorUI;

    [SerializeField]
    private TextMeshProUGUI doorTextUI;

    [SerializeField]
    private LayerMask LayerMask;

    [SerializeField]
    private LayerMask DoorMask;

    [SerializeField]
    [Min(1)]
    private float hitRange;

    [SerializeField]
    [Min(1)]
    private float unlockRange;

    [SerializeField]
    private Transform pickUpParent;

    [SerializeField]
    private GameObject inHandItem;

    [SerializeField]
    int Key = 0;



    [SerializeField]
    private InputActionReference interactionInput, dropInput, useInput;

    private RaycastHit hit;

    private RaycastHit unlock;

    private void Start()
    {
        interactionInput.action.performed += Interact;
        dropInput.action.performed += Drop;
        useInput.action.performed += Use;
    }

    private void Use(InputAction.CallbackContext obj)
    {



    }

    private void Drop(InputAction.CallbackContext obj)
    {
        
    }

    private void Interact(InputAction.CallbackContext obj)
    {
        if(hit.collider != null)
        {
            Debug.Log(hit.collider.name);
            if (hit.collider.GetComponent<Key>())
            {

                Debug.Log("It's a Key!");
                inHandItem = hit.collider.gameObject;
                inHandItem.transform.position = Vector3.zero;
                inHandItem.transform.rotation = Quaternion.identity;
                inHandItem.transform.SetParent(pickUpParent.transform, false);
                Key += 1;
                Destroy(hit.transform.gameObject);
                return;

            }
        }

        if(unlock.collider != null)
        {
            Debug.Log(unlock.collider.name);
            if (unlock.collider.GetComponent<Door>() && Key >= 1)
            {

                Debug.Log("You Win");

            }
            else if(unlock.collider.GetComponent<Door>() && Key <= 0)
            {

                Debug.Log("You need to find something to unlock the door");

            }
            else
            {
                Debug.Log("this isnt working");
            }
        }
    }

    private void Update()
    {
        
        if (hit.collider != null)
        {
            pickUpUI.SetActive(false);
            doorUI.SetActive(false);
        }

        if (inHandItem != null)
        {

            return;

        }

        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out hit, hitRange, LayerMask))
        {
            pickUpUI.SetActive(true);

        }

        if(Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out unlock, unlockRange, DoorMask))
        {

            return;

        }
    }

}
