using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private RectTransform canvas;
    [SerializeField]
    private Image Upgrademenu;
    [SerializeField]
    private Sprite[] UpgradeMenusSprites;
    [SerializeField]
    private GameObject BasicTank;
    [SerializeField]
    private TextMeshProUGUI moneycounter;
    [SerializeField]
    private TextMeshProUGUI healthcounter;
    [SerializeField]
    private GameObject SupremeBoss;
    private Camera cam;
    private int CoinAmount = 30;
    private int Health = 3;
    private string SupremeCheatCode = "supreme";
    private int SupremeCheatIndex = 0;
    public static GameManager Instance;
    public static TankSlot CurrentSelectedTankSlot;
    public static GameObject CurrentSelectedTank;
    private Vector3 LastCampos;
    private TankUpgrade[] availableupgrades;

    private void Awake()
    {
        cam = Camera.main;
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        Keyboard.current.onTextInput += CheatCodeInput;
    }

    private void CheatCodeInput(char letter)
    {
        if (char.ToLower(letter) == SupremeCheatCode[SupremeCheatIndex])
        {
            SupremeCheatIndex++;
        }
        else
        {
            SupremeCheatIndex = 0;
        }
        if (SupremeCheatIndex == SupremeCheatCode.Length)
        {
            Instantiate(SupremeBoss, new Vector2(-21.5f, 9.5f), Quaternion.identity);
            SupremeCheatIndex = 0;
        }
    }

    public void BuyTank()
    {
        if (CurrentSelectedTankSlot.transform.childCount == 0 && CurrentSelectedTankSlot != null && CoinAmount >= 10)
        {
            Instantiate(BasicTank, CurrentSelectedTankSlot.transform.position, Quaternion.identity, CurrentSelectedTankSlot.transform);
            CoinAmount -= 10;
            moneycounter.SetText(CoinAmount.ToString());
        }
    }

    public void SellTank()
    {
        if (CurrentSelectedTankSlot.transform.childCount == 1 && CurrentSelectedTankSlot != null)
        {
            Destroy(CurrentSelectedTankSlot.transform.GetChild(0).gameObject);
            CoinAmount += 5;
            moneycounter.SetText(CoinAmount.ToString());
        }
    }

    public void AddMoney(int amount)
    {
        CoinAmount += amount;
        moneycounter.SetText(CoinAmount.ToString());
    }

    public void DealDamage()
    {
        Health--;
        healthcounter.SetText(Health.ToString());
    }

    public void LocateUpgradeMenu()
    {
        if (CurrentSelectedTank.GetComponent<SkillSystem>().availableupgrades == null)
            return;
        var screenpos = cam.WorldToScreenPoint(CurrentSelectedTank.transform.position);
        var canvaspoint = RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, screenpos, null, out var targetpoint);
        Upgrademenu.rectTransform.anchoredPosition = targetpoint;
		Upgrademenu.gameObject.SetActive(true);
		availableupgrades = CurrentSelectedTank.GetComponent<SkillSystem>().availableupgrades;
		int length = availableupgrades.Length;
		Upgrademenu.sprite = UpgradeMenusSprites[length - 1];
		for (int i = 0; i < length; i++)
		{
            switch (length)
            {   
                case 1:
                    Upgrademenu.transform.GetChild(1).gameObject.SetActive(false);
                    Upgrademenu.transform.GetChild(2).gameObject.SetActive(false);
                    break;
                case 2:
					Upgrademenu.transform.GetChild(1).gameObject.SetActive(true);
					Upgrademenu.transform.GetChild(2).gameObject.SetActive(false);
                    break;
                case 3:
					Upgrademenu.transform.GetChild(1).gameObject.SetActive(true);
					Upgrademenu.transform.GetChild(2).gameObject.SetActive(true);
                    break;

				default:
                    break;
            }
            Upgrademenu.rectTransform.GetChild(i).GetComponent<Image>().sprite = availableupgrades[i].Icon;
		}
		if (length == 2)
			(Upgrademenu.rectTransform.GetChild(1).transform as RectTransform).anchoredPosition = new Vector3(100, -70);
		else
			(Upgrademenu.rectTransform.GetChild(1).transform as RectTransform).anchoredPosition = new Vector3(0,-125);
        Upgrademenu.transform.GetChild(0).GetComponent<UpgradeButton>().Click += Upgrade1;
        Upgrademenu.transform.GetChild(1).GetComponent<UpgradeButton>().Click += Upgrade2;
        Upgrademenu.transform.GetChild(2).GetComponent<UpgradeButton>().Click += Upgrade3;
    }

    private void Update()
    {
        if (cam.transform.position != LastCampos)
		{
			Upgrademenu.gameObject.SetActive(false);
		}
		LastCampos = cam.transform.position;
	}

    private void Upgrade1()
    {
        if (CoinAmount >= availableupgrades[0].Cost)
        {
            CoinAmount -= availableupgrades[0].Cost;
            var cst = Instantiate(availableupgrades[0].Upgrade, CurrentSelectedTank.transform.position, Quaternion.identity, CurrentSelectedTankSlot.transform);
            Destroy(CurrentSelectedTank);
            CurrentSelectedTank = cst;
			moneycounter.SetText(CoinAmount.ToString());
			Upgrademenu.gameObject.SetActive(false);
		}
    }

    private void Upgrade2()
    {
        if (!(availableupgrades.Length >= 2))
            return;
        if (CoinAmount >= availableupgrades[1].Cost)
		{
			CoinAmount -= availableupgrades[1].Cost;
			var cst = Instantiate(availableupgrades[1].Upgrade, CurrentSelectedTank.transform.position, Quaternion.identity, CurrentSelectedTankSlot.transform);
            Destroy(CurrentSelectedTank);
            CurrentSelectedTank = cst;
			moneycounter.SetText(CoinAmount.ToString());
			Upgrademenu.gameObject.SetActive(false);
		}
    }

    private void Upgrade3()
	{
		if (availableupgrades.Length != 3)
			return;
		if (CoinAmount >= availableupgrades[2].Cost)
		{
			CoinAmount -= availableupgrades[2].Cost;
			var cst = Instantiate(availableupgrades[2].Upgrade, CurrentSelectedTank.transform.position, Quaternion.identity, CurrentSelectedTankSlot.transform);
            Destroy(CurrentSelectedTank);
            CurrentSelectedTank = cst;
			moneycounter.SetText(CoinAmount.ToString());
			Upgrademenu.gameObject.SetActive(false);
		}
    }
}