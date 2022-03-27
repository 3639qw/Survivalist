using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Act_function : MonoBehaviour
{
    static public Act_function instance = null;

    GameManager gm;
    TextSync ts;

    public static Act_function Instance
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


    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
        ts = TextSync.Instance;
    }



    public int GetWater(int firewood) // 물 얻으러
    {
        if (gm.cooltime > 0)
        {
            gm.act_sec += Time.deltaTime;
            gm.cooltime -= Time.deltaTime;
        }
        else if (gm.cooltime <= 0)
        {
            gm.isCooltime = false;
            gm.isGetsWater = false;
        }

        if (gm.act_sec >= 1f) // 실행
        {
            int isSuccess = Gacha(0, gm.act_success[0]); // 성공 확률 가챠 난수 생성
            int ran = Gacha(gm.act_getwater_range[0], gm.act_getwater_range[1]); // 아이템 획득 가챠 난수 생성
            
            if(isSuccess != 0) // 0이 아니면 성공
            {
                if (ts.Energy.value > (ran * gm.act_getwater[0]) && ts.Hunger.value > (ran * gm.act_getwater[1]) && ts.Moist.value > (ran * gm.act_getwater[2]) && (100 - ts.Hardest.value) > (ran * gm.act_getwater[3])) // 능력 감소 전 체크
                {
                    // 실질적인 탐험 성공
                    ts.Energy.value -= (ran * gm.act_getwater[0]);
                    ts.Hunger.value -= (ran * gm.act_getwater[1]);
                    ts.Moist.value -= (ran * gm.act_getwater[2]);
                    ts.Hardest.value += (ran * gm.act_getwater[3]);
                }
                else
                {
                    // 능력치 부족
                    Debug.Log("능력치가 모자라..");
                    return 0;
                }
                gm.act_sec = 0; // 초시계 초기화
                Debug.Log(ran);
            }
            else if(isSuccess == 0) // 0이면 실패
            {
                if (ts.Energy.value > (gm.act_vain[0] * gm.act_getwater[0]) && ts.Hunger.value > (gm.act_vain[0] * gm.act_getwater[1]) && ts.Moist.value > (gm.act_vain[0] * gm.act_getwater[2]) && (100 - ts.Hardest.value) > (gm.act_vain[0] * gm.act_getwater[3])) // 능력 감소 전 체크
                {
                    // 정해진 임의값으로 수치 감소
                    ts.Energy.value -= (gm.act_vain[0] * gm.act_getwater[0]);
                    ts.Hunger.value -= (gm.act_vain[0] * gm.act_getwater[1]);
                    ts.Moist.value -= (gm.act_vain[0] * gm.act_getwater[2]);
                    ts.Hardest.value += (gm.act_vain[0] * gm.act_getwater[3]);
                    Debug.Log("망함");
                    return 0;
                }
                else
                {
                    return 0;
                }
            }
            return ran; // 난수 값 리턴
        }
        return 0;
    }

    public int GoForage() // 파밍
    {
        if (gm.cooltime > 0)
        {
            gm.act_sec += Time.deltaTime;
            gm.cooltime -= Time.deltaTime;
        }
        else if (gm.cooltime <= 0)
        {
            gm.isCooltime = false;
            gm.isForage = false;
        }

        if (gm.act_sec >= 1f) // 실행
        {
            int isSuccess = Gacha(0, gm.act_success[1]); // 성공 확률 가챠 난수 생성
            int ran = Gacha(gm.act_goforage_range[0], gm.act_goforage_range[1]); // 아이템 획득 가챠 난수 생성

            if (isSuccess != 0) // 0이 아니면 성공
            {
                if (ts.Energy.value > (ran * gm.act_goforage[0]) && ts.Hunger.value > (ran * gm.act_goforage[1]) && ts.Moist.value > (ran * gm.act_goforage[2]) && (100 - ts.Hardest.value) > (ran * gm.act_goforage[3])) // 능력 감소 전 체크
                {
                    // 실질적인 탐험 성공
                    ts.Energy.value -= (ran * gm.act_goforage[0]);
                    ts.Hunger.value -= (ran * gm.act_goforage[1]);
                    ts.Moist.value -= (ran * gm.act_goforage[2]);
                    ts.Hardest.value += (ran * gm.act_goforage[3]);
                }
                else
                {
                    // 능력치 부족
                    Debug.Log("능력치가 모자라..");
                    return 0;
                }
                gm.act_sec = 0; // 초시계 초기화
                Debug.Log(ran);
            }
            else if (isSuccess == 0) // 0이면 실패
            {
                if (ts.Energy.value > (gm.act_vain[1] * gm.act_goforage[0]) && ts.Hunger.value > (gm.act_vain[1] * gm.act_goforage[1]) && ts.Moist.value > (gm.act_vain[1] * gm.act_goforage[2]) && (100 - ts.Hardest.value) > (gm.act_vain[1] * gm.act_goforage[3])) // 능력 감소 전 체크
                {
                    // 정해진 임의값으로 수치 감소
                    ts.Energy.value -= (gm.act_vain[1] * gm.act_goforage[0]);
                    ts.Hunger.value -= (gm.act_vain[1] * gm.act_goforage[1]);
                    ts.Moist.value -= (gm.act_vain[1] * gm.act_goforage[2]);
                    ts.Hardest.value += (gm.act_vain[1] * gm.act_goforage[3]);
                    Debug.Log("망함");
                    return 0;
                }
                else
                {
                    return 0;
                }
            }
            return ran; // 난수 값 리턴
        }
        return 0;
    }

    public int GoHunt(int rock) // 사냥
    {
        if (gm.cooltime > 0)
        {
            gm.act_sec += Time.deltaTime; // 이벤트 초시계 작동
            gm.cooltime -= Time.deltaTime;
        }
        else if (gm.cooltime <= 0)
        {
            gm.isCooltime = false;
            gm.isHunt = false;
        }

        if (gm.act_sec >= 1f) // 실행
        {
            int isSuccess = Gacha(0, gm.act_success[2]); // 성공 확률 가챠 난수 생성
            int ran = Gacha(gm.act_hunt_range[0], gm.act_hunt_range[1]); // 아이템 획득 가챠 난수 생성

            if (isSuccess != 0) // 0이 아니면 성공
            {
                if (ts.Energy.value > (ran * gm.act_hunt[0]) && ts.Hunger.value > (ran * gm.act_hunt[1]) && ts.Moist.value > (ran * gm.act_hunt[2]) && (100 - ts.Hardest.value) > (ran * gm.act_hunt[3])) // 능력 감소 전 체크
                {
                    // 실질적인 탐험 성공
                    ts.Energy.value -= (ran * gm.act_hunt[0]);
                    ts.Hunger.value -= (ran * gm.act_hunt[1]);
                    ts.Moist.value -= (ran * gm.act_hunt[2]);
                    ts.Hardest.value += (ran * gm.act_hunt[2]);
                }
                else
                {
                    // 능력치 부족
                    Debug.Log("능력치가 모자라..");
                    return 999;
                }
                gm.act_sec = 0; // 초시계 초기화
                Debug.Log(ran);
            }
            else if (isSuccess == 0) // 0이면 실패
            {
                if (ts.Energy.value > (gm.act_vain[2] * gm.act_hunt[0]) && ts.Hunger.value > (gm.act_vain[2] * gm.act_hunt[1]) && ts.Moist.value > (gm.act_vain[2] * gm.act_hunt[2]) && (100 - ts.Hardest.value) > (gm.act_vain[2] * gm.act_hunt[3])) // 능력 감소 전 체크
                {
                    // 정해진 임의값으로 수치 감소
                    ts.Energy.value -= (gm.act_vain[2] * gm.act_hunt[0]);
                    ts.Hunger.value -= (gm.act_vain[2] * gm.act_hunt[1]);
                    ts.Moist.value -= (gm.act_vain[2] * gm.act_hunt[2]);
                    ts.Hardest.value += (gm.act_vain[2] * gm.act_hunt[3]);
                    Debug.Log("망함");
                    return 0;
                }
                else
                {
                    return 0;
                }
            }
            return ran; // 난수 값 리턴
        }
        return 0;
    }

    public int EatFood(int straw) // 먹고
    {
        if (gm.cooltime > 0)
        {
            gm.act_sec += 1 * Time.deltaTime;
            gm.cooltime -= Time.deltaTime;
        }
        else if (gm.cooltime <= 0)
        {
            gm.isCooltime = false;
            gm.isEat = false;
        }

        if (gm.act_sec >= 1f) // 실행
        {
            int ran = Gacha(gm.act_eat_range[0], gm.act_eat_range[1]); // 음식차감 횟수 가챠 난수 생성

            if (ts.Moist.value > (ran * gm.act_eatfood[2])) // 능력 감소 전 체크
            {
                if(gm.food >= ran) // 난수보다 갖고있는 음식수 적으면 현재 음식수로 return
                {
                    ts.Moist.value -= (ran * gm.act_eatfood[2]);
                }
                else // 음식 수 미달이면 최소수 -1 로 차감
                {
                    Debug.Log("식품 미달");
                    ts.Moist.value -= (gm.food * gm.act_eatfood[2]);
                    return gm.food;
                }
            }
            else
            {
                // 능력치 부족
                Debug.Log("능력치가 모자라..");
                return 999;
            }
            gm.act_sec = 0; // 초시계 초기화
            Debug.Log(ran);
            
            return ran; // 난수 값 리턴
        }
        return 0;
    }

    public int DrinkWater(int straw) // 마시고
    {
        if (gm.cooltime > 0)
        {
            gm.act_sec += Time.deltaTime;
            gm.cooltime -= Time.deltaTime;
        }
        else if (gm.cooltime <= 0)
        {
            gm.isCooltime = false;
            gm.isDrink = false;
        }

        if (gm.act_sec >= 1f) // 실행
        {
            int ran = Gacha(gm.act_drink_range[0], gm.act_drink_range[1]); // 아이템 획득 가챠 난수 생성

            gm.act_sec = 0; // 초시계 초기화
            Debug.Log(ran);

            return ran; // 난수 값 리턴
        }
        return 0;
    }

    public int GetMat() // 자원 종류
    {
        int ran = 0;
        if (gm.cooltime > 0)
        {
            gm.act_sec += Time.deltaTime;
            gm.cooltime -= Time.deltaTime;
        }
        else if (gm.cooltime <= 0)
        {
            gm.isCooltime = false;
            gm.isMaterial = false;
        }

        if (gm.act_sec >= 1f) // 실행
        {
            ran = Random.Range(1, 5);
            gm.act_sec = 0;
        }

        return ran;
    }

    public int Sleep() // 잔다
    {
        if (gm.cooltime > 0)
        {
            gm.act_sec += Time.deltaTime;
            gm.cooltime -= Time.deltaTime;
        }
        else if (gm.cooltime <= 0)
        {
            gm.isCooltime = false;
            gm.isSleep = false;
        }

        if (gm.act_sec >= 1f) // 실행
        {
            int ran = Gacha(gm.act_sleep_range[0], gm.act_sleep_range[1]); // 피로도 차감 수치 가챠 난수 생성

            if (ts.Hunger.value > (ran * gm.act_sleep[1]) && ts.Moist.value > (ran * gm.act_sleep[2])) // 능력 감소 전 체크
            {
                if(ts.Hardest.value < 1) // 피로 다 해소됐을때
                {
                    return 998;
                }
                else if(ts.Hardest.value > 1)
                {
                    Debug.Log("성공");
                    ts.Hunger.value -= (ran * gm.act_sleep[1]);
                    ts.Moist.value -= (ran * gm.act_sleep[2]);
                }
            }
            else
            {
                // 능력치 부족
                Debug.Log("능력치가 모자라..");
                return 999;
            }
            gm.act_sec = 0; // 초시계 초기화
            //Debug.Log(ran);

            return ran; // 난수 값 리턴
        }
        return 0;
    }

    public int Gacha(int a, int b)
    {
        return Random.Range(a, b + 1);
    }


}
