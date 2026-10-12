using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Lecutre04_Clicker : MonoBehaviour
{
    // 유니티 에디터에서 드래그 앤 드롭으로 연결할 텍스트 컴포넌트
    public TextMeshProUGUI clickText;

    // 클릭 횟수를 저장할 변수
    public int clickCount = 0;

    void Start()
    {
        // 게임 시작 시 텍스트 초기화
        UpdateClickText();
    }

    void Update()
    {
        // 1. 현재 PC에 연결된 마우스 장치가 존재하는지 확인
        if (Mouse.current != null)
        {
            // 2. 마우스 왼쪽 버튼이 '이번 프레임에 눌렸는지' 매 프레임 직접 감지 (화면 전체 영역)
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                clickCount++;
                UpdateClickText();
            }
        }
    }

    // 텍스트를 화면에 업데이트하는 함수
    void UpdateClickText()
    {
        if (clickText != null)
        {
            clickText.text = "Clicked Number: " + clickCount;
        }
    }
}





