using PEProtocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    ResService resSvc;
    TimerService timer;

    public void InitManager()
    {
        resSvc = ResService.Instance;
        timer = TimerService.Instance;
        PECommon.Log("SkillManager Loading");
    }
    /// <summary>
    /// ���������߼�
    /// </summary>
    public void AttackEffect(EntityBase entity, int skillId)
    {
        SkillCfg data_skill = resSvc.GetSkillData(skillId);

        SetSkillMove(entity, data_skill);
    }

    private void SetSkillMove(EntityBase entity, SkillCfg data_skill)
    {
        
        if (data_skill != null)
        {
            entity.LockCtrl = true;
            entity.SetDir(Vector2.zero);
            entity.Lock();
            float sumTime = 0;
            for (int i = 0; i < data_skill.skillMoveLst.Count; i++)
            {
                SkillMoveCfg data_skillMove = resSvc.GetSkillMoveCfg(data_skill.skillMoveLst[i]);
                float _speed = data_skillMove.moveDis / (data_skillMove.moveTime / 1000f);
                
                entity.SetAction(data_skill.aniAction);
                entity.SetFX(data_skill.fx, data_skill.skillTime);
                sumTime += data_skillMove.delayTime;

                if (sumTime > 0)
                {
                    timer.AddTimeTask((int tid) =>
                    {
                        entity.SetSkillMoveState(true, _speed);
                    }, data_skillMove.delayTime);
                }
                else
                {
                    entity.SetSkillMoveState(true, _speed);
                }

                sumTime += data_skillMove.moveTime;

                timer.AddTimeTask((int tid) =>
                {
                    entity.SetSkillMoveState(false);
                }, sumTime);

                timer.AddTimeTask((int tid) =>
                {
                    entity.Idle();
                }, data_skill.skillTime);
            }

        }
    }
}
