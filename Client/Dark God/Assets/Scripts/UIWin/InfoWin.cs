using PEProtocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoWin : WinRoot
{
    #region UIDefine
    public Text txtInfo;
    public Text txtExp;
    public Image imgExpPrg;
    public Text txtPower;
    public Image imgPowerPrg;

    public Text txtJob;
    public Text txtFight;
    public Text txtHP;
    public Text txtHurt;
    public Text txtDef;

    #endregion
    protected override void InitWin()
    {
        base.InitWin();
        RefreshUI();
    }

    private void RefreshUI()
    {
        PlayerData pd = GameRoot.Instance.PlayerData;
        SetText(txtInfo, pd.name + " LV." + pd.lv);
        SetText(txtExp, pd.exp + "/" + PECommon.GetExpUpValByLv(pd.lv));
        imgExpPrg.fillAmount = pd.exp * 1f / PECommon.GetExpUpValByLv(pd.lv);
        SetText(txtPower, pd.power + "/" + PECommon.GetPowerLimit(pd.lv));
        imgPowerPrg.fillAmount = pd.power * 1f / PECommon.GetPowerLimit(pd.lv);

        SetText(txtJob, "ְҵ        " + "��ҹ�̿�");
        SetText(txtFight, "ս��        " + PECommon.GetFightByProps(pd));
        SetText(txtHP, "����        " + pd.hp);
        SetText(txtHurt, "�˺�        " + (pd.ad + pd.ap));
        SetText(txtDef, "����        " + (pd.apdef + pd.apdef));
    }

    public void OnClickCloseBtn()
    {
        audioSvc.PlayUIAudio(Message.UIClickBtn);
        SetWinState(false);
    }
}
