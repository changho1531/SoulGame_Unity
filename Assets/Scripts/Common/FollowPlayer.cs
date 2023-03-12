using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Vector3 offset;

    protected virtual void Update()
    {
        //Linear Interpolate
        //L         erp           5           22
        // ����      ����          A           B         �� ������ ����   0�� A    1�� B
        //                           0.1
        //                        A * 0.9     B * 0.1
        //                         4.5     +    2.2    = 6.7
        transform.position = Vector3.Lerp(transform.position, GetGoalPosition(), 0.01f);
    }

    protected virtual Vector3 GetGoalPosition()
    {
        return GameManager.PlayerFind().transform.position + offset;
    }
}
