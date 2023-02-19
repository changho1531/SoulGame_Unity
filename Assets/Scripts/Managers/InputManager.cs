using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    //�Է��̶�� �ϴ� �� ��� �����Ǿ��ִ°�?
    [System.Serializable]
    public class InputStruct
    {
        public KeyType keyType;
        public KeyCode Code;
    }
    public InputStruct[] inputs;

    protected static byte[] keyValues  = new byte[(int)KeyType.Lenght];
    protected bool[] keyDown    = new bool[(int)KeyType.Lenght];
    protected bool[] keyUp      = new bool[(int)KeyType.Lenght];

    //                                       ���� Ű�� 1�� �̻� ������ �ִ� ���
    public static bool GetKey(KeyType wantKey) { return keyValues[(int)wantKey] > 0; }

    public bool GetKeyDown(KeyType wantKey) { return keyDown[(int)wantKey]; }
    public bool GetKeyUp(KeyType wantKey) { return keyUp[(int)wantKey]; }
    void Update()
    {
        //���� �����ӿ� �����ų�, �������ٴ� �ҽ��� ���� ����� ����
        for(int i = 0; i < keyValues.Length; i++)
        {
            keyDown[i] = keyUp[i] = false;
        }
        
        //inputs �Է��� ����� �ֵ�
        //�ش� ģ������ ���� �����ֱ�
        //��� ����
        //�ݿ� �ִ� ģ������ ��� Ȯ���Ѵٰ� ������ "���� ���"�̶�� �ϴ� �̸����� �θ���?
        //            ���� ���� ģ��  �ȿ� inputs
        foreach (InputStruct current in inputs) 
        {
            //���� ���� �ִ� ģ�� �ȿ��� keycode�� ������� �ſ���
            //����Ƽ�� Input �̶�� �ϴ� ����� ���ؼ� �Է��� �������ݴϴ�
            //���ϴ� Ű�ڵ尡 ��� ���ȳ���?
            if (Input.GetKeyDown(current.Code))
            {
                //ŰŸ���� ���ڷ� ��ȯ : front(0) back(1)
                //�ش� ĭ�� �ϳ� �߰�
                keyValues[(int)current.keyType]++;
                //�������ϱ� ������ ������
                keyDown[(int)current.keyType] = true;
            }
            else if (Input.GetKeyUp(current.Code)) //���� ���� �� ����?
            {
                keyValues[(int)current.keyType]--;
                //���������ϱ� ������ ����
                keyUp[(int)current.keyType] = true;

            }
        }
    }
}
