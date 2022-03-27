using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinking : MonoBehaviour
{
    static public Blinking instance = null;

    GameManager gm;
    TextSync ts;

    public GameObject btxt; // 깜박이는 오브젝트
    public GameObject notentxt; // 능력치 부족 txt
    float nontime; // 능력치 부족 시계
    bool nonfade; // 능력치 부족 Fade In Out
    float btime; // 깜박임 시계
    bool bfade = true; // Fade In Out

    float stime; 
    bool sfade = true; 

    public static Blinking Instance
    {
        get
        {
            if(instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        gm = GameManager.Instance;
        ts = TextSync.Instance;
    }


    public void Blink()
    {
        if (gm.isCooltime)
        {
            btxt.SetActive(false);
        }
        else if (!gm.isCooltime)
        {
            btxt.SetActive(bfade);
            btime += Time.deltaTime;
            if (bfade)
            {
                if (btime >= 1.25f)
                {
                    bfade = false;
                    btime = 0;
                }
            }
            else if (!bfade)
            {
                if (btime >= .7f)
                {
                    bfade = true;
                    btime = 0;
                }
            }
        }
    }

    public void SliderBlink(GameObject obj)
    {
        obj.SetActive(sfade);
        stime += Time.deltaTime;
        if (sfade)
        {
            if (stime >= .25f)
            {
                sfade = false;
                stime = 0;
            }
        }
        else if (!sfade)
        {
            if (stime >= .25f)
            {
                sfade = true;
                stime = 0;
            }
        }
    }

    public void BlinkNotEnough()
    {
        notentxt.SetActive(bfade);
        nontime += Time.deltaTime;
        if (nonfade)
        {
            if (nontime >= 1.25f)
            {
                nonfade = false;
                nontime = 0;
            }
        }
        else if (!nonfade)
        {
            if (nontime >= .7f)
            {
                nonfade = true;
                nontime = 0;
                notentxt.SetActive(false);
            }
        }
    }




}
