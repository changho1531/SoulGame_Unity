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
        public KeyCode code;
    }
    public InputStruct[] inputs;
    public static Vector3 movement = Vector3.zero;

    protected static byte[] keyValues = new byte[(int)KeyType.Length];

    protected static bool[] keyDown   = new bool[(int)KeyType.Length];
    protected static bool[] keyUp     = new bool[(int)KeyType.Length];


    //                                             ���� Ű�� 1�� �̻� �������ִ� ���!
    public static bool GetKey(KeyType wantKey)     { return keyValues[(int)wantKey] > 0; }

    public static bool GetKeyDown(KeyType wantKey) { return keyDown[(int)wantKey]; }
    public static bool GetKeyUp(KeyType wantKey)   { return keyUp[(int)wantKey]; }

    void Update()
    {
        //���� �����ӿ� �����ų�, �������ٴ� �ҽ��� ���� ����� �����ϱ�!
        for(int i = 0; i < keyValues.Length; i++)
        {
            keyDown[i] = keyUp[i] = false;
        };

        //inputs �Է��� ����� �ֵ�!
        //�ش� ģ������ ���� �����ֱ�!
        //��� ����!
        //�ݿ� �ִ� ģ������ ��� Ȯ���Ѵٰ� ���� ��! "���� ���"�̶�� �ϴ� �̸����� �θ���?
        //         ���� ���� ģ��!  �ȿ� inputs
        foreach(InputStruct current in inputs)
        {
            //���� ���� �ִ� ģ�� �ȿ��� keycode�� ������� �ſ���!
            //����Ƽ�� Input �̶�� �ϴ� ����� ���ؼ� �Է��� �������ݴϴ�!
            //���ϴ� Ű�ڵ尡 ��� ���ȳ���?
            if(Input.GetKeyDown(current.code))
            {
                //ŰŸ���� ���ڷ� ��ȯ : front(0) back(1)
                //�ش� ĭ�� �ϳ� �߰�!
                keyValues[(int)current.keyType]++;
                //�������ϱ� ������ ������!
                keyDown[(int)current.keyType] = true;
            }
            else if(Input.GetKeyUp(current.code)) //����.. ���� �� �ų�?
            {
                keyValues[(int)current.keyType]--;
                //���������ϱ� ������ ������!
                keyUp[(int)current.keyType] = true;
            };
        };

        //�������� ����غ��ô�!
        //�ϴ� 0���� �������!
        movement = Vector2.zero;

        if (GetKey(KeyType.Front)) movement.z++;
        if (GetKey(KeyType.Back))  movement.z--;

        if (GetKey(KeyType.Right)) movement.x++;
        if (GetKey(KeyType.Left))  movement.x--;

        //x�൵ �ֱ� ������, y�൵ ���� ������ ��!
        //x�ุ �����̸� ������ �Ÿ��� 1�Դϴ�!
        //y�൵ �����̸� ������ �Ÿ��� ��2�Դϴ�!
        //movement�� �׻� �Ÿ��� 1�� ��������� �ؿ�!
        //���ϴ� �ӵ��� ������ ��, �� �ӵ��� �״�� �������� �� ���� �ſ���!
        //���⸸ ����� ���! Normalize() ����ȭ��� �ϴ� ���Դϴ�!
        movement.Normalize();
    }
}
