using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmFinderTMDB.Source.Presentation.NavigationService
{
    public interface INavigationService
    {
        Task NavigationToAsync(string route, IDictionary<string, object> parameter = null);

        T GetPageViewModel<T>() where T : new();
    }
}
