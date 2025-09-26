using UpdateDebian.Models;

namespace UpdateDebian.Interfaces
{
    public interface IDebianVersionParser<TVersionResponse>
    {
        public Task<DebianVersion> ParseVersionAsync(string fileName);
    }
}