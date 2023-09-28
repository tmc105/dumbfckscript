using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPanelUI : MonoBehaviour
{
    public GameObject characterPanelBackground;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            characterPanelBackground.gameObject.SetActive(!characterPanelBackground.gameObject.activeSelf);
        }
    }
}
