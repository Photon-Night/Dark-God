using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicWin : WinRoot
{
    public Animation tipsAnim;
    public Text txtTips;

    public Transform hpItemRoot;

    private bool isTipsShow;

    private Queue<string> tipsQue = new Queue<string>();
    private Dictionary<string, ItemEntityHP> hpUIItemDic = new Dictionary<string, ItemEntityHP>();
    protected override void InitWin()
    {
        base.InitWin();

        SetActive(txtTips, false);
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
        //��ʱ�ر�
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

}
