using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressEnterToSubmit : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
        {
            GameObject signup, login, prompt;
            signup = GameObject.Find("Sign up");
            login = GameObject.Find("Login");
            prompt = GameObject.Find("Prompt");
            if (prompt)
            {
                prompt.SetActive(false);
            }
            else if (signup)
            {
                signup.GetComponent<Register>().newuser();
            }
            else if (login)
            {
                login.GetComponent<Login>().signIn();
            }
        }
	}
}
