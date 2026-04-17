using System.Collections;
using UnityEngine;

public class AnimatorControls : MonoBehaviour
{
    [SerializeField]
    int health = 3;
    Animator _animator;
    SpriteRenderer _sr;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponent<Animator>();
        _sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerInputManager.Instance.JumpPressed)
        {
            _animator.SetTrigger("Jump");
            _animator.SetBool("IsGrounded", false);
        }
        _animator.SetBool("IsFalling", PlayerInputManager.Instance.Movement.y < 0);

        _animator.SetFloat("DeltaX", PlayerInputManager.Instance.Movement.x);
        if (PlayerInputManager.Instance.Movement.x < 0)
        {
            _sr.flipX = true;
        }
        else if (PlayerInputManager.Instance.Movement.x > 0)
        {
            _sr.flipX = false;
        }

        if (PlayerInputManager.Instance.DamagePressed)
        {
            TakeDamage();
        }
        if (PlayerInputManager.Instance.GroundPressed)
        {
            _animator.SetBool("IsGrounded", true);
        }
        if (PlayerInputManager.Instance.WallslidePressed)
        {
            _animator.SetBool("WallSliding", !_animator.GetBool("WallSliding"));
        }
    }

    void TakeDamage(int damage = 1)
    {
        health -= damage;
        _animator.SetTrigger("Hit");
        if (health <= 0)
        {
            _animator.SetTrigger("Dead");
            health = 3;
        }
    }
}
