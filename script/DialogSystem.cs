using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    [Header("UI���")]
    public Image headImage;
    public Text textLabel;

    [Header("�ı��ļ�")]
    public TextAsset textFile;
    public int index;
    public float textSpeed;

    [Header("ͷ��")]
    public Sprite headPlayer;
    public Sprite headNPC;

    bool textFinished;  //�ı��Ƿ���ʾ���
    bool isTyping;  //�Ƿ���������ʾ

    List<string> textList = new List<string>();

    void Awake()
    {
        GetTextFromFile(textFile);
    }

    void OnEnable()
    {
        index = 0;  //�Ի���ÿ�����ر�Ϊ��ʾ�����öԻ�
        textFinished = true;    //�Ի���ÿ�����ر�Ϊ��ʾ״̬��Ϊ�ı��ѽ���
        StartCoroutine(setTextUI());
    }

    void Update()
    {
        //�������F�����ҶԻ�ȫ��������رնԻ���
        if (Input.GetKeyDown(KeyCode.F) && index == textList.Count)
        {
            gameObject.SetActive(false);
            return;
        }

        //����F������ǰ���ı���ɾ�ִ��Э�̣���ǰ���ı�δ��ɾ�ֱ����ʾ��ǰ���ı�
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (textFinished)
            {
                StartCoroutine(setTextUI());
            }
            else if (!textFinished)
            {
                isTyping = false;
            }
        }
    }

    void GetTextFromFile(TextAsset file)
    {
        //����ı�����
        textList.Clear();

        //�и��ı��ļ�����Ȼ��һ��һ�мӵ�list������
        var lineDate = file.text.Split('\n');
        foreach (var line in lineDate)
        {
            textList.Add(line);
        }
    }

    IEnumerator setTextUI()
    {
        textFinished = false;   //����������ʾ״̬
        textLabel.text = "";    //�����ı�����

        //�ж��ı��ļ��������
        switch (textList[index].Trim())
        {
            case "A":
                headImage.sprite = headPlayer;
                index++;
                break;
            case "B":
                headImage.sprite = headNPC;
                index++;
                break;
        }

        //ÿ��һ��F������һ������
        int word = 0;
        while (isTyping && word < textList[index].Length - 1)
        {
            //������ʾ
            textLabel.text += textList[index][word];
            word++;
            yield return new WaitForSeconds(textSpeed);
        }
        //������ʾ�ı�����Ϊ��������
        textLabel.text = textList[index];

        isTyping = true;
        textFinished = true;
        index++;
    }
}
