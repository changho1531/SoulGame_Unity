using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttackInfo
{
    public string attackName;
    public GameObject prefab;
    public Vector3 location;
    public Vector3 size;
    public float duration;
}
public class AttackGenerator : MonoBehaviour
{
    public AttackInfo[] attacks;
    void Start()
    {
        
    }
    public void Generate(string wantName)
    {
        foreach(var current in attacks)
        {
            if(current.attackName == wantName) //�ƴ�! �̰� ����� �;��� ���ΰ�?
            {
                GameObject created = Instantiate(current.prefab);
                //������� ��ġ��                �� ��ġ          +   �߰� ��ġ!
                created.transform.position = transform.position + current.location;
                created.transform.localScale = current.size;

                //���ӽð� �ڿ� �ı��ǵ��� �ϼ���!
                Destroy(created, current.duration);
            };
        }
    }
}
