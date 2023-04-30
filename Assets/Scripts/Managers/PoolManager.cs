using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//struct
[System.Serializable]
public struct PoolInfo
{
    //�� �� ��Ʈ���� �̸��� ����!
    public string infoName;

    public GameObject prefab;
    public int amount;
}

public class PoolManager : MonoBehaviour
{
    //Object Pooling
    //������Ʈ�� �ı��ϴ� ������ ������ �ı����� �ʰ�, ��Ȱ��ȭ ���ѳ��� ��

    //Instantiate : ������ �ִ� ������Ʈ�� �����ؼ� ���ο� ������ �����.
    //               -> ���� ���� ���� ������Ʈ�� �ٽ� �Ҵ�.
    //Destroy     : �μ���!
    //               -> ����� �Ǵ� ������Ʈ�� ����.

    //Ǯ���� ���� : ���� ����ų� �μ��� ������ ��� ������ ������
    //           : ����� ��� ���ִ� �������� > �̸� �����־� �մϴ�!
    //                                         �޸𸮿� �Ҵ��Ѵ�.
    //                                             �ε�

    //��� : �����Ǿ� �ִ� ��!
    //       �׷��� ������?
    //Ǯ���� �� ������ �̸� ����Ʈ�� ������ �̴ϴ�!
    //�迭�̶� �ٸ� ���� �߰��� �߰��� �� �ִٴ� �̴ϴ�!
    //����Ʈ�� �迭���� Ž���� ������!
    //�迭�� �߰��� �ȵſ�!
    //�ǽð����� ���� �ٲٴ� ���� �ִٸ� ����Ʈ!
    public List<PoolInfo> infoList = new List<PoolInfo>();

    // Queue : ť = ��⿭   ->  ���Լ���
    //         ������ ������Ʈ���� ��Ȱ�� ��ٸ��ϴ�.
    //         ���� ���� �� �� ���� ���� �Ѵ� ����!
    //         ��� ���� ���� �ٽ� �ѷ��� ���� ��, �긦 �����ϰ� �ִ� �ְ� ���� �� �־��!
    //         �Ѿ��� Ǯ���ϰ� �־�� -> ��� ���� �Ѿ��� �ٸ����� ����;��!
    //         Ȯ���ϰ� ������ ������ ģ���� ����ϱ� ����!
    //         ť���ٰ� ����!

    //���� = Key�� �ش��ϴ� Value�� ã���ݴϴ�!
    // Key   Value
    // ���  ��������� ����.
    //                   Key,          Value
    //                  ������     ��⿭ �ν��Ͻ� 
    public static Dictionary<GameObject, Queue<GameObject>> disableQueue;

    public void InitializePool()
    {
        //����ó��
        //�̹� ����������ȴ�!
        if (disableQueue != null) return;

        Transform poolRoot = new GameObject("Pool Root").transform;

        //�ϴ� �����!
        disableQueue = new Dictionary<GameObject, Queue<GameObject>>();

        foreach(PoolInfo info in infoList)
        {
            //info.prefab
            //info.amount

            //�̹� �� ���������� �Ǿ��ִ� ������ �ִٸ� ���ؾ߰���!
            if (disableQueue.ContainsKey(info.prefab)) continue;

            Transform currentRoot = new GameObject(info.infoName).transform;
            currentRoot.SetParent(poolRoot);

            //��⿭ ������ ������ ������ ������ �ϰ���?
            Queue<GameObject> waitQueue = new Queue<GameObject>();

            //�����ϱ� �߰����ֱ�!
            disableQueue.Add(info.prefab, waitQueue);

            //������ŭ �����
            for(int i = 0; i < info.amount; i++)
            {
                //������ ����� �ڵ�!
                GameObject inst = Instantiate(info.prefab, currentRoot);
                //�ʸ� Ǯ ������Ʈ�� �Ӹ��ϸ�!                   ���� �θ��~!
                inst.AddComponent<PoolObject>().originPrefab = info.prefab;
                inst.SetActive(false);
                waitQueue.Enqueue(inst);
            };
        };
    }

    public static GameObject Instantiate(GameObject target)
    {
        //Ÿ���� ������ ���� �͵� ����!
        if (target == null) return null;

        //�̰� ������ �־��µ�?                                             ��� ����������!
        if (disableQueue != null && disableQueue.ContainsKey(target) && disableQueue[target].Count > 0)
        {
            //����� �� ��� ���ش�!
            GameObject result = disableQueue[target].Dequeue();

            result.SetActive(true);
            return result;
        }
        else
        {
            //�� �Լ� ���� ����!
            return GameObject.Instantiate(target);
        };
    }

    public static void Destroy(GameObject target)
    {
        PoolObject asPool = target.GetComponent<PoolObject>();

        //�̰� Ǯ���� ��ϵ� �༮���ݾ�?
        if (asPool && disableQueue != null && disableQueue.ContainsKey(asPool.originPrefab))
        {
            //���ݴϴ�!
            target.SetActive(false);
            //���� ��⿭�� ���� ������!
            disableQueue[asPool.originPrefab].Enqueue(target);
        }
        else
        {
            GameObject.Destroy(target);
        };
    }
}
