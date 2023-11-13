using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Animal : MonoBehaviour
{
    [SerializeField] private string animalName; // 동물의 이름
    public int hp;  // 동물의 체력

    [SerializeField] private float walkSpeed;  // 걷기 속력
    [SerializeField] private float runSpeed;  // 걷기 속력

    private Vector3 direction;  // 방향

    // 상태 변수
    private bool isAction;  // 행동 중인지 아닌지 판별
    private bool isWalking; // 걷는지, 안 걷는지 판별
    private bool isRunning;

    [SerializeField] private float walkTime;  // 걷기 시간
    [SerializeField] private float waitTime;  // 대기 시간
    [SerializeField] private float runTime;  // 뛰는 시간
    private float currentTime;

    // 필요한 컴포넌트
    private Animator anim;
    private Rigidbody rigidl;
    private BoxCollider boxCol;

    protected Vector3 destination;  // 목적지
    protected NavMeshAgent nav;

    public LayerMask PlayerLayer;

    public float viewDistance;
    public float viewAngle;

    private float hpcul = 0;

    void Start()
    {
        currentTime = waitTime;   // 대기 시작
        isAction = true;   // 대기도 행동
        anim = GetComponent<Animator>();
        rigidl = GetComponent<Rigidbody>();
        boxCol = GetComponent<BoxCollider>();
        nav = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (hp > 0)
        {
            Move();
            ElapseTime();
            View();
        }
        if (hpcul > 0)
        {
            hpcul -= Time.deltaTime;
        }
    }

    private void Move()
    {
        if (isWalking || isRunning)
            nav.SetDestination(transform.position + destination * 5f);
    }

    private void ElapseTime()
    {
        if (isAction)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)  // 랜덤하게 다음 행동을 개시
                ReSet();
        }
    }

    private void ReSet()  // 다음 행동 준비
    {
        isAction = true;

        nav.ResetPath();

        isWalking = false;
        anim.SetBool("isWalk", isWalking);
        isRunning = false;
        anim.SetBool("isRun", isRunning);
        nav.speed = walkSpeed;

        destination.Set(Random.Range(-0.2f, 0.2f), 0f, Random.Range(0.5f, 1f));
        RandomAction();
    }

    private void RandomAction()
    {
        int _random = Random.Range(0, 3); // 대기, 풀뜯기, 두리번, 걷기
        anim.SetBool("isEat", false);

        if (_random == 0)
            Wait();
        else if (_random == 1)
            Eat();
        else if (_random == 2)
            TryWalk();
    }

    private void Wait()  // 대기
    {
        currentTime = waitTime;
        rigidl.velocity = Vector3.zero;
        rigidl.useGravity = false;
    }

    private void Eat()  // 풀 뜯기
    {
        currentTime = waitTime;
        rigidl.velocity = Vector3.zero;
        rigidl.useGravity = false;
        anim.SetBool("isEat",true);
    }

    private void TryWalk()  // 걷기
    {
        rigidl.useGravity = true;
        currentTime = walkTime;
        isWalking = true;
        anim.SetBool("isWalk", isWalking);
        nav.speed = walkSpeed;
    }

    public void Run(Vector3 _targetPos)
    {
        rigidl.useGravity = true;
        destination = new Vector3(transform.position.x - _targetPos.x, 0f, transform.position.z - _targetPos.z).normalized;

        currentTime = runTime;
        isWalking = false;
        isRunning = true;
        nav.speed = runSpeed;

        anim.SetBool("isRun", isRunning);
    }

    public bool View()
    {
        Collider[] _target = Physics.OverlapSphere(transform.position, viewDistance, PlayerLayer);

        for (int i = 0; i < _target.Length; i++)
        {
            Transform _targetTf = _target[i].transform;
            if (_targetTf.name == "Player")
            {
                Vector3 _direction = (_targetTf.position - transform.position).normalized;
                float _angle = Vector3.Angle(_direction, transform.forward);

                if (_angle < viewAngle)
                {
                    RaycastHit _hit;
                    if (Physics.Raycast(transform.position + transform.up, _direction, out _hit, viewDistance))
                    {
                        if (_hit.transform.name == "Player" && !isRunning)
                        {
                            Debug.DrawRay(transform.position + transform.up, _direction, Color.blue);

                            Run(_targetTf.transform.position);


                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }
    public void Dead()
    {
        rigidl.useGravity = false;
        rigidl.velocity = Vector3.zero;
        nav.ResetPath();
        anim.SetTrigger("isDie");
        gameObject.layer = LayerMask.NameToLayer("Item");
    }

    public void hit()
    {
        if (hpcul <= 0)
        {
            hpcul = 1;
            hp -= 1;
            Run(GameObject.FindGameObjectWithTag("Player").transform.position);
            if (hp == 0)
            {
                Dead();
            }
        }
    }
}