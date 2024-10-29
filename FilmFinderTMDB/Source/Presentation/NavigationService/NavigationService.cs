using FilmFinderTMDB.Source.Presentation.NavigationService;

namespace FilmFinderTMDB.Source.Presentation.NavigationService
{
    public class NavigationService : INavigationService
    {
        public Task NavigationToAsync(string route, IDictionary<string, object> parameter = null)
        {
            if (parameter != null && parameter.Any())
                return Shell.Current.GoToAsync(route, parameter);
            else
                return Shell.Current.GoToAsync(route);
        }

        public T GetPageViewModel<T>() where T : new()
        {
            var pageDetails = Shell.Current.CurrentItem.CurrentItem.Stack.Where(f => f != null && f.BindingContext.GetType() == typeof(T)).FirstOrDefault();
            if (pageDetails != null)
                return (T)pageDetails.BindingContext;
            return default(T);
        }
    }
}
