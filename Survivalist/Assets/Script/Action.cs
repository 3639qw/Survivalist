using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour
{
    GameManager gm;
    Act_function act;
    TextSync ts;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
        act = Act_function.Instance;
        ts = TextSync.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.isCooltime) // 쿨타임 걸렸을때 행동개시
        {

            if (gm.isGetsWater)
            {
                gm.water += act.GetWater(0);
            }

            if (gm.isForage)
            {
                gm.food += act.GoForage();
            }

            if (gm.isHunt)
            {
                int count = act.GoHunt(0);
                if (count == 999)
                {
                    gm.cooltime -= Time.deltaTime;
                }
                else
                {
                    gm.food += count;
                }
            }

            if (gm.isEat)
            {
                int i = act.EatFood(0);
                if (gm.food >= i)
                {
                    gm.food -= i;
                    if (i != 0)
                    {
                        ts.Hunger.value = Mathf.Clamp(ts.Hunger.value, 0, 100) + (i * gm.act_food_ratio[0]); // 포만감 설정
                    }
                }
                else
                {
                    gm.cooltime -= Time.deltaTime;
                }
            }

            if (gm.isDrink)
            {
                int i = act.DrinkWater(0);
                if (gm.water >= i)
                {
                    gm.water -= i;
                    if (i != 0)
                    {
                        ts.Moist.value = Mathf.Clamp(ts.Moist.value, 0, 100) + (i * gm.act_food_ratio[1]); // 갈증 설정
                    }
                }
            }

            if (gm.isMaterial)
            {

            }

            if (gm.isSleep)
            {
                int i = act.Sleep();
                if(i == 999) // 능력치가 모자라
                {
                    gm.cooltime -= Time.deltaTime;
                }else if(i == 998) // 피로도 능력치 0까지 하락 했으면 
                {
                    ts.Hardest.value = 0;
                    gm.cooltime -= Time.deltaTime;
                }
                else
                {
                    ts.Hardest.value -= i;
                }
            }

        }
    }
}
