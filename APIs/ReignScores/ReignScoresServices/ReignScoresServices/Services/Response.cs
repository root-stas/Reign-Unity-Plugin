﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace ReignScores.Services
{
	public enum ResponseTypes
	{
		Error,
		Succeeded
	}

	public class WebResponse_Score
	{
		[XmlElement("UserID")] public string UserID;
		[XmlElement("Username")] public string Username;
		[XmlElement("Score")] public int Score;
	}

	public class WebResponse_Achievement
	{
		[XmlElement("ID")] public string ID;
		[XmlElement("PercentComplete")] public float PercentComplete;
	}

	[XmlRoot("ClientResponse")]
	public class WebResponse
	{
		[XmlAttribute("Type")] public ResponseTypes Type;
		[XmlElement("ErrorMessage")] public string ErrorMessage;
		[XmlElement("ClientID")] public string ClientID;
		[XmlElement("UserID")] public string UserID;
		[XmlElement("Score")] public List<WebResponse_Score> Scores;
		[XmlElement("Achievement")] public List<WebResponse_Achievement> Achievements;

		public WebResponse() {}
		public WebResponse(ResponseTypes type)
		{
			this.Type = type;
		}
	}

	public static class ResponseTool
	{
		public static string CheckAPIKey(string apiKey, string requiredAPIKey)
		{
			if (apiKey != requiredAPIKey)
			{
				var response = new WebResponse(ResponseTypes.Error)
				{
					ErrorMessage = "Invalide API Key"
				};

				return ResponseTool.GenerateXML(response);
			}
			
			return null;
		}

		public static string GenerateXML(WebResponse response)
		{
			var xml = new XmlSerializer(typeof(WebResponse));
			using (var writer = new StringWriter())
			{
				xml.Serialize(writer, response);
				return writer.ToString();
			}
		}
	}
}