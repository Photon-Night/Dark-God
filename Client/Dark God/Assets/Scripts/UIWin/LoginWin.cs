using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PEProtocol;

public class LoginWin : WinRoot
{
    public InputField accInput;
    public InputField pasInput;

    public Button loginButton;

    protected override void InitWin()
    {
        base.InitWin();
        
        if(PlayerPrefs.GetString("acc") != null && PlayerPrefs.GetString("pas") != null)
        {
            accInput.text = PlayerPrefs.GetString("acc");
            pasInput.text = PlayerPrefs.GetString("pas");
        }
        else
        {
            accInput.text = "";
            pasInput.text = "";
        }
    }

    public void ClickEnterBtn()
    {
        audioSvc.PlayUIAudio(Message.UILoginBtn);
        string _acc = accInput.text;
        string _pas = pasInput.text;

        if (_acc != "" && _pas != "")
        {
            PlayerPrefs.SetString("acc", _acc);
            PlayerPrefs.SetString("pas", _pas);
            GameMsg msg = new GameMsg
            {
                cmd = (int)CMD.ReqLogin,
                reqLogin = new ReqLogin 
                {
                    acc = _acc,
                    pas = _pas
                }
            };

            netSvc.SendMessage(msg);
        }

        else
            GameRoot.AddTips("�������˺�����");
    }
   
}
