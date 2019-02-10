using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Register : MonoBehaviour {

    //private string registerURL = "http://localhost/register.php";
    private string registerURL = "http://localhost:8080/JavaBridge/register.php";
    private string pname;

    [System.Serializable]
    public class userData
    {
        public string prompt;
        public int check;
    }

    public void newuser()
    {
        string username = GameObject.Find("Username").GetComponent<InputField>().text;
        string password = GameObject.Find("Password").GetComponent<InputField>().text;
        pname = GameObject.Find("Name").GetComponent<InputField>().text;

        if (string.Compare(username, "") == 0 || string.Compare(password, "") == 0 || string.Compare(pname, "") == 0)
        {
            GameObject.Find("PopWindow").transform.GetChild(0).gameObject.SetActive(true);
            GameObject.Find("Warning").GetComponent<Text>().text = "Please complete all fields.";
        }
        else
        {
            WWWForm form = new WWWForm();
            form.AddField("user", username);
            form.AddField("pword", password);
            form.AddField("name", pname);

            WWW www = new WWW(registerURL, form);
            GameObject prompt = GameObject.Find("PopWindow").transform.GetChild(0).gameObject;
            prompt.SetActive(true);
            prompt.transform.GetChild(1).gameObject.SetActive(false);
            prompt.transform.GetChild(2).gameObject.GetComponent<Text>().text = "Please wait ...";
            StartCoroutine(register(www));
        }
    }

    IEnumerator register(WWW www)
    {
        yield return www;
        if (www.error == null)
        {
            userData data = JsonUtility.FromJson<userData>(www.text);
            GameObject.Find("Warning").GetComponent<Text>().text = data.prompt;
            GameObject.Find("PopWindow").transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
            if (data.check == 1)
            {
                GameObject.Find("PlayerProfile").GetComponent<playerProfile>().pname = pname;
                SceneManager.LoadScene("Battlefield");
            }
        }
        else
        {
            GameObject.Find("Warning").GetComponent<Text>().text = www.error;
            GameObject.Find("PopWindow").transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        }
    }
}
