using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwitchPanels : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData data)
    {
        if (string.Compare(this.name, "Login_Tag") == 0)
        {
            if (!GameObject.Find("Login_Panel"))
            {
                // Inactive login panel
                GameObject loginPanel;
                loginPanel = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
                loginPanel.SetActive(true);

                // Modify the alpha value of 2 tags
                GetComponent<Text>().color = new Color(1, 1, 1, 1);
                transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                GameObject.Find("Signup_Tag").GetComponent<Text>().color = new Color(1, 1, 1, 0.2f);
                GameObject.Find("Signup_Tag").transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.2f);

                // Play the animations to show login panel and hide signup panel
                Animator loginAnimator = loginPanel.GetComponent<Animator>();
                Animator signupAnimator = GameObject.Find("Signup_Panel").GetComponent<Animator>();
                loginAnimator.Play("Show", -1, 0f);
                signupAnimator.Play("Hide", -1, 0f);

                StartCoroutine(inactivateSignup());
            }
        }
        else if (string.Compare(this.name, "Signup_Tag") == 0)
        {
            if (!GameObject.Find("Signup_Panel"))
            {
                // Inactive signup panel
                GameObject signupPanel;
                signupPanel = GameObject.Find("Canvas").transform.GetChild(1).gameObject;
                signupPanel.SetActive(true);

                // Modify the alpha value of 2 tags
                GetComponent<Text>().color = new Color(1, 1, 1, 1);
                transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                GameObject.Find("Login_Tag").GetComponent<Text>().color = new Color(1, 1, 1, 0.2f);
                GameObject.Find("Login_Tag").transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.2f);

                // Play the animations to show signup panel and hide login panel
                Animator signupAnimator = signupPanel.GetComponent<Animator>();
                Animator loginAnimator = GameObject.Find("Login_Panel").GetComponent<Animator>();
                signupAnimator.Play("Show", -1, 0f);
                loginAnimator.Play("Hide", -1, 0f);

                StartCoroutine(inactivateLogin());
            }
        }
    }

    IEnumerator inactivateSignup()
    {
        yield return new WaitForSeconds(1);
        GameObject.Find("Signup_Panel").SetActive(false);
    }

    IEnumerator inactivateLogin()
    {
        yield return new WaitForSeconds(1);
        GameObject.Find("Login_Panel").SetActive(false);
    }
}
