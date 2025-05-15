using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public static Controller Instance { get; private set; }

    private CharacterController controller;
    private SpriteRenderer spriteRenderer;
    private Vector3 playerVelocity;
    private bool isImmune = false;
    private Color OriginalColor;
    private PlayerControl playerInput;
    private float resetInterval;

    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private Animator animator;
    [SerializeField] public int Health = 5;
    [SerializeField] private float shootInterval;

    public int playerLayer;
    public int enemyLayer;
    public int enemyLayer2;
    public GameObject UltPrefab;
    public Transform UltPoint;

    public Shooting shooting;
    BarManager barManager;

    private void Awake()
    {
        //Ult.gameObject.SetActive(false);
        barManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<BarManager>();
        resetInterval = shootInterval;
        shooting = GetComponent<Shooting>();
        playerInput = new PlayerControl();
        controller =GetComponent<CharacterController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        OriginalColor = spriteRenderer.material.color;
    }

    private void OnEnable()
    {
         playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    void Update()
    {

        Vector2 MovementInput = playerInput.Player.Move.ReadValue<Vector2>();
        Vector2 move = new Vector2(MovementInput.x, MovementInput.y);
        controller.Move(move * Time.deltaTime * playerSpeed);

        controller.Move(playerVelocity * Time.deltaTime);

        if(MovementInput.x > 0)
        {
            animator.SetInteger("Vector2_XValue", 1);
        }
        else if(MovementInput.x < 0)
        {
            animator.SetInteger("Vector2_XValue", -1);
        }
        else
        {
            animator.SetInteger("Vector2_XValue", 0);
        }

        shootInterval -= Time.deltaTime;
        if(shootInterval <= 0)
        {
            shooting.Fire();
            shootInterval = resetInterval;
        }

        if(Health >= 5)
        {
            Health = 5;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Hit an Enemy");
            Health -= 2;
            //StartCoroutine(BriefTimeFreezeAfterTakingDamage());
            barManager.takeDamage(2);
            StartCoroutine(immuneCountdown(2f));
            GetHealth();
            Debug.Log(Health);
            if (Health <= 0)
            {
                GetHealth();
                Destroy(gameObject);
            }
        }

        if (other.gameObject.CompareTag("Health"))
        {
            Debug.Log("Hit a health pack");
            Health += 2;
            barManager.heal(2);
            GetHealth();
        }
    }

    public void dodgeButton()
    {
        StartCoroutine(immuneCountdown(1f));

    }

    public void UltButton()
    {
        StartCoroutine(UltDuration(5f));
    }

    IEnumerator immuneCountdown(float duration)
    {
        isImmune = true;
        barManager.DodgeButton.interactable = false;
        spriteRenderer.material.color = Color.black;
        Physics.IgnoreLayerCollision(playerLayer, enemyLayer, true);
        yield return new WaitForSeconds(duration);
        isImmune = false;
        barManager.DodgeButton.interactable = true;
        Physics.IgnoreLayerCollision(playerLayer, enemyLayer, false);
        spriteRenderer.material.color = OriginalColor;
    }

    IEnumerator UltDuration(float duration)
    {
        GameObject ultimate = Instantiate(UltPrefab, UltPoint);
        barManager.UltButton.interactable = false;
        yield return new WaitForSeconds(duration);
        barManager.UltButton.interactable = true;
        barManager.resetEnergy();
        Destroy(ultimate);
    }

    IEnumerator BriefTimeFreezeAfterTakingDamage()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(0.1f);
        Time.timeScale = 1;
    }

    /*private void OpenUltimate()
    {
    }*/

    public int GetHealth()
    {
        return Health;
    }
}

