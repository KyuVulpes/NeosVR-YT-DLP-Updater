using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using NeosModLoader;
using Newtonsoft.Json;

namespace YT_DLP_Updater {
	public class MainClass : NeosMod {
		private const           string REPO_LATEST_URL = "https://api.github.com/repos/yt-dlp/yt-dlp/releases/latest";
		private static readonly string YT_DLP_EXE_LOC  = Path.Combine( Environment.CurrentDirectory, "RuntimeData", $"yt-dlp{( RuntimeInformation.IsOSPlatform( OSPlatform.Windows ) ? ".exe" : string.Empty )}" );

		public override string Name {
			get;
		} = "YT-DLP Updater";

		public override string Author {
			get;
		} = "Kyu Vulpes";

		public override string Version {
			get;
		} = "1.0.0";

		public override string Link {
			get;
		} = "https://github.com/KyuVulpes/NeosVR-YT-DLP-Updater";

		public override void OnEngineInit() {
			string  currentVer;

			base.OnEngineInit();
			
			var release = GetLatestRelease();

			try {
				currentVer = GetYouTubeDownloaderPlusVersion();
			} catch ( Exception e ) {
				Error( $"Received: {e}" );
				
				UpdateYouTubeDownloaderPlus( release );

				return;
			}

			Debug( $"Checking to see if {currentVer} is the same as {release}" );

			if ( currentVer == release.TagName ) {
				return;
			}

			UpdateYouTubeDownloaderPlus( release );
		}

		private static void UpdateYouTubeDownloaderPlus( Release release ) {
			var exe = default( Asset );

			foreach ( var asset in release.Assets ) {
				if ( RuntimeInformation.IsOSPlatform( OSPlatform.Windows ) ) {
					if ( asset.Name != "yt-dlp.exe" ) {
						continue;
					}

					exe = asset;
						
					break;
				} else {
					if ( asset.Name != "yt-dlp" ) {
						continue;
					}

					exe = asset;
						
					break;
				}
			}

			if ( exe == default ) {
				Error( "Failed to find correct asset that contains the executable, aborting for now." );

				return;
			}

			using ( var webClient = new WebClient() ) {
				webClient.Headers["User-Agent"] = "NeosVR YT-DLP Updater/1.0.0";
				
				webClient.DownloadFile( exe.BrowserDownloadUrl, YT_DLP_EXE_LOC );
			}

			Msg( "Successfully updated YouTube Downloader Plus, enjoy better video downloading!" );
		}

		private static string GetYouTubeDownloaderPlusVersion() {
			using ( var ytdlp = new Process() {
					   StartInfo = {
						   FileName               = YT_DLP_EXE_LOC,
						   Arguments              = "--version",
						   RedirectStandardOutput = true,
						   UseShellExecute        = false,
					   },
				   } ) {
				ytdlp.Start();
				ytdlp.WaitForExit();

				return ytdlp.StandardOutput.ReadToEnd().Trim();
			}
		}

		private static Release GetLatestRelease() {
			string json;

			using ( var webClient = new WebClient() ) {
				webClient.Headers["User-Agent"] = "NeosVR YT-DLP Updater/1.0.0";
				
				json = webClient.DownloadString( REPO_LATEST_URL );
			}

			return JsonConvert.DeserializeObject<Release>( json );
		}
	}

	// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
	internal class Asset {
		[JsonProperty( "url" )]
		public string Url { get; }

		[JsonProperty( "browser_download_url" )]
		public string BrowserDownloadUrl { get; }

		[JsonProperty( "id" )]
		public int Id { get; }

		[JsonProperty( "node_id" )]
		public string NodeId { get; }

		[JsonProperty( "name" )]
		public string Name { get; }

		[JsonProperty( "label" )]
		public string Label { get; }

		[JsonProperty( "state" )]
		public string State { get; }

		[JsonProperty( "content_type" )]
		public string ContentType { get; }

		[JsonProperty( "size" )]
		public int Size { get; }

		[JsonProperty( "download_count" )]
		public int DownloadCount { get; }

		[JsonProperty( "created_at" )]
		public DateTime CreatedAt { get; }

		[JsonProperty( "updated_at" )]
		public DateTime UpdatedAt { get; }

		[JsonProperty( "uploader" )]
		public Uploader Uploader { get; }

		[JsonConstructor]
		public Asset( [JsonProperty( "url" )] string url, [JsonProperty( "browser_download_url" )] string browserDownloadUrl, [JsonProperty( "id" )] int id, [JsonProperty( "node_id" )] string nodeId,
					  [JsonProperty( "name" )] string name, [JsonProperty( "label" )] string label, [JsonProperty( "state" )] string state, [JsonProperty( "content_type" )] string contentType,
					  [JsonProperty( "size" )] int size, [JsonProperty( "download_count" )] int downloadCount, [JsonProperty( "created_at" )] DateTime createdAt, [JsonProperty( "updated_at" )] DateTime updatedAt,
					  [JsonProperty( "uploader" )] Uploader uploader
		) {
			Url                = url;
			BrowserDownloadUrl = browserDownloadUrl;
			Id                 = id;
			NodeId             = nodeId;
			Name               = name;
			Label              = label;
			State              = state;
			ContentType        = contentType;
			Size               = size;
			DownloadCount      = downloadCount;
			CreatedAt          = createdAt;
			UpdatedAt          = updatedAt;
			Uploader           = uploader;
		}
	}

