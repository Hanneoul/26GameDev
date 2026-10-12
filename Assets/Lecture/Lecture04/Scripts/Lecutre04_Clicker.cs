/*
 * ==============================================================================
 * [Unity] New Input System (새로운 입력 시스템) - Mouse.current 및 폴링 방식 해설
 * ==============================================================================
 * 
 * 1. New Input System의 특징 및 기존 방식과의 비교
 *    - 기존 입력 시스템 (Input 클래성):
 *      * Input.GetMouseButtonDown(0) 같은 방식을 사용했으나, 전역적으로 관리되어 확장성이 떨어짐.
 *    - 새로운 입력 시스템 (New Input System):
 *      * 입력 장치를 객체화하여 직접 접근함 (예: Mouse.current, Keyboard.current, Gamepad.current).
 *      * 멀티플레이어, 다양한 플랫폼(PC, 모바일, 콘솔), 유저 커스텀 키 리매핑(Remapping) 대응이 매우 뛰어남.
 * 
 * 2. 폴링(Polling) 방식과 장단점
 *    - 폴링 방식이란?
 *      * Update() 함수 안에서 매 프레임마다 장치의 상태(wasPressedThisFrame, isPressed 등)를 
 *        직접 확인(조사)하는 방식임.
 *    - 장점: 
 *      * 코드가 직관적이고 구현이 간단하며, 특정 조건 검사가 매 프레임 즉각적으로 이루어짐.
 *    - 단점: 
 *      * 매 프레임 입력을 체크하므로 이벤트 기반(Callback) 방식에 비해 미세한 자원 소모가 있을 수 있음.
 * 
 * 3. Mouse.current의 주요 속성 예시
 *    - Mouse.current.leftButton.wasPressedThisFrame: 이번 프레임에 마우스 왼쪽 버튼을 '처음 누른 순간' (True)
 *    - Mouse.current.leftButton.isPressed: 마우스 왼쪽 버튼을 '누르고 있는 동안' (True)
 *    - Mouse.current.leftButton.wasReleasedThisFrame: 이번 프레임에 마우스 왼쪽 버튼을 '뗀 순간' (True)
 *    - Mouse.current.position.ReadValue(): 현재 화면상에서 마우스 커서의 2D 좌표(Vector2)를 읽어옴
 * ==============================================================================
 */

using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Lecutre04_Clicker : MonoBehaviour
{
    // 유니티 에디터에서 드래그 앤 드롭으로 연결하여 화면에 텍스트를 표시할 TextMeshProUGUI 컴포넌트 변수임
    public TextMeshProUGUI clickText;

    // 마우스가 클릭된 횟수를 누적하여 저장할 정수형 변수임
    public int clickCount = 0;

    // Start 함수: 오브젝트가 생성된 후 첫 번째 프레임 업데이트 전에 단 한 번 실행됨
    void Start()
    {
        // 게임이 시작될 때 초기 클릭 횟수(0)를 화면에 곧바로 반영하기 위해 텍스트 업데이트 함수를 호출함
        UpdateClickText();
    }

    // Update 함수: 화면이 렌더링되는 매 프레임마다 반복해서 호출됨
    void Update()
    {
        // 1. 현재 PC에 마우스 장치가 정상적으로 연결되어 있는지(null이 아닌지) 안전하게 확인인함
        if (Mouse.current != null)
        {
            // 2. New Input System을 사용하여 마우스 왼쪽 버튼이 '이번 프레임에 막 눌렸는지'를 직접 감지함
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                // 클릭이 감지되었으므로 클릭 횟수를 1 증가시킴
                clickCount++;

                // 변경된 클릭 횟수를 화면의 텍스트에 즉시 반영함
                UpdateClickText();
            }
        }
    }

    // 클릭 횟수 데이터를 화면의 텍스트 UI에 반영해주는 커스텀 함수임
    void UpdateClickText()
    {
        // 텍스트 컴포넌트가 에디터에서 정상적으로 연결되어 있는지 확인(NullReferenceException 방지)함
        if (clickText != null)
        {
            // "Clicked Number: " 문자열 뒤에 현재 누적된 clickCount 숫자를 이어붙여 텍스트에 대입함
            clickText.text = "Clicked Number: " + clickCount;
        }
    }
}