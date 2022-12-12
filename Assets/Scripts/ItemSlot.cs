using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public GameObject flower0, flower1, flower2, flower3, flower4, flower5, water;
    public GameObject restart, gameovertext;
    private int watercount;
    public TextMeshProUGUI timer;
    private DateTime StringToDate(string value) => DateTime.Parse(value);
    private string DateToString(DateTime value) => value.ToString();

    public void OnDrop(PointerEventData eventData)
    {
        if (PlayerPrefs.HasKey("water"))
        {
            watercount = PlayerPrefs.GetInt("water");
            watercount++;
            PlayerPrefs.SetInt("water", watercount);
        }
        else
        {
            watercount++;
            PlayerPrefs.SetInt("water", watercount);
        }
        var now = DateTime.Now;
        var textDate = DateToString(now);
        PlayerPrefs.SetString("recentDate", textDate);
    }

    public int CanHydrate()
    {
        if (!PlayerPrefs.HasKey("recentDate")) return 1;
        var recent = PlayerPrefs.GetString("recentDate");
        var recentDate = StringToDate(recent);
        var dateDiff = DateTime.Now.Subtract(recentDate);
        if (dateDiff.Seconds >= 5) return 1;
        return 0;

    }
    public int DeadFlower()
    {
        if (!PlayerPrefs.HasKey("recentDate")) return 0;
        var recent = PlayerPrefs.GetString("recentDate");
        var recentDate = StringToDate(recent);
        var dateDiff = DateTime.Now.Subtract(recentDate);
        if (dateDiff.Seconds >= 10) return 1;
        return 0;
    }
    private string RecentDateToString()
    {
        var limit = TimeSpan.FromSeconds(5); // 2 hours
        var recent = PlayerPrefs.GetString("recentDate");
        var recentDate = StringToDate(recent);
        var dateDiff = DateTime.Now.Subtract(recentDate); // 0 hours 10 minutes 0 seconds
        var nextDate = limit.Subtract(dateDiff); // 1 hours 50 minutes 0 seconds
        var ndTicks = Mathf.Clamp(nextDate.Ticks, TimeSpan.Zero.Ticks, TimeSpan.MaxValue.Ticks);
        nextDate = TimeSpan.FromTicks((long)ndTicks);
        return $"{nextDate.Hours} saat, {nextDate.Minutes} dakika, {nextDate.Seconds} saniye";
    }
    private void Start() {
        if (CanHydrate()==1)
        {
            water.GetComponent<DragDrop>().enabled = true;
            timer.text="Çiçeği Sula";
        }
        if (PlayerPrefs.HasKey("recentDate")&&CanHydrate()==0)
        {
            timer.text = RecentDateToString();
            water.GetComponent<DragDrop>().enabled=false;
        }   
    }
    private void Update()
    {
        PlayerPrefs.SetInt("gameover",DeadFlower());
        PlayerPrefs.SetInt("canhydrate",CanHydrate());
        if (PlayerPrefs.GetInt("canhydrate")==1)
        {
            water.GetComponent<DragDrop>().enabled = true;
            timer.text="Çiçeği Sula";
        }
        if (PlayerPrefs.HasKey("recentDate")&&PlayerPrefs.GetInt("canhydrate")==0)
        {
            timer.text = RecentDateToString();
        }
        if (PlayerPrefs.GetInt("water") == 2)
        {
            flower0.SetActive(false);
            flower1.SetActive(true);
        }
        if (PlayerPrefs.GetInt("water") == 4)
        {
            flower1.SetActive(false);
            flower2.SetActive(true);
        }
        if (PlayerPrefs.GetInt("water") == 6)
        {
            flower2.SetActive(false);
            flower3.SetActive(true);
        }
        if (PlayerPrefs.GetInt("water") == 8)
        {
            flower3.SetActive(false);
            flower4.SetActive(true);
        }
        if (PlayerPrefs.GetInt("gameover")==1)
            {
                if(PlayerPrefs.GetInt("water")>=6){
                flower3.SetActive(false);
                flower4.SetActive(false);
                flower5.SetActive(true);
                water.GetComponent<DragDrop>().enabled = false;
                gameovertext.SetActive(true);
                restart.SetActive(true);
                }
                else{
                water.GetComponent<DragDrop>().enabled = false;
                gameovertext.SetActive(true);
                restart.SetActive(true);
                }
                
            }
    }
    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
        PlayerPrefs.DeleteAll();
    }
}
