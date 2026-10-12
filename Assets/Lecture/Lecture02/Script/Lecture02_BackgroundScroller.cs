/*
 * ==============================================================================
 * [Unity] Transform 컴포넌트 및 이동/회전/확대축소 함수 상세 해설
 * ==============================================================================
 * 
 * 1. Transform 컴포넌트 개요
 *    - 모든 게임 오브젝트는 공간 상에 존재하기 위해 Transform 컴포넌트를 필수적으로 가짐.
 *    - 3차원 공간에서의 위치(Position), 회전(Rotation), 크기(Scale) 정보를 관리함.
 *    - 부모-자식(Hierarchy) 계층 구조를 형성하여 상속 관계에 따른 변환을 처리함.
 * 
 * 2. 주요 Transform 함수 및 속성
 *    - 위치 제어 (Position & Translate):
 *      1) transform.position: 월드(World) 좌표계 기준의 절대 위치를 나타냄.
 *      2) transform.localPosition: 부모 오브젝트 기준의 상대 위치를 나타냄.
 *      3) transform.Translate(Vector3 translation): 현재 위치를 기준으로 지정된 방향과 
 *         거리만큼 상대적으로 이동시킴. (오브젝트 자체의 로컬 축 또는 월드 축 기준 이동 가능)
 *    - 회전 제어 (Rotation & Rotate):
 *      1) transform.rotation: 쿼터니언(Quaternion) 형태의 절대 회전값을 관리함.
 *      2) transform.eulerAngles: 사람이 읽기 쉬운 오일러 각도(x, y, z 각도)로 회전값을 제어함.
 *      3) transform.Rotate(Vector3 eulerAngles): 현재 각도를 기준으로 지정된 값만큼 회전시킴.
 *    - 확대/축소 제어 (Scale):
 *      1) transform.localScale: 부모 오브젝트 기준의 상대적 크기(스케일)를 조절함.
 * 
 * 3. 배경 스크롤러(Background Scroller) 구현 원리
 *    - 배경 오브젝트를 매 프레임 일정한 속도로 왼쪽(-x 방향)으로 이동시킴.
 *    - 특정 지점(-15.0f)을 넘어가면 반대편 오른쪽(15.0f)으로 좌표를 강제로 재배치하여 
 *      끊기지 않는 무한 반복 배경(Infinite Scrolling) 효과를 만듦.
 * ==============================================================================
 */

using UnityEngine;

public class Lecture02_BackgroundScroller : MonoBehaviour
{
    // 게임이 시작되었는지 여부를 결정하는 불리언 플래그 변수임
    public bool isGameStarted = false;

    // 스크롤된 거리를 누적하여 기록하기 위한 변수임 (현재 예제에서는 선언만 됨)
    public float scrollDistance = 0;

    // 배경이 이동하는 속도를 조절하는 변수임
    public float speed = 1;

    // Start 함수: 게임 오브젝트 생성 직후 첫 번째 Update 호출 전에 단 한 번 실행됨
    void Start()
    {
        // 초기화 단계에서 처리할 별도의 로직이 없어 비워둠
    }

    // Update 함수: 화면이 렌더링되는 매 프레임마다 반복해서 호출됨
    void Update()
    {
        // 게임이 시작된 상태(true)일 때만 배경을 움직임
        if (isGameStarted)
        {
            // transform.Translate를 사용해 현재 위치에서 x축 방향으로 -speed만큼 이동시킴
            transform.Translate(-speed, 0, 0);

            // 배경이 왼쪽으로 너무 많이 이동해서 지정된 한계선(-15.0f)을 벗어났는지 검사함
            if (transform.position.x < -15.0f)
            {
                // 화면 오른쪽 끝(15.0f) 위치로 좌표를 순간 이동시켜 다시 왼쪽으로 흘러오게 만듦
                transform.position = new Vector3(15.0f, 0f, 0f);
            }
        }
    }
}