using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PEProtocol;

public class MainCitySystem : SystemRoot
{
    public static MainCitySystem Instance = null;
    public MainCityWin mainCityWin;
    public override void InitSystem()
    {
        base.InitSystem();

        Instance = this;
        PECommon.Log("MainCitySystem Loading");
    }

    public void EnterMainCity()
    {
        resSvc.LoadSceneAsync(Message.SceneMainCity, () =>
        {
            PECommon.Log("Enter MainCity");

            //TODO ���ؽ�ɫģ��

            //��������ui
            mainCityWin.SetWinState();

            audioSvc.PlayerBGMusic(Message.BGMMainCity);

            //TODO ��������
        });
    }
}
