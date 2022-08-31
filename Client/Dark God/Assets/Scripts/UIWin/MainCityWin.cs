using PEProtocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainCityWin : WinRoot
{

    public Image imgDirBg;
    public Image imgDirPoint;
    public Image imgTouch;


    public Text txtFight;
    public Text txtPower;
    public Image imgPowerPrg;
    public Text txtLevel;
    public Text txtName;
    public Text txtExpPrg;

    public Transform expPrgTrans;
    public Animation menuRootAnim;

    private bool menuRootState = true;

    private float pointDis;
    private Vector2 startPos;
    private Vector2 defaultPos;
    protected override void InitWin()
    {
        base.InitWin();
        pointDis = Screen.height * 1f / Message.ScreenStandardHeight * Message.ScreenOPDis;
        defaultPos = imgDirBg.transform.position;
        SetActive(imgDirPoint, false);
        RefreshUI();
        RegisterTouchEvts();
    }

    private void RefreshUI()
    {
        PlayerData pd = GameRoot.Instance.PlayerData;
        SetText(txtFight, PECommon.GetFightByProps(pd));
        SetText(txtPower, "����" + pd.power + "/" + PECommon.GetPowerLimit(pd.lv));
        imgPowerPrg.fillAmount = pd.power * 1f / PECommon.GetPowerLimit(pd.lv);
        SetText(txtLevel, pd.lv);
        SetText(txtName, pd.name);

        
        int expPrgVal = (int)(pd.exp * 100f / PECommon.GetExpUpValByLv(pd.lv));
        Debug.Log(expPrgVal +" "+ pd.exp);
        SetText(txtExpPrg, expPrgVal + "%");

        //���þ�������ʾ�����㾭�����������Ӧ�����㾭����ʾ
        int index = expPrgVal / 10;

        GridLayoutGroup gird = expPrgTrans.GetComponent<GridLayoutGroup>();

        float globalRate = 1f * Message.ScreenStandardHeight / Screen.height;
        float screenWidth = Screen.width * globalRate;
        float Width = (screenWidth - 223) / 10;

        gird.cellSize = new Vector2(Width, 19);

        for (int i = 0; i < expPrgTrans.childCount; i++)
        {
            Image img = expPrgTrans.GetChild(i).GetComponent<Image>();
            if (i < index)
            {
                img.fillAmount = 1;
            }
            else if (i == index)
            {
                img.fillAmount = expPrgVal % 10 * 1f / 10;
            }
            else
                img.fillAmount = 0;
        }

    }

    public void OnClickMenuRoot()
    {
        menuRootState = !menuRootState;
        AnimationClip clip;
        if(menuRootState)
        {
            clip = menuRootAnim.GetClip("MenuOpen");
        }
        else
        {
            clip = menuRootAnim.GetClip("MenuClose");
        }
        audioSvc.PlayUIAudio(Message.UIExtenBtn);
        menuRootAnim.Play(clip.name);
    }

    public void RegisterTouchEvts()
    {
        OnClickDown(imgTouch.gameObject, (PointerEventData evt) => 
        {
            startPos = evt.position;
            SetActive(imgDirPoint);
            imgDirBg.transform.position = evt.position;
        });

        OnClickUp(imgTouch.gameObject, (PointerEventData evt) => 
        {
            imgDirBg.transform.position = defaultPos;
            SetActive(imgDirPoint, false);
            imgDirPoint.transform.localPosition = Vector2.zero;

        });

        OnDrag(imgTouch.gameObject, (PointerEventData evt) =>
        {
            Vector2 _dir = evt.position - startPos;
            float len = _dir.magnitude;
            if(len > pointDis)
            {
                Vector2 clampDir = Vector2.ClampMagnitude(_dir, Message.ScreenOPDis);
                imgDirPoint.transform.position = startPos + clampDir;
            }
            else
            {
                imgDirPoint.transform.position = evt.position;
            }
        });
    }
}