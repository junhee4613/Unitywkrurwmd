using System.Collections;
// ������̼Ǳ��������ϱ������߰��ؾ��ϴ³��ӽ����̽�
public class MonsterCtrl : MonoBehaviour
{
    // �����ǻ�������
    public enum State
    {
        IDLE,
        TRACE,
        ATTACK,
        DIE
    }
    // �������������
    public State state = State.IDLE;
    // ���������Ÿ�
    public float traceDist = 10.0f;
    // ���ݻ����Ÿ�
    public float attackDist = 2.0f;
    // �����ǻ������
    public bool isDie = false;
    // ������Ʈ��ĳ�ø�ó���Һ���
    private Transform monsterTr;
    private Transform playerTr;
    private NavMeshAgent agent;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        // ������Transform �Ҵ�
        monsterTr = GetComponent<Transform>();
        // ���������Player��Transform �Ҵ�
        playerTr = GameObject.FindWithTag("PLAYER").GetComponent<Transform>();
        // NavMeshAgent������Ʈ�Ҵ�
        agent = GetComponent<NavMeshAgent>();
        // �����������ġ�������ϸ�ٷ���������
        //agent.destination= playerTr.position;
        // �����ǻ��¸�üũ�ϴ��ڷ�ƾ�Լ�ȣ��
        StartCoroutine(CheckMonsterState());
    }
    // �����Ѱ������θ������ൿ���¸�üũ
    IEnumerator CheckMonsterState()
    {
        while (!isDie)
        {
            // 0.3�ʵ�������(���)�ϴµ�����������޽����������纸
            yield return new WaitForSeconds(0.3f);
            // ���Ϳ����ΰ�ĳ���ͻ����ǰŸ�����
            float distance = Vector3.Distance(playerTr.position, monsterTr.position);
            // ���ݻ����Ÿ������ε��Դ���Ȯ��
            if (distance <= attackDist)
            {
                state = State.ATTACK;
            }
            // ���������Ÿ������ε��Դ���Ȯ��
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
                     // IDLE ����
                    caseState.IDLE:
                         // ��������
                        agent.isStopped = true;
            // Animator��IsTrace ������false�μ���
            anim.SetBool("IsTrace", false);
            break;
            // ��������
            caseState.TRACE:
             // �����������ǥ���̵�����
                    agent.SetDestination(playerTr.position);
            agent.isStopped = false;
            // Animator��IsTrace ������true�μ���
            anim.SetBool("IsTrace", true);
            break;
            // ���ݻ���
            caseState.ATTACK:
                break;
            // ���
            caseState.DIE:
                break;
        }
        yieldreturnnewWaitForSeconds(0.3f);
    }
}
void OnDrawGizmos()
{
    // ���������Ÿ�ǥ��
    if (state == State.TRACE)
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, traceDist);
    }
    // ���ݻ����Ÿ�ǥ��
    if (state == State.ATTACK)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDist);
    }
}

}