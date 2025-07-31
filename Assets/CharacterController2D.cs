using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections.Generic;
using System;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 400f;							// Amount of force added when the player jumps.
	// [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField] private List<LayerMask> m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck;							// A position marking where to check for ceilings
	[SerializeField] private float maxVerticalSpeed;
	[SerializeField] private float maxHorizontalSpeed;
	[SerializeField] private float maxMovementSpeed;
	[SerializeField] private float acceleration;
	[SerializeField] private float brakeForce;
	[SerializeField] private float runSpeed = 40f;

    private float horizontalMove = 0f;

	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
	const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 velocity = Vector3.zero;
	private FollowCursor cursor;

	// Ability ability1;
	// Ability ability2;
	// Heat heatComponent;

	[SerializeField] TMP_Text ability1Cooldown;
	[SerializeField] TMP_Text ability2Cooldown;

	protected bool activated1 = false;
	protected bool activated2 = false;

	// public List<Modifier> touchingModifier;
	public bool touchingDisassembler = false;

	// so variable jump height can't cancel vertical momentum if you didn't jump
	// canceling will be lost on knockback or recoil
	public bool canCancelJump = false;

	bool jump = false;
	bool hasJumped = false;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
        // if (ability1)
        // {
        // 	ability1Cooldown.text = "" + ability1.cooldown;
        // }
        // if (ability2)
        // {
        // 	ability2Cooldown.text = "" + ability2.cooldown;
        // }
        
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		if (activated1)
        {
            Activate1();
        }
		if (activated2)
		{
			Activate2();
		}
	}


    private void FixedUpdate()
    {
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        List<Collider2D> colliders = new List<Collider2D>();
        foreach (LayerMask layerIndex in m_WhatIsGround)
        {
            colliders.AddRange(Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, layerIndex));
        }
        for (int i = 0; i < colliders.Count; i++)
        {
            if (colliders[i].gameObject != gameObject)
                m_Grounded = true;
        }

        // limit speed
        float clampedVerticalSpeed = Mathf.Clamp(m_Rigidbody2D.velocity.y, -maxVerticalSpeed, maxVerticalSpeed);
        float clampedHorizontalSpeed = Mathf.Clamp(m_Rigidbody2D.velocity.x, -maxHorizontalSpeed, maxHorizontalSpeed);
        m_Rigidbody2D.velocity = new Vector2(clampedHorizontalSpeed, clampedVerticalSpeed);
        
        // Move our character
		Move(horizontalMove * Time.fixedDeltaTime);

	}
	
	private Vector2 getAim()
	{
		// this worked with the orthographic camera but is inaccurate with a perspective camera
		// Vector3 mousePosInput = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		// Vector2 mousePos = new Vector2(mousePosInput.x, mousePosInput.y);

		// FollowCursor is already doing this work so we'll just use its position
		if (!cursor)
		{
			cursor = FindObjectOfType<FollowCursor>();
		}
		Vector2 mousePos = cursor.transform.position;
		Vector2 playerPos = new Vector2(m_Rigidbody2D.position.x, m_Rigidbody2D.position.y);
		Vector2 aim = mousePos - playerPos;
		return aim;
	}

	void OnJump(InputValue value)
	{
		Debug.Log("jump");
		jump = value.isPressed;
		if (!jump)
		{
			hasJumped = false;
		}
	}


	public void Move(float move)
	{
		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{

			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector3(move * acceleration * m_Rigidbody2D.mass, 0f, 0f);

			// allows recoil to slow the player down while preserving snappy movement
			// uses calculated recoil value to  lower movement speed cap based on aim
			// only reduce speed if you've fired recently (fixes recoil for long cooldown weapons	)
			// todo: recoil doesn't need to slow your movement speed in the opposite direction
			// float firedRecently = ability1.cooldownSeconds - .1f;
			float recoil = 0f;
			// if (activated1 && (ability1.cooldown >= firedRecently))
			// {
			// 	recoil += ability1.cooldownScaledRecoil;
			// }
			// if (activated2 && (ability2.cooldown >= firedRecently))
			// {
			// 	recoil += ability2.cooldownScaledRecoil;
			// }
			float recoilAdjustedMaxSpeed = maxMovementSpeed - (recoil * 10f * Math.Abs(getAim().normalized.x));

			// if not at top speed
			if (Math.Abs(m_Rigidbody2D.velocity.x) < recoilAdjustedMaxSpeed)
			{
				m_Rigidbody2D.AddForce(targetVelocity, ForceMode2D.Force);
			}

			// brake force
			float moveSign = move / Math.Abs(move);
			float xSign = m_Rigidbody2D.velocity.x / Math.Abs(m_Rigidbody2D.velocity.x);
			bool movingOppositeMomentum = moveSign != xSign && Math.Abs(m_Rigidbody2D.velocity.x) > .05f;
			if (movingOppositeMomentum)
			{
				m_Rigidbody2D.AddForce(new Vector3(-xSign * (brakeForce * m_Rigidbody2D.mass), 0f, 0f), ForceMode2D.Force);
			}
			// brake down from bonus recoil speed (or other) to normal speed
			bool overTopSpeed = Math.Abs(m_Rigidbody2D.velocity.x) > maxMovementSpeed;
			if (overTopSpeed)
			{
				m_Rigidbody2D.AddForce(new Vector3(-xSign * .3f * (brakeForce * m_Rigidbody2D.mass), 0f, 0f), ForceMode2D.Force);
			}

			// actually stop when slow
			// without this we roll forever due to frictionless material
			if (Math.Abs(m_Rigidbody2D.velocity.x) < 1f)
			{
				m_Rigidbody2D.velocity = new Vector2(0f, m_Rigidbody2D.velocity.y);
			}

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
		if (m_Grounded && jump && !hasJumped)
		{
			// Add a vertical force to the player.
			m_Grounded = false;
			canCancelJump = true;
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
			hasJumped = true;
		}

		// variable jump height
		if (canCancelJump && !jump)
		{
			if (m_Rigidbody2D.velocity.y > 0f)
			{
				m_Rigidbody2D.velocity = new Vector3(m_Rigidbody2D.velocity.x, 0f, 0f);
			}
			
		}

		// to prevent jump cancelling from being used to stop falls
		if (m_Rigidbody2D.velocity.y < -.5f)
		{
			canCancelJump = false;
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
        GameObject gunArm = GetComponentInChildren<RotateTowardTarget>().gameObject;
        gunArm.transform.localScale *= -1;
	}

	void Start()
	{
		gameObject.tag = "Player";
		gameObject.layer = 6;
        // Movement mvt = gameObject.GetComponentInChildren<Movement>();
        // Effect effect = gameObject.GetComponentInChildren<Effect>();
        // ability1 = gameObject.GetComponentsInChildren<Ability>()[0];
		// heatComponent = gameObject.GetComponent<Heat>();
		activated1 = false;
		activated2 = false;
	}

	void OnActivate1(InputValue value)
	{
		activated1 = value.isPressed;
	}

	void Activate1()
	{
		Debug.Log("activate1");
		// this.ability1.Activate(getAim().normalized);
	}

	void OnActivate2(InputValue value)
	{
		activated2 = value.isPressed;
	}

	void Activate2()
	{
		Debug.Log("activate2");
	}

	// void OnPickUp1(InputValue value)
	// {
	// 	if (value.isPressed)
	// 	{
	// 		if (!touchingDisassembler)
	// 		{
	// 			// Debug.Log("pick up 1");
	// 			if (touchingModifier.Count > 0)
	// 			{
	// 				Modifier mod = touchingModifier[0];
	// 				mod.setPhysical(false);
	// 				mod.gameObject.transform.SetParent(ability1.transform, true);
	// 				ability1.RecalculateModifiers();
	// 			}
	// 		} else
	// 		{
	// 			// Debug.Log("disassemble 1");
	// 			Modifier[] mods = ability1.GetComponentsInChildren<Modifier>();
	// 			foreach (Modifier mod in mods)
	// 			{
	// 				mod.gameObject.GetComponent<Rigidbody2D>().position = Vector3.zero;
	// 				mod.setPhysical(true);
	// 				mod.gameObject.transform.SetParent(GameObject.FindWithTag("Level").transform, true);
	// 				mod.gameObject.transform.localScale = new Vector3(1, 1, 0); // undo player flip
	// 			}
	// 			ability1.RecalculateModifiers();
	// 		}
	// 	}
	// }

	// void OnPickUp2(InputValue value)
	// {
	// 	Debug.Log("pick up 2");
	// }
	
    void OnLook(InputValue value)
	{
		// made look input action have no mouse component
		// seems a bit jank but should work for now
		cursor.lookVector = (Vector2) (value.Get() ?? Vector2.zero);
	}
}