using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GoToNextInputField : MonoBehaviour
{
    private EventSystem system;

	// Use this for initialization
	void Start () {
        system = EventSystem.current;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Tab))
        {
            var sec = system.currentSelectedGameObject;
            Selectable nextField = sec.GetComponent<InputField>().FindSelectableOnDown();
            system.SetSelectedGameObject(nextField.gameObject, new BaseEventData(system));
        }
	}
}
