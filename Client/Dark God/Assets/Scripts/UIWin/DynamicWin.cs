using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicWin : WinRoot
{
    public Animation tipsAnim;
    public Text txtTips;

    public Transform hpItemRoot;

    public Text txtPlayerDodge;
    public Animation playerDodge;

    private bool isTipsShow;

    private Queue<string> tipsQue = new Queue<string>();
    private Dictionary<string, ItemEntityHP> hpUIItemDic = new Dictionary<string, ItemEntityHP>();
    protected override void InitWin()
    {
        base.InitWin();

        SetActive(txtTips, false);
        SetActive(txtPlayerDodge, false);
    }

    public void AddTips(string tips)
    {
        lock(tipsQue)
        {
            tipsQue.Enqueue(tips);
        }
    }

    private void Update()
    {
         if(tipsQue.Count > 0 && !isTipsShow)
        {
                
            lock(tipsQue)
            {
                string tip = tipsQue.Dequeue();
                isTipsShow = true;
                SetTips(tip);
            }
        }
    }

    public void SetTips(string tips)
    {
        SetActive(txtTips);
        SetText(txtTips, tips);
        AnimationClip clip = tipsAnim.GetClip("TipEnter");
        tipsAnim.Play();

        StartCoroutine(AnimPlayDone(clip.length, () =>
        {
            SetActive(txtTips, false);
            isTipsShow = false;
        }));
    }

    private IEnumerator AnimPlayDone(float sec, System.Action cb)
    {
        yield return new WaitForSeconds(sec);
        if(cb != null)
        {
            cb();
        }
    }

    public void AddHpUIItem(string name, int hp, Transform trans)
    {
        ItemEntityHP item = null;
        if(hpUIItemDic.TryGetValue(name, out item))
        {
            return;
        }

        GameObject go = resSvc.LoadPrefab(PathDefine.HpUIItem, true);
        go.transform.SetParent(hpItemRoot);
        go.transform.localPosition = new Vector3(-1000, 0, 0);
        item = go.GetComponent<ItemEntityHP>();
        item.InitItemInfo(hp, trans);

        hpUIItemDic.Add(name, item);
    }

    public void ReMoveHpUIItem(string name)
    {
        ItemEntityHP _uiItem = null;
        if(hpUIItemDic.TryGetValue(name, out _uiItem))
        {
            Destroy(_uiItem.gameObject);
            hpUIItemDic.Remove(name);
        }
    }

    public void RemoveAllHPUIItem()
    {
        foreach (var item in hpUIItemDic)
        {
            Destroy(item.Value.gameObject);
        }

        hpUIItemDic.Clear();
    }

    public void SetHurt(string index, int hurt)
    {
        ItemEntityHP item = null;
        if(hpUIItemDic.TryGetValue(index, out item))
        {
            item.SetHurt(hurt);
        }
    }

    public void SetDodge(string index)
    {
        ItemEntityHP item = null;
        if(hpUIItemDic.TryGetValue(index, out item))
        {
            item.SetDodge();
        }
    }

    public void SetCritical(string index)
    {
        ItemEntityHP item = null;
        if(hpUIItemDic.TryGetValue(index, out item))
        {
            item.SetCritical();
        }
    }

    public void SetHpVal(string index, int oldHp, int newHp)
    {
        ItemEntityHP item = null;
        if(hpUIItemDic.TryGetValue(index, out item))
        {
            item.SetHpVal(oldHp, newHp);
        }
    }

    public void SetDodgePlayer()
    {
        SetActive(txtPlayerDodge);
        playerDodge.Stop();
        playerDodge.Play();
    }
}
