using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    //입력이라고 하는 건 어떻게 구성되어있는가?
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

    //                                       현재 키가 1개 이상 눌려져 있는 경우
    public static bool GetKey(KeyType wantKey) { return keyValues[(int)wantKey] > 0; }

    public bool GetKeyDown(KeyType wantKey) { return keyDown[(int)wantKey]; }
    public bool GetKeyUp(KeyType wantKey) { return keyUp[(int)wantKey]; }
    void Update()
    {
        //직전 프레임에 눌리거나, 떼어졌다는 소식은 전부 지우고 시작
        for(int i = 0; i < keyValues.Length; i++)
        {
            keyDown[i] = keyUp[i] = false;
        }
        
        //inputs 입력을 써놓은 애들
        //해당 친구들을 전부 돌아주기
        //모두 돌기
        //반에 있는 친구들을 모두 확인한다고 했을때 "다음 사람"이라고 하는 이름으로 부르죠?
        //            지금 보는 친구  안에 inputs
        foreach (InputStruct current in inputs) 
        {
            //지금 보고 있는 친구 안에는 keycode가 들어있을 거예요
            //유니티는 Input 이라고 하는 기능을 통해서 입력을 전달해줍니다
            //원하는 키코드가 방금 눌렸나요?
            if (Input.GetKeyDown(current.Code))
            {
                //키타입을 숫자로 변환 : front(0) back(1)
                //해당 칸을 하나 추가
                keyValues[(int)current.keyType]++;
                //눌렸으니까 눌림이 켜지고
                keyDown[(int)current.keyType] = true;
            }
            else if (Input.GetKeyUp(current.Code)) //지금 손을 뗏 나요?
            {
                keyValues[(int)current.keyType]--;
                //떼어졌으니까 떼짐이 켜짐
                keyUp[(int)current.keyType] = true;

            }
        }
    }
}
