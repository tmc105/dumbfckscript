using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityController : MonoBehaviour
{
    public PlayerSkill rightClick, shiftLeftClick, key1, key2, key3, key4;
    private PlayerController player;
    private PlayerStats playerStats;
    [SerializeField] GameObject leftHand, rightHand;
    private Animator anim;

    public bool isCasting;

    private void Start()
    {
        anim = GetComponent<Animator>();
        player = GetComponent<PlayerController>();
        playerStats = GetComponent<PlayerStats>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && !isCasting)
        {
            StartCoroutine(CastSkill(rightClick));
        }


        if (Input.GetKeyDown(KeyCode.Alpha1) && !isCasting)
        {
            StartCoroutine(CastSkill(key1));
        }
    }

    public bool CanCastSkill(PlayerSkill keyPressed)
    {
        return playerStats.rage >= keyPressed.resourceCost && !isCasting;
    }

    public void TriggerCastSkill(PlayerSkill keyPressed)
    {
        StartCoroutine(CastSkill(keyPressed));
    }
    IEnumerator CastSkill(PlayerSkill keyPressed)
    {
        if (CanCastSkill(keyPressed))
        {
            player.DisableMovement();
            isCasting = true;
            anim.SetBool("isRunning", false);

            FaceMousePosition();
            playerStats.rage -= keyPressed.resourceCost;
            anim.SetTrigger(keyPressed.animationTrigger);
            yield return new WaitForSeconds(keyPressed.animationDuration);
            isCasting = false;
            player.EnableMovement();
        }

    }



    public void FaceMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 targetDirection = hit.point - transform.position;
            targetDirection.y = 0; // Keep the player's Y-axis rotation at 0 to prevent tilting.
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = targetRotation;
        }
    }

    public void ParticleEffectRightClick()
    {
        Instantiate(rightClick.abilityPrefab, transform.position + transform.forward * rightClick.distanceForward, Quaternion.identity);
    }

    public void ParticleEffectBattleShout()
    {
        Instantiate(key1.abilityPrefab, leftHand.transform.position, Quaternion.identity, leftHand.transform);
        Instantiate(key1.abilityPrefab, rightHand.transform.position, Quaternion.identity, rightHand.transform);
    }

    public void ParticleEffectShiftLeftClick()
    {
        Instantiate(shiftLeftClick.abilityPrefab, transform.position + transform.forward * shiftLeftClick.distanceForward + Vector3.up * 1, Quaternion.identity);
    }
}

