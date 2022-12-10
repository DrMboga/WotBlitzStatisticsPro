namespace WotBlitzStatisticsPro.Model
{
    	/// <summary>
	/// Represents Application settings section related with Wargaming API
	/// </summary>
	public interface IWargamingApiSettings
	{
		/// <summary>
		/// Wargaming applicationId
		/// </summary>
		string ApplicationId { get; set; }

		/// <summary>
		/// WOT Blitz API url
		/// </summary>
		string BlitzApiUrl { get; set; }

		/// <summary>
		/// WOT API url
		/// </summary>
		string WotApiUrl { get; set; }

		/// <summary>
		/// If true, application uses mock data instead of WG API calls
		/// </summary>
		bool UseMockData { get; set; }
	}

}