/*
 * ==============================================================================
 * [Unity 6] GameObject.SetActive() 상세 해설 및 라이프사이클 가이드
 * ==============================================================================
 * 
 * 1. 개요 및 기능 (What is it?)
 *    - GameObject.SetActive(bool value)는 해당 게임 오브젝트의 활성화 상태(Active State)를 
 *      직접 켜거나(true) 끄는(false) 함수임.
 *    - 오브젝트가 비활성화되면 씬(Scene)에서 렌더링되지 않고, 컴포넌트들의 Update 등의 
 *      생명주기 함수가 호출되지 않으며, 물리 연산(Physics) 및 충돌 검사에서도 제외됨.
 * 
 * 2. 왜 사용하는가? (Why use it?)
 *    - 성능 최적화: 화면에 보이지 않거나 현재 사용되지 않는 오브젝트(예: 인벤토리 UI, 풀링된 적 캐릭터 등)의 
 *      불필요한 CPU 연산(Update 등)을 차단하여 자원을 절약함.
 *    - 게임 로직 제어: 특정 조건(퀘스트 완료, 아이템 획득 등)에 따라 오브젝트를 등장시키거나 
 *      숨기는 연출 및 시스템 구현에 필수적임.
 * 
 * 3. 유니티 라이프사이클과의 관계 (Lifecycle & Callbacks)
 *    - 활성화 시점 (SetActive(true) 호출 직후):
 *      1) 해당 오브젝트와 그 자식 오브젝트들에 부착된 모든 활성화된 컴포넌트의 OnEnable()이 호출됨.
 *      2) 이후 첫 번째 Update() 호출 전에 Start()가 실행되지 않았다면 Start()가 호출됨.
 *    - 비활성화 시점 (SetActive(false) 호출 직후):
 *      1) 즉시 해당 오브젝트와 자식 컴포넌트들의 OnDisable()이 호출됨.
 *      2) 이후 Update, FixedUpdate, LateUpdate 등의 프레임 루프에서 완전히 제외됨.
 * 
 * 4. 주의사항 (Unity 6 최적화 팁)
 *    - 매 프레임마다 동일한 상태(true 또는 false)를 반복해서 SetActive에 전달하는 것은 
 *      내부적인 상태 변경 체크 비용을 발생시킬 수 있으므로, 상태가 바뀔 때만 호출하는 것이 바람직함.
 *    - 부모 오브젝트가 비활성화되면, 자식 오브젝트는 자신의 activeSelf가 true여도 
 *      hierarchy 상에서 비활성화(activeInHierarchy = false) 상태가 됨.
 * ==============================================================================
 */

using UnityEngine;

public class Lecture03_SetActive : MonoBehaviour
{
    // 인스펙터 창에서 활성화/비활성을 제어할 대상 게임 오브젝트를 할당받는 변수임
    public GameObject activeGameObject;

    // 대상 오브젝트의 켜고 끌 여부를 결정하는 스위치 역할을 하는 불리언 변수임
    public bool isActive = true;

    // Start 함수: 게임 오브젝트가 생성된 후 첫 번째 Update 실행 전에 단 한 번 호출됨
    void Start()
    {
        // 현재 예제 코드에서는 초기화 단계에서 수행할 별도의 로직이 없음
    }

    // Update 함수: 매 프레임마다 반복해서 호출됨
    void Update()
    {
        // 매 프레임마다 isActive 변수 상태에 맞춰 지정된 게임 오브젝트를 켜거나 끔
        // (실무에서는 값이 변경될 때만 호출하도록 최적화하는 경우가 많음)
        activeGameObject.SetActive(isActive);
    }
}