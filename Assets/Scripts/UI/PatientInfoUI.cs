using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PatientInfoUI : MonoBehaviour
{
    bool isVisible = true;

    // texties
    public string[] pReason = new string[3];
    public string[] pLikes = new string[3];
    public string[] pBg = new string[3];
    public string[] pOther = new string[3];
    public string[] pName = new string[3];
    public string[] pAge = new string[3];
    public string[] pSex = new string[3];

    //patient picture
    public Sprite[] patient = new Sprite[3];


    void Start()
	{
        Image img = GameObject.Find("PImage").GetComponent<Image>();
        Text rText = GameObject.Find("Reason").GetComponent<Text>();
        Text lText = GameObject.Find("LD").GetComponent<Text>();
        Text bText = GameObject.Find("Back").GetComponent<Text>();
        Text oText = GameObject.Find("Other").GetComponent<Text>();
        Text nText = GameObject.Find("Name").GetComponent<Text>();
        Text aText = GameObject.Find("Age").GetComponent<Text>();
        Text sText = GameObject.Find("Sex").GetComponent<Text>();

        int rnd = Random.Range(0, 2);

        if (rnd == 0)
        {   
            img.sprite = patient[0];
            rText.text = pReason[0];
            lText.text = pLikes[0];
            bText.text = pBg[0];
            oText.text = pOther[0];
            nText.text = pName[0];
            aText.text = pAge[0];
            sText.text = pSex[0];
        }

        if (rnd == 1)
        {
            img.sprite = patient[1];
            rText.text = pReason[1];
            lText.text = pLikes[1];
            bText.text = pBg[1];
            oText.text = pOther[1];
            nText.text = pName[1];
            aText.text = pAge[1];
            sText.text = pSex[1];
        }

        if (rnd == 2)
        {
            img.sprite = patient[2];
            rText.text = pReason[2];
            lText.text = pLikes[2];
            bText.text = pBg[2];
            oText.text = pOther[2];
            nText.text = pName[2];
            aText.text = pAge[2];
            sText.text = pSex[2];
        }
    }

	void Update()
	{
		if (Input.GetMouseButtonDown(0) && isVisible)
			SetIsVisible(false);
	}

	public void SetIsVisible(bool enable)
	{
		isVisible = enable;
		GameStateManager.instance.SetState(enable ? GameStateManager.State.InMenu : GameStateManager.State.Free);
		gameObject.SetActive(enable);
	}
}