	internal class Author {

		[JsonProperty( "login" )]
		public string Login { get; }

		[JsonProperty( "id" )]
		public int Id { get; }

		[JsonProperty( "node_id" )]
		public string NodeId { get; }

		[JsonProperty( "avatar_url" )]
		public string AvatarUrl { get; }

		[JsonProperty( "gravatar_id" )]
		public string GravatarId { get; }

		[JsonProperty( "url" )]
		public string Url { get; }

		[JsonProperty( "html_url" )]
		public string HtmlUrl { get; }

		[JsonProperty( "followers_url" )]
		public string FollowersUrl { get; }

		[JsonProperty( "following_url" )]
		public string FollowingUrl { get; }

		[JsonProperty( "gists_url" )]
		public string GistsUrl { get; }

		[JsonProperty( "starred_url" )]
		public string StarredUrl { get; }

		[JsonProperty( "subscriptions_url" )]
		public string SubscriptionsUrl { get; }

		[JsonProperty( "organizations_url" )]
		public string OrganizationsUrl { get; }

		[JsonProperty( "repos_url" )]
		public string ReposUrl { get; }

		[JsonProperty( "events_url" )]
		public string EventsUrl { get; }

		[JsonProperty( "received_events_url" )]
		public string ReceivedEventsUrl { get; }

		[JsonProperty( "type" )]
		public string Type { get; }

		[JsonProperty( "site_admin" )]
		public bool SiteAdmin { get; }

		[JsonConstructor]
		public Author( [JsonProperty( "login" )] string login, [JsonProperty( "id" )] int id, [JsonProperty( "node_id" )] string nodeId, [JsonProperty( "avatar_url" )] string avatarUrl,
					   [JsonProperty( "gravatar_id" )] string gravatarId, [JsonProperty( "url" )] string url, [JsonProperty( "html_url" )] string htmlUrl, [JsonProperty( "followers_url" )] string followersUrl,
					   [JsonProperty( "following_url" )] string followingUrl, [JsonProperty( "gists_url" )] string gistsUrl, [JsonProperty( "starred_url" )] string starredUrl,
					   [JsonProperty( "subscriptions_url" )] string subscriptionsUrl, [JsonProperty( "organizations_url" )] string organizationsUrl, [JsonProperty( "repos_url" )] string reposUrl,
					   [JsonProperty( "events_url" )] string eventsUrl, [JsonProperty( "received_events_url" )] string receivedEventsUrl, [JsonProperty( "type" )] string type, [JsonProperty( "site_admin" )] bool siteAdmin
		) {
			Login             = login;
			Id                = id;
			NodeId            = nodeId;
			AvatarUrl         = avatarUrl;
			GravatarId        = gravatarId;
			Url               = url;
			HtmlUrl           = htmlUrl;
			FollowersUrl      = followersUrl;
			FollowingUrl      = followingUrl;
			GistsUrl          = gistsUrl;
			StarredUrl        = starredUrl;
			SubscriptionsUrl  = subscriptionsUrl;
			OrganizationsUrl  = organizationsUrl;
			ReposUrl          = reposUrl;
			EventsUrl         = eventsUrl;
			ReceivedEventsUrl = receivedEventsUrl;
			Type              = type;
			SiteAdmin         = siteAdmin;
		}
	}

	internal class Release {

		[JsonProperty( "url" )]
		public string Url { get; }

		[JsonProperty( "html_url" )]
		public string HtmlUrl { get; }

		[JsonProperty( "assets_url" )]
		public string AssetsUrl { get; }

		[JsonProperty( "upload_url" )]
		public string UploadUrl { get; }

		[JsonProperty( "tarball_url" )]
		public string TarballUrl { get; }

		[JsonProperty( "zipball_url" )]
		public string ZipballUrl { get; }

		[JsonProperty( "discussion_url" )]
		public string DiscussionUrl { get; }

		[JsonProperty( "id" )]
		public int Id { get; }

		[JsonProperty( "node_id" )]
		public string NodeId { get; }

		[JsonProperty( "tag_name" )]
		public string TagName { get; }

		[JsonProperty( "target_commitish" )]
		public string TargetCommitish { get; }

		[JsonProperty( "name" )]
		public string Name { get; }

		[JsonProperty( "body" )]
		public string Body { get; }

		[JsonProperty( "draft" )]
		public bool Draft { get; }

		[JsonProperty( "prerelease" )]
		public bool Prerelease { get; }

		[JsonProperty( "created_at" )]
		public DateTime CreatedAt { get; }

		[JsonProperty( "published_at" )]
		public DateTime PublishedAt { get; }

		[JsonProperty( "author" )]
		public Author Author { get; }

		[JsonProperty( "assets" )]
		public IReadOnlyList<Asset> Assets { get; }

