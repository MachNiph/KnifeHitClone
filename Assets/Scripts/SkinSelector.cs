using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinSelector : MonoBehaviour
{
    [SerializeField]
    private SelectData[] selectDatas;

    private void Start()
    {
        for (int i = 0; i < selectDatas.Length; i++)
        {
            selectDatas[i].button.onClick.AddListener(SelectKnife);
        }
    }

    public void SelectKnife()
    {

    }
}


[Serializable]
public class SelectData
{
    public Button button;
    public Sprite knifeSprite;
}

