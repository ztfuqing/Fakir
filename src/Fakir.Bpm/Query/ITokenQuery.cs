namespace Fakir.Bpm.Query
{
    public interface ITokenQuery
    {
        ITokenQuery ProcessInstanceId(int id);

        ITokenQuery TokenId(int id);

        ITokenQuery End(bool end);

        ITokenQuery Run(bool running);

        ITokenQuery History(bool history);

        ITokenQuery OrderByTokenId();

        ITokenQuery OrderByProcessInstanceId();
    }
}
