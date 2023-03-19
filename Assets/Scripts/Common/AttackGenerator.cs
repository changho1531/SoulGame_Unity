using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttackInfo
{
    public string attackName;

    [Header("Attribute")]
    public string attachObject = "None";
    public GameObject prefab;
    public Vector3 location;
    public Vector3 size;
    public float duration;

    [Header("Values")]
    public float damage;
    public Vector3 knockBack;
}
public class AttackGenerator : MonoBehaviour
{
    public AttackInfo[] attacks;
    //이 공격을 실행하는 주체!
    protected CharacterBase owner;

    void Start()
    {
        //나는 부모에서 왔다!
        owner = GetComponentInParent<CharacterBase>();
    }

    protected virtual Vector3 GetKnockBack(AttackInfo info)
    {
        Vector3 result = info.knockBack;

        if (owner.lookForward.x < 0) result.x *= -1; //왼쪽 보니까 왼쪽으로!

        return result;
    }

    //공격은 어느 곳에서 나타날 것인가?
    protected virtual Vector3 GetAttackLocation(AttackInfo info)
    {
        Vector3 result = info.location;

        //아하.. 이 친구 왼쪽 보는구만..
        if (owner.lookForward.x < 0) result.x *= -1;
        //위치까지 조정해주면 끝!
        result += transform.position;

        return result;
    }
    //공격은 어느 방향으로 돌려줄 것인가?
    protected virtual Quaternion GetAttackRotation(AttackInfo info)
    {
        return Quaternion.identity;
    }

    public void Generate(string wantName)
    {
        foreach(var current in attacks)
        {
            if(current.attackName == wantName) //아니! 이걸 만들고 싶었단 말인가?
            {
                GameObject created = Instantiate(current.prefab);
                //                               코드를 통해 컴포넌트를 추가하기!
                AttackCollision attack = created.AddComponent<AttackCollision>();
                //누가 주인인지 확실하게 알게 해주기!
                attack.owner = owner;
                //공격범위에 데미지를 추가해주기!
                attack.damage = current.damage;
                //현재 공격의 넉백을 넣어주기!
                attack.knockBack = GetKnockBack(current);

                //만든것의 위치는                제 위치          +   추가 위치!
                created.transform.position = GetAttackLocation(current);
                created.transform.rotation = GetAttackRotation(current);
                created.transform.localScale = current.size;

                //붙여야 되는 곳이 있구만..
                if(current.attachObject != "None")
                {
                    //transform은 위치, 크기, 회전을 가지고 있다고 했습니다.
                    //그걸 foreach로 돌린다구요?
                    //자식은 부모와 같이 움직입니다! 그것이 원칙! transform에 붙여야 편하겠네!
                    //transform을 foreach로 돌려주면 모든 자식을 다 돌 수 있습니다!
                    foreach (Transform childObject in transform)
                    {
                        //아~! 이름 똑같은 애가 있다! 얜가보다!
                        if(childObject.name == current.attachObject)
                        {
                            created.transform.SetParent(childObject); //이 녀석을 너의 부모로 삼거라!
                            break; //더 찾을 필요 없어요!
                        };
                    };
                };

                //지속시간 뒤에 파괴되도록 하세요!
                Destroy(created, current.duration);
            };
        }
    }
}
