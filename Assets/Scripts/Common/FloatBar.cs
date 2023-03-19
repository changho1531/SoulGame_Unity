using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[SerializeField]라고 하는 걸 통해서 직렬화를 하려면!
//직렬화 할 수 있는 애들만 할 수 있어요!
//직렬화하는 친구들의 조건은 뭘까요?
[Serializable]
public class FloatBar
{
    //    Action                 <float>
    //반환값이 void이고, 매개변수가 float인 함수를 모을 거예요!
    //void OnHealthChange(float value) 같은 친구를 모아둘 거예요!
    //여기에다가 함수를 전부 담아두면, 특별한 일이 일어났을 때 실행해줄 거예요!
    public Action<float> OnValueChange;
    public Action<float> OnValueEmpty;
    public Action<float> OnValueFull;

    [SerializeField] protected float _current;
    public float Current
    {
        get => _current;
        //바꾸는 것은 0과 _max사이의 값으로만!
        //set => _current = Mathf.Clamp(value, 0, _max);
        set
        {
            _current = Mathf.Clamp(value, 0, _max);
            //값이 바뀌었을 때 해야하는 일이 있었어요!
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

            //현재 생명력보다.. 최대 생명력이 작아질 수 있지 않을까요?
            if (_max < _current) Current = _max;
        }
    }

    //항상 나누기는 예외처리를 잊어버리시면 안됩니다!
    public float Ratio => Max > 0 ? Current / Max : 0;

    //Float Bar를 출력을 부탁하면 이 ToString이 발동되어서 표현을 해줍니다!
    public override string ToString(){ return $"{Current} / {Max}"; }
}
