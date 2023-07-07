using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    GameObject SettingUI;
    protected override void Init()
    {
        base.Init();
        Managers.Scene.CurrentScene = Define.Scene.Game;

        Managers.Map.LoadMap(1);
        //SettingUI = Managers.Object.FindById(Managers.Game.MyPlayerId).GetComponent<MyPlayerController>().SettingUI;
        //GameObject player = Managers.Resource.Instantiate("Creature/Player");
        //player.name = "Player";
        //Managers.Object.Add(player);

        //Managers.UI.ShowSceneUI<UI_Inven>();
        //Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;
        //gameObject.GetOrAddComponent<CursorController>();

        //GameObject player = Managers.Game.Spawn(Define.WorldObject.Player, "UnityChan");
        //Camera.main.gameObject.GetOrAddComponent<CameraController>().SetPlayer(player);

        ////Managers.Game.Spawn(Define.WorldObject.Monster, "Knight");
        //GameObject go = new GameObject { name = "SpawningPool" };
        //SpawningPool pool = go.GetOrAddComponent<SpawningPool>();
        //pool.SetKeepMonsterCount(2);
    }
    private void Start()
    {
        AudioSync();
    }
    void Update()
    {
        if (SettingUI == null)
        {
            SettingUI = Managers.Object.FindById(Managers.Game.MyPlayerId).GetComponent<MyPlayerController>().SettingUI;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SettingUI.SetActive(true);
        }
    }
}
