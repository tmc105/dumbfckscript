using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{

    #region Variables
    public Camera cam;
    public NavMeshAgent agent;
    public Interactable focus;
    private List<RaycastResult> results = new List<RaycastResult>();
    AbilityController abilityController;
    private Animator animator;
    public bool canMove = true;

    #endregion

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        abilityController = GetComponent<AbilityController>();
    }

    private void Start()
    {

    }

    void FixedUpdate()
    {
        LeftClick();
    }

    void LeftClick()
    {

        if (!canMove)
        {
            return;
        }
        // Check if the current selected object is a UI element
        // if (EventSystem.current.IsPointerOverGameObject() && !IsPointerOverIgnoredUI())
        // {
        //     return;
        // }


        if (Input.GetMouseButton(0))
        {

            CheckShiftLeftClickInput();


            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Instantiate(clickEffect, hit.point, Quaternion.identity);

                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    SetFocus(interactable);
                    agent.SetDestination(interactable.transform.position);
                    // if (interactable.tag == "Enemy")
                    // {
                    //     float distance = Vector3.Distance(transform.position, interactable.transform.position);
                    //     if (distance <= abilityController.shiftLeftClick.distanceForward)
                    //         abilityController.TriggerCastSkill(abilityController.shiftLeftClick);
                    // }


                }
                else
                {
                    RemoveFocus();
                    agent.SetDestination(hit.point);

                }
            }


        }
        bool isRunning = agent.remainingDistance > agent.stoppingDistance;
        animator.SetBool("isRunning", isRunning);
    }



    private bool IsPointerOverIgnoredUI()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);

        foreach (RaycastResult result in raycastResults)
        {
            if (result.gameObject.layer == LayerMask.NameToLayer("IgnoreRaycastUI"))
            {
                return true;
            }
        }

        return false;
    }

    private void CheckShiftLeftClickInput()
    {
        if ((Input.GetKeyDown(KeyCode.LeftShift) && Input.GetMouseButton(0)) ||
            (Input.GetKey(KeyCode.LeftShift) && Input.GetMouseButton(0)) ||
            (Input.GetMouseButton(0) && Input.GetKeyDown(KeyCode.LeftShift)))
        {
            abilityController.FaceMousePosition();
            abilityController.TriggerCastSkill(abilityController.shiftLeftClick);
        }
    }



    public void DisableMovement()
    {
        agent.SetDestination(transform.position);
        agent.isStopped = true;
        canMove = false;
    }

    public void EnableMovement()
    {
        canMove = true;
        agent.isStopped = false;
    }


    void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
                focus.OnDefocused();
            focus = newFocus;
            newFocus.OnFocused(transform);
        }
    }

    void RemoveFocus()
    {
        focus = null;
    }


}

