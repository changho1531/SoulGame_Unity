using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[SerializeField]��� �ϴ� �� ���ؼ� ����ȭ�� �Ϸ���
//����ȭ �� �� �ִ� �ֵ鸸 �� �� �־��
//����ȭ�ϴ� ģ������ ������ �����?
//using System; ����
[Serializable]
public class FloatBar
{
    //Field : �������
    //public�Ⱦ��� [SerializeField] ���� ����
    //������ ���� �ٲٴ°� ��������
    //�ٸ� ����̶� ���� �� �ʹ� ������ ������ �� ����
    //������ �� �� �ְ� �ʿ��� �͸� �����ֱ� ����. ���Ȼ�
    [SerializeField] protected float _current;
    [SerializeField] protected float _max;
}
