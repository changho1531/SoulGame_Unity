using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestImage : MonoBehaviour
{
    public Image targetImage;

    void Start()
    {
        
    }

    void Update()
    {
        FloatBar bar = new FloatBar();
        bar.Max = 30;
        bar.Current = 12;
        targetImage.fillAmount = bar.Ratio;
        //targetImage.fillAmount = GameManager.PlayerFind().GetCharacter().health.Ratio;
        //�̹��� �ٲٰ� �����ø� ��������Ʈ!
        Sprite wantSprite;
        //targetImage.sprite
        //                         ����, �ʷ�, �Ķ�, ������
        targetImage.color = new Color(1,0,0,1);
        //Color.green;
    }
}
