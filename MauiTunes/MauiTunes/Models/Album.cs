//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Text.Json.Serialization;
//using System.Threading.Tasks;

//namespace MauiTunes.Entities
//{
//    public class Album
//    {
//        [JsonPropertyName("album_type")]
//        public string AlbumType { get; set; }

//        [JsonPropertyName("artists")]
//        public List<Artist> Artists { get; set; }

//        [JsonPropertyName("available_markets")]
//        public List<string> AvailableMarkets { get; set; }

//        [JsonPropertyName("external_urls")]
//        public string ExternalUrls { get; set; }

//        [JsonPropertyName("href")]
//        public string Href { get; set; }

//        [JsonPropertyName("id")]
//        public string Id { get; set; }

//        [JsonPropertyName("images")]
//        public List<Image> Images { get; set; }

//        [JsonPropertyName("name")]
//        public string Name { get; set; }

//        [JsonPropertyName("release_date")]
//        public string ReleaseDate { get; set; }

//        [JsonPropertyName("release_date_precision")]
//        public string ReleaseDatePrecision { get; set; }

//        [JsonPropertyName("total_tracks")]
//        public int TotalTracks { get; set; }

//        [JsonPropertyName("type")]
//        public string Type { get; set; }

//        [JsonPropertyName("uri")]
//        public string Uri { get; set; }
//    }

//    public class Albums
//    {
//        [JsonPropertyName("href")]
//        public string Href { get; set; }

//        [JsonPropertyName("items")]
//        public List<Album> Items { get; set; }

//        [JsonPropertyName("limit")]
//        public int Limit { get; set; }

//        [JsonPropertyName("next")]
//        public string Next { get; set; }

//        [JsonPropertyName("offset")]
//        public int Offset { get; set; }

//        [JsonPropertyName("previous")]
//        public object Previous { get; set; }

//        [JsonPropertyName("total")]
//        public int Total { get; set; }
//    }
//}
