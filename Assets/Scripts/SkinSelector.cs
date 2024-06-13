using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinSelector : MonoBehaviour
{
    [SerializeField]
    private SelectData[] selectDatas;
    [SerializeField]
    public Image selectedImage;

    private void Start()
    {
        for (int i = 0; i < selectDatas.Length; i++)
        {
            int index = i;
            SelectData data = selectDatas[i];
            data.button.onClick.AddListener(()=>SelectKnife(index));
        }
    }

    public void SelectKnife(int index)
    {
        selectedImage.sprite = selectDatas[index].knifeSprite;
        PlayerPrefs.SetInt("knifeIndex", index);
    }
}


[Serializable]
public class SelectData
{
    public Button button;
    public Sprite knifeSprite;
}

