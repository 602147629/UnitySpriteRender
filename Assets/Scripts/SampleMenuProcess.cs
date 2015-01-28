using UnityEngine;
using System.Collections;
using System ;//for BitConverter

public enum ENUM_UPGRADE_STATE
{
	NONE,
	EXPERIENCE,
	LEVEL,
	MONEY,
	RATIO,
	EMPTY,
	MAX
}

public class SampleMenuProcess : MonoBehaviour
{
	public		Transform 		uiCamera	= null ;
	public		Transform 		MenuRoot	= null ;
	public		Transform 		MenuNewTest	= null ;
	protected	GameObject[] 	UpgradeExp	;
	protected	GameObject[] 	UpgradeLevel;
	protected	GameObject[] 	UpgradeMoney;
	protected	GameObject[] 	UpgradeRatio;
	protected	GameObject[] 	UpgradeEmpty;
	//Init
	void Awake()
	{
		UpgradeMoney	= new GameObject[4] ;
		UpgradeMoney[0] = MenuRoot.Find("Money/GameObject0").gameObject;
		UpgradeMoney[1] = MenuRoot.Find("Money/GameObject1").gameObject;
		UpgradeMoney[2] = MenuRoot.Find("Money/GameObject2").gameObject;
		UpgradeMoney[3] = MenuRoot.Find("Money/GameObject3").gameObject;
		UpgradeExp		= new GameObject[4] ;
		UpgradeExp[0]	= MenuRoot.Find("Experience/GameObject0").gameObject;
		UpgradeExp[1]	= MenuRoot.Find("Experience/GameObject1").gameObject;
		UpgradeExp[2]	= MenuRoot.Find("Experience/GameObject2").gameObject;
		UpgradeExp[3]	= MenuRoot.Find("Experience/GameObject3").gameObject;
		UpgradeLevel	= new GameObject[4] ;
		UpgradeLevel[0] = MenuRoot.Find("Level/GameObject0").gameObject;
		UpgradeLevel[1] = MenuRoot.Find("Level/GameObject1").gameObject;
		UpgradeLevel[2] = MenuRoot.Find("Level/GameObject2").gameObject;
		UpgradeLevel[3] = MenuRoot.Find("Level/GameObject3").gameObject;
		UpgradeRatio	= new GameObject[4] ;
		UpgradeRatio[0] = MenuNewTest.Find("Ratio/GameObject0").gameObject;
		UpgradeRatio[1] = MenuNewTest.Find("Ratio/GameObject1").gameObject;
		UpgradeRatio[2] = MenuNewTest.Find("Ratio/GameObject2").gameObject;
		UpgradeRatio[3] = MenuNewTest.Find("Ratio/GameObject3").gameObject;
		UpgradeEmpty	= new GameObject[4] ;
		UpgradeEmpty[0] = MenuNewTest.Find("Empty/GameObject0").gameObject;
		UpgradeEmpty[1] = MenuNewTest.Find("Empty/GameObject1").gameObject;
		UpgradeEmpty[2] = MenuNewTest.Find("Empty/GameObject2").gameObject;
		UpgradeEmpty[3] = MenuNewTest.Find("Empty/GameObject3").gameObject;
		//創建全局數字sprite庫prefab.
		GameObject obj = GameObject.Find("SpriteRendererUtility(DontDestroy)");
		if( !obj ) SpriteRendererUtility.SpriteRendererObject = (GameObject)Instantiate(Resources.Load("Prefab/SpriteRendererUtility"));
	}
	// Use this for initialization
	void Start () 
	{
	
	}
	// Update is called once per frame
	void Update () 
	{

	}
	//
	void OnGUI()
	{	
		if (GUI.Button(new Rect(10, 110 , 150, 30), "Random!"))
		{ 
			int iRandomExp	= UnityEngine.Random.Range(0, 10000);
			int iRandomLevel= UnityEngine.Random.Range(0, 10000);
			int iRandomMoney= UnityEngine.Random.Range(0, 10000);
			int iRandomRatio= UnityEngine.Random.Range(0, 10000);
			SetUpgradeState(ENUM_UPGRADE_STATE.EXPERIENCE,iRandomExp,	0);
			SetUpgradeState(ENUM_UPGRADE_STATE.LEVEL	,iRandomLevel,	0);
			SetUpgradeState(ENUM_UPGRADE_STATE.MONEY	,iRandomMoney,	0);
			SetUpgradeState(ENUM_UPGRADE_STATE.RATIO	,iRandomRatio,	0);
		}
		if (GUI.Button(new Rect(10, 150 , 150, 30), "RandomII"))
		{ 
			int iRandomRatio= UnityEngine.Random.Range(0, 10000);
			int iRandomFont	= UnityEngine.Random.Range(0, 5);
			SetUpgradeState(ENUM_UPGRADE_STATE.RATIO	,iRandomRatio,	iRandomFont);
		}
		if (GUI.Button(new Rect(10, 190 , 150, 30), "RandomIII"))
		{ 
			int iRandomEmpty= UnityEngine.Random.Range(0, 10000);
			int iRandomFont	= UnityEngine.Random.Range(0, 5);
			SetUpgradeState(ENUM_UPGRADE_STATE.EMPTY	,iRandomEmpty,	iRandomFont);
		}
		//Test DontDestroyOnLoad()
		if (GUI.Button(new Rect(160, 110 , 150, 30), "Main Scene!"))
		{ 
			Application.LoadLevel("main") ;
		}
		if (GUI.Button(new Rect(160, 150 , 150, 30), "Empty Scene!"))
		{ 
			Application.LoadLevel("test") ;
		}
	}
	//	
	void SetUpgradeState(ENUM_UPGRADE_STATE state, int iIndex , int iOption)
	{
		switch( state )
		{
			case ENUM_UPGRADE_STATE.NONE: break;
			case ENUM_UPGRADE_STATE.EXPERIENCE:
			{
				if ((iIndex > 9999) || (iIndex < 0)) iIndex = 0 ;//out of range
				string sResult = Convert.ToString(iIndex) ;
				int iMaxDigit = 4 ;//位數上限
				for (int i = 0 ; i < iMaxDigit ; i++) UpgradeExp[i].SetActive(true);//reset
				int iCount = 0 ;
				for (int i = 0 ; i < sResult.Length ; i++)
				{
					string s = "" + sResult[sResult.Length-1-i] ;//從此字串個位數開始
					int vOut = int.Parse(s);
					if (i >= iMaxDigit) break ;//防當
					UpgradeExp[iMaxDigit-1-i].GetComponent<JSK_MenuObject>().setMenuState(JSK_MenuState.ENUM_SPRITE_INDEX, vOut);//從畫面上的個位數開始
					iCount++ ;
				}
				for (/*    */; iCount < iMaxDigit ; iCount++)
				{
					UpgradeExp[iMaxDigit-1-iCount].GetComponent<JSK_MenuObject>().setMenuState(JSK_MenuState.ENUM_SPRITE_INDEX, 0);//從畫面上的個位數開始補0
				}
				break ;
			}
			case ENUM_UPGRADE_STATE.LEVEL:
			{
				if ((iIndex > 9999) || (iIndex < 0)) iIndex = 0 ;//out of range
				string sResult = Convert.ToString(iIndex) ;
				int iMaxDigit = 4 ;//位數上限
				for (int i = 0 ; i < iMaxDigit ; i++) UpgradeLevel[i].SetActive(true);//reset
				int iCount = 0 ;
				for (int i = 0 ; i < sResult.Length ; i++)
				{
					string s = "" + sResult[i] ;//從此字串最高位數開始
					int vOut = int.Parse(s);
					if (i >= iMaxDigit) break ;//防當
					UpgradeLevel[i].GetComponent<JSK_MenuObject>().setMenuState(JSK_MenuState.ENUM_SPRITE_INDEX, vOut);//從畫面上的最高位數開始
					iCount++ ;
				}
				for (/*    */; iCount < iMaxDigit ; iCount++)
				{
					UpgradeLevel[iCount].SetActive(false);//把未顯示的位數Hide
				}
				break ;
			}
			case ENUM_UPGRADE_STATE.MONEY:
			{
				if ((iIndex > 9999) || (iIndex < 0)) iIndex = 0 ;//out of range
				string sResult = Convert.ToString(iIndex) ;
				int iMaxDigit = 4 ;//位數上限
				for (int i = 0 ; i < iMaxDigit ; i++) UpgradeMoney[i].SetActive(true);//reset
				int iCount = 0 ;
				for (int i = 0 ; i < sResult.Length ; i++)
				{
					string s = "" + sResult[sResult.Length-1-i] ;//從此字串個位數開始
					int vOut = int.Parse(s);
					if (i >= iMaxDigit) break ;//防當
					UpgradeMoney[iMaxDigit-1-i].GetComponent<JSK_MenuObject>().setMenuState(JSK_MenuState.ENUM_SPRITE_INDEX, vOut);//從畫面上的個位數開始
					iCount++ ;
				}
				for (/*    */; iCount < iMaxDigit ; iCount++)
				{
					UpgradeMoney[iMaxDigit-1-iCount].SetActive(false);//把未顯示的位數Hide
				}
				break ;
			}
			case ENUM_UPGRADE_STATE.RATIO:
			{
				if ((iIndex > 9999) || (iIndex < 0)) iIndex = 0 ;//out of range
				string sResult = Convert.ToString(iIndex) ;
				int iMaxDigit = 4 ;//位數上限
				for (int i = 0 ; i < iMaxDigit ; i++) UpgradeRatio[i].SetActive(true);//reset
				int iCount = 0 ;
				for (int i = 0 ; i < sResult.Length ; i++)
				{
					string s = "" + sResult[sResult.Length-1-i] ;//從此字串個位數開始
					int vOut = int.Parse(s);
					if (i >= iMaxDigit) break ;//防當
					SpriteRendererUtility.Instance().SetGameObjectSprite(UpgradeRatio[iMaxDigit-1-i] , vOut , iOption) ;
					iCount++ ;
				}
				for (/*    */; iCount < iMaxDigit ; iCount++)
				{
					UpgradeRatio[iMaxDigit-1-iCount].SetActive(false);//把未顯示的位數Hide
				}
				break ;
			}
			case ENUM_UPGRADE_STATE.EMPTY:
			{
				int iRandomBehaviour = UnityEngine.Random.Range(0, 3);
				SpriteRendererUtility.Instance().SetGameObjectNumber(UpgradeEmpty , 359 , 4 , iOption , iRandomBehaviour) ;
				break ;
			}
			default:break ;
		}
	}
}
