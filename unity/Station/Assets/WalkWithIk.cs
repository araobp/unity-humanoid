using UnityEngine;

public class WalkWithIk : MonoBehaviour
{
    public GameObject leftHand;

    private Animator animator;
    float leftHandPositionWeight = 0.9F;
    float leftHandRotationWeight = 0.5F;
    Transform leftHandObj;

    void Start()
    {
        animator = GetComponent<Animator>();
        leftHandObj = leftHand.transform;
    }

    void OnAnimatorIK()
    {
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, leftHandPositionWeight);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, leftHandRotationWeight);
        animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandObj.position);
        animator.SetIKRotation(AvatarIKGoal.LeftFoot, leftHandObj.rotation);
    }
}
