using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour
{

    public GameObject AOEAimingTemplate;
    [Header("Controller")]
    BuffSystem buffSystem;
    ExperienceSystem experienceSystem;
    CharacterStats characterStats;
    CharacterCombatController combatController;
    SkillController skillController;
    SkillTree skillTree;
    IStunnable stunnable;
    HotkeyController hotkeyController;
    CanGrabController canGrabController;
    TargetingSystem targetingSystem;



    public Ability Ability1;



    [Header("Movement")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 720f;
    public float jumpForce = 1f;
    public LayerMask groundLayer;

    [Header("Camera")]
    public Transform cameraTarget;
    public float cameraDistance = 5f;
    public float cameraHeight = 2f;
    public float cameraRotationSpeed = 2f;


    private Rigidbody rb;
    private Animator animator;
    private Vector3 moveDirection;
    private bool isGrounded;
    private Transform mainCamera;
    private float cameraRotationY;

    Coroutine aimingCorotine;


    private void Start()
    {
        canGrabController = GetComponent<CanGrabController>();
        combatController = GetComponent<CharacterCombatController>();
        characterStats = GetComponent<CharacterStats>();
        skillController = GetComponent<SkillController>();
        targetingSystem = GetComponent<TargetingSystem>();

        //EDITOR CODE
        skillController.skillTree.resetAllNodes();

        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        mainCamera = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;



        skillController.OnSkillUnlocked += UpdateToSkillEvents;
        stunnable = GetComponent<IStunnable>();
    }


    private void Update()
    {
        HandleMovement();
        HandleJump();
        HandleCamera();
        HandleActions();
    }
    private void UpdateToSkillEvents(SkillNode node)
    {
        characterStats.UpdateSubStats();
    }
    GameObject target;
    public void HandleActions()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {

            target = targetingSystem.GetTarget();
            if (target == null) { return; }

            if (target.GetComponent<IInteractable>() != null)
            {
                if (Vector3.Distance(target.transform.position, transform.position) < 10f)
                {
                    target.GetComponent<IInteractable>().Interact(transform);
                }
            }
        }
 
    }

    private void HandleMovement()
    {
        if (stunnable != null && stunnable.isStunned())
        {
            return;
        }
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        moveDirection = mainCamera.forward * vertical + mainCamera.right * horizontal;
        moveDirection.y = 0f;
        moveDirection.Normalize();

        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        }

        animator.SetFloat("Speed", moveDirection.magnitude);
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.deltaTime);
    }

    private void HandleJump()
    {
        if (stunnable != null && stunnable.isStunned())
        {
            return;
        }
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.4f, groundLayer);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // animator.SetBool("IsGrounded", isGrounded);
    }

    private void HandleCamera()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        cameraRotationY -= mouseY * cameraRotationSpeed;
        cameraRotationY = Mathf.Clamp(cameraRotationY, -80f, 80f);

        mainCamera.RotateAround(cameraTarget.position, Vector3.up, mouseX * cameraRotationSpeed);
        mainCamera.localRotation = Quaternion.Euler(cameraRotationY, mainCamera.localEulerAngles.y, 0f);

        Vector3 cameraOffset = new Vector3(0f, cameraHeight, -cameraDistance);
        Vector3 targetPosition = cameraTarget.position + mainCamera.TransformDirection(cameraOffset);

        mainCamera.position = Vector3.Lerp(mainCamera.position, targetPosition, Time.deltaTime * rotationSpeed);
        mainCamera.LookAt(cameraTarget);
    }
    public void PerformAbility(Ability ability, GameObject target)
    {
        if (ability.animingMode == AnimingMode.Default)
        {
            Debug.Log("PerformAbility AmingMode Default");
            combatController.PerformAbility(ability, target);
        }
        else if (ability.animingMode == AnimingMode.PrePositionPlacement)
        {
            Debug.Log("PerformAbility AmingMode PrePositionPlacement");
            aimingCorotine = StartCoroutine(AimingPositionPlacement(ability));
        }
    }
    public void StopPerformAbility()
    {
        if (aimingCorotine != null)
        {
            StopCoroutine(aimingCorotine);
            Destroy(templateObject);
        }
    }
    GameObject templateObject;
    public IEnumerator AimingPositionPlacement(Ability ability)
    {
        Debug.Log("PrePositionPlacement");
        templateObject = Instantiate(AOEAimingTemplate);
        // Cache the camera for screen to world point conversion
        Camera mainCamera = Camera.main;
        while (true)
        {
            // Get the mouse position and convert to world space
            RaycastHit hit;

            // Create a ray from the center of the camera
            Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            // Check if the raycast hits anything
            if (Physics.Raycast(ray, out hit))
            {
                // Get the hit position
                Vector3 hitPosition = hit.point;

                // Move the templateObject to the hit position
                templateObject.transform.position = hitPosition;
            }
            if (Input.GetMouseButton(0)){
                combatController.PerformAbility(ability, templateObject);
                Debug.Log(ability.TotalAbilityStats.cooldown);
                Destroy(templateObject);
                break;
            }
            yield return null;
        }

    }

    public Camera GetCamera()
    {
        return mainCamera.GetComponent<Camera>();
    }

    public void faceIndirectionOfCamera()
    {
        transform.rotation = Quaternion.Euler(0f, mainCamera.localEulerAngles.y, 0f);
    }
    public bool TryUnlockSkillNode(SkillNode skillNode)
    {
        if (skillNode == null)
        {
            Debug.LogWarning("Invalid skill node.");
            return false;
        }
        if (skillNode.isUnlocked)
        {
            Debug.LogWarning("Already learned.");
            return false;
        }

        // Check if the character has enough skill points to unlock the node.
        if (skillController.availableSkillPoints < skillNode.skillPointCost)
        {
            Debug.LogWarning("Not enough skill points.");
            return false;
        }

        // Check if the required main stat meets the node's requirement.
        bool statRequirementsMet = true;
        for (int i = 0; i < skillNode.mainStatRequirement.Count; i++)
        {
            Archetype statName = skillNode.mainStatRequirement[i];
            int requiredValue = skillNode.mainStatValue[i];

            switch (statName)
            {
                case Archetype.Strength:
                    if (characterStats.strength < requiredValue) statRequirementsMet = false;
                    break;
                case Archetype.Intelligence:
                    if (characterStats.intelligence < requiredValue) statRequirementsMet = false;
                    break;
                case Archetype.Dexterity:
                    if (characterStats.dexterity < requiredValue) statRequirementsMet = false;
                    break;
                case Archetype.Endurance:
                    if (characterStats.endurance < requiredValue) statRequirementsMet = false;
                    break;
                case Archetype.Wisdom:
                    if (characterStats.wisdom < requiredValue) statRequirementsMet = false;
                    break;
                default:
                    Debug.LogWarning("Invalid stat name in the skill node.");
                    break;
            }
        }

        if (!statRequirementsMet)
        {
            Debug.LogWarning("Main stat requirement not met.");
            return false;
        }

        // Check if the required prerequisite skill has been unlocked.
        if (skillNode.prerequisiteSkill != null && !skillNode.prerequisiteSkill.isUnlocked)
        {
            Debug.LogWarning("Prerequisite skill not unlocked.");
            return false;
        }

        // Check if the skill node is visible based on the fog of war mechanic.
        if (!skillController.skillTree.IsVisible(skillNode))
        {
            Debug.LogWarning("Skill node is not visible.");
            return false;
        }

        // Unlock the skill node.
        skillNode.isUnlocked = true;

        skillController.LearnSkill(skillNode);


        return true;
    }
    public bool TryUnLearnSkillNode(SkillNode skillNode)
    {
        if (skillNode.isUnlocked == false)
        {
            return false;
        }
        skillNode.isUnlocked = false;
        skillController.UnlearnSkill(skillNode);
        return true;
    }

}
