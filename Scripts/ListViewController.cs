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
        Transmission trans; //�ĂԃX�N���v�g�ɂ����Ȃ���
        GameObject obj = GameObject.Find("Canvas"); //Player���Ă����I�u�W�F�N�g��T��
        trans = obj.GetComponent<Transmission>(); //�t���Ă���X�N���v�g���擾
        if (trans.ViewFlg == true && flg == false) 
        {
            flg = true;
            UpdateListView();
        }

    }

    private void UpdateListView()
    {
        RemoveAllListViewItem(); // ListView�̍��ڂ���x�폜����.

        // item���ɍ��킹��Content�̍�����ύX����.
        int setting_count = 10;
        float newHeight = setting_count * itemHight_;
        content_.sizeDelta = new Vector2(content_.sizeDelta.x, -100); // ������ύX����.

        GameObject item = GameObject.Instantiate(item_prefab_) as GameObject; // ListViewItem �̃C���X�^���X�쐬.

        RectTransform itemTransform = (RectTransform)item.transform;
        itemTransform.SetParent(content_, false); // �쐬����Item��Content�̎q�v�f�ɐݒ�.

        Text itemText = item.GetComponentInChildren<Text>(); // Text�R���|�[�l���g���擾.
        itemText.text = "";
        itemText.fontSize = 1;

        // Content�̎q�v�f��ListViewItem��ǉ����Ă���.
        for (int i=0;i<10;i++)
        {
            item = GameObject.Instantiate(item_prefab_) as GameObject; // ListViewItem �̃C���X�^���X�쐬.

            itemTransform = (RectTransform)item.transform;
            itemTransform.SetParent(content_, false); // �쐬����Item��Content�̎q�v�f�ɐݒ�.

            itemText = item.GetComponentInChildren<Text>(); // Text�R���|�[�l���g���擾.
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
