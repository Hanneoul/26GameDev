using UnityEngine;

/*
====================================================================================================
 [Unity 2D Physics & Scripting API Reference]
====================================================================================================

1. Rigidbody2D 심층 분석 (2D 물리 컴포넌트)
   - 역할: 오브젝트에 질량, 마찰력, 중력 등 2D 물리 특성을 부여하고 Box2D 물리 엔진의 제어를 받게 함.
   - 3D Rigidbody와의 차이점: 
     * 연산 축이 X, Y (2D) 중심이며, Z축 연산이 배제되어 연산 부하가 적음.
     * 3D Physics(PhysX)와 2D Physics(Box2D)는 내부 엔진이 완전히 분리되어 있어, 
       Rigidbody(3D)와 Collider2D(2D)처럼 서로 다른 차원의 컴포넌트는 상호작용하지 못함.
   - BodyType (주요 세 가지 물리 모드):
     1) Dynamic: 중력, 힘, 충돌 반응 등 모든 물리 법칙의 영향을 받음 (주인공, 적 캐릭터 등).
     2) Kinematic: 중력이나 충돌의 힘을 받지 않으며, 코드로 직접 속도/위치를 제어함 (움직이는 발판 등).
     3) Static: 완전히 고정된 오브젝트로 물리 연산에서 이동하지 않음 (바닥, 벽 등).
   - Interpolation (보간):
     * Physics Step(FixedUpdate)과 Rendering Step(Update) 간의 주기가 달라 발생하는 화면 떨림(Jittering)을 보정함.
     * Interpolate(이전 프레임 기반 보간) 또는 Extrapolate(다음 프레임 예측 보간)를 설정하여 부드러운 움직임 구현.
   - Collision Detection Mode:
     * Discrete: 매 고정 프레임마다 충돌 검사 (기본값, 성능 최적화).
     * Continuous: 빠른 속도로 이동하는 오브젝트가 바닥을 뚫고 지나가는 현상(Tunneling)을 방지함.

2. RigidbodyConstraints2D Enum 상세 분석 (수정된 제약 조건 설정)
   - 역할: 2D 물리 연산 과정에서 특정 축의 이동(Translation)이나 회전(Rotation)을 물리 엔진이 제어하지 못하도록 고정(Freeze)함.
   - 주요 Enum 멤버 (비트 마스크 플래그 방식):
     * RigidbodyConstraints2D.None: 아무런 제약을 두지 않음 (모든 축 이동 및 회전 허용).
     * RigidbodyConstraints2D.FreezePositionX: X축(수평) 이동 고정.
     * RigidbodyConstraints2D.FreezePositionY: Y축(수직) 이동 고정.
     * RigidbodyConstraints2D.FreezePosition: X축과 Y축 이동을 모두 고정.
     * RigidbodyConstraints2D.FreezeRotation: Z축 회전만 고정 (2D 캐릭터가 충돌 시 넘어지는 현상 방지).
     * RigidbodyConstraints2D.FreezeAll: 위치 이동과 회전을 모두 고정.
   - 연산 팁: 플래그(Flag) enum이므로 비트 OR 연산자(`|`)를 통해 여러 제약을 동시에 적용 가능.
     (예: RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation)

3. 사용된 주요 유니티 API 및 함수 상세 기찰
   - [RequireComponent(typeof(Rigidbody2D))]:
     * 에디터 레벨 속성. 스크립트 부착 시 Rigidbody2D 컴포넌트가 없으면 자동으로 추가하여 컴포넌트 누락 방지.
   - TryGetComponent<T>(out T component):
     * C# 7.0 이상의 out 키워드를 활용한 컴포넌트 검색 API.
     * 기존 GetComponent<T>() != null 방식과 달리, GC Alloc(메모리 할당)을 유발하지 않고 
       성공 여부를 bool로 반환하여 성능상 이점을 제공함.
   - gameObject.AddComponent<T>():
     * 런타임 환경에서 지정한 게임 오브젝트에 동적으로 컴포넌트를 부착함.
   - Input.GetMouseButtonDown(0):
     * 마우스 좌클릭(0번 버튼)이 발생한 해당 프레임에만 true를 반환하는 단발성 입력 감지 함수.
   - Physics2D.Raycast(Vector2 origin, Vector2 direction, float distance, int layerMask):
     * 특정 원점에서 지정한 방향과 거리만큼 광선(Ray)을 쏘아 충돌한 Collider2D 정보를 검출함.
     * 결과값으로 RaycastHit2D 구조체를 반환하며, bool(implicit conversion) 형태로 충돌 여부 판단 가능.
   - rb2d.linearVelocity:
     * Unity 6(6000.x) 표준 API. Rigidbody2D의 선형 속도 Vector2(x, y)를 제어함.
     * 이전 버전의 rb2d.velocity를 대체함.
   - rb2d.AddForce(Vector2 force, ForceMode2D mode):
     * Rigidbody2D에 물리적인 힘을 가함.
     * ForceMode2D.Impulse: 질량을 고려하여 순간적으로 폭발적인 힘을 가할 때 사용 (점프, 충격 등).
     * ForceMode2D.Force: 지속적인 힘을 가할 때 사용 (추진력, 가속 등).

====================================================================================================
*/

