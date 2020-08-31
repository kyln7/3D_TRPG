using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseReturn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        GameObject PauseBackground = GameObject.Find("PauseBackground");
        PauseBackground.transform.localPosition = new Vector3(0, -1900, 0);
        GameControl.inputMode = GameControl.InputMode.Game;
    }
}
