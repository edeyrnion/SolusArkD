using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Animator))]
public class TamCharacterController : MonoBehaviour
{
	[SerializeField] David.TamWeaponTrigger trigger;	

	[SerializeField] float movingTurnSpeed = 360;
	[SerializeField] float stationaryTurnSpeed = 180;
	[SerializeField] float jumpPower = 12f;
	[Range(1f, 4f)] [SerializeField] float gravityMultiplier = 2f;
	[SerializeField] float runCycleLegOffset = 0.2f;
	[SerializeField] float moveSpeedMultiplier = 1f;
	[SerializeField] float animSpeedMultiplier = 1f;
	[SerializeField] float groundCheckDistance = 0.1f;

	Rigidbody rb;
	Animator animator;
	bool isGrounded;
	float origGroundCheckDistance;
	const float half = 0.5f;
	float turnAmount;
	float forwardAmount;
	Vector3 groundNormal;
	float capsuleHeight;
	Vector3 capsuleCenter;
	CapsuleCollider capsule;
	bool crouching;
	
	bool wait;

	void Start()
	{
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody>();
		capsule = GetComponent<CapsuleCollider>();
		capsuleHeight = capsule.height;
		capsuleCenter = capsule.center;

		rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
		origGroundCheckDistance = groundCheckDistance;
	}

	public void Attack()
	{
		if (!wait)
		{
			if (animator.GetCurrentAnimatorStateInfo(0).IsName("Grounded"))
			{
				var atk = Random.Range(0, 2);
				switch (atk)
				{
					case 0:
						animator.SetTrigger("Attack1");
						break;
					case 1:
						animator.SetTrigger("Attack2");
						break;
					default:
						break;
				}
				wait = true;
				IEnumerator coroutine = Wait(0.5f);
				StartCoroutine(coroutine);
			}
		}
	}

	IEnumerator Wait(float time)
	{
		yield return new WaitForSeconds(time);
		trigger.GetComponent<Collider>().enabled = true;		
		wait = false;
	}

	public void Move(Vector3 move, bool crouch, bool jump)
	{
		if (move.magnitude > 1f) move.Normalize();
		move = transform.InverseTransformDirection(move);
		CheckGroundStatus();
		move = Vector3.ProjectOnPlane(move, groundNormal);
		turnAmount = Mathf.Atan2(move.x, move.z);
		forwardAmount = move.z;

		ApplyExtraTurnRotation();

		if (isGrounded)
		{
			HandleGroundedMovement(crouch, jump);
		}
		else
		{
			HandleAirborneMovement();
		}

		ScaleCapsuleForCrouching(crouch);
		PreventStandingInLowHeadroom();

		UpdateAnimator(move);
	}

	void ScaleCapsuleForCrouching(bool crouch)
	{
		if (isGrounded && crouch)
		{
			if (crouching) return;
			capsule.height = capsule.height / 2f;
			capsule.center = capsule.center / 2f;
			crouching = true;
		}
		else
		{
			Ray crouchRay = new Ray(rb.position + Vector3.up * capsule.radius * half, Vector3.up);
			float crouchRayLength = capsuleHeight - capsule.radius * half;
			if (Physics.SphereCast(crouchRay, capsule.radius * half, crouchRayLength, Physics.AllLayers, QueryTriggerInteraction.Ignore))
			{
				crouching = true;
				return;
			}
			capsule.height = capsuleHeight;
			capsule.center = capsuleCenter;
			crouching = false;
		}
	}

	void PreventStandingInLowHeadroom()
	{
		if (!crouching)
		{
			Ray crouchRay = new Ray(rb.position + Vector3.up * capsule.radius * half, Vector3.up);
			float crouchRayLength = capsuleHeight - capsule.radius * half;
			if (Physics.SphereCast(crouchRay, capsule.radius * half, crouchRayLength, Physics.AllLayers, QueryTriggerInteraction.Ignore))
			{
				crouching = true;
			}
		}
	}

	void UpdateAnimator(Vector3 move)
	{
		animator.SetFloat("Forward", forwardAmount, 0.1f, Time.deltaTime);
		animator.SetFloat("Turn", turnAmount, 0.1f, Time.deltaTime);
		animator.SetBool("Crouch", crouching);
		animator.SetBool("OnGround", isGrounded);
		if (!isGrounded)
		{
			animator.SetFloat("Jump", rb.velocity.y);
		}

		float runCycle =
			Mathf.Repeat(
				animator.GetCurrentAnimatorStateInfo(0).normalizedTime + runCycleLegOffset, 1);
		float jumpLeg = (runCycle < half ? 1 : -1) * forwardAmount;
		if (isGrounded)
		{
			animator.SetFloat("JumpLeg", jumpLeg);
		}

		if (isGrounded && move.magnitude > 0)
		{
			animator.speed = animSpeedMultiplier;
		}
		else
		{
			animator.speed = 1;
		}
	}

	void HandleAirborneMovement()
	{
		Vector3 extraGravityForce = (Physics.gravity * gravityMultiplier) - Physics.gravity;
		rb.AddForce(extraGravityForce);

		groundCheckDistance = rb.velocity.y < 0 ? origGroundCheckDistance : 0.01f;
	}

	void HandleGroundedMovement(bool crouch, bool jump)
	{
		if (jump && !crouch && animator.GetCurrentAnimatorStateInfo(0).IsName("Grounded"))
		{
			rb.velocity = new Vector3(rb.velocity.x, jumpPower, rb.velocity.z);
			isGrounded = false;
			animator.applyRootMotion = false;
			groundCheckDistance = 0.1f;
		}
	}

	void ApplyExtraTurnRotation()
	{
		float turnSpeed = Mathf.Lerp(stationaryTurnSpeed, movingTurnSpeed, forwardAmount);
		transform.Rotate(0, turnAmount * turnSpeed * Time.deltaTime, 0);
	}

	public void OnAnimatorMove()
	{
		if (isGrounded && Time.deltaTime > 0)
		{
			Vector3 v = (animator.deltaPosition * moveSpeedMultiplier) / Time.deltaTime;

			v.y = rb.velocity.y;
			rb.velocity = v;
		}
	}

	void CheckGroundStatus()
	{
		RaycastHit hitInfo;

		if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, groundCheckDistance))
		{
			groundNormal = hitInfo.normal;
			isGrounded = true;
			animator.applyRootMotion = true;
		}
		else
		{
			isGrounded = false;
			groundNormal = Vector3.up;
			animator.applyRootMotion = false;
		}
	}
}
