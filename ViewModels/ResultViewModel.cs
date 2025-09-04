namespace ApiPerfil.ViewModels
{
    public class ResultViewModel<T>
    {
        public ResultViewModel(T data, List<string> errors)
        {
            Data = data;
            Errors = errors;
        }

        public ResultViewModel(T data) //Se der certo
        {
            Data = data;
        }

        public ResultViewModel(List<string> errors) //Se der errado e for uma lista de erros
        {
            Errors = errors;
        }

        public ResultViewModel(string error) // Se der errado, mas só tiver um erro
        {
            Errors.Add(error);
        }

        public T Data { get; private set; }
        public List<string> Errors { get; private set; } = new();
    }
}