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
    //�� ������ �����ϴ� ��ü!
    protected CharacterBase owner;

    void Start()
    {
        //���� �θ𿡼� �Դ�!
        owner = GetComponentInParent<CharacterBase>();
    }

    protected virtual Vector3 GetKnockBack(AttackInfo info)
    {
        Vector3 result = info.knockBack;

        if (owner.lookForward.x < 0) result.x *= -1; //���� ���ϱ� ��������!

        return result;
    }

    //������ ��� ������ ��Ÿ�� ���ΰ�?
    protected virtual Vector3 GetAttackLocation(AttackInfo info)
    {
        Vector3 result = info.location;

        //����.. �� ģ�� ���� ���±���..
        if (owner.lookForward.x < 0) result.x *= -1;
        //��ġ���� �������ָ� ��!
        result += transform.position;

        return result;
    }
    //������ ��� �������� ������ ���ΰ�?
    protected virtual Quaternion GetAttackRotation(AttackInfo info)
    {
        return Quaternion.identity;
    }

    public void Generate(string wantName)
    {
        foreach(var current in attacks)
        {
            if(current.attackName == wantName) //�ƴ�! �̰� ����� �;��� ���ΰ�?
            {
                GameObject created = Instantiate(current.prefab);
                //                               �ڵ带 ���� ������Ʈ�� �߰��ϱ�!
                AttackCollision attack = created.AddComponent<AttackCollision>();
                //���� �������� Ȯ���ϰ� �˰� ���ֱ�!
                attack.owner = owner;
                //���ݹ����� �������� �߰����ֱ�!
                attack.damage = current.damage;
                //���� ������ �˹��� �־��ֱ�!
                attack.knockBack = GetKnockBack(current);

                //������� ��ġ��                �� ��ġ          +   �߰� ��ġ!
                created.transform.position = GetAttackLocation(current);
                created.transform.rotation = GetAttackRotation(current);
                created.transform.localScale = current.size;

                //�ٿ��� �Ǵ� ���� �ֱ���..
                if(current.attachObject != "None")
                {
                    //transform�� ��ġ, ũ��, ȸ���� ������ �ִٰ� �߽��ϴ�.
                    //�װ� foreach�� �����ٱ���?
                    //�ڽ��� �θ�� ���� �����Դϴ�! �װ��� ��Ģ! transform�� �ٿ��� ���ϰڳ�!
                    //transform�� foreach�� �����ָ� ��� �ڽ��� �� �� �� �ֽ��ϴ�!
                    foreach (Transform childObject in transform)
                    {
                        //��~! �̸� �Ȱ��� �ְ� �ִ�! �밡����!
                        if(childObject.name == current.attachObject)
                        {
                            created.transform.SetParent(childObject); //�� �༮�� ���� �θ�� ��Ŷ�!
                            break; //�� ã�� �ʿ� �����!
                        };
                    };
                };

                //���ӽð� �ڿ� �ı��ǵ��� �ϼ���!
                Destroy(created, current.duration);
            };
        }
    }
}
