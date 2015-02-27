﻿using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;


namespace Portal.ASP
{


	public class NET
	{


		public static string GetParameter(string strRequestedKey)
		{
			if (StringComparer.OrdinalIgnoreCase.Equals(System.Web.HttpContext.Current.Request.HttpMethod, "GET")) {
				return System.Web.HttpContext.Current.Request.QueryString[strRequestedKey];
			} else if (StringComparer.OrdinalIgnoreCase.Equals(System.Web.HttpContext.Current.Request.HttpMethod, "POST")) {
				return System.Web.HttpContext.Current.Request.Form[strRequestedKey];
			} else {
				throw new System.Web.HttpException(500, "Invalid request method");
			}

			return null;
		}


		// COR.ASP.NET.RecursiveFindControl(Me, ".NET ID")
		public static System.Web.UI.Control RecursiveFindControl(System.Web.UI.Control ctrlParentControl, string strControlName)
		{
			if (StringComparer.OrdinalIgnoreCase.Equals(ctrlParentControl.ID, strControlName)) {
				return ctrlParentControl;
			}

			foreach (System.Web.UI.Control ctrlThisControl in ctrlParentControl.Controls) {
				if (StringComparer.OrdinalIgnoreCase.Equals(ctrlThisControl.ID, strControlName)) {
					return ctrlThisControl;
				} else {
					if (ctrlThisControl.HasControls()) {
						System.Web.UI.Control ctrlFoundControl = RecursiveFindControl(ctrlThisControl, strControlName);
						if (ctrlFoundControl != null) {
							return ctrlFoundControl;
						}
					}
				}
			}

			return null;
		}


		// COR.ASP.NET.StripInvalidPathChars("")
		public static string StripInvalidPathChars(string str)
		{
			string strReturnValue = null;

			if (str == null) {
				return strReturnValue;
			}

			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			char[] achrInvalidPathChars = System.IO.Path.GetInvalidPathChars();


			foreach (char cThisChar in str) {
				bool bIsValid = true;

				foreach (char cInvalid in achrInvalidPathChars) {
					if (cThisChar == cInvalid) {
						bIsValid = false;
						break; // TODO: might not be correct. Was : Exit For
					}
				}

				if (bIsValid) {
					sb.Append(cThisChar);
				}
			}

			strReturnValue = sb.ToString();
			sb = null;
			return strReturnValue;
		}
		// StripInvalidPathChars


		public static string GetContentDisposition(string strFileName)
		{
			return GetContentDisposition(strFileName, "attachment");
		}


		// http://www.iana.org/assignments/cont-disp/cont-disp.xhtml
		public static string GetContentDisposition(string strFileName, string strDisposition)
		{
			// http://stackoverflow.com/questions/93551/how-to-encode-the-filename-parameter-of-content-disposition-header-in-http
			string contentDisposition = null;
			strFileName = StripInvalidPathChars(strFileName);

			if (string.IsNullOrEmpty(strDisposition)) {
				strDisposition = "inline";
			}

			if (System.Web.HttpContext.Current != null && System.Web.HttpContext.Current.Request.Browser != null) {
				if ((System.Web.HttpContext.Current.Request.Browser.Browser == "IE" & (System.Web.HttpContext.Current.Request.Browser.Version == "7.0" | System.Web.HttpContext.Current.Request.Browser.Version == "8.0"))) {
					contentDisposition = strDisposition + "; filename=" + Uri.EscapeDataString(strFileName).Replace("'", Uri.HexEscape('\''));
				} else if ((System.Web.HttpContext.Current.Request.Browser.Browser == "Safari")) {
					contentDisposition = strDisposition + "; filename=" + strFileName;
				} else {
					contentDisposition = strDisposition + "; filename*=UTF-8''" + Uri.EscapeDataString(strFileName);
				}
			} else {
				contentDisposition = strDisposition + "; filename*=UTF-8''" + Uri.EscapeDataString(strFileName);
			}

			return contentDisposition;
		}
		// GetContentDisposition


		public static string ContentUrl(string strPath)
		{
			return ContentUrl(strPath, false);
		}


		public static string ContentUrl(string strPath, bool bNoCheck)
		{
			long lngFileTime = COR.AJAX.Time.ToUnixTicksMapped(strPath, bNoCheck);

			string strReturnValue = null;
			if (lngFileTime == 0) {
				return System.Web.VirtualPathUtility.ToAbsolute(strPath);
			} else {
				strReturnValue = System.Web.VirtualPathUtility.ToAbsolute(strPath);
			}

			if (!bNoCheck) {
				strReturnValue += "?no_cache_LastWriteTimeUtc=" + lngFileTime.ToString();
			}

			return strReturnValue;
			//Response.Write("<h1>" + VirtualPathUtility.ToAbsolute("~/lol/yuk/Home.aspx") + "</h1>")

			//strPath = HttpContext.Current.Server.MapPath(strPath)
			//Dim strAppPath As String = HttpContext.Current.Server.MapPath("~")
			//'Dim url As String = String.Format("~{0}", strPath.Replace(strAppPath, "").Replace("\", "/"))
			//'Dim AbsolutePath As String = Request.ServerVariables("APPL_PHYSICAL_PATH")
			//'Dim AbsolutePath2 As String = HttpContext.Current.Request.ApplicationPath
			//'Dim str As String = HttpRuntime.AppDomainAppVirtualPath

			//Dim url As String = String.Format("{0}", HttpContext.Current.Request.ApplicationPath + strPath.Replace(strAppPath, "").Replace("\", "/"))

			//' https://www4.cor-asp.ch/REM_Demo_DMSc:/inetpub/wwwroot/ajax/NavigationContent.ashx?filter=nofilter1342703258627 404

			//Return url
		}
		// ContentUrl


	}
	// NET


}
// COR.ASP
