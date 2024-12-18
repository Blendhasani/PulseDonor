namespace PulseDonor.MVC.Helper.Interfaces
{
    public interface IApiClientHelper
    {
        Task<TResult> PostAsync<TRequest, TResult>(string url, TRequest request);
        Task<TResult> PutAsync<TRequest, TResult>(string url, TRequest request);
        Task<TResult> GetAsync<TResult>(string url);
        Task<TResult> DeleteAsync<TResult>(string url);
    }

}
