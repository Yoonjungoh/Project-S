using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : CreatureController
{
    private UI_EnemyHpBar _hpBar;

    protected PlayerController _target;
    protected bool _canAttack = true;
    protected int _monsterId;
    public AudioSource AudioSource;

    public int MonsterId { get { return _monsterId; } set { _monsterId = value; } }
    public PlayerController Target { get { return _target; } set { _target = value; } }
    void Start()
    {
        Init();
    }
    protected override void Init()
    {
        base.Init();
        InitStat();
        // TODO - 서버에서 변경
        //State = CreatureState.Idle;
        _hpBar = Managers.Resource.InstantiateResources("UI_EnemyHpBar").GetComponent<UI_EnemyHpBar>();
        _hpBar.Monster = this;
        AudioSource = GetComponent<AudioSource>();
    }
    protected override void UpdateAnimation()
    {
        if (_animator == null)
            return;

        /*if (State == CreatureState.Idle)
        {
            _animator.Play($"MONSTER_{MonsterId}_IDLE");
        }
        // 우선 MOVING도 IDLE로 하기
        else if (State == CreatureState.Moving)
        {
            _animator.Play($"MONSTER_{MonsterId}_IDLE");
        }
        // 우선 CHASING도 IDLE로 하기
        else if (State == CreatureState.Chasing)
        {
            _animator.Play($"MONSTER_{MonsterId}_CHASING");
        }
        // 몬스터의 기본 공격은 SKILL로 하기
        else if (State == CreatureState.Skill)
        {
            _animator.Play($"MONSTER_{MonsterId}_ATTACK");
        }
        else if (State == CreatureState.Dead)
        {
            _animator.Play($"MONSTER_{MonsterId}_DEAD");
        }*/

        switch (State)
        {
            case CreatureState.Idle:
                //_animator.Play($"PLAYER_IDLE_{(int)WeaponType}");
                _animator.SetBool("isMoving", false);
                break;
            case CreatureState.Moving:
                //_animator.Play($"PLAYER_MOVE_{(int)WeaponType}");
                _animator.SetBool("isMoving", true);
                break;
            case CreatureState.Skill:
                // 여기는 attack 한번만 하는 걸로 하는게 좋을듯
                //UseSkill에서 실행
                break;
            case CreatureState.Dead:
                _animator.SetBool("isMoving", false);
                break;
        }
    }

    protected override void UpdateController()
    {
        base.UpdateController();
    }

    protected void InitStat()
    {
        // TODO - 나중에 json으로 받아오기
        //Stat.Hp = 50;
        //Stat.Attack = 7;
        //Stat.Defense = 0;
        //Stat.Speed = 3;
    }

    /*protected void Attack()
    {
        StartCoroutine(CoAttack());
    }

    protected IEnumerator CoAttack()
    {
        _canAttack = false;
        //if(_scanner._nearestTarget)
        //    _scanner._nearestTarget.GetComponent<PlayerController>().OnDamaged(Stat.Attack);
        Debug.Log(this + " hits " + _target.transform.name);

        yield return new WaitForSeconds(1f);

        _canAttack = true; 
        // TODO - 서버에서 변경
        //State = CreatureState.Moving;
    }*/

    public override void UseSkill(int skillId)
    {
        base.UseSkill(skillId);
        _animator.SetBool("isMoving", false);
        _animator.SetTrigger("doAttack");
    }
    public override void OnDead()
    {
        base.OnDead();
        if (_hpBar != null)
            Destroy(_hpBar.gameObject);
    }
}