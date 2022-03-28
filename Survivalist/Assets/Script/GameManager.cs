using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static public GameManager instance = null;

    GameSave fileio;
    Act_function act;
    TextSync ts;
    LogEdit le;

    // 게임내 변수들

    /*(저장대상)*/ protected internal int water = 0; // 물
    /*(저장대상)*/ protected internal int food = 0; // 음식
    /*(저장대상)*/ protected internal int wood = 0; // 나무
    /*(저장대상)*/ protected internal int leather = 0; // 가죽
    /*(저장대상)*/ protected internal int straw = 0; // 짚
    /*(저장대상)*/ protected internal int leaf = 0; // 나뭇잎
    /*(저장대상)*/ protected internal int rock = 0; // 돌
                  
    /*(저장대상)*/ protected internal int day = 1; // 생존한 일수
    protected internal int log_index = 0; // 로그 번호

    // 쿨타임중 행동개시
    protected internal bool isGetsWater; // 물 증류
    protected internal bool isForage; // 파밍
    protected internal bool isHunt; // 사냥
    protected internal bool isEat; // 식사
    protected internal bool isDrink; // 물이나 먹어라
    protected internal bool isMaterial; // 자원 수집
    protected internal bool isSleep; // 취침


    public Text logtxt; // 로그 Txt
    public Text timetxt; // 시간 Txt
    public Text cooltimetxt; // 쿨타임 Txt

    public Text cheattxt; // 치트 txt

    protected internal string nowact; // 현재 하고 있는일 string

    float sec; // 초시계

    /* 지역변수화 전역변수 */
    protected internal float act_sec; // 행동할때 체크하는 초시계
    
    //-------------------


    int time = 6; // 월드 시간 (오전 6시)
    bool isAm = true; // 시간이 오전인지 여부


    protected internal float cooltime = 0; // 쿨타임
    protected internal bool isCooltime; // 쿨타임 걸렸는지 여부


    //---------------------------------------------------------------------------

    // 프리셋 변수
    // 행동별 쿨타임 프리셋
    // 0: 물, 1: 파밍, 2: 사냥, 3: 먹는다, 4: 마신다, 5: 재료, 6: 취침

    float[] act_cooltime = new float[] { 5, 9, 12, 5, 5, 15, 10 };

    // 행동별 수치 하락 비율
    // 0: 체력, 1: 포만감, 2: 갈증, 3: 피로도
    protected internal float[] act_getwater = new float[] {1.2f, 1.0f, .3f, .8f}; // 물
    protected internal float[] act_goforage = new float[] {1.2f, .6f, 0.8f, 1.0f}; // 파밍
    protected internal float[] act_hunt = new float[] {.4f, .3f, .35f, .35f}; // 사냥
    protected internal float[] act_eatfood = new float[] {0, 0, .07f };  // 식사
    protected internal float[] act_material = new float[] {.2f, .2f, .2f, .2f }; // 재료
    protected internal float[] act_sleep = new float[] { 0, .07f, .1f }; // 취침

    // 활동시 난수값 0 나왔을때 (허탕쳤을때) 능력치 임의 값으로 감소
    // 0: 물, 1. 파밍, 2. 사냥, 3. 재료 
    protected internal float[] act_vain = new float[] { .6f, .8f, .3f, .1f };

    // 활동 성공 확률 (아이템 파밍 활동에만 해당)
    // 0: 물, 1: 파밍, 2: 사냥, 3: 재료
    // 아래 숫자만큼 난수기 삽입
    // 0이 걸렸을때만 실패
    protected internal int[] act_success = new int[] { 5, 5, 3, 3 };

    // 아이템 갯수 가챠 바운더리
    // 0: 물, 1: 파밍, 2: 사냥, 3: 먹는다, 4: 마신다, 5: 재료, 6: 취침
    // 0번인덱스부터 시작하여 1번인덱스까지 난수기 삽입
    protected internal int[] act_getwater_range = new int[] { 1, 10 }; // 물
    protected internal int[] act_goforage_range = new int[] { 1, 4 }; // 파밍
    protected internal int[] act_hunt_range = new int[] { 4, 10 }; // 사냥
    protected internal int[] act_eat_range = new int[] { 1, 10 }; // 식사
    protected internal int[] act_drink_range = new int[] { 1, 10 }; // 콸콸
    protected internal int[] act_material_range = new int[] { 1, 10 }; // 재료
    protected internal int[] act_sleep_range = new int[] { 1, 5 }; // 취침

    // 활동시 필요한 최소한의 수치
    // 0: 물, 1: 파밍, 2: 사냥, 3: 먹는다, 4: 재료
    // 0: 에너지, 1: 포만감, 2: 촉촉함, 3: 피로도
    protected internal float[] act_getwater_min = new float[] {1, 1, 1, 99}; // 물
    protected internal float[] act_goforgage_min = new float[] {3, 3, 3, 80}; // 파밍
    protected internal float[] act_hunt_min = new float[] {5, 5, 5, 70}; // 사냥
    protected internal float[] act_eatfood_min = new float[] {0, 0, 1 }; // 먹는다
    protected internal float[] act_material_min = new float[] { }; // 재료
    protected internal float[] act_sleep_min = new float[] {0, 1, 1}; // 취침

    // 먹고 마실때 먹는 음식 대비 상승비
    // 0: 음식, 1: 물
    protected internal float[] act_food_ratio = new float[] {.7f, 1.0f };

    // Slider 가 해당 값 미만 일경우 깜박거림
    protected internal float lowvalue = 20;

    //---------------------------------------------------------------------------

    /* 행동별 쿨타임
     * 
     * 1. 물: 5초 
     * 2. 파밍: 9초
     * 3. 사냥: 15초
     * 4. 먹는다: 5초
     * 5. 마신다: 5초
     * 6. 재료: 20초
     * 7. 취침: 10초
     */

    /* 행동별 필요한 재료 or 스킬
     * 
     * 1. 물: 나무, 짚, 나뭇잎 (왼쪽으로 갈수록 많은 물을 얻을 수 있음) | 체력---, 허기--, 갈증-
     * 2. 파밍:   | 체력--, 허기---, 갈증-
     * 3. 사냥: 돌(선택사항, 있으면 식량획득 확률증가) | 체력---, 허기---, 갈증--
     * 4. 먹는다: 짚(선택사항, 있으면 식중독 확률낮춤) | 허기+++, 갈증-
     * 5. 마신다: 짚(선택사항, 있으면 수인성 전염병 감염 확률낮춤) | 갈증 +++
     * 6. 재료:  | 체력--, 허기-, 갈증-
     * 7. 취침: 확률에 따라 야생동물에 습격을 받아 생명 깍일수 있음
     */


    /* 수치별 상승 요인
     * 
     * 1. 생명: 체력, 포만감, 수분감, 피로도에 따라 자연 상승 (단 자연적으로는 60이상 상승 불가)
     * 2. 체력: 포만감, 수분감, 피로도에 따라 자연 상승
     * 3. 포만감: 밥 많이 먹으면 
     * 4. 수분감: 물 많이 마셔
     * 5. 피로도: 잠 많이 자
     */

    /* 자원별 용도
     * 
     * 1. 물: 갈증 해결
     * 2. 음식: 허기 해결
     * 3. 나무: 물 획득수 ++
     * 4. 가죽: 추운 날씨의 영향을 적게 받을 수 있음
     * 5. 짚: 물 획득수 +
     * 6. 나뭇잎: 물 획득
     * 7. 돌: 몬스터나 야생동물이 덮칠때 대응할 수 있음, 사냥시에 확률증가
     */


    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    void Awake()
    {
        sec = 0;
        if (instance == null)
        {
            instance = this;
        }

        fileio = GameSave.Instance;
        act = Act_function.Instance;
        ts = TextSync.Instance;
        le = LogEdit.Instance;
        
    }

    void Start()
    {
        // 게임 로딩
        //fileio.Load();

    }

    void Update()
    {
        // 게임 종료
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }


        if (isGetsWater)
        {
            nowact = "물 정화하는 중...";
        }
        else if (isForage)
        {
            nowact = "식량을 찾는 중...";
        }
        else if (isHunt)
        {
            nowact = "사냥하는 중...";
        }
        else if (isEat)
        {
            nowact = "밥 먹는 중...";
        }
        else if (isDrink)
        {
            nowact = "물 마시는 중...";
        }
        else if (isMaterial)
        {
            nowact = "자원을 찾는 중...";
        }
        else if (isSleep)
        {
            nowact = "꿀잠 숙면 중...";
        }
        else
        {
            nowact = "쉬는 중...";
        }


        // 쿨타임없을때 할 일을 선택하세요
        if (isCooltime)
        {
            Blinking.Instance.btxt.SetActive(false);
        }
        else
        {
            Blinking.Instance.Blink(); // "할 일을 선택하세요" 깜박임 출력
        }



        if (isCooltime && cooltime > 0)
        {
            cooltimetxt.text = Mathf.CeilToInt(cooltime).ToString() + "초)";
        }
        else
        {
            cooltimetxt.text = "없음)";
            act_sec = 0; // 이벤트 초시계 초기화
        }

        sec += Time.deltaTime; // 초시계

        if(sec >= 3f) // 3초가 한시간
        {
            // 아무것도 안할때 로그 출력
            if (!isCooltime)
            {
                int i_log_none = act.Gacha(0, le.log_none.Count - 1); // 로그 리스트 인덱스 값에서 가챠 난수 생성
                string ment = le.log_none[i_log_none]; // 난수를 기반으로 string 멘트 산출.
                logtxt.text = log_index+1 + ". " + ment + "\n" + logtxt.text; // 멘트 출력
                log_index++; // 로그 인덱스 ++
            }

            // 포만감, 갈증에 따라 체력 회복
            if(ts.Energy.value < 100 && !isCooltime)
            {
                if (ts.Hunger.value > 0 && ts.Moist.value > 0)
                {
                    int i; // 가챠 난수
                    if (ts.Hunger.value >= 90 && ts.Moist.value >= 90 && ts.Hardest.value <= 10)
                    {
                        i = act.Gacha(6, 9);
                        Debug.Log("A");
                    }
                    else if (ts.Hunger.value >= 80 && ts.Moist.value >= 80 && ts.Hardest.value <= 20)
                    {
                        i = act.Gacha(4, 6);
                        Debug.Log("B");
                    }
                    else if (ts.Hunger.value >= 70 && ts.Moist.value >= 70 && ts.Hardest.value <= 30)
                    {
                        i = act.Gacha(2, 4);
                        Debug.Log("C");
                    }
                    else if (ts.Hunger.value >= 60 && ts.Moist.value >= 60 && ts.Hardest.value <= 40)
                    {
                        i = act.Gacha(1, 2);
                        Debug.Log("D");
                    }
                    else
                    {
                        i = act.Gacha(1, 2);
                        Debug.Log("E");
                    }

                    if (ts.Hunger.value >= 60 && ts.Moist.value >= 60 && ts.Hardest.value <= 40) // 포만감, 갈증, 피로도 60이상인 경우 체력 100까지 상승 가능
                    {
                        if (i > (100 - ts.Energy.value)) // 필요량을 넘어갈때
                        {
                            ts.Energy.value = 100; // 그냥 100으로 맞춰버린다
                        }
                        else // 100 - energy 가 i 보다 클때 
                        {
                            ts.Energy.value += i; // 체력 증가
                        }
                    }
                    else // 포만감, 갈증, 피로도 60이하인 경우 체력 50까지 상승 가능
                    {
                        if (i > (50 - ts.Energy.value)) // 필요량을 넘어갈때
                        {
                            ts.Energy.value = 50; // 그냥 50으로 맞춰버린다
                        }
                        else // 50 - energy 가 i 보다 클때 
                        {
                            ts.Energy.value += i; // 체력 증가
                        }
                    }


                } // if(hunger > 0 && thirst > 0)

            } // if(energy < 100 && !isCooltime)

            time++;
            sec = 0;
        }

        sec += Time.deltaTime; // 초시계

        if (isAm) // 오전
        {
            timetxt.text = time.ToString() + " 오전";

            if (time >= 12)
            {
                isAm = false;
                //Debug.Log("오후");
                time = 1;
            }
        }
        else if (!isAm) // 오후
        {
            timetxt.text = time.ToString() + " 오후";

            if (time >= 12) // 하루가 다 지나갔으면
            {
                isAm = true;
                //Debug.Log("오전");
                day++;
                fileio.Save(); // 게임 세이브
                time = 1;
            }
        }

        // Input 영역
        if (!isCooltime) // 쿨타임 안 걸렸으면
        {

            if(Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) // 물 얻으러
            {
                if(ts.Energy.value >= act_getwater_min[0] && ts.Hunger.value >= act_getwater_min[1] && ts.Moist.value >= act_getwater_min[2] && ts.Hardest.value <= act_getwater_min[3])
                {
                    //Debug.Log("물 얻으러");
                    isGetsWater = true;
                    isCooltime = true;
                    cooltime = act_cooltime[0];
                }
            }

            else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2)) // 파밍
            {
                if (ts.Energy.value >= act_goforgage_min[0] && ts.Hunger.value >= act_goforgage_min[1] && ts.Moist.value >= act_goforgage_min[2] && ts.Hardest.value <= act_goforgage_min[3])
                {
                    //Debug.Log("파밍");
                    isForage = true;
                    isCooltime = true;
                    cooltime = act_cooltime[1];
                }
            }

            else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3)) // 사냥
            {
                if (ts.Energy.value >= act_hunt_min[0] && ts.Hunger.value >= act_hunt_min[1] && ts.Moist.value >= act_hunt_min[2] && ts.Hardest.value <= act_hunt_min[3])
                {
                    //Debug.Log("사냥");
                    isHunt = true;
                    isCooltime = true;
                    cooltime = act_cooltime[2];
                }
            }

            else if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4)) // 먹는다
            {
                if (food > 0 && ts.Moist.value >= act_eatfood_min[2] && ts.Hunger.value < 100f) // 음식이 한단위 이상 있는지, 
                {
                    //Debug.Log("먹는다");
                    isEat = true;
                    isCooltime = true;
                    cooltime = act_cooltime[3];
                }
            }

            else if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5)) // 콸콸
            {
                if(water > 0 && ts.Moist.value < 100f) // 물이 최소한 만큼 있는지, 갈증이 이미 해소되었는지
                {
                    //Debug.Log("콸콸");
                    isDrink = true;
                    isCooltime = true;
                    cooltime = act_cooltime[4];
                }
            }

            //else if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6)) // 재료
            //{
            //    if (ts.Energy.value >= act_material_min[0] && ts.Hunger.value >= act_material_min[1] && ts.Moist.value >= act_material_min[2])
            //    {
            //        isMaterial = true;
            //        isCooltime = true;
            //        cooltime = act_cooltime[5];
            //    }
                
            //}

            else if (Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7)) // 취침
            {
                if(ts.Hunger.value >= act_sleep_min[1] && ts.Moist.value >= act_sleep_min[2] && ts.Hardest.value > 0)
                {
                    //Debug.Log("취침");
                    isSleep = true;
                    isCooltime = true;
                    cooltime = act_cooltime[6];
                }
            }

        }

    }


}
