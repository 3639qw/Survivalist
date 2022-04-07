using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actions : MonoBehaviour
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
            } // if (gm.isHunt)

            if (gm.isEat)
            {
                // 리턴 비정상 코드표
                // -1 갖고 있는 음식이 난수 값보다 작을 경우

                int i = act.EatFood(0);

                if (i == -1) // 니 음식 보다 난수가 더 큼 == 한마디로 니가 가지고 있는 음식이 부족함
                {
                    Debug.Log("음식 부족");
                    gm.food = 0; // 음식 양 초기화
                    gm.used_food += gm.food; // 먹은 음식의 양 History ++
                    ts.Hunger.value = Mathf.Clamp(ts.Hunger.value, 0, 100) + (gm.food * gm.act_food_ratio[0]);
                }
                else if (i > (100 - ts.Hunger.value)) // 난수값이 포만감 최대값을 넘어갈 경우 == 오버플로
                {
                    if (ts.Hunger.value != 100)
                    {
                        gm.food -= i; // 임의로 음식 차감
                        gm.used_food += i; // 먹은 음식의 양 History ++
                        ts.Hunger.value = 100;
                        Debug.Log("포만감 오버플로");
                    }
                }
                else if (ts.Hunger.value > 99) // 포만감 수치가 최대치 일경우
                {
                    gm.cooltime -= Time.deltaTime; // 강제로 시간을 까버린다.
                    Debug.Log("최대치");
                }
                else
                {
                    Debug.Log("정상");
                    gm.food -= i; // 음식 차감
                    gm.used_food += i; // 먹은 음식의 양 History ++
                    ts.Hunger.value += (i * gm.act_food_ratio[0]); // 먹은 음식에 비례하여 포만감 증가
                }
                
            } // if (gm.isEat)

            if (gm.isDrink)
            {
                // 리턴 비정상 코드표
                // -1 갖고있는 물이 난수 값보다 작을 경우
                
                int i = act.DrinkWater(0);

                if(i == -1) // 니 물보다 난수가 더큼 한마디로 니 물이 부족함
                {
                    Debug.Log("물 부족");
                    gm.water = 0; // 물의 양 초기화
                    gm.used_water += gm.water; // 마신 물의 양 History ++
                    ts.Moist.value = Mathf.Clamp(ts.Moist.value, 0, 100) + (gm.water * gm.act_food_ratio[1]); // 수분 추가
                }
                else if(i > (100 - ts.Moist.value)) // 난수값이 수분최대값을 넘어갈 경우 == 오버플로
                {
                    if(ts.Moist.value != 100)
                    {
                        gm.water -= i; // 임의로 물 차감
                        gm.used_water += i; // 마신 물의 양 History ++
                        ts.Moist.value = 100;
                        Debug.Log("수분감 오버플로");
                    }
                }
                else if(ts.Moist.value > 99) // 수분수치가 최대치 일경우
                {
                    gm.cooltime -= Time.deltaTime; // 강제로 시간을 까버린다.
                    Debug.Log("최대치");
                }
                else
                {
                    Debug.Log("정상");
                    gm.water -= i; // 물 차감
                    gm.used_water += i; // 마신 물의 양 History ++
                    ts.Moist.value += (i * gm.act_food_ratio[1]); // 마신 물에 비례하여 수분수치 증가
                }
            } // if (gm.isDrink)

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
