using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyAnimatorHandler : MonoBehaviour
{

    private Enemy _parent;
    
    Animator _animator;
    private readonly int _hashIsWalking = Animator.StringToHash("isWalking");
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    private void Start()
    {
        _parent = transform.parent.GetComponentInParent<Enemy>();
        _animator.SetBool(_hashIsWalking, true);
    }

    private void Update()
    {
        var isWalking = _parent.IsWalking;
        
        
        _animator.SetBool(_hashIsWalking, isWalking);
    }
    
}