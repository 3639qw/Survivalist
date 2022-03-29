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

    /*(저장대상)*/
    protected internal int water = 0; // 물
    /*(저장대상)*/
    protected internal int food = 0; // 음식
    /*(저장대상)*/
    protected internal int wood = 0; // 나무
    /*(저장대상)*/
    protected internal int leather = 0; // 가죽
    /*(저장대상)*/
    protected internal int straw = 0; // 짚
    /*(저장대상)*/
    protected internal int leaf = 0; // 나뭇잎
    /*(저장대상)*/
    protected internal int rock = 0; // 돌

    /*(저장대상)*/
    protected internal int day = 1; // 생존한 일수
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

    //---------------------


    int time = 1; // 월드 시간 (오전 12시 == 0시)
    bool isAm = true; // 시간이 오전인지 여부


    protected internal float cooltime = 0; // 쿨타임
    protected internal bool isCooltime; // 쿨타임 걸렸는지 여부


    //---------------------------------------------------------------------------
    // 프리셋 변수
    // 행동별 쿨타임 프리셋
    // 0: 물, 1: 파밍, 2: 사냥, 3: 먹는다, 4: 마신다, 5: 재료, 6: 취침

    protected internal float[] act_cooltime = new float[] { 5, 9, 12, 5, 5, 15, 10 };

    // 행동별 수치 하락 비율
    // (난수값 * 변수) 계산식으로 수치 하락
    // 0: 체력, 1: 포만감, 2: 갈증, 3: 피로도
    protected internal float[] act_getwater = new float[] { 1.2f, 1.0f, .3f, .8f }; // 물
    protected internal float[] act_goforage = new float[] { 1.2f, .6f, 0.8f, 1.0f }; // 파밍
    protected internal float[] act_hunt = new float[] { .4f, .3f, .35f, .35f }; // 사냥
    protected internal float[] act_eatfood = new float[] { 0, 0, .07f };  // 식사 -- 일시적으로 무력화됨 
    protected internal float[] act_material = new float[] { .2f, .2f, .2f, .2f }; // 재료
    protected internal float[] act_sleep = new float[] { 0, .07f, .1f }; // 취침 -- 일시적으로 무력화됨

    // 활동시 난수값 0 나왔을때 (허탕쳤을때) 능력치 임의 값으로 감소
    // (난수값 * 변수) 계산식으로 수치 하락
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
    protected internal int[] act_getwater_range = new int[] { 5, 10 }; // 물
    protected internal int[] act_goforage_range = new int[] { 3, 4 }; // 파밍
    protected internal int[] act_hunt_range = new int[] { 4, 10 }; // 사냥
    protected internal int[] act_eat_range = new int[] { 1, 10 }; // 식사
    protected internal int[] act_drink_range = new int[] { 1, 10 }; // 콸콸
    protected internal int[] act_material_range = new int[] { 1, 10 }; // 재료
    protected internal int[] act_sleep_range = new int[] { 1, 5 }; // 취침

    // 피로도 누적으로 인한 능률 저하시 가챠 바운더리
    // 0: 물, 1: 파밍, 2: 사냥, 3: 재료
    protected internal int[] act_hard_getwater_range = new int[] {1, 4};
    protected internal int[] act_hard_forgage_range = new int[] {1, 2};
    protected internal int[] act_hard_hunt_range = new int[] {2, 5};
    protected internal int[] act_hard_material_range = new int[] {1, 5};

    // 피로도가 해당 값 이상 일경우 능률 저하
    // 0: 물, 1: 파밍, 2: 사냥
    protected internal float[] act_hardest_decline = new float[] {80, 70, 60};

    // 활동시 필요한 최소한의 수치 -- 피로도는 현재치가 프리셋 이상일 경우
    // 0: 물, 1: 파밍, 2: 사냥, 3: 먹는다, 4: 재료
    // 0: 에너지, 1: 포만감, 2: 촉촉함, 3: 피로도
    protected internal float[] act_getwater_min = new float[] {1, 1, 1, 99}; // 물
    protected internal float[] act_goforgage_min = new float[] {3, 3, 3, 80}; // 파밍
    protected internal float[] act_hunt_min = new float[] {5, 5, 5, 80}; // 사냥
    protected internal float[] act_eatfood_min = new float[] {0, 0, 1 }; // 먹는다
    protected internal float[] act_material_min = new float[] { }; // 재료
    protected internal float[] act_sleep_min = new float[] {0, 1, 1}; // 취침

    // 먹고 마실때 먹는 식품 대비 수치 상승비
    // 0: 음식, 1: 물
    // (음식수 * 변수값) 계산식으로 수치 상승
    protected internal float[] act_food_ratio = new float[] {.7f, 1.0f };

    // 평시에 아무것도 안해서 까는 수치값
    // 0: 포만감, 1: 수분
    protected internal float[] none_minus = new float[] { .5f, .8f };

    // Slider 가 해당 값 미만 일경우 깜박거림
    protected internal float lowvalue = 20;

    // 피로도가 특정 수치 이상일 경우 강제로 잠을 잔다.
    protected internal int act_force_sleep = 90;

    // 이벤트 (부상) 가챠 확률 (숫자만큼 난수기 삽입)
    protected internal int event_injury_chance = 10;

    // 이벤트 (부상) 까는 생명수치 바운더리
    protected internal int[] event_injury_range = new int[] {3, 10};

    // 이벤트 (습격) 가챠 확률 (숫자만큼 난수기 삽입)
    protected internal int ev_attack_chance = 1;

    // 이벤트 (습격) 까는 생명수치 바운더리
    protected internal int[] ev_attack_range = new int[] { 5, 15 };

    // 이벤트 (습격) 시 돌을 가지고 있을경우 피할 수 있는데 필요한 돌의 양
    // (까이는피 * 필요한 돌의 양) 계산식
    protected internal int ev_attack_defence_am = 1;

    //---------------------------------------------------------------------------


    /* 자원별 용도
     * 
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

        // 로그 txt 초기화
        logtxt.text = "";
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


        // 행동을 하고 있어 쿨타임이 있을때에는 쿨타임 표시
        if (isCooltime && cooltime > 0)
        {
            cooltimetxt.text = Mathf.CeilToInt(cooltime).ToString() + "초)";
        }
        else // 쿨타임 없으면 없다고 표시
        {
            cooltimetxt.text = "없음)";
            act_sec = 0; // 이벤트 초시계 초기화
        }

        
        sec += (Time.deltaTime * 2); // 초시계
        if (sec >= 3f) // 3초가 한시간 -- 2를 곱했기 때문에 1.5초가 한시간
        {
            if (time == 12) // 12에서 13으로 넘어가지 않고 1로 리턴
            {
                time = 1;
            }
            else
            {
                time++;
                if (time == 12) // 12시에 이르면
                {
                    if (isAm) // 오전 11시 에서 오후 12시로 넘어갈 때
                    {
                        isAm = false; // 오후로 전환
                    }
                    else if (!isAm) // 오후 11시 에서 오전 12시로 넘어갈 때
                    {
                        isAm = true; // 오전으로 전환
                        day++; // 생존일 +1
                        fileio.Save(); // 게임 저장
                    }
                }
            }

            if (!isCooltime)
            {

                // 아무것도 안 하고 있으면 포만감, 수분 깐다
                ts.Hunger.value -= none_minus[0]; // 포만감 감소
                ts.Moist.value -= none_minus[1]; // 수분 감소
                
                
                
                
                if((time >= 9 && !isAm && time != 12) | (time <= 5 | time == 12 && isAm)) // 밤시간 (오후 9 ~ 오전 5) 밤 전용 로그 출력
                {
                    // 아무것도 안할때 밤 로그 출력
                    int i_log_none = act.Gacha(0, le.log_none_night.Count - 1); // 로그 리스트 인덱스 값에서 가챠 난수 생성
                    string ment = le.log_none_night[i_log_none]; // 난수를 기반으로 string 멘트 산출.
                    logtxt.text = (log_index + 1) + ". " + ment + "\n" + logtxt.text; // 멘트 출력
                    log_index++; // 로그 인덱스 ++
                }
                else
                {
                    // 아무것도 안할때 로그 출력
                    int i_log_none = act.Gacha(0, le.log_none.Count - 1); // 로그 리스트 인덱스 값에서 가챠 난수 생성
                    string ment = le.log_none[i_log_none]; // 난수를 기반으로 string 멘트 산출.
                    logtxt.text = (log_index+1) + ". " + ment + "\n" + logtxt.text; // 멘트 출력
                    log_index++; // 로그 인덱스 ++
                }


                // 이벤트 부상, 습격 종류 결정
                // 1 ~ 5 : 부상, 6 ~ 10 : 습격
                int event_kindof = act.Gacha(1, 10);
                if(event_kindof < 6) 
                {
                    // 이벤트 (부상)
                    int i_injury_chance = act.Gacha(0, event_injury_chance); // 부상 걸리는지 여부 결정
                    if(i_injury_chance == 0) // 부상 걸렸으면 깔 생명 양 결정
                    {
                        int i_injury_health = act.Gacha(event_injury_range[0], event_injury_range[1]);
                        if(ts.Health.value > i_injury_health) // 현재 생명수치가 더 많을 경우
                        {
                            ts.Health.value -= i_injury_health; // 생명 깍고

                            // 부상 로그 출력
                            string ment = "이벤트: 부상 (-" + i_injury_health + " 건강)";
                            logtxt.text = (log_index + 1) + ". " + ment + "\n" + logtxt.text; // 멘트 출력
                            log_index++; // 로그 인덱스 ++


                        }
                        else // 현재 생명수치가 더 적을 경우 == 사망
                        {
                            ts.Health.value = 0;
                            // 부상 로그 출력
                            string ment = "이벤트: 부상 (-" + i_injury_health + ") \n 사망..";
                            logtxt.text = (log_index + 1) + ". " + ment + "\n" + logtxt.text; // 멘트 출력
                            isCooltime = true;
                            log_index++; // 로그 인덱스 ++
                        }

                    } // if(i_injury_chance == 0)
                } // if(event_kindof > 5)
                else
                {
                    // 이벤트 (습격)
                    int i_attack_chance = act.Gacha(0, ev_attack_chance); // 부상 걸리는지 여부 결정
                    if (i_attack_chance == 0) // 부상 걸렸으면 깔 생명 결정
                    {
                        int i_attack_health = act.Gacha(ev_attack_range[0], ev_attack_range[1]);
                        if(rock >= (i_attack_health * ev_attack_defence_am)) // 돌로 막을수 있을 경우
                        {
                            // 막았다
                            string ment = "이벤트: 돌로 습격을 막았다 (-" + (i_attack_health * ev_attack_defence_am) + " 돌)";
                            logtxt.text = (log_index + 1) + ". " + ment + "\n" + logtxt.text; // 멘트 출력
                            rock -= (i_attack_health * ev_attack_defence_am); // 돌 차감
                            log_index++; // 로그 인덱스 ++
                        }
                        else
                        {
                            if (ts.Health.value > i_attack_health) // 현재 생명수치가 더 많을 경우
                            {
                                ts.Health.value -= i_attack_health; // 생명 깍고

                                // 부상 로그 출력
                                string ment = "이벤트: 습격 (-" + i_attack_health + " 건강)";
                                logtxt.text = (log_index + 1) + ". " + ment + "\n" + logtxt.text; // 멘트 출력
                                log_index++; // 로그 인덱스 ++
                            }
                            else // 현재 생명수치가 더 적을 경우 == 사망
                            {
                                ts.Health.value = 0;
                                string ment = "이벤트: 습격 (-" + i_attack_health + ") \n 사망..";
                                logtxt.text = (log_index + 1) + ". " + ment + "\n" + logtxt.text; // 멘트 출력
                                log_index++; // 로그 인덱스 ++
                            }
                        }
                    } // if (i_attack_chance == 0)
                } // if(event_kindof < 6) else




                // 안자고 일만하니까 강제로 자
                if (ts.Hardest.value >= act_force_sleep)
                {
                    isSleep = true;
                    isCooltime = true;
                    cooltime = act_cooltime[6];
                }


            }

            // 체력 회복
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

                    if (ts.Hunger.value >= 60 && ts.Moist.value >= 60 && ts.Hardest.value <= 40 && ts.Health.value >= 60) // 포만감, 갈증, 피로도, 생명 60% 상위권 경우 체력 100까지 상승 가능
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
                    else // 포만감, 갈증, 피로도, 생명 60이하인 경우 체력 50까지 상승 가능
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
            
            sec = 0;
        }

        if (isAm) // 오전
        {
            timetxt.text = "오전 " + time.ToString() + "시";
        }
        else if (!isAm) // 오후
        {
            timetxt.text = "오후 " + time.ToString() + "시";
        }

        

    } // void Update

    // 물 생산 로그 전송
    public void Log_getWater(int amount)
    {
        string ment = "(+" + amount + " 물)"; // 얻은 자원 갯수 기반으로 string 멘트 산출.
        logtxt.text = (log_index + 1) + ". " + ment + "\n" + logtxt.text; // 멘트 출력
        log_index++; // 로그 인덱스 ++
    }

    // 파밍 로그 전송
    public void Log_forgage(int amount)
    {
        string ment = "(+" + amount + " 음식)"; // 얻은 자원 갯수 기반으로 string 멘트 산출.
        logtxt.text = (log_index + 1) + ". " + ment + "\n" + logtxt.text; // 멘트 출력
        log_index++; // 로그 인덱스 ++
    }

    // 사냥 로그 전송
    public void Log_hunt(int amount)
    {
        string ment = "(+" + amount + " 음식)"; // 얻은 자원 갯수 기반으로 string 멘트 산출.
        logtxt.text = (log_index + 1) + ". " + ment + "\n" + logtxt.text; // 멘트 출력
        log_index++; // 로그 인덱스 ++
    }




}
