namespace PulseDonor.MVC.Helper.Interfaces
{
    public interface IApiClientHelper
    {
        Task<TResult> LoginAsync<TRequest, TResult>(string url, TRequest request);
        Task<TResult> PostAsync<TRequest, TResult>(string url, TRequest request);
        Task<TResult> PutAsync<TRequest, TResult>(string url, TRequest request);
        Task<TResult> GetAsync<TResult>(string url);
        Task<TResult> GetByIdAsync<TResult>(string url, int id);
        Task<TResult> DeleteAsync<TResult>(string url);
    }

}
