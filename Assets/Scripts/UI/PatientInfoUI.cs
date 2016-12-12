using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PatientInfoUI : MonoBehaviour
{
    bool isVisible = true;
	const int patientNum = 3;

    // texties
    public string[] pReason = new string[patientNum];
    public string[] pLikes = new string[patientNum];
    public string[] pBg = new string[patientNum];
    public string[] pOther = new string[patientNum];
    public string[] pName = new string[patientNum];
    public string[] pAge = new string[patientNum];
    public string[] pSex = new string[patientNum];

    //patient picture
    public Sprite[] patient = new Sprite[patientNum];


    void Start()
	{
        Image img = transform.Find("PImage").GetComponent<Image>();
        Text rText = transform.Find("Reason").GetComponent<Text>();
        Text lText = transform.Find("LD").GetComponent<Text>();
        Text bText = transform.Find("Back").GetComponent<Text>();
        Text oText = transform.Find("Other").GetComponent<Text>();
        Text nText = transform.Find("Name").GetComponent<Text>();
        Text aText = transform.Find("Age").GetComponent<Text>();
        Text sText = transform.Find("Sex").GetComponent<Text>();

        int rnd = Random.Range(0, patientNum);

        img.sprite = patient[rnd];
        rText.text = pReason[rnd];
        lText.text = pLikes[rnd];
        bText.text = pBg[rnd];
        oText.text = pOther[rnd];
        nText.text = pName[rnd];
        aText.text = pAge[rnd];
        sText.text = pSex[rnd];
    }

	void Update()
	{
		if (Input.GetMouseButtonDown(0) && isVisible)
		{
			SoundManager.instance.PlaySound(SoundManager.instance.UIclick);
			SetIsVisible(false);
		}
	}

	public void SetIsVisible(bool enable)
	{
		isVisible = enable;
		GameStateManager.instance.SetState(enable ? GameStateManager.State.InMenu : GameStateManager.State.Free);
		gameObject.SetActive(enable);
	}
}