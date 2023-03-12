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
            if(current.attackName == wantName) //아니! 이걸 만들고 싶었단 말인가?
            {
                GameObject created = Instantiate(current.prefab);
                //만든것의 위치는                제 위치          +   추가 위치!
                created.transform.position = transform.position + current.location;
                created.transform.localScale = current.size;

                //지속시간 뒤에 파괴되도록 하세요!
                Destroy(created, current.duration);
            };
        }
    }
}
