namespace StationCasinos.WebAPI.Service.Models
{
    public sealed class GetPatronRequest
    {
        /// <summary>
        /// Gets or sets the PatronId value.
        /// </summary>
        public string PatronId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the optional boarding pass magnetic stripe OCR
        /// </summary>
        public string Magstripe
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the optional PropertyCode value.
        /// </summary>
        public string PreferredProperty
        {
            get;
            set;
        }
    }
}
