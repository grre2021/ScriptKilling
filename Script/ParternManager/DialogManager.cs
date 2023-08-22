using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DialogManager : Singleton<DialogManager>
{
    public TextAsset textAsset;

    public TMPro.TextMeshProUGUI textName;

    public TMPro.TextMeshProUGUI textContent;

    private string[] dialogRows;

    private int dialogIndex;

    private void Start()
    {
        ReadAsset(textAsset);       
    }


    public void UpdateTextAsset(TextAsset _textAsset)
    {
        if (_textAsset == null) return;
        textAsset= _textAsset;
    }

    public void UpdateText(string _name,string _content)
    {
        textName.text= _name;
        textContent.text= _content;
    }

    void ReadAsset(TextAsset _textAsset)
    {
        dialogRows = _textAsset.text.Split('\n');      
    }

    public void ShowText()
    {
        if (dialogRows == null) return;
        foreach(string row in dialogRows)
        {
            string[] cell= row.Split(',');
            //表示对话标志
            if(cell[0]=="#"&&int.Parse(cell[1])==dialogIndex)
            {
                UpdateText(cell[3],cell[2]);
                dialogIndex++;
                break;
            }
        }
    }
}
