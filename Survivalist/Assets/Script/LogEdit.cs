using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogEdit : MonoBehaviour
{
    static public LogEdit instance = null;

    // 쉬는 중... 일때
    protected internal List<string> log_none = new List<string> ();
    // 쉬는 중... 일때 (밤 일때)
    protected internal List<string> log_none_night = new List<string> ();
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
        log_none.Add("모래위에 누워있으니 정말 뜨겁다..."); // 13
        log_none.Add("해변에 게가 기어 다니는 것이 보인다..."); // 14
        log_none.Add("저 수평선 너머로 보이는 배는 뭘까."); // 15

        // 밤에 아무것도 안 할때
        log_none_night.Add("어둠속에서 내가 할 수 있는 일이 있을까..."); // 0
        log_none_night.Add("저쪽에서 무언가 보인 거 같은데 잘 안 보이는군..."); // 1
        log_none_night.Add("아무것도 안보이니 일을 하기엔 힘들겠다..."); // 2
        log_none_night.Add("밤에 해변을 걸어도 될지..."); // 3
        log_none_night.Add("하늘을 보니 별이 정말 많다."); // 4
        log_none_night.Add("하늘에서 뭔가 떨어졌는데? 뭐지..."); // 5
        log_none_night.Add("별이 당장이라도 쏟아질 것 같아."); // 6
        log_none_night.Add("밤에는 공격을 받아도 방어하기 힘들 것 같다."); // 7
        log_none_night.Add("밤에는 모래도 정말 차갑다..."); // 8
        log_none_night.Add("밤이 되니 너무 춥다..."); // 9

        // 물을 정화중 일때
        log_getwater.Add(""); // 0
        log_getwater.Add("(+1 물)"); // 1
        log_getwater.Add("(+2 물)"); // 2
        log_getwater.Add("(+3 물)"); // 3
        log_getwater.Add("(+4 물)"); // 4
        log_getwater.Add("(+5 물)"); // 5
        log_getwater.Add("(+6 물)"); // 6
        log_getwater.Add("(+7 물)"); // 7
        log_getwater.Add("(+8 물)"); // 8
        log_getwater.Add("(+9 물)"); // 9
        log_getwater.Add("(+10 물)"); // 10

        // 파밍중 일때
        log_forgage.Add(""); // 0
        log_forgage.Add("(+1 음식)"); // 1
        log_forgage.Add("(+2 음식)"); // 2
        log_forgage.Add("(+3 음식)"); // 3
        log_forgage.Add("(+4 음식)"); // 4

        // 사냥중 일때
        log_hunt.Add(""); // 0
        log_hunt.Add(""); // 1
        log_hunt.Add(""); // 2
        log_hunt.Add(""); // 3
        log_hunt.Add("(+4 음식)"); // 4
        log_hunt.Add("(+5 음식)"); // 5
        log_hunt.Add("(+6 음식)"); // 6
        log_hunt.Add("(+7 음식)"); // 7
        log_hunt.Add("(+8 음식)"); // 8
        log_hunt.Add("(+9 음식)"); // 9
        log_hunt.Add("(+10 음식)"); // 10

        // 매우 피로함
        log_tired.Add("너무 피곤해서 제대로 쉴 수가 없다");

    }

}
