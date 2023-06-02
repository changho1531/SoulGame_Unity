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
        //이미지 바꾸고 싶으시면 스프라이트!
        Sprite wantSprite;
        //targetImage.sprite
        //                         빨강, 초록, 파랑, 반투명
        targetImage.color = new Color(1,0,0,1);
        //Color.green;
    }
}
