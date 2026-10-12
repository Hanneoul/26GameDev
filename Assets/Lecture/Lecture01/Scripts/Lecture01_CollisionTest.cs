/*
 * ==============================================================================
 * [Unity] 2D 충돌(Collision) 감지 시스템 및 판정 방식 상세 해설
 * ==============================================================================
 * 
 * 1. 유니티 2D 충돌 시스템 개요 (Collision System Overview)
 *    - 유니티 물리 엔진은 오브젝트 간의 물리적 상호작용을 처리하기 위해 
 *      Collider2D(충돌 범위를 지정하는 컴포넌트)와 Rigidbody2D(물리 성질을 부여하는 컴포넌트)의 
 *      조합을 사용함.
 *    - 충돌이 발생하면 물리 엔진이 이를 감지하고, 스크립트에 정의된 특정 이벤트 함수를 
 *      자동으로 호출하여 개발자가 반응할 수 있도록 지원함.
 * 
 * 2. 충돌 감지의 다양한 방법과 측면 (Various Collision Detection Methods)
 *    - 물리적 충돌 (Collision vs Trigger):
 *      1) 일반 콜라이더 충돌 (Collision): 
 *         두 물체가 서로 밀어내며 물리적으로 관통하지 못하고 부딪히는 현상임. 
 *         OnCollisionEnter2D, OnCollisionStay2D, OnCollisionExit2D 함수를 사용함.
 *      2) 트리거 충돌 (Trigger): 
 *         물리적으로 밀어내는 힘은 발생하지 않고, 영역에 '진입'했는지 여부만 체크하는 센서 방식임. 
 *         (예: 아이템 획득, 포탈 통과 등). OnTriggerEnter2D, OnTriggerExit2D 함수를 사용함.
 *    - 판정 시점에 따른 분류 (Lifecycle of Collision):
 *      - Enter: 두 오브젝트가 처음 닿는 순간 (딱 1회 호출)
 *      - Stay: 닿아 있는 동안 매 프레임 호출
 *      - Exit: 닿아 있다가 서로 떨어지는 순간 (딱 1회 호출)
 *    - 레이캐스트 / 오버랩 방식 (Raycast & Overlap - 비물리적/직접 검사):
 *      - 충돌 컴포넌트의 이벤트 함수에 의존하지 않고, 코드로 직접 특정 위치에 레이저를 쏘거나(Raycast) 
 *        특정 영역을 지정해(OverlapBox/Circle) 그 안에 오브젝트가 있는지 실시간으로 검사하는 방식임. 
 *        주로 캐릭터 바닥 체크나 공격 범위 판정에 활용됨.
 * 
 * 3. 본 코드의 핵심 기능 (OnCollisionEnter2D / Exit2D)
 *    - 오브젝트가 바닥이나 다른 물체에 부딪히는 순간(Enter)과 떨어지는 순간(Exit)을 각각 카운트하여 
 *      디버그 로그로 출력함으로써 충돌 횟수와 시점을 명확하게 추적할 수 있도록 구성됨.
 * ==============================================================================
 */

using UnityEngine;

public class Lecture01_CollisionTest : MonoBehaviour
{
    // 물체가 서로 충돌하여 진입한 횟수를 기록하기 위한 정수형 변수 선언함
    int EnterCount = 0;

    // 물체가 충돌 상태에서 서로 멀어져 떨어질 때의 횟수를 기록하기 위한 정수형 변수 선언함
    int ExitCount = 0;

    // Start 함수: 게임 오브젝트가 생성된 후 첫 번째 프레임 업데이트 전에 단 한 번 실행됨
    void Start()
    {
        // 이 예제에서는 초기화 시 별도로 처리할 로직이 없어 비워둠
    }

    // Update 함수: 화면이 렌더링되는 매 프레임마다 반복해서 호출됨
    void Update()
    {
        // 이 예제에서는 매 프레임마다 처리할 입력이나 상태 체크가 없어 비워둠
    }

    // OnCollisionEnter2D 함수: 다른 콜라이더와 물리적으로 '처음 부딪히는 순간' 유니티 물리 엔진이 자동 호출함
    void OnCollisionEnter2D(Collision2D col)
    {
        // 진입 횟수를 1 증가시킴
        EnterCount++;

        // 현재까지 누적된 충돌 진입 횟수를 콘솔창에 로그로 출력함
        Debug.Log("OnCollisionEnter2D Counter : " + EnterCount);
    }

    // OnCollisionExit2D 함수: 부딪혀 있던 다른 오브젝트와 서로 '떨어지는 순간' 유니티 물리 엔진이 자동 호출함
    void OnCollisionExit2D(Collision2D col)
    {
        // 이탈 횟수를 1 증가시킴
        ExitCount++;

        // 현재까지 누적된 충돌 이탈 횟수를 콘솔창에 로그로 출력함
        Debug.Log("OnCollisionExit2D Counter : " + ExitCount);
    }
}