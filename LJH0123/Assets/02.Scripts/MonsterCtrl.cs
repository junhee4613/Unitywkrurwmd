using System.Collections;
// 내비게이션기능을사용하기위해추가해야하는네임스페이스
public class MonsterCtrl : MonoBehaviour
{
    // 몬스터의상태정보
    public enum State
    {
        IDLE,
        TRACE,
        ATTACK,
        DIE
    }
    // 몬스터의현재상태
    public State state = State.IDLE;
    // 추적사정거리
    public float traceDist = 10.0f;
    // 공격사정거리
    public float attackDist = 2.0f;
    // 몬스터의사망여부
    public bool isDie = false;
    // 컴포넌트의캐시를처리할변수
    private Transform monsterTr;
    private Transform playerTr;
    private NavMeshAgent agent;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        // 몬스터의Transform 할당
        monsterTr = GetComponent<Transform>();
        // 추적대상인Player의Transform 할당
        playerTr = GameObject.FindWithTag("PLAYER").GetComponent<Transform>();
        // NavMeshAgent컴포넌트할당
        agent = GetComponent<NavMeshAgent>();
        // 추적대상의위치를설정하면바로추적시작
        //agent.destination= playerTr.position;
        // 몬스터의상태를체크하는코루틴함수호출
        StartCoroutine(CheckMonsterState());
    }
    // 일정한간격으로몬스터의행동상태를체크
    IEnumerator CheckMonsterState()
    {
        while (!isDie)
        {
            // 0.3초동안중지(대기)하는동안제어권을메시지루프에양보
            yield return new WaitForSeconds(0.3f);
            // 몬스터와주인공캐릭터사이의거리측정
            float distance = Vector3.Distance(playerTr.position, monsterTr.position);
            // 공격사정거리범위로들어왔는지확인
            if (distance <= attackDist)
            {
                state = State.ATTACK;
            }
            // 추적사정거리범위로들어왔는지확인
            else if (distance <= traceDist)
            {
                state = State.TRACE;
            }
            else
            {
                state = State.IDLE;
            }
        }
    }
    IEnumerator MonsterAction()
    {
        while (!isDie)
        {
            switch (state)
            {
                     // IDLE 상태
                    caseState.IDLE:
                         // 추적중지
                        agent.isStopped = true;
            // Animator의IsTrace 변수를false로설정
            anim.SetBool("IsTrace", false);
            break;
            // 추적상태
            caseState.TRACE:
             // 추적대상의좌표로이동시작
                    agent.SetDestination(playerTr.position);
            agent.isStopped = false;
            // Animator의IsTrace 변수를true로설정
            anim.SetBool("IsTrace", true);
            break;
            // 공격상태
            caseState.ATTACK:
                break;
            // 사망
            caseState.DIE:
                break;
        }
        yieldreturnnewWaitForSeconds(0.3f);
    }
}
void OnDrawGizmos()
{
    // 추적사정거리표시
    if (state == State.TRACE)
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, traceDist);
    }
    // 공격사정거리표시
    if (state == State.ATTACK)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDist);
    }
}

}