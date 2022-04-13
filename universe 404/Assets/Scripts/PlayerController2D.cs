using UnityEngine;
using Fungus;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class PlayerController2D : MonoBehaviour
{
	public static  float  m_JumpForce = 800f;							// Amount of force added when the player jumps.
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck;							// A position marking where to check for ceilings
	[SerializeField] private Collider2D m_CrouchDisableCollider;                // A collider that will be disabled when crouching

	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
	const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D m_Rigidbody2D;
	private CharacterLife m_Life;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 velocity = Vector3.zero;

	public Animator anim;
	public Animator anim_elevater;
	public Animator anim_enemyHit1;
	public Animator anim_enemyHitTank;
	public Animator anim_break;
	public Animator anim_Fall;

	public static bool canFlow;

	public GameObject Enmey_1;

	public static bool isDead;
	public static bool isFall;


	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		m_Life = GetComponent<CharacterLife>();
	}


	private void FixedUpdate()
	{
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
				m_Grounded = true;
		}


	}
	public void Move(float move, bool crouch, bool jump)
	{
		if (!m_Life.Alive())
			return;

		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{

			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref velocity, m_MovementSmoothing);


			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
		}
		// If the player should jump...
		if (m_Grounded && jump)
		{
			// Add a vertical force to the player.
			m_Grounded = false;
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
		}
	}


	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.GetComponent<Flowchart>() != null && collision.GetComponent<UniqueId>() != null)
        {
			GameManager.instance.TriggeredFlowcharts.Add(collision.GetComponent<UniqueId>().uniqueId);
        }
		if (collision.gameObject.name == "EnmeyHit")
		{
			anim_enemyHit1.SetBool("canRun", false);
		}
		if (collision.gameObject.name == "EnmeyHit_tank")
		{
			anim_enemyHitTank.SetBool("canRun", false);
		}
	}
	private void OnTriggerStay2D(Collider2D collision)
    {
		if (collision.gameObject.name == "Wind")
        {
			m_Rigidbody2D.AddForce(Vector2.up * 40f);
		}

		if (collision.gameObject.name == "AirShipHit")
		{
			m_Rigidbody2D.velocity = Vector2.left * 10f;
		}

		if (collision.gameObject.name == "Break_1")
		{
			anim_break.SetBool("canBreak", true);
		}
		else
        {
			anim_break.SetBool("canBreak", false);
		}

		
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.name == "elevater_3")
		{
			anim_elevater.SetBool("canMove", true);
		}
        else
        {
			anim_elevater.SetBool("canMove", false);
		}

		if (collision.gameObject.name == "EnmeyHit")

		{
			anim_enemyHit1.SetBool("canRun", true);
		}
		if (collision.gameObject.name == "EnmeyHit_tank")
		{
			anim_enemyHitTank.SetBool("canRun", true);
		}

		if (collision.gameObject.name == "EnmeyHit" && Enmey_1.transform.position.x <= 111.5)
		{
			anim_enemyHit1.SetBool("canRun", true);
			CameraShake.Instance.canShake(0.08f, 0.8f);
		}

		if(collision.gameObject.tag == "disappear")
        {
			collision.gameObject.SetActive(false);
        }

		if (collision.gameObject.tag == "Enemy")
		{
			m_Life.Hit(collision.gameObject);
		}

		
		//ÔÚÕâÀï£¡£¡£¡£¡
		if (collision.gameObject.name == "Flowchart_111")
        {
			GameManager.instance.isOver_111 = true;
        }
		if (collision.gameObject.name == "Flowchart_222")
		{
			GameManager.instance.isOver_222 = true;
		}









		if (collision.gameObject.name == "FallHit")
        {
			isFall = true;
			anim_Fall.SetBool("canFall", true);

		}
	}

    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {

		if (collision.gameObject.name == "Break_1")
		{
			anim_break.SetBool("canBreak", true);
		}
		else
		{
			anim_break.SetBool("canBreak", false);
		}

	}
}