		[JsonConstructor]
		public Release( [JsonProperty( "url" )] string url, [JsonProperty( "html_url" )] string htmlUrl, [JsonProperty( "assets_url" )] string assetsUrl, [JsonProperty( "upload_url" )] string uploadUrl,
						[JsonProperty( "tarball_url" )] string tarballUrl, [JsonProperty( "zipball_url" )] string zipballUrl, [JsonProperty( "discussion_url" )] string discussionUrl, [JsonProperty( "id" )] int id,
						[JsonProperty( "node_id" )] string nodeId, [JsonProperty( "tag_name" )] string tagName, [JsonProperty( "target_commitish" )] string targetCommitish, [JsonProperty( "name" )] string name,
						[JsonProperty( "body" )] string body, [JsonProperty( "draft" )] bool draft, [JsonProperty( "prerelease" )] bool prerelease, [JsonProperty( "created_at" )] DateTime createdAt,
						[JsonProperty( "published_at" )] DateTime publishedAt, [JsonProperty( "author" )] Author author, [JsonProperty( "assets" )] List<Asset> assets
		) {
			Url             = url;
			HtmlUrl         = htmlUrl;
			AssetsUrl       = assetsUrl;
			UploadUrl       = uploadUrl;
			TarballUrl      = tarballUrl;
			ZipballUrl      = zipballUrl;
			DiscussionUrl   = discussionUrl;
			Id              = id;
			NodeId          = nodeId;
			TagName         = tagName;
			TargetCommitish = targetCommitish;
			Name            = name;
			Body            = body;
			Draft           = draft;
			Prerelease      = prerelease;
			CreatedAt       = createdAt;
			PublishedAt     = publishedAt;
			Author          = author;
			Assets          = assets;
		}
	}

	internal class Uploader {

		[JsonProperty( "login" )]
		public string Login { get; }

		[JsonProperty( "id" )]
		public int Id { get; }

		[JsonProperty( "node_id" )]
		public string NodeId { get; }

		[JsonProperty( "avatar_url" )]
		public string AvatarUrl { get; }

		[JsonProperty( "gravatar_id" )]
		public string GravatarId { get; }

		[JsonProperty( "url" )]
		public string Url { get; }

		[JsonProperty( "html_url" )]
		public string HtmlUrl { get; }

		[JsonProperty( "followers_url" )]
		public string FollowersUrl { get; }

		[JsonProperty( "following_url" )]
		public string FollowingUrl { get; }

		[JsonProperty( "gists_url" )]
		public string GistsUrl { get; }

		[JsonProperty( "starred_url" )]
		public string StarredUrl { get; }

		[JsonProperty( "subscriptions_url" )]
		public string SubscriptionsUrl { get; }

		[JsonProperty( "organizations_url" )]
		public string OrganizationsUrl { get; }

		[JsonProperty( "repos_url" )]
		public string ReposUrl { get; }

		[JsonProperty( "events_url" )]
		public string EventsUrl { get; }

		[JsonProperty( "received_events_url" )]
		public string ReceivedEventsUrl { get; }

		[JsonProperty( "type" )]
		public string Type { get; }

		[JsonProperty( "site_admin" )]
		public bool SiteAdmin { get; }

		[JsonConstructor]
		public Uploader( [JsonProperty( "login" )] string login, [JsonProperty( "id" )] int id, [JsonProperty( "node_id" )] string nodeId, [JsonProperty( "avatar_url" )] string avatarUrl,
						 [JsonProperty( "gravatar_id" )] string gravatarId, [JsonProperty( "url" )] string url, [JsonProperty( "html_url" )] string htmlUrl, [JsonProperty( "followers_url" )] string followersUrl,
						 [JsonProperty( "following_url" )] string followingUrl, [JsonProperty( "gists_url" )] string gistsUrl, [JsonProperty( "starred_url" )] string starredUrl,
						 [JsonProperty( "subscriptions_url" )] string subscriptionsUrl, [JsonProperty( "organizations_url" )] string organizationsUrl, [JsonProperty( "repos_url" )] string reposUrl,
						 [JsonProperty( "events_url" )] string eventsUrl, [JsonProperty( "received_events_url" )] string receivedEventsUrl, [JsonProperty( "type" )] string type,
						 [JsonProperty( "site_admin" )] bool siteAdmin
		) {
			Login             = login;
			Id                = id;
			NodeId            = nodeId;
			AvatarUrl         = avatarUrl;
			GravatarId        = gravatarId;
			Url               = url;
			HtmlUrl           = htmlUrl;
			FollowersUrl      = followersUrl;
			FollowingUrl      = followingUrl;
			GistsUrl          = gistsUrl;
			StarredUrl        = starredUrl;
			SubscriptionsUrl  = subscriptionsUrl;
			OrganizationsUrl  = organizationsUrl;
			ReposUrl          = reposUrl;
			EventsUrl         = eventsUrl;
			ReceivedEventsUrl = receivedEventsUrl;
			Type              = type;
			SiteAdmin         = siteAdmin;
		}
	}


}
