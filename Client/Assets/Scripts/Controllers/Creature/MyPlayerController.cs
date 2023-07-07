using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Protobuf.Protocol;
using Unity.VisualScripting;

public class MyPlayerController : PlayerController
{
    Camera minimapCam;

    //private float[] _coolTimeList = { 0.7f, 0.5f, 0.4f, 0.3f, 0.2f };

    //private int _level = 1;

    private bool _portalAvail;
    private float _coolTime;
    private bool _inPortal;

    public float CoolTime { get { return _coolTime; } set { _coolTime = value; } }
    public bool PortalAvail {  get { return _portalAvail; } set { _portalAvail = value; } }
    public bool InPortal { get { return _inPortal; } set { _inPortal = value; } }
    public bool IsWinner = false;
    //public int Level { get { return _level; } set { _level = value; } }
    public GameObject @Sound;
    void Awake()
    {
        @Sound = GameObject.Find("@Sound");
    }
    void SoundSourceFollowPlayer()
    {
        @Sound.transform.position = transform.position;
    }
    void Start()
    {
        Init();
        //_coolTime = _coolTimeList[_level - 1];
        switch (Managers.Game.MyPlayerWeaponType)
        {
            case WeaponType.Pistol:
                //CoolTime = 0.5f;
                _weaponUI.ChangeImage(1);
                break;
            case WeaponType.Rifle:
                //CoolTime = 0.2f;
                _weaponUI.ChangeImage(2);
                break;
            case WeaponType.Sniper:
                //CoolTime = 1.2f;
                _weaponUI.ChangeImage(3);
                break;
            case WeaponType.Shotgun:
                //CoolTime = 1.0f;
                _weaponUI.ChangeImage(4);
                break;
        }
        minimapCam = Managers.Resource.InstantiateResources("MinimapCamera").GetComponent<Camera>();
        _portalAvail = true;
    }
    void Update()
    {
        SoundSourceFollowPlayer();
        base.UpdateAnimation();
        UpdateMinimapCam();
        //Debug.Log($"내 플레이어 ID: {Id}: {State}");
    }
    void LateUpdate()
    {
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }
    protected override void Init()
    {
        base.Init();
        InitGameUI();
    }
    protected void InitGameUI()
    {
        _movingTilt = Managers.Resource.InstantiateResources("UI_MovingTilt").GetComponent<UI_MovingTilt>();
        _movingTilt.MyPlayer = this;
        _fireTilt = Managers.Resource.InstantiateResources("UI_FireTilt").GetComponent<UI_FireTilt>();
        _fireTilt.MyPlayer = this;
        _weaponUI = Managers.Resource.InstantiateResources("UI_Weapon").GetComponent<UI_Weapon>();
        _weaponUI.MyPlayer = this;
        _timerUI = Managers.Resource.InstantiateResources("UI_Timer").GetComponent<UI_Timer>();
        _pauseUI = Managers.Resource.InstantiateResources("UI_Pause").GetComponent<UI_Pause>();
        _hpBarUI = Managers.Resource.InstantiateResources("UI_HpBar").GetComponent<UI_HpBar>();
        _hpBarUI.MyPlayer = this;
        _minimapUI = Managers.Resource.InstantiateResources("UI_Minimap").GetComponent<UI_Minimap>();
        _goldUI = Managers.Resource.InstantiateResources("UI_Gold").GetComponent<UI_Gold>();
        _levelUI = Managers.Resource.InstantiateResources("UI_Level").GetComponent<UI_Level>();
        _levelUI.MyPlayer = this;
        _portalCoolUI = Managers.Resource.InstantiateResources("UI_PortalCool").GetComponent<UI_PortalCool>();
        _portalCoolUI.MyPlayer = this;
        _systemTextViewerUI = Managers.Resource.InstantiateResources("UI_SystemTextViewer").GetComponent<UI_SystemTextViewer>();
        _portalDesUI = Managers.Resource.InstantiateResources("UI_PortalDes").GetComponent<UI_PortalDes>();
        _portalDesUI.MyPlayer = this;

        SettingUI = Managers.Resource.InstantiateResources("Pause");
        SettingUI.GetComponent<UI_PausePopup>().MyPlayer = this;
        SettingUI.SetActive(false);
        //_weaponUI.MyPlayer = this;
    }
    public void CloseGameUI()
    {
        _portalDesUI.gameObject.SetActive(false);
        _systemTextViewerUI.gameObject.SetActive(false);
        _levelUI.gameObject.SetActive(false);
        _goldUI.gameObject.SetActive(false);
        _minimapUI.gameObject.SetActive(false);
        _hpBarUI.gameObject.SetActive(false);
        _pauseUI.gameObject.SetActive(false);
        _timerUI.gameObject.SetActive(false);
        _weaponUI.gameObject.SetActive(false);
        _fireTilt.gameObject.SetActive(false);
        _movingTilt.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어가 아이템 획득 시 
        if (collision.gameObject.tag == "DarkRoom")
        {
            //GameObject go = Managers.Resource.InstantiateResources("DarkroomUI");
            //go.GetComponent<DarkRoomUIController>().MyPlayer = this;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // 플레이어가 아이템 획득 시 
        if (collision.gameObject.tag == "DarkRoom")
        {
            //go.GetComponent<DarkRoomUIController>().MyPlayer = this;
        }
    }
    public override void LevelUp()
    {
        GameObject effect = Managers.Resource.InstantiateResources("LevelUpEffect");
        effect.GetComponent<EffectController>().Creature = this;
        effect.transform.position = transform.position;
        if (Managers.Sound.SoundOn == true)
            Managers.Sound.Play("Effect/Player/LevelUpSound");
        GameObject.Destroy(effect, 5f);
    }

    private void UpdateMinimapCam()
    {
        if (transform.position.x <= -50 && transform.position.x >= -90)
        {
            if (transform.position.y <= 90 && transform.position.y >= 50)
            {
                minimapCam.transform.position = new Vector3(-70, 70, -10);
            }
            else if (transform.position.y <= -50 && transform.position.y >= -90)
            {
                minimapCam.transform.position = new Vector3(-70, -70, -10);
            }
        }
        else if (transform.position.x <= 90 && transform.position.x >= 50)
        {
            if (transform.position.y <= 90 && transform.position.y >= 50)
            {
                minimapCam.transform.position = new Vector3(70, 70, -10);
            }
            else if (transform.position.y <= -50 && transform.position.y >= -90)
            {
                minimapCam.transform.position = new Vector3(70, -70, -10);
            }
        }
        else if(transform.position.x <= 15 && transform.position.x >= -15)
        {
            if(transform.position.y <= 15 && transform.position.y >= -15)
            {
                minimapCam.transform.position = new Vector3(0, 0, -10);
            }
        }
    }

    public Camera mainCamera;
    Vector3 cameraPos;
    float shakeRange = 0.05f;
    float duration = 0.2f;
    protected override void Shake()
    {
        if (Managers.Sound.ShakeOn)
        {
            mainCamera = Camera.main;
            cameraPos = mainCamera.transform.position;
            InvokeRepeating("StartShake", 0f, 0.005f);
            Invoke("StopShake", duration);
        }
    }
    protected override void StartShake()
    {
        float cameraPosX = Random.value * shakeRange * 2 - shakeRange;
        float cameraPosY = Random.value * shakeRange * 2 - shakeRange;
        Vector3 cameraPos = mainCamera.transform.position;
        cameraPos.x += cameraPosX;
        cameraPos.y += cameraPosY;
        mainCamera.transform.position = cameraPos;
    }

    protected override void StopShake()
    {
        CancelInvoke("StartShake");
        mainCamera.transform.position = cameraPos;
    }
    public void ResetFlag()
    {
        StartCoroutine(CoResetFlag());
    }
    IEnumerator CoResetFlag()
    {
        PortalAvail = false;
        PortalCool.IconVisible();
        yield return new WaitForSeconds(5f);
        PortalAvail = true;
        PortalCool.IconInvisible();
    }
    public override void OnDamaged()
    {
        base.OnDamaged();
    }
}