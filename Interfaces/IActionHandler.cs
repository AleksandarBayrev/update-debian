namespace UpdateDebian.Interfaces
{
    public interface IActionHandler<TVersionResponse>
    {
        Task HandleAsync(TVersionResponse debianVersion);
    }
}