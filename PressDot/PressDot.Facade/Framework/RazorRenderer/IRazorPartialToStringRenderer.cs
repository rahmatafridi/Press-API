using System.Threading.Tasks;

namespace PressDot.Facade.Framework.RazorRenderer
{
    public interface IRazorPartialToStringRenderer
    {
        Task<string> RenderPartialToStringAsync<TModel>(string partialName, TModel model);
    }
}
