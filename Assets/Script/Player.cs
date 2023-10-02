using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    #region Properties
    [Header("MOVEMENT")]
    private Vector3 moveDirection;
    private float currentSpeed;
    public bool blockPlayerMovement;

    [Header("DASH")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashCooldown;
    private bool isDashing = false;
    //private bool canDash = true;

    [Header("STATS")]
    private float maxHealth;
    private float currentHealth;
    public float getHealth() { return currentHealth;  }

    [Header("COMPONENTS")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject mesh;
    [SerializeField] private Animator animator;
    [SerializeField] private UI_ValueBar healthBar;
    [SerializeField] private BlinkColor blink;

    [Header("INTERACTION SYSTEM")]
    public Vector3 aimDirection;
    //private List<Interaction> interactionList = new List<Interaction>();

    [Header("WEAPONS")]
    public Weapon_Sword sword;
    public Weapon_Axe axe;

    [SerializeField] private AudioSource audioSource;
    #endregion

    #region Methods
    //private void Dash(InputAction.CallbackContext context)
    //{
    //    //if (!PermanentDataHolder.Instance.currentAbilities.abilityDash) return;
    //    if (!canDash) return;
    //    isDashing = true;
    //    canDash = false;
    //    //DOTween.To(() => CinemachineShake.Instance.cinemachineVirtualCamera.m_Lens.FieldOfView, x => CinemachineShake.Instance.cinemachineVirtualCamera.m_Lens.FieldOfView = x, 24, .2f);
    //    StartCoroutine(StopDashing());
    //    //Instantiate(playerFX.dashParticle, smokeSource).transform.position = smokeSource.transform.position;
    //    animator.SetTrigger("Dashing");
    //}

    //private IEnumerator StopDashing()
    //{
    //    yield return new WaitForSeconds(dashTime);
    //    //DOTween.To(() => CinemachineShake.Instance.cinemachineVirtualCamera.m_Lens.FieldOfView, x => CinemachineShake.Instance.cinemachineVirtualCamera.m_Lens.FieldOfView = x, 23, .2f);
    //    isDashing = false;
    //    yield return new WaitForSeconds(dashCooldown);
    //    canDash = true;
    //}

    public void Damage(float _damage)
    {
        if (currentHealth <= 0) return;
            //Instantiate(playerFX.damageParticle, transform).transform.position = transform.position;
        //CinemachineShake.Instance.ShakeCamera(3, .1f);
        currentHealth -= _damage;
        healthBar.SetBarValue(currentHealth, maxHealth);
        blink.Blink();
        if (audioSource != null) audioSource.clip = GPCtrl.Instance.GeneralData.playerDamageSound[Random.Range(0, GPCtrl.Instance.GeneralData.playerDamageSound.Count)];
        if (audioSource != null) audioSource.Play();
        CinemachineShake.Instance.ShakeCamera(3, .1f);
        //GPCtrl.Instance.UICtrl.healthCount.SetText(currentHealth.ToString() + "/" + maxHealth.ToString());
        if (currentHealth <= 0)
            Death();
        //else
            //blinkColor.Blink();
    }

    public void Death()
    {
        Debug.Log("DEATH");
        blockPlayerMovement = true;
        animator.SetTrigger("Death");
        GPCtrl.Instance.GameOver();
        //DOVirtual.DelayedCall(.8f, () => {
        //    PermanentDataHolder.Instance.FadeIn(() =>
        //    {
        //        PermanentDataHolder.Instance.currentMaterial = 0;
        //        SceneManager.LoadScene("Base");
        //        PermanentDataHolder.Instance.FadeOut();
        //    });
        //});
    }

    public void Attack()
    {

    }
    #endregion

    #region UnityAPI
    private void Start()
    {
        currentSpeed = GPCtrl.Instance.GeneralData.playerNormalSpeed;
        maxHealth = GPCtrl.Instance.GeneralData.playerMaxHealth;
        currentHealth = maxHealth;
        healthBar.SetBarValue(currentHealth, maxHealth);
        //GPCtrl.Instance.UICtrl.healthCount.SetText(currentHealth.ToString() + "/" + maxHealth.ToString());
    }

    void Update()
    {
        if (blockPlayerMovement)
        {
            moveDirection = Vector3.zero;
            return;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            GPCtrl.Instance.UICtrl.UpgradeMenu.CallMenu();
        }

        if (GPCtrl.Instance.pause)
        {
            moveDirection = Vector3.zero;
            return;
        }
        moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        //Rotation system
        //if (aimDirection != Vector3.zero) mesh.transform.forward = Vector3.RotateTowards(mesh.transform.forward, aimDirection, 10 * Time.deltaTime, 0);

        var mouse_pos = Input.mousePosition;
        mouse_pos.z = 5.23f; //The distance between the camera and object
        var object_pos = Camera.main.WorldToScreenPoint(mesh.transform.position);
        mouse_pos.x = mouse_pos.x - object_pos.x;
        mouse_pos.y = mouse_pos.y - object_pos.y;
        var angle = -Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;

        mesh.transform.rotation = Quaternion.Euler(new Vector3(0, angle + 90, 0));
        //Vector3.RotateTowards(mesh.transform.forward, moveDirection, 30 * Time.deltaTime, 0);

        //else if (aimDirection != Vector3.zero) mesh.transform.forward = aimDirection;

        //currentSpeed = aimingSpeed;
        //currentProjectile.transform.forward = mesh.transform.forward;
        //if (moveDirection != Vector3.zero) animator.speed = 1; // moving
        //else animator.speed = 0;
        //currentSpeed = runningSpeed;
        //animator.speed = 1;

        if (moveDirection != Vector3.zero) animator.SetBool("Walking", true);
        else animator.SetBool("Walking", false);
    }
    private void FixedUpdate()
    {
        if(isDashing)
        {
            rb.velocity = new Vector3(moveDirection.x * dashSpeed, rb.velocity.y, moveDirection.z * dashSpeed);
        }
        else
        {
            rb.velocity = new Vector3(moveDirection.x * currentSpeed, rb.velocity.y, moveDirection.z * currentSpeed);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //if(other.GetComponent<Drop>() != null) //DROP
        //{
        //    other.GetComponent<Drop>().currentPlayer = this;
        //}
        //if (other.GetComponent<Interaction>() != null)
        //{
        //    if (!interactionList.Contains(other.GetComponent<Interaction>()))
        //    {
        //        Debug.Log("interaction add to list : " + other.GetComponent<Interaction>().name);
        //        interactionList.Add(other.GetComponent<Interaction>());
        //        GPCtrl.Instance.UICtrl.callToAction.ShowCallToAction(other.transform.position);
        //        GPCtrl.Instance.UICtrl.inputIndication.ShowInputIndication();
        //    }
        //}
    }
    private void OnTriggerExit(Collider other)
    {
        //Interaction _interaction = other.GetComponent<Interaction>();
        //if (_interaction != null)
        //{
        //    if(interactionList.Contains(_interaction))
        //    {
        //        if (other.GetComponent<Interaction>() == interactionList[interactionList.Count - 1] && interactionList.Count > 1)
        //        {
        //            interactionList.Remove(_interaction);
        //            GPCtrl.Instance.UICtrl.callToAction.ShowCallToAction(interactionList[interactionList.Count - 1].transform.position);
        //            GPCtrl.Instance.UICtrl.inputIndication.ShowInputIndication();
        //        } else
        //        {
        //            interactionList.Remove(_interaction);
        //            GPCtrl.Instance.UICtrl.callToAction.HideCallToAction();
        //            GPCtrl.Instance.UICtrl.inputIndication.HideInputIndication();
        //        }
        //    }
        //}
    }
    #endregion
}
