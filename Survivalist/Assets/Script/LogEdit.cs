using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogEdit : MonoBehaviour
{
    static public LogEdit instance = null;

    // 쉬는 중... 일때
    protected internal List<string> log_none = new List<string> ();
    // 물을 정화중 일때 
    protected internal List<string> log_getwater = new List<string>();
    // 파밍중 일때
    protected internal List<string> log_forgage = new List<string>();
    // 사냥중 일때
    protected internal List<string> log_hunt = new List<string>();
    // 먹는중 일때
    protected internal List<string> log_eatfood = new List<string>();
    // 마시는중 일때
    protected internal List<string> log_drink = new List<string>();
    // 재료를 찾는중 일때
    protected internal List<string> log_mat = new List<string>();
    // 취침중 일때
    protected internal List<string> log_sleep = new List<string>();



    // 이벤트
    // 부상
    protected internal List<string> log_injury = new List<string> ();
    // 동물의 공격
    protected internal List<string> log_aniattack = new List<string>();



    

    // 게이지 미달
    protected internal List<string> log_sick = new List<string> (); // 건강
    protected internal List<string> log_exhausted = new List<string> (); // 체력
    protected internal List<string> log_hungry = new List<string> (); // 포만감
    protected internal List<string> log_thirsty = new List<string> (); // 수분
    protected internal List<string> log_tired = new List<string> (); // 피로


    public static LogEdit Instance
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

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        
        // 아무것도 안 할때
        log_none.Add("너무 덥고 습해서 오래 있진 못할 거 같군..."); // 0
        log_none.Add("파란 하늘은 봐도 봐도 질리지 않네."); // 1
        log_none.Add("난 생산적인 일을 시작해야 할 거 같다..."); // 2
        log_none.Add("여긴 어디지? 태평양? 대서양? 흠..."); // 3
        log_none.Add("난 해변을 걷는 것을 즐긴다."); // 4
        log_none.Add("해변을 걷다 보니 많은 것을 생각하게 하는군..."); // 5
        log_none.Add("경치를 감상하니 여기도 나쁘진 않은 것 같다..."); // 6 
        log_none.Add("또 한시간이 갔다. 나는 아직도 여기 있네..."); // 7
        log_none.Add("내가 여기를 어떻게 오게 되었을까? 여긴 너무 이상해..."); // 8
        log_none.Add("내 꿈을 기억할 수만 있다면..."); // 9 
        log_none.Add("여기서는 사진 한번 찍어도 괜찮을 것 같다..."); // 10
        log_none.Add("저 구름 모양을 봐."); // 11
        log_none.Add("서서 보는 것도 좀 지루하다."); // 12


        // 매우 피로함
        log_tired.Add("너무 피곤해서 제대로 쉴 수가 없다");













    }

















}
