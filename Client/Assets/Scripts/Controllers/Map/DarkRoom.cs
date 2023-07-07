using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DarkRoom : MapController
{

    private GameObject _darkRoom;
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Managers.Object.MyPlayer.CanLightBuff)
            _darkRoom = Managers.Resource.LoadResources<GameObject>("BuffDarkroomUI");
        else
            _darkRoom = Managers.Resource.LoadResources<GameObject>("DarkroomUI");
        // 플레이어가 아이템 획득 시 
        if (collision.gameObject.tag == "Player")
        {
            GameObject go = Instantiate(_darkRoom, new Vector3(0,0,0), transform.rotation);
            // 화면에 꽉차게
            //Image image = go.transform.GetChild(0).GetComponent<Image>();
            Image image = go.GetComponent<Image>();
            if (image == null)
                return;
            float spriteX = image.sprite.bounds.size.x;
            float spriteY = image.sprite.bounds.size.y;
            float screenY = Camera.main.orthographicSize * 2;
            float screenX = screenY / Screen.height * Screen.width;

            transform.localScale = new Vector2(Mathf.Ceil(screenX / spriteX), Mathf.Ceil(screenY / spriteY));

            go.transform.SetParent(collision.gameObject.transform);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject go;
            if (Managers.Object.MyPlayer.CanLightBuff)
            {
                go = GameObject.Find("BuffDarkroomUI(Clone)");
                Destroy(go);
            }
            else
            {
                go = GameObject.Find("DarkroomUI(Clone)");
                Destroy(go);
            }
        }
    }



}
