using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//텍스트 메시 프로를 사용하는 네임스페이스!
using TMPro;

public class TestTextMeshPro : MonoBehaviour
{
    public TextMeshProUGUI targetText;
    void Start()
    {
        targetText.text = "바뀌었다!";
    }
}
