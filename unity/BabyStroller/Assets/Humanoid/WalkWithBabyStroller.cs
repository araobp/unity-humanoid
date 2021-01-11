﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkWithBabyStroller : MonoBehaviour
{
    public GameObject leftHand;
    public GameObject rightHand;

    private Animator animator;

    Transform leftHandObj;
    Transform rightHandObj;

    float leftHandPositionWeight = 0.9F;
    float rightHandPositionWeight = 0.9F;

    public Vector3 leftHandPosAdjust = new Vector3(-0.06F, 0.08F, -0.11F);
    public Vector3 rightHandPosAdjust = new Vector3(0.06F, 0.08F, -0.11F);

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        // With position adjustment
        leftHandObj = leftHand.transform;
        rightHandObj = rightHand.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnAnimatorIK()
    {
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, leftHandPositionWeight);
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, rightHandPositionWeight);

        animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandObj.position + leftHandPosAdjust);
        animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandObj.position + rightHandPosAdjust);
    }
}