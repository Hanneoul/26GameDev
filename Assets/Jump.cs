/*
 * ==============================================================================
 * [Unity] 새로운 입력 시스템(New Input System) 및 클래식 입력 시스템 비교 해설
 * ==============================================================================
 * 
 * 1. 기존 입력 시스템(Old Input System) vs 새 입력 시스템(New Input System)
 *    - 기존 시스템 (Input 클래스):
 *      * Input.GetKey(), Input.GetMouseButtonDown() 등의 메서드를 통해 폴링(Polling) 방식으로 
 *        매 프레임마다 키 입력을 직접 검사함.
 *      * 단점: 키 매핑을 변경하기가 번거롭고, 멀티플레이어나 다양한 기기(게임패드, 터치, 키보드 등) 
 *        확장성이 떨어지며, 코드 내에 키 이름이 하드코딩되는 경우가 많음.
 *    - 새로운 입력 시스템 (New Input System):
 *      * 입력 처리와 게임 로직을 분리(Action 기반)하여 이벤트 주도형(Event-driven) 구조를 제공함.
 *      * 장점: 
 *        1) 멀티 플랫폼(PC, 모바일, 콘솔 게임패드 등) 대응이 매우 유연함.
 *        2) 유저가 직접 키 설정을 바꿀 수 있는 리매핑(Remapping) 기능을 쉽게 구현 가능함.
 *        3) 폴링 방식 외에도 콜백 및 C# 이벤트 형식으로 입력을 받을 수 있어 구조가 깔끔해짐.
 * 
 * 2. New Input System에서 코드로 직접 접근하는 주요 방법과 예시
 *    - 키보드/마우스 직접 폴링 (Keyboard.current, Mouse.current):
 *      * Input 클래스를 대체하여 현재 연결된 장치 상태에 직접 접근함.
 *      * 예시: 
 *        if (Keyboard.current.spaceKey.wasPressedThisFrame) { Debug.Log("스페이스바 누름"); }
 *        if (Mouse.current.leftButton.isPressed) { Vector2 mousePos = Mouse.current.position.ReadValue(); }
 *    - 액션 기반 맵핑 (Player Input & InputAction):
 *      * 에디터에서 Input Actions Asset을 만들어 "Jump", "Move" 등의 액션을 정의하고 스크립트에 바인딩함.
 *      * 예시: 콜백 함수나 인스펙터 연결을 통해 입력이 들어올 때만 함수가 호출되도록 설정 가능함.
 * 
 * 3. 본 코드의 구조 및 동작 원리
 *    - 기존 OnMouseDown 방식을 유지하면서, 새 시스템 도입 전후의 흐름을 이해할 수 있는 기본 점프 스크립트임.
 *    - 마우스 클릭 시 Rigidbody2D에 힘을 가해 위로 튀어 오르게 동작함.
 * ==============================================================================
 */

using UnityEngine;

public class Jump : MonoBehaviour
{
    // 2D 물리 연산을 처리하기 위해 Rigidbody2D 컴포넌트를 담을 변수 선언함
    Rigidbody2D rb;

    // 점프할 때 위쪽으로 가할 힘의 크기를 인스펙터 창에서 조절할 수 있게 공개(public)로 선언함
    public float force = 1.0f;

    // Start 함수: 게임 오브젝트가 생성된 후 첫 번째 프레임 업데이트 전에 단 한 번 실행됨
    void Start()
    {
        // 이 오브젝트에 부착된 Rigidbody2D 컴포넌트를 가져와서 rb 변수에 저장함 (캐싱)
        rb = GetComponent<Rigidbody2D>();
    }

    // OnMouseDown 함수: 콜라이더 영역 안에서 마우스 왼쪽 버튼을 누르는 순간 유니티 엔진이 자동 호출함
    void OnMouseDown()
    {
        // x축은 0, y축은 1인 방향 벡터에 force 값을 곱해서 위쪽 방향의 힘 크기를 계산함
        Vector2 up = new Vector2(0f, 1f) * force;

        // 미리 찾아둔 Rigidbody2D 컴포넌트에 위쪽 방향으로 순간적인 힘을 가해 점프시킴
        rb.AddForce(up);

        // 마우스 클릭이 정상적으로 인식되었음을 확인하기 위해 콘솔창에 로그를 출력함
        Debug.Log("마우스 눌림");
    }

    // Update 함수: 화면이 렌더링되는 매 프레임마다 반복해서 호출됨
    void Update()
    {
        // 현재 이 예제에서는 매 프레임 처리할 별도의 로직이 없어 비워둠
    }
}