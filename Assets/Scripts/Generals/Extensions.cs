using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    //확장 메서드란 : float int같이 일반적인 숫자에다가 메서드를 만들고 싶었던 기억 있으셨나요?
    //               int angle = -52;
    //               angle.abs();   << 이렇게만 써도 절대값을 돌려주면 어떨까요?
    //               angle.normalized();   << 값이 음수라면 -1, 양수라면 1, 0이면 그대로

    //제네릭 메서드란 : <T> 이런 식으로 부등호 안쪽에 타입을 자유롭게 쓸 수 있습니다!
    //                 하나의 메서드를 만들어도 여러 타입에 적용할 수도 있다는 말이죠!

    //이 클래스가, 이 부모클래스의 자식인가요?          object는 모든 타입을 이야기!
    public static ParentType IsSubclassOf<ParentType>(this object target) 
        where ParentType : class
    {
        //자식이면 형변환해도 문제 없어요!
        //    이 타입과  저 타입이 같거나               이 타입이     저 타입의 자식이라면
        if(target.GetType() == typeof(ParentType) || target.GetType().IsSubclassOf(typeof(ParentType)))
        {
            return (ParentType)target;
        }
        else
        {
            //아.. 변환을 할 수가 없는걸?
            return null;
        };
    }

    //재귀 함수란 함수 내에서 본인을 다시 호출하는 함수!
    public static Transform FindChildByName(this Transform target, string wantName)
    {
        //여기까지 왔다는 것은 없다는 거예요!
        //2가지 상황이 있습니다! 자식이 있을때, 자식이 없을 때!
        if(target.childCount > 0)
        {
            //자식을 쭉 둘러봤는데
            foreach(Transform child in target)
            {
                //이름 맞는 애가 딱 있어보림!
                if(child.name == wantName) return child;
            };

            //자식은 자식에게 -> 또 자식에게 -> 또 자식에게
            foreach (Transform child in target)
            {
                Transform childFinder = child.FindChildByName(wantName);
                if(childFinder)
                {
                    return childFinder;
                };
            };
        };
        //자식이 없거나, 자식한테도 없으면 null
        return null;
    }


    //                           누구?    target을 대상으로 Normalized라는 메서드를 넣어줄 것!
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
        //각도를 라디안으로 바꾼 다음에 돌려야해요!
        float rad = angle * Mathf.Deg2Rad;
        //                      x                   z
        return new Vector3(Mathf.Cos(rad), 0, Mathf.Sin(rad));
    }

    public static Vector3 ToVerticalVector(this float angle)
    {
        //각도를 라디안으로 바꾼 다음에 돌려야해요!
        float rad = angle * Mathf.Deg2Rad;
        //                      x                   y
        return new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0);
    }

    //각도를 가지고 벡터를 만들기!
    public static Vector3 ToAngularVector(this Vector3 result, float horizontalAngle, float verticalAngle)
    {
        //각도를 라디안으로 바꾼 다음에 돌려야해요!
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
        //y는 그대로지만, Cos에 해당하는 부분은 수평 각도의 크기로 변환이 되어버렸으니까!
        //수평의 사이즈를 재는 것으로! x^2 + y^2을 sqrt 한 것이라고 했었죠?
        return Mathf.Atan2(direction.y, Mathf.Sqrt((direction.x * direction.x) + (direction.z * direction.z))) * Mathf.Rad2Deg;
    }

    public static float GetHorizontalAngle(this Vector3 direction)
    {
        //atan2
        //역 탄젠트라는 뜻!
        //탄젠트를 역으로 추출해내면 각도가 몇인지를 알 수 있어요!
        //atan2는 360도 atan은 180도만 구할 수 있어요!
        return Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
    }
}
