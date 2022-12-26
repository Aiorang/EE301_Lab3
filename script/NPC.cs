using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public GameObject Button;   //按钮提示
    public GameObject talkUI;   //对话框

    public string NPCname;
    public int indexRecord;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        Button.SetActive(true);
        //load();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Button.SetActive(false);
        talkUI.SetActive(false);
        // save();
    }

    public int GetIndexRecord()
    {
        return indexRecord;
    }

    public int SetIndexRecord()
    {
        indexRecord++;
        return indexRecord;
    }

    void Update()
    {
        if (Button.activeSelf && Input.GetKeyDown(KeyCode.F))
        {
            talkUI.SetActive(true);
        }
    }
    #region PlayerPrefs

    void SaveByPlayerPrefs()
    {
        PlayerPrefs.SetInt(NPCname, indexRecord);
        PlayerPrefs.Save();
    }

    void LoadFromPlayerPrefs()
    {
        indexRecord = PlayerPrefs.GetInt(NPCname);
    }

    #endregion

    public void save()
    {
        SaveByPlayerPrefs();
    }
    public void load()
    {
        LoadFromPlayerPrefs();
    }
}
