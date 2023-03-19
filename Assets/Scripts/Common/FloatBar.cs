using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[SerializeField]��� �ϴ� �� ���ؼ� ����ȭ�� �Ϸ���!
//����ȭ �� �� �ִ� �ֵ鸸 �� �� �־��!
//����ȭ�ϴ� ģ������ ������ �����?
[Serializable]
public class FloatBar
{
    //    Action                 <float>
    //��ȯ���� void�̰�, �Ű������� float�� �Լ��� ���� �ſ���!
    //void OnHealthChange(float value) ���� ģ���� ��Ƶ� �ſ���!
    //���⿡�ٰ� �Լ��� ���� ��Ƶθ�, Ư���� ���� �Ͼ�� �� �������� �ſ���!
    public Action<float> OnValueChange;
    public Action<float> OnValueEmpty;
    public Action<float> OnValueFull;

    [SerializeField] protected float _current;
    public float Current
    {
        get => _current;
        //�ٲٴ� ���� 0�� _max������ �����θ�!
        //set => _current = Mathf.Clamp(value, 0, _max);
        set
        {
            _current = Mathf.Clamp(value, 0, _max);
            //���� �ٲ���� �� �ؾ��ϴ� ���� �־����!
            if (OnValueChange != null) OnValueChange(_current);

            if (OnValueEmpty != null && _current == 0) OnValueEmpty(value);
            if (OnValueFull != null && _current == _max) OnValueFull(_current);
        }
    }
    [SerializeField] protected float _max;
    public float Max
    {
        get => _max;
        set
        {
            if (value <= 0) value = 0;
            _max = value;

            //���� ����º���.. �ִ� ������� �۾��� �� ���� �������?
            if (_max < _current) Current = _max;
        }
    }

    //�׻� ������� ����ó���� �ؾ�����ø� �ȵ˴ϴ�!
    public float Ratio => Max > 0 ? Current / Max : 0;

    //Float Bar�� ����� ��Ź�ϸ� �� ToString�� �ߵ��Ǿ ǥ���� ���ݴϴ�!
    public override string ToString(){ return $"{Current} / {Max}"; }
}