[RequireComponent(typeof(Rigidbody2D))]
public class MouseClickJump2D : MonoBehaviour
{
    [Header("Jump Settings")]
    [Tooltip("점프 시 가해지는 2D 물리 힘의 크기")]
    [SerializeField] private float jumpForce = 7.0f;

    [Header("Ground Check Settings")]
    [Tooltip("지면으로 인식할 LayerMask")]
    [SerializeField] private LayerMask groundLayer;
    [Tooltip("지면 검사 2D Raycast 거리 (오브젝트 하단 위치 기준)")]
    [SerializeField] private float groundCheckDistance = 1.1f;

    // Component Reference & State Flags
    private Rigidbody2D rb2d;
    private bool isGrounded;
    private bool jumpRequested;

    private void Awake()
    {
        // 런타임 안전장치: Rigidbody2D 컴포넌트 존재 유무 검사 및 동적 추가
        EnsureRigidbody2D();
    }

    /// <summary>
    /// Rigidbody2D 컴포넌트를 검사하고 누락 시 코드로 자동 생성 및 초기 옵션을 설정하는 보장 함수
    /// </summary>
    private void EnsureRigidbody2D()
    {
        if (!TryGetComponent(out rb2d))
        {
            rb2d = gameObject.AddComponent<Rigidbody2D>();

            // 2D 게임 기본 옵션 설정: Z축 회전 고정 (올바른 Enum 명칭: RigidbodyConstraints2D)
            rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;

            // 물리 연산 반응 방식 설정 (화면 떨림 완화)
            rb2d.interpolation = RigidbodyInterpolation2D.Interpolate;

            Debug.LogWarning($"[{gameObject.name}] Rigidbody2D 컴포넌트가 없어 런타임에서 자동 추가 및 초기화됨.");
        }
    }

    private void Update()
    {
        // 1. 사용자 입력 감지 (가변 프레임 루프)
        if (Input.GetMouseButtonDown(0) && isGrounded)
        {
            jumpRequested = true;
        }

        // 2. 2D 지면 검사 (Physics2D.Raycast 사용)
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
        isGrounded = hit.collider != null;

#if UNITY_EDITOR
        // 에디터 씬 뷰 시각화
        Debug.DrawRay(transform.position, Vector2.down * groundCheckDistance, isGrounded ? Color.green : Color.red);
#endif
    }

    private void FixedUpdate()
    {
        // 3. 2D 물리 연산 실행 (고정 프레임 루프)
        if (jumpRequested)
        {
            PerformJump2D();
            jumpRequested = false;
        }
    }

    private void PerformJump2D()
    {
        if (rb2d == null) return;

        // Y축 기존 속도를 0으로 초기화하여 중력 낙하 중이어도 일정한 높이로 점프하도록 보장
        rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, 0f);

        // 2D 수평/수직 벡터 중 상향(Vector2.up) 방향으로 순간적인 힘 적용
        rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}