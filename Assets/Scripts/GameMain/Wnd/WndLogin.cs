using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using XEngine;

public class WndLogin : Window {

	private InputField m_edtName;
	private InputField m_edtPwd;
	private Button m_btnLogin;


	public override void Destroy ()
	{
		m_btnLogin = null;
		base.Destroy ();
	}

	protected override void InitCtrl (Transform tf)
	{
		m_edtName = tf.Find ("EdtName").GetComponent<InputField> ();
		m_edtPwd = tf.Find ("EdtPwd").GetComponent<InputField> ();
		m_btnLogin = tf.Find ("BtnLogin").GetComponent<Button>();
	}

	protected override void Register ()
	{
		m_btnLogin.onClick.AddListener(OnLogin);
	}

	void OnLogin()
	{
		Debug.Log ("Login Name:"+m_edtName.text+",Pwd:"+m_edtPwd.text);
		GameMain.m_wndMgr.CloseWindow (WndID.Login);
	}
}
