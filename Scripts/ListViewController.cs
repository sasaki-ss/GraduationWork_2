using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ListViewController : MonoBehaviour
{
    public RectTransform content_;
    public GameObject item_prefab_;
    public string[] itemList_;
    private float itemHight_;

    private bool flg;

    // Use this for initialization
    void Start()
    {
        flg = false;

        GameObject item = GameObject.Instantiate(item_prefab_) as GameObject;
        GameObject.Destroy(item);

        RectTransform rect = item.GetComponent<RectTransform>();
        itemHight_ = rect.rect.height;
    }
    // Update is called once per frame
    void Update()
    {
        Transmission trans; //呼ぶスクリプトにあだなつける
        GameObject obj = GameObject.Find("Canvas"); //Playerっていうオブジェクトを探す
        trans = obj.GetComponent<Transmission>(); //付いているスクリプトを取得
        if (trans.ViewFlg == true && flg == false) 
        {
            flg = true;
            UpdateListView();
        }

    }

    private void UpdateListView()
    {
        RemoveAllListViewItem(); // ListViewの項目を一度削除する.

        // item数に合わせてContentの高さを変更する.
        int setting_count = 10;
        float newHeight = setting_count * itemHight_;
        content_.sizeDelta = new Vector2(content_.sizeDelta.x, -100); // 高さを変更する.

        GameObject item = GameObject.Instantiate(item_prefab_) as GameObject; // ListViewItem のインスタンス作成.

        RectTransform itemTransform = (RectTransform)item.transform;
        itemTransform.SetParent(content_, false); // 作成したItemをContentの子要素に設定.

        Text itemText = item.GetComponentInChildren<Text>(); // Textコンポーネントを取得.
        itemText.text = "";
        itemText.fontSize = 1;

        // Contentの子要素にListViewItemを追加していく.
        for (int i=0;i<10;i++)
        {
            item = GameObject.Instantiate(item_prefab_) as GameObject; // ListViewItem のインスタンス作成.

            itemTransform = (RectTransform)item.transform;
            itemTransform.SetParent(content_, false); // 作成したItemをContentの子要素に設定.

            itemText = item.GetComponentInChildren<Text>(); // Textコンポーネントを取得.
            itemText.text = Transmission.getUserData[i];
            itemText.fontSize = 30;
        }
    }

    private void RemoveAllListViewItem()
    {
        foreach (Transform child in content_.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
