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
    //Ǯ���� ������Ʈ�� �θ� Ȯ���غ���!
    public static Dictionary<GameObject, Transform> rootDictionary = new Dictionary<GameObject, Transform>();

    //�ν��Ͻ��� Ȯ���ϴ� �뵵��!
    protected static PoolManager _instance;
    public static PoolManager Instance
    {
        get
        {
            //���µ�?
            if(_instance == null)
            {
                //ã�ƺ�!
                _instance = FindObjectOfType<PoolManager>();
                //��¥ ���ٴϱ�?
                if(_instance == null)
                {
                    //�׷� ��������!
                    GameObject obj = new GameObject("PoolManager");
                    _instance = obj.AddComponent<PoolManager>();
                };
            }
            return _instance;
        }
    }      

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

            //�θ� �̸� ����س���!
            rootDictionary.Add(info.prefab, currentRoot);
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
            target.transform.SetParent(rootDictionary[asPool.originPrefab]);
        }
        else
        {
            GameObject.Destroy(target);
        };
    }

    public static void Destroy(GameObject target, float wantTime)
    {
        //�ڷ�ƾ�� �׻� ��ŸƮ�ڷ�ƾ���� �ϼž� �ؿ�!
        Instance.StartCoroutine(Instance.DestroyCoroutine(target, wantTime));
    }

    //�ڷ�ƾ�Դϴ�.
    //��ũ��Ʈ�� ���� ���� ��, ���ÿ� ���� ���� ģ��!
    //���� ���µ� ������ ���� �� ���� �ϰ� �ִ� �ſ���!
    //��-��-��-��-��-��-��-��
    //��-��-��-��-��-��-��-��
    //�׷��� �����ؼ� ���ð� �ִ��� �� ���ð�...
    //�ٵ� ���� ���ؿ�..����
    //���� �������Դϴ�..
    //��ȯ���� ���� �ڸ�! �̰� ��¥�� ��ȯ�����!
    //IEnumerator�� StartCoroutine���� ������ ��!
    //�ڷ�ƾ�� �����Ͻ÷��� �̷��� �����س����ø� �ſ�!
    //IEnumerator currentCoroutine = DestroyCoroutine(null, 3);
    IEnumerator DestroyCoroutine(GameObject target, float wantTime)
    {
        //���ϴ� �ð���ŭ �ڵ带 "��ٷ�" �� �� �ֽ��ϴ�!
        //"�纸"�϶��?
        //yield return�� �Լ��� ������ �ʽ��ϴ�!
        //��ٷȴٰ� ���� �ڵ带 �����ϴ� �뵵�� ���ô� �ſ���!
        yield return new WaitForSeconds(wantTime);
        //�׷��� �� �ð� ��ٸ��� Destroy�����ϱ�!
        PoolManager.Destroy(target);
    }
}
