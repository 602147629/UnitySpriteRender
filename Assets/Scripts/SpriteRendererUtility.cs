using UnityEngine;
using System.Collections;
using System ;//for BitConverter

public enum ENUM_SPRITE_RENDERER_BEHAVIOUR : int
{
	FILL_ZERO = 0,			//最高位數為0時補0,(沒有個位數為0時補0這種東西),	Ex:"0359"
	LEFT_ZERO_DISABLE = 1,	//從最高位數開始,最高位數為0時disable它,		Ex:" 359"
	RIGHT_ZERO_DISABLE = 2,	//從個位數開始,最高位數為0時disable它,			Ex:"359 "
	MAX
}

public enum ENUM_SPRITE_PACKAGE : int
{
	WHITE		= 0,
	BLUE		= 1,
	WHITE_BLUE	= 2,
	LIGHT_YELLOW= 3,
	DARK_YELLOW	= 4,
	MAX
}

public class SpriteRendererUtility : MonoBehaviour 
{
	public	Sprite[]	SpriteListWhite		= null;
	public	Sprite[]	SpriteListBlue		= null;
	public	Sprite[]	SpriteListWhiteBlue	= null;
	public	Sprite[]	SpriteListLightYellow= null;
	public	Sprite[]	SpriteListDarkYellow= null;
	//self
	public	static		GameObject	SpriteRendererObject = null ;//指向自己的prefab,不保存也沒關係?
	protected static 	SpriteRendererUtility Self;
	protected Sprite 	SpriteTemp = null ;
	//
	//Init
	void Awake()
	{
		DontDestroyOnLoad(gameObject);//保護自己不被刪除
		gameObject.name = "SpriteRendererUtility(DontDestroy)" ;
	}
	// Use this for initialization
	void Start () 
	{
		Self = this;
	}	
	// Update is called once per frame
	void Update () 
	{
	
	}
	//Instance
	public static SpriteRendererUtility Instance()
	{
		return Self;
	}
	//Set GameObject 的 SpriteRenderer 給指定 value 的數字
	public void SetGameObjectSprite(GameObject gameObject , int iValue , int iSpritePackageIndex)
	{
		switch( iSpritePackageIndex )
		{
			case (int)ENUM_SPRITE_PACKAGE.WHITE: SpriteTemp = SpriteListWhite[iValue]; break ;
			case (int)ENUM_SPRITE_PACKAGE.BLUE: SpriteTemp = SpriteListBlue[iValue]; break ;
			case (int)ENUM_SPRITE_PACKAGE.WHITE_BLUE: SpriteTemp = SpriteListWhiteBlue[iValue]; break ;
			case (int)ENUM_SPRITE_PACKAGE.LIGHT_YELLOW: SpriteTemp = SpriteListLightYellow[iValue]; break ;
			case (int)ENUM_SPRITE_PACKAGE.DARK_YELLOW: SpriteTemp = SpriteListDarkYellow[iValue]; break ;
			default:SpriteTemp = SpriteListWhite[iValue]; break ;
		}
		//set sprite
		gameObject.transform.GetComponent<SpriteRenderer>().sprite = SpriteTemp ;
	}
	//
	public void SetGameObjectNumber(GameObject[] gameObject , int iValue , int iDigit , int iSpritePackageIndex , int iBehaviour)
	{
		int iMaxDigit = iDigit ;//位數上限
		int iLimit = 1 ;
		for (int i = 0 ; i < iMaxDigit ; i++) iLimit *= 10 ;
		if ((iValue >= iLimit) || (iValue < 0)) iValue = 0 ;//out of range
		string sResult = Convert.ToString(iValue) ;
		if (sResult.Length > iMaxDigit)
			return ;//不可能出現?
		else if (sResult.Length == iMaxDigit)
		{
			for (int i = 0 ; i < iMaxDigit ; i++)
			{
				gameObject[i].SetActive(true);//reset enable
				string s = "" + sResult[i] ;//位數與欄位數相符,從那邊開始都可
				int iOut = int.Parse(s);
				Instance().SetGameObjectSprite(gameObject[i] , iOut , iSpritePackageIndex) ;
			}
		}
		else
		{
			for (int i = 0 ; i < iMaxDigit ; i++) gameObject[i].SetActive(true);//reset enable
			//
			int iFillCount = 0 ;
			if (iBehaviour == (int)ENUM_SPRITE_RENDERER_BEHAVIOUR.FILL_ZERO)//最高位數為0時補0
			{
				string sTemp  = sResult.PadLeft(iDigit, '0');//高位數,不足補0
				sResult = sTemp ;
			}
			else if (iBehaviour == (int)ENUM_SPRITE_RENDERER_BEHAVIOUR.LEFT_ZERO_DISABLE)//從最高位數開始,最高位數為0時disable它		
			{
				iFillCount = iMaxDigit - sResult.Length ;
			}
			else if (iBehaviour == (int)ENUM_SPRITE_RENDERER_BEHAVIOUR.RIGHT_ZERO_DISABLE)//從個位數開始,最高位數為0時disable它
			{
				iFillCount = 0 ;
			}
			int iCount = 0 ;
			for (int i = 0 ; i < iFillCount ; i++) 
			{
				gameObject[i].SetActive(false);//從最高位數開始,把未顯示的位數Hide
				iCount++ ;
			}
			for (int i = 0 ; i < sResult.Length ; i++)
			{
				string s = "" + sResult[i] ;
				int iOut = int.Parse(s);
				Instance().SetGameObjectSprite(gameObject[iFillCount+i] , iOut , iSpritePackageIndex) ;
				iCount++ ;
			}
			for (/*    */; iCount < iMaxDigit ; iCount++)
			{
				gameObject[iCount].SetActive(false);//把未顯示的位數Hide
			}
		}
	}
}
