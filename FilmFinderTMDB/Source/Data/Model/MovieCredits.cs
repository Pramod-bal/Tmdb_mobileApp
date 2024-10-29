using System;
using System.Collections.Generic;

namespace FilmFinderTMDB.Source.Data.Model
{
    public class MovieCredits : BaseResponseModel
    {
        public int Id { get; set; }
        public List<Cast> Cast { get; set; }
        public List<Crew> Crew { get; set; }
    }
}

