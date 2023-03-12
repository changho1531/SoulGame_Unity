using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//                         �� ��ӹ޴� ��!
//                         �̰� ��ӹ����� ������Ʈ�� �߰��Ͻ� �� �ֽ��ϴ�!
public class GameManager : MonoBehaviour
{
    // ���ӸŴ�����.. ���� ��ü�� �����ϴ� ģ��..
    // ���� ĳ������ ������ �� ����!
    // �ν��Ͻ����� �ִ� ���� �ƴ϶�, Ŭ������ �� �ٿ����� ����� ������ ����ߴ���!
    // Static�̶�� �ϴ� ���� �־����ϴ�!
    // ���� ��ü�� �����ϴ� ģ���ϱ� �� �ϳ��� �ν��Ͻ��� �־�� �մϴ�.
    // �̱����̶�� �ҷ���!
    // ���� ��������ִ� ģ���� Ȯ���غ��� ����!
    // ������ ���¸� �����Ѵٸ� �׿����� �ſ���!
    // Ŭ�������� ���� �޼ҵ� ������Ƽ(�� �� ��!)
    // ������ �� �� �ִ� ��
    // Debug.Log(a); ���� ����!  get
    // a = 3;        ���� ����!  set
    // get�� set�� �� ������� �ٲٱ�!
    // ������ ���ų� �ٲ� ���� �߰����� ��� �߰�!
    //                           Instance�� ����� instance��
    //                           �ڵ��ϼ����� ������ �ʰ� _�� �־��ݽô�!
    protected static GameManager _instance;
    public static GameManager Instance
    {
        //get�� ���� ���� �; �� ģ������!
        //Debug.Log(Instance)ó��!
        //���� ��������� �ϰ���! �޼ҵ忡�� ���� �����ϴ� ����� return!
        get
        {
            //���ӸŴ����� ã�� �ڰ� �־��ٸ�.. ����� "����"�� �˷��ָ� �ȵ˴ϴ�!
            if(_instance == null)
            {
                //���� �̹� �÷����� ���ӸŴ����� ���� �� ���� �������?
                //����Ƽ���� ���� ������ �ִ� �ν��Ͻ��� ã�� �� �ִ� ���!
                //Start�� ����Ǳ� ���� �ٲٷ��� �Ѵٸ�!
                //�׷� �ϴ� ã�ƺ�!
                _instance = FindObjectOfType<GameManager>();

                //ã�Ҵµ��� ����? �׷�.. ��;���..
                if (_instance == null)
                {
                    //���ο� ���ӸŴ����� �ø����� ���ӸŴ����� "������Ʈ"�̴ϱ�
                    //������Ʈ�� "���ӿ�����Ʈ" ���� �ö󰡾� �ؿ�!
                    //                                 �̸� ������ ����!
                    GameObject manager = new GameObject("GameManager");
                    //���� ������Ʈ �ȿ� ���ο� ������Ʈ�� "�ڵ�"�� ���ؼ� �ִ� ���!
                    _instance = manager.AddComponent<GameManager>();
                };
            };
            return _instance;
        }
        //protected : �� ģ����, �� ģ���� ��ӹ޴� �ڽĸ� �� �� ����!
        protected set  //Instance = null
        {
            _instance = value;
        }
    }

    //�÷��̾��.. �ϳ��� �ƴ� ���� ���� ������?
    protected static List<ControllerBase> players = new List<ControllerBase>();

    public static void PlayerAdd(ControllerBase newPlayer) { players.Add(newPlayer); }
    public static void PlayerRemove(ControllerBase target) { players.Remove(target); }
    //����Ʈ�� "���"�̶�� �߾��! �迭�̶� �Ȱ��ƿ�!
    public static ControllerBase PlayerFind(int index = 0)
    {
        if (index < 0 || index >= players.Count) return null;

        return players[index];
    }
    
    void Start()
    {
        //���¿� �������� �õ��ϴ� �ſ���!
        //���� ������� �ν��Ͻ��� ���ٸ�?
        //�׷� ���� ���� �� �����ΰ�?

        //�ʳװ� ã�� ���� ������? �ٷ� ����!
        //Instance = value; �̰� Instance�� set�� �θ��� �Ǵϱ�!
        //                  �׷��� �Ǹ� ���� �ݺ��ϰ� �˴ϴ�!
        //                  �׷��� Instance�� set���ʿ�����
        //                  Instance�� �����ϴ� ���� ���� �� ����
        //                  �׷��� �ڿ������� "����"����� �Ұ��������� �̴ϴ�!
        //get�̳� set�� ����� ������ �������� ���!
        //Instance�� �ٲ۴ٴ� �� _instance�� �ٲ۴ٴ� ����!
        //set�� "����"�� ���� ������ ģ���ϱ�! �����Ϸ��� ����� value�� �־����!
        if (Instance != this)
        {
            //��.. ���� �ִٰ�..?
            //��������! �߸� �Գ��δ�!
            //�� �ν��Ͻ��� �ı��Ǿ�� �ؿ�!
            Destroy(this);
        };
    }

    // ������ �� ���� �������� ������ �Ű澲��?
    // 30Fps�̶�� �ϸ� ����ȭ ��ȯ��..
    // 1�ʿ� �� �� ����ϴ°�? ������!
    // ������Ʈ��� �ϴ� ģ���� �����Ӹ��� �� ���� �̾߱��մϴ�!
    void Update()
    {
    }
}
