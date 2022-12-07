using PEProtocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBase
{
    protected AniState currentState = AniState.None;
    public StateManager stateMgr;
    protected EntityController controller;
    public SkillManager skillMgr;
    public BattleManager battleMgr;
    protected string name;

    public Queue<int> comboQue = new Queue<int>();
    public int nextCombo;
    public SkillCfg currentSkillCfg;
    public string Name
    {
        get
        {
            return name;
        }
        set
        {
            name = value;
        }
    }

    protected BattleProps props;

    public BattleProps Props
    {
        get
        {
            return props;
        }
        protected set
        {
            props = value;
        }
    }

    private int hp;
    public int HP
    {
        get
        {
            return hp;
        }

        set
        {
            SetHpVal(hp, value);
            hp = value;
        }
    }
    public bool LockCtrl;
    public AniState CurrentState
    {
        get
        {
            return currentState;
        }

        set
        {
            currentState = value;
        }
    }

    public void SetController(EntityController controller)
    {
        this.controller = controller;
    }

    public void SetActive(bool isActive = true)
    {
        controller.gameObject.SetActive(isActive);
    }
    public AnimationClip GetAnimationClip(params string[] mask)
    {
        AnimationClip[] _clips = controller.anim.runtimeAnimatorController.animationClips;
        AnimationClip result = null;
        foreach (AnimationClip clip in _clips)
        {
            string name = clip.name;
            for(int i = 0; i < mask.Length; i++)
            {
                if(name.Contains(mask[i]))
                {
                    result = clip;
                    return clip;
                }
            }
        }
        return null;
    }

    public virtual void SetBattleProps(BattleProps props)
    {
        HP = props.hp;
        Props = props;
    }

    public void Move()
    {
        stateMgr.ChangeState(this, AniState.Move,null);
    }

    public void Idle()
    {
        stateMgr.ChangeState(this, AniState.Idle,null);
    }

    public void Attack(int skillId)
    {
        stateMgr.ChangeState(this, AniState.Attack, skillId);
    }

    public void Born()
    {
        stateMgr.ChangeState(this, AniState.Born);
    }

    public void Die()
    {
        stateMgr.ChangeState(this, AniState.Die);
    }

    public void Hit()
    {
        stateMgr.ChangeState(this, AniState.Hit);
    }
    public virtual void SetBlend(int blend)
    {
        if(controller != null)
        {
            controller.SetBlend(blend);
        }
    }

    public virtual void SetDir(Vector2 dir)
    {
        if (controller != null)
            controller.Dir = dir;
    }

    public virtual void SetAction(int action)
    {
        if (controller != null)
            controller.SetAction(action);
    }

    public virtual void SetFX(string name, float destroy)
    {
        if (controller != null)
            controller.SetFX(name, destroy);
    }

    public void SkillAttack(int skillId)
    {
        skillMgr.SkillAttack(this, skillId);
    }

    public void SetSkillMoveState(bool skillMove, float moveSpeed = 0)
    {
        if(controller != null)
        {
            controller.SetSkillMoveState(skillMove, moveSpeed);
        }
    }

    public void Lock()
    {
        LockCtrl = true;
        
    }

    public void UnLock()
    {
        LockCtrl = false;
    }

    public virtual Vector2 GetInputDir()
    {
        return Vector2.zero;
    }

    public virtual Vector3 GetPos()
    {
        return controller.transform.position;
    }

    public virtual Transform GetTrans()
    {
        return controller.transform;
    }

    public void SetDodge()
    {
        GameRoot.Instance.dynamicWin.SetDodge(controller.name);
    }

    public void SetHurt(int hurt)
    {
        GameRoot.Instance.dynamicWin.SetHurt(controller.name, hurt);
    }

    public void SetCritical()
    {
        GameRoot.Instance.dynamicWin.SetCritical(controller.name);
    }

    private void SetHpVal(int oldHp, int newHp)
    {
        GameRoot.Instance.dynamicWin.SetHpVal(controller.name, oldHp, newHp);
    }

    public void ExitCurrentAtk()
    {
        LockCtrl = false;
        if (currentSkillCfg.isCombo)
        {
            if (comboQue.Count > 0)
            {
                nextCombo = comboQue.Dequeue();
            }
            else
            {
                nextCombo = 0;
                currentSkillCfg = null;
            }
        }
        else
        {
            currentSkillCfg = null;
        }
    }

    public virtual void SetAtkRotation(Vector2 dir, bool offest = false)
    {
        if(controller != null)
        {
            if (offest)
                controller.SetAtkRotationCam(dir);
            else
                controller.SetAtkRotationLocal(dir);
        }
    }

    public virtual Vector2 GetClosedTarget()
    {
        return Vector2.zero;
    }

    public bool isAttack()
    {
        return currentSkillCfg != null;
    }

}
