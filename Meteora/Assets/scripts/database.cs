using UnityEngine;
using System.Collections;

public class database : MonoBehaviour 
{
	public static string user = "", name = "", Act = "Login";
	private string password = "", rePass = "", message = "";

	private bool register = false;

	private void OnGUI()
	{
		if (message != "")
			GUILayout.Box(message);

		if (register)
		{
			GUILayout.Label("Ник в игре");
			user = GUILayout.TextField(user);
			GUILayout.Label("Имя");
			name = GUILayout.TextField(name);
			GUILayout.Label("Пароль");
			password = GUILayout.PasswordField(password, "*"[0]);
			GUILayout.Label("Повторите пароль");
			rePass = GUILayout.PasswordField(rePass, "*"[0]);

			GUILayout.BeginHorizontal();

			if (GUILayout.Button("Назад"))
				register = false;

			if (GUILayout.Button("Регистрация"))
			{
				message = "";

				if (user == "" || name == "" || password == "")
					message += "Заполните все поля! \n";
				else
				{
					if (password == rePass)
					{
						Act = "Register";
						WWWForm form = new WWWForm();
						form.AddField("user", user);
						form.AddField("name", name);
						form.AddField("password", password);
						form.AddField("Act", Act);
						WWW w = new WWW("http://sinepolsky.000webhostapp.com/Login.php", form);
						StartCoroutine(registerFunc(w));
					}
					else
						message += "Повторите пароль! \n";
				}
			}

			GUILayout.EndHorizontal();
		}
		else
		{
			GUILayout.Label("Ник в игре:");
			user = GUILayout.TextField(user);
			GUILayout.Label("Пароль:");
			password = GUILayout.PasswordField(password, "*"[0]);

			GUILayout.BeginHorizontal();

			if (GUILayout.Button("Вход"))
			{
				message = "";

				if (user == "" || password == "")
					message += "Заполните все поля! \n";
				else
				{
					WWWForm form = new WWWForm();
					form.AddField("user", user);
					form.AddField("password", password);
					form.AddField("Act", Act);
					WWW w = new WWW("http://sinepolsky.000webhostapp.com/Login.php", form);
					StartCoroutine(login(w));
				}
			}

			if (GUILayout.Button("Регистрация"))
				register = true;

			GUILayout.EndHorizontal();
		}
	}

	IEnumerator login(WWW w)
	{
		yield return w;
		if (w.error == null)
		{
			if (w.text == "login-SUCCESS")
			{
				print("WOOOOOOOOOOOOOOO!");
			}
			else
				message += w.text;
		}
		else
		{
			message += "ERROR: " + w.error + "\n";
		}
	}

	IEnumerator registerFunc(WWW w)
	{
		yield return w;
		if (w.error == null)
		{
			message += w.text;
		}
		else
		{
			message += "ERROR: " + w.error + "\n";
		}
	}
}
