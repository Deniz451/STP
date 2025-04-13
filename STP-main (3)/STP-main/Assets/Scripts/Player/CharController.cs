using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CharController : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] Slider dashBar;


    [SerializeField] float moveSpeed = 800f;

    [SerializeField] float dashForce = 60f;
    [SerializeField] float dashCD = 3f;
    [SerializeField] float dashDuration = 0.3f;

    [SerializeField] float cloneDistance = 1.5f;
    [SerializeField] float cloneDuration = 0.3f;
    [SerializeField] GameObject dashClone;

    bool canMove = true;
    bool isDashing = false;
    float cooldown;

    Vector3 clonePosition;
    Vector3 moveDirection = Vector3.zero;
    Vector3 dashDirection = Vector3.zero;

    private void OnEnable()
    {
        EventManager.Instance.Subscribe(GameEvents.EventType.PlayerEnabled, () => canMove = true);
        EventManager.Instance.Subscribe(GameEvents.EventType.PlayerDisabled, () => canMove = false);
    }

    private void OnDestroy()
    {
        EventManager.Instance.Unsubscribe(GameEvents.EventType.PlayerEnabled, () => canMove = true);
        EventManager.Instance.Unsubscribe(GameEvents.EventType.PlayerDisabled, () => canMove = false);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    

    private void Update()
    {
        if (!canMove) { return; }

        if (Input.GetKeyDown(KeyCode.Space) && isDashing == false && rb != null && cooldown <= 0)
        {
            isDashing = true;
            cooldown = dashCD;

            StartCoroutine(Dodge());
        }

        if(cooldown > 0)
        {
            cooldown -= Time.deltaTime;
            dashBar.value = dashCD - cooldown;
        }
    }

    private void FixedUpdate()
    {
        if(transform.position.y >= 1) { gameObject.transform.position -= new Vector3(0, 0.5f, 0); }


        if (rb != null)
        {
            moveDirection = Vector3.zero;

            if (!isDashing)
            { 
                if (Input.GetKey(KeyCode.W))
                {
                    moveDirection += Vector3.forward;
                }
                if (Input.GetKey(KeyCode.S))
                {
                    moveDirection += Vector3.back;
                }
                if (Input.GetKey(KeyCode.A))
                {
                    moveDirection += Vector3.left;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    moveDirection += Vector3.right;
                }

                moveDirection = moveDirection.normalized;
                dashDirection = moveDirection;
                
                rb.velocity = Vector3.zero;
                rb.AddForce(moveDirection * moveSpeed * Time.deltaTime, ForceMode.VelocityChange);
            }
           
        }
        else { Debug.Log("Wtf kde RB?!?"); }
    }
    IEnumerator Dodge()
    {
        float elapsedTime = 0f;
        clonePosition = transform.position;

        dashDirection = (moveDirection == Vector3.zero ? Vector3.forward : moveDirection).normalized;
        Vector3 dashVelocity = dashDirection * dashForce;

        while(elapsedTime < dashDuration)
        {
            rb.velocity = dashVelocity;

            if(Vector3.Distance(clonePosition, transform.position)>=cloneDistance && isDashing)
            {
                clonePosition = transform.position;
                GameObject clone = Instantiate(dashClone, transform.position, transform.rotation);
                Destroy(clone, cloneDuration);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rb.velocity = Vector3.zero;
        isDashing = false;
    }
}
