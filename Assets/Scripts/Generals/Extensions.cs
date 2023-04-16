using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    //Ȯ�� �޼���� : float int���� �Ϲ����� ���ڿ��ٰ� �޼��带 ����� �;��� ��� �����̳���?
    //               int angle = -52;
    //               angle.abs();   << �̷��Ը� �ᵵ ���밪�� �����ָ� ����?
    //               angle.normalized();   << ���� ������� -1, ������ 1, 0�̸� �״��

    //���׸� �޼���� : <T> �̷� ������ �ε�ȣ ���ʿ� Ÿ���� �����Ӱ� �� �� �ֽ��ϴ�!
    //                 �ϳ��� �޼��带 ���� ���� Ÿ�Կ� ������ ���� �ִٴ� ������!

    //�� Ŭ������, �� �θ�Ŭ������ �ڽ��ΰ���?          object�� ��� Ÿ���� �̾߱�!
    public static ParentType IsSubclassOf<ParentType>(this object target) 
        where ParentType : class
    {
        //�ڽ��̸� ����ȯ�ص� ���� �����!
        //    �� Ÿ�԰�  �� Ÿ���� ���ų�               �� Ÿ����     �� Ÿ���� �ڽ��̶��
        if(target.GetType() == typeof(ParentType) || target.GetType().IsSubclassOf(typeof(ParentType)))
        {
            return (ParentType)target;
        }
        else
        {
            //��.. ��ȯ�� �� ���� ���°�?
            return null;
        };
    }

    //��� �Լ��� �Լ� ������ ������ �ٽ� ȣ���ϴ� �Լ�!
    public static Transform FindChildByName(this Transform target, string wantName)
    {
        //������� �Դٴ� ���� ���ٴ� �ſ���!
        //2���� ��Ȳ�� �ֽ��ϴ�! �ڽ��� ������, �ڽ��� ���� ��!
        if(target.childCount > 0)
        {
            //�ڽ��� �� �ѷ��ôµ�
            foreach(Transform child in target)
            {
                //�̸� �´� �ְ� �� �־��!
                if(child.name == wantName) return child;
            };

            //�ڽ��� �ڽĿ��� -> �� �ڽĿ��� -> �� �ڽĿ���
            foreach (Transform child in target)
            {
                Transform childFinder = child.FindChildByName(wantName);
                if(childFinder)
                {
                    return childFinder;
                };
            };
        };
        //�ڽ��� ���ų�, �ڽ����׵� ������ null
        return null;
    }


    //                           ����?    target�� ������� Normalized��� �޼��带 �־��� ��!
    public static int Normalized(this int target)
    {
        if      (target == 0)   return 0;
        else if (target < 0)    return -1;
        else                    return 1;
    }
    public static float Normalized(this float target)
    {
        if (target == 0) return 0;
        else if (target < 0) return -1;
        else return 1;
    }

    public static Vector3 ToHorizontalVector(this float angle)
    {
        //������ �������� �ٲ� ������ �������ؿ�!
        float rad = angle * Mathf.Deg2Rad;
        //                      x                   z
        return new Vector3(Mathf.Cos(rad), 0, Mathf.Sin(rad));
    }

    public static Vector3 ToVerticalVector(this float angle)
    {
        //������ �������� �ٲ� ������ �������ؿ�!
        float rad = angle * Mathf.Deg2Rad;
        //                      x                   y
        return new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0);
    }

    //������ ������ ���͸� �����!
    public static Vector3 ToAngularVector(this Vector3 result, float horizontalAngle, float verticalAngle)
    {
        //������ �������� �ٲ� ������ �������ؿ�!
        float horRad = horizontalAngle * Mathf.Deg2Rad;
        float verRad = verticalAngle * Mathf.Deg2Rad;
        //                      x                   y
        return new Vector3
            (
                Mathf.Cos(horRad) * Mathf.Abs(Mathf.Cos(verRad)),
                Mathf.Sin(verRad),
                Mathf.Sin(horRad) * Mathf.Abs(Mathf.Cos(verRad))
            );
    }

    public static float GetVerticalAngle(this Vector3 direction)
    {
        //y�� �״������, Cos�� �ش��ϴ� �κ��� ���� ������ ũ��� ��ȯ�� �Ǿ�������ϱ�!
        //������ ����� ��� ������! x^2 + y^2�� sqrt �� ���̶�� �߾���?
        return Mathf.Atan2(direction.y, Mathf.Sqrt((direction.x * direction.x) + (direction.z * direction.z))) * Mathf.Rad2Deg;
    }

    public static float GetHorizontalAngle(this Vector3 direction)
    {
        //atan2
        //�� ź��Ʈ��� ��!
        //ź��Ʈ�� ������ �����س��� ������ �������� �� �� �־��!
        //atan2�� 360�� atan�� 180���� ���� �� �־��!
        return Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
    }
}
