using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    //�I�u�W�F�N�g���\����
    private struct ObjectInfo
    {
        public int          coolTime;       //�����N�[���^�C��
        public int          coolDownCnt;    //���݂̃N�[���^�C���J�E���g
        public bool         isCoolDown;     //�N�[���^�C���t���O
        public GameObject   obj;            //�I�u�W�F�N�g
    };

    //�I�u�W�F�N�g���
    private enum ObjectList
    {
        Default,
        Default_L,
        Default_R,
        Default_None,
        Vanish_Obj,
        Moving_Obj,
        MovingFloor_Obj
    };

    private ObjectInfo[]    obj;            //�I�u�W�F�N�g���
    
    private float   defaultY;         //�������W
    private float   destroyPointY;    //�j��Ԋu

    private int     generateCnt;    //������
    private int     oneBeforeObj;   //�ЂƂO�̐����I�u�W�F�N�g
    private bool    isChange;       //��Փx�ύX�t���O

    private GameObject  jumpItemObj;        //�W�����v�A�C�e���p�I�u�W�F�N�g
    private int         itemCoolDownCnt;    //�A�C�e���̐����N�[���^�C���J�E���g
    private int         itemCoolTime;       //�A�C�e���̐����N�[���^�C��
    private bool        isItemCoolDown;     //�A�C�e���̃N�[���_�E���t���O

    //����������
    private void Start()
    {
        FollowCamara fCamera = GameObject.Find("Main Camera").GetComponent<FollowCamara>();

        //�z����m��
        obj = new ObjectInfo[Define.MAP_OBJECT_NUM];

        //private�ϐ��̏�����
        defaultY = -4f;
        destroyPointY = fCamera.bottomY - 3f;
        generateCnt = 0;
        oneBeforeObj = 0;
        isChange = false;

        for (int i = 0; i < Define.MAP_OBJECT_NUM; i++)
        {
            obj[i].coolDownCnt = 0;
            obj[i].isCoolDown = false;
        }

        itemCoolDownCnt = 0;
        isItemCoolDown = true;

        //�����N�[���^�C����ݒ�
        obj[(int)ObjectList.Default].coolTime = 0;
        obj[(int)ObjectList.Default_L].coolTime = 4;
        obj[(int)ObjectList.Default_R].coolTime = 4;
        obj[(int)ObjectList.Default_None].coolTime = 6;
        obj[(int)ObjectList.Vanish_Obj].coolTime = 6;
        obj[(int)ObjectList.Moving_Obj].coolTime = 20;
        obj[(int)ObjectList.MovingFloor_Obj].coolTime = 6;
        obj[7].coolTime = 0;
        obj[8].coolTime = 0;
        obj[9].coolTime = 0;

        itemCoolTime = 50;

        //Resources�t�H���_�̃I�u�W�F�N�g��ǂݍ���
        obj[(int)ObjectList.Default].obj = 
            (GameObject)Resources.Load("Default_Obj");
        obj[(int)ObjectList.Default_L].obj = 
            (GameObject)Resources.Load("DefaultL_Obj");
        obj[(int)ObjectList.Default_R].obj = 
            (GameObject)Resources.Load("DefaultR_Obj");
        obj[(int)ObjectList.Default_None].obj = 
            (GameObject)Resources.Load("DefaultNone_Obj");
        obj[(int)ObjectList.Vanish_Obj].obj = 
            (GameObject)Resources.Load("Vanish_Obj");
        obj[(int)ObjectList.Moving_Obj].obj =
            (GameObject)Resources.Load("MovingWallObj");
        obj[(int)ObjectList.MovingFloor_Obj].obj =
            (GameObject)Resources.Load("MovingFloor_Obj");

        jumpItemObj = (GameObject)Resources.Load("JumpItem");

        //���������s��
        ReInit();
    }

    //�X�V����
    private void Update()
    {
        //�I�u�W�F�N�g�̔j��|�C���g���X�V
        FollowCamara fCamera = GameObject.Find("Main Camera").GetComponent<FollowCamara>();
        destroyPointY = fCamera.bottomY - 3f;

        if (generateCnt % Define.MAP_DIFFICULTY_CNT == 0 && !isChange)
        {
            for (int i = 0; i < Define.MAP_OBJECT_NUM; i++)
            {
                if (obj[i].coolTime == 0) continue;

                obj[i].coolTime--;
            }
            isChange = true;
        }
        else if (isChange && generateCnt % Define.MAP_DIFFICULTY_CNT != 0)
        {
            isChange = false;
        }

        //�I�u�W�F�N�g�j�󏈗�
        foreach (Transform t in this.gameObject.transform)
        {
            //�j��|�C���g��艺�̏ꍇ�Ώۂ̃I�u�W�F�N�g��j�󂷂�
            if (t.GetComponent<StageObject>().destroyPoint <= destroyPointY)
            {
                GameObject.Destroy(t.gameObject);

                //�I�u�W�F�N�g��V�K����
                Generate();
            }
        }
    }

    //�ď���������
    private void ReInit()
    {
        //��ԃx�[�X�̃I�u�W�F�N�g�𐶐�
        GenerateObject(0, true);

        //2�i�ڈȍ~�̃I�u�W�F�N�g�𐶐�
        for (int i = 0; i < 15; i++)
        {
            Generate();
        }
    }

    //��������
    private void Generate()
    {
        int     randNum = 0;        //����
        bool    isCheck = false;    //�N�[���_�E���`�F�b�N�t���O

        //�N�[���_�E���֘A�̏���
        for (int i = 0; i < Define.MAP_OBJECT_NUM; i++)
        {
            //�N�[���_�E�����̏ꍇ
            if (obj[i].isCoolDown)
            {
                //�J�E���g��coolTime�ɂȂ����Ƃ��N�[���_�E�����I������
                if (obj[i].coolDownCnt >= obj[i].coolTime)
                {
                    obj[i].coolDownCnt = 0;
                    obj[i].isCoolDown = false;

                    continue;
                }

                //�N�[���_�E���J�E���g�����Z
                obj[i].coolDownCnt++;
            }
        }

        if (isItemCoolDown)
        {
            if(itemCoolDownCnt >= itemCoolTime)
            {
                itemCoolDownCnt = 0;
                isItemCoolDown = false;
            }
            else
            {
                itemCoolDownCnt++;
            }
        }

        //�������N�[���_�E�����̃I�u�W�F�N�g�����肷��
        while (!isCheck)
        {
            randNum = (int)Random.Range(0, 7);

            if (!(oneBeforeObj >= (int)ObjectList.Default &&
                oneBeforeObj <= (int)ObjectList.Default_R) &&
                randNum == (int)ObjectList.MovingFloor_Obj) continue;

            if (!obj[randNum].isCoolDown)
            {
                isCheck = true;
                oneBeforeObj = randNum;
            }
        }

        //�I�u�W�F�N�g�𐶐�
        GenerateObject(randNum);
    }

    //�I�u�W�F�N�g��������
    private void GenerateObject(int _objNum,bool _isOnPlayer = false)
    {
        //�I�u�W�F�N�g�𐶐�
        GameObject inst = (GameObject)Instantiate(obj[_objNum].obj,
            new Vector3(0f, defaultY, 0f), Quaternion.identity);

        //�e�I�u�W�F�N�g��ݒ�
        inst.transform.SetParent(this.transform, false);


        int itemRandNum = (int)Random.Range(0, 100);

        if(itemRandNum % 10 == 0 && !isItemCoolDown)
        {
            GameObject item = (GameObject)Instantiate(jumpItemObj,
                new Vector3(Random.Range(-2.4f, 2.4f), defaultY - 0.5f, 0f), Quaternion.identity);

            item.name = "JumpItem";
            item.transform.SetParent(this.transform, false);
            item.GetComponent<StageObject>().destroyPoint = defaultY;

            isItemCoolDown = true;
        }

        //�m�[�}���n�̐����̏ꍇ�̏���
        if (_objNum >= (int)ObjectList.Default && _objNum <= (int)ObjectList.Default_None)
        {
            inst.GetComponentInChildren<FloorObj>().isOnPlayer = _isOnPlayer;
        }

        if(_objNum == (int)ObjectList.Moving_Obj)
        {
            int floorNum = (int)Random.Range(5, 10);

            inst.GetComponent<MovingWallObj>().SetFloorNum(floorNum);

            defaultY += (Define.MAP_INTERVAL_Y * floorNum);
        }

        //�f�t�H���g�I�u�W�F�N�g(0�Ԗڂ�Obj)�ȊO�̏ꍇ�N�[���_�E���ɂ���
        if(_objNum != (int)ObjectList.Default)
        {
            obj[_objNum].isCoolDown = true;
        }

        //�Ԋu���X�V
        if(_objNum != (int)ObjectList.Moving_Obj)
        {
            defaultY += Define.MAP_INTERVAL_Y;
        }

        inst.GetComponent<StageObject>().destroyPoint = defaultY;
        generateCnt++;
    }
}
