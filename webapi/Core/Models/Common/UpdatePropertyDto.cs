namespace ThoughtzLand.Core.Models.Common
{
    public class UpdateStringPropertyDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public string value { get; set; }
    }

	public class UpdateNumberPropertyDto
	{
		public int id { get; set; }
		public string name { get; set; }
		public decimal value { get; set; }
	}
}
