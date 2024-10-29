using System;
namespace FilmFinderTMDB.Source.Data.Model
{
	public class BaseResponseModel
	{
		public BaseResponseModel()
		{
		}
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}

