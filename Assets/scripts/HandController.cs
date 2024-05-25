using UnityEngine;

public class HandController : MonoBehaviour
{
    private Animator handAnimator;

    void Start()
    {
        handAnimator = GetComponent<Animator>();
    }

    public void SetAttack(bool isAttacking)
    {
        handAnimator.SetBool("test", isAttacking);
    }
}
