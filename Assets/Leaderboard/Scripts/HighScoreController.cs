using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HighScore
{
	public HighScore(string player, int score)
	{
		this.player = player;
		this.score = score;
	}

	public string player { get; set; }
	public int score { get; set; }
}

public class HighScoreController : MonoBehaviour
{
	private static string secretKey = "mySecretKey"; // Edit this value and make sure it's the same as the one stored on the server
	private static string hostUrl = "http://vps61447.ovh.net/games/jsn/";
	private static string addScoreURL = hostUrl + "addscore.php?"; //be sure to add a ? to your url
	private static string highscoreURL = hostUrl + "display.php";
	public static string playerName = "Default";
	public List<HighScore> scores = new List<HighScore>();

	public static string Md5Sum(string strToEncrypt)
	{
		System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
		byte[] bytes = ue.GetBytes(strToEncrypt);
		
		// encrypt bytes
		System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
		byte[] hashBytes = md5.ComputeHash(bytes);
		
		// Convert the encrypted bytes back to a string (base 16)
		string hashString = "";
		
		for (int i = 0; i < hashBytes.Length; i++)
		{
			hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
		}
		
		return hashString.PadLeft(32, '0');
	}

	public void UpdateHighscores(){
		StartCoroutine(GetScores());
	}

	public void AddHighscore(string name, int score){
		Debug.Log ("Adding highscore " + name + " " + score);
		StartCoroutine(PostScores(name, score));
	}

	IEnumerator PostScores(string name, float score)
	{
		//This connects to a server side php script that will add the name and score to a MySQL DB.
		// Supply it with a string representing the players name and the players score.
		string hash = Md5Sum(name + score + secretKey);
		
		string post_url = addScoreURL + "name=" + WWW.EscapeURL(name) + "&score=" + score + "&hash=" + hash;
		Debug.Log ("url " + post_url);
		Debug.Log ("hash = " + hash);
		// Post the URL to the site and create a download object to get the result.
		WWW hs_post = new WWW(post_url);
		yield return hs_post; // Wait until the download is done
		
		if (hs_post.error != null)
		{
			print("There was an error posting the high score: " + hs_post.error);
		}
	}

	IEnumerator GetScores()
	{
		// gameObject.guiText.text = "Loading Scores";
		WWW hs_get = new WWW(highscoreURL);
		yield return hs_get;
		
		if (hs_get.error != null)
		{
			print("There was an error getting the high score: " + hs_get.error);
		}
		else
		{
			scores = new List<HighScore>();
			string[] scoreList = hs_get.text.Split('\n');
			foreach(string scoreEntry in scoreList)
			{
				if(scoreEntry == "") continue;
				string[] entry = scoreEntry.Split('|');
				scores.Add(new HighScore(entry[0], int.Parse(entry[1])));
			}
		}
	}
}
