using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//struct
[System.Serializable]
public struct PoolInfo
{
    //맨 위 스트링에 이름을 적용!
    public string infoName;

    public GameObject prefab;
    public int amount;
}

public class PoolManager : MonoBehaviour
{
    //Object Pooling
    //오브젝트를 파괴하는 순간에 실제로 파괴하지 않고, 비활성화 시켜놓는 것

    //Instantiate : 기존에 있는 오브젝트를 복사해서 새로운 내용을 만든다.
    //               -> 가장 먼저 꺼진 오브젝트를 다시 켠다.
    //Destroy     : 부순다!
    //               -> 대상이 되는 오브젝트를 끈다.

    //풀링의 이점 : 새로 만들거나 부수는 과정이 없어서 굉장히 빠르다
    //           : 만드는 대신 켜주는 전제조건 > 미리 만들어둬야 합니다!
    //                                         메모리에 할당한다.
    //                                             로딩

    //목록 : 나열되어 있는 것!
    //       그래서 뭐만듦?
    //풀링을 할 정보를 미리 리스트로 따놓는 겁니다!
    //배열이랑 다른 점은 중간에 추가할 수 있다는 겁니다!
    //리스트는 배열보다 탐색이 느려요!
    //배열은 추가가 안돼요!
    //실시간으로 무언가 바꾸는 일이 있다면 리스트!
    public List<PoolInfo> infoList = new List<PoolInfo>();

    // Queue : 큐 = 대기열   ->  선입선출
    //         꺼놓은 오브젝트들이 부활을 기다립니다.
    //         제일 먼저 끈 걸 제일 먼저 켜는 이유!
    //         방금 꺼진 것은 다시 켜려고 했을 때, 얘를 참조하고 있는 애가 있을 수 있어요!
    //         총알을 풀링하고 있어요 -> 방금 맞은 총알을 다른데서 쓰고싶어요!
    //         확실하게 참조가 없어진 친구를 사용하기 위해!
    //         큐에다가 저장!

    //사전 = Key에 해당하는 Value를 찾아줍니다!
    // Key   Value
    // 사과  사과나무의 열매.
    //                   Key,          Value
    //                  프리팹     대기열 인스턴스 
    public static Dictionary<GameObject, Queue<GameObject>> disableQueue;
    //풀링용 오브젝트의 부모를 확인해보죠!
    public static Dictionary<GameObject, Transform> rootDictionary = new Dictionary<GameObject, Transform>();

    //인스턴스를 확인하는 용도만!
    protected static PoolManager _instance;
    public static PoolManager Instance
    {
        get
        {
            //없는데?
            if(_instance == null)
            {
                //찾아봐!
                _instance = FindObjectOfType<PoolManager>();
                //진짜 없다니까?
                if(_instance == null)
                {
                    //그럼 만들어야지!
                    GameObject obj = new GameObject("PoolManager");
                    _instance = obj.AddComponent<PoolManager>();
                };
            }
            return _instance;
        }
    }      

    public void InitializePool()
    {
        //예외처리
        //이미 만들어져버렸다!
        if (disableQueue != null) return;

        Transform poolRoot = new GameObject("Pool Root").transform;

        //일단 만들고!
        disableQueue = new Dictionary<GameObject, Queue<GameObject>>();

        foreach(PoolInfo info in infoList)
        {
            //info.prefab
            //info.amount

            //이미 이 프리팹으로 되어있는 내용이 있다면 안해야겠죠!
            if (disableQueue.ContainsKey(info.prefab)) continue;

            Transform currentRoot = new GameObject(info.infoName).transform;
            currentRoot.SetParent(poolRoot);

            //대기열 서버를 만들어야 유저가 접속을 하겠죠?
            Queue<GameObject> waitQueue = new Queue<GameObject>();

            //부모를 미리 등록해놓기!
            rootDictionary.Add(info.prefab, currentRoot);
            //없으니까 추가해주기!
            disableQueue.Add(info.prefab, waitQueue);

            //개수만큼 만들고
            for(int i = 0; i < info.amount; i++)
            {
                //실제로 만드는 코드!
                GameObject inst = Instantiate(info.prefab, currentRoot);
                //너를 풀 오브젝트로 임명하마!                   너의 부모다~!
                inst.AddComponent<PoolObject>().originPrefab = info.prefab;
                inst.SetActive(false);
                waitQueue.Enqueue(inst);
            };
        };
    }

    public static GameObject Instantiate(GameObject target)
    {
        //타깃이 없으면 만들 것도 없음!
        if (target == null) return null;

        //이거 사전에 있었는데?                                             재고가 남아있으면!
        if (disableQueue != null && disableQueue.ContainsKey(target) && disableQueue[target].Count > 0)
        {
            //만드는 것 대신 켜준다!
            GameObject result = disableQueue[target].Dequeue();

            result.SetActive(true);
            return result;
        }
        else
        {
            //이 함수 말고 원본!
            return GameObject.Instantiate(target);
        };
    }

    public static void Destroy(GameObject target)
    {
        PoolObject asPool = target.GetComponent<PoolObject>();

        //이거 풀링에 등록된 녀석이잖아?
        if (asPool && disableQueue != null && disableQueue.ContainsKey(asPool.originPrefab))
        {
            //꺼줍니다!
            target.SetActive(false);
            //꺼준 대기열에 들어가는 것이죠!
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
        //코루틴은 항상 스타트코루틴으로 하셔야 해요!
        Instance.StartCoroutine(Instance.DestroyCoroutine(target, wantTime));
    }

    //코루틴입니다.
    //스크립트가 돌고 있을 때, 동시에 같이 도는 친구!
    //게임 도는데 옆에서 따로 할 일을 하고 있는 거예요!
    //업-업-업-업-업-업-업-업
    //업-코-업-코-업-코-업-코
    //그래서 조심해서 쓰시고 최대한 안 쓰시고...
    //근데 아주 편해요..ㅎㅎ
    //아주 직관적입니다..
    //반환값이 들어가는 자리! 이걸 진짜로 반환해줘요!
    //IEnumerator를 StartCoroutine으로 돌리는 식!
    //코루틴을 관리하시려면 이렇게 저장해놓으시면 돼요!
    //IEnumerator currentCoroutine = DestroyCoroutine(null, 3);
    IEnumerator DestroyCoroutine(GameObject target, float wantTime)
    {
        //원하는 시간만큼 코드를 "기다려" 줄 수 있습니다!
        //"양보"하라니?
        //yield return은 함수가 끝나지 않습니다!
        //기다렸다가 다음 코드를 실행하는 용도로 쓰시는 거예요!
        yield return new WaitForSeconds(wantTime);
        //그래서 그 시간 기다리고 Destroy실행하기!
        PoolManager.Destroy(target);
    }
}
