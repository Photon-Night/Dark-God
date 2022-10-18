using PEProtocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    ResService resSvc;

    public void InitManager()
    {
        resSvc = ResService.Instance;
        PECommon.Log("SkillManager Loading");
    }
    /// <summary>
    /// ���������߼�
    /// </summary>
    public void AttackEffect(EntityBase entity, int skillId)
    {
        SkillCfg data = resSvc.GetSkillData(skillId);

        if(data != null)
        {
            entity.SetAction(data.aniAction);
            entity.SetFX(data.fx, data.skillTime);
        }
    }
}